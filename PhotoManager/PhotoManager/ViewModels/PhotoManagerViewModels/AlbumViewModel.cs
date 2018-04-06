using System.Collections.Generic;

namespace PhotoManager.ViewModels.PhotoManagerViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }

        public ICollection<PhotoViewModel> Photos { get; set; }
    }
}