using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    public class AlbumController : Controller
    {
        public ActionResult Index(int id=0)
        {
            if (id != 0)
            {
                TempData["AlbumId"] = id;
                ViewBag.FilterParameter = id;
            }
            return View();
        }
    }
}