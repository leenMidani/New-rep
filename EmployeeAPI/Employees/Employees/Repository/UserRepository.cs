using Employees.Data;
using Employees.Dto;
using Microsoft.AspNetCore.Identity;

namespace Employees.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManager;


        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _userManger = userManager;
            _signInManager = signIn;
        }
        public async Task<IdentityResult> CreateAsync(AddUserDto addUser)
        {
            var user = new AppUser()
            {
                FullName = addUser.Name,
                UserName = addUser.Email,
                Email = addUser.Email,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            };
            var result = await _userManger.CreateAsync(user,
           addUser.Password);
            return result;


        }

        public async Task<SignInResult> Login(LogInDto loginUser)
        {
           return await _signInManager.PasswordSignInAsync(loginUser.Email,loginUser.Password,false,false);
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

