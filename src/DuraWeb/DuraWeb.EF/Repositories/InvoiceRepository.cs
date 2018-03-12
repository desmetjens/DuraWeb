using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DuraWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace DuraWeb.EF.Repositories
{
  public class InvoiceRepository : IInvoiceRepository
  {
    private readonly DuraContext _context;

    public InvoiceRepository(DuraContext context)
    {
      _context = context;
    }

    public async Task<Invoice> GetAsync(int key)
    {
      return await _context.Invoices
        .Include(x => x.Items)
        .FirstOrDefaultAsync(x => x.Id == key);
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
      return await _context.Invoices
        .Include(x => x.Items)
        .ToListAsync();
    }

    public async Task<int> AddAsync(Invoice entity)
    {
      var customer = await _context.Customers.FindAsync(entity.CustomerId);
      customer.Invoices.Add(entity);
      
      return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(int id, Invoice entity)
    {
      var original = await _context.Invoices.FindAsync(id);
      if (original == null) return 0;

      _context.Entry(original).CurrentValues.SetValues(entity);
      foreach (var item in entity.Items)
      {
        original.Items.Single(x => x.Id == item.Id).Amount = item.Amount;
        original.Items.Single(x => x.Id == item.Id).UnitPrice = item.UnitPrice;
        original.Items.Single(x => x.Id == item.Id).Description = item.Description;
      }

      var deletedItems = original.Items.Where(x => !entity.Items.Select(i => i.Id).Contains(x.Id)).ToList();
      foreach (var invoiceItem in deletedItems)
        original.Items.Remove(invoiceItem);

      return await _context.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<Invoice> FindAsync(Expression<Func<Invoice, bool>> predicate)
    {
      return await _context.Invoices
        .Include(x => x.Items)
        .FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Invoice>> WhereAsync(Expression<Func<Invoice, bool>> predicate)
    {
      return await _context.Invoices
        .Include(x => x.Items)
        .Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetOpenInvoicesAsync()
    {
      return await _context.Invoices
        .Include(x => x.Items)
        .Where(x => !x.Paid).ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetExpiredInvoicesAsync()
    {
      var openstaande = await GetOpenInvoicesAsync();
      return openstaande.Where(x => x.ExpiryDate < DateTime.Now.Date);
    }

    public async Task SetPaid(int id)
    {
      var invoice = await _context.Invoices.FindAsync(id);
      invoice.Paid = true;
      await _context.SaveChangesAsync();
    }
  }
}