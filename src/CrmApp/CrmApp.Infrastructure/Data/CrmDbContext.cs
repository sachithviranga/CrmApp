using CrmApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Infrastructure.Data
{
    /// <summary>
    /// Entity Framework Core database context for the CRM application.
    /// Exposes DbSets and configures entity mappings and constraints.
    /// </summary>
    public class CrmDbContext : DbContext
    {
        public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customer => Set<CustomerEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CustomerEntity>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CustomerEntity>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<CustomerEntity>()
                .Property(c => c.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
