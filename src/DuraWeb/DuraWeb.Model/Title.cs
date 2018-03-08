using System.ComponentModel;

namespace DuraWeb.Model
{
  public enum Title
  {
    [Description("Dhr. ")]
    Dhr = 0,
    [Description("Mevr. ")]
    Mvr = 1,
    [Description("Dhr. & Mevr. ")]
    DhrMvr = 2,
    [Description("BVBA ")]
    Bvba = 3,
    [Description("nv ")]
    Nv = 4
  }
}