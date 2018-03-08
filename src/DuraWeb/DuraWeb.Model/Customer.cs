using System.Collections.Generic;

namespace DuraWeb.Model
{
  public class Customer
  {
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Telephone { get; set; }
    public string VatNumber { get; set; }
    public Title Title { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }
  }
}
