using System.Collections.Generic;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Repository
{
    public interface IRepository
    {
        List<Photo> GetPhotos();
    }
}