using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntPaymentAPI.Models;

namespace IntPaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return await _context.Payments.Include(p => p.Customer).ToListAsync();
        }

        // GET: api/payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments.Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }
[HttpPost]
public async Task<IActionResult> CreatePayment(PaymentDto paymentDto)
{
   {
        // Validate if the customer exists
        var customer = await _context.Customers.FindAsync(paymentDto.CustomerId);
        if (customer == null)
        {
            return BadRequest("Customer not found");
        }

        // Create the Payment entity from the PaymentDto
        var payment = new Payment
        {
            Amount = paymentDto.Amount,
            Currency = paymentDto.Currency,
            Provider = paymentDto.Provider,
            Status = paymentDto.Status,
            Date = paymentDto.Date,
            CustomerId = paymentDto.CustomerId
        };

        // Add payment to the database
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
    }
}
// public async Task<IActionResult> GetPaymentsByCustomer(int customerId)
// {
//     var payments = await _context.Payments
//         .Where(p => p.CustomerId == customerId)
//         .ToListAsync();

//     return Ok(payments);
// }


        // PUT: api/payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
