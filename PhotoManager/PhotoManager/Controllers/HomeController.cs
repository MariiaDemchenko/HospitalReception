using PhotoManager.Filters;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [ExceptionHandlingAttributeWebApi]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}