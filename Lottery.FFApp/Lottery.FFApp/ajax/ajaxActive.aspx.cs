// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxActive
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.WebApp
{
  public partial class ajaxActive : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.CheckFormUrl())
        this.Response.End();
      this.Admin_Load("master", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "GetHBInfo":
          this.GetHBInfo();
          break;
        case "PaiFaHB":
          this.PaiFaHB();
          break;
        case "GetBetActiveInfo":
          this.GetBetActiveInfo();
          break;
        case "PaiFaBetActive":
          this.PaiFaBetActive();
          break;
        case "GetGroup2Gz":
          this.GetGroup2Gz();
          break;
        case "SaveGroup2Gz":
          this.SaveGroup2Gz();
          break;
        case "GetGroup3Gz":
          this.GetGroup3Gz();
          break;
        case "SaveGroup3Gz":
          this.SaveGroup3Gz();
          break;
        case "GetGroupGzJSON":
          this.GetGroupGzJSON();
          break;
        case "GetRegChargeJSON":
          this.GetRegChargeJSON();
          break;
        case "SaveRegCharge":
          this.SaveRegCharge();
          break;
        default:
          this.DefaultResponse();
          break;
      }
      this.Response.Write(this._response);
    }

    private void DefaultResponse()
    {
      this._response = this.JsonResult(0, "未知操作");
    }

    private void ajaxGetList()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [UserGroup] FROM [N_User] where Id=" + this.AdminId;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    public void GetGroupGzJSON()
    {
      string GroupId = this.q("gId");
      string _jsonstr = "";
      new Lottery.DAL.Flex.ActiveDAL().GetGroupGzJSON(GroupId, this.AdminId, ref _jsonstr);
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + _jsonstr + "}";
    }

    private void GetRegChargeJSON()
    {
      string _jsonstr = "";
      new Lottery.DAL.Flex.ActiveDAL().GetRegChargeJSON(this.AdminId, ref _jsonstr);
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + _jsonstr + "}";
    }

    private void SaveRegCharge()
    {
      this._response = new Lottery.DAL.Flex.ActiveDAL().SaveRegCharge(this.AdminId);
      this._response = this._response.Replace("[", "").Replace("]", "");
    }

    public void GetHBInfo()
    {
      string _jsonstr = "";
      new Lottery.DAL.Flex.ActiveDAL().GetHBInfoJSON(this.AdminId, ref _jsonstr);
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + _jsonstr + "}";
    }

    public void PaiFaHB()
    {
      this._response = "{\"result\":\"0\",\"message\":\"红包大派送活动已停止，请等待下次开放！\"}";
    }

    public void GetBetActiveInfo()
    {
      string _jsonstr = "";
      new Lottery.DAL.Flex.ActiveDAL().GetBetActiveInfoJSON(this.AdminId, ref _jsonstr);
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + _jsonstr + "}";
    }

    public void PaiFaBetActive()
    {
      this._response = new Lottery.DAL.Flex.ActiveDAL().SaveBetActive(this.AdminId, "ActBet", "消费大闯关", "消费大闯关");
      this._response = this._response.Replace("[", "").Replace("]", "");
    }

    private void GetGroup2Gz()
    {
      string _jsonstr = "";
      new Lottery.DAL.Flex.ActiveDAL().GetGroup2GzJSON(this.AdminId, ref _jsonstr);
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + _jsonstr + "}";
    }

    private void SaveGroup2Gz()
    {
      this._response = new Lottery.DAL.Flex.ActiveDAL().SaveGroup2Active(this.AdminId);
      this._response = this._response.Replace("[", "").Replace("]", "");
    }

    private void GetGroup3Gz()
    {
      string _jsonstr = "";
      new Lottery.DAL.Flex.ActiveDAL().GetGroup3GzJSON(this.AdminId, ref _jsonstr);
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + _jsonstr + "}";
    }

    private void SaveGroup3Gz()
    {
      this._response = new Lottery.DAL.Flex.ActiveDAL().SaveGroup3Active(this.AdminId);
      this._response = this._response.Replace("[", "").Replace("]", "");
    }
  }
}
