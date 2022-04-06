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
    public class CookLogic : ICookLogic
    {
        private readonly ICookStorage cookStorage;
        public CookLogic(ICookStorage cookStorage)
        {
            this.cookStorage = cookStorage;
        }
        public List<CookViewModel> Read(CookBindingModel model)
        {
            if (model == null)
            {
                return cookStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CookViewModel> { cookStorage.GetElement(model) };
            }
            return cookStorage.GetFilteredList(model);
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
        public void Create()
        {
            string newName = GenerateName();
            cookStorage.Insert(new CookBindingModel { Name = newName, ManagerLogin = "SystemStorekeeper" });
        }
    }
}
