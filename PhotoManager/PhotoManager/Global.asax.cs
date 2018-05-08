using NLog;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LogLevel = NLog.LogLevel;

namespace PhotoManager
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.RegisterDependencies();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Initialize();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            var httpException = exception as HttpException;
            string action;
            if (httpException == null)
            {
                LogManager.GetCurrentClassLogger().Log(LogLevel.Error, exception, exception.Message);
                action = "Index";
            }
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        action = "NotFound";
                        break;
                    case 500:
                        action = "ServerError";
                        break;
                    default:
                        action = "Index";
                        break;
                }
            }

            Server.ClearError();
            Response.Redirect($"~/Error/{action}");
        }
    }
}