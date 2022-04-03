using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Cutlery
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public int Count { get; set; }
        public String VisitorLogin { get; set; }
        public virtual Visitor Visitor { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
