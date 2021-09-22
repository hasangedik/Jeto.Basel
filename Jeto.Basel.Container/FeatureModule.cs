using System.Reflection;
using Autofac;
using Jeto.Basel.Application.Features.Email;
using Module = Autofac.Module;

namespace Jeto.Basel.Container
{
    public class FeatureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var coreAssemblyType = typeof(EmailService).GetTypeInfo();
            builder.RegisterAssemblyTypes(coreAssemblyType.Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}