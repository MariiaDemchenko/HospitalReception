using System.Collections.Generic;

namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class DeleteModel
    {
        public IEnumerable<int> PhotosId { get; set; }
        public int AlbumId { get; set; }
    }
}