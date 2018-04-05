// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL.Flex
{
    public class SbfDAL : ComData
    {
        /// <summary>
        /// 读取随笔付支持的支付方式
        /// </summary>
        /// <returns></returns>
        public IList<ChannelMapModel> GetSbfChannels()
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                ArrayList existsChns = new ArrayList(); //已存在的类型
                IList<ChannelMapModel> channels = new List<ChannelMapModel>();
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT SysCode, SbfChannel, SbfCode FROM Sys_SbfChannelMap WHERE IsUsed = 1 ORDER BY Id DESC;";
                DataTable dataTable = dbOperHandler.GetDataTable();

                for (int index = 0; index < dataTable.Rows.Count; ++index)
                {
                    string sysCode = dataTable.Rows[index]["SysCode"].ToString().Trim();
                    string sbfChannel = dataTable.Rows[index]["SbfChannel"].ToString().Trim();
                    string sbfCode = dataTable.Rows[index]["SbfCode"].ToString().Trim();

                    if (existsChns.Contains(sysCode) == false)
                    {
                        channels.Add(new ChannelMapModel() { SysCode = sysCode, SbfChannel = sbfChannel, SbfCode = sbfCode });
                        existsChns.Add(sysCode);
                    }
                }

                dataTable.Clear();
                dataTable.Dispose();

                return channels;
            }
        }
    }
}