using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Remp.DataAccess.Data;
using Serilog;

namespace Remp.API;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Create SeriLog Instance
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
        builder.Host.UseSerilog();

        // Add services to the container.
        builder.Services.AddControllers();

        // Add Database
        builder.Services.AddDbContext<RempSQLServerDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("RempSQLServer"));
        });

        builder.Services.Configure<RempMongoDbSettings>(options =>
        {
            options.ConnectionString = builder.Configuration.GetConnectionString("RempMongoDb");
            options.DatabaseName = builder.Configuration["DatabaseSettings:DatabaseName"];
        });
        builder.Services.AddSingleton<RempMongoDbContext>();

        // Add Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Remp API",
                Description = "An ASP.NET Core Web API for Managing Recam Platform"
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            options.IncludeXmlComments(xmlPath);
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        try
        {
            Log.Information("🚀 Application starting up...");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "❌ Application terminated unexpectedly.");
        }
        finally
        {
            Log.Information("🛑 Application shutting down...");
            Log.CloseAndFlush();
        }
    }
}
