using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Remp.Model.Entities;

namespace Remp.DataAccess.Data;

public class RempSQLServerDbContext : IdentityDbContext<User>
{

    public RempSQLServerDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
