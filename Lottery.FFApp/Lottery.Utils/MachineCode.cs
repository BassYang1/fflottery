// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.MachineCode
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Lottery.Utils
{
  public static class MachineCode
  {
    public static int[] intCode = new int[(int) sbyte.MaxValue];
    public static int[] intNumber = new int[64];
    public static char[] Charcode = new char[64];

    public static void setIntCode()
    {
      for (int index = 1; index < MachineCode.intCode.Length; ++index)
        MachineCode.intCode[index] = index % 9;
    }

    public static string GetDiskVolumeSerialNumber()
    {
      ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
      ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
      managementObject.Get();
      return managementObject.GetPropertyValue("VolumeSerialNumber").ToString();
    }

    public static string getCpu()
    {
      string str = (string) null;
      using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementClass("win32_Processor").GetInstances().GetEnumerator())
      {
        if (enumerator.MoveNext())
          str = enumerator.Current.Properties["Processorid"].Value.ToString();
      }
      return str;
    }

    public static string getMNum()
    {
      return (MachineCode.getCpu() + MachineCode.GetDiskVolumeSerialNumber()).Substring(0, 24);
    }

    public static string getRNum()
    {
      MachineCode.setIntCode();
      string str1 = MachineCode.getCpu() + MachineCode.GetDiskVolumeSerialNumber() + MachineCode.EncryptDES("mobstermobstermobstermobstermobstermobstermobstermobster", "shuangseq");
      for (int index = 1; index < MachineCode.Charcode.Length; ++index)
        MachineCode.Charcode[index] = Convert.ToChar(str1.Substring(index - 1, 1));
      for (int index = 1; index < MachineCode.intNumber.Length; ++index)
        MachineCode.intNumber[index] = MachineCode.intCode[Convert.ToInt32(MachineCode.Charcode[index])] + Convert.ToInt32(MachineCode.Charcode[index]);
      string str2 = "";
      for (int index = 1; index < MachineCode.intNumber.Length; ++index)
        str2 = MachineCode.intNumber[index] < 48 || MachineCode.intNumber[index] > 57 ? (MachineCode.intNumber[index] < 65 || MachineCode.intNumber[index] > 90 ? (MachineCode.intNumber[index] < 97 || MachineCode.intNumber[index] > 122 ? (MachineCode.intNumber[index] <= 122 ? str2 + Convert.ToChar(MachineCode.intNumber[index] - 9).ToString() : str2 + Convert.ToChar(MachineCode.intNumber[index] - 10).ToString()) : str2 + Convert.ToChar(MachineCode.intNumber[index]).ToString()) : str2 + Convert.ToChar(MachineCode.intNumber[index]).ToString()) : str2 + Convert.ToChar(MachineCode.intNumber[index]).ToString();
      return str2.ToUpper();
    }

    public static string getRNum(string str)
    {
      MachineCode.setIntCode();
      string str1 = str + MachineCode.EncryptDES("mobstermobstermobstermobstermobstermobstermobstermobster", "shuangseq");
      for (int index = 1; index < MachineCode.Charcode.Length; ++index)
        MachineCode.Charcode[index] = Convert.ToChar(str1.Substring(index - 1, 1));
      for (int index = 1; index < MachineCode.intNumber.Length; ++index)
        MachineCode.intNumber[index] = MachineCode.intCode[Convert.ToInt32(MachineCode.Charcode[index])] + Convert.ToInt32(MachineCode.Charcode[index]);
      string str2 = "";
      for (int index = 1; index < MachineCode.intNumber.Length; ++index)
        str2 = MachineCode.intNumber[index] < 48 || MachineCode.intNumber[index] > 57 ? (MachineCode.intNumber[index] < 65 || MachineCode.intNumber[index] > 90 ? (MachineCode.intNumber[index] < 97 || MachineCode.intNumber[index] > 122 ? (MachineCode.intNumber[index] <= 122 ? str2 + Convert.ToChar(MachineCode.intNumber[index] - 9).ToString() : str2 + Convert.ToChar(MachineCode.intNumber[index] - 10).ToString()) : str2 + Convert.ToChar(MachineCode.intNumber[index]).ToString()) : str2 + Convert.ToChar(MachineCode.intNumber[index]).ToString()) : str2 + Convert.ToChar(MachineCode.intNumber[index]).ToString();
      return str2.ToUpper();
    }

    public static string EncryptDES(string encryptString, string encryptKey)
    {
      byte[] numArray = new byte[8]
      {
        (byte) 18,
        (byte) 52,
        (byte) 86,
        (byte) 120,
        (byte) 144,
        (byte) 171,
        (byte) 205,
        (byte) 239
      };
      try
      {
        byte[] bytes1 = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
        byte[] rgbIV = numArray;
        byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(bytes1, rgbIV), CryptoStreamMode.Write);
        cryptoStream.Write(bytes2, 0, bytes2.Length);
        cryptoStream.FlushFinalBlock();
        return Convert.ToBase64String(memoryStream.ToArray());
      }
      catch
      {
        return encryptString;
      }
    }
  }
}
