using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employees.Data
{
    public class EmpDBContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public EmpDBContext(DbContextOptions options) : base(options)
        {

        }

       public DbSet<Employee> Employees { get; set; }

    }
}
