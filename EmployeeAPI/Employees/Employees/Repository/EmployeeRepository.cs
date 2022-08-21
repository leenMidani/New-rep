using Employees.Data;
using Employees.Dto;
using Microsoft.EntityFrameworkCore;

namespace Employees.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpDBContext _empDBContext;

        public EmployeeRepository(EmpDBContext empDBContext)
        {
            _empDBContext = empDBContext;
        }
        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var emp = await _empDBContext.Employees.Select(
                x => new EmployeeDto
                {Id=x.Id,
                    Name = x.Name,
                    DepartmentName = x.DepartmentName,
                    Salary = x.Salary,
                    JoinDate = x.JoinDate
                }).ToListAsync();


            return emp;

        }
        public async Task<int> CreateEmployee(EmployeeDto employeeDto)
        {
            var oldemp = await _empDBContext.Employees.FirstOrDefaultAsync(x => x.Name == employeeDto.Name);
            if (oldemp != null)
            {
                return 0;
            }    
            var emp = new Employee()
            {
                Name = employeeDto.Name,
                DepartmentName = employeeDto.DepartmentName,
                JoinDate = employeeDto.JoinDate,
                Salary = employeeDto.Salary
            };
            _empDBContext.AddAsync(emp);
            await _empDBContext.SaveChangesAsync();
            return emp.Id;
                
            }
        public async Task<Employee> UpdateEmployee(int id,EmployeeDto employeeDto)
        {
            Employee emp =await _empDBContext.Employees.FirstOrDefaultAsync(x=>x.Id== id);
            if(emp!=null)
            {
                emp.Name = employeeDto.Name;
                emp.DepartmentName = employeeDto.DepartmentName;
                emp.Salary = employeeDto.Salary;
                emp.JoinDate = employeeDto.JoinDate;
               
            };
             _empDBContext.Update(emp);
            await _empDBContext.SaveChangesAsync();
            return emp;

        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            Employee emp = await _empDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (emp != null)
            {
                return new EmployeeDto
                {
                    Name = emp.Name,
                    DepartmentName = emp.DepartmentName,
                    Salary = emp.Salary,
                    JoinDate = emp.JoinDate
                };
            }
            return null;
        }
        public async Task<String> DeleteEmployee(int id)
        {
            Employee emp = await _empDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (emp == null)
            {
                
return null;
            };
            _empDBContext.Employees.Remove(emp);
            await _empDBContext.SaveChangesAsync();
            return "Removed";

        }

    }
}
