using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DiningRoomDatabaseImplement.Models
{
    public class LunchProducts
    {
        public int Id { get; set; }
        public int LunchId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public int ProductCount { get; set; }
        public virtual Lunch Lunch { get; set; }
        public virtual Product Product { get; set; }
    }
}
