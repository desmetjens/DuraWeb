using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuraWeb.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuraWeb.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
      private readonly DuraContext _context;

      public CustomersController(DuraContext context)
      {
        _context = context;
      }


    }
}