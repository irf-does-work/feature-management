﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FeatureToggle.Domain.Entity.Custom_Schema;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace FeatureToggle.Domain.Configurations
//{
//    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
//    {
//        public void Configure(EntityTypeBuilder<Business> builder)
//        {
//            builder.ToTable("Business", "FeatureDB");

//            builder.Property(x => x.Name).IsRequired()
//                    .HasColumnType("nvarchar").HasMaxLength(20);

//            builder.HasMany(x => x.BusinessFeatures)
//                .WithOne(x => x.Business)
//                .HasForeignKey(x => x.BusinessId);
//        }
//    }
//}
