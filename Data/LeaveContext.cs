using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalProjectNew.Models;

namespace FinalProjectNew.Data
{
    public class LeaveContext : DbContext
    {
        public LeaveContext (DbContextOptions<LeaveContext> options)
            : base(options)
        {
        }

        public DbSet<FinalProjectNew.Models.Employee> Employees { get; set; }
        public DbSet<FinalProjectNew.Models.LeaveRequest> LeaveRequests { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("integer").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Firstname).HasColumnType("text").IsRequired().HasMaxLength(50);
                entity.Property(e => e.Lastname).HasColumnType("text").IsRequired().HasMaxLength(50);
                entity.Property(e => e.YearsServed).HasColumnType("integer").IsRequired().HasDefaultValue(1);
                entity.HasData(new Models.Employee()
                {
                    Id = 1,
                    Firstname = "Emma",
                    Lastname = "Salgado",
                    YearsServed = 1
                });
                entity.HasData(new Models.Employee()
                {
                    Id = 2,
                    Firstname = "Patrick",
                    Lastname = "Chipman",
                    YearsServed = 1
                });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("integer").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Firstname).HasColumnType("text").IsRequired().HasMaxLength(50);
                entity.Property(e => e.Lastname).HasColumnType("text").IsRequired().HasMaxLength(50);
                entity.Property(e => e.YearsServed).HasColumnType("integer").IsRequired().HasDefaultValue(1);
            });

        }

    }
}
