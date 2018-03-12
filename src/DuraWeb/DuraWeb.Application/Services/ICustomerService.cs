using System.Threading.Tasks;
using DuraWeb.Application.Model;

namespace DuraWeb.Application.Services
{
  public interface ICustomerService
  {
    Task<CustomerModel> CreateOrUpdateAsync(CustomerModel model);
  }
}