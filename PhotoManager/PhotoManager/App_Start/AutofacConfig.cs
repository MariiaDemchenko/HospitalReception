using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using PhotoManager.DAL;
using PhotoManager.DAL.Context;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Repository;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace PhotoManager
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterType<PhotoManagerDbContext>().As<IPhotoManagerDbContext>().InstancePerRequest();
            builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}