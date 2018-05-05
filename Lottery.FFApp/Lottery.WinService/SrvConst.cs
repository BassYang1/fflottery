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

        static SrvConst()
        {
            if (ConfigurationManager.ConnectionStrings["ConnStr"] != null)
            {
                ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            }
        }
    }
}
