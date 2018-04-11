using PhotoManager.DAL.Models;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IAlbumRepository
    {
        List<Album> GetAllAlbums();

        Album GetAlbumById(int? id);
    }
}