using FeatureToggle.Domain.Configurations;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Infrastructure.Models
{
    public class FeatureManagementContext : IdentityDbContext<User>
    {
        public DbSet<Log> Logs { get; set; }  
        public FeatureManagementContext(DbContextOptions<FeatureManagementContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // for Identity
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>().ToTable("User", "featuremanagement");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRole", "featuremanagement");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaim", "featuremanagement");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogin", "featuremanagement");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaim", "featuremanagement");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRole", "featuremanagement");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserToken", "featuremanagement");
        }
    
    }
}
