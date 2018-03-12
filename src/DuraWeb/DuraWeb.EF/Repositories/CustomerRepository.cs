using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuraWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace DuraWeb.EF.Repositories
{
  public class CustomerRepository : IRepository<Customer>, IAsyncRepository<Customer>
  {
    private readonly DuraContext _context;

    public CustomerRepository(DuraContext context)
    {
      _context = context;
    }

    public Customer Get(int key)
    {
      return _context.Customers
        .Include(x => x.Address)
        .Include(x => x.Invoices)
        .ThenInclude(x => x.Items)
        .FirstOrDefault(x => x.Id == key);
    }

    public IEnumerable<Customer> GetAll()
    {
      return _context.Customers
        .Include(x => x.Address)
        .Include(x => x.Invoices)
        .ThenInclude(x => x.Items)
        .ToList();
    }

    public int Add(Customer entity)
    {
      _context.Add(entity);
      return _context.SaveChanges();
    }

    public int Update(int id, Customer entity)
    {
      var original = _context.Customers.Find(id);
      if (original == null) return 0;

      _context.Entry(original).CurrentValues.SetValues(entity);
      return _context.SaveChanges();
    }

    public int Delete(int id)
    {
      var customer = _context.Customers.Find(id);
      if (customer == null) return 0;

      _context.Customers.Remove(customer);
      return _context.SaveChanges();
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