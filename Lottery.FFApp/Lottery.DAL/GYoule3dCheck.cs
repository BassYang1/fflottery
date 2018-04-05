// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.GYoule3dCheck
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Lottery.DAL
{
  public static class GYoule3dCheck
  {
    public static void RunOfIssueNum(int LotteryId, string IssueNum)
    {
      GYoule3dCheck.DoWord doWord = new GYoule3dCheck.DoWord(GYoule3dCheck.Run);
      doWord.BeginInvoke(LotteryId, IssueNum, new AsyncCallback(GYoule3dCheck.CallBack), (object) doWord);
    }

    public static void CallBack(IAsyncResult r)
    {
      GYoule3dCheck.DoWord asyncState = (GYoule3dCheck.DoWord) r.AsyncState;
    }

    private static void Run(int LotteryId, string IssueNum)
    {
      try
      {
        DataTable dataTable = LotteryDAL.GetDataTable(LotteryId.ToString(), IssueNum);
        if (dataTable.Rows.Count > 0)
        {
          DataTable lotteryCheck = LotteryDAL.GetLotteryCheck(LotteryId);
          if (LotteryDAL.GetCurRealGet(LotteryId) < Convert.ToDecimal(lotteryCheck.Rows[0]["CheckPer"]))
          {
            List<KeyValue> source = new List<KeyValue>();
            int int32_1 = Convert.ToInt32(lotteryCheck.Rows[0]["CheckNum"]);
            string[] strArray = new string[20];
            int num1 = 0;
            do
            {
              Decimal num2 = new Decimal(0);
              Decimal num3 = new Decimal(0);
              Decimal num4 = new Decimal(0);
              string[] code20 = NumberCode.CreateCode20();
              string LotteryNumber = ((Convert.ToInt32(code20[0]) + Convert.ToInt32(code20[1]) + Convert.ToInt32(code20[2]) + Convert.ToInt32(code20[3]) + Convert.ToInt32(code20[4]) + Convert.ToInt32(code20[5]) + Convert.ToInt32(code20[6])) % 10).ToString() + "," + (object) ((Convert.ToInt32(code20[7]) + Convert.ToInt32(code20[8]) + Convert.ToInt32(code20[9]) + Convert.ToInt32(code20[10]) + Convert.ToInt32(code20[11]) + Convert.ToInt32(code20[12]) + Convert.ToInt32(code20[13])) % 10) + "," + (object) ((Convert.ToInt32(code20[14]) + Convert.ToInt32(code20[15]) + Convert.ToInt32(code20[16]) + Convert.ToInt32(code20[17]) + Convert.ToInt32(code20[18]) + Convert.ToInt32(code20[19])) % 10);
              for (int index = 0; index < dataTable.Rows.Count; ++index)
              {
                DataRow row = dataTable.Rows[index];
                int int32_2 = Convert.ToInt32(row["Id"]);
                int int32_3 = Convert.ToInt32(row["UserId"]);
                string sType = row["PlayCode"].ToString();
                string CheckNumber = BetDetailDAL.GetBetDetail2(Convert.ToDateTime(row["STime2"]).ToString("yyyyMMdd"), int32_3.ToString(), int32_2.ToString());
                if (string.IsNullOrEmpty(CheckNumber))
                  CheckNumber = "";
                string Pos = row["Pos"].ToString();
                Decimal num5 = Convert.ToDecimal(row["SingleMoney"]);
                Decimal num6 = Convert.ToDecimal(row["Bonus"]);
                Decimal num7 = Convert.ToDecimal(row["PointMoney"]);
                Decimal num8 = Convert.ToDecimal(row["Times"]);
                Decimal num9 = Convert.ToDecimal(row["Total"]);
                num3 += num9 * num8;
                int num10 = CheckPlay.Check(LotteryNumber, CheckNumber, Pos, sType);
                num4 += num6 * num8 * num5 * (Decimal) num10 / new Decimal(2) + num7 * num8;
              }
              Decimal num11 = num3 - num4;
              if (num11 > new Decimal(0))
                num1 = int32_1;
              source.Add(new KeyValue()
              {
                tKey = string.Join(",", code20),
                tValue = num11
              });
              ++num1;
            }
            while (num1 < int32_1);
            List<KeyValue> list = source.OrderByDescending<KeyValue, Decimal>((Func<KeyValue, Decimal>) (a => a.tValue)).ToList<KeyValue>();
            GYoule3dCheck.SetOpenListJson(LotteryId, IssueNum, list[0].tKey, DateTime.Now.ToString(), string.Concat((object) list[0].tValue));
          }
          else
          {
            string[] code20 = NumberCode.CreateCode20();
            GYoule3dCheck.SetOpenListJson(LotteryId, IssueNum, string.Join(",", code20), DateTime.Now.ToString(), "0");
          }
        }
        else
        {
          string[] code20 = NumberCode.CreateCode20();
          GYoule3dCheck.SetOpenListJson(LotteryId, IssueNum, string.Join(",", code20), DateTime.Now.ToString(), "0");
        }
      }
      catch (Exception ex)
      {
        new LogExceptionDAL().Save("派奖异常", ex.Message);
      }
    }

    public static void SetOpenListJson(int lotteryId, string expect, string opencode, string opentime, string realget)
    {
      string str1 = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<xml rows=\"1\" code=\"ssc\" remain=\"1hrs\">" + "<row expect=\"" + expect + "\" opencode=\"" + opencode + "\" opentime=\"" + opentime + "\" realget=\"" + realget + "\"/>" + "</xml>";
      string str2 = ConfigurationManager.AppSettings["DataUrl"].ToString() + "lottery" + (object) lotteryId + ".xml";
      DirFile.CreateFolder(DirFile.GetFolderPath(false, str2));
      StreamWriter streamWriter = new StreamWriter(str2, false, Encoding.UTF8);
      streamWriter.Write(str1);
      streamWriter.Close();
    }

    public delegate void DoWord(int LotteryId, string IssueNum);
  }
}
