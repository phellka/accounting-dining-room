using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DiningRoomDatabaseImplement.Models
{
    public class ProductCooks
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CookId { get; set; }
        [Required]
        public String Method { get; set; }
        public virtual Product Product { get; set; }
        public virtual Cook Cook { get; set; }
    }
}
