using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementService.Data;
using TaskManagementService.Model;

namespace TaskManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees.ToListAsync();
                return Ok(employees); // Return a 200 OK response with the list of employees
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // GET api/employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound(); // Return a 404 Not Found response if employee is not found
                }
                return Ok(employee); // Return a 200 OK response with the employee details
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // POST api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee); // Return a 201 Created response with the created employee
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // PUT api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            try
            {
                if (id != updatedEmployee.Id)
                {
                    return BadRequest("Employee ID mismatch"); // Return a 400 Bad Request response if the IDs don't match
                }

                _context.Entry(updatedEmployee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(updatedEmployee); // Return a 200 OK response with the updated employee
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // DELETE api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound(); // Return a 404 Not Found response if employee is not found
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return NoContent(); // Return a 204 No Content response indicating successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
