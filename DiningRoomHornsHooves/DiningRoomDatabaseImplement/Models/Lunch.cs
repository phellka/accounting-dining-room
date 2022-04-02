using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Lunch
    {
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string WorkerLogin { get; set; }
        public virtual Worker Worker { get; set; }

        [ForeignKey("LunchId")]
        public virtual List<LunchOrders> LunchOrders { get; set; }
        [ForeignKey("LunchId")]
        public virtual List<LunchProducts> LunchProducts { get; set; }
    }
}
