using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IAlbumRepository
    {
        IEnumerable<ThumbnailModel> GetAllAlbums();

        AlbumIndexModel GetAlbumById(int? id, bool allPhotosWithSelectedState = false);

        Album EditAlbum(AlbumIndexModel album);

        Album AddAlbum(AlbumIndexModel album);

        IEnumerable<Album> DeleteAlbums(IEnumerable<int> albumsId);

        int GetUserAlbumsCount(string userId);
    }
}