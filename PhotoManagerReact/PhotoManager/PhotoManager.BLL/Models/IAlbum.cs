using System.Collections.Generic;

namespace PhotoManager.BLL.Models
{
    public interface IAlbum
    {
        string Id { get; set; }
        
        string AlbumName { get; set; }
        
        string Description { get; set; }
        
        string Owner { get; set; }

        List<string> Photos { get; set; }

        string Cover { get; set; }
    }
}