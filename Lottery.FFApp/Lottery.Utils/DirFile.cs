// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.DirFile
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace Lottery.Utils
{
  public static class DirFile
  {
    public static void CreateDir(string dir)
    {
      if (dir.Length == 0 || Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
        return;
      Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));
    }

    public static void CreateFolder(string folderPath)
    {
      if (Directory.Exists(folderPath))
        return;
      Directory.CreateDirectory(folderPath);
    }

    public static void DeleteDir(string dir)
    {
      if (dir.Length == 0 || !Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
        return;
      Directory.Delete(HttpContext.Current.Server.MapPath(dir), true);
    }

    public static bool FileExists(string file)
    {
      return File.Exists(HttpContext.Current.Server.MapPath(file));
    }

    public static string ReadFile(string file)
    {
      if (!DirFile.FileExists(file))
        return "";
      try
      {
        StreamReader streamReader = new StreamReader(HttpContext.Current.Server.MapPath(file), Encoding.UTF8);
        string end = streamReader.ReadToEnd();
        streamReader.Close();
        return end;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void SaveFile(string TxtStr, string tempDir)
    {
      DirFile.SaveFile(TxtStr, tempDir, true);
    }

    public static void SaveFile(string TxtStr, string tempDir, bool noBom)
    {
      try
      {
        DirFile.CreateDir(DirFile.GetFolderPath(true, tempDir));
        StreamWriter streamWriter = !noBom ? new StreamWriter(HttpContext.Current.Server.MapPath(tempDir), false, Encoding.UTF8) : new StreamWriter(HttpContext.Current.Server.MapPath(tempDir), false, (Encoding) new UTF8Encoding(false));
        streamWriter.Write(TxtStr);
        streamWriter.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void CopyFile(string file1, string file2, bool overwrite)
    {
      if (!File.Exists(HttpContext.Current.Server.MapPath(file1)))
        return;
      if (overwrite)
      {
        File.Copy(HttpContext.Current.Server.MapPath(file1), HttpContext.Current.Server.MapPath(file2), true);
      }
      else
      {
        if (File.Exists(HttpContext.Current.Server.MapPath(file2)))
          return;
        File.Copy(HttpContext.Current.Server.MapPath(file1), HttpContext.Current.Server.MapPath(file2));
      }
    }

    public static void DeleteFile(string file)
    {
      if (file.Length == 0 || !File.Exists(HttpContext.Current.Server.MapPath(file)))
        return;
      File.Delete(HttpContext.Current.Server.MapPath(file));
    }

    public static string GetFolderPath(string filePath)
    {
      return DirFile.GetFolderPath(false, filePath);
    }

    public static string GetFolderPath(bool isUrl, string filePath)
    {
      if (isUrl)
        return filePath.Substring(0, filePath.LastIndexOf("/") + 1);
      return filePath.Substring(0, filePath.LastIndexOf("\\") + 1);
    }

    public static string GetFileName(string filePath)
    {
      return DirFile.GetFileName(false, filePath);
    }

    public static string GetFileName(bool isUrl, string filePath)
    {
      if (isUrl)
        return filePath.Substring(filePath.LastIndexOf("/") + 1, filePath.Length - filePath.LastIndexOf("/") - 1);
      return filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.Length - filePath.LastIndexOf("\\") - 1);
    }

    public static string GetFileExt(string filePath)
    {
      return filePath.Substring(filePath.LastIndexOf(".") + 1, filePath.Length - filePath.LastIndexOf(".") - 1).ToLower();
    }

    public static void CopyDir(string OldDir, string NewDir)
    {
      DirFile.CopyDir(new DirectoryInfo(OldDir), new DirectoryInfo(NewDir));
    }

    private static void CopyDir(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
    {
      string path = NewDirectory.FullName + "\\" + OldDirectory.Name;
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      foreach (FileInfo file in OldDirectory.GetFiles())
        File.Copy(file.FullName, path + "\\" + file.Name, true);
      foreach (DirectoryInfo directory in OldDirectory.GetDirectories())
        DirFile.CopyDir(directory, new DirectoryInfo(path));
    }

    public static void DelDir(string OldDir)
    {
      new DirectoryInfo(OldDir).Delete(true);
    }

    public static void CopyAndDelDir(string OldDirectory, string NewDirectory)
    {
      DirFile.CopyDir(OldDirectory, NewDirectory);
      DirFile.DelDir(OldDirectory);
    }

    public static bool DownloadFile(HttpRequest _Request, HttpResponse _Response, string _fullPath, long _speed)
    {
      string fileName = DirFile.GetFileName(false, _fullPath);
      try
      {
        FileStream fileStream = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        BinaryReader binaryReader = new BinaryReader((Stream) fileStream);
        try
        {
          _Response.AddHeader("Accept-Ranges", "bytes");
          _Response.Buffer = false;
          long length = fileStream.Length;
          long offset = 0;
          double num1 = 10240.0;
          int millisecondsTimeout = (int) Math.Floor(1000.0 * num1 / (double) _speed) + 1;
          if (_Request.Headers["Range"] != null)
          {
            _Response.StatusCode = 206;
            offset = Convert.ToInt64(_Request.Headers["Range"].Split(new char[2]
            {
              '=',
              '-'
            })[1]);
          }
          _Response.AddHeader("Content-Length", (length - offset).ToString());
          _Response.AddHeader("Connection", "Keep-Alive");
          _Response.ContentType = "application/octet-stream";
          _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
          binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
          int num2 = (int) Math.Floor((double) (length - offset) / num1) + 1;
          for (int index = 0; index < num2; ++index)
          {
            if (_Response.IsClientConnected)
            {
              _Response.BinaryWrite(binaryReader.ReadBytes(int.Parse(num1.ToString())));
              Thread.Sleep(millisecondsTimeout);
            }
            else
              index = num2;
          }
        }
        catch
        {
          return false;
        }
        finally
        {
          binaryReader.Close();
          fileStream.Close();
        }
      }
      catch
      {
        return false;
      }
      return true;
    }
  }
}
