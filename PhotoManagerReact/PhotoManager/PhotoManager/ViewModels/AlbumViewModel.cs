using PhotoManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoManager.ViewModels
{
    public class AlbumViewModel
    {
        public string AlbumName { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public string IsSelected { get; set; }
        public string AlbumId { get; set; }
        public List<string> Photos { get; set; }
    }
}
