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
        [Route("search/{keyWord?}")]
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
    }
}