// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Mail.SmtpServerHelper
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Lottery.Utils.Mail
{
  public class SmtpServerHelper
  {
    private string CRLF = "\r\n";
    private string logs = "";
    private Hashtable ErrCodeHT = new Hashtable();
    private Hashtable RightCodeHT = new Hashtable();
    private string errmsg;
    private TcpClient tcpClient;
    private NetworkStream networkStream;

    public string ErrMsg
    {
      set
      {
        this.errmsg = value;
      }
      get
      {
        return this.errmsg;
      }
    }

    public SmtpServerHelper()
    {
      this.SMTPCodeAdd();
    }

    ~SmtpServerHelper()
    {
      this.networkStream.Close();
      this.tcpClient.Close();
    }

    private string Base64Encode(string str)
    {
      return Convert.ToBase64String(Encoding.Default.GetBytes(str));
    }

    private string Base64Decode(string str)
    {
      return Encoding.Default.GetString(Convert.FromBase64String(str));
    }

    private string GetStream(string FilePath)
    {
      FileStream fileStream = new FileStream(FilePath, FileMode.Open);
      byte[] numArray = new byte[Convert.ToInt32(fileStream.Length)];
      fileStream.Read(numArray, 0, numArray.Length);
      fileStream.Close();
      return Convert.ToBase64String(numArray);
    }

    private void SMTPCodeAdd()
    {
      this.ErrCodeHT.Add((object) "421", (object) "服务未就绪，关闭传输信道");
      this.ErrCodeHT.Add((object) "432", (object) "需要一个密码转换");
      this.ErrCodeHT.Add((object) "450", (object) "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）");
      this.ErrCodeHT.Add((object) "451", (object) "放弃要求的操作；处理过程中出错");
      this.ErrCodeHT.Add((object) "452", (object) "系统存储不足，要求的操作未执行");
      this.ErrCodeHT.Add((object) "454", (object) "临时认证失败");
      this.ErrCodeHT.Add((object) "500", (object) "邮箱地址错误");
      this.ErrCodeHT.Add((object) "501", (object) "参数格式错误");
      this.ErrCodeHT.Add((object) "502", (object) "命令不可实现");
      this.ErrCodeHT.Add((object) "503", (object) "服务器需要SMTP验证");
      this.ErrCodeHT.Add((object) "504", (object) "命令参数不可实现");
      this.ErrCodeHT.Add((object) "530", (object) "需要认证");
      this.ErrCodeHT.Add((object) "534", (object) "认证机制过于简单");
      this.ErrCodeHT.Add((object) "538", (object) "当前请求的认证机制需要加密");
      this.ErrCodeHT.Add((object) "550", (object) "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）");
      this.ErrCodeHT.Add((object) "551", (object) "用户非本地，请尝试<forward-path>");
      this.ErrCodeHT.Add((object) "552", (object) "过量的存储分配，要求的操作未执行");
      this.ErrCodeHT.Add((object) "553", (object) "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）");
      this.ErrCodeHT.Add((object) "554", (object) "传输失败");
      this.RightCodeHT.Add((object) "220", (object) "服务就绪");
      this.RightCodeHT.Add((object) "221", (object) "服务关闭传输信道");
      this.RightCodeHT.Add((object) "235", (object) "验证成功");
      this.RightCodeHT.Add((object) "250", (object) "要求的邮件操作完成");
      this.RightCodeHT.Add((object) "251", (object) "非本地用户，将转发向<forward-path>");
      this.RightCodeHT.Add((object) "334", (object) "服务器响应验证Base64字符串");
      this.RightCodeHT.Add((object) "354", (object) "开始邮件输入，以<CRLF>.<CRLF>结束");
    }

    private bool SendCommand(string str)
    {
      if (str == null || str.Trim() == string.Empty)
        return true;
      this.logs += str;
      byte[] bytes = Encoding.Default.GetBytes(str);
      try
      {
        this.networkStream.Write(bytes, 0, bytes.Length);
      }
      catch
      {
        this.errmsg = "网络连接错误";
        return false;
      }
      return true;
    }

    private string RecvResponse()
    {
      string empty = string.Empty;
      byte[] numArray = new byte[1024];
      int length;
      try
      {
        length = this.networkStream.Read(numArray, 0, numArray.Length);
      }
      catch
      {
        this.errmsg = "网络连接错误";
        return "false";
      }
      if (length == 0)
        return empty;
      string str = Encoding.Default.GetString(numArray).Substring(0, length);
      SmtpServerHelper smtpServerHelper = this;
      smtpServerHelper.logs = smtpServerHelper.logs + str + this.CRLF;
      return str;
    }

    private bool Dialog(string str, string errstr)
    {
      if (str == null || str.Trim() == string.Empty)
        return true;
      if (!this.SendCommand(str))
        return false;
      string str1 = this.RecvResponse();
      if (str1 == "false")
        return false;
      string str2 = str1.Substring(0, 3);
      if (this.RightCodeHT[(object) str2] != null)
        return true;
      if (this.ErrCodeHT[(object) str2] != null)
      {
        SmtpServerHelper smtpServerHelper = this;
        smtpServerHelper.errmsg = smtpServerHelper.errmsg + str2 + this.ErrCodeHT[(object) str2].ToString();
        this.errmsg += this.CRLF;
      }
      else
        this.errmsg += str1;
      this.errmsg += errstr;
      return false;
    }

    private bool Dialog(string[] str, string errstr)
    {
      for (int index = 0; index < str.Length; ++index)
      {
        if (!this.Dialog(str[index], ""))
        {
          this.errmsg += this.CRLF;
          this.errmsg += errstr;
          return false;
        }
      }
      return true;
    }

    private bool Connect(string smtpServer, int port)
    {
      try
      {
        this.tcpClient = new TcpClient(smtpServer, port);
      }
      catch (Exception ex)
      {
        this.errmsg = ex.ToString();
        return false;
      }
      this.networkStream = this.tcpClient.GetStream();
      if (this.RightCodeHT[(object) this.RecvResponse().Substring(0, 3)] != null)
        return true;
      this.errmsg = "网络连接失败";
      return false;
    }

    private string GetPriorityString(MailPriority mailPriority)
    {
      string str = "Normal";
      switch (mailPriority)
      {
        case MailPriority.Low:
          str = "Low";
          break;
        case MailPriority.High:
          str = "High";
          break;
      }
      return str;
    }

    public bool SendEmail(string smtpServer, int port, MailMessage mailMessage)
    {
      return this.SendEmail(smtpServer, port, false, "", "", mailMessage);
    }

    public bool SendEmail(string smtpServer, int port, string username, string password, MailMessage mailMessage)
    {
      return this.SendEmail(smtpServer, port, true, username, password, mailMessage);
    }

    private bool SendEmail(string smtpServer, int port, bool ESmtp, string username, string password, MailMessage mailMessage)
    {
      if (!this.Connect(smtpServer, port))
        return false;
      string priorityString = this.GetPriorityString(mailMessage.Priority);
      bool flag = mailMessage.BodyFormat == MailFormat.HTML;
      if (ESmtp)
      {
        if (!this.Dialog(new string[4]
        {
          "EHLO " + smtpServer + this.CRLF,
          "AUTH LOGIN" + this.CRLF,
          this.Base64Encode(username) + this.CRLF,
          this.Base64Encode(password) + this.CRLF
        }, "SMTP服务器验证失败，请核对用户名和密码。"))
          return false;
      }
      else if (!this.Dialog("HELO " + smtpServer + this.CRLF, ""))
        return false;
      if (!this.Dialog("MAIL FROM:<" + username + ">" + this.CRLF, "发件人地址错误，或不能为空"))
        return false;
      string[] str1 = new string[mailMessage.Recipients.Count];
      for (int index = 0; index < mailMessage.Recipients.Count; ++index)
        str1[index] = "RCPT TO:<" + (string) mailMessage.Recipients[index] + ">" + this.CRLF;
      if (!this.Dialog(str1, "收件人地址有误") || !this.Dialog("DATA" + this.CRLF, ""))
        return false;
      string str2 = "From:" + mailMessage.FromName + "<" + mailMessage.From + ">" + this.CRLF;
      if (mailMessage.Recipients.Count == 0)
        return false;
      string str3 = str2 + "To:=?" + mailMessage.Charset.ToUpper() + "?B?" + this.Base64Encode((string) mailMessage.Recipients[0]) + "?=<" + (string) mailMessage.Recipients[0] + ">" + this.CRLF;
      string str4;
      if (!(mailMessage.Subject == string.Empty) && mailMessage.Subject != null)
      {
        if (!(mailMessage.Charset == ""))
          str4 = "Subject:=?" + mailMessage.Charset.ToUpper() + "?B?" + this.Base64Encode(mailMessage.Subject) + "?=";
        else
          str4 = "Subject:" + mailMessage.Subject;
      }
      else
        str4 = "Subject:";
      string crlf = this.CRLF;
      string str5 = str3 + str4 + crlf + "X-Priority:" + priorityString + this.CRLF + "X-MSMail-Priority:" + priorityString + this.CRLF + "Importance:" + priorityString + this.CRLF + "X-Mailer: Lottery.Utils.Mail.SmtpMail Pubclass [cn]" + this.CRLF + "MIME-Version: 1.0" + this.CRLF;
      if (mailMessage.Attachments.Count != 0)
        str5 = str5 + "Content-Type: multipart/mixed;" + this.CRLF + " boundary=\"=====" + (flag ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====\"" + this.CRLF + this.CRLF;
      string str6;
      if (flag)
      {
        str6 = (mailMessage.Attachments.Count != 0 ? str5 + "This is a multi-part message in MIME format." + this.CRLF + this.CRLF + "--=====001_Dragon520636771063_=====" + this.CRLF + "Content-Type: multipart/alternative;" + this.CRLF + " boundary=\"=====003_Dragon520636771063_=====\"" + this.CRLF + this.CRLF : str5 + "Content-Type: multipart/alternative;" + this.CRLF + " boundary=\"=====003_Dragon520636771063_=====\"" + this.CRLF + this.CRLF + "This is a multi-part message in MIME format." + this.CRLF + this.CRLF) + "--=====003_Dragon520636771063_=====" + this.CRLF + "Content-Type: text/plain;" + this.CRLF + (mailMessage.Charset == "" ? " charset=\"iso-8859-1\"" : " charset=\"" + mailMessage.Charset.ToLower() + "\"") + this.CRLF + "Content-Transfer-Encoding: base64" + this.CRLF + this.CRLF + this.Base64Encode("邮件内容为HTML格式，请选择HTML方式查看") + this.CRLF + this.CRLF + "--=====003_Dragon520636771063_=====" + this.CRLF + "Content-Type: text/html;" + this.CRLF + (mailMessage.Charset == "" ? " charset=\"iso-8859-1\"" : " charset=\"" + mailMessage.Charset.ToLower() + "\"") + this.CRLF + "Content-Transfer-Encoding: base64" + this.CRLF + this.CRLF + this.Base64Encode(mailMessage.Body) + this.CRLF + this.CRLF + "--=====003_Dragon520636771063_=====--" + this.CRLF;
      }
      else
      {
        if (mailMessage.Attachments.Count != 0)
          str5 = str5 + "--=====001_Dragon303406132050_=====" + this.CRLF;
        str6 = str5 + "Content-Type: text/plain;" + this.CRLF + (mailMessage.Charset == "" ? " charset=\"iso-8859-1\"" : " charset=\"" + mailMessage.Charset.ToLower() + "\"") + this.CRLF + "Content-Transfer-Encoding: base64" + this.CRLF + this.CRLF + this.Base64Encode(mailMessage.Body) + this.CRLF;
      }
      if (mailMessage.Attachments.Count != 0)
      {
        for (int index = 0; index < mailMessage.Attachments.Count; ++index)
        {
          string attachment = mailMessage.Attachments[index];
          str6 = str6 + "--=====" + (flag ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====" + this.CRLF + "Content-Type: text/plain;" + this.CRLF + " name=\"=?" + mailMessage.Charset.ToUpper() + "?B?" + this.Base64Encode(attachment.Substring(attachment.LastIndexOf("\\") + 1)) + "?=\"" + this.CRLF + "Content-Transfer-Encoding: base64" + this.CRLF + "Content-Disposition: attachment;" + this.CRLF + " filename=\"=?" + mailMessage.Charset.ToUpper() + "?B?" + this.Base64Encode(attachment.Substring(attachment.LastIndexOf("\\") + 1)) + "?=\"" + this.CRLF + this.CRLF + this.GetStream(attachment) + this.CRLF + this.CRLF;
        }
        str6 = str6 + "--=====" + (flag ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====--" + this.CRLF + this.CRLF;
      }
      if (!this.Dialog(str6 + this.CRLF + "." + this.CRLF, "错误信件信息") || !this.Dialog("QUIT" + this.CRLF, "断开连接时错误"))
        return false;
      this.networkStream.Close();
      this.tcpClient.Close();
      return true;
    }
  }
}
