namespace PhotoManager.DAL.Models
{
    public class Photo2Album
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public int AlbumId { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual Album Album { get; set; }
    }
}