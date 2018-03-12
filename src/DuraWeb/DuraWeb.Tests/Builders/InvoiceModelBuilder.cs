using System;
using System.Collections.Generic;
using DuraWeb.Application.Model;
using DuraWeb.Model;

namespace DuraWeb.Tests.Builders
{
  public class InvoiceModelBuilder
  {
    protected InvoiceModel Entity;

    public InvoiceModel Default => WithInvoiceNumber(1)
      .WithDate(new DateTime(2018, 1, 1))
      .WithPaid()
      .WithRemarks("remarks")
      .WithVat(VatRegime.Delivery)
      .WithItems(new List<InvoiceItemModel>
      {
        new InvoiceItemModelBuilder().WithDescription("Item 1").WithAmount(5d).WithUnitPrice(500.6m).Build(),
        new InvoiceItemModelBuilder().WithDescription("Item 2").WithAmount(1d).WithUnitPrice(65.88m).Build()
      })
      .Build();

    public InvoiceModelBuilder()
    {
      Entity = new InvoiceModel();
    }

    public InvoiceModelBuilder WithId(int id)
    {
      Entity.Id = id;
      return this;
    }
    public InvoiceModelBuilder WithDate(DateTime date)
    {
      Entity.Date = date;
      return this;
    }

    public InvoiceModelBuilder WithInvoiceNumber(int number)
    {
      Entity.InvoiceNumber = number;
      return this;
    }

    public InvoiceModelBuilder WithRemarks(string remarks)
    {
      Entity.Remarks = remarks;
      return this;
    }
    public InvoiceModelBuilder WithVat(VatRegime regime)
    {
      Entity.VatRegime = (int)regime;
      return this;
    }

    public InvoiceModelBuilder WithPaid()
    {
      Entity.Paid = true;
      return this;
    }

    public InvoiceModelBuilder WithItems(List<InvoiceItemModel> items)
    {
      Entity.Items = items;
      return this;
    }

    public InvoiceModel Build()
    {
      return Entity;
    }
  }


}