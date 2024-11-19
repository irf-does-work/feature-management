using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Feature", "business");

            builder.Property(x => x.Name).IsRequired()
                     .HasColumnType("nvarchar").HasMaxLength(50);

            builder.HasMany(x => x.BusinessFeatures)
                .WithOne(x => x.Feature)
                .HasForeignKey(x => x.FeatureId);
        }
    }
}
