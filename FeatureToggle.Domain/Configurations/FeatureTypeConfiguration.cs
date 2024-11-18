using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureToggle.Domain.Configurations
{
    public class FeatureTypeConfiguration : IEntityTypeConfiguration<FeatureType>
    {
        public void Configure(EntityTypeBuilder<FeatureType> builder)
        {
            builder.ToTable("FeatureType", "business");

        }
    }
}
