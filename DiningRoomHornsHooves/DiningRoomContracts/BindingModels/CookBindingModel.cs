using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.BindingModels
{
    public class CookBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public String StorekeeperLogin { get; set; }
    }
}
