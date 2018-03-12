namespace DuraWeb.Model
{
  public class Address
  {
    public int Id { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Bus { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
  }
}