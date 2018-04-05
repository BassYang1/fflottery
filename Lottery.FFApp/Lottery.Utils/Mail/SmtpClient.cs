// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Mail.SmtpClient
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

namespace Lottery.Utils.Mail
{
    public class SmtpClient
    {
        private string _SmtpServer;
        private int _SmtpPort;
        private string errmsg;

        public SmtpClient()
        {
        }

        public SmtpClient(string _smtpServer, int _smtpPort)
        {
            this._SmtpServer = _smtpServer;
            this._SmtpPort = _smtpPort;
        }

        public string ErrMsg
        {
            get
            {
                return this.errmsg;
            }
        }

        public string SmtpServer
        {
            set
            {
                this._SmtpServer = value;
            }
            get
            {
                return this._SmtpServer;
            }
        }

        public int SmtpPort
        {
            set
            {
                this._SmtpPort = value;
            }
            get
            {
                return this._SmtpPort;
            }
        }

        public bool Send(MailMessage mailMessage, string username, string password)
        {
            SmtpServerHelper smtpServerHelper = new SmtpServerHelper();
            if (smtpServerHelper.SendEmail(this._SmtpServer, this._SmtpPort, username, password, mailMessage))
                return true;
            this.errmsg = smtpServerHelper.ErrMsg;
            return false;
        }
    }
}
