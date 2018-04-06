using System.Collections;
using System.Collections.Generic;
using PhotoManager.Common;

namespace PhotoManager.DAL.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public Constants.ImageSize Size { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
