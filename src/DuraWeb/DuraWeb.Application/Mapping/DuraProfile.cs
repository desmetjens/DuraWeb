using AutoMapper;
using DuraWeb.Application.Model;
using DuraWeb.Model;

namespace DuraWeb.Application.Mapping
{
  public class DuraProfile : Profile
  {
    public DuraProfile()
    {
      CreateMap<CustomerModel, Customer>();
      CreateMap<Customer, CustomerModel>()
        .ForMember(x => x.FullName, opt => opt.MapFrom(s => s.ToString()));

      CreateMap<AddressModel, Address>();
      CreateMap<Address, AddressModel>();

      CreateMap<Invoice, InvoiceModel>();
      CreateMap<InvoiceModel, Invoice>();

      CreateMap<InvoiceItem, InvoiceItemModel>();
      CreateMap<InvoiceItemModel, InvoiceItem>();
    }
  }
}