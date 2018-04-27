using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using System.Collections.Generic;

namespace PhotoManager.DAL.Contracts
{
    public interface IAlbumRepository
    {
        IEnumerable<ThumbnailModel> GetAllAlbums();

        AlbumIndexModel GetAlbumByModel(AlbumSearchModel model, string userId = "", bool allPhotosWithSelectedState = false);

        AlbumIndexModel GetAlbumById(int? id, string userId, bool allPhotosWithSelectedState = false);

        Album EditAlbum(AlbumIndexModel album);

        bool AddAlbum(AlbumIndexModel album);

        IEnumerable<Album> DeleteAlbums(IEnumerable<int> albumsId);

        int GetUserAlbumsCount(string userId);
    }
}