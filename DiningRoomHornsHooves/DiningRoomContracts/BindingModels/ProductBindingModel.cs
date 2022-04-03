using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.BindingModels
{
    public class ProductBindingModel
    {
        public int? Id { get; set; }
        public String Name { get; set; }
        public String Country { get; set; }
        public String ManagerLogin { get; set; }
        public Dictionary<int, string> ProductCooks { get; set; }
    }
}
