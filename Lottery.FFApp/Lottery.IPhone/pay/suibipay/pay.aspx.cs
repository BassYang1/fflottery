using log4net;
using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.IPhone.SBF
{
    /// <summary>
    /// 支付页面
    /// </summary>
    public partial class pay : Page
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(pay));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //用户Id
                string userId = SbfHelper.Trim(Request["userId"]);
                //支付方式唯一识别码Code
                string code = SbfHelper.Trim(Request["setCode"]);
                int chargeSetId = new Lottery.DAL.Flex.UserBankDAL().GetIphoneChargeSetByCode(code);

                #region 组装随笔付参数
                //商户Id
                string pUserId = SbfHelper.SBF_USER;
                //用户订单号
                string pOrderId = SbfHelper.Trim(Request["orderId"]);
                if (String.IsNullOrEmpty(pOrderId))
                {
                    throw new Exception("支付订单无效");
                }

                //银行卡和密码
                string pCardId = string.Empty;
                string pCardPass = string.Empty;

                //支付金额
                decimal amount;
                if (Decimal.TryParse(SbfHelper.Trim(Request["amount"]), out amount) == false)
                {
                    throw new Exception("支付金额无效");
                }

                //amount = 0.01M;
                string pFaceValue = amount.ToString();
                string pPrice = amount.ToString();

                //支付方式: 移动微信或支付宝
                string bank = SbfHelper.Trim(Request["bank"]);
                ChannelMapModel channelMap = SbfHelper.GetChannelMap(bank);

                if (channelMap == null)
                {
                    throw new Exception("支付方式不支持");
                }

                string pChannelId = channelMap.SbfCode; //随笔付支付类型Id
                string pChannelCode = channelMap.SbfCode; //随笔付支付类型码


                //商品名称
                string pSubject = "随笔付支付";
                string pDescription = "随笔付支付" + bank;
                string pNotic = "随笔付支付";
                //商品数量
                string pQuantity = "1";

                //回调页面
                string pResult_url = SbfHelper.GetUrl("result.aspx");
                string pNotify_url = SbfHelper.GetUrl("notify.aspx");

                //签名认证串
                string preEncodeStr = pUserId + "|" + pOrderId + "|" + pCardId + "|" + pCardPass + "|" + pFaceValue + "|" + pChannelId + "|" + SbfHelper.SBF_USER_KEY;
                string pPostKey = SbfHelper.GetMD5(preEncodeStr, "gb2312");

                //组装请求参数
                StringBuilder pars = new StringBuilder();
                pars.AppendFormat("P_UserId={0}", pUserId);
                pars.AppendFormat("&P_OrderId={0}", pOrderId);
                pars.AppendFormat("&P_CardId={0}", pCardId);
                pars.AppendFormat("&P_CardPass={0}", pCardPass);
                pars.AppendFormat("&P_FaceValue={0}", pFaceValue);
                pars.AppendFormat("&P_ChannelId={0}", pChannelId);
                pars.AppendFormat("&P_Subject={0}", pSubject);
                pars.AppendFormat("&P_Price={0}", pPrice);
                pars.AppendFormat("&P_Quantity={0}", pQuantity);
                pars.AppendFormat("&P_Description={0}", pDescription);
                pars.AppendFormat("&P_Notic={0}", pNotic);
                pars.AppendFormat("&P_Result_url={0}", pResult_url);
                pars.AppendFormat("&P_Notify_url={0}", pNotify_url);
                pars.AppendFormat("&channelID={0}", pChannelCode);
                pars.AppendFormat("&P_PostKey={0}", pPostKey);
                #endregion

                //支付API
                string gateway = SbfHelper.SBF_API;

                Log.Info(String.Format("开始支付，订单号: {0}", pOrderId));
                Log.Debug("*系统支付参数********************************");
                Log.Debug(SbfHelper.GetRequestData());

                //支付
                int num = (new UserChargeDAL()).Save(userId, pOrderId, chargeSetId.ToString(), bank, Convert.ToDecimal(pPrice));

                //成功
                if (num == -1)
                {
                    Log.Error("充值金额不能小于最小充值金额!");
                    Response.Write("充值金额不能小于最小充值金额!");
                }
                //失败
                else if (num > 0)
                {
                    Log.Info(String.Format("开始请求第三方支付: {0}?{1}", gateway, pars.ToString()));
                    Response.Redirect(String.Format("{0}?{1}", gateway, pars.ToString()), false); //页面调转
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("支付失败: {0}", ex);
                Response.Write(ex.Message);
            }
        }
    }
}