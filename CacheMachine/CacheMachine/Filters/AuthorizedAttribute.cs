using CacheMachine.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CacheMachine.Filters
{
    public class AuthorizedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Trace.Write("(Authorized Filter)Action Executing: " +
                                                  filterContext.ActionDescriptor.ActionName);
            if (!(bool)HttpContext.Current.Session["IsAuthorized"])
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{
                        {"controller", "Home"},
                        {"action", "Error"},
                        {"message", Resources.UserIsUnauthorized}
                    });
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
                filterContext.HttpContext.Trace.Write("(Authorized Filter)Exception thrown");
            base.OnActionExecuted(filterContext);
        }
    }
}