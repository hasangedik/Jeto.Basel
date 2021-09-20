using Autofac;
using FluentValidation;
using Jeto.Basel.Common.Helpers;
using Jeto.Basel.Common.Providers;
using Jeto.Basel.Common.StartupConfigurations;
using Jeto.Basel.Container;
using Jeto.Basel.Core.Handlers.CommandHandlers.User;
using Jeto.Basel.Core.Validators;
using Jeto.Basel.Core.Validators.User;
using Jeto.Basel.WebApi.Middlewares;
using Jeto.Basel.WebApi.PipelineBehaviours;
using Jeto.Basel.WebApi.Configurations.Startup;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jeto.Basel.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptionConfiguration(Configuration);
            services.AddDatabaseContext(Configuration);
            services.AddJwtAuthorizationConfiguration();
            services.AddControllers();
            services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddHttpContextAccessor();
            services.AddHealthCheckConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHealthCheckConfiguration();
            app.UseSecuritySettings(); 
            app.UseRouting();
            app.UseAuthorization();
            app.UseHealthCheckConfiguration();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        /// <summary>
        /// Autofac DI Configuration
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            
            builder.RegisterType<TenantProvider>()
                .As<ITenantProvider>()
                .WithParameter("isTenantIdFilterEnabled", true)
                .SingleInstance();

            builder.RegisterType<JwtManager>()
                .As<IJwtManager>()
                .SingleInstance();
            
            builder.RegisterMediatR(typeof(CreateUserCommandHandler).Assembly);
        }
    }
}