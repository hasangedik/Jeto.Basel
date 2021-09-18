using HealthChecks.UI.Client;
using Jeto.Basel.Common.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeto.Basel.Common.StartupConfigurations
{
    /// <summary>
    /// Health check configuration extension
    /// </summary>
    public static class ConfigureHealthCheck
    {
        /// <summary>
        /// Add Health check configuration extension
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="configuration">Configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString(AppConstants.PostgreSqlConnectionString));

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            return services;
        }

        /// <summary>
        /// Use Health check configuration
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthCheckConfiguration(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health-check", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(p => { p.UIPath = "/health-check-ui"; });

            return app;
        }
    }
}