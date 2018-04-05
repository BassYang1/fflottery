// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.UserContractDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL.Flex
{
    public class UserContractDAL : ComData
    {
        public void GetContractInfoJSON(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT * from N_User where Id=" + UserId);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("SELECT * from Act_SetFHDetail where IsUsed=0");
                        dataTable = dbOperHandler.GetDataTable();
                    }
                    else
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("SELECT * from Act_UserFHDetail where UserId=" + UserId);
                        dataTable = dbOperHandler.GetDataTable();
                    }
                }
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetBetInfoJSON(string StartTime, string EndTime, string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT * from N_User where Id=" + UserId);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("select isnull((select top 1 Group3 from Act_SetFHDetail with(nolock) where IsUsed=0 and Bet>=MinMoney*150000 order by MinMoney desc),0) as Per,Bet \r\n                                            from (SELECT isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as Bet\r\n                                            FROM [N_UserMoneyStatAll] with(nolock)\r\n                                            where (STime>='{0} 00:00:00' and STime<'{1} 00:00:00') and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code({2})+'%') A", (object)StartTime, (object)EndTime, (object)UserId);
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = this.ConverTableToJSON(dataTable);
                    }
                    else
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("select isnull((select top 1 Group3 from Act_UserFHDetail with(nolock) where UserId={2} and Bet>=MinMoney*150000 order by MinMoney desc),0) as Per,Bet \r\n                                            from (SELECT isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as Bet\r\n                                            FROM [N_UserMoneyStatAll] with(nolock)\r\n                                            where (STime>='{0} 00:00:00' and STime<'{1} 00:00:00') and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code({2})+'%') A", (object)StartTime, (object)EndTime, (object)UserId);
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = this.ConverTableToJSON(dataTable);
                    }
                }
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        /// <summary>
        /// 获取用户最大提成百分比
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">提成类型</param>
        /// <returns></returns>
        public double GetMaxPercent(int userId, int type)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                double percent = 0;

                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format(@"SELECT MAX([money]) AS MaxPer FROM N_UserContract A 
                    INNER JOIN N_UserContractDetail B ON A.Id = B.UcId WHERE A.[Type]={0} AND UserId={1}", type, userId);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    percent = Convert.IsDBNull(dataTable.Rows[0]["MaxPer"]) ? 0 : (double)dataTable.Rows[0]["MaxPer"];
                }

                dataTable.Clear();
                dataTable.Dispose();

                return percent;
            }
        }

        /// <summary>
        /// 获取用户最大提成百分比
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">提成类型</param>
        /// <returns></returns>
        public bool HasUsedContract(int userId, int type)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                bool hasContract = false;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format(@"SELECT 1 FROM N_UserContract where [Type]={0} AND UserId={1} AND IsUsed=1", type, userId);
                DataTable dataTable = dbOperHandler.GetDataTable();

                if (dataTable.Rows.Count > 0)
                {
                    hasContract = true;
                }

                dataTable.Clear();
                dataTable.Dispose();

                return hasContract;
            }
        }

        public string SaveContractState(string UserId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "UserId=@UserId";
                dbOperHandler.AddConditionParameter("@UserId", (object)UserId);
                dbOperHandler.AddFieldItem("IsUsed", (object)0);
                if (dbOperHandler.Update("Act_UserFHDetail") > 0)
                    return this.GetJsonResult(1, "契约签订成功！");
            }
            return this.GetJsonResult(0, "契约签订失败！");
        }

        public string SaveContract(string ParentId, string UserId, Decimal money, Decimal per)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "UserId=" + UserId;
                dbOperHandler.Delete("Act_UserFHDetail");
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItem("ParentId", (object)ParentId);
                dbOperHandler.AddFieldItem("UserId", (object)UserId);
                dbOperHandler.AddFieldItem("MinMoney", (object)money);
                dbOperHandler.AddFieldItem("Group3", (object)per);
                dbOperHandler.AddFieldItem("IsUsed", (object)1);
                dbOperHandler.Insert("Act_UserFHDetail");
            }
            return this.JsonResult(1, "分配契约成功！");
        }
    }
}
