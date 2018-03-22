using System;
using System.Collections.Generic;
using Autofac;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CacheMachine
{
    public class MvcApplication : HttpApplication
    {
        public static IContainer Container { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                context.Session["InvalidPinCodes"] = new Dictionary<long, int>();
            }
        }
    }
}
