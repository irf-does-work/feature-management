using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class BusinessFeatureFlagConfiguration : IEntityTypeConfiguration<BusinessFeatureFlag>
    {
        public void Configure(EntityTypeBuilder<BusinessFeatureFlag> builder)
        {
            builder.ToTable("BusinessFeatureFlag", "business");

            builder.HasOne(bf => bf.Business)
              .WithMany(b => b.BusinessFeatures)
              .HasForeignKey(bf => bf.BusinessId);

            builder.HasOne(bf => bf.Feature)
                   .WithMany(f => f.BusinessFeatures)
                   .HasForeignKey(bf => bf.FeatureId);
        }
    }
}
