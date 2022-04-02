using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.StoragesContracts
{
    public interface IWorkerStorage
    {
        WorkerBindingModel GetAutorizedWorker();
        bool Registered(WorkerBindingModel model);
        void Insert(WorkerBindingModel model);
        bool Login(WorkerBindingModel model);
    }
}
