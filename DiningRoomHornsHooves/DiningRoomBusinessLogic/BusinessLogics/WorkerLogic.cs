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
    public class WorkerLogic : IWorkerLogic
    {
        private readonly IWorkerStorage workerStorage;
        public WorkerLogic(IWorkerStorage workerStorage)
        {
            this.workerStorage = workerStorage;
        }
        public void Create(WorkerBindingModel model)
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
        public Boolean Login(WorkerBindingModel model)
        {
            return workerStorage.Login(model);
        }
    }
}
