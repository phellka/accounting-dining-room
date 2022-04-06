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
    public class LunchLogic : ILunchLogic
    {
        private readonly ILunchStorage lunchStorage;
        private readonly IOrderStorage orderStorage;
        public LunchLogic(ILunchStorage lunchStorage, IOrderStorage orderStorage)
        {
            this.lunchStorage = lunchStorage;
            this.orderStorage = orderStorage;
        }
        public List<LunchViewModel> Read(LunchBindingModel model)
        {
            if (model == null)
            {
                return lunchStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<LunchViewModel> { lunchStorage.GetElement(model) };
            }
            return lunchStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(LunchBindingModel model)
        {
            if (model.Id.HasValue)
            {
                lunchStorage.Update(model);
            }
            else
            {
                lunchStorage.Insert(model);
            }
        }
        public void Delete(LunchBindingModel model)
        {
            var element = lunchStorage.GetElement(model);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            lunchStorage.Delete(model);
        }
        public void AddOrder(AddLunchOrderBindingModel addedOrder)
        {
            var lunch = lunchStorage.GetElement(new LunchBindingModel { Id = addedOrder.LunchId, VisitorLogin = addedOrder.VisitorLogin });
            if (lunch == null)
            {
                throw new Exception("Обед не найден");
            }
            var order = orderStorage.GetElement(new OrderBindingModel { Id = addedOrder.OrderId, VisitorLogin = addedOrder.VisitorLogin });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            lunchStorage.AddOrder(addedOrder);
        }
    }
}
