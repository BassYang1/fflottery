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
    public static class LotteryExtension
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(LotteryExtension));
        
        /// <summary>
        /// 转换模型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static LotteryModel ToModel(this Sys_Lottery entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new LotteryModel()
            {
                Id = entity.Id,
                CloseTime = entity.CloseTime ?? 0,
                Code = entity.Code,
                Title = entity.Title,
                IphoneSort = entity.IphoneSort ?? 0,
                IsOpen = entity.IsOpen ?? 0,
                IssNum = entity.IssNum ?? 0,
                Ltype = entity.Ltype ?? 0,
                MaxTimes = entity.MaxTimes ?? 0,
                MinTimes = entity.MinTimes ?? 0,
                Sort = entity.Sort ?? 0
            };

            return model;
        }
    }
}
