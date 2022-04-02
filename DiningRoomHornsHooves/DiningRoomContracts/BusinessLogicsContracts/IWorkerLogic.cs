using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.BusinessLogicsContracts
{
    public interface IWorkerLogic
    {
        void Create(WorkerBindingModel model);
        bool Login(WorkerBindingModel model);
    }
}
