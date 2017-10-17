using System.IO;
using System.Linq;
using DBMapper.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DBMapper
{
    class NorthwindContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var lines = File.ReadLines("credentials.txt").ToArray(); //Use credential file since we have different passwords on our DB servers
            optionsBuilder.UseMySql("server=localhost;database=northwind;uid=" + lines[0] +";" + "pwd= "+ lines[1]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Category
            modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");

            //Order
            modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("orderdate");
            modelBuilder.Entity<Order>().Property(x => x.Required).HasColumnName("requireddate");
            modelBuilder.Entity<Order>().Property(x => x.Shipped).HasColumnName("shippeddate");

            //Product

            //OrderDetails
        }
    }
}
