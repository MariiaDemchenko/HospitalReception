using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            TempData["AlbumId"] = 0;
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            TempData["AlbumId"] = 0;
            ViewBag.FilterParameter = model.KeyWord;
            return View();
        }
    }
}