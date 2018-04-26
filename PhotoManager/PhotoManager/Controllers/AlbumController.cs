using System.Web.Mvc;
using PhotoManager.DAL.ProjectionModels;
using PhotoManager.ViewModels.PhotoManagerViewModels;

namespace PhotoManager.Controllers
{
    [RoutePrefix("albums")]
    public class AlbumController : Controller
    {
        [HttpGet]
        [Route("{name}")]
        public ActionResult Index(string name)
        {
            return View(new AlbumSearchModel { Name = name });
        }

        [HttpGet]
        [Route("album")]
        public ActionResult Index(AlbumSearchModel model)
        {
            return View(model);
        }

        [HttpGet]
        [Route("manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return View();
        }
    }
}