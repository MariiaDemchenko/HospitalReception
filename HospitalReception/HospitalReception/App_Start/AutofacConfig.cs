using Autofac;
using Autofac.Integration.WebApi;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Repositories;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace HospitalReception
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterType<HospitalReceptionDbContext>()
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                .As(typeof(IEntityBaseRepository<>))
                .InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}