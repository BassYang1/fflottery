// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CKEditorToolbar
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

namespace Lottery.Utils
{
  public static class CKEditorToolbar
  {
    public static object[] Simple
    {
      get
      {
        return new object[2]
        {
          (object) new object[8]
          {
            (object) "Source",
            (object) "-",
            (object) "JustifyLeft",
            (object) "JustifyCenter",
            (object) "JustifyRight",
            (object) "-",
            (object) "Styles",
            (object) "FontSize"
          },
          (object) new object[8]
          {
            (object) "Bold",
            (object) "Italic",
            (object) "-",
            (object) "NumberedList",
            (object) "BulletedList",
            (object) "-",
            (object) "Link",
            (object) "Unlink"
          }
        };
      }
    }

    public static object[] Basic
    {
      get
      {
        return new object[1]
        {
          (object) new object[8]
          {
            (object) "Bold",
            (object) "Italic",
            (object) "-",
            (object) "OrderedList",
            (object) "UnorderedList",
            (object) "-",
            (object) "Link",
            (object) "Unlink"
          }
        };
      }
    }
  }
}
