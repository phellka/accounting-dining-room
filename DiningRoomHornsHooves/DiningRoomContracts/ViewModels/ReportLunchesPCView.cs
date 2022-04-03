using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.ViewModels
{
    public class ReportLunchesPCView
    {
        public DateTime DateCreate { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public List<CutleryViewModel> Cutleries { get; set; }
        public List<CookViewModel> Cooks { get; set; }
    }
}
