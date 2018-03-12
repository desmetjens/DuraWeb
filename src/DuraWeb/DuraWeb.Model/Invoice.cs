using System;
using System.Collections.Generic;

namespace DuraWeb.Model
{
  public class Invoice
  {
    public int Id { get; set; }
    public string Remarks { get; set; }
    public int InvoiceNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime ExpiryDate { get; set; }
    public byte[] File { get; set; }
    public bool Paid { get; set; }
    public VatRegime VatRegime { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public virtual ICollection<InvoiceItem> Items { get; set; }

  }
}