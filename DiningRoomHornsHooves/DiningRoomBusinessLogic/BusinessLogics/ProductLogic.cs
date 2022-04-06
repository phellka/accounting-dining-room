using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.StoragesContracts;
using DiningRoomContracts.BusinessLogicsContracts;
using DiningRoomContracts.BindingModels;

namespace DiningRoomBusinessLogic.BusinessLogics
{
    public class ProductLogic : IProductLogic
    {
        private static List<String> CookingMethods = new List<string> { "варить", "жарить", "парить", "мариновать", "запекать" };
        private static List<String> ProductNames = new List<string> { "мясо", "масло", "сыр", "рыба", "мёд", "паста", "картофель", "морковь", "лук" };
        private static List<String> ProductCountries = new List<string> { "Россия", "Беларусь", "Турция", "Молдова", "Китай", "Бельгия", "Казахстан", "Сербия" };
        private readonly IProductStorage productStorage;
        private readonly ICookStorage cookStorage;
        public ProductLogic(IProductStorage productStorage, ICookStorage cookStorage)
        {
            this.productStorage = productStorage;
            this.cookStorage = cookStorage;
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            if (model == null)
            {
                return productStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ProductViewModel> { productStorage.GetElement(model) };
            }
            return productStorage.GetFilteredList(model);
        }
        public void Create()
        {
            Random rng = new Random();
            productStorage.Insert(new ProductBindingModel { Name = ProductNames[rng.Next(100) % 9], Country = ProductCountries[rng.Next(100) % 8], ManagerLogin = "SystemStorekeeper" });
        }
        public void AddCooks()
        {
            Random rng = new Random();
            var products = productStorage.GetFullList();
            var cooks = cookStorage.GetFullList();
            int productId = products[rng.Next(products.Count())].Id;
            List<int> cooksId = new List<int>();
            int maxLen = rng.Next(cooks.Count());
            for (int i = 0; i < maxLen; ++i)
            {
                int newCookId = cooks.ToList()[rng.Next(cooks.Count())].Id;
                if (!cooksId.Contains(newCookId))
                {
                    cooksId.Add(newCookId);
                }
            }
            String method = CookingMethods[rng.Next(100) % 5];
            productStorage.AddCooks(new AddedProductCooksBindingModel() { 
                ProductId = productId,
                CooksId = cooksId,
                Method = method
            });
        }
    }
}
