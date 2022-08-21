using System.ComponentModel.DataAnnotations;

namespace Employees.Dto
{
    public class AddUserDto
    {
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
