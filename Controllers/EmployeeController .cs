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


[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] EmployeeLoginRequest loginRequest)
{
    // Check if username and password are provided
    if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
    {
        return BadRequest(new { message = "Username and password are required." });
    }

    // Fetch the employee from the database using the username
    var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Username == loginRequest.Username);

    if (employee == null)
    {
        return Unauthorized(new { message = "Invalid credentials" });
    }

    // Verify the password using BCrypt (hash comparison)
    var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, employee.PasswordHash);

    if (!isPasswordValid)
    {
        return Unauthorized(new { message = "Invalid credentials" });
    }

    // Generate a mock token for now (in production, use JWT or another method)
    var token = "mock-token"; // Replace this with a real token generation method

    // Return a successful response with employee data and token
    return Ok(new
    {
        employee = new
        {
            id = employee.Id,
            username = employee.Username,
            role = "employee"
        },
        token = token // This would be replaced by a real token in production
    });
}

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
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

        // ===== CREATE NEW EMPLOYEE =====
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
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
