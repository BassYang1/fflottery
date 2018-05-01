using Lottery.FFData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 投注记录扩展实体
    /// </summary>
    [Description("投注记录扩展实体")]
    public class UserBetEntity: N_UserBet
    {
        public UserBetEntity(N_UserBet bet)
        {
            Id = bet.Id;
            SsId = bet.SsId;
            UserId = bet.UserId;
            UserMoney = bet.UserMoney;
            LotteryId = bet.LotteryId;
            PlayId = bet.PlayId;
            PlayCode = bet.PlayCode;
            IssueNum = bet.IssueNum;
            Number = bet.Number;
            SingleMoney = bet.SingleMoney;
            Times = bet.Times;
            Num = bet.Num;
            Detail = bet.Detail;
            DX = bet.DX;
            DS = bet.DS;
            Total = bet.Total;
            Point = bet.Point;
            PointMoney = bet.PointMoney;
            Bonus = bet.Bonus;
            WinNum = bet.WinNum;
            WinBonus = bet.WinBonus;
            RealGet = bet.RealGet;
            Pos = bet.Pos;
            STime = bet.STime;
            STime2 = bet.STime2;
            IsOpen = bet.IsOpen;
            State = bet.State;
            IsDelay = bet.IsDelay;
            IsWin = bet.IsWin;
            STime9 = bet.STime9;
            IsCheat = bet.IsCheat;
            ZhId = bet.ZhId;
            Source = bet.Source;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public String UserName { get; set; }

        /// <summary>
        /// 彩种名
        /// </summary>
        [Description("彩种")]
        public String LotteryName { get; set; }
    }
}
