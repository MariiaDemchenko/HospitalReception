using PhotoManager.DAL.Contracts;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoManagerRepository _repository;

        public HomeController(IPhotoManagerRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.GetPhotos());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}