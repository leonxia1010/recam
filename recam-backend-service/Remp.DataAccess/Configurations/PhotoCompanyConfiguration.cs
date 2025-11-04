using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remp.Model.Entities;

namespace Remp.DataAccess.Configurations;

public class PhotographyCompanyConfiguration : IEntityTypeConfiguration<PhotographyCompany>
{
    public void Configure(EntityTypeBuilder<PhotographyCompany> builder)
    {
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Id).HasMaxLength(36);

        builder.Property(pc => pc.PhotographyCompanyName).IsRequired().HasMaxLength(200);

        builder.HasMany(pc => pc.Agents).WithMany(a => a.PhotographyCompanies);
    }
}
