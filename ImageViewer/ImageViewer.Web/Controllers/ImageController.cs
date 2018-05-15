using ImageClient.Filters;
using ImageClient.ImageServiceReference;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using ImageInfo = ImageClient.ImageServiceReference.ImageInfo;

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

        [WcfError]
        public FileResult Download(ImageInfo imageInfo)
        {
            var image = _client.DownloadImage(imageInfo);
            return new FileStreamResult(image, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Upload(ImageInfo uploadedImage)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, ImageInfo imageInfo)
        {
            ModelState.Clear();
            if (upload == null)
            {
                ModelState.AddModelError("Image", "Select a photo");
            }
            if (imageInfo.Name == null)
            {
                ModelState.AddModelError("Name", "Name field is required");
            }

            if (!ModelState.IsValid)
            {
                return View(imageInfo);
            }

            try
            {
                _client.UploadImage(new ImageInfo { Name = imageInfo.Name }, upload.InputStream);
            }
            catch (FaultException exception)
            {
                ModelState.AddModelError("Exception", exception.Message);
                return View(imageInfo);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}