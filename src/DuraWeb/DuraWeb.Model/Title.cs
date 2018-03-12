using System.ComponentModel;

namespace DuraWeb.Model
{
  public enum Title
  {
    [Description("")]
    None = -1,
    [Description("Dhr. ")]
    Mr = 0,
    [Description("Mevr. ")]
    Mdm = 1,
    [Description("Dhr. & Mevr. ")]
    MrMdm = 2,
    [Description("BVBA ")]
    Bvba = 3,
    [Description("nv ")]
    Nv = 4
  }

  public static class TitleExtensions
  {
    public static string Description(this Title titel)
    {
      var fieldInfo = titel.GetType().GetField(titel.ToString());

      var attribArray = fieldInfo.GetCustomAttributes(false);

      if (attribArray.Length == 0)
        return titel.ToString();

      return attribArray[0] is DescriptionAttribute attrib ? attrib.Description : titel.ToString();
    }
  }
}