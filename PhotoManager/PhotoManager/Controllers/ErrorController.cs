using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ServerError()
        {
            return View();
        }
    }
}