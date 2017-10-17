using System.IO;
using System.Linq;
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
            optionsBuilder.UseMySql("server=localhost;database=northwind;uid="+ lines[0] + ";pwd="+ lines[1]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        }
    }
}
