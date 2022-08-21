using Employees.Dto;
using Employees.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("all")]
     
        public async Task <IActionResult> GetEmps()
        {
            try
            {
             var result =  await _employeeRepository.GetEmployees();
                return Ok(result);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("update/{id}")]

        public async Task<IActionResult> UpdateEmp(int id,EmployeeDto employeeDto)
        {
            try
            {
                var result = await _employeeRepository.UpdateEmployee(id,employeeDto);
                if(result==null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("add")]

        public async Task<IActionResult> CreateEmp( EmployeeDto employeeDto)
        {
            try
            {
                var result = await _employeeRepository.CreateEmployee(employeeDto);
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetEmp(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("remove/{id}")]

        public async Task<IActionResult> RemoveEmp(int id)
        {
            try
            {
                var result = await _employeeRepository.DeleteEmployee(id);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
