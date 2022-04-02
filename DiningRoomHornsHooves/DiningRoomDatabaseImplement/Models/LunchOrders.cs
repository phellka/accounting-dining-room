using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DiningRoomDatabaseImplement.Models
{
    public class LunchOrders
    {
        public int Id { get; set; }
        public int LunchId { get; set; }
        public int OrderId { get; set; }
        [Required]
        public int OrderCount { get; set; }
        public virtual Lunch Lunch { get; set; }
        public virtual Order Order { get; set; }
    }
}
