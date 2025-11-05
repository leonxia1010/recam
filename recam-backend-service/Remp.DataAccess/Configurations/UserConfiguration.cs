using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remp.Model.Entities;
using Remp.Model.Enums;

namespace Remp.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.IsDeleted).HasDefaultValue(false);
        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();

        builder.Property(u => u.UserProfile).HasDefaultValue(UserProfile.None);

        builder.HasMany(u => u.ListingCases).WithOne(lc => lc.User).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.MediaAssets).WithOne(lc => lc.User).OnDelete(DeleteBehavior.Restrict);
    }
}
