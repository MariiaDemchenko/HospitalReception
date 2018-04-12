using System.Web.Mvc;
using PhotoManager.ViewModels.PhotoManagerViewModels;

namespace PhotoManager.Controllers
{
    [RoutePrefix("albums")]
    public class AlbumController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            return View(id);
        }
    }
}