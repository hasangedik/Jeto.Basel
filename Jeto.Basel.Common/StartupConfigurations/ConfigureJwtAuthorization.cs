using Jeto.Basel.Common.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Jeto.Basel.Common.StartupConfigurations
{
    /// <summary>
    /// Add Swagger configuration extension
    /// </summary>
    public static class ConfigureJwtAuthorization
    {
        /// <summary>
        /// Add Swagger configuration extension
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="configuration">Configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddJwtAuthorizationConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = JwtManager.ValidationParameters;
            });
            return services;
        }
    }
}