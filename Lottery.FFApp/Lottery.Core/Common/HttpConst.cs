using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Lottery.Core
{
    /// <summary>
    /// Http相关常量
    /// </summary>
    [Description("Http相关常量")]
    public class HttpConst
    {
        #region 请求状态码 Status Code
        /// <summary>
        /// 200，请求成功
        /// </summary>
        public const string STATUS_CODE_200 = "200";

        /// <summary>
        /// 404，请求资源异常
        /// </summary>
        public const string STATUS_CODE_404 = "404";

        /// <summary>
        /// 500, 服务器异常
        /// </summary>
        public const string STATUS_CODE_500 = "500";

        /// <summary>
        /// 601, 请重新登录
        /// </summary>
        public const string STATUS_CODE_601 = "601";

        /// <summary>
        /// 602, 会员余额不足
        /// </summary>
        public const string STATUS_CODE_602 = "602";
        #endregion

        #region Cookies & Session
        /// <summary>
        /// 登录用户Token键值
        /// </summary>
        public const string COOKIE_MEMBER_AUTH_TOKEN = "Ticket_Token";

        /// <summary>
        /// 登录用户Session键值
        /// </summary>
        public const string SESION_MEMBER = "Ticket_Member";
        #endregion
    }
}
