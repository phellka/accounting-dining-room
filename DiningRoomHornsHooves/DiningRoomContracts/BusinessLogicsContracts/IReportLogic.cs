using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;
using DiningRoomContracts.BindingModels;

namespace DiningRoomContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<CookViewModel> GetCooksByLanches(ReportBindingModel model);
        List<ReportLunchesPCView> GetLunchesPCView(ReportBindingModel model);
        void saveLunchesToPdfFile(ReportBindingModel model);
        void saveCooksToWord(ReportBindingModel model);
        void saveCooksToExcel(ReportBindingModel model);
    }
}
