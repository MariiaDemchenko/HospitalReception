using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class PhotoManagerRepository : IPhotoManagerRepository
    {
        private readonly IPhotoManagerDbContext _context;

        public PhotoManagerRepository(IPhotoManagerDbContext context)
        {
            _context = context;
        }

        public List<Album> GetAllAlbums()
        {
            return _context.Albums.Include(a => a.Photos).ToList();
        }

        public Album GetAlbumById(int id)
        {
            return (_context.Albums.Include(a => a.Photos).FirstOrDefault(a => a.Id == id));
        }

        public Photo GetPhotoById(int id)
        {
            return _context.Photos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Photo> GetPhotoByKeyWord(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return _context.Photos.ToList();
            }
            return _context.Photos.Where(p => p.Name.Contains(keyWord)).ToList();
        }
    }
}