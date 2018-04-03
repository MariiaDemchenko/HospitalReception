using Microsoft.AspNet.Identity.EntityFramework;
using PhotoManager.DAL.Models;

namespace PhotoManager.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("PhotoManagerContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}