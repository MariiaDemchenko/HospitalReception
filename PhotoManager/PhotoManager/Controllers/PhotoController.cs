﻿using PhotoManager.ViewModels.PhotoManagerViewModels;
using System.Web.Mvc;

namespace PhotoManager.Controllers
{
    [RoutePrefix("photos")]
    public class PhotoController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            return View(new PhotoDisplayViewModel { Id = id, Size = (int)Common.Constants.ImageSize.Original });
        }

        [HttpGet]
        [Route("properties/{id}")]
        public ActionResult Properties(int id)
        {
            var x = new PhotoDisplayViewModel { Id = id, Size = (int)Common.Constants.ImageSize.Original };

            return View(x);

        }

        [Authorize]
        [HttpGet]
        [Route("edit/{id}/album/{albumId}")]
        public ActionResult Edit(int id, int? albumId)
        {
            return View(new PhotoEditModel { Id = id, AlbumId = albumId });
        }

        [Authorize]
        [HttpGet]
        [Route("add/{id}")]
        public ActionResult Add(int? id)
        {
            return View(new PhotoAddModel { Id = id });
        }
    }
}