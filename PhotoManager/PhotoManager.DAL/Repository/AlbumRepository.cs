using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IPhotoManagerDbContext _context;

        public AlbumRepository(IPhotoManagerDbContext context)
        {
            _context = context;
        }

        public List<Album> GetAllAlbums()
        {
            return _context.Albums.Include(a => a.Photos).ToList();
        }

        public Album GetAlbumById(int? id)
        {
            return _context.Albums.FirstOrDefault(a => a.Id == id);
        }
    }
}