using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DiningRoomContracts.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [DisplayName("Каллорийность заказа")]
        public int Calorie { get; set; }
        [DisplayName("Пожелания к заказу")]
        public String Wishes { get; set; }
        public string VisitorLogin { get; set; }
        override
        public string ToString()
        {
            return String.Format(@"Id = {0}, Калорийность = {1}, Пожелания = {2}", Id, Calorie, Wishes);
        }
    }
}
