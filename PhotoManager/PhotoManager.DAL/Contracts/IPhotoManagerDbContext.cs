using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoManagerDbContext
    {
        DbSet<Image> Images { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<CameraSettings> CameraSettings { get; set; }
        DbSet<Album> Albums { get; set; }

        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}