using System.Collections.ObjectModel;

namespace PhotoManager.DAL.Models
{
    public class CameraSettings
    {
        public int Id { get; set; }
        public string CameraModel { get; set; }
        public int LensFocalLength { get; set; }
        public double Diaphragm { get; set; }
        public int ShutterSpeed { get; set; }
        public int Iso { get; set; }
        public double Flash { get; set; }

        public virtual Collection<Photo> Photo { get; set; }
    }
}