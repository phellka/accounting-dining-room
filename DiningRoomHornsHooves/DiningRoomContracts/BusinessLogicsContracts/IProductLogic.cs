using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.BusinessLogicsContracts
{
    public interface IProductLogic
    {
        List<ProductViewModel> Read(ProductBindingModel model);
        void Create();
        void AddCooks();
    }
}
