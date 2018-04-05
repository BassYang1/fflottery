// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.FileValidation
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.IO;
using System.Text;
using System.Web;

namespace Lottery.Utils
{
  public static class FileValidation
  {
    public static bool IsAllowedExtension(HttpPostedFile oFile, FileExtension[] fileEx)
    {
      int contentLength = oFile.ContentLength;
      byte[] buffer = new byte[contentLength];
      oFile.InputStream.Read(buffer, 0, contentLength);
      MemoryStream memoryStream = new MemoryStream(buffer);
      BinaryReader binaryReader = new BinaryReader((Stream) memoryStream);
      string s = "";
      try
      {
        s = binaryReader.ReadByte().ToString();
        byte num = binaryReader.ReadByte();
        s += num.ToString();
      }
      catch
      {
      }
      binaryReader.Close();
      memoryStream.Close();
      foreach (FileExtension fileExtension in fileEx)
      {
        if ((FileExtension) int.Parse(s) == fileExtension)
          return true;
      }
      return false;
    }

    public static bool IsSecureUploadPhoto(HttpPostedFile oFile)
    {
      bool flag = false;
      string lower = Path.GetExtension(oFile.FileName).ToLower();
      string[] strArray = new string[5]
      {
        ".gif",
        ".png",
        ".jpeg",
        ".jpg",
        ".bmp"
      };
      foreach (string str in strArray)
      {
        if (lower == str)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return true;
      FileExtension[] fileEx = new FileExtension[4]
      {
        FileExtension.BMP,
        FileExtension.GIF,
        FileExtension.JPG,
        FileExtension.PNG
      };
      return FileValidation.IsAllowedExtension(oFile, fileEx);
    }

    public static bool IsSecureUpfilePhoto(string photoFile)
    {
      bool flag = false;
      string str1 = "Yes";
      string lower = Path.GetExtension(photoFile).ToLower();
      string[] strArray = new string[5]
      {
        ".gif",
        ".png",
        ".jpeg",
        ".jpg",
        ".bmp"
      };
      foreach (string str2 in strArray)
      {
        if (lower == str2)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return true;
      StreamReader streamReader = new StreamReader(photoFile, Encoding.Default);
      string end = streamReader.ReadToEnd();
      streamReader.Close();
      string str3 = "request|<script|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=";
      char[] chArray = new char[1]{ '|' };
      foreach (string str2 in str3.Split(chArray))
      {
        if (end.ToLower().IndexOf(str2) != -1)
        {
          File.Delete(photoFile);
          str1 = "No";
          break;
        }
      }
      return str1 == "Yes";
    }
  }
}
