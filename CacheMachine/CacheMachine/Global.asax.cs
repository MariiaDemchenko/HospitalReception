using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CacheMachine.Helpers;

namespace CacheMachine
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            var session = new SessionHelper();
            var context = HttpContext.Current;
            if (context == null)
            {
                return;
            }
            session.IsAuthorized = false;
            session.CardNumber = null;
            session.InvalidPinCodes = new Dictionary<string, int>();
        }
    }
}
