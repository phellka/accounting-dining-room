using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.BusinessLogicsContracts
{
    public interface ICutleryLogic
    {
        List<CutleryViewModel> Read(CutleryBindingModel model);
        void CreateOrUpdate(CutleryBindingModel model);
        void Delete(CutleryBindingModel model);
    }
}
