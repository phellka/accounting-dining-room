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
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new DiningRoomDatabase();
            return context.Orders.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            return context.Orders.Where(rec => rec.Calorie == model.Calorie && rec.VisitorLogin == VisitorStorage.AutorizedWorker).Select(CreateModel).ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            var order = context.Orders.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            model.VisitorLogin = VisitorStorage.AutorizedWorker;
            context.Orders.Add(CreateModel(model, new Order()));
            context.SaveChanges();
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            var element = context.Orders.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            Order element = context.Orders.Where(rec => rec.VisitorLogin == VisitorStorage.AutorizedWorker).FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.Calorie = model.Calorie;
            order.Wishes = model.Wishes;
            order.VisitorLogin = VisitorStorage.AutorizedWorker;
            return order;
        }
        private static OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Calorie = order.Calorie,
                Wishes = order.Wishes,
                VisitorLogin = order.VisitorLogin
            };
        }
    }
}
