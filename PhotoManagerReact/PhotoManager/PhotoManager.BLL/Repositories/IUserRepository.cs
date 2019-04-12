using PhotoManager.BLL.Models;

namespace PhotoManager.BLL.Repositories
{
    public interface IUserRepository
    {
        IUser GetByEmail(string email);

        IUser Create(IUser user);
    }
}