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
    /// 六合彩
    /// </summary>
    public static class CheckHK3_Start
    {
        public static string[] SXNums = new string[50];
        public static string[] SX = { "狗", "猪", "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡" };
        public static int BaseYear = 2018;
        public static int[] Hong = { 01, 02, 07, 08, 12, 13, 18, 19, 23, 24, 29, 30, 34, 35, 40, 45, 46 };
        public static int[] Lan = { 03, 04, 09, 10, 14, 15, 20, 25, 26, 31, 36, 37, 41, 42, 47, 48 };
        public static int[] Lv = { 05, 06, 11, 16, 17, 21, 22, 27, 28, 32, 33, 38, 39, 43, 44, 49 };

        private static void InitSX()
        {
            SXNums[0] = "";
            int sxIndex;
            string sx;
            checkYearSX(out sxIndex);

            sx = SX[sxIndex];
            SXNums[01] = sx;
            SXNums[13] = sx;
            SXNums[25] = sx;
            SXNums[37] = sx;
            SXNums[49] = sx;

            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[12] =sx;
            SXNums[24] =sx;
            SXNums[36] =sx;
            SXNums[48] =sx;

            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[11] =sx;
            SXNums[23] =sx;
            SXNums[35] =sx;
            SXNums[47] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[10] =sx;
            SXNums[22] =sx;
            SXNums[34] =sx;
            SXNums[46] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[09] =sx;
            SXNums[21] =sx;
            SXNums[33] =sx;
            SXNums[33] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[08] =sx;
            SXNums[20] =sx;
            SXNums[32] =sx;
            SXNums[44] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[07] =sx;
            SXNums[19] =sx;
            SXNums[31] =sx;
            SXNums[43] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[06] =sx;
            SXNums[18] =sx;
            SXNums[30] =sx;
            SXNums[42] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[05] =sx;
            SXNums[17] =sx;
            SXNums[29] =sx;
            SXNums[41] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[04] =sx;
            SXNums[16] =sx;
            SXNums[28] =sx;
            SXNums[40] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[03] =sx;
            SXNums[15] =sx;
            SXNums[27] =sx;
            SXNums[39] =sx;
            
            sxIndex += 1;
            sx = SX[sxIndex >= 12 ? sxIndex - 12 :  sxIndex];
            SXNums[02] =sx;
            SXNums[14] =sx;
            SXNums[26] =sx;
            SXNums[38] =sx;
        }

        public static string checkYearSX(out int sxIndex)
        {
            int diffYear = DateTime.Now.Year - BaseYear;
            sxIndex = diffYear >= 12 ? diffYear - 12 : diffYear;

            return SX[sxIndex];
        }
        
        /// <summary>
        /// 是否和局
        /// </summary>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static bool isDrawTM(string lotNumber)
        {
            if (string.IsNullOrEmpty(lotNumber))
            {
                return false;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return false;
            }

            string tm = lotNums[6];

            if (tm == "49")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否和局
        /// </summary>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int checkDrawZM(string lotNumber)
        {
            if (string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');
            int zm = 0;

            if (lotNums.Length != 7)
            {
                return 0;
            }

            int draw = 0;

            for (int i = 0; i < lotNums.Length - 1; i++)
            {
                zm = Convert.ToInt32(lotNums[i]);

                if (zm == 49)
                {
                    draw++;
                }
            }

            return draw;
        }

        /// <summary>
        /// 平一中一
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMP1Z1(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;

            if (lotNums.Length != 7 || userNums.Length < 1)
            {
                return 0;
            }

            foreach (string n in lotNums)
            {
                if (userNums.Contains(n))
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 平二中二
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMP2Z2(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;

            if (lotNums.Length != 7 || userNums.Length < 2)
            {
                return 0;
            }

            foreach (string n in lotNums)
            {
                if (userNums.Contains(n))
                {
                    num++;
                }
            }

            return num * (num - 1) / 2 * 1;
        }

        /// <summary>
        /// 平三中二
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMP3Z2X3(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;

            if (lotNums.Length != 7 || userNums.Length < 3)
            {
                return 0;
            }

            foreach (string n in lotNums)
            {
                if (userNums.Contains(n))
                {
                    num++;
                }
            }

            return num * (num - 1) / 2 * 1;
        }

        /// <summary>
        /// 平三中三
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMP3Z3(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;

            if (lotNums.Length != 7 || userNums.Length < 3)
            {
                return 0;
            }

            foreach (string n in lotNums)
            {
                if (userNums.Contains(n))
                {
                    num++;
                }
            }

            return num * (num - 1) * (num - 2) / 3 * 2 * 1;
        }

        /// <summary>
        /// 平三中三
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMP4Z4(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;

            if (lotNums.Length != 7 || userNums.Length < 4)
            {
                return 0;
            }

            foreach (string n in lotNums)
            {
                if (userNums.Contains(n))
                {
                    num++;
                }
            }

            return num * (num - 1) * (num - 2) * (num - 3) / 4 * 3 * 2 * 1;
        }

        /// <summary>
        /// 尾数大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int ZMPTX(string lotNumber, string CheckNumber, int len)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber) || len <= 0 || len > 5)
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return 0;
            }

            InitSX();

            string[] lotSX = new string[7];
            int ma;
            string sx;

            for (int i = 0; i < 7; i++)
            {
                ma = Convert.ToInt32(lotNums[i]);
                sx = SXNums[ma];

                if (lotSX.Contains(sx) == false)
                {
                    lotSX[i] = sx;
                }
            }

            int num = 0;


            foreach (string betSX in userNums)
            {
                if (!string.IsNullOrEmpty(betSX) && lotSX.Contains(betSX))
                {
                    num++;
                }
            }

            if (len == 1)
            {
                return num;
            }
            else if (len == 2)
            {
                return num * (num - 1) / 2 * 1;
            }
            else if (len == 3)
            {
                return num * (num - 1) * (num - 2) / 3 * 2 * 1;
            }
            else if (len == 4)
            {
                return num * (num - 1) * (num - 2) * (num - 3) / 4 * 3 * 2 * 1;
            }
            else if (len == 5)
            {
                return num * (num - 1) * (num - 2) * (num - 3) * (num - 4) / 5 * 4 * 3 * 2 * 1;
            }

            return 0;
        }

        /// <summary>
        /// 尾数大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int BZ(string lotNumber, string CheckNumber, int type)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber) || type < 5 || type > 15)
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');
            List<string> nums = new List<string>();

            foreach (var n in lotNums)
            {
                if (userNums.Contains(n) == false && nums.Contains(n) == false)
                {
                    nums.Add(n);
                }
            }
            
            int num = 1;
            int repeart = 1;

            if (nums.Count < type)
            {
                return 0;
            }
            else
            {
                for (int i = 0; i < type; i++)
                {
                    num *= (nums.Count - i);
                    repeart *= (type - i);
                }

                return num / repeart;
            }
        }

        /// <summary>
        /// 尾数大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMWSDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;
            string bet = "";
            string[] betNums = null;
            string zm = "";
            int ws = 0;

            if (lotNums.Length != 7 || userNums.Length != 6)
            {
                return 0;
            }

            for (int i = 0; i < lotNums.Length - 1; i++)
            {
                zm = lotNums[i];

                if (zm == "49")
                {
                    continue;
                }

                bet = userNums[i];

                if (string.IsNullOrEmpty(bet))
                {
                    continue;
                }

                betNums = bet.Split('_');


                ws = zm.Length > 1
                    ? Convert.ToInt32(zm.Substring(1, 1))
                    : Convert.ToInt32(zm.Substring(0, 1));

                if (ws <= 4 && betNums.Count(n => n == "小") > 0)
                {
                    num++;
                }
                else if (ws >= 5 && betNums.Count(n => n == "大") > 0)
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 正码大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMSB(string lotNumber, string CheckNumber, string bs)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber) || string.IsNullOrEmpty(bs))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;
            string bet = "";
            string[] betNums = null;
            int zm = 0;

            if (lotNums.Length != 7 || userNums.Length != 6)
            {
                return 0;
            }

            for (int i = 0; i < lotNums.Length - 1; i++)
            {
                zm = Convert.ToInt32(lotNums[i]);
                bet = userNums[i];

                if (string.IsNullOrEmpty(bet))
                {
                    continue;
                }

                if ("红".Equals(bs) && Hong.Contains(zm) && betNums.Contains("红"))
                {
                    num++;
                }
                else if ("蓝".Equals(bs) && Lan.Contains(zm) && betNums.Contains("蓝"))
                {
                    num++;
                }
                else if ("绿".Equals(bs) && Lv.Contains(zm) && betNums.Contains("绿"))
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 正码大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMHSDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;
            string bet = "";
            string[] betNums = null;
            string zm = "";
            int he = 0;

            if (lotNums.Length != 7 || userNums.Length != 6)
            {
                return 0;
            }

            for (int i = 0; i < lotNums.Length - 1; i++)
            {
                zm = lotNums[i];

                if (zm == "49")
                {
                    continue;
                }

                bet = userNums[i];

                if (string.IsNullOrEmpty(bet))
                {
                    continue;
                }

                betNums = bet.Split('_');

                he = zm.Length > 1
                    ? Convert.ToInt32(zm.Substring(0, 1)) + Convert.ToInt32(zm.Substring(1, 1))
                    : Convert.ToInt32(zm.Substring(0, 1));

                if (he <= 6 && betNums.Count(n => n == "小") > 0)
                {
                    num++;
                }
                else if (he >= 7 && betNums.Count(n => n == "大") > 0)
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 正码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMHSDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;
            string bet = "";
            string[] betNums = null;
            string zm = "";
            int he = 0;

            if (lotNums.Length != 7 || userNums.Length != 6)
            {
                return 0;
            }

            for (int i = 0; i < lotNums.Length - 1; i++)
            {
                zm = lotNums[i];

                if (zm == "49")
                {
                    continue;
                }

                bet = userNums[i];

                if (string.IsNullOrEmpty(bet))
                {
                    continue;
                }

                betNums = bet.Split('_');

                he = zm.Length > 1
                    ? Convert.ToInt32(zm.Substring(0, 1)) + Convert.ToInt32(zm.Substring(1, 1))
                    : Convert.ToInt32(zm.Substring(0, 1));

                if (he % 2 == 0 && betNums.Count(n => n == "双") > 0)
                {
                    num++;
                }
                else if (he % 2 != 0 && betNums.Count(n => n == "单") > 0)
                {
                    num++;
                }

            }

            return num;
        }

        /// <summary>
        /// 正码大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;
            string bet = "";
            string[] betNums = null;
            int zm = 0;

            if (lotNums.Length != 7 || userNums.Length != 6)
            {
                return 0;
            }

            for (int i = 0; i < lotNums.Length - 1; i++)
            {
                zm = Convert.ToInt32(lotNums[i]);

                if (zm == 49)
                {
                    continue;
                }

                bet = userNums[i];

                if (string.IsNullOrEmpty(bet))
                {
                    continue;
                }

                betNums = bet.Split('_');

                if (zm <= 24 && betNums.Count(n => n == "小") > 0)
                {
                    num++;
                }
                else if (zm >= 25 && betNums.Count(n => n == "大") > 0)
                {
                    num++;
                }

            }

            return num;
        }

        /// <summary>
        /// 正码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZMDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split(',');
            string[] lotNums = lotNumber.Split(',');
            int num = 0;
            string bet = "";
            string[] betNums = null;
            int zm = 0;

            if (lotNums.Length != 7 || userNums.Length != 6)
            {
                return 0;
            }

            for (int i = 0; i < lotNums.Length - 1; i++)
            {
                zm = Convert.ToInt32(lotNums[i]);

                if (zm == 49)
                {
                    continue;
                }

                bet = userNums[i];

                if (string.IsNullOrEmpty(bet))
                {
                    continue;
                }

                betNums = bet.Split('_');

                if (zm % 2 == 0 && betNums.Count(n => n == "双") > 0)
                {
                    num++;
                }
                else if (zm % 2 != 0 && betNums.Count(n => n == "单") > 0)
                {
                    num++;
                }

            }

            return num;
        }

        /// <summary>
        /// 总和，总和大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZHDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return 0;
            }

            int sum = 0, num = 0;

            for (int i = 0; i < lotNums.Length; i++)
            {
                sum += Convert.ToInt32(lotNums[i]);
            }

            if (sum >= 175 && CheckNumber.Contains("大"))
            {
                num += 1;
            }
            else if (sum <= 174 && CheckNumber.Contains("小"))
            {
                num += 1;
            }

            return num;
        }

        /// <summary>
        /// 总和，总和单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int ZHDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return 0;
            }

            int sum = 0, num = 0;

            for (int i = 0; i < lotNums.Length; i++)
            {
                sum += Convert.ToInt32(lotNums[i]);
            }

            if (sum % 2 == 0 && CheckNumber.Contains("双"))
            {
                num += 1;
            }
            else if (sum % 2 != 0 && CheckNumber.Contains("单"))
            {
                num += 1;
            }

            return num;
        }

        /// <summary>
        /// 生肖总肖, 总肖单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int SXZXDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return 0;
            }

            InitSX();
            string zx = "";
            int num = 0;

            for (int i = 0; i < lotNums.Length; i++)
            {
                num = Convert.ToInt32(lotNums[i]);

                if (zx.IndexOf(SXNums[num]) < 0)
                {
                    zx += SXNums[num];
                }
            }

            if (zx.Length % 2 == 0 && userNums[0] == "双")
            {
                return 1;
            }
            else if (zx.Length % 2 != 0 && userNums[0] == "单")
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 生肖总肖
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int SXZX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return 0;
            }

            InitSX();
            string zx = "";
            int num = 0;

            for (int i = 0; i < lotNums.Length; i++)
            {
                num = Convert.ToInt32(lotNums[i]);

                if (zx.IndexOf(SXNums[num]) < 0)
                {
                    zx += SXNums[num];
                }
            }

            if (zx.Length == Convert.ToInt32(userNums[0]))
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码生肖
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMSX(string lotNumber, string CheckNumber)
        
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return 0;
            }

            InitSX();
            int tm = Convert.ToInt32(lotNums[6]);
            string sx = SXNums[tm];

            return userNums.Count(n => n == sx);
        }

        /// <summary>
        /// 特码
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TM(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            return userNums.Count(n => n == lotNums[6]);
        }

        /// <summary>
        /// 特码大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);
            if (tm >= 1 && tm <= 24 && userNums.Count(n => n == "小") > 0)
            {
                return 1;
            }
            else if (tm >= 25 && tm <= 48 && userNums.Count(n => n == "大") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);
            if ((tm % 2) == 0 && userNums.Count(n => n == "双") > 0)
            {
                return 1;
            }
            else if ((tm % 2) != 1 && userNums.Count(n => n == "单") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 合数大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMHDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int sum = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(0, 1)) + Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));

            if (sum <= 6 && userNums.Count(n => n == "小") > 0)
            {
                return 1;
            }
            else if (sum >= 7 && userNums.Count(n => n == "大") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMHDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int sum = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(0, 1)) + Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));


            if ((sum % 2) == 0 && userNums.Count(n => n == "双") > 0)
            {
                return 1;
            }
            else if ((sum % 2) != 1 && userNums.Count(n => n == "单") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 合数大小
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMWDX(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int ws = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));

            if (ws <= 4 && userNums.Count(n => n == "小") > 0)
            {
                return 1;
            }
            else if (ws >= 5 && userNums.Count(n => n == "大") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码单双
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMWDS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            int ws = tm.Length > 1
                ? Convert.ToInt32(tm.Substring(1, 1))
                : Convert.ToInt32(tm.Substring(0, 1));


            if ((ws % 2) == 0 && userNums.Count(n => n == "双") > 0)
            {
                return 1;
            }
            else if ((ws % 2) != 1 && userNums.Count(n => n == "单") > 0)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码头数
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMTS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            string ts = tm.Length > 1
                ? tm.Substring(0, 1)
                : "0";

            return userNums.Count(n => n == ts);
        }

        /// <summary>
        /// 特码属数
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMWS(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] userNums = CheckNumber.Split('_');
            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            string tm = lotNums[6];
            string ws = tm.Length > 1
                ? tm.Substring(1, 1)
                : tm.Substring(0, 1);

            return userNums.Count(n => n == ws);
        }

        /// <summary>
        /// 特码半特
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMBT(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);
            string dx = tm <= 24 ? "小" : "大";
            string ds = tm % 2 == 0 ? "双" : "单";

            if (CheckNumber.Contains(dx) && CheckNumber.Contains(ds))
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 色波
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMSB(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7)
            {
                return 0;
            }

            int tm = Convert.ToInt32(lotNums[6]);

            if (Hong.Contains(tm) && CheckNumber.Contains("红"))
            {
                return 1;
            }
            else if (Lan.Contains(tm) && CheckNumber.Contains("蓝"))
            {
                return 1;
            }
            else if (Lv.Contains(tm) && CheckNumber.Contains("绿"))
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 特码半波
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMBB(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int num = 0;
            int tm = Convert.ToInt32(lotNums[6]);
            string dx = tm <= 24 ? "小" : "大";
            string ds = tm % 2 == 0 ? "双" : "单";
            string sb = Hong.Contains(tm) ? "红" : (Lan.Contains(tm) ? "蓝" : (Lv.Contains(tm) ? "绿" : ""));

            if (CheckNumber.Contains(sb))
            {
                if (CheckNumber.Contains(dx))
                {
                    num++;
                }

                if (CheckNumber.Contains(ds))
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 特码半半波
        /// </summary>
        /// <param name="CheckNumber"></param>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        public static int TMBBB(string lotNumber, string CheckNumber)
        {
            if (string.IsNullOrEmpty(CheckNumber) || string.IsNullOrEmpty(lotNumber))
            {
                return 0;
            }

            string[] lotNums = lotNumber.Split(',');

            if (lotNums.Length != 7 || lotNums[6] == "49")
            {
                return 0;
            }

            int num = 0;
            int tm = Convert.ToInt32(lotNums[6]);
            string dx = tm <= 24 ? "小" : "大";
            string ds = tm % 2 == 0 ? "双" : "单";
            string sb = Hong.Contains(tm) ? "红" : (Lan.Contains(tm) ? "蓝" : (Lv.Contains(tm) ? "绿" : ""));

            if (CheckNumber.Contains(sb) && CheckNumber.Contains(dx) && CheckNumber.Contains(ds))
            {
                return 1;
            }

            return num;
        }
    }
}
