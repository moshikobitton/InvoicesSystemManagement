using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Models;

namespace server_side.Controllers
{


    public class InvoicesController : Controller
    {
        private readonly InvoiceDbContext _context;

        public InvoicesController(InvoiceDbContext context)
        {
            _context = context;
        }

        // GET: Invoices

        public async Task<IActionResult> Index()
        {
            return _context.invoices != null ?
                        Ok(await _context.invoices.ToListAsync()) :
                        Problem("Entity set 'InvoiceDbContext.invoices'  is null.");
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.invoices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }


        // POST: Invoices/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Date,Status,Amount")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(invoice);
        }


        // PUT: Invoices/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Status,Amount")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok("Updated successfully");
            }
            return Ok(invoice);
        }


        // Delete: Invoices/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.invoices == null)
            {
                return Problem("Entity set 'InvoiceDbContext.invoices'  is null.");
            }
            var invoice = await _context.invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.invoices.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return Ok("Deleted successfully");
        }

        private bool InvoiceExists(int id)
        {
            return (_context.invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
