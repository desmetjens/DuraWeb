using DuraWeb.Application.Model;

namespace DuraWeb.Tests.Builders
{
  public class InvoiceItemModelBuilder
  {
    protected InvoiceItemModel Entity;
    
    public InvoiceItemModelBuilder()
    {
      Entity = new InvoiceItemModel();
    }

    public InvoiceItemModelBuilder WithId(int id)
    {
      Entity.Id = id;
      return this;
    }
    public InvoiceItemModelBuilder WithAmount(double amount)
    {
      Entity.Amount = amount;
      return this;
    }

    public InvoiceItemModelBuilder WithUnitPrice(decimal unitPrice)
    {
      Entity.UnitPrice = unitPrice;
      return this;
    }

    public InvoiceItemModelBuilder WithDescription(string description)
    {
      Entity.Description = description;
      return this;
    }

  
    public InvoiceItemModel Build()
    {
      return Entity;
    }
  }


}