using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Visitor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]  
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("VisitorLogin")]
        public virtual List<Order> Orders { get; set; }
        [ForeignKey("VisitorLogin")]
        public virtual List<Lunch> Lunches { get; set; }
        [ForeignKey("VisitorLogin")]
        public virtual List<Cutlery> Cutleries { get; set; }
    }
}
