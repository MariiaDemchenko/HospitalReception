using PhotoManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoManager.BLL.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<IAlbum> Get(int start, int count);

        IAlbum Get(string id);

        IAlbum Create(IAlbum album);

        void Update(string id, IAlbum albumIn);

        void RemoveMany(IEnumerable<string> id);

        string ValidateAlbum(IAlbum albumIn);
    }
}
