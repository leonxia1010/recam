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
        builder.Property(a => a.AgentCompanyName).IsRequired().HasMaxLength(200);

        builder.HasOne(a => a.User).WithOne(u => u.Agent).HasForeignKey<Agent>(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(a => a.PhotographyCompanies)
            .WithMany(pc => pc.Agents)
            .UsingEntity<Dictionary<string, object>>(
                "AgentPhotographyCompany",
                joinToPhotographyCompany => joinToPhotographyCompany
                    .HasOne<PhotographyCompany>()
                    .WithMany()
                    .HasForeignKey("PhotographyCompaniesId")
                    .OnDelete(DeleteBehavior.Restrict),
                joinToAgent => joinToAgent
                    .HasOne<Agent>()
                    .WithMany()
                    .HasForeignKey("AgentsId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}
