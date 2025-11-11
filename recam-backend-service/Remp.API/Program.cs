using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Remp.DataAccess.Data;
using Remp.Model.Entities;
using Remp.Application.Profiles;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Remp.Model.Settings;
using Remp.Application.Interfaces;
using Remp.Application.Services;

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
            var MyAllowSpecificOriginsDev = "_myAllowSpecificOriginsDev";

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
                options.ConnectionString = builder.Configuration.GetConnectionString("RempMongoDb")
                    ?? throw new InvalidOperationException("Connection string 'RempMongoDb' not configured.");
                options.DatabaseName = builder.Configuration["DatabaseSettings:DatabaseName"]
                    ?? throw new InvalidOperationException("Configuration 'DatabaseSettings:DatabaseName' not configured.");
            });
            builder.Services.AddSingleton<RempMongoDbContext>();

            // Add Jwt
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<RempSQLServerDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured"),
                    ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured")))
                };
            });

            // Add EmailService
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Add Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOriginsDev, policy =>
                {
                    policy.WithOrigins("http://localhost:5000").AllowAnyHeader().AllowAnyMethod();
                });
            });

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
            app.UseCors(MyAllowSpecificOriginsDev);
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Log application startup information after it actually starts no matter what the logging level is.
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                var addresses = app.Services
                    .GetRequiredService<IServer>()
                    .Features
                    .Get<IServerAddressesFeature>()
                    ?.Addresses;

                var urls = addresses != null && addresses.Any() ? string.Join(", ", addresses) : "Unknown";

                Log.Information("Environment: {Environment}", app.Environment.EnvironmentName);
                Log.Information("Listening on: {Urls}", urls);

                if (app.Environment.IsDevelopment() && addresses != null && addresses.Any())
                {
                    var swaggerProvider = app.Services.GetService<Swashbuckle.AspNetCore.Swagger.ISwaggerProvider>();

                    if (swaggerProvider != null)
                    {
                        var firstUrl = addresses.First();
                        Log.Information("Swagger UI: {SwaggerUrl}/swagger", firstUrl);
                        Log.Information("Swagger JSON: {SwaggerUrl}/swagger/v1/swagger.json", firstUrl);
                    }
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
