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
    }
}