namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class ImageViewModel : PhotoViewModel
    {
        public int AlbumId { get; set; }
        public byte[] Image { get; set; }
    }
}