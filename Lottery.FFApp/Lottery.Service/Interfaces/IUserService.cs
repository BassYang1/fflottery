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
        string RegiterUser(UserRegModel model);

        /// <summary>
        /// 代理商登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns>用户登录Token</returns>
        string GetUserToken(UserAddModel model);
        
        /// <summary>
        /// 根据Token获取用户详细信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns>用户详细信息</returns>
        UserModel GetUserDetailByToken(string token);
        
        /// <summary>
        /// 获取用户下注，最新20条
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="lotteryId">彩票种类Id</param>
        /// <returns>用户下注，最新20条</returns>
        string GetUserBets(int userId, int lotteryId);
    }
}
