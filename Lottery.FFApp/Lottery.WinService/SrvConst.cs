using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.WinService
{
    public class SrvConst
    {
        public static string ConnStr = string.Empty;
        public static string ApiHost = string.Empty;

        static SrvConst()
        {
            if (ConfigurationManager.ConnectionStrings["ConnStr"] != null)
            {
                ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            }

            if (ConfigurationManager.AppSettings["ApiHost"] != null)
            {
                ApiHost = ConfigurationManager.AppSettings["ApiHost"].ToString();
            }

        }

        public const string SuccMsg = "succ";
        public const string SuccState = "200";
    }
}
