// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_2Start
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Utils
{
    /// <summary>
    /// 快三
    /// </summary>
    public static class CheckK3_Start
    {
        /// <summary>
        /// 和值
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNum"></param>
        /// <returns></returns>
        public static int K_3HZ(string CheckNumber, string lotNum)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNum))
            {
                return 0;
            }

            return CheckNumber.Split('_').Count(n => n.Equals(lotNum));
        }

        /// <summary>
        /// 三同号单选
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int K_3STDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            foreach(string num in userNums){
                if(lotNums.Count(n => n != num) <= 0){
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// 三同号通选
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int K_3STTX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] nums = new string[]{"1", "2", "3", "4", "5", "6"};
            string[] lotNums = lotNumber.Split(',');

            foreach (string num in nums)
            {
                if (lotNums.Count(n => n != num) <= 0)
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// 二同号单选
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int K_32TDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            foreach (string num in userNums)
            {
                if (lotNums.Count(n => n != num) <= 1)
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// 二同号通选
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int K_32TTX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] nums = new string[] { "1", "2", "3", "4", "5", "6" };
            string[] lotNums = lotNumber.Split(',');

            foreach (string num in nums)
            {
                if (lotNums.Count(n => n != num) <= 1)
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// 二不同直选
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int K_32BT(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string nums = "";
            int count = 0;

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            foreach (string num in userNums)
            {
                if (lotNums.Count(n => n == num) > 0 && nums.IndexOf(num) < 0)
                {
                    count++;
                    nums += num;
                }
            }

            return count * (count - 1) / 2;
        }

        /// <summary>
        /// 三不同直选
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int K_33BT(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string nums = "";
            int count = 0;

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            foreach (string num in userNums)
            {
                if (lotNums.Count(n => n == num) > 0 && nums.IndexOf(num) < 0)
                {
                    count++;
                    nums += num;
                }
            }

            return count * (count - 2) * (count - 1) / 6;
        }

        /// <summary>
        /// 三连号通选
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int K_33LTX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            Array.Sort(userNums);

            string[] lotNums = lotNumber.Split(',');
            Array.Sort(lotNums);

            if (string.Join("", userNums).IndexOf((string.Join("", lotNums))) >= 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
