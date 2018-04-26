using PhotoManager.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoManagerDbContext
    {
        DbSet<Image> Images { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<CameraSettings> CameraSettings { get; set; }
        DbSet<Album> Albums { get; set; }
        DbSet<Like> Likes { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }

        int SaveChanges();
        void Dispose();
        DbEntityEntry Entry(object entity);
        Database Database { get; }
    }
}