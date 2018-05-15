using System.Web.Mvc;

namespace ImageClient.Filters
{
    public class ExceptionHandlingAttribute : HandleErrorAttribute
    {
        public string Message { get; set; }

        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Controller.TempData["ErrorMessage"] = filterContext.Exception.Message;
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                TempData = filterContext.Controller.TempData
            };
        }
    }
}