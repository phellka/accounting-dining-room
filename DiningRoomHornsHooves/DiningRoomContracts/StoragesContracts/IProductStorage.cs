using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.BindingModels;
using DiningRoomContracts.ViewModels;

namespace DiningRoomContracts.StoragesContracts
{
    public interface IProductStorage
    {
        List<ProductViewModel> GetFullList();
        List<ProductViewModel> GetFilteredList(ProductBindingModel model);
        ProductViewModel GetElement(ProductBindingModel model);
        void Insert(ProductBindingModel model);
        void AddCooks(AddedProductCooksBindingModel addedProductCooks);
    }
}
