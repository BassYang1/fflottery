// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.betUpdate
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class betUpdate : AdminCenter
  {
    protected HtmlForm form1;
    protected HtmlTableRow NumberShow;
    protected TextBox txtDetail;
    protected TextBox txtId;
    protected Button btnSave;
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

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.Page.IsPostBack)
        return;
      string str1 = this.Str2Str(this.q("id"));
      string whereStr = " Id=" + str1;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      this.txtId.Text = str1;
      string sql0 = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetUserCode(UserId) as UserCode,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,Detail,DX,DS,Times*Total as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,dbo.f_GetBetState(State) as StateName,IsDelay,IsWin,STime9", "N_UserBet", "STime2", 999, 1, "desc", whereStr);
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
        this.L_SingleMoney = Convert.ToDecimal(row["SingleMoney"]).ToString("0.00") + " 元";
        Decimal num1 = Convert.ToDecimal(row["RealGet"]);
        this.L_RealGet = num1.ToString("0.00") + " 元";
        this.L_Times = row["Times"].ToString();
        num1 = Convert.ToDecimal(row["Total"]);
        this.L_Total = num1.ToString("0.00");
        Decimal num2 = Convert.ToDecimal(row["Point"]);
        num1 = Convert.ToDecimal(this.L_Total) * num2 / new Decimal(1000);
        this.L_PointMoney = num1.ToString("0.00") + " 元";
        num1 = Convert.ToDecimal(row["WinBonus"]);
        this.L_Bonus = num1.ToString("0.00") + " 元";
        Decimal num3 = Convert.ToDecimal(row["Bonus"]);
        num1 = Convert.ToDecimal(num3);
        string str2 = num1.ToString("0.00");
        string str3 = "/";
        num1 = Convert.ToDecimal(num2);
        string str4 = num1.ToString("0.00");
        string str5 = " %";
        this.L_Point = str2 + str3 + str4 + str5;
        if (row["PlayCode"].ToString().Contains("3HX"))
        {
          string[] strArray1 = new string[6];
          string[] strArray2 = strArray1;
          int index1 = 0;
          num1 = Convert.ToDecimal(num3 / new Decimal(2));
          string str6 = num1.ToString("0.00");
          strArray2[index1] = str6;
          strArray1[1] = "/";
          string[] strArray3 = strArray1;
          int index2 = 2;
          num1 = Convert.ToDecimal(num3);
          string str7 = num1.ToString("0.00");
          strArray3[index2] = str7;
          strArray1[3] = "/";
          string[] strArray4 = strArray1;
          int index3 = 4;
          num1 = Convert.ToDecimal(num2);
          string str8 = num1.ToString("0.00");
          strArray4[index3] = str8;
          strArray1[5] = " %";
          this.L_Point = string.Concat(strArray1);
        }
        this.L_Num = row["Num"].ToString();
        Convert.ToInt32(row["State"]);
        this.L_State = row["StateName"].ToString();
        this.L_STime = row["STime"].ToString();
        this.L_STime2 = row["STime2"].ToString();
        this.txtDetail.Text = row["Detail"].ToString().Replace("#", " # ");
        this.L_Pos = row["Pos"].ToString();
        if (this.L_Pos != "")
        {
          string str6 = "";
          string[] strArray = this.L_Pos.Split(',');
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (Convert.ToInt32(strArray[index]) == 1)
              str6 = str6 + "," + index.ToString();
          }
          this.L_Pos = str6.Substring(1);
        }
        if (!(this.L_Pos != ""))
          ;
        this.NumberShow.Visible = false;
      }
      else
      {
        this.Response.Write("参数错误");
        this.Response.End();
      }
      dataTable.Clear();
      dataTable.Dispose();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.txtId.Text);
      this.doh.AddFieldItem("Detail", (object) this.txtDetail.Text);
      this.doh.Update("N_UserBet");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + this.txtId.Text + "的订单进行改单");
      this.FinalMessage("改单成功，请选中记录点击选中派奖进行手动派奖", "/admin/close.htm", 0);
    }
  }
}
