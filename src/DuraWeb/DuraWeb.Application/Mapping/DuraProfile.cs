using AutoMapper;
using DuraWeb.Application.Model;
using DuraWeb.Model;

namespace DuraWeb.Application.Mapping
{
  public class DuraProfile : Profile
  {
    public DuraProfile()
    {
      CreateMap<CustomerModel, Customer>()
        .ForMember(x => x.Title, opt => opt.MapFrom(s => (Title)s.Title));
      CreateMap<Customer, CustomerModel>();

      CreateMap<AddressModel, Address>();
      CreateMap<Address, AddressModel>();
    }
  }
}