using Microsoft.AspNetCore.Identity;

namespace Employees.Data
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

}
