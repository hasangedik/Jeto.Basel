using System.Reflection;
using Autofac;
using Jeto.Basel.Data.Repositories.Abstract;
using Jeto.Basel.Data.Repositories.Concrete;
using Module = Autofac.Module;

namespace Jeto.Basel.Container
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // TypeInfo assemblyType = typeof(UserRepository).GetTypeInfo();
            //
            // builder.RegisterAssemblyTypes(assemblyType.Assembly)
            //     .Where(x => typeof(IGenericRepository<>).IsAssignableFrom(x))
            //     .AsImplementedInterfaces()
            //     .InstancePerLifetimeScope();
            //
            // base.Load(builder);
            
            var assemblyType = typeof(UserRepository).GetTypeInfo();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}