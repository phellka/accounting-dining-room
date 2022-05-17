using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.StoragesContracts
{
    public interface IVisitorStorage
    {
        bool Registered(VisitorBindingModel model);
        void Insert(VisitorBindingModel model);
        bool Login(VisitorBindingModel model);
        VisitorBindingModel GetVisitorData(VisitorBindingModel model);
    }
}
