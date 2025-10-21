
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Remp.DataAccess.Data;

public class RempSQLServerDbContextFactory : IDesignTimeDbContextFactory<RempSQLServerDbContext>

{

    public RempSQLServerDbContext CreateDbContext(string[] args)
    {

        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Remp.API");

        var configuration = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile("appsettings.Development.json", optional: false).Build();

        var connectionString = configuration.GetConnectionString("RempSQLServer");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidDataException("Connection string 'RempSQLService' not found.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<RempSQLServerDbContext>();

        optionsBuilder.UseSqlServer(connectionString, sql =>
{
    sql.MigrationsAssembly(typeof(RempSQLServerDbContext).Assembly.FullName);
    sql.EnableRetryOnFailure();
});


        return new RempSQLServerDbContext(optionsBuilder.Options);
    }
}

