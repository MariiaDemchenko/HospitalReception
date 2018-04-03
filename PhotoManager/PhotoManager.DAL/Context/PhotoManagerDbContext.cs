using System.Data.Entity;
using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Initializer;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Context
{
    public class PhotoManagerDbContext : ApplicationDbContext, IPhotoManagerDbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<CameraSettings> CameraSettings { get; set; }
        public DbSet<Album> Albums { get; set; }

        public PhotoManagerDbContext()
        {
            Database.SetInitializer(new PhotoManagerInitializer());
        }
    }
}