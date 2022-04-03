using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace DiningRoomDatabaseImplement
{
    public class DiningRoomDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-HJ4MGBC;Initial Catalog=DiningRoomBasedDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Cook> Cooks { get; set; }
        public virtual DbSet<Cutlery> Cutleries { get; set; }
        public virtual DbSet<Lunch> Lunches { get; set; }
        public virtual DbSet<LunchOrders> LunchOrders { get; set; }
        public virtual DbSet<LunchProducts> LunchProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCooks> ProductCooks { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
    }
}
