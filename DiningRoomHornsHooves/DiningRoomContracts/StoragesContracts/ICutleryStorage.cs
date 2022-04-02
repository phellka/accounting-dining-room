using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.BindingModels;
using DiningRoomContracts.ViewModels;

namespace DiningRoomContracts.StoragesContracts
{
    public interface ICutleryStorage
    {
        List<CutleryViewModel> GetFullList();
        List<CutleryViewModel> GetFilteredList(CutleryBindingModel model);
        CutleryViewModel GetElement(CutleryBindingModel model);
        void Insert(CutleryBindingModel model);
        void Update(CutleryBindingModel model);
        void Delete(CutleryBindingModel model);
    }
}
