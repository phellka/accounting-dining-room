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
            return context.Cooks.Where(rec => rec.Name.Contains(model.Name)).Select(CreateModel).ToList();
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
        public void Insert()
        {
            using var context = new DiningRoomDatabase();
            string newName = GenerateName();
            context.Cooks.Add(new Cook { Name = newName, ManagerLogin = "SystemStorekeeper"});
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
        public static string GenerateName()
        {
            Random r = new Random();
            int len = r.Next(2) + 2;
            len *= 2;
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }
            return Name;
        }
    }
}
