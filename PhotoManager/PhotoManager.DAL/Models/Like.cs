namespace PhotoManager.DAL.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PhotoId { get; set; }
        public int AlbumId { get; set; }
        public bool IsPositive { get; set; }

        public ApplicationUser User { get; set; }
        public Photo Photo { get; set; }
    }
}
