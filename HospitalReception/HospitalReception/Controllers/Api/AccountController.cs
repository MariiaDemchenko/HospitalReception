using HospitalReception.DAL;
using HospitalReception.DAL.Models;
using HospitalReception.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Web.Http;

namespace HospitalReception.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class AccountController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        [Route("register")]
        public IdentityResult Register(RegisterViewModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new HospitalReceptionDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };
            IdentityResult result = manager.Create(user, model.Password);
            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("getUserClaims")]
        public LoginViewModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;

            return new LoginViewModel
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
        }
    }
}