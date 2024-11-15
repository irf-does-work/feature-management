using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Log", "UserDB");

            builder.Property(x => x.Time)
               .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Logs)
                   .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.BusinessFeature)
                   .WithMany()
                   .HasForeignKey(x => x.BusinessFeatureId);
        }
    }
}
