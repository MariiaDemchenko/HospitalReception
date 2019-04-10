using PhotoManager.BLL.Models;

namespace PhotoManager.BLL.Repositories
{
    public interface IUserRepository
    {
        IUser Get(IUser user);

        IUser GetByEmail(string email);

        IUser Create(IUser user);

        void Update(string id, IUser userIn);

        void Remove(IUser userIn);

        void Remove(string id);
    }
}
