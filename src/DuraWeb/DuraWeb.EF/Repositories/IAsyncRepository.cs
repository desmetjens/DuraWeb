using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuraWeb.EF.Repositories
{
  public interface IAsyncRepository<T>
  {
    Task<T> GetAsync(int key);
    Task<IEnumerable<T>> GetAllAsync();
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(int id, T b);
    Task<int> DeleteAsync(int id);
  }
}