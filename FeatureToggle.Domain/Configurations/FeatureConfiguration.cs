using System.Reflection.Emit;
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

            builder.Property(x => x.FeatureName).IsRequired()
                     .HasColumnType("nvarchar").HasMaxLength(50);

            //builder.HasMany(x => x.BusinessFeatures)
            //    .WithOne(x => x.Feature)
            //    .HasForeignKey(x => x.FeatureId);

            builder.HasOne(f => f.FeatureType) // Feature has one FeatureType
           .WithMany() // FeatureType can have many Features
           .HasForeignKey(f => f.FeatureTypeId) // FeatureTypeId is the foreign key
           .OnDelete(DeleteBehavior.Restrict); // Configure delete behavior (optional)
        }
    }
}
