using System;
using Microsoft.EntityFrameworkCore;

namespace server_side.Models
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        { }

        public DbSet<Invoice> invoices { get; set; }
    }
}
