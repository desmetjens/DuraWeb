using System.Threading.Tasks;
using DuraWeb.Application.Model;
using DuraWeb.Application.Services;
using DuraWeb.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace DuraWeb.Tests
{
  [TestFixture]
  public class CustomerServiceTest : DuraInMemoryTest
  {
    private readonly CustomerModel _model;

    public CustomerServiceTest()
    {
      _model = new CustomerModel
      {
        Address = new AddressModel
        {
          City = "Ruiselede",
          Number = "2c",
          PostalCode = "8755",
          Street = "Kruisbergstraat"
        },
        Firstname = "Jens",
        Lastname = "Desmet",
        Telephone = "0498 38 96 33",
        Title = (int) Title.Dhr,
        VatNumber = "BE0540722837"
      };
    }
    [Test]
    public async Task EnsureCustomerCreated()
    {
      using (var ctx = CreateContext())
      {
        var service = new CustomerService(ctx);
        await service.CreateOrUpdateAsync(_model);
      }

      using (var ctx = CreateContext())
      {

        var count = await ctx.Customers.CountAsync();
        count.ShouldBe(1);

      }
    }
  }
}