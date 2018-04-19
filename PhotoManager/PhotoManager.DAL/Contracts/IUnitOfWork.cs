using System;

namespace PhotoManager.DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPhotoRepository Photos { get; set; }

        IAlbumRepository Albums { get; set; }

        IUserRepository Users { get; set; }

        void Save();
    }
}
