using Employees.Data;
using Employees.Dto;

namespace Employees.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetEmployees();
        Task<int> CreateEmployee(EmployeeDto employeeDto);
        Task<Employee> UpdateEmployee(int id,EmployeeDto employee);
        Task<EmployeeDto> GetEmployee(int id);
        Task<String> DeleteEmployee(int id);
    }
}