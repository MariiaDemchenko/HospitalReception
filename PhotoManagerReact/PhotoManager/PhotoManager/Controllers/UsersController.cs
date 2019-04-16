using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhotoManager.BLL.Repositories;
using PhotoManager.Models;
using PhotoManager.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PhotoManager.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IHashcodeHelper _hashHelper;
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepository repository, IMapper mapper, IHashcodeHelper hashHelper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _hashHelper = hashHelper;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserViewModel user)
        {
            IActionResult response = Unauthorized();
            var userToAuthorize = _repository.GetByEmail(user.Email);
            if (_hashHelper.CheckHashMatch(user.Password, userToAuthorize?.Salt, userToAuthorize?.Password))
            {
                var tokenString = GenerateToken(user);
                return Ok(new { token = tokenString, name = userToAuthorize.Name });
            }
            return BadRequest("No such user was registered");
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]UserViewModel user)
        {
            IActionResult response = Unauthorized();
            var userToAuthorize = _repository.GetByEmail(user.Email);
            if (userToAuthorize != null)
            {
                return BadRequest("The user with such email has already been registered");
            }

            var hash = _hashHelper.GenerateHash(user.Password, out var salt);
            var userToRegister = _mapper.Map<User>(user);
            userToRegister.Password = hash;
            userToRegister.Salt = salt;

            var registeredUser = _repository.Create(userToRegister);
            if (registeredUser != null)
            {
                var tokenString = GenerateToken(user);
                return Ok(new { token = tokenString, name = user.Name });
            }

            return BadRequest("Error registering");
        }

        private string GenerateToken(UserViewModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings").GetSection("SecurityKey").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration.GetSection("JwtSettings").GetSection("ValidIssuer").Value,
              _configuration.GetSection("JwtSettings").GetSection("ValidAudience").Value,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}