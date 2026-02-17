using System;
using System.Collections.Generic;
using System.Text;
using EFCoreValidationApp.Models;
using EntityValidation.Models;
using Microsoft.EntityFrameworkCore;
namespace CoreValidation.Models.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Lenovo_Ideapad\\SQLEXPRESS;Initial Catalog=CoreValidation;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique(); // unique email constraint

            modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);
        }
    }
}