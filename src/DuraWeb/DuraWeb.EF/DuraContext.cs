using DuraWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace DuraWeb.EF
{
  public class DuraContext : DbContext
  {
    public DuraContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
  }
}
