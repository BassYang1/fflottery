using Lottery.FFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 注册会员
        /// </summary>
        /// <param name="model">注册信息</param>
        /// <returns>用户登录凭证Token</returns>
        string Regiter(UserRegModel model);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns>用户登录Token</returns>
        UserModel Login(UserLoginModel model);

        /// <summary>
        /// 根据Token获取用户详细信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns>用户详细信息</returns>
        UserModel GetUserDetailByToken(string token);
    }
}
