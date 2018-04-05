// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.ContractGzDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL.Flex
{
    public class ContractGzDAL : ComData
    {
        public void IsContract(string UserId, string strWhere, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT top 1 Id from N_UserContract where Type=2 and UserId=" + UserId + strWhere);
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = dataTable.Rows.Count <= 0 ? "{\"result\" :\"0\",\"returnval\" :\"操作成功\"}" : "{\"result\" :\"1\",\"returnval\" :\"操作成功\"}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void IsContract(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT top 1 Id from N_UserContract where Type=2 and UserId=" + UserId);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"}";
                }
                else
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + UserId);
                    dataTable = dbOperHandler.GetDataTable();
                    _jsonstr = Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) != 3 ? "{\"result\" :\"0\",\"returnval\" :\"操作成功\"}" : "{\"result\" :\"1\",\"returnval\" :\"操作成功\"}";
                }
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetContractInfo(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + UserId);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 4) //招商
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("SELECT 4 as [GroupId],[MinMoney],[Money],[IsUsed],[soft] FROM [Act_DayGzSet] where GroupId=4 and IsUsed=0");
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
                    }
                    else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3) //特权直属
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("SELECT 3 as [GroupId],[MinMoney],[Money],[IsUsed],[soft] FROM [Act_DayGzSet] where GroupId=3 and IsUsed=0");
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
                    }
                    else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 2) //直属
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("SELECT 2 as [GroupId],[MinMoney],[Money],[IsUsed],[soft] FROM [Act_DayGzSet] where GroupId=2 and IsUsed=0");
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
                    }
                    else if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) >= 5)
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("SELECT 5 as groupId");
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
                    }
                    else //代理,会员
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = @"SELECT 0 as groupId,[Type],[ParentId],[UserId],[IsUsed],[STime],b.*
                                                    FROM [N_UserContract] a left join [N_UserContractDetail] b on a.Id=b.UcId where Type=2 and UserId=" + UserId;
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
                    }
                }
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        /// <summary>
        /// 获取工资契约信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="_jsonstr"></param>
        public void GetContractInfo2(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT top 1 UserGroup from N_User where Id=" + UserId);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) < 5) //代理,会员,直属,特权直属,招商
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = @"SELECT 0 as groupId,[Type],[ParentId],[UserId],[IsUsed],[STime],b.*
                                                    FROM [N_UserContract] a left join [N_UserContractDetail] b on a.Id=b.UcId where Type=2 and UserId=" + UserId;
                        dataTable = dbOperHandler.GetDataTable();
                        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSONNOHTML(dataTable, 0, "recordcount", "table", true) + "}";
                    }
                }
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public string UpdateContractState(string UserId, string state)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Type=2 and UserId=@UserId";
                dbOperHandler.AddConditionParameter("@UserId", (object)UserId);
                dbOperHandler.AddFieldItem("IsUsed", (object)state);
                dbOperHandler.AddFieldItem("STime2", (object)DateTime.Now);
                if (dbOperHandler.Update("N_UserContract") > 0)
                    return this.JsonResult(1, "契约签订成功！");
            }
            return this.JsonResult(0, "契约签订失败！");
        }

        public int SaveContract(UserContract list)
        {
            int num = 0;
            if (list.UserContractDetails.Count > 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    try
                    {
                        sqlCommand.CommandText = string.Format("delete from [N_UserContract] where Type=2 and UserId={0}", (object)list.UserId);
                        sqlCommand.ExecuteScalar();
                        sqlCommand.CommandText = string.Format("INSERT INTO [N_UserContract]([Type],[ParentId],[UserId],[IsUsed],[STime]) VALUES (2,{0},{1},0,getdate())", (object)list.ParentId, (object)list.UserId);
                        sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                        num = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        sqlCommand.CommandText = string.Format("delete from [N_UserContractDetail] where UcId={0}", (object)num);
                        sqlCommand.ExecuteScalar();
                        foreach (UserContractDetail userContractDetail in list.UserContractDetails)
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [N_UserContractDetail]([UcId],[MinMoney],[Money],[Sort]) VALUES ({0},{1},{2},0)", (object)num, (object)userContractDetail.MinMoney, (object)userContractDetail.Money);
                            sqlCommand.ExecuteScalar();
                        }
                    }
                    catch (Exception ex)
                    {
                        new LogExceptionDAL().Save("系统异常", ex.Message);
                        num = 0;
                    }
                }
            }
            return num;
        }

        public void Delete(string ucid)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Id=" + ucid;
                dbOperHandler.Delete("N_UserContract");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "UcId=" + ucid;
                dbOperHandler.Delete("N_UserContractDetail");
            }
        }
    }
}
