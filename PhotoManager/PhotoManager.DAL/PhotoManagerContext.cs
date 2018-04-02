using PhotoManager.DAL.Models;
using System.Data.Entity;

namespace PhotoManager.DAL
{
    public class PhotoManagerContext : ApplicationDbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<CameraSettings> CameraSettings { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo2Album> Photo2Albums { get; set; }

        public PhotoManagerContext()
        {
            Database.SetInitializer(new PhotoManagerInitializer());
        }
    }
}