using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Lottery.FFApp
{
    public class Common
    {
        public static void SendCashRecords()
        {
            //邮箱服务器信息
            string server = ConfigurationManager.AppSettings["EmailServer"];
            string account = ConfigurationManager.AppSettings["EmailAccount"];
            string pwd = ConfigurationManager.AppSettings["EmailPassword"];
            int port;
            Int32.TryParse(ConfigurationManager.AppSettings["EmailPort"], out port);
            string receiver = ConfigurationManager.AppSettings["EmailReceiver"];

            StringBuilder content = new StringBuilder("以下用户申请提现");
            IList<CashGetModel> records = (new UserGetCashDAL()).GetListDataTable();

            if (records.Count > 0)
            {
                content.Append("<table border='1' cellpadding='2'");
                content.Append("<tr><td>提现单号</td><td>用户名</td><td>银行</td><td>银行账户</td><td>支付名称</td><td>提现金额</td><td>备注</td></tr>");

                foreach (CashGetModel m in records)
                {

                    content.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>", m.SsId, m.UserName, m.PayBank, m.PayAccount, m.PayName, m.Money, m.Msg);
                }

                content.Append("</table>");

                MailHelp.SendOK(receiver, "提现记录", content.ToString(), true, account, "非凡娱乐", pwd, server, port);
            }

        }
    }
}