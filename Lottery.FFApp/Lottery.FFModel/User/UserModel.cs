using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 会员
    /// </summary>
    [Description("会员")]
    public class UserModel
    {
        [Description("Token")]
        public string Token { get; set; }

        [Description("用户Id")]
        public int Id { get; set; }
    }
}
