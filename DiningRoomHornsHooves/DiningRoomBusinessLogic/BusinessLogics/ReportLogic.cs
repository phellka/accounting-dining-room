using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.BusinessLogicsContracts;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.StoragesContracts;
using DiningRoomContracts.BindingModels;
using DiningRoomBusinessLogic.OfficePackage;
using DiningRoomBusinessLogic.OfficePackage.HelperEnums;
using DiningRoomBusinessLogic.OfficePackage.HelperModels;

namespace DiningRoomBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly ILunchStorage lunchStorage;
        private readonly ICutleryStorage cutleryStorage;
        private readonly ICookStorage cookStorage;
        private readonly IProductStorage productStorage;
        private readonly IVisitorStorage workerStorage;
        private readonly AbstractSaveToPdf saveToPdf;
        private readonly AbstractSaveToWord saveToWord;
        private readonly AbstractSaveToExcel saveToExcel;
        public ReportLogic(ILunchStorage lunchStorage, ICutleryStorage cutleryStorage, ICookStorage cookStorage, IProductStorage productStorage,
            IVisitorStorage workerStorage, AbstractSaveToPdf saveToPdf, AbstractSaveToWord saveToWord, AbstractSaveToExcel saveToExcel)
        {
            this.cookStorage = cookStorage;
            this.cutleryStorage = cutleryStorage;
            this.lunchStorage = lunchStorage;
            this.productStorage = productStorage;
            this.workerStorage = workerStorage;
            this.saveToPdf = saveToPdf;
            this.saveToWord = saveToWord;
            this.saveToExcel = saveToExcel;
        }
        public List<ReportLunchesPCView> GetLunchesPCView(ReportBindingModel model)
        {
            var list = new List<ReportLunchesPCView>();
            var lunches = lunchStorage.GetFilteredList(new LunchBindingModel
            {
                after = model.DateAfter,
                before = model.DateBefore
            });
            foreach (var lunch in lunches)
            {
                var record = new ReportLunchesPCView
                {
                    DateCreate = lunch.Date,
                    Price = lunch.Price,
                    Weight = lunch.Weight,
                    Cooks = new List<CookViewModel>(),
                    Cutleries = new List<CutleryViewModel>()
                };
                var lunchProducts = lunch.LunchProduts.Keys.ToList().Select(rec => productStorage.GetElement(new ProductBindingModel { Id = rec }));
                var listCookIds = new List<int>();
                foreach (var elem in lunchProducts)
                {
                    listCookIds.AddRange(elem.ProductCooks.Keys.ToList());
                }
                record.Cooks = listCookIds.Distinct().ToList().Select(rec => cookStorage.GetElement(new CookBindingModel { Id = rec })).ToList();
                var lunchOrders = lunch.LunchOrders.Keys.ToList();
                var lunchCutleries = cutleryStorage.GetFullList().Where(rec => lunchOrders.Contains(rec.CulteryOrder)).ToList();
                record.Cutleries = lunchCutleries;
                list.Add(record);
            }
            return list;
        }
        public void saveLunchesToPdfFile(ReportBindingModel model)
        {
            var worker = workerStorage.GetAutorizedWorker();
            saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateAfter = model.DateAfter.Value,
                DateBefore = model.DateBefore.Value,
                Lunches = GetLunchesPCView(model)
            });
        }
        public List<CookViewModel> GetCooksByLanches(ReportBindingModel model)
        {
            var list = new List<CookViewModel>();
            var listCookIds = new List<int>();
            foreach (var lunch in model.lunches)
            {
                var lunchProducts = lunch.LunchProduts.Keys.ToList().Select(rec => productStorage.GetElement(new ProductBindingModel { Id = rec }));
                foreach (var elem in lunchProducts)
                {
                    listCookIds.AddRange(elem.ProductCooks.Keys.ToList());
                }
            }
            list = listCookIds.Distinct().ToList().Select(rec => cookStorage.GetElement(new CookBindingModel { Id = rec })).ToList();
            return list;
        }
        public void saveCooksToExcel(ReportBindingModel model)
        {
            saveToExcel.CreateReport(new ExcelInfo()
            {
                FileName = model.FileName,
                Title = "Список поваров:",
                Cooks = GetCooksByLanches(model)
            });
        }
        public void saveCooksToWord(ReportBindingModel model)
        {
            saveToWord.CreateDoc(new WordInfo()
            {
                FileName = model.FileName,
                Title = "Список поваров",
                Cooks = GetCooksByLanches(model)
            });
        }
    }
}   
