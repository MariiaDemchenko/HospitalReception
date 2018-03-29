using Autofac;
using Autofac.Integration.Mvc;
using CashMachine.DAL.Repository;
using System.Web.Mvc;

namespace CashMachine
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<CashMachineRepository>().As<IRepository>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}