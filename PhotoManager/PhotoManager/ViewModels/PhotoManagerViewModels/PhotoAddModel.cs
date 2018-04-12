namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class PhotoAddModel
    {
        public int? Id { get; set; }
        public string Url
        {
            get
            {
                int.TryParse(Id?.ToString(), out var id);
                return "/api/photos/album/"+ id;
            }
        }
    }
}