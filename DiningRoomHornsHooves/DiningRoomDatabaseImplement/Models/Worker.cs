using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Worker
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
        [ForeignKey("WorkerLogin")]
        public virtual List<Order> Orders { get; set; }
        [ForeignKey("WorkerLogin")]
        public virtual List<Lunch> Lunches { get; set; }
        [ForeignKey("WorkerLogin")]
        public virtual List<Cutlery> Cutleries { get; set; }
    }
}
