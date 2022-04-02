using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DiningRoomContracts.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название продукта")]
        public String Name { get; set; }
        [DisplayName("Страна происхождения")]
        public String Country { get; set; }
        public String StorekeeperLogin { get; set; }
        public Dictionary<int, string> ProductCooks { get; set; }
        override
        public string ToString()
        {
            return String.Format(@"Название = {0}, Происхождение = {1}", Name, Country);
        }
    }
}
