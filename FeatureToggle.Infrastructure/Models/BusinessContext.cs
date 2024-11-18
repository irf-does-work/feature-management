using FeatureToggle.Domain.Configurations;
using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Infrastructure.Models
{
    public class BusinessContext(DbContextOptions<BusinessContext> options) : DbContext(options)
    { 
        public DbSet<Business> Business { get; set; }
        public DbSet<Feature> Feature { get; set; }
        public DbSet<BusinessFeatureFlag> BusinessFeatureFlag { get; set; }
        public DbSet<FeatureType> FeatureType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BusinessConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessFeatureFlagConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureTypeConfiguration());
        }

    }
}
