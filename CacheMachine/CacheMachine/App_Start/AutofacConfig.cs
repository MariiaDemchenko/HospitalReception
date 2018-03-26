using Autofac;
using Autofac.Integration.Mvc;
using CacheMachine.DAL.Repository;
using System.Web.Mvc;

namespace CacheMachine
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<CacheMachineRepository>().As<IRepository>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}