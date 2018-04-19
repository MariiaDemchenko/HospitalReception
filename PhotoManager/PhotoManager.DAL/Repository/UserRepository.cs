using PhotoManager.DAL.Contracts;
using PhotoManager.DAL.Models;
using System.Linq;

namespace PhotoManager.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IPhotoManagerDbContext _context;

        public UserRepository(IPhotoManagerDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
