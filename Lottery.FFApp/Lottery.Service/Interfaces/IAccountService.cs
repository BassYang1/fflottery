using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using Lottery.FFModel;
using Lottery.Core;
using Lottery.FFData;
using System.Data;
using Lottery.Utils;
using Lottery.DAL;

namespace Lottery.Service
{
    public interface IAccountService
    {
        
        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="model">充值信息</param>
        /// <returns>账户余额</returns>
        UserChargeResultModel Charge(UserChargeModel model);

        /// <summary>
        /// 会员取款
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        UserWithdrawResultModel Withdraw(UserWithdrawModel model);        

        /// <summary>
        /// 分页查询充值记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>充值记录</returns>
        PageData<UserChargeRecordModel> SearchCharge(SearchChargeModel query, int pageSize, int pageIndex);
        
        /// <summary>
        /// 分页查询提现记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>提现记录</returns>
        PageData<UserWithdrawRecordModel> SearchWithdraw(SearchWithdrawModel query, int pageSize, int pageIndex);
    }
}