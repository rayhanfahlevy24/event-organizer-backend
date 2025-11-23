using Microsoft.EntityFrameworkCore;
using event_organizer.Models;

namespace event_organizer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants => Set<Tenant>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>()
                .Property(t => t.TenantType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<Tenant>()
                .Property(t => t.TenantName)
                .HasMaxLength(250);

            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.TenantName);
        }
    }
}
