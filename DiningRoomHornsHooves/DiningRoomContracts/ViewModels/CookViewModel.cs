using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DiningRoomContracts.ViewModels
{
    public class CookViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя повара")]
        public string Name { get; set; }
        public String StorekeeperLogin { get; set; }
    }
}
