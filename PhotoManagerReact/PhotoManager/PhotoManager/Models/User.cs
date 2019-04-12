using PhotoManager.BLL.Models;

namespace PhotoManager.Models
{
    public class User : IUser
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Salt { get; set; }
    }
}