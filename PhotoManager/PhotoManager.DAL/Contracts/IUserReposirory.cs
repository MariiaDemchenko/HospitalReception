using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Contracts
{
    public interface IUserRepository
    {
        ApplicationUser GetUserById(string id);
    }
}
