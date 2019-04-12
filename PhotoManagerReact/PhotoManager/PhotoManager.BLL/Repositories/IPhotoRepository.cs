using PhotoManager.BLL.Models;
using System.Collections.Generic;

namespace PhotoManager.BLL.Repositories
{
    public interface IPhotoRepository
    {
        IEnumerable<IPhoto> GetAll(int start, int count);

        IEnumerable<IPhoto> GetByAlbumId(int start, int count, string albumId);

        IPhoto Get(string id);

        IPhoto Create(IPhoto photo, string albumId);

        void Update(string id, IPhoto albumIn);

        void Remove(IPhoto photo, string albumId);
    }
}