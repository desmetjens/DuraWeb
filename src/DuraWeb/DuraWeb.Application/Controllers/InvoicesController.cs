using System.Threading.Tasks;
using AutoMapper;
using DuraWeb.Application.Model;
using DuraWeb.EF.Repositories;
using DuraWeb.Model;
using Microsoft.AspNetCore.Mvc;

namespace DuraWeb.Application.Controllers
{
  [Produces("application/json")]
  [Route("api/invoices")]
  public class InvoicesController : Controller
  {
    private readonly IInvoiceRepository _repository;

    public InvoicesController(IInvoiceRepository repository)
    {
      _repository = repository;
    }
    

    [HttpPost]
    public async Task<IActionResult> Create(InvoiceModel invoice)
    {
      var model = Mapper.Map<Invoice>(invoice);
      var changesMade = await _repository.AddAsync(model);
      return Ok(changesMade);
    }

    [HttpPost]
    public async Task<IActionResult> Update(InvoiceModel invoice)
    {
      var model = Mapper.Map<Invoice>(invoice);
      var changesMade = await _repository.UpdateAsync(invoice.Id, model);
      return Ok(changesMade);
    }

  }
}