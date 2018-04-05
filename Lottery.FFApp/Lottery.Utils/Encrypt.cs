// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Encrypt
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lottery.Utils
{
  public class Encrypt
  {
    private static byte[] Keys = new byte[8]
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

    public static string EncryptDES(string encryptString, string encryptKey)
    {
      try
      {
        byte[] bytes1 = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
        byte[] keys = Encrypt.Keys;
        byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(bytes1, keys), CryptoStreamMode.Write);
        cryptoStream.Write(bytes2, 0, bytes2.Length);
        cryptoStream.FlushFinalBlock();
        return Convert.ToBase64String(memoryStream.ToArray());
      }
      catch
      {
        return encryptString;
      }
    }

    public static string DecryptDES(string decryptString, string decryptKey)
    {
      try
      {
        byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
        byte[] keys = Encrypt.Keys;
        byte[] buffer = Convert.FromBase64String(decryptString);
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
        cryptoStream.Write(buffer, 0, buffer.Length);
        cryptoStream.FlushFinalBlock();
        return Encoding.UTF8.GetString(memoryStream.ToArray());
      }
      catch
      {
        return decryptString;
      }
    }
  }
}
