using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remp.Model.Entities;
using Remp.Model.Enums;

namespace Remp.DataAccess.Configurations;

public class ListingCaseConfiguration : IEntityTypeConfiguration<ListingCase>
{
    public void Configure(EntityTypeBuilder<ListingCase> builder)
    {
        builder.HasKey(lc => lc.Id);
        builder.Property(lc => lc.Id).ValueGeneratedOnAdd();

        builder.Property(lc => lc.Title).IsRequired().HasMaxLength(200);
        builder.Property(lc => lc.Description).HasMaxLength(1024);
        builder.Property(lc => lc.Street).IsRequired().HasMaxLength(200);
        builder.Property(lc => lc.City).IsRequired().HasMaxLength(50);
        builder.Property(lc => lc.State).IsRequired().HasConversion<string>();
        builder.Property(lc => lc.PostCode).IsRequired().HasMaxLength(4);
        builder.Property(lc => lc.Price).IsRequired();
        builder.Property(lc => lc.Longitude).HasColumnType("decimal(18,6)");
        builder.Property(lc => lc.Latitude).HasColumnType("decimal(18,6)");

        builder.Property(lc => lc.Bedrooms).IsRequired();
        builder.Property(lc => lc.Bathrooms).IsRequired();
        builder.Property(lc => lc.Garages).IsRequired();
        builder.Property(lc => lc.FloorArea).IsRequired();

        builder.Property(lc => lc.PropertyType).IsRequired();
        builder.Property(lc => lc.SaleCategory).IsRequired();
        builder.Property(lc => lc.ListCaseStatus).IsRequired().HasDefaultValue(ListingCaseStatus.Created);

        builder.Property(lc => lc.CreatedAt).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
        builder.Property(lc => lc.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasMany(lc => lc.CaseContacts).WithOne(cc => cc.ListingCase).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(lc => lc.MediaAssets).WithOne(ma => ma.ListingCase).OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(tb =>
        {
            tb.HasCheckConstraint("CK_ListingCase_Price_Positive", "[Price] >= 0");
            tb.HasCheckConstraint("CK_ListingCase_Bedrooms_Positive", "[Bedrooms] >= 0");
            tb.HasCheckConstraint("CK_ListingCase_Bathrooms_Positive", "[Bathrooms] >= 0");
            tb.HasCheckConstraint("CK_ListingCase_Garages_Positive", "[Garages] >= 0");
            tb.HasCheckConstraint("CK_ListingCase_FloorArea_Positive", "[FloorArea] >= 0");
        });

        builder.HasIndex(lc => lc.City);
        builder.HasIndex(lc => lc.State);
        builder.HasIndex(lc => lc.PostCode);
        builder.HasIndex(lc => lc.ListCaseStatus);
        builder.HasIndex(lc => lc.SaleCategory);
    }
}
