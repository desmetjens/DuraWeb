using System.Collections.Generic;

namespace DuraWeb.EF.Repositories
{
  public interface IRepository<T>
  {
    T Get(int key);
    IEnumerable<T> GetAll();
    int Add(T entity);
    int Update(int id, T b);
    int Delete(int id);
  }
}