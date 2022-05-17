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
    public class VisitorLogic : IVisitorLogic
    {
        private readonly IVisitorStorage workerStorage;
        public VisitorLogic(IVisitorStorage workerStorage)
        {
            this.workerStorage = workerStorage;
        }
        public void Create(VisitorBindingModel model)
        {
            if (!workerStorage.Registered(model))
            {
                workerStorage.Insert(model);
            }
            else
            {
                throw new Exception("Работник с таким логином или почтой уже существует");
            }
        }
        public Boolean Login(VisitorBindingModel model)
        {
            return workerStorage.Login(model);
        }
        public VisitorBindingModel GetVisitorData(VisitorBindingModel model)
        {
            return workerStorage.GetVisitorData(model);
        }
    }
}
