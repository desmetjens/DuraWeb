using System;
using System.Collections.Generic;
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
  public class InvoiceRepositoryTest : DuraInMemoryTest
  {

    [Test]
    public async Task EnsureInvoiceCreated()
    {
      var model = new InvoiceModelBuilder().Default;
      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var customer = Mapper.Map<Customer>(new CustomerModelBuilder().Default);
        await repo.AddAsync(customer);
        var first = ctx.Customers.First();
        model.CustomerId = first.Id;
        var invoiceRepository = new InvoiceRepository(ctx);
        await invoiceRepository.AddAsync(Mapper.Map<Invoice>(model));
      }

      using (var ctx = CreateContext())
      {
        var repo = new CustomerRepository(ctx);
        var customer = await repo.GetAsync(model.CustomerId);

        var invoices = customer.Invoices;
        invoices.Count.ShouldBe(1);

      }
    }





  }
}