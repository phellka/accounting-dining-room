using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int Calorie { get; set; }
        [Required]
        public String Wishes { get; set; }
        public string WorkerLogin { get; set; }
        public virtual Worker Worker { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<LunchOrders> LunchOrders { get; set; }
    }
}
