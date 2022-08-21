using Employees.Data;
using Employees.Dto;
using Employees.Model;
using Employees.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security;
using Microsoft.AspNetCore.Identity;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly JwTConfig _jwTConfig;
        private readonly UserManager<AppUser> _userManager;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, UserManager<AppUser>userManager,IOptions<JwTConfig> jwtconfig)
        {
            _logger = logger;
            _userRepository = userRepository;
            _jwTConfig = jwtconfig.Value;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<string> RegisterUser([FromBody] AddUserDto adduser)

        {
            try {
             var result=   _userRepository.CreateAsync(adduser);
        if (result.Result.Succeeded) 
 { 
 return await Task.FromResult("User is Created");
    }
                return await Task.FromResult(string.Join(",", result.Result));
 }
 catch (Exception ex)
{
    return await Task.FromResult(ex.Message);
}
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LogInDto loginuser)

        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return  BadRequest("User not Selected");
                }
                var result = _userRepository.Login(loginuser);
                if (result.Result.Succeeded)
                {
                    var appUser = await _userManager.FindByEmailAsync(loginuser.Email);
                    var user = new UserDto(appUser.FullName, appUser.Email, appUser.UserName, appUser.CreatedDate);
                     user.Token = GenerateToken(appUser);
                    return  Ok( user);
                }
                return  BadRequest("Wrong UserName or Password");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string GenerateToken(AppUser user)
        {
            var JwtHandler = new JwtSecurityTokenHandler();
            var key= Encoding.ASCII.GetBytes(_jwTConfig.Key);
            var Desc = new SecurityTokenDescriptor
            {
                Subject =new  System.Security.Claims.ClaimsIdentity(new[]
                {
                   new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                Audience=_jwTConfig.Audience,
                Issuer=_jwTConfig.Issuer
            };
            var token = JwtHandler.CreateToken(Desc);
            return JwtHandler.WriteToken(token);

        }

    }
}
