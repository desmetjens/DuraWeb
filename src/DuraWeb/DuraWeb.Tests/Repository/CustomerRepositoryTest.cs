using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DuraWeb.Application.Model;
using DuraWeb.EF.Repositories;
using DuraWeb.Model;
using NUnit.Framework;
using Shouldly;

namespace DuraWeb.Tests.Repository
{
  [TestFixture]
  public class CustomerRepositoryTest : DuraInMemoryTest
  {
    private readonly CustomerModel _model;

    public CustomerRepositoryTest()
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
        Title = (int)Title.Dhr,
        VatNumber = "BE0540722837"
      };
    }

    [Test]
    public async Task EnsureCustomerCreated()
    {
      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var customer = Mapper.Map<Customer>(_model);
        await repo.AddAsync(customer);
      }

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var all = await repo.GetAllAsync();

        var customer = all.First();
        customer.ShouldSatisfyAllConditions(
          () => customer.Firstname.ShouldBe(_model.Firstname),
          () => customer.Lastname.ShouldBe(_model.Lastname),
          () => customer.Telephone.ShouldBe(_model.Telephone),
          () => customer.VatNumber.ShouldBe(_model.VatNumber),
          () => ((int)customer.Title).ShouldBe(_model.Title)
          );
        customer.Address.ShouldSatisfyAllConditions(
          () => customer.Address.Bus.ShouldBe(_model.Address.Bus),
          () => customer.Address.City.ShouldBe(_model.Address.City),
          () => customer.Address.Street.ShouldBe(_model.Address.Street),
          () => customer.Address.PostalCode.ShouldBe(_model.Address.PostalCode),
          () => customer.Address.CustomerId.ShouldBe(customer.Id)
            );
      }
    }
  }
}