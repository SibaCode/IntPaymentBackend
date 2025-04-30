using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntPaymentAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IntPaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransactionDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDetails>>> GetAll()
        {
            return await _context.TransactionDetails.ToListAsync();
        }

        // GET: api/TransactionDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetails>> GetById(int id)
        {
            var transaction = await _context.TransactionDetails.FindAsync(id);
            if (transaction == null)
                return NotFound();

            return transaction;
        }

        // POST: api/TransactionDetails
        [HttpPost]
        public async Task<ActionResult<TransactionDetails>> Create(TransactionDetails transaction)
        {
            _context.TransactionDetails.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
        }

        // PUT: api/TransactionDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransactionDetails transaction)
        {
            if (id != transaction.Id)
                return BadRequest();

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionDetailsExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/TransactionDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.TransactionDetails.FindAsync(id);
            if (transaction == null)
                return NotFound();

            _context.TransactionDetails.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TransactionDetailsExists(int id)
        {
            return _context.TransactionDetails.Any(e => e.Id == id);
        }
    }
}
