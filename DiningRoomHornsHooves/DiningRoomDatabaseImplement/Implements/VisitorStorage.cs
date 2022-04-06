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
    public class VisitorStorage : IVisitorStorage
    {
        public bool Registered(VisitorBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            if (context.Visitors.FirstOrDefault(rec => rec.Login == model.Login || rec.Email == model.Email) != null)
            {
                return true;
            }
            return false;
        }
        public void Insert(VisitorBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            context.Visitors.Add(new Visitor { Login = model.Login, Password = model.Password, Email = model.Email, Name = model.Name});
            context.SaveChanges();
        }
        public bool Login(VisitorBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            if (!context.Visitors.Contains(new Visitor { Login = model.Login, Password = model.Password })) { 
                return false;
            }
            if (!context.Visitors.Select(rec => rec.Login).Contains(model.Login))
            {
                return false;
            }
            if (context.Visitors.FirstOrDefault(rec => rec.Login == model.Login).Password != model.Password)
            {
                return false;
            }
            return true;
        }
        public VisitorBindingModel GetElement(VisitorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            var worker = context.Visitors.FirstOrDefault(rec => rec.Login == model.Login);
            return worker != null ? CreateModel(worker) : null;
        }
        public VisitorBindingModel CreateModel(Visitor worker)
        {
            return new VisitorBindingModel()
            {
                Email = worker.Email,
                Login = worker.Login,
                Password = worker.Password,
                Name = worker.Name
            };
        }
    }
}
