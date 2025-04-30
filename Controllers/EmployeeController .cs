using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntPaymentAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;  // Add this at the top to use BCrypt

namespace IntPaymentAPI.Controllers
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
 [HttpPost]
public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
{
    // Remove Id before adding the employee, so that SQL Server can auto-generate it
    employee.Id = 0; // Reset the Id if you're manually setting it (but you shouldn't need this step)

    _context.Employees.Add(employee);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
}
       [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] EmployeeLoginRequest loginRequest)
{
    // Validate request parameters
    if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
    {
        return BadRequest(new { message = "Username and password are required." });
    }

    // Find employee by username
    var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Username == loginRequest.Username);

    // Check if the employee exists
    if (employee == null)
    {
        return Unauthorized(new { message = "Invalid credentials" });
    }

    // Verify the password using BCrypt (hash comparison)
    try
    {
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, employee.PasswordHash);
        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "Error verifying password", details = ex.Message });
    }

    // Generate mock token (you would replace this with actual token logic)
    var token = "mock-token";

    // Return the employee data and token
    return Ok(new
    {
        employee = new
        {
            id = employee.Id,
            username = employee.Username,
            role = "employee"
        },
        token = token
    });
}

        // ===== GET ALL EMPLOYEES =====
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // ===== GET SINGLE EMPLOYEE BY ID =====
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            return employee;
        }

   
        // ===== UPDATE EMPLOYEE =====
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ===== DELETE EMPLOYEE =====
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
