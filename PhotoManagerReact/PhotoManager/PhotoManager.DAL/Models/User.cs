using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PhotoManager.BLL.Models;

namespace PhotoManager.DAL.Models
{
    public class User : IUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
              
        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Salt")]
        public string Salt { get; set; }
    }
}