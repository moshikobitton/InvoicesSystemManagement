using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Models;

namespace server_side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceDbContext _context;

        public InvoiceController(InvoiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> Getinvoices()
        {
            if (_context.invoices == null)
            {
                return NotFound();
            }
            return await _context.invoices.ToListAsync();
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            if (_context.invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            invoice.Id = id;
            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound("Invoice with not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Invoice edited successfully");
        }


        // POST: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice([FromBody] Invoice invoice)
        {
            if (_context.invoices == null)
            {
                return Problem("Entity set 'InvoiceDbContext.invoices'  is null.");
            }
            _context.invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            if (_context.invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return Ok("Invoice deleted successfully");
        }

        private bool InvoiceExists(int id)
        {
            return (_context.invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

