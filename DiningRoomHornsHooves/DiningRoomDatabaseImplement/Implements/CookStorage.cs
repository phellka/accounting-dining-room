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
    public class CookStorage : ICookStorage
    {
        public List<CookViewModel> GetFullList()
        {
            using var context = new DiningRoomDatabase();
            return context.Cooks.Select(CreateModel).ToList();
        }
        public List<CookViewModel> GetFilteredList(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            return context.Cooks.Where(rec => 
                !String.IsNullOrEmpty(model.ManagerLogin) && rec.ManagerLogin == model.ManagerLogin)
                .Select(CreateModel).ToList();
        }
        public CookViewModel GetElement(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            var cook = context.Cooks.FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return cook != null ? CreateModel(cook) : null;
        }
        public void Insert(CookBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            context.Cooks.Add(new Cook { Name = model.Name, ManagerLogin = model.ManagerLogin});
            context.SaveChanges();
        }
        private static CookViewModel CreateModel(Cook cook)
        {
            return new CookViewModel
            {
                Id = cook.Id,
                Name = cook.Name,
                ManagerLogin = cook.ManagerLogin
            };
        }
        
    }
}
