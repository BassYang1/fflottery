using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Lottery.FFApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            
            //定时器
            System.Timers.Timer myTimer = new System.Timers.Timer(12 * 60 * 60 * 1000);
            //System.Timers.Timer myTimer = new System.Timers.Timer(10 * 60 * 1000);
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
        }

        /// <summary>
        /// 发送提现记录
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void myTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                Common.SendCashRecords();
            }
            catch { }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}