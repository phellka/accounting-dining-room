using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.BindingModels
{
    public class CutleryBindingModel
    {
        public int? Id { get; set; }
        public String Name { get; set; }
        public int Count { get; set; }
        public String VisitorLogin { get; set; }
        public int CulteryOrder { get; set; }
    }
}
