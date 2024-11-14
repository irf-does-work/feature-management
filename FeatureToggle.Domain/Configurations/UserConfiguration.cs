using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.User_Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "UserDB");

            builder.Property(x => x.Name)
                    .IsRequired();
                    
            builder.HasMany(x => x.Logs)
              .WithOne(x => x.User)
              .HasForeignKey(x => x.UserId);
        }
    }
}
