using PhotoManager.DAL.Contracts;
using System;

namespace PhotoManager.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPhotoManagerDbContext _context;

        public IPhotoRepository Photos { get; set; }

        public IAlbumRepository Albums { get; set; }

        public IUserRepository Users { get; set; }

        public UnitOfWork(IPhotoManagerDbContext context, IPhotoRepository photoRepository, IAlbumRepository albumRepository, IUserRepository userRepository)
        {
            _context = context;
            Photos = photoRepository;
            Albums = albumRepository;
            Users = userRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}