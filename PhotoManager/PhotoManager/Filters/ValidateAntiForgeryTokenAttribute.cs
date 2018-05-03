using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http.Controllers;

namespace PhotoManager.Filters
{
    public class ValidateAntiForgeryToken : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var headerToken = actionContext
                .Request
                .Headers
                .GetValues("__RequestVerificationToken")
                .FirstOrDefault(); ;

            var cookieToken = actionContext
                .Request
                .Headers
                .GetCookies()
                .Select(c => c[AntiForgeryConfig.CookieName])
                .FirstOrDefault();

            if (cookieToken == null || headerToken == null)
            {
                return false;
            }

            try
            {
                AntiForgery.Validate(cookieToken.Value, headerToken);
            }
            catch
            {
                return false;
            }
            return base.IsAuthorized(actionContext);
        }
    }
}