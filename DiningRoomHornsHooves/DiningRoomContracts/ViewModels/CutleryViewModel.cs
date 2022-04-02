using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DiningRoomContracts.ViewModels
{
    public class CutleryViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название столового прибора")]
        public String Name { get; set; }
        [DisplayName("Количество приборов")]
        public int Count { get; set; }
        public String WorkerLogin { get; set; }
        public int CulteryOrder { get; set; }
    }
}
