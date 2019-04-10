using PhotoManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoManager.BLL.Services
{
    public interface IAlbumService
    {
        IEnumerable<IAlbum> Get(int startIndex, int count);

        IAlbum Get(string id);

        IAlbum Create(IAlbum album);

        void Update(string id, IAlbum albumIn);

        void Remove(IAlbum albumIn);

        void Remove(string id);
    }
}
