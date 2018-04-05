using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Lottery.FFModel;
using Lottery.FFData;

namespace Lottery.Service
{
    public static class UserExtension
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserExtension));
        
        /// <summary>
        /// 转换模型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UserModel ToModel(this N_User entity) 
        {
            if (entity == null)
            {
                return null;
            }

            var model = new UserModel()
            {
                Id = entity.Id,
            };

            return model;
        }

        /// <summary>
        /// 会员是否过期
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool IsTokenExpired(this UserModel model)
        {
            return false;
        }

        /// <summary>
        /// 会员登录是否过期(7天)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsTokenExpired(this N_User entity)
        {
            return entity == null || !entity.ExpirationTime.HasValue || entity.ExpirationTime.Value < DateTime.Now;
        }
    }
}
