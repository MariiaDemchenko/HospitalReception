using PhotoManager.DAL.Models;
using PhotoManager.DAL.ProjectionModels;
using System.Collections.Generic;
using PhotoManager.Common;

namespace PhotoManager.DAL.Contracts
{
    public interface IAlbumRepository
    {
        CollectionModel<ThumbnailModel> GetAllAlbums(int pageIndex, int pageSize);

        AlbumIndexModel GetAlbumByModel(AlbumSearchModel model, int pageIndex = 0, int pageSize = 0, string userId = "", bool allPhotosWithSelectedState = false);

        AlbumIndexModel GetAlbumById(int? id, int pageIndex, int pageSize, string userId, bool allPhotosWithSelectedState = false);

        Album EditAlbum(AlbumIndexModel album);

        bool AddAlbum(AlbumIndexModel album);

        IEnumerable<Album> DeleteAlbums(IEnumerable<int> albumsId);

        int GetUserAlbumsCount(string userId);
    }
}