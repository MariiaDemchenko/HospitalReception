using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/images")]
    public class ImagesController : ApiController
    {
        [Route("{id}")]
        public HttpResponseMessage GetImage(string id)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            byte[] image = null;
            var rootPath = HostingEnvironment.MapPath("~/Content/Images/");
            var filePath = Path.Combine(rootPath, id + ".jpg");
            if (File.Exists(filePath))
            {
                image = File.ReadAllBytes(filePath);
            }

            var ms = new MemoryStream(image);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return response;
        }
    }
}