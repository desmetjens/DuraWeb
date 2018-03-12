using System.Collections.Generic;
using System.Threading.Tasks;
using DuraWeb.Model;

namespace DuraWeb.EF.Repositories
{
  public interface ICustomerRepository : IAsyncRepository<Customer>
  {
    Task<IEnumerable<Customer>> SearchAsync(string criteria, int pagesize, int page);
  }
}