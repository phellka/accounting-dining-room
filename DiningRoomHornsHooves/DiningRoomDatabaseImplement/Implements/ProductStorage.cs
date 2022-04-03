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
    public class ProductStorage : IProductStorage
    {
        private static List<String> CookingMethods = new List<string> { "варить", "жарить", "парить", "мариновать", "запекать" };
        private static List<String> ProductNames = new List<string> { "мясо", "масло", "сыр", "рыба", "мёд", "паста", "картофель", "морковь", "лук" };
        private static List<String> ProductCountries = new List<string> { "Россия", "Беларусь", "Турция", "Молдова", "Китай", "Бельгия", "Казахстан", "Сербия"};
        public List<ProductViewModel> GetFullList()
        {
            using var context = new DiningRoomDatabase();
            var list = context.Products.ToList();
            return context.Products.Include(rec => rec.LunchProducts).Include(rec => rec.ProductCooks).Select(CreateModel).ToList();
        }
        public List<ProductViewModel> GetFilteredList(ProductBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            return context.Products.Where(rec => rec.Name.Contains(model.Name)).Include(rec => rec.LunchProducts).Include(rec => rec.ProductCooks).Select(CreateModel).ToList();
        }
        public ProductViewModel GetElement(ProductBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new DiningRoomDatabase();
            var product = context.Products.Include(rec => rec.LunchProducts).Include(rec => rec.ProductCooks).FirstOrDefault(rec => rec.Id == model.Id);
            return product != null ? CreateModel(product) : null;
        }
        public void Insert()
        {
            Random rng = new Random();
            using var context = new DiningRoomDatabase();
            context.Products.Add(new Product { Name = ProductNames[rng.Next(100) % 9], Country = ProductCountries[rng.Next(100) % 8], ManagerLogin = "SystemStorekeeper" });
            context.SaveChanges();
        }
        public void AddCooks(){
            Random rng = new Random();
            using var context = new DiningRoomDatabase();
            (int, List<int>) addedCooks = (new int(), new List<int>());
            addedCooks.Item1 = context.Products.ToList()[rng.Next(context.Products.Count())].Id;
            addedCooks.Item2 = new List<int>();
            int maxLen = rng.Next(context.Cooks.Count());
            for (int i = 0; i < maxLen; ++i)
            {
                int newCookId = context.Cooks.ToList()[rng.Next(context.Cooks.Count())].Id;
                if (!addedCooks.Item2.Contains(newCookId))
                {
                    addedCooks.Item2.Add(newCookId);
                }
            }
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Product product = context.Products.FirstOrDefault(rec => rec.Id == addedCooks.Item1);
                foreach(int cookId in addedCooks.Item2)
                {
                    List<ProductCooks> textList = context.ProductCooks.ToList();
                    if (!context.ProductCooks.Where(rec => rec.ProductId == addedCooks.Item1).Select(rec => rec.CookId).ToList().Contains(cookId))
                    {
                        context.ProductCooks.Add(new ProductCooks { ProductId = addedCooks.Item1, CookId = cookId, Method = CookingMethods[rng.Next(100) % 5] });
                    }
                }
                transaction.Commit();
                context.SaveChanges();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public static ProductViewModel CreateModel(Product product)
        {
            return new ProductViewModel {
                Id = product.Id,
                Name = product.Name,
                Country = product.Country,
                ManagerLogin = product.ManagerLogin,
                ProductCooks = product.ProductCooks.ToDictionary(rec => rec.CookId, rec => rec.Method)
            };
        }
    }
}
