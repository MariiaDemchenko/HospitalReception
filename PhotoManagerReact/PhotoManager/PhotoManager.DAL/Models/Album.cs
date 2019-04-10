using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PhotoManager.BLL.Models;
using System.Collections.Generic;

namespace PhotoManager.DAL.Models
{
    public class Album : IAlbum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string AlbumName { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Owner")]
        public string Owner { get; set; }

        [BsonElement("Photos")]
        public List<string> Photos { get; set; }

        public string Cover { get; set; }
    }
}
