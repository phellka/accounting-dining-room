using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public int Calorie { get; set; }
        public String Wishes { get; set; }
        public string WorkerLogin { get; set; }
    }
}
