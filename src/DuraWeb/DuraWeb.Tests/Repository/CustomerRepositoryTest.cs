using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DuraWeb.Application.Model;
using DuraWeb.EF.Repositories;
using DuraWeb.Model;
using DuraWeb.Tests.Builders;
using NUnit.Framework;
using Shouldly;

namespace DuraWeb.Tests.Repository
{
  [TestFixture]
  public class CustomerRepositoryTest : DuraInMemoryTest
  {
   
    [Test]
    public async Task EnsureCustomerCreated()
    {
      var model = new CustomerModelBuilder().Default;
      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var customer = Mapper.Map<Customer>(model);
        await repo.AddAsync(customer);
      }

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var all = await repo.GetAllAsync();

        var customer = all.First();
        CheckCustomer(customer, model);
      }
    }


    [Test]
    public async Task EnsureCustomerUpdated()
    {
      var model = new CustomerModelBuilder().Default;

      CustomerModel toUpdate;
      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var entity = Mapper.Map<Customer>(model);
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
      var model = new CustomerModelBuilder().Default;

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var entity = Mapper.Map<Customer>(model);
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
      var model = new CustomerModelBuilder().Default;

      using (var ctx = CreateContext())
      {
        ctx.Customers.RemoveRange(ctx.Customers);
        await ctx.SaveChangesAsync();

        var repo = new CustomerRepository(ctx);
        var customer = Mapper.Map<Customer>(model);
        await repo.AddAsync(customer);
      }

      await EnsureSearch(model.Lastname);
      await EnsureSearch(model.Firstname);
      await EnsureSearch($"{model.Firstname} {model.Lastname}");
      await EnsureSearch($"{model.Lastname} {model.Firstname}");
      await EnsureSearch(model.VatNumber);
      await EnsureSearch(model.Telephone);
      await EnsureSearch(model.Address.Street);
      await EnsureSearch($"{model.Address.Street} {model.Address.Number}");
      await EnsureSearch(model.Address.PostalCode);
      await EnsureSearch(model.Address.City);
    }


    private async Task EnsureSearch(string criteria)
    {
      var model = new CustomerModelBuilder().Default;

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var all = await repo.SearchAsync(criteria, 10, 0);

        var customer = all.First();
        CheckCustomer(customer, model);
      }
    }

  }
}