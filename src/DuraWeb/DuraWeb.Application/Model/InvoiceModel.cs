using System;
using System.Collections.Generic;

namespace DuraWeb.Application.Model
{
  public class InvoiceModel
  {
    public int Id { get; set; }
    public string Remarks { get; set; }
    public int InvoiceNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Paid { get; set; }
    public int VatRegime { get; set; }
    public int CustomerId { get; set; }

    public List<InvoiceItemModel> Items { get; set; }
  }
}