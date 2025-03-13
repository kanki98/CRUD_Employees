using Microsoft.EntityFrameworkCore;
using CRUD_Employees.Models;

namespace CRUD_Employees.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.StartedWorking)
                .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue), 
                v => DateOnly.FromDateTime(v));

            modelBuilder.Entity<Employee>()
                .Property(e => e.ContractDue)
                .HasConversion(
                v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                v => v.HasValue ? DateOnly.FromDateTime(v.Value) : (DateOnly?)null);
        }
    }
}

