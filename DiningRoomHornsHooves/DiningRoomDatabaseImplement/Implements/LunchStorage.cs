using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomDatabaseImplement.Models;
using DiningRoomContracts.BindingModels;
using DiningRoomContracts.StoragesContracts;
using DiningRoomContracts.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DiningRoomDatabaseImplement.Implements
{
    public class LunchStorage : ILunchStorage
    {
        public List<LunchViewModel> GetFullList()
        {
            using var context = new DiningRoomDatabase();
            return context.Lunches.Include(rec => rec.LunchProducts).Include(rec => rec.LunchOrders).Where(rec => rec.WorkerLogin == WorkerStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public List<LunchViewModel> GetFilteredList(LunchBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            return context.Lunches.Include(rec => rec.LunchProducts).Include(rec => rec.LunchOrders).Where(rec => rec.Id == model.Id && rec.WorkerLogin == WorkerStorage.AutorizedWorker ||
                rec.Date > model.after && rec.Date < model.before && rec.WorkerLogin == WorkerStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public LunchViewModel GetElement(LunchBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            var lunch = context.Lunches.Include(rec => rec.LunchProducts).Include(rec => rec.LunchOrders).Where(rec => rec.WorkerLogin == WorkerStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            return lunch != null ? CreateModel(lunch) : null;
        }
        public void Insert(LunchBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Lunch lunch = new Lunch()
                {
                    Price = model.Price,
                    Date = model.Date,
                    Weight = model.Weight,
                    WorkerLogin = WorkerStorage.AutorizedWorker
                };
                context.Lunches.Add(lunch);
                context.SaveChanges();
                CreateModel(model, lunch, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(LunchBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Lunches.Include(rec => rec.LunchProducts).Where(rec => rec.WorkerLogin == WorkerStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(LunchBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            Lunch element = context.Lunches.Include(rec => rec.LunchProducts).Where(rec => rec.WorkerLogin == WorkerStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Lunches.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void AddOrder((int, (int, int)) addedOrder)
        {
            using var context = new DiningRoomDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (context.LunchOrders.FirstOrDefault(rec => rec.LunchId == addedOrder.Item1 && rec.OrderId == addedOrder.Item2.Item1) != null)
                {
                    var lunchOrder = context.LunchOrders.FirstOrDefault(rec => rec.LunchId == addedOrder.Item1 && rec.OrderId == addedOrder.Item2.Item1);
                    lunchOrder.OrderCount = addedOrder.Item2.Item2;
                }
                else
                {
                    context.LunchOrders.Add(new LunchOrders { LunchId = addedOrder.Item1, OrderId = addedOrder.Item2.Item1, OrderCount = addedOrder.Item2.Item2 });
                }
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private static Lunch CreateModel(LunchBindingModel model, Lunch lunch, DiningRoomDatabase context)
        {
            lunch.Price = model.Price;
            lunch.Weight = model.Weight;
            lunch.Date = model.Date;
            if (model.Id.HasValue)
            {
                var lunchProducts = context.LunchProducts.Where(rec => rec.LunchId == model.Id.Value).ToList();
                context.LunchProducts.RemoveRange(lunchProducts.Where(rec => !model.LunchProduts.ContainsKey(rec.ProductId)).ToList());
                context.SaveChanges();
                lunchProducts = context.LunchProducts.Where(rec => rec.LunchId == model.Id.Value).ToList();
                foreach (var updateProducts in lunchProducts)
                {
                    updateProducts.ProductCount = model.LunchProduts[updateProducts.ProductId];
                    model.LunchProduts.Remove(updateProducts.ProductId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.LunchProduts)
            {
                context.LunchProducts.Add(new LunchProducts
                {
                    LunchId = lunch.Id,
                    ProductId = pc.Key,
                    ProductCount = pc.Value
                });
                context.SaveChanges();
            }
            return lunch;
        }
        private static LunchViewModel CreateModel(Lunch lunch)
        {
            return new LunchViewModel
            {
                Id = lunch.Id,
                Date = lunch.Date,
                Price = lunch.Price,
                Weight = lunch.Weight,
                WorkerLogin = lunch.WorkerLogin,
                LunchProduts = lunch.LunchProducts.ToDictionary(rec => rec.ProductId, rec => rec.ProductCount),
                LunchOrders = lunch.LunchOrders.ToDictionary(rec => rec.OrderId, rec => rec.OrderCount)
            };
        }
    }
}
