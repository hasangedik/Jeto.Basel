using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeto.Basel.Common.StartupConfigurations
{
    /// <summary>
    /// Add Swagger configuration extension
    /// </summary>
    public static class ConfigureOptions
    {
        /// <summary>
        /// Add Swagger configuration extension
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="configuration">Configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddOptionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<RabbitMqOption>(configuration.GetSection(AppConstants.RabbitMqSettingsOptionName));
            return services;
        }
    }
}