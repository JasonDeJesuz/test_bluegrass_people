using Bluegrass.Data.Context;
using Bluegrass.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bluegrass.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration, string? connectionString = null)
        {
            // Using a in memory database
            services.AddDbContext<BluegrassContext>(o => o.UseInMemoryDatabase("Bluegrass"));
            
            // Using SQL Server
            // services.AddDbContext<BluegrassContext>(o =>
            //     o.UseSqlServer(configuration.GetConnectionString("BlueGrassContext")));
            
            services.AddTransient<IBluegrassContextService, BluegrassContextService>();
            
            return services;
        }
    }
}