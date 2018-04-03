using Autofac;
using Autofac.Integration.Mvc;
using PhotoManager.DAL.Context;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Repository;
using System.Web.Mvc;

namespace PhotoManager
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<PhotoManagerDbContext>().As<IPhotoManagerDbContext>().InstancePerRequest();
            builder.RegisterType<PhotoManagerRepository>().As<IPhotoManagerRepository>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}