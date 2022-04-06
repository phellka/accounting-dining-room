using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.BusinessLogicsContracts
{
    public interface ILunchLogic
    {
        List<LunchViewModel> Read(LunchBindingModel model);
        void CreateOrUpdate(LunchBindingModel model);
        void Delete(LunchBindingModel model);
        void AddOrder(AddLunchOrderBindingModel addedOrder);
    }
}
