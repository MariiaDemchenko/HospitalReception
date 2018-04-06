using PhotoManager.ViewModels.PhotoManagerViewModels;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    public class PhotoController : Controller
    {
        private string _baseUrl = "http://localhost:49239/api/";

        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(PhotoViewModel photo)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync("photos/edit", photo).Result;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return RedirectToAction("Error");
            }

            var albumId = (int)TempData["AlbumId"];

            if (albumId == 0)
            {
                return RedirectToAction("Index", "Gallery", new { id = albumId });
            }


            return RedirectToAction("Index", "Album", new { id = albumId });
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file, PhotoViewModel viewModel)
        {
            var albumId = 0;
            if (TempData["AlbumId"] != null)
            {
                albumId = (int)TempData["AlbumId"];
            }

            if (file != null)
            {
                using (var target = new MemoryStream())
                {
                    file.InputStream.CopyTo(target);
                    viewModel.Image = target.ToArray();
                }
            }

            viewModel.AlbumId = albumId;

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsJsonAsync("photos/add", viewModel).Result;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return RedirectToAction("Error");
            }

            if (albumId == 0)
            {
                return RedirectToAction("Index", "Gallery", new { id = albumId });
            }

            return RedirectToAction("Index", "Album", new { id = albumId });
        }
    }
}