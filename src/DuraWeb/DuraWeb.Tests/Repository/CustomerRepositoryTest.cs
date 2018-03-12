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
        CheckCustomer(customer, _model);
      }
    }


    [Test]
    public async Task EnsureCustomerUpdated()
    {
      CustomerModel toUpdate;
      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var entity = Mapper.Map<Customer>(_model);
        await repo.AddAsync(entity);

        var all = await repo.GetAllAsync();
        var customer = all.First();

        toUpdate = Mapper.Map<CustomerModel>(customer);
        toUpdate.Lastname = "Updated";
        toUpdate.Address.Street = "AlsoUpdated";

        var updated = Mapper.Map<Customer>(toUpdate);
        await repo.UpdateAsync(updated.Id, updated);
      }

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var all = await repo.GetAllAsync();

        var customer = all.First();
        CheckCustomer(customer, toUpdate);
      }
    }


    [Test]
    public async Task EnsureCustomerDeleted()
    {
      Customer customer;

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var entity = Mapper.Map<Customer>(_model);
        await repo.AddAsync(entity);

        var all = await repo.GetAllAsync();
        customer = all.First();
        await repo.DeleteAsync(customer.Id);
      }

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var found = await repo.GetAsync(customer.Id);

        found.ShouldBeNull();
      }
    }

    protected virtual void CheckCustomer(Customer customer, CustomerModel model)
    {
      customer.ShouldSatisfyAllConditions(
        () => customer.Firstname.ShouldBe(model.Firstname),
        () => customer.Lastname.ShouldBe(model.Lastname),
        () => customer.Telephone.ShouldBe(model.Telephone),
        () => customer.VatNumber.ShouldBe(model.VatNumber),
        () => ((int)customer.Title).ShouldBe(model.Title)
      );
      customer.Address.ShouldSatisfyAllConditions(
        () => customer.Address.Bus.ShouldBe(model.Address.Bus),
        () => customer.Address.City.ShouldBe(model.Address.City),
        () => customer.Address.Street.ShouldBe(model.Address.Street),
        () => customer.Address.PostalCode.ShouldBe(model.Address.PostalCode),
        () => customer.Address.CustomerId.ShouldBe(customer.Id)
      );
    }

    [Test]
    public async Task EnsureSearchOnAllCriteria()
    {
      using (var ctx = CreateContext())
      {
        ctx.Customers.RemoveRange(ctx.Customers);
        await ctx.SaveChangesAsync();

        var repo = new CustomerRepository(ctx);
        var customer = Mapper.Map<Customer>(_model);
        await repo.AddAsync(customer);
      }

      await EnsureSearch(_model.Lastname);
      await EnsureSearch(_model.Firstname);
      await EnsureSearch($"{_model.Firstname} {_model.Lastname}");
      await EnsureSearch($"{_model.Lastname} {_model.Firstname}");
      await EnsureSearch(_model.VatNumber);
      await EnsureSearch(_model.Telephone);
      await EnsureSearch(_model.Address.Street);
      await EnsureSearch($"{_model.Address.Street} {_model.Address.Number}");
      await EnsureSearch(_model.Address.PostalCode);
      await EnsureSearch(_model.Address.City);
    }


    private async Task EnsureSearch(string criteria)
    {
      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var all = await repo.SearchAsync(criteria, 10, 0);

        var customer = all.First();
        CheckCustomer(customer, _model);
      }
    }

  }
}