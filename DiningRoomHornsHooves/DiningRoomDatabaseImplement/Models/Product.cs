using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Country { get; set; }
        public String ManagerLogin { get; set; }
        public virtual Manager Manager { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<LunchProducts> LunchProducts { get; set; }
        [ForeignKey("ProductId")]
        public virtual List<ProductCooks> ProductCooks { get; set; }
    }
}
