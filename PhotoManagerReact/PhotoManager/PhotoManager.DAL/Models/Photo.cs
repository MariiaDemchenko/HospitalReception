using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PhotoManager.BLL.Models;
using System;
using System.Collections.Generic;

namespace PhotoManager.DAL.Models
{
    public class Photo : IPhoto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string PhotoName { get; set; }

        [BsonElement("ServerName")]
        public string ServerName { get; set; }

        [BsonElement("Format")]
        public string Format { get; set; }

        [BsonElement("CameraModel")]
        public string CameraModel { get; set; }

        [BsonElement("ShotDate")]
        public DateTime ShotDate { get; set; }

        [BsonElement("LensFocalLength")]
        public int LensFocalLength { get; set; }      

        [BsonElement("Owner")]
        public string Owner { get; set; }

        [BsonElement("Albums")]
        public List<string> Albums { get; set; }
    }
}