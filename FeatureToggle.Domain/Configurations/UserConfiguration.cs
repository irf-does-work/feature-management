using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "featuremanagement");

            builder.Property(x => x.Name)
                    .IsRequired();
                    
            //builder.HasMany(x => x.Logs)
            //  .WithOne(x => x.User)
            //  .HasForeignKey(x => x.UserId);
        }
    }
}
