using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.BusinessLogicsContracts
{
    public interface IVisitorLogic
    {
        void Create(VisitorBindingModel model);
        bool Login(VisitorBindingModel model);
        VisitorBindingModel GetVisitorData(VisitorBindingModel model);
    }
}
