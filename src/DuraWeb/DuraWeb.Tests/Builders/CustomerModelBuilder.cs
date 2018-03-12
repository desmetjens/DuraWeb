using DuraWeb.Application.Model;
using DuraWeb.Model;

namespace DuraWeb.Tests.Builders
{
  public class CustomerModelBuilder
  {
    private readonly CustomerModel _entity;

    public CustomerModel Default => WithFirstName("Jens")
      .WithLastName("Desmet")
      .WithVatNumber("BE0540722837")
      .WithTelephone("+32 498 38 96 33")
      .WithTitle(Title.Mr)
      .WithAddress(new AddressModelBuilder().Default)
      .Build();
    public CustomerModelBuilder()
    {
      _entity = new CustomerModel();
    }

    public CustomerModelBuilder WithId(int id)
    {
      _entity.Id = id;
      return this;
    }
    public CustomerModelBuilder WithFirstName(string name)
    {
      _entity.Firstname = name;
      return this;
    }

    public CustomerModelBuilder WithTitle(Title title)
    {
      _entity.Title = (int)title;
      return this;
    }
    public CustomerModelBuilder WithLastName(string name)
    {
      _entity.Lastname = name;
      return this;
    }

    public CustomerModelBuilder WithVatNumber(string vat)
    {
      _entity.VatNumber = vat;
      return this;
    }

    public CustomerModelBuilder WithTelephone(string tel)
    {
      _entity.Telephone = tel;
      return this;
    }

    public CustomerModelBuilder WithAddress(AddressModel address)
    {
      _entity.Address = address;
      return this;
    }

    public CustomerModel Build()
    {
      return _entity;
    }
  }
}