using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using HomeCinema.Data.Repositories;
using HospitalReception.DAL;
using HospitalReception.DAL.Infrastructure;
using HospitalReception.DAL.Repositories;
using Autofac.Integration.WebApi;

namespace HospitalReception
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            //var config = GlobalConfiguration.Configuration;
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF HomeCinemaContext
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
            //builder.RegisterType<PhotoManagerDbContext>().As<IPhotoManagerDbContext>().InstancePerRequest();
            //builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            //builder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
            //builder.RegisterType<UserRepository>().As<IUserRepository>();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}