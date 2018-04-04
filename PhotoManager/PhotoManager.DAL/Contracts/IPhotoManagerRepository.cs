using System.Collections.Generic;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoManagerRepository
    {
        List<Album> GetAllAlbums();

        Album GetAlbumById(int id);

        Photo GetPhotoById(int id);

        IEnumerable<Photo> GetPhotoByKeyWord(string keyWord);
    }
}