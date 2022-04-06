using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomContracts.ViewModels;

namespace DiningRoomContracts.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateAfter { get; set; }
        public DateTime? DateBefore { get; set; }
        public List<LunchViewModel>? lunches { get; set; }
        public String VisitorLogin { get; set; }
    }
}
