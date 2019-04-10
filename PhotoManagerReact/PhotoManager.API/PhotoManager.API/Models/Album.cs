using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PhotoManagerApi.Models
{
    public class Album
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
    }
}
