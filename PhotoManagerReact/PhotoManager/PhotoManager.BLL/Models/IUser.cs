namespace PhotoManager.BLL.Models
{
    public interface IUser
    {
        string Id { get; set; }

        string Name { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string Salt { get; set; }
    }
}