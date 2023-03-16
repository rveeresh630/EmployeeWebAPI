using EmployeeAPI.Models;
using EmployeeAPI.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeservice)
        {
            _employeeService = employeeservice;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {         

            try
            {
                return Ok(_employeeService.GetAllEmployees());
            }

            catch (Exception ex)
            {
                return BadRequest("Something has gone wrong" + ex);
            }
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _employeeService.GetEmployeeById(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
               
            }

            catch (Exception ex)
            {
                return BadRequest("Something has gone wrong" + ex);
            }
        }
    

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();

                if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
                {
                    ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var createdEmployee = _employeeService.AddEmployee(employee);

                return Created("employee", createdEmployee);
            }

            catch (Exception ex)
            {
                return BadRequest("Something has gone wrong" + ex);
            }
        }


        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {           

            try
            {
                if (employee == null)
                    return BadRequest();

                if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
                {
                    ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employeeToUpdate = _employeeService.GetEmployeeById(employee.EmployeeId);

                if (employeeToUpdate == null)
                    return NotFound();

                return Ok(_employeeService.UpdateEmployee(employee));
            }

            catch (Exception ex)
            {
                return BadRequest("Something has gone wrong" + ex);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {     

            try
            {
                if (id == 0)
                    return BadRequest();

                var employeeToDelete = _employeeService.GetEmployeeById(id);
                if (employeeToDelete == null)
                    return NotFound();

                _employeeService.DeleteEmployee(id);

                return Ok("Deleted Successfully");
            }

            catch (Exception ex)
            {
                return BadRequest("Something has gone wrong" + ex);
            }
        }
    }
}
