// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.betinfo
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;
using System;
using System.Data;
using System.Web.UI.HtmlControls;

namespace Lottery.Admin
{
  public class betinfo : AdminCenter
  {
    protected HtmlForm form1;
    protected HtmlTableRow NumberShow;
    public string L_Bonus;
    public string L_Detail;
    public string L_IssueNumber;
    public string L_Lottery;
    public string L_Num;
    public string L_Number;
    public string L_PlayType;
    public string L_Point;
    public string L_PointMoney;
    public string L_Pos;
    public string L_RealGet;
    public string L_SingleMoney;
    public string L_State;
    public string L_STime;
    public string L_STime2;
    public string L_Times;
    public string L_Total;
    public string UserId;
    public string UserName;
    public string L_WinNum;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.Page.IsPostBack)
        return;
      string BetId = this.Str2Str(this.q("id"));
      string whereStr = " Id=" + BetId;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      string sql0 = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetUserCode(UserId) as UserCode,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,Times*Total as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,dbo.f_GetBetState(State) as StateName,IsWin,Number", "N_UserBet", "STime2", 999, 1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count > 0)
      {
        DataRow row = dataTable.Rows[0];
        this.UserName = row["UserName"].ToString();
        this.L_Lottery = row["LotteryName"].ToString();
        this.L_PlayType = row["PlayName"].ToString();
        this.L_IssueNumber = row["IssueNum"].ToString();
        this.L_SingleMoney = Convert.ToDecimal(row["SingleMoney"]).ToString("0.0000") + " 元";
        this.L_RealGet = Convert.ToDecimal(row["RealGet"]).ToString("0.0000") + " 元";
        this.L_Times = row["Times"].ToString();
        this.L_Total = Convert.ToDecimal(row["Total"]).ToString("0.0000");
        Decimal num1 = Convert.ToDecimal(row["Point"]);
        Decimal num2 = Convert.ToDecimal(this.L_Total) * num1 / new Decimal(100);
        this.L_PointMoney = num2.ToString("0.0000") + " 元";
        num2 = Convert.ToDecimal(row["WinBonus"]);
        this.L_Bonus = num2.ToString("0.0000") + " 元";
        Decimal num3 = Convert.ToDecimal(row["Bonus"]);
        num2 = Convert.ToDecimal(num3);
        string str1 = num2.ToString("0.0000");
        string str2 = "/";
        num2 = Convert.ToDecimal(num1);
        string str3 = num2.ToString("0.00");
        string str4 = " %";
        this.L_Point = str1 + str2 + str3 + str4;
        if (row["PlayCode"].ToString().Contains("3HX"))
        {
          string[] strArray1 = new string[6];
          string[] strArray2 = strArray1;
          int index1 = 0;
          num2 = Convert.ToDecimal(num3 / new Decimal(2));
          string str5 = num2.ToString("0.0000");
          strArray2[index1] = str5;
          strArray1[1] = "/";
          string[] strArray3 = strArray1;
          int index2 = 2;
          num2 = Convert.ToDecimal(num3);
          string str6 = num2.ToString("0.0000");
          strArray3[index2] = str6;
          strArray1[3] = "/";
          string[] strArray4 = strArray1;
          int index3 = 4;
          num2 = Convert.ToDecimal(num1);
          string str7 = num2.ToString("0.0000");
          strArray4[index3] = str7;
          strArray1[5] = " %";
          this.L_Point = string.Concat(strArray1);
        }
        this.L_Num = row["Num"].ToString();
        int int32 = Convert.ToInt32(row["State"]);
        this.L_State = row["StateName"].ToString();
        this.L_STime = row["STime"].ToString();
        this.L_STime2 = row["STime2"].ToString();
        this.L_Detail = BetDetailDAL.GetBetDetail(Convert.ToDateTime(row["STime2"]).ToString("yyyyMMdd"), row["UserId"].ToString(), BetId);
        this.L_Pos = row["Pos"].ToString();
        this.L_WinNum = row["WinNum"].ToString();
        if (this.L_Pos != "")
        {
          string str5 = "";
          string[] strArray = this.L_Pos.Split(',');
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (Convert.ToInt32(strArray[index]) == 1)
              str5 = str5 + "," + index.ToString();
          }
          this.L_Pos = "任选位数:" + str5.Substring(1).Replace("0", "万位").Replace("1", "千位").Replace("2", "百位").Replace("3", "十位").Replace("4", "个位") + "\n";
        }
        this.L_Detail = this.L_Pos + BetDetailDAL.GetBetDetail(Convert.ToDateTime(row["STime2"]).ToString("yyyyMMdd"), row["UserId"].ToString(), BetId);
        this.NumberShow.Visible = false;
        if (int32 >= 1)
        {
          this.NumberShow.Visible = true;
          this.doh.Reset();
          this.doh.ConditionExpress = "Type=@Type and Title=@Title";
          this.doh.AddConditionParameter("@Type", (object) row["LotteryId"].ToString());
          this.doh.AddConditionParameter("@Title", (object) this.L_IssueNumber);
          this.L_Number = string.Concat(this.doh.GetField("Sys_LotteryData", "Number"));
        }
      }
      else
      {
        this.Response.Write("参数错误");
        this.Response.End();
      }
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
