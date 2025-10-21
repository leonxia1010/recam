using System;
using Microsoft.EntityFrameworkCore;

namespace Remp.DataAccess.Data;

public class RempSQLServerDbContext : DbContext
{

    public RempSQLServerDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
