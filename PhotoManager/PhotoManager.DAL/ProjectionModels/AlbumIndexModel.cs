﻿using System.Collections.Generic;
using PhotoManager.Common;

namespace PhotoManager.DAL.ProjectionModels
{
    public class AlbumIndexModel : ThumbnailModel
    {
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<PhotoThumbnailModel> Photos { get; set; }
    }
}