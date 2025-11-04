using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remp.Model.Entities;

namespace Remp.DataAccess.Configurations;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasMaxLength(36);

        builder.Property(a => a.AgentFirstName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.AgentLastName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.AvatarUrl).HasMaxLength(2000);
        builder.Property(a => a.CompanyName).IsRequired().HasMaxLength(200);

        builder.HasMany(a => a.ListingCases).WithMany(lc => lc.Agents);
        builder.HasMany(a => a.PhotographyCompanies).WithMany(pc => pc.Agents);
    }
}
