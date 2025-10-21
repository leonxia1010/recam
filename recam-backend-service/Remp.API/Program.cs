using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Remp.DataAccess.Data;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Remp.API;

public class Program
{
    public static void Main(string[] args)
    {
        // Create bootstrap logger for early startup logging
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog with full configuration and DI integration
            builder.Services.AddSerilog((services, lc) => lc
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext());

            // Add services to the container.
            builder.Services.AddControllers();

            // Add Database
            builder.Services.AddDbContext<RempSQLServerDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("RempSQLServer"));
            });

            builder.Services.Configure<RempMongoDbSettings>(options =>
            {
                options.ConnectionString = builder.Configuration.GetConnectionString("RempMongoDb") ?? string.Empty;
                options.DatabaseName = builder.Configuration["DatabaseSettings:DatabaseName"] ?? string.Empty;
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
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            // Log application startup information after it actually starts
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                var addresses = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>()?.Addresses;
                var urls = addresses != null && addresses.Any() ? string.Join(", ", addresses) : "Unknown";

                Log.Information("Environment: {Environment}", app.Environment.EnvironmentName);
                Log.Information("Listening on: {Urls}", urls);

                if (app.Environment.IsDevelopment())
                {
                    var firstUrl = addresses?.FirstOrDefault() ?? "http://localhost:5000";
                    Log.Information("Swagger UI: {SwaggerUrl}/swagger", firstUrl);
                    Log.Information("Swagger JSON: {SwaggerUrl}/swagger/v1/swagger.json", firstUrl);
                }
            });

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
