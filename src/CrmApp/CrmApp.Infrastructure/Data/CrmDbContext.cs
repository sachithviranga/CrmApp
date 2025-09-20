using CrmApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrmApp.Infrastructure.Data
{
    public class CrmDbContext : DbContext
    {
        public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customer => Set<CustomerEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<CustomerEntity>()
                .Property(c => c.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
