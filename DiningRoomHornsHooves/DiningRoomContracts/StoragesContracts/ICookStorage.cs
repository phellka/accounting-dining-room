using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.StoragesContracts
{
    public interface ICookStorage
    {
        List<CookViewModel> GetFullList();
        List<CookViewModel> GetFilteredList(CookBindingModel model);
        CookViewModel GetElement(CookBindingModel model);
        void Insert();
    }
}
