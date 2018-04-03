using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class PhotoManagerRepository : IPhotoManagerRepository
    {
        private readonly IPhotoManagerDbContext _context;

        public PhotoManagerRepository(IPhotoManagerDbContext photoManagerContext)
        {
            _context = photoManagerContext;
        }

        public List<Photo> GetPhotos()
        {
            return _context.Photos.ToList();
        }
    }
}