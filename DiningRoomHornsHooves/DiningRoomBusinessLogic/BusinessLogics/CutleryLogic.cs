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
    public class CutleryLogic : ICutleryLogic
    {
        private readonly ICutleryStorage cutleryStorage;
        public CutleryLogic(ICutleryStorage cutleryStorage)
        {
            this.cutleryStorage = cutleryStorage;
        }
        public List<CutleryViewModel> Read(CutleryBindingModel model)
        {
            if (model == null)
            {
                return cutleryStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CutleryViewModel> { cutleryStorage.GetElement(model) };
            }
            return cutleryStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(CutleryBindingModel model)
        {
            if (model.Id.HasValue)
            {
                cutleryStorage.Update(model);
            }
            else
            {
                cutleryStorage.Insert(model);
            }
        }
        public void Delete(CutleryBindingModel model)
        {
            var element = cutleryStorage.GetElement(new CutleryBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элмент не найден");
            }
            cutleryStorage.Delete(model);
        }
    }
}
