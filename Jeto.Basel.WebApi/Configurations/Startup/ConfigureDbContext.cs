using Jeto.Basel.Common.Constants;
using Jeto.Basel.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeto.Basel.WebApi.Configurations.Startup
{
    public static class ConfigureDbContext
    {
        /// <summary>
        /// Add DataBase configurations
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="configuration">Configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(AppConstants.PostgreSqlConnectionString))
                    .EnableSensitiveDataLogging()
                    .UseLowerCaseNamingConvention()
                    .UseLazyLoadingProxies();
            });

            // using (var serviceScope = services.BuildServiceProvider().GetService<IServiceScopeFactory>()?.CreateScope())
            // {
            //     if (serviceScope == null) 
            //         return services;
            //     var dataBaseService = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
            //     dataBaseService.Database.EnsureCreated();
            //     dataBaseService.Database.Migrate();
            // }

            return services;
        }
    }
}