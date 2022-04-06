using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.BindingModels
{
    public class AddLunchOrderBindingModel
    {
        public int LunchId { get; set; }
        public int OrderId { get; set; }
        public int OrderCount { get; set; }
        public string VisitorLogin { get; set; }
    }
}
