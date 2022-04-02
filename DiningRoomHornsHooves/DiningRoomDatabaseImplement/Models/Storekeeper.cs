using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningRoomDatabaseImplement.Models
{
    public class Storekeeper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("StorekeeperLogin")]
        public virtual List<Product> Products { get; set; }
        [ForeignKey("StorekeeperLogin")]
        public virtual List<Cook> Cooks { get; set; }
    }
}
