using NLog;
using System.Web.Mvc;

namespace PhotoManager.Filters
{
    public class ExceptionHandlingAttributeMvc : HandleErrorAttribute
    {
        public string Message { get; set; }

        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString());
            LogManager.GetCurrentClassLogger().Log(LogLevel.Error, filterContext.Exception, $"/{model.ControllerName}/{model.ActionName}");
            filterContext.Controller.TempData["ErrorMessage"] = Message;
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                TempData = filterContext.Controller.TempData
            };
        }
    }
}