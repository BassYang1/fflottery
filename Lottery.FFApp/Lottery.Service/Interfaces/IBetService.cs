using Lottery.FFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    public interface IBetService
    {
        /// <summary>
        /// 查询投注记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>投注数据</returns>
        PageData<UserBetRecordModel> SearchBets(SearchBetModel query, int pageSize, int pageIndex);

    }
}
