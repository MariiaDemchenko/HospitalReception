using PhotoManager.DAL.ProjectionModels;
using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Http;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [System.Web.Mvc.RoutePrefix("gallery")]
    public class GalleryController : Controller
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("search")]
        public ActionResult Search()
        {
            return View(new GallerySearchViewModel { KeyWord = string.Empty });
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("search/{keyWord}")]
        public ActionResult Search(string keyword)
        {
            return View(new GallerySearchViewModel { KeyWord = keyword });
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("advancedSearch")]
        public ActionResult AdvancedSearch([FromUri]SearchModel model)
        {
            return View(model);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("error/index")]
        public ActionResult ErrorIndex()
        {
            TempData["ErrorMessage"] = "Error getting photos";
            return RedirectToAction("Error");
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("error/search")]
        public ActionResult ErrorSearch()
        {
            TempData["ErrorMessage"] = "Error searching photos";
            return RedirectToAction("Error");
        }
    }
}