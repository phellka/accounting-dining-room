using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Cook
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public String ManagerLogin { get; set; }
        public virtual Manager Manager { get; set; }

        [ForeignKey("CookId")]
        public virtual List<ProductCooks> ProductCooks { get; set; }
    }
}
