using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [RoutePrefix("gallery")]
    public class GalleryController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("search/{keyWord}")]
        public ActionResult Index(string keyword)
        {
            ViewBag.FilterParameter = keyword;
            return View();
        }
    }
}