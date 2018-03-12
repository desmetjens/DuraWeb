using System;

namespace DuraWeb.Model
{
  public class InvoiceItem
  {
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public double Amount { get; set; }

    public virtual decimal TotalAmount => Math.Round((decimal)Amount * UnitPrice, 2);
  }
}