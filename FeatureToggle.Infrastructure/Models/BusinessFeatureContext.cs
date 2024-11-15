using FeatureToggle.Domain.Entity.BusinessSchema;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Infrastructure.Models;

public partial class BusinessFeatureContext : DbContext
{
    public BusinessFeatureContext()
    {
    }

    public BusinessFeatureContext(DbContextOptions<BusinessFeatureContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<BusinessFeatureFlag> BusinessFeatureFlags { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<FeatureType> FeatureTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1401;Database=ControlQore;User Id=sa;Password=Pass@word;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Business>(entity =>
        {
            entity
                .ToTable("Business", "business", tb => tb.HasComment("The table holds the information about the business"))
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Business", "businessHT");
                        ttb
                            .HasPeriodStart("PeriodStart")
                            .HasColumnName("PeriodStart");
                        ttb
                            .HasPeriodEnd("PeriodEnd")
                            .HasColumnName("PeriodEnd");
                    }));

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(500)
                .HasDefaultValue("");
            entity.Property(e => e.AddressLine2).HasMaxLength(500);
            entity.Property(e => e.BusinessIdentifier).HasComputedColumnSql("([BusinessId]+(10010))", true);
            entity.Property(e => e.BusinessName).HasMaxLength(250);
            entity.Property(e => e.DialingCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.DomainName).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .HasDefaultValue("");
            entity.Property(e => e.ImageUrl).HasMaxLength(1000);
            entity.Property(e => e.NameStartsWith).HasDefaultValue(1);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasDefaultValue("");
            entity.Property(e => e.RepresentativeRelationshipTitle)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.RepresentativeSsn)
                .HasMaxLength(9)
                .HasColumnName("RepresentativeSSN");
            entity.Property(e => e.Rowversion)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.StatusId).HasComment("The enum column denotes the current status of the business");
            entity.Property(e => e.TaxId).HasMaxLength(50);
            entity.Property(e => e.Website)
                .HasMaxLength(300)
                .HasDefaultValue("");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(12)
                .HasDefaultValue("");
        });

        modelBuilder.Entity<BusinessFeatureFlag>(entity =>
        {
            entity.HasKey(e => e.FeatureFlagId);
            

            entity
                .ToTable("BusinessFeatureFlag", "business")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("BusinessFeatureFlag", "businessHT");
                        ttb
                            .HasPeriodStart("PeriodStart")
                            .HasColumnName("PeriodStart");
                        ttb
                            .HasPeriodEnd("PeriodEnd")
                            .HasColumnName("PeriodEnd");
                    }));

            entity.HasIndex(e => e.BusinessId, "IX_BusinessFeatureFlag_BusinessId");

            entity.HasIndex(e => new { e.FeatureId, e.BusinessId }, "IX_BusinessFeatureFlag_FeatureId_BusinessId").IsUnique();

            entity.HasOne(d => d.Business).WithMany(p => p.BusinessFeatureFlags).HasForeignKey(d => d.BusinessId);

            entity.HasOne(d => d.Feature).WithMany(p => p.BusinessFeatureFlags)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity
                .ToTable("Feature", "business")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Feature", "businessHT");
                        ttb
                            .HasPeriodStart("PeriodStart")
                            .HasColumnName("PeriodStart");
                        ttb
                            .HasPeriodEnd("PeriodEnd")
                            .HasColumnName("PeriodEnd");
                    }));

            entity.HasIndex(e => e.FeatureName, "IX_Feature_FeatureName").IsUnique();

            entity.HasIndex(e => e.FeatureTypeId, "IX_Feature_FeatureTypeId");

            entity.Property(e => e.FeatureId).ValueGeneratedNever();
            entity.Property(e => e.FeatureTypeId).HasDefaultValue(1);

            entity.HasOne(d => d.FeatureType).WithMany(p => p.Features)
                .HasForeignKey(d => d.FeatureTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FeatureType>(entity =>
        {
            entity
                .ToTable("FeatureType", "business")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("FeatureType", "businessHT");
                        ttb
                            .HasPeriodStart("PeriodStart")
                            .HasColumnName("PeriodStart");
                        ttb
                            .HasPeriodEnd("PeriodEnd")
                            .HasColumnName("PeriodEnd");
                    }));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(20);
        });
        modelBuilder.HasSequence<int>("Seq_Batch_1", "accountspayable").StartsAt(1001L);
        modelBuilder.HasSequence<int>("Seq_Bill_1", "accountspayable").StartsAt(1001L);
        modelBuilder.HasSequence<int>("Seq_Bill_4", "accountspayable").StartsAt(1001L);
        modelBuilder.HasSequence<int>("Seq_CustomerCredit_1", "accountsreceivable").StartsAt(1001L);
        modelBuilder.HasSequence<int>("Seq_Invoice_1", "accountsreceivable").StartsAt(1001L);
        modelBuilder.HasSequence<int>("Seq_LienWaiver_1", "lienwaiver").StartsAt(1001L);
        modelBuilder.HasSequence<int>("Seq_Payment_1", "accountsreceivable").StartsAt(1001L);
        modelBuilder.HasSequence<int>("Seq_Tran_DC_1", "transaction").StartsAt(1001L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
