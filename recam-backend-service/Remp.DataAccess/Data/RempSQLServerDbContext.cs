using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Remp.Model.Entities;
using Remp.DataAccess.Configurations;

namespace Remp.DataAccess.Data;

public class RempSQLServerDbContext : IdentityDbContext<User>
{
    public DbSet<ListingCase> ListingCases { get; set; }
    public DbSet<CaseContact> CaseContacts { get; set; }
    public DbSet<MediaAsset> MediaAssets { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public DbSet<PhotographyCompany> PhotographyCompanies { get; set; }

    public RempSQLServerDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ListingCaseConfiguration).Assembly);
    }
}
