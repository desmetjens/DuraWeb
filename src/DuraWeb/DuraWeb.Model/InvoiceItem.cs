namespace DuraWeb.Model
{
  public class InvoiceItem
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public double Amount { get; set; }
  }
}