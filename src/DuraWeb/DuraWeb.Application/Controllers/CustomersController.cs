using System.Linq;
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
    private readonly ICustomerRepository _repository;

    public CustomersController(ICustomerRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Find(SearchFilter filter)
    {
      var customers = await _repository.SearchAsync(filter.Criteria, filter.PageSize, filter.Page);
      var customerModels = customers.Select(Mapper.Map<CustomerModel>);
      return Ok(customerModels);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
      var customer = await _repository.GetAsync(id);
      var customerModel = Mapper.Map<CustomerModel>(customer);
      return Ok(customerModel);
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