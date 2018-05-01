using Lottery.FFData;
using Lottery.FFModel;
using Lottery.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    /// <summary>
    /// 投注
    /// </summary>
    public class BetService : BaseService, IBetService
    {
        /// <summary>
        /// 查询投注记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>投注数据</returns>
        public PageData<UserBetRecordModel> SearchBets(SearchBetModel query, int pageSize, int pageIndex)
        {
            query.StartTime = Convert.ToDateTime(query.StartTime.HasValue == false
                ? DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd") + " 00:00:00"
                : query.StartTime.Value.ToString("yyyy-MM-dd HH:ss:mm"));

            query.EndTime = Convert.ToDateTime(query.EndTime.HasValue == false
                ? DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00"
                : query.EndTime.Value.ToString("yyyy-MM-dd HH:ss:mm"));
                
            using (var db = new TicketEntities())
            {
                var entities = db.N_UserBet.Where(b => b.STime.HasValue == false || (b.STime >= query.StartTime && b.STime <= query.EndTime));

                //状态
                if (query != null && query.State != BetStateEnum.NONE)
                {
                    entities = entities.Where(item => item.State == (int)query.State);
                }
                
                //用户名
                var betEntities = entities.Join(db.N_User.Where(u => string.IsNullOrEmpty(query.UserName) || u.UserName.Contains(query.UserName)),
                    b => b.UserId, u => u.Id,
                    (b, u) => new UserBetEntity(b)
                    {
                        UserName = u.UserName
                    });


                //分页处理
                int totalCount;
                var pgData = GetPagingData(betEntities.ToList(), pageSize, pageIndex, out totalCount);

                //转换实体
                var bets = new List<UserBetRecordModel>();
                pgData.ForEach(entity => bets.Add(entity.ToModel()));

                return new PageData<UserBetRecordModel>(bets, pageSize, pageIndex, totalCount);
            }

            //string str3 = this.q("lid");
            //string str4 = this.q("pid");
            //string str5 = this.q("type");
            //string str6 = this.q("state");
            //string str7 = this.q("moshi");
            //string str8 = this.q("sel");
            //string str9 = this.q("u");
            //int _thispage = this.Int_ThisPage();
            //int _pagesize = this.Str2Int(this.q("pagesize"), 20);
            //this.Str2Int(this.q("flag"), 0);


            //if (Convert.ToDateTime(startTime) > Convert.ToDateTime(str2))
            //    str1 = str2;
            //string _wherestr1 = "";
            //if (str5 == "")
            //    _wherestr1 = _wherestr1 + " UserCode like '%" + Strings.PadLeft(this.AdminId) + "%'";
            //if (str5 == "1")
            //    _wherestr1 = _wherestr1 + " UserId =" + this.AdminId;
            //if (str5 == "2")
            //    _wherestr1 = _wherestr1 + " ParentId =" + this.AdminId;
            //if (str5 == "3")
            //    _wherestr1 = _wherestr1 + " UserCode like '%" + Strings.PadLeft(this.AdminId) + "%' and UserId<>" + this.AdminId;
            //if (!string.IsNullOrEmpty(str9))
            //{
            //    if (str8 == "ssid")
            //        _wherestr1 = _wherestr1 + " and ssid LIKE '" + str9 + "%'";
            //    if (str8 == "UserName")
            //        _wherestr1 = _wherestr1 + " and UserName LIKE '" + str9 + "%'";
            //    if (str8 == "")
            //        _wherestr1 = _wherestr1 + " and (UserName LIKE '" + str9 + "%' or ssid LIKE '" + str9 + "%')";
            //}
            //if (!string.IsNullOrEmpty(str3))
            //    _wherestr1 = _wherestr1 + " and LotteryId =" + str3;
            //if (!string.IsNullOrEmpty(str4))
            //    _wherestr1 = _wherestr1 + " and PlayId ='" + str4 + "'";
            //if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
            //    _wherestr1 = _wherestr1 + " and STime2 >='" + str1 + "' and STime2 <='" + str2 + "'";
            //if (!string.IsNullOrEmpty(str6))
            //    _wherestr1 = _wherestr1 + " and State =" + str6;
            //if (!string.IsNullOrEmpty(str7))
            //    _wherestr1 = _wherestr1 + " and SingleMoney ='" + str7 + "'";
            //string _jsonstr = "";
            //new WebAppListOper().GetListJSON(_thispage, _pagesize, _wherestr1, this.AdminId, ref _jsonstr);
            //this._response = _jsonstr;
        }
    }
}
