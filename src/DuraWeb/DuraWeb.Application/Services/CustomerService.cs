using System.Threading.Tasks;
using AutoMapper;
using DuraWeb.Application.Model;
using DuraWeb.EF;
using DuraWeb.Model;

namespace DuraWeb.Application.Services
{
  public class CustomerService : ICustomerService
  {
    private readonly DuraContext _context;

    public CustomerService(DuraContext context)
    {
      _context = context;
    }
    public async Task<CustomerModel> CreateOrUpdateAsync(CustomerModel model)
    {
      var customer = Mapper.Map<Customer>(model);
      var returnModel = model;
      if (customer.Id <= 0)
      {
        var entityEntry = await _context.Customers.AddAsync(customer);
        returnModel = Mapper.Map<CustomerModel>(entityEntry.Entity);
      }
      else
        Update(customer);

      await _context.SaveChangesAsync();

      return returnModel;
    }

    private async void Update(Customer item)
    {
      var entity = await _context.Customers.FindAsync(item.Id);
      if (entity == null)
        return;

      _context.Entry(entity).CurrentValues.SetValues(item);
    }
  }
}