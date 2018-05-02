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

        public ActionResult Error()
        {
            return View();
        }

        [Route("home/index/error")]
        public ActionResult ErrorIndex()
        {
            TempData["ErrorMessage"] = "Error getting albums";
            return RedirectToAction("Error");
        }

        [Route("home/error/default")]
        public ActionResult ErrorDefault()
        {
            return RedirectToAction("Error");
        }

        [Route("users/error")]
        public ActionResult ErrorUser()
        {
            TempData["ErrorMessage"] = "Error getting users";
            return RedirectToAction("Error");
        }
    }
}