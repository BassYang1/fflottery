// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.jpfileHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Diagnostics;
using System.IO;
using System.Web;

namespace Lottery.Utils
{
  public static class jpfileHelp
  {
    public static bool FileCrypt(string oFileName, string EncodeOrDecode, string Password)
    {
      string path = HttpContext.Current.Server.MapPath("~/Bin/tools/jpfile.exe");
      if (!File.Exists(path) || !File.Exists(HttpContext.Current.Server.MapPath(oFileName)))
        return false;
      oFileName = HttpContext.Current.Server.MapPath(oFileName);
      string str = EncodeOrDecode + " \"" + oFileName + "\" \"" + Password + "\"";
      Process process = new Process();
      process.StartInfo.FileName = path;
      process.StartInfo.Arguments = str;
      process.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/Bin/");
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = false;
      process.Start();
      process.BeginErrorReadLine();
      process.WaitForExit();
      process.Close();
      process.Dispose();
      return true;
    }

    public static bool FileCrypt(string oFileName, string EncodeOrDecode)
    {
      return jpfileHelp.FileCrypt(oFileName, EncodeOrDecode, "12345678");
    }

    public static bool FileCrypt(string oFileName)
    {
      return jpfileHelp.FileCrypt(oFileName, "-d", "12345678");
    }
  }
}
