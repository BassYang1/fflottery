// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.swftoolsHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lottery.Utils
{
  public static class swftoolsHelp
  {
    public static bool PDF2SWF(string pdfPath, string swfPath)
    {
      return swftoolsHelp.PDF2SWF(pdfPath, swfPath, 1, swftoolsHelp.GetPageCount(HttpContext.Current.Server.MapPath(pdfPath)), 80);
    }

    public static bool PDF2SWF(string pdfPath, string swfPath, int page)
    {
      return swftoolsHelp.PDF2SWF(pdfPath, swfPath, 1, page, 80);
    }

    public static bool PDF2SWF(string pdfPath, string swfPath, int beginpage, int endpage, int photoQuality)
    {
      string path = HttpContext.Current.Server.MapPath("~/Bin/tools/pdf2swf-0.9.1.exe");
      pdfPath = HttpContext.Current.Server.MapPath(pdfPath);
      swfPath = HttpContext.Current.Server.MapPath(swfPath);
      if (!File.Exists(path) || !File.Exists(pdfPath))
        return false;
      if (File.Exists(swfPath))
        File.Delete(swfPath);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(" \"" + pdfPath + "\"");
      stringBuilder.Append(" -o \"" + swfPath + "\"");
      stringBuilder.Append(" -s flashversion=9");
      if (endpage > swftoolsHelp.GetPageCount(pdfPath))
        endpage = swftoolsHelp.GetPageCount(pdfPath);
      stringBuilder.Append(" -p \"" + (object) beginpage + "-" + (object) endpage + "\"");
      stringBuilder.Append(" -j " + (object) photoQuality);
      string str = stringBuilder.ToString();
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
      return File.Exists(swfPath);
    }

    public static int GetPageCount(string pdfPath)
    {
      byte[] bytes = File.ReadAllBytes(pdfPath);
      if (bytes == null || bytes.Length <= 0)
        return -1;
      return new Regex("/Type\\s*/Page[^s]").Matches(Encoding.Default.GetString(bytes)).Count;
    }
  }
}
