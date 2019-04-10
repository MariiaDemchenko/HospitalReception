using PhotoManager.BLL.Models;
using System.Collections.Generic;

namespace PhotoManager.Models
{
    public class Album : IAlbum
    {
        public string Id { get; set; }

        public string AlbumName { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public List<string> Photos { get; set; }

        public string Cover { get; set; }
    }
}
