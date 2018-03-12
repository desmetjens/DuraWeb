using System.Threading.Tasks;
using AutoMapper;
using DuraWeb.Application.Model;
using DuraWeb.EF.Repositories;
using DuraWeb.Model;
using Microsoft.AspNetCore.Mvc;

namespace DuraWeb.Application.Controllers
{
  [Produces("application/json")]
  [Route("api/customers")]
  public class CustomersController : Controller
  {
    private readonly IAsyncRepository<Customer> _repository;

    public CustomersController(IAsyncRepository<Customer> repository)
    {
      _repository = repository;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CustomerModel customer)
    {
      var model = Mapper.Map<Customer>(customer);
      var changesMade = await _repository.AddAsync(model);
      return Ok(changesMade);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(CustomerModel customer)
    {
      var model = Mapper.Map<Customer>(customer);
      var changesMade = await _repository.UpdateAsync(customer.Id, model);
      return Ok(changesMade);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var changesMade = await _repository.DeleteAsync(id);
      return Ok(changesMade);
    }
  }
}