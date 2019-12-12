using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model.Entities;

namespace Model
{
    public class CanteenContext : DbContext
    {
        public DbSet<Reciept> Reciepts { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WarehouseItem> WarehouseItems { get; set; }
        public DbSet<DishName> DishNames { get; set; }
        public DbSet<ProductName> ProductNames { get; set; }

        public CanteenContext() : base("Default")
        {

        }
        public CanteenContext(string connection) : base(connection)
        {

        }
    }
}
