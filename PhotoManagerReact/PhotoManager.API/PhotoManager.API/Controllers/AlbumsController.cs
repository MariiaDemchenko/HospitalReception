using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoManagerApi.Models;
using PhotoManagerApi.Services;

namespace PhotoManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumsController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public ActionResult<List<Album>> Get()
        {
            return _albumService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetAlbum")]
        public ActionResult<Album> Get(string id)
        {
            var album = _albumService.Get(id);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        [HttpPost]
        public ActionResult<Album> Create(Album album)
        {
            _albumService.Create(album);

            return CreatedAtRoute("GetAlbum", new { id = album.Id.ToString() }, album);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Album albumIn)
        {
            var album = _albumService.Get(id);

            if (album == null)
            {
                return NotFound();
            }

            _albumService.Update(id, album);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var album = _albumService.Get(id);

            if (album == null)
            {
                return NotFound();
            }

            _albumService.Remove(album.Id);

            return NoContent();
        }

    }
}