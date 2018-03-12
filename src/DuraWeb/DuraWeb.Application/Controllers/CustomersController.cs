using System.Threading.Tasks;
using DuraWeb.Application.Model;
using DuraWeb.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuraWeb.Application.Controllers
{
  [Produces("application/json")]
  [Route("api/customers")]
  public class CustomersController : Controller
  {
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
      _service = service;
    }

    [Route("createupdate")]
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate(CustomerModel customer)
    {
      var customerModel = await _service.CreateOrUpdateAsync(customer);
      return Ok(customerModel);
    }


  }
}