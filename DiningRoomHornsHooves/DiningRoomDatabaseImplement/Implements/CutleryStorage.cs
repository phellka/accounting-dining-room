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
    public class CutleryStorage : ICutleryStorage
    {
        public List<CutleryViewModel> GetFullList()
        {
            using var context = new DiningRoomDatabase();
            return context.Cutleries.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public List<CutleryViewModel> GetFilteredList(CutleryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            return context.Cutleries.Where(rec => rec.OrderId == model.CulteryOrder && rec.VisitorLogin == VisitorStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public CutleryViewModel GetElement(CutleryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            var cutlery = context.Cutleries.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            return cutlery != null ? CreateModel(cutlery) : null;
        }
        public void Insert(CutleryBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Cutlery cutlery = new Cutlery()
                {
                    Name = model.Name,
                    Count = model.Count,
                    VisitorLogin = VisitorStorage.AutorizedWorker,
                    OrderId = model.CulteryOrder,
                    Order = context.Orders.FirstOrDefault(rec => rec.Id == model.CulteryOrder)
                };
                context.Cutleries.Add(cutlery);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(CutleryBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Cutleries.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.Name = model.Name;
                element.Count = model.Count;
                element.OrderId = model.CulteryOrder;
                element.Order = context.Orders.FirstOrDefault(rec => rec.Id == model.CulteryOrder);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(CutleryBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            Cutlery element = context.Cutleries.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Cutleries.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static CutleryViewModel CreateModel(Cutlery cutlery)
        {
            return new CutleryViewModel
            {
                Id = cutlery.Id,
                Name = cutlery.Name,
                Count = cutlery.Count,
                VisitorLogin = cutlery.VisitorLogin,
                CulteryOrder = cutlery.OrderId
            };
        }
    }
}
