using MongoDB.Driver;
using PhotoManager.BLL.Models;
using PhotoManager.BLL.Repositories;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("PhotosDb");
            _users = database.GetCollection<User>("Users");
        }

        public IUser GetByEmail(string email)
        {
            return _users.Find(u => u.Email == email).FirstOrDefault() as IUser;
        }

        public IUser Create(IUser user)
        {
            var userToInsert = new User
            {
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                Name = user.Name
            };
            _users.InsertOne(userToInsert);
            return user;
        }
    }
}