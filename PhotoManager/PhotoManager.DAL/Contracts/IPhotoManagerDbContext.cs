using System.Data.Entity;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Contracts
{
    public interface IPhotoManagerDbContext
    {
        DbSet<Photo> Photos { get; set; }
        DbSet<CameraSettings> CameraSettings { get; set; }
        DbSet<Album> Albums { get; set; }
    }
}