using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lottery.Core
{
    public static class MD5Cryptology
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            //MD5 md5 = MD5.Create();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(buffer);
            return BitConverter.ToString(result);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            return null;
        }
    }
}
