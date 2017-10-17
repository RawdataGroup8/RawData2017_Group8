using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TestADO_livecode
{
    class NorthwindContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var lines = File.ReadLines("credentials.txt").ToArray();
            var username = lines[0];
            var password = lines[1];
            optionsBuilder.UseMySql("server=localhost;database=northwind;uid="+username+";pwd="+password);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        }
    }
}
