// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.IPScaner
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public class IPScaner
  {
    private string country;
    private int countryFlag;
    private string dataPath;
    private long endIp;
    private long endIpOff;
    private string errMsg;
    private long firstStartIp;
    private string ip;
    private long lastStartIp;
    private string local;
    private FileStream objfs;
    private long startIp;

    private string GetCountry()
    {
      switch (this.countryFlag)
      {
        case 1:
        case 2:
          this.country = this.GetFlagStr(this.endIpOff + 4L);
          this.local = 1 == this.countryFlag ? " " : this.GetFlagStr(this.endIpOff + 8L);
          break;
        default:
          this.country = this.GetFlagStr(this.endIpOff + 4L);
          this.local = this.GetFlagStr(this.objfs.Position);
          break;
      }
      return " ";
    }

    private long GetEndIp()
    {
      this.objfs.Position = this.endIpOff;
      byte[] buffer = new byte[5];
      this.objfs.Read(buffer, 0, 5);
      this.endIp = Convert.ToInt64(buffer[0].ToString()) + Convert.ToInt64(buffer[1].ToString()) * 256L + Convert.ToInt64(buffer[2].ToString()) * 256L * 256L + Convert.ToInt64(buffer[3].ToString()) * 256L * 256L * 256L;
      this.countryFlag = (int) buffer[4];
      return this.endIp;
    }

    private string GetFlagStr(long offSet)
    {
      byte[] buffer = new byte[3];
      while (true)
      {
        this.objfs.Position = offSet;
        int num = this.objfs.ReadByte();
        switch (num)
        {
          case 1:
          case 2:
            this.objfs.Read(buffer, 0, 3);
            if (num == 2)
            {
              this.countryFlag = 2;
              this.endIpOff = offSet - 4L;
            }
            offSet = Convert.ToInt64(buffer[0].ToString()) + Convert.ToInt64(buffer[1].ToString()) * 256L + Convert.ToInt64(buffer[2].ToString()) * 256L * 256L;
            continue;
          default:
            goto label_2;
        }
      }
label_2:
      if (offSet < 12L)
        return " ";
      this.objfs.Position = offSet;
      return this.GetStr();
    }

    private long GetStartIp(long recNO)
    {
      this.objfs.Position = this.firstStartIp + recNO * 7L;
      byte[] buffer = new byte[7];
      this.objfs.Read(buffer, 0, 7);
      this.endIpOff = Convert.ToInt64(buffer[4].ToString()) + Convert.ToInt64(buffer[5].ToString()) * 256L + Convert.ToInt64(buffer[6].ToString()) * 256L * 256L;
      this.startIp = Convert.ToInt64(buffer[0].ToString()) + Convert.ToInt64(buffer[1].ToString()) * 256L + Convert.ToInt64(buffer[2].ToString()) * 256L * 256L + Convert.ToInt64(buffer[3].ToString()) * 256L * 256L * 256L;
      return this.startIp;
    }

    private string GetStr()
    {
      string str = "";
      byte[] bytes = new byte[2];
      while (true)
      {
        byte num1 = (byte) this.objfs.ReadByte();
        if ((int) num1 != 0)
        {
          if ((int) num1 > (int) sbyte.MaxValue)
          {
            byte num2 = (byte) this.objfs.ReadByte();
            bytes[0] = num1;
            bytes[1] = num2;
            Encoding encoding = Encoding.GetEncoding("GB2312");
            str += encoding.GetString(bytes);
          }
          else
            str += (string) (object) (char) num1;
        }
        else
          break;
      }
      return str;
    }

    private string IntToIP(long ip_Int)
    {
      long num1 = (ip_Int & 4278190080L) >> 24;
      if (num1 < 0L)
        num1 += 256L;
      long num2 = (ip_Int & 16711680L) >> 16;
      if (num2 < 0L)
        num2 += 256L;
      long num3 = (ip_Int & 65280L) >> 8;
      if (num3 < 0L)
        num3 += 256L;
      long num4 = ip_Int & (long) byte.MaxValue;
      if (num4 < 0L)
        num4 += 256L;
      return num1.ToString() + "." + num2.ToString() + "." + num3.ToString() + "." + num4.ToString();
    }

    public string IPLocation()
    {
      this.QQwry();
      return this.country + this.local;
    }

    public string IPLocation(string dataPath, string ip)
    {
      this.dataPath = dataPath;
      this.ip = ip;
      this.QQwry();
      return this.country + this.local;
    }

    private long IpToInt(string ip)
    {
      char[] chArray = new char[1]{ '.' };
      if (ip.Split(chArray).Length == 3)
        ip += ".0";
      string[] strArray = ip.Split(chArray);
      return long.Parse(strArray[0]) * 256L * 256L * 256L + long.Parse(strArray[1]) * 256L * 256L + long.Parse(strArray[2]) * 256L + long.Parse(strArray[3]);
    }

    private int QQwry()
    {
      if (!new Regex("(((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))\\.){3}((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))").Match(this.ip).Success)
      {
        this.errMsg = "IP格式错误";
        return 4;
      }
      long num1 = this.IpToInt(this.ip);
      int num2 = 0;
      if (num1 >= this.IpToInt("127.0.0.0") && num1 <= this.IpToInt("127.255.255.255"))
      {
        this.country = "本机内部环回地址";
        this.local = "";
        num2 = 1;
      }
      else if (num1 >= this.IpToInt("0.0.0.0") && num1 <= this.IpToInt("2.255.255.255") || num1 >= this.IpToInt("64.0.0.0") && num1 <= this.IpToInt("126.255.255.255") || num1 >= this.IpToInt("58.0.0.0") && num1 <= this.IpToInt("60.255.255.255"))
      {
        this.country = "网络保留地址";
        this.local = "";
        num2 = 1;
      }
      this.objfs = new FileStream(this.dataPath, FileMode.Open, FileAccess.Read);
      try
      {
        this.objfs.Position = 0L;
        byte[] buffer = new byte[8];
        this.objfs.Read(buffer, 0, 8);
        this.firstStartIp = (long) ((int) buffer[0] + (int) buffer[1] * 256 + (int) buffer[2] * 256 * 256 + (int) buffer[3] * 256 * 256 * 256);
        this.lastStartIp = (long) ((int) buffer[4] + (int) buffer[5] * 256 + (int) buffer[6] * 256 * 256 + (int) buffer[7] * 256 * 256 * 256);
        long int64 = Convert.ToInt64((double) (this.lastStartIp - this.firstStartIp) / 7.0);
        if (int64 <= 1L)
        {
          this.country = "FileDataError";
          this.objfs.Close();
          return 2;
        }
        long num3 = int64;
        long recNO1 = 0;
        while (recNO1 < num3 - 1L)
        {
          long recNO2 = (num3 + recNO1) / 2L;
          this.GetStartIp(recNO2);
          if (num1 == this.startIp)
          {
            recNO1 = recNO2;
            break;
          }
          if (num1 > this.startIp)
            recNO1 = recNO2;
          else
            num3 = recNO2;
        }
        this.GetStartIp(recNO1);
        this.GetEndIp();
        if (this.startIp <= num1 && this.endIp >= num1)
        {
          this.GetCountry();
          this.local = this.local.Replace("（我们一定要解放台湾！！！）", "");
        }
        else
        {
          num2 = 3;
          this.country = "未知";
          this.local = "";
        }
        this.objfs.Close();
        return num2;
      }
      catch
      {
        return 1;
      }
    }

    public string Country
    {
      get
      {
        return this.country;
      }
    }

    public string DataPath
    {
      set
      {
        this.dataPath = value;
      }
    }

    public string ErrMsg
    {
      get
      {
        return this.errMsg;
      }
    }

    public string IP
    {
      set
      {
        this.ip = value;
      }
    }

    public string Local
    {
      get
      {
        return this.local;
      }
    }
  }
}
