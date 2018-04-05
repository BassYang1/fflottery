// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.IPSearchHelp.SearchIndex
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lottery.Utils.IPSearchHelp
{
  public class SearchIndex
  {
    private static object lockHelper = new object();
    private static SearchIndex.PHCZIP pcz = new SearchIndex.PHCZIP();
    private static string filePath = "";
    private static bool fileIsExsit = true;

    static SearchIndex()
    {
      SearchIndex.filePath = HttpContext.Current.Server.MapPath("/statics/data/ipdata.config");
      SearchIndex.pcz.SetDbFilePath(SearchIndex.filePath);
    }

    public static string GetIPLocation(string IPValue)
    {
      lock (SearchIndex.lockHelper)
      {
        string addressWithIp = SearchIndex.pcz.GetAddressWithIP(IPValue.Trim());
        if (!SearchIndex.fileIsExsit)
          return (string) null;
        if (addressWithIp.IndexOf("IANA") >= 0)
          return "";
        return addressWithIp;
      }
    }

    public class CZ_INDEX_INFO
    {
      public uint IpSet;
      public uint IpEnd;
      public uint Offset;

      public CZ_INDEX_INFO()
      {
        this.IpSet = 0U;
        this.IpEnd = 0U;
        this.Offset = 0U;
      }
    }

    public class PHCZIP
    {
      protected bool bFilePathInitialized;
      protected string FilePath;
      protected FileStream FileStrm;
      protected uint Index_Set;
      protected uint Index_End;
      protected uint Index_Count;
      protected uint Search_Index_Set;
      protected uint Search_Index_End;
      protected SearchIndex.CZ_INDEX_INFO Search_Set;
      protected SearchIndex.CZ_INDEX_INFO Search_Mid;
      protected SearchIndex.CZ_INDEX_INFO Search_End;

      public PHCZIP()
      {
        this.bFilePathInitialized = false;
      }

      public PHCZIP(string dbFilePath)
      {
        this.bFilePathInitialized = false;
        this.SetDbFilePath(dbFilePath);
      }

      public void Initialize()
      {
        this.Search_Index_Set = 0U;
        this.Search_Index_End = this.Index_Count - 1U;
      }

      public void Dispose()
      {
        if (!this.bFilePathInitialized)
          return;
        this.bFilePathInitialized = false;
        this.FileStrm.Close();
        this.FileStrm.Dispose();
      }

      public bool SetDbFilePath(string dbFilePath)
      {
        if (dbFilePath == "")
          return false;
        try
        {
          this.FileStrm = new FileStream(dbFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        catch
        {
          return false;
        }
        if (this.FileStrm.Length < 8L)
        {
          this.FileStrm.Close();
          this.FileStrm.Dispose();
          return false;
        }
        this.FileStrm.Seek(0L, SeekOrigin.Begin);
        this.Index_Set = this.GetUInt32();
        this.Index_End = this.GetUInt32();
        this.Index_Count = (this.Index_End - this.Index_Set) / 7U + 1U;
        this.bFilePathInitialized = true;
        return true;
      }

      public string GetAddressWithIP(string IPValue)
      {
        if (!this.bFilePathInitialized)
          return "";
        this.Initialize();
        uint uint32 = this.IPToUInt32(IPValue);
        while (true)
        {
          this.Search_Set = this.IndexInfoAtPos(this.Search_Index_Set);
          this.Search_End = this.IndexInfoAtPos(this.Search_Index_End);
          if (uint32 < this.Search_Set.IpSet || uint32 > this.Search_Set.IpEnd)
          {
            if (uint32 < this.Search_End.IpSet || uint32 > this.Search_End.IpEnd)
            {
              this.Search_Mid = this.IndexInfoAtPos((this.Search_Index_End + this.Search_Index_Set) / 2U);
              if (uint32 < this.Search_Mid.IpSet || uint32 > this.Search_Mid.IpEnd)
              {
                if (uint32 < this.Search_Mid.IpSet)
                  this.Search_Index_End = (this.Search_Index_End + this.Search_Index_Set) / 2U;
                else
                  this.Search_Index_Set = (this.Search_Index_End + this.Search_Index_Set) / 2U;
              }
              else
                goto label_8;
            }
            else
              goto label_6;
          }
          else
            break;
        }
        return this.ReadAddressInfoAtOffset(this.Search_Set.Offset);
label_6:
        return this.ReadAddressInfoAtOffset(this.Search_End.Offset);
label_8:
        return this.ReadAddressInfoAtOffset(this.Search_Mid.Offset);
      }

      private string ReadAddressInfoAtOffset(uint Offset)
      {
        this.FileStrm.Seek((long) (Offset + 4U), SeekOrigin.Begin);
        string str1;
        string str2;
        switch (this.GetTag())
        {
          case 1:
            this.FileStrm.Seek((long) this.GetOffset(), SeekOrigin.Begin);
            if ((int) this.GetTag() == 2)
            {
              uint offset = this.GetOffset();
              str1 = this.ReadArea();
              this.FileStrm.Seek((long) offset, SeekOrigin.Begin);
              str2 = this.ReadString();
              break;
            }
            this.FileStrm.Seek(-1L, SeekOrigin.Current);
            str2 = this.ReadString();
            str1 = this.ReadArea();
            break;
          case 2:
            uint offset1 = this.GetOffset();
            str1 = this.ReadArea();
            this.FileStrm.Seek((long) offset1, SeekOrigin.Begin);
            str2 = this.ReadString();
            break;
          default:
            this.FileStrm.Seek(-1L, SeekOrigin.Current);
            str2 = this.ReadString();
            str1 = this.ReadArea();
            break;
        }
        return str2 + " " + str1;
      }

      private uint GetOffset()
      {
        return BitConverter.ToUInt32(new byte[4]
        {
          (byte) this.FileStrm.ReadByte(),
          (byte) this.FileStrm.ReadByte(),
          (byte) this.FileStrm.ReadByte(),
          (byte) 0
        }, 0);
      }

      protected string ReadArea()
      {
        switch (this.GetTag())
        {
          case 1:
          case 2:
            this.FileStrm.Seek((long) this.GetOffset(), SeekOrigin.Begin);
            return this.ReadString();
          default:
            this.FileStrm.Seek(-1L, SeekOrigin.Current);
            return this.ReadString();
        }
      }

      protected string ReadString()
      {
        uint num = 0;
        byte[] bytes = new byte[256];
        bytes[num] = (byte) this.FileStrm.ReadByte();
        while ((int) bytes[num] != 0)
        {
          ++num;
          bytes[num] = (byte) this.FileStrm.ReadByte();
        }
        return Encoding.Default.GetString(bytes).TrimEnd(new char[1]);
      }

      protected byte GetTag()
      {
        return (byte) this.FileStrm.ReadByte();
      }

      protected SearchIndex.CZ_INDEX_INFO IndexInfoAtPos(uint Index_Pos)
      {
        SearchIndex.CZ_INDEX_INFO czIndexInfo = new SearchIndex.CZ_INDEX_INFO();
        this.FileStrm.Seek((long) (this.Index_Set + 7U * Index_Pos), SeekOrigin.Begin);
        czIndexInfo.IpSet = this.GetUInt32();
        czIndexInfo.Offset = this.GetOffset();
        this.FileStrm.Seek((long) czIndexInfo.Offset, SeekOrigin.Begin);
        czIndexInfo.IpEnd = this.GetUInt32();
        return czIndexInfo;
      }

      public uint IPToUInt32(string IpValue)
      {
        string[] strArray = IpValue.Split('.');
        int upperBound = strArray.GetUpperBound(0);
        if (upperBound != 3)
        {
          strArray = new string[4];
          for (int index = 1; index <= 3 - upperBound; ++index)
            strArray[upperBound + index] = "0";
        }
        byte[] numArray = new byte[4];
        for (int index = 0; index <= 3; ++index)
        {
          if (this.IsNumeric(strArray[index]))
            numArray[3 - index] = (byte) (Convert.ToInt32(strArray[index]) & (int) byte.MaxValue);
        }
        return BitConverter.ToUInt32(numArray, 0);
      }

      protected bool IsNumeric(string str)
      {
        return str != null && Regex.IsMatch(str, "^-?\\d+$");
      }

      protected uint GetUInt32()
      {
        byte[] buffer = new byte[4];
        this.FileStrm.Read(buffer, 0, 4);
        return BitConverter.ToUInt32(buffer, 0);
      }
    }
  }
}
