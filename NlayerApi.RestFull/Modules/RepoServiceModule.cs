using Autofac;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.UnitOfWork;
using NlayerApi.Repository.Context;
using NlayerApi.Repository.GenericRepositories;
using NlayerApi.Repository.UnitOfWorks;
using NlayerApi.Service.Mappings;
using NlayerApi.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace NlayerApi.RestFull.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssebly = Assembly.GetAssembly(typeof(MapperProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssebly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssebly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
