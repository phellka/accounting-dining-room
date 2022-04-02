using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.BindingModels;
using DiningRoomContracts.ViewModels;

namespace DiningRoomContracts.StoragesContracts
{
    public interface ILunchStorage
    {
        List<LunchViewModel> GetFullList();
        List<LunchViewModel> GetFilteredList(LunchBindingModel model);
        LunchViewModel GetElement(LunchBindingModel model);
        void Insert(LunchBindingModel model);
        void Update(LunchBindingModel model);
        void Delete(LunchBindingModel model);
        void AddOrder((int, (int, int)) addedOrder);
    }
}
