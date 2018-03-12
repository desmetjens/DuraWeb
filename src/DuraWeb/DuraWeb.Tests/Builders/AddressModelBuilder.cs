using DuraWeb.Application.Model;

namespace DuraWeb.Tests.Builders
{
  public class AddressModelBuilder
  {
    protected AddressModel Entity;
    
    public AddressModel Default => WithStreet("Kruisbergstraat")
      .WithNumber("2")
      .WithBus("C")
      .WithPostalCode("8755")
      .WithCity("Ruiselede")
      .Build();

    public AddressModelBuilder()
    {
      Entity = new AddressModel();
    }

    public AddressModelBuilder WithId(int id)
    {
      Entity.Id = id;
      return this;
    }
    public AddressModelBuilder WithStreet(string street)
    {
      Entity.Street = street;
      return this;
    }

    public AddressModelBuilder WithPostalCode(string postalcode)
    {
      Entity.PostalCode = postalcode;
      return this;
    }

    public AddressModelBuilder WithCity(string city)
    {
      Entity.City = city;
      return this;
    }

    public AddressModelBuilder WithNumber(string number)
    {
      Entity.Number = number;
      return this;
    }

    public AddressModelBuilder WithBus(string bus)
    {
      Entity.Bus = bus;
      return this;
    }

    public AddressModel Build()
    {
      return Entity;
    }
  }


}