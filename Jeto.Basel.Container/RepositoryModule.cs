using System.Reflection;
using Autofac;
using Jeto.Basel.Data.Repositories.Concrete;
using Module = Autofac.Module;

namespace Jeto.Basel.Container
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyType = typeof(UserRepository).GetTypeInfo();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}