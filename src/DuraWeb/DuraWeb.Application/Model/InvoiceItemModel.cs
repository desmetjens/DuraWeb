namespace DuraWeb.Application.Model
{
  public class InvoiceItemModel
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public double Amount { get; set; }
    public decimal TotalAmount { get; set; }
  }
}