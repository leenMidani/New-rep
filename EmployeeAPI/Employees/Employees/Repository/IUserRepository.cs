using Employees.Dto;
using Microsoft.AspNetCore.Identity;

namespace Employees.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(AddUserDto addUser);
        Task<SignInResult> Login(LogInDto loginUser);
        Task Logout();
    }
}