using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.BindingModels;
using DiningRoomContracts.ViewModels;

namespace DiningRoomContracts.StoragesContracts
{
    public interface IOrderStorage
    {
        List<OrderViewModel> GetFullList();
        List<OrderViewModel> GetFilteredList(OrderBindingModel model);
        OrderViewModel GetElement(OrderBindingModel model);
        void Insert(OrderBindingModel model);
        void Update(OrderBindingModel model);
        void Delete(OrderBindingModel model);

    }
}
