using System;
using System.Collections.Generic;

namespace DuraWeb.Application.Model
{
  public class InvoiceModel
  {
    public int Id { get; set; }
    public string Remarks { get; set; }
    public int InvoiceNumber { get; set; }

    private DateTime _date;
    public DateTime Date
    {
      get => _date;
      set
      {
        _date = value;
        ExpiryDate = Date.AddMonths(1);
      }
    }
    public DateTime ExpiryDate { get; private set; }
    public bool Paid { get; set; }
    public int VatRegime { get; set; }
    public int CustomerId { get; set; }

    public List<InvoiceItemModel> Items { get; set; }
  }
}