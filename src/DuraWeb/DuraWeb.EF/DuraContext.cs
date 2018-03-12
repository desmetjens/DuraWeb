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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>()
        .HasOne(a => a.Address)
        .WithOne(b => b.Customer)
        .HasForeignKey<Address>(b => b.CustomerId);

      modelBuilder.Entity<Customer>()
        .HasMany(a => a.Invoices)
        .WithOne(b => b.Customer)
        .HasForeignKey(b => b.CustomerId);

      modelBuilder.Entity<Invoice>()
        .HasMany(a => a.Items)
        .WithOne(b => b.Invoice)
        .HasForeignKey(b => b.InvoiceId);
    }
  }
}
