using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remp.Model.Entities;

namespace Remp.DataAccess.Configurations;

public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(EntityTypeBuilder<MediaAsset> builder)
    {
        builder.HasKey(ma => ma.Id);
        builder.Property(ma => ma.Id).ValueGeneratedOnAdd();

        builder.Property(ma => ma.MediaType).IsRequired();
        builder.Property(ma => ma.MediaUrl).IsRequired().HasMaxLength(2000);

        builder.Property(ma => ma.UploadedAt).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
        builder.Property(ma => ma.IsSelected).HasDefaultValue(false);
        builder.Property(ma => ma.IsHero).HasDefaultValue(false);
        builder.Property(ma => ma.IsDeleted).HasDefaultValue(false);

        builder.HasOne(ma => ma.User).WithMany(u => u.MediaAssets);
    }
}
