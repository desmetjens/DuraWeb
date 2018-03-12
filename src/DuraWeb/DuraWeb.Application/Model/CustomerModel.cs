namespace DuraWeb.Application.Model
{
  public class CustomerModel
  {
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Telephone { get; set; }
    public string VatNumber { get; set; }
    public int Title { get; set; }
    public int AddressId { get; set; }
    public AddressModel Address { get; set; }
  }
}