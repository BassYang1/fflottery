// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.ffmpegHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Web;

namespace Lottery.Utils
{
  public static class ffmpegHelp
  {
    public static bool Convert2Flv(string vFileName, string WidthAndHeight, string ExportName)
    {
      try
      {
        vFileName = HttpContext.Current.Server.MapPath(vFileName);
        ExportName = HttpContext.Current.Server.MapPath(ExportName);
        string str = " -i \"" + vFileName + "\" -y -ab 32 -ar 22050 -b 800000 -s " + WidthAndHeight + " \"" + ExportName + "\"";
        Process process = new Process();
        process.StartInfo.FileName = "ffmpeg.exe";
        process.StartInfo.Arguments = str;
        process.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/bin/tools/");
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        process.WaitForExit();
        process.Close();
        process.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public static string CatchImg(string vFileName, string FlvImgSize, string Second)
    {
      if (!File.Exists(HttpContext.Current.Server.MapPath(vFileName)))
        return "";
      try
      {
        string path = vFileName.Substring(0, vFileName.Length - 4) + "_thumb.jpg";
        string str = " -i \"" + HttpContext.Current.Server.MapPath(vFileName) + "\" -y -f image2 -ss " + Second + " -t 0.1 -s " + FlvImgSize + " \"" + HttpContext.Current.Server.MapPath(path) + "\"";
        Process process = new Process();
        process.StartInfo.FileName = "ffmpeg.exe";
        process.StartInfo.Arguments = str;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/bin/tools/");
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        process.WaitForExit();
        process.Close();
        process.Dispose();
        Thread.Sleep(4000);
        if (File.Exists(HttpContext.Current.Server.MapPath(path)))
          return path;
        return "";
      }
      catch
      {
        return "";
      }
    }
  }
}
