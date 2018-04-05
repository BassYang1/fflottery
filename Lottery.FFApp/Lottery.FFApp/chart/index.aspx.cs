// Decompiled with JetBrains decompiler
// Type: Web.index
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;
using System.Data;
using System.Web.UI;

namespace Web
{
  public partial class index : Page
  {
    public string lotteryId = "1001";
    public int count = 10;
    public string LotteryHeadLines;
    public string LotteryLines;
    public string LName;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Request.QueryString["id"] != null)
        this.lotteryId = this.Request.QueryString["id"].ToString();
      this.LName = LotteryUtils.LotteryTitle(Convert.ToInt32(this.lotteryId));
      this.LotteryLines = "";
      if (this.IsPostBack)
        return;
      int top = 50;
      if (this.Request["n"] != null)
        top = Convert.ToInt32(this.Request["n"]);
      DataTable listDataTable = new LotteryDataDAL().GetListDataTable(Convert.ToInt32(this.lotteryId), top);
      string[,] strArray1;
      if (this.lotteryId.Substring(0, 1) == "1")
      {
        this.count = 10;
        int[,] numArray1 = new int[5, 10];
        int[,] numArray2 = new int[5, 10];
        int[,] numArray3 = new int[5, 10];
        int[,] numArray4 = new int[5, 10];
        int[,] numArray5 = new int[5, 10];
        strArray1 = new string[5, 10];
        for (int index1 = 0; index1 < listDataTable.Rows.Count; ++index1)
        {
          DataRow row = listDataTable.Rows[index1];
          string str1 = row["Title"].ToString();
          string str2 = row["Number"].ToString();
          string[] strArray2 = str2.Split(',');
          string str3 = "<tr>" + "<td rowspan=\"2\" style=\"width:100px;\">期号</td>" + "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>" + "<td colspan=\"10\">万位</td>" + "<td colspan=\"10\">千位</td>" + "<td colspan=\"10\">百位</td>" + "<td colspan=\"10\">十位</td>" + "<td colspan=\"10\">个位</td>" + "</tr>" + "<tr>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 0; index3 < 10; ++index3)
              str3 = str3 + "<td>" + (object) index3 + "</td>";
          }
          this.LotteryHeadLines = str3 + "</tr>";
          string str4 = "<tr>" + "<td class=\"issue\">" + str1 + "</td>" + "<td align=\"center\" class=\"tdwth\">" + str2 + "</td>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 0; index3 <= 9; ++index3)
            {
              if (index3 == Convert.ToInt32(strArray2[index2]))
              {
                ++numArray1[index2, index3];
                numArray2[index2, index3] = -1;
                ++numArray4[index2, index3];
                if (numArray3[index2, index3] < numArray4[index2, index3])
                  numArray3[index2, index3] = numArray4[index2, index3];
              }
              else
              {
                numArray4[index2, index3] = 0;
                ++numArray2[index2, index3];
                if (numArray5[index2, index3] < numArray2[index2, index3])
                  numArray5[index2, index3] = numArray2[index2, index3];
              }
              if (index3 == Convert.ToInt32(strArray2[index2]))
              {
                str4 = index2 % 2 != 0 ? str4 + "<td class=\"charball td1\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>" : str4 + "<td class=\"charball td0\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>";
                ++numArray2[index2, index3];
              }
              else if (index2 % 2 == 0)
                str4 = str4 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
              else
                str4 = str4 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
            }
          }
          this.LotteryLines += str4 + "</tr>";
        }
        string str5 = "<tr>" + "<td colspan=\"2\">" + "当前最大连开" + "</td>";
        for (int index1 = 0; index1 < 5; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str5 = str5 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
            else
              str5 = str5 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
          }
        }
        string str6 = str5 + "</tr>";
        string str7 = "<tr>" + "<td colspan=\"2\">" + "当前最大遗漏" + "</td>";
        for (int index1 = 0; index1 < 5; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str7 = str7 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
            else
              str7 = str7 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
          }
        }
        string str8 = str7 + "</tr>";
        string str9 = "<tr>" + "<td colspan=\"2\">" + "当前出现次数" + "</td>";
        for (int index1 = 0; index1 < 5; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str9 = str9 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
            else
              str9 = str9 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
          }
        }
        string str10 = str9 + "</tr>";
        index index = this;
        index.LotteryLines = index.LotteryLines + str10 + str6 + str8;
      }
      if (this.lotteryId.Substring(0, 1) == "2")
      {
        this.count = 11;
        int[,] numArray1 = new int[5, 11];
        int[,] numArray2 = new int[5, 11];
        int[,] numArray3 = new int[5, 11];
        int[,] numArray4 = new int[5, 11];
        int[,] numArray5 = new int[5, 11];
        strArray1 = new string[5, 11];
        for (int index1 = 0; index1 < listDataTable.Rows.Count; ++index1)
        {
          DataRow row = listDataTable.Rows[index1];
          string str1 = row["Title"].ToString();
          string str2 = row["Number"].ToString();
          string[] strArray2 = str2.Split(',');
          string str3 = "<tr>" + "<td rowspan=\"2\" style=\"width:100px;\">期号</td>" + "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>" + "<td colspan=\"11\">万位</td>" + "<td colspan=\"11\">千位</td>" + "<td colspan=\"11\">百位</td>" + "<td colspan=\"11\">十位</td>" + "<td colspan=\"11\">个位</td>" + "</tr>" + "<tr>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 1; index3 <= 11; ++index3)
              str3 = str3 + "<td>" + (object) index3 + "</td>";
          }
          this.LotteryHeadLines = str3 + "</tr>";
          string str4 = "<tr>" + "<td class=\"issue\">" + str1 + "</td>" + "<td align=\"center\" class=\"tdwth\">" + str2 + "</td>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 0; index3 <= 10; ++index3)
            {
              if (index3 + 1 == Convert.ToInt32(strArray2[index2]))
              {
                ++numArray1[index2, index3];
                numArray2[index2, index3] = -1;
                ++numArray4[index2, index3];
                if (numArray3[index2, index3] < numArray4[index2, index3])
                  numArray3[index2, index3] = numArray4[index2, index3];
              }
              else
              {
                numArray4[index2, index3] = 1;
                ++numArray2[index2, index3];
                if (numArray5[index2, index3] < numArray2[index2, index3])
                  numArray5[index2, index3] = numArray2[index2, index3];
              }
              if (index3 + 1 == Convert.ToInt32(strArray2[index2]))
              {
                str4 = index2 % 2 != 0 ? str4 + "<td class=\"charball td1\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>" : str4 + "<td class=\"charball td0\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>";
                ++numArray2[index2, index3];
              }
              else if (index2 % 2 == 0)
                str4 = str4 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
              else
                str4 = str4 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
            }
          }
          this.LotteryLines += str4 + "</tr>";
        }
        string str5 = "<tr>" + "<td colspan=\"2\">" + "当前最大连开" + "</td>";
        for (int index1 = 0; index1 < 5; ++index1)
        {
          for (int index2 = 0; index2 <= 10; ++index2)
          {
            if (index1 % 2 == 0)
              str5 = str5 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
            else
              str5 = str5 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
          }
        }
        string str6 = str5 + "</tr>";
        string str7 = "<tr>" + "<td colspan=\"2\">" + "当前最大遗漏" + "</td>";
        for (int index1 = 0; index1 < 5; ++index1)
        {
          for (int index2 = 0; index2 <= 10; ++index2)
          {
            if (index1 % 2 == 0)
              str7 = str7 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
            else
              str7 = str7 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
          }
        }
        string str8 = str7 + "</tr>";
        string str9 = "<tr>" + "<td colspan=\"2\">" + "当前出现次数" + "</td>";
        for (int index1 = 0; index1 < 5; ++index1)
        {
          for (int index2 = 0; index2 <= 10; ++index2)
          {
            if (index1 % 2 == 0)
              str9 = str9 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
            else
              str9 = str9 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
          }
        }
        string str10 = str9 + "</tr>";
        index index = this;
        index.LotteryLines = index.LotteryLines + str10 + str6 + str8;
      }
      if (this.lotteryId.Substring(0, 1) == "3")
      {
        this.count = 10;
        int[,] numArray1 = new int[3, 10];
        int[,] numArray2 = new int[3, 10];
        int[,] numArray3 = new int[3, 10];
        int[,] numArray4 = new int[3, 10];
        int[,] numArray5 = new int[3, 10];
        strArray1 = new string[3, 10];
        for (int index1 = 0; index1 < listDataTable.Rows.Count; ++index1)
        {
          DataRow row = listDataTable.Rows[index1];
          string str1 = row["Title"].ToString();
          string str2 = row["Number"].ToString();
          string[] strArray2 = str2.Split(',');
          string str3 = "<tr>" + "<td rowspan=\"2\" style=\"width:100px;\">期号</td>" + "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>" + "<td colspan=\"10\">百位</td>" + "<td colspan=\"10\">十位</td>" + "<td colspan=\"10\">个位</td>" + "</tr>" + "<tr>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 0; index3 < 10; ++index3)
              str3 = str3 + "<td>" + (object) index3 + "</td>";
          }
          this.LotteryHeadLines = str3 + "</tr>";
          string str4 = "<tr>" + "<td class=\"issue\">" + str1 + "</td>" + "<td align=\"center\" class=\"tdwth\">" + str2 + "</td>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 0; index3 <= 9; ++index3)
            {
              if (index3 == Convert.ToInt32(strArray2[index2]))
              {
                ++numArray1[index2, index3];
                numArray2[index2, index3] = -1;
                ++numArray4[index2, index3];
                if (numArray3[index2, index3] < numArray4[index2, index3])
                  numArray3[index2, index3] = numArray4[index2, index3];
              }
              else
              {
                numArray4[index2, index3] = 0;
                ++numArray2[index2, index3];
                if (numArray5[index2, index3] < numArray2[index2, index3])
                  numArray5[index2, index3] = numArray2[index2, index3];
              }
              if (index3 == Convert.ToInt32(strArray2[index2]))
              {
                str4 = index2 % 2 != 0 ? str4 + "<td class=\"charball td1\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>" : str4 + "<td class=\"charball td0\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>";
                ++numArray2[index2, index3];
              }
              else if (index2 % 2 == 0)
                str4 = str4 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
              else
                str4 = str4 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
            }
          }
          this.LotteryLines += str4 + "</tr>";
        }
        string str5 = "<tr>" + "<td colspan=\"2\">" + "当前最大连开" + "</td>";
        for (int index1 = 0; index1 < 3; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str5 = str5 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
            else
              str5 = str5 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
          }
        }
        string str6 = str5 + "</tr>";
        string str7 = "<tr>" + "<td colspan=\"2\">" + "当前最大遗漏" + "</td>";
        for (int index1 = 0; index1 < 3; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str7 = str7 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
            else
              str7 = str7 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
          }
        }
        string str8 = str7 + "</tr>";
        string str9 = "<tr>" + "<td colspan=\"2\">" + "当前出现次数" + "</td>";
        for (int index1 = 0; index1 < 3; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str9 = str9 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
            else
              str9 = str9 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
          }
        }
        string str10 = str9 + "</tr>";
        index index = this;
        index.LotteryLines = index.LotteryLines + str10 + str6 + str8;
      }
      if (this.lotteryId.Substring(0, 1) == "4")
      {
        this.count = 10;
        int[,] numArray1 = new int[10, 11];
        int[,] numArray2 = new int[10, 11];
        int[,] numArray3 = new int[10, 11];
        int[,] numArray4 = new int[10, 11];
        int[,] numArray5 = new int[10, 11];
        strArray1 = new string[10, 11];
        for (int index1 = 0; index1 < listDataTable.Rows.Count; ++index1)
        {
          DataRow row = listDataTable.Rows[index1];
          string str1 = row["Title"].ToString();
          string str2 = row["Number"].ToString();
          string[] strArray2 = str2.Split(',');
          string str3 = "<tr>" + "<td rowspan=\"2\" style=\"width:100px;\">期号</td>" + "<td rowspan=\"2\" style=\"width:100px;\">开奖号码</td>" + "<td colspan=\"10\">一</td>" + "<td colspan=\"10\">二</td>" + "<td colspan=\"10\">三</td>" + "<td colspan=\"10\">四</td>" + "<td colspan=\"10\">五</td>" + "<td colspan=\"10\">六</td>" + "<td colspan=\"10\">七</td>" + "<td colspan=\"10\">八</td>" + "<td colspan=\"10\">九</td>" + "<td colspan=\"10\">十</td>" + "</tr>" + "<tr>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 1; index3 <= 10; ++index3)
              str3 = str3 + "<td>" + (object) index3 + "</td>";
          }
          this.LotteryHeadLines = str3 + "</tr>";
          string str4 = "<tr>" + "<td class=\"issue\">" + str1 + "</td>" + "<td align=\"center\" class=\"tdwth\">" + str2 + "</td>";
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            for (int index3 = 0; index3 <= 9; ++index3)
            {
              if (index3 + 1 == Convert.ToInt32(strArray2[index2]))
              {
                ++numArray1[index2, index3];
                numArray2[index2, index3] = -1;
                ++numArray4[index2, index3];
                if (numArray3[index2, index3] < numArray4[index2, index3])
                  numArray3[index2, index3] = numArray4[index2, index3];
              }
              else
              {
                numArray4[index2, index3] = 1;
                ++numArray2[index2, index3];
                if (numArray5[index2, index3] < numArray2[index2, index3])
                  numArray5[index2, index3] = numArray2[index2, index3];
              }
              if (index3 + 1 == Convert.ToInt32(strArray2[index2]))
              {
                str4 = index2 % 2 != 0 ? str4 + "<td class=\"charball td1\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>" : str4 + "<td class=\"charball td0\"><div class=\"ball01\">" + strArray2[index2] + "</div></td>";
                ++numArray2[index2, index3];
              }
              else if (index2 % 2 == 0)
                str4 = str4 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
              else
                str4 = str4 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray2[index2, index3] + "</div></td>";
            }
          }
          this.LotteryLines += str4 + "</tr>";
        }
        string str5 = "<tr>" + "<td colspan=\"2\">" + "当前最大连开" + "</td>";
        for (int index1 = 0; index1 < 10; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str5 = str5 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
            else
              str5 = str5 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray3[index1, index2] + "</div></td>";
          }
        }
        string str6 = str5 + "</tr>";
        string str7 = "<tr>" + "<td colspan=\"2\">" + "当前最大遗漏" + "</td>";
        for (int index1 = 0; index1 < 10; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str7 = str7 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
            else
              str7 = str7 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray5[index1, index2] + "</div></td>";
          }
        }
        string str8 = str7 + "</tr>";
        string str9 = "<tr>" + "<td colspan=\"2\">" + "当前出现次数" + "</td>";
        for (int index1 = 0; index1 < 10; ++index1)
        {
          for (int index2 = 0; index2 <= 9; ++index2)
          {
            if (index1 % 2 == 0)
              str9 = str9 + "<td class=\"wdh td0\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
            else
              str9 = str9 + "<td class=\"wdh td1\"><div class=\"ball14\">" + (object) numArray1[index1, index2] + "</div></td>";
          }
        }
        string str10 = str9 + "</tr>";
        index index = this;
        index.LotteryLines = index.LotteryLines + str10 + str6 + str8;
      }
    }
  }
}
