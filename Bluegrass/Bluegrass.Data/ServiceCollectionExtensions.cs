using Bluegrass.Data.Authentication;
using Bluegrass.Data.Context;
using Bluegrass.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddDbContext<BluegrassContext>(o => o.UseInMemoryDatabase("Bluegrass"));

            // Using SQL Server
            // services.AddDbContext<BluegrassContext>(o =>
            //       o.UseSqlServer(configuration.GetConnectionString("BlueGrassContext")));

            services.AddTransient<IBluegrassContextService, BluegrassContextService>();
            
            return services;
        }
    }
}