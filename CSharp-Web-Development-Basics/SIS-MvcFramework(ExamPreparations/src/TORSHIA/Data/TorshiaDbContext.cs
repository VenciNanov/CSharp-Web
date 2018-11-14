using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TORSHIA.Models;

namespace TORSHIA.Data
{
    public class TorshiaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Sector> Sectors { get; set; }

        public DbSet<TaskSector> TaskSectors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Torshia;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskSector>().HasKey(x => new {x.SectorId, x.TaskId});

            modelBuilder.Entity<Report>()
                .HasOne(x => x.Task)
                .WithOne(x => x.Report)
                .HasForeignKey<Report>(x => x.TaskId);
        }
    }
}
