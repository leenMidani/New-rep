using System.ComponentModel.DataAnnotations;

namespace Employees.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        
        public int Salary { get; set; }
       // public string Token { get; set; }
    }
}
