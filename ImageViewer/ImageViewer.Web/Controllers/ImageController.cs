using ImageClient.Filters;
using ImageClient.ImageServiceReference;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageClient.Controllers
{
    public class ImageController : Controller
    {
        readonly ImageViewerClient _client;
        public ImageController()
        {
            _client = new ImageViewerClient();
        }

        [HttpGet]
        public ActionResult Index(ImageInfo imageInfo)
        {
            return View(imageInfo);
        }

        [ExceptionHandling]
        public FileResult Download(ImageInfo imageInfo)
        {
            var image = _client.DownloadImage(imageInfo);
            return new FileStreamResult(image, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View(new ImageInfo());
        }

        [HttpPost]
        [ExceptionHandling]
        public ActionResult Upload(HttpPostedFileBase upload, ImageInfo imageInfo)
        {
            if (upload != null && imageInfo.Name.Split('.').LastOrDefault() == "jpg")
            {
                _client.UploadImage(new ImageInfo { Name = imageInfo.Name }, upload.InputStream);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}