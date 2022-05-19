using Bluegrass.Data.Authentication;
using Bluegrass.Data.Context;
using Bluegrass.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Bluegrass.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("jwtConfig");
            var secretKey = jwtConfig["secret"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["validIssuer"],
                    ValidAudience = jwtConfig["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<BluegrassContext>()
            .AddDefaultTokenProviders();
        }
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration, string? connectionString = null)
        {
            // Using a in memory database
            // services.AddDbContext<BluegrassContext>(o => o.UseInMemoryDatabase("Bluegrass"));

            // Using SQL Server
            services.AddDbContext<BluegrassContext>(o =>
                  o.UseSqlServer(configuration.GetConnectionString("BlueGrassContext")));

            services.AddTransient<IBluegrassContextService, BluegrassContextService>();
            
            return services;
        }
        public static void EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<BluegrassContext>();
            dbContext?.Database?.EnsureCreated();
        }
        public static void ApplyDatabaseMigrations(this IApplicationBuilder app)
        {
            // Load options file here to determine if we should run migration or not
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<BluegrassContext>();

            if (!dbContext?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory") ?? false)
            {
                // Migrate database structure
                dbContext?.Database.Migrate();

                // Execute SQL Scripts from the embeded resources
                var files = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                var filePrefix = $"Bluegrass.Data";
                foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql")).Select(f => new
                {
                    PhysicalFile = f,
                    LogicalFile = f.Replace(filePrefix, string.Empty)
                })
                .OrderBy(f => f.LogicalFile.Substring(0, 1)))
                {
                    using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(file.PhysicalFile);
                    using var reader = new StreamReader(stream);
                    string command = reader.ReadToEnd();

                    if (string.IsNullOrEmpty(command))
                        continue;

                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try {
                            dbContext.Database.ExecuteSqlRaw(command);
                            dbContext.SaveChanges();
                            transaction.Commit();
                        } catch
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
        }
    }
}