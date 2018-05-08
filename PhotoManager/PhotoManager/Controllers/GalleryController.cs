using PhotoManager.DAL.ProjectionModels;
using PhotoManager.ViewModels.PhotoManagerViewModels;
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
        [Route("search")]
        public ActionResult Search()
        {
            return View(new GallerySearchViewModel { KeyWord = string.Empty });
        }

        [HttpGet]
        [Route("search/{keyWord}")]
        public ActionResult Search(string keyword)
        {
            return View(new GallerySearchViewModel { KeyWord = keyword });
        }

        [HttpGet]
        [Route("advancedSearch")]
        public ActionResult AdvancedSearch(SearchModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        [Route("error/index")]
        public ActionResult ErrorIndex()
        {
            TempData["ErrorMessage"] = "Error getting photos";
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Route("error/search")]
        public ActionResult ErrorSearch()
        {
            TempData["ErrorMessage"] = "Error searching photos";
            return RedirectToAction("Error");
        }
    }
}