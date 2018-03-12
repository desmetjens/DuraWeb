using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DuraWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace DuraWeb.EF.Repositories
{
  public class CustomerRepository : ICustomerRepository
  {
    private readonly DuraContext _context;

    public CustomerRepository(DuraContext context)
    {
      _context = context;
    }

    public async Task<Customer> FindAsync(Expression<Func<Customer, bool>> predicate)
    {
      return await _context.Customers
        .Include(x => x.Address)
        .Include(x => x.Invoices)
        .ThenInclude(x => x.Items)
        .FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Customer>> WhereAsync(Expression<Func<Customer, bool>> predicate)
    {
      return await _context.Customers
        .Include(x => x.Address)
        .Include(x => x.Invoices)
        .ThenInclude(x => x.Items)
        .Where(predicate)
        .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string criteria, int pagesize, int page)
    {
      var customers = await WhereAsync(x => $"{x.Lastname} {x.Firstname}".Contains(criteria)
                                             || $"{x.Firstname} {x.Lastname}".Contains(criteria)
                                             || x.Telephone.Contains(criteria)
                                             || x.VatNumber.Contains(criteria)
                                            || $"{x.Address.ToString()}".Contains(criteria));

      return customers.Skip(pagesize * page).Take(pagesize);
    }

    public async Task<Customer> GetAsync(int key)
    {
      return await _context.Customers
        .Include(x => x.Address)
        .Include(x => x.Invoices)
        .ThenInclude(x => x.Items)
        .FirstOrDefaultAsync(x => x.Id == key);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
      return await _context.Customers
        .Include(x => x.Address)
        .Include(x => x.Invoices)
        .ThenInclude(x => x.Items)
        .ToListAsync();
    }

    public async Task<int> AddAsync(Customer entity)
    {
      await _context.AddAsync(entity);
      return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(int id, Customer entity)
    {
      var original = await _context.Customers.FindAsync(id);
      if (original == null) return 0;

      _context.Entry(original).CurrentValues.SetValues(entity);
      original.Address.Bus = entity.Address.Bus;
      original.Address.Street = entity.Address.Street;
      original.Address.PostalCode = entity.Address.PostalCode;
      original.Address.Number = entity.Address.Number;

      return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
      var customer = await _context.Customers.FindAsync(id);
      if (customer == null) return 0;

      _context.Customers.Remove(customer);
      return await _context.SaveChangesAsync();
    }
  }
}