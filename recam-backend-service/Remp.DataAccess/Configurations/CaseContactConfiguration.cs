using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remp.Model.Entities;

namespace Remp.DataAccess.Configurations;

public class CaseContactConfiguration : IEntityTypeConfiguration<CaseContact>
{
    public void Configure(EntityTypeBuilder<CaseContact> builder)
    {
        builder.HasKey(cc => cc.Id);
        builder.Property(cc => cc.Id).ValueGeneratedOnAdd();

        builder.Property(cc => cc.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(cc => cc.LastName).IsRequired().HasMaxLength(50);
        builder.Property(cc => cc.CompanyName).IsRequired().HasMaxLength(200);
        builder.Property(cc => cc.ProfileUrl).HasMaxLength(2000);
        builder.Property(cc => cc.Email).IsRequired().HasMaxLength(200);
        builder.Property(cc => cc.PhoneNumber).IsRequired().HasMaxLength(10);
    }
}
