// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.MailHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using Lottery.Utils.Mail;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;

namespace Lottery.Utils
{
    public static class MailHelp
    {
        public static bool SendOK(string MailTo, string MailSubject, string MailBody, bool IsHtml, string MailFrom, string MailFromName, string MailPwd, string MailSmtpHost, int MailSmtpPort)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.MaxRecipientNum = 80;
            mailMessage.From = ConfigurationManager.AppSettings["Lottery:WebmasterEmail"];
            mailMessage.FromName = MailFromName;
            string str = MailTo;
            char[] chArray = new char[1] { ',' };
            foreach (string recipient in str.Split(chArray))
                mailMessage.AddRecipients(recipient);
            mailMessage.Subject = MailSubject;
            mailMessage.BodyFormat = !IsHtml ? MailFormat.Text : MailFormat.HTML;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.Body = MailBody;
            SmtpClient smtpClient = new SmtpClient(MailSmtpHost, MailSmtpPort);
            if (smtpClient.Send(mailMessage, MailFrom, MailPwd))
                return true;
            MailHelp.SaveErrLog(MailTo, MailFrom, MailFromName, MailSmtpHost, smtpClient.ErrMsg);
            return false;
        }

        private static void SaveSucLog(string MailTo, string MailFrom, string MailFromName, string MailSmtpHost)
        {
            StreamWriter streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath("~/statics/log/mailsuccess_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"), true, Encoding.UTF8);
            streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            streamWriter.WriteLine("\t收 信 人：" + MailTo);
            streamWriter.WriteLine("\tSMTP服务器：" + MailSmtpHost);
            streamWriter.WriteLine("\t发 信 人：" + MailFromName + "<" + MailFrom + ">");
            streamWriter.WriteLine("---------------------------------------------------------------------------------------------------");
            streamWriter.Close();
            streamWriter.Dispose();
        }

        private static void SaveErrLog(string MailTo, string MailFrom, string MailFromName, string MailSmtpHost, string ErrMsg)
        {
            StreamWriter streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath("~/statics/log/mailerror_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"), true, Encoding.UTF8);
            streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            streamWriter.WriteLine("\t收 信 人：" + MailTo);
            streamWriter.WriteLine("\tSMTP服务器：" + MailSmtpHost);
            streamWriter.WriteLine("\t发 信 人：" + MailFromName + "<" + MailFrom + ">");
            streamWriter.WriteLine("\t错误信息：\r\n" + ErrMsg);
            streamWriter.WriteLine("---------------------------------------------------------------------------------------------------");
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}
