using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.BindingModels
{
    public class LunchBindingModel
    {
        public  int? Id { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public DateTime Date { get; set; }
        public string WorkerLogin { get; set; }
        public Dictionary<int, int> LunchOrders { get; set; }
        public Dictionary<int, int> LunchProduts { get; set; }
        public DateTime? after { get; set; }
        public DateTime? before { get; set; }
    }
}
