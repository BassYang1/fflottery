// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxMoney
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using log4net;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;
using System;

namespace Lottery.WebApp
{
    public partial class ajaxMoney : UserCenterSession
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(ajaxMoney));

        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "json");
            this._operType = this.q("oper");
            switch (this._operType)
            {
                case "ajaxCharge":
                    this.ajaxCharge();
                    break;
                case "ajaxChargeState": //支付订单状态
                    this.ajaxChargeState();
                    break;
                case "ajaxChargeOrderId": //获取支付订单Id
                    this._response = this.JsonResult(1, SsId.Charge);
                    break;
                case "ajaxGetChargeList":
                    this.ajaxGetChargeList();
                    break;
                case "ajaxCash":
                    this.ajaxCash();
                    break;
                case "ajaxGetCashList":
                    this.ajaxGetCashList();
                    break;
                case "ajaxGetTranAccList":
                    this.ajaxGetTranAccList();
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

        private void ajaxCharge()
        {
            string bankId = this.f("setid");
            string str1 = this.f("name");
            string str2 = this.f("money");
            this.f("code");
            int num = new Lottery.DAL.Flex.UserChargeDAL().Save(this.AdminId, bankId, str1.Trim(), Convert.ToDecimal(str2));
            if (num == -1)
                this._response = this.JsonResult(0, "充值金额不能小于最小充值金额!");
            else if (num > 0)
                this._response = this.JsonResult(1, this.AdminId.ToString());
            else
                this._response = this.JsonResult(0, "充值失败");
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        private void ajaxChargeState()
        {
            //用户Id
            int userId;
            Int32.TryParse(this.q("userId"), out userId);

            //订单号
            string orderId = this.q("orderid");
            orderId = string.IsNullOrEmpty(orderId) ? "" : orderId;

            //无效订单
            this._response = this.JsonResult(-1, "无效订单");

            if (!string.IsNullOrEmpty(orderId))
            {
                try
                {
                    int num = new Lottery.DAL.Flex.UserChargeDAL().GetState(userId, orderId);

                    if (num == 0)
                    {
                        this._response = this.JsonResult(0, "待支付");
                    }
                    else if (num == 1)
                    {
                        this._response = this.JsonResult(1, "支付成功");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    this._response = this.JsonResult(-1, "无效订单");
                }
            }
        }

        private void ajaxGetChargeList()
        {
            string str1 = this.q("state");
            string str2 = this.q("d1") + " 00:00:00";
            string str3 = this.q("d2") + " 23:59:59";
            int _thispage = this.Int_ThisPage();
            int _pagesize = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            string _wherestr1 = "UserId =" + this.AdminId;
            if (str2.Trim().Length == 0)
                str2 = this.StartTime;
            if (str3.Trim().Length == 0)
                str3 = this.EndTime;
            if (Convert.ToDateTime(str2) > Convert.ToDateTime(str3))
                str2 = str3;
            if (str2.Trim().Length > 0 && str3.Trim().Length > 0)
                _wherestr1 = _wherestr1 + " and STime >='" + str2 + "' and STime <='" + str3 + "'";
            if (!string.IsNullOrEmpty(str1))
                _wherestr1 = _wherestr1 + " and state=" + str1;
            string _jsonstr = "";
            new WebAppListOper().GetChargeListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
            this._response = _jsonstr;
        }

        private void ajaxCash()
        {
            string BankId = this.f("bankId");
            string UserBankId = this.f("userBankId");
            string Money = this.f("money");
            string PassWord = this.f("pass");
            this.f("code");
            this._response = this.JsonResult(0, new UserGetCashDAL().UserGetCash(this.AdminId, UserBankId, BankId, Money, PassWord));
        }

        private void ajaxGetCashList()
        {
            string str1 = this.q("d1") + " 00:00:00";
            string str2 = this.q("d2") + " 23:59:59";
            string str3 = this.q("state");
            int _thispage = this.Int_ThisPage();
            int _pagesize = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            string _wherestr1 = "UserId =" + this.AdminId;
            if (str1.Trim().Length == 0)
                str1 = this.StartTime;
            if (str2.Trim().Length == 0)
                str2 = this.EndTime;
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
                _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
            if (!string.IsNullOrEmpty(str3))
                _wherestr1 = _wherestr1 + " and State =" + str3;
            string _jsonstr = "";
            new WebAppListOper().GetCashListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
            this._response = _jsonstr;
        }

        private void ajaxGetTranAccList()
        {
            string str1 = this.q("d1") + " 00:00:00";
            string str2 = this.q("d2") + " 23:59:59";
            string str3 = this.q("state");
            int _thispage = this.Int_ThisPage();
            int _pagesize = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            string _wherestr1 = string.Format("(UserId ={0} or ToUserId={0})", (object)this.AdminId);
            if (str1.Trim().Length == 0)
                str1 = this.StartTime;
            if (str2.Trim().Length == 0)
                str2 = this.EndTime;
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
                _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
            if (!string.IsNullOrEmpty(str3))
                _wherestr1 = _wherestr1 + " and Type =" + str3;
            string _jsonstr = "";
            new WebAppListOper().GetTranAccListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
            this._response = _jsonstr;
        }
    }
}
