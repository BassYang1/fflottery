// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.UserBetModel
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;

namespace Lottery.Entity
{
    [Serializable]
    public class SysLotteryModel
    {
        /// <summary>
        /// 彩票系统配置
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 彩票名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 封单时间
        /// </summary>
        public int CloseTime { get; set; }

        /// <summary>
        /// API
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
