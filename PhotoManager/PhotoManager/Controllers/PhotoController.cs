using System.Web.Mvc;
using PhotoManager.ViewModels.PhotoManagerViewModels;

namespace PhotoManager.Controllers
{
    [RoutePrefix("photos")]
    public class PhotoController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            return View(id);
        }

        [HttpGet]
        [Route("edit/{id}/album/{albumId}")]
        public ActionResult Edit(int id, int? albumId)
        {
            return View(new PhotoEditModel { Id = id, AlbumId = albumId });
        }

        [HttpGet]
        [Route("add/{id}")]
        public ActionResult Add(int? id)
        {
            return View(new PhotoAddModel { Id = id });
        }
    }
}