using System.Collections.Generic;
using System.Threading.Tasks;
using DuraWeb.Model;

namespace DuraWeb.EF.Repositories
{
  public interface IInvoiceRepository : IAsyncRepository<Invoice>
  {
    Task<IEnumerable<Invoice>> GetOpenInvoicesAsync();
    Task<IEnumerable<Invoice>> GetExpiredInvoicesAsync();
    Task SetPaid(int id);
  }
}