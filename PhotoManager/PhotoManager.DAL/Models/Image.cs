using PhotoManager.Common;

namespace PhotoManager.DAL.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public Constants.ImageSize Size { get; set; }

        public Photo Photo { get; set; }
    }
}
