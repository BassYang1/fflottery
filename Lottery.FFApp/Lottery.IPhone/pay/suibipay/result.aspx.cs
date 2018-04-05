using log4net;
using Lottery.DAL.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.IPhone.SBF
{
    /// <summary>
    /// 支付返回页面
    /// </summary>
    public partial class result : System.Web.UI.Page
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(result));

        /// <summary>
        /// 支付结果
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 支付消息
        /// </summary>
        public string Message { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.IsSuccess = false;
            this.Message = string.Empty;

            try
            {
                Log.Debug("*随笔付回调参数********************************");
                Log.Debug(SbfHelper.GetRequestData());

                //商户Id
                string pUserId = SbfHelper.Trim(Request["P_UserId"]);
                //订单号
                string pOrderId = SbfHelper.Trim(Request["P_OrderId"]);
                //银行卡和密码
                string pCardId = SbfHelper.Trim(Request["P_CardId"]);
                string pCardPass = SbfHelper.Trim(Request["P_CardId"]);
                //支付金额
                string pFaceValue = SbfHelper.Trim(Request["P_FaceValue"]);
                string pPrice = SbfHelper.Trim(Request["P_Price"]);
                //支付方式
                string pChannelId = SbfHelper.Trim(Request["P_ChannelId"]);
                //商品名称
                string pSubject = SbfHelper.Trim(Request["P_Subject"]);
                string pDescription = SbfHelper.Trim(Request["P_Description"]);
                string pNotic = SbfHelper.Trim(Request["P_Notic"]);
                //商品数量
                string pQuantity = SbfHelper.Trim(Request["P_Quantity"]);
                //支付金额
                string pPayMoney = SbfHelper.Trim(Request["P_PayMoney"]);
                //错误信息
                string pErrCode = SbfHelper.Trim(Request["P_ErrCode"]);

                //签名认证串
                string pPostKey = SbfHelper.Trim(Request["P_PostKey"]);
                string preEncodeStr = pUserId + "|" + pOrderId + "|" + pCardId + "|" + pCardPass + "|" + pFaceValue + "|" + pChannelId + "|" + SbfHelper.SBF_USER_KEY;
                string encodeStr = SbfHelper.GetMD5(preEncodeStr, "gb2312");

                //if (pPostKey.CompareTo(encodeStr) == 0)
                //{
                    if (int.Parse(pErrCode) == 0)//说明是充值成功了的
                    {
                        if (new UserChargeDAL().Update(pOrderId) == false)
                        {
                            Log.ErrorFormat("更新系统支付订单失败，订单号: {0}", pOrderId);
                            this.Message = "更新系统支付订单失败";
                        }
                        else
                        {
                            Log.InfoFormat("支付成功，订单号: {0}", pOrderId);
                            this.IsSuccess = true;
                            this.Message = "支付成功";
                        }
                    }
                //}
                //else
                //{
                //    Log.ErrorFormat("第三方支付失败，订单号: {0}", pOrderId);
                //    this.Message = "第三方支付失败";
                //}
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("支付失败: {0}", ex);
                this.Message = ex.Message;
            }
        }
    }
}