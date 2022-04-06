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
            return context.Products.Where(rec => 
                !String.IsNullOrEmpty(model.ManagerLogin) && rec.ManagerLogin == model.ManagerLogin)
                .Include(rec => rec.LunchProducts).Include(rec => rec.ProductCooks).Select(CreateModel).ToList();
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
        public void Insert(ProductBindingModel model)
        {
            using var context = new DiningRoomDatabase();
            context.Products.Add(new Product { Name = model.Name, Country = model.Country, ManagerLogin = model.ManagerLogin });
            context.SaveChanges();
        }
        public void AddCooks(AddedProductCooksBindingModel addedProductCooks)
        {
            using var context = new DiningRoomDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                foreach(int cookId in addedProductCooks.CooksId)
                {
                    List<ProductCooks> textList = context.ProductCooks.ToList();
                    if (!context.ProductCooks.Where(rec => rec.ProductId == addedProductCooks.ProductId).Select(rec => rec.CookId).ToList().Contains(cookId))
                    {
                        context.ProductCooks.Add(new ProductCooks { ProductId = addedProductCooks.ProductId, CookId = cookId, Method = addedProductCooks.Method});
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
