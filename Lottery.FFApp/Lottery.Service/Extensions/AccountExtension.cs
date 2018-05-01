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
    public static class AccountExtension 
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountExtension));
        
        /// <summary>
        /// 转换模型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UserWithdrawRecordModel ToModel(this UserWithdrawEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new UserWithdrawRecordModel()
            {
                SsId = entity.SsId,
                UserName = entity.UserName,
                Amount = entity.Money ?? 0,
                OrderId = entity.Ss3Id,
                State = entity.State.HasValue && entity.State == 0
                    ? (entity.State == 1 ? "已完成" : "待处理")
                    : "提款失败",
                STime = entity.STime.HasValue ? entity.STime.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""
            };

            return model;
        }

        /// <summary>
        /// 转换模型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UserChargeRecordModel ToModel(this UserChargeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new UserChargeRecordModel()
            {
                SsId = entity.SsId,
                UserName = entity.UserName,
                Amount = entity.InMoney ?? 0,
                InAmount = entity.DzMoney ?? 0,
                OrderId = entity.Ss3Id,
                State = entity.State.HasValue && entity.State == 1 ? "已完成" : "待处理",
                STime = entity.STime.HasValue ? entity.STime.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""
            };

            return model;
        }

        public static string LotteryState(int state)
        {
            switch (state)
            {
                case 1:
                    return "未开奖";
                case 2:
                    return "已撤销";
                case 3:
                    return "未中奖";
                case 4:
                    return "已中奖";
                default:
                    return "无效状态";
            }
        }

        public static string LotteryTitle(int Id)
        {
            switch (Id)
            {
                case 1001:
                    return "重庆时时彩";
                case 1002:
                    return "江西时时彩";
                case 1003:
                    return "新疆时时彩";
                case 1004:
                    return "纽约30秒彩";
                case 1005:
                    return "腾讯分分彩";
                case 1006:
                    return "优乐秒秒彩";
                case 1007:
                    return "天津时时彩";
                case 1008:
                    return "云南时时彩";
                case 1009:
                    return "泰国一分彩";
                case 1010:
                    return "韩国1.5分";
                case 1011:
                    return "新德里1.5分彩";
                case 1012:
                    return "新加坡2分彩";
                case 1013:
                    return "台湾5分彩";
                case 1014:
                    return "老东京1.5分彩";
                case 1015:
                    return "菲律宾1.5分彩";
                case 1016:
                    return "东京1.5分彩";
                case 1017:
                    return "老韩国1.5分";
                case 1018:
                    return "新加坡30秒";
                case 1019:
                    return "台湾45秒";
                case 1020:
                    return "首尔60秒";
                case 2001:
                    return "山东11选5";
                case 2002:
                    return "广东11选5";
                case 2003:
                    return "上海11选5";
                case 2004:
                    return "江西11选5";
                case 2005:
                    return "纽约30秒11选5";
                case 2006:
                    return "韩国1.5分11选5";
                case 3001:
                    return "时时乐";
                case 3002:
                    return "福彩3D";
                case 3003:
                    return "体彩P3";
                case 3004:
                    return "韩国90秒3D";
                case 3005:
                    return "纽约30秒3D";
                case 3006:
                    return "一分3d";
                case 4001:
                    return "北京PK10";
                case 4002:
                    return "英国30秒PK10";
                case 4003:
                    return "英国60秒 ";
                case 4004:
                    return "英国120秒赛车";
                case 5005:
                    return "广西快3";
                case 6001:
                    return "六合彩";
                default:
                    return "未知彩票";
            }
        }
    }
}
