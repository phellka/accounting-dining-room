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
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            this.orderStorage = orderStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { orderStorage.GetElement(model) };
            }
            return orderStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            if (model.Id.HasValue)
            {
                orderStorage.Update(model);
            }
            else
            {
                orderStorage.Insert(model);
            }
        }
        public void Delete(OrderBindingModel model)
        {
            var element = orderStorage.GetElement(model);
            if (element == null)
            {
                throw new Exception("Элмент не найден");
            }
            orderStorage.Delete(model);
        }
    }
}
