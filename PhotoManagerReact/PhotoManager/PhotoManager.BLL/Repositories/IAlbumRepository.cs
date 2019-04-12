using PhotoManager.BLL.Models;
using System.Collections.Generic;

namespace PhotoManager.BLL.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<IAlbum> Get(int start, int count);

        IAlbum Create(IAlbum album);

        void Update(string id, IAlbum albumIn);

        void RemoveMany(IEnumerable<string> id);

        string ValidateAlbum(IAlbum albumIn);
    }
}