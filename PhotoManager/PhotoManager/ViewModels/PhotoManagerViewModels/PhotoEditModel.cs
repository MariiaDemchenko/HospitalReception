namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class PhotoEditModel
    {
        public int Id { get; set; }
        public int? AlbumId { get; set; }
        public string Url
        {
            get
            {
                int.TryParse(AlbumId?.ToString(), out var albumId);
                return $"/api/photos/{Id}/album/{albumId}";
            }
        }
    }
}