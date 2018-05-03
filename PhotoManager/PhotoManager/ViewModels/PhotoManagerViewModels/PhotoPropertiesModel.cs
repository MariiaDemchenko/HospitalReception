using PhotoManager.DAL.ProjectionModels;

namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class PhotoPropertiesModel : PhotoEditModel
    {
        public new string ShutterSpeed { get; set; }
        public new string Diaphragm { get; set; }
        public new string Flash { get; set; }
    }
}