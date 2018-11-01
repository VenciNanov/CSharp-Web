using System;
using System.Collections.Generic;
using System.Text;
using CHUSHKA.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace CHUSHKA.Data
{
    public class ChuskaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=.\\SQLEXPRESS;Database=ChushkaDb;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
