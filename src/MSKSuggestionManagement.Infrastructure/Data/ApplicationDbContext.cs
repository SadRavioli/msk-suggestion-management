using Microsoft.EntityFrameworkCore;
using MSKSuggestionManagement.Domain.Entities;

namespace MSKSuggestionManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suggestion>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Suggestions)
                .HasForeignKey(s => s.EmployeeId);

            // Configure enums to be stored as strings
            modelBuilder.Entity<Employee>()
                .Property(e => e.RiskLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Suggestion>()
                .Property(s => s.Type)
                .HasConversion<string>();
                
            modelBuilder.Entity<Suggestion>()
                .Property(s => s.Status)
                .HasConversion<string>();
                
            modelBuilder.Entity<Suggestion>()
                .Property(s => s.Priority)
                .HasConversion<string>();
                
            modelBuilder.Entity<Suggestion>()
                .Property(s => s.Source)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}