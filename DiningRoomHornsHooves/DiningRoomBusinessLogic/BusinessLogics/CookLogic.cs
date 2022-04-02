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
        public void Create()
        {
            cookStorage.Insert();
        }
    }
}
