using System.Collections.Generic;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoManagerRepository
    {
        List<Photo> GetPhotos();
    }
}