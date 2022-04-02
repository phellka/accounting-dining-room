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
    public class WorkerStorage : IWorkerStorage
    {
        public static string AutorizedWorker;
        public bool Registered(WorkerBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            if (context.Workers.FirstOrDefault(rec => rec.Login == model.Login || rec.Email == model.Email) != null)
            {
                return true;
            }
            return false;
        }
        public void Insert(WorkerBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            context.Workers.Add(new Worker { Login = model.Login, Password = model.Password, Email = model.Email, Name = model.Name});
            context.SaveChanges();
        }
        public bool Login(WorkerBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            if (!context.Workers.Contains(new Worker { Login = model.Login, Password = model.Password })) { 
                return false;
            }
            if (!context.Workers.Select(rec => rec.Login).Contains(model.Login))
            {
                return false;
            }
            if (context.Workers.FirstOrDefault(rec => rec.Login == model.Login).Password != model.Password)
            {
                return false;
            }
            AutorizedWorker = model.Login;
            return true;
        }
        public WorkerBindingModel GetAutorizedWorker()
        {
            return GetElement(new WorkerBindingModel() { Login = AutorizedWorker });
        }
        public WorkerBindingModel GetElement(WorkerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            var worker = context.Workers.FirstOrDefault(rec => rec.Login == model.Login);
            return worker != null ? CreateModel(worker) : null;
        }
        public WorkerBindingModel CreateModel(Worker worker)
        {
            return new WorkerBindingModel()
            {
                Email = worker.Email,
                Login = worker.Login,
                Password = worker.Password,
                Name = worker.Name
            };
        }
    }
}
