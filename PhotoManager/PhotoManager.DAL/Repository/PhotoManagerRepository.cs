using PhotoManager.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class PhotoManagerRepository
    {
        public List<Photo> GetPhotos()
        {
            List<Photo> photos;
            using (var context = new PhotoManagerContext())
            {
                photos = context.Photos.ToList();
            }
            return photos;
        }
    }
}