// Decompiled with JetBrains decompiler
// Type: Lottery.Web.temp.WebPoint
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Lottery.Web.temp
{
    public partial class WebPoint : UserCenterSession
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.doh.Reset();
            this.doh.SqlCmd = "select * from N_UserLevel";
            DataTable dataTable1 = this.doh.GetDataTable();
            for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                string str1 = dataTable1.Rows[index1]["Point"].ToString();
                this.doh.Reset();
                this.doh.SqlCmd = "select * from Sys_PlaySmallType where IsOpen=0";
                DataTable dataTable2 = this.doh.GetDataTable();
                for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
                {
                    string str2 = "{\"SmallTypeId\":\"" + dataTable2.Rows[index2]["Id"].ToString() + "\",\"SmallTypeName\":\"" + dataTable2.Rows[index2]["Title"].ToString() + "\",\"points\": [";
                    Decimal num1;
                    Decimal num2;

                    #region LotteryId = 1
                    if (Convert.ToInt32(dataTable2.Rows[index2]["LotteryId"].ToString()) == 1)
                    {
                        Decimal num3 = Convert.ToDecimal(dataTable2.Rows[index2]["MaxBonus"].ToString());
                        Decimal num4 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus"].ToString());
                        Decimal num5 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus2"].ToString());
                        Decimal num6 = (num3 - num4) / new Decimal(260);
                        if (num5 == new Decimal(0))
                        {
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + str1 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(str1) - num7) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        else
                        {
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z3_L");
                            object[] fields1 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num7 = (Convert.ToDecimal(fields1[0]) - Convert.ToDecimal(fields1[1])) / new Decimal(260);
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z6_L");
                            object[] fields2 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num8 = (Convert.ToDecimal(fields2[0]) - Convert.ToDecimal(fields2[1])) / new Decimal(260);
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num9 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num10 = num4 + Convert.ToDecimal(num9) * new Decimal(2) * num7;
                                    Decimal num11 = num5 + Convert.ToDecimal(num9) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num10).ToString("0.00") + "/" + Convert.ToDouble(num11).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + str1 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num9 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num10 = num4 + Convert.ToDecimal(num9) * new Decimal(2) * num7;
                                    Decimal num11 = num5 + Convert.ToDecimal(num9) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num10).ToString("0.00") + "/" + Convert.ToDouble(num11).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(str1) - num9) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        str2 = str2.Substring(0, str2.Length - 1) + "]}";
                        if (index2 == dataTable2.Rows.Count - 1)
                            stringBuilder.Append(str2);
                        else
                            stringBuilder.Append(str2 + ",");
                    }
                    #endregion
                    
                    #region LotteryId = 2
                    Decimal num12;
                    if (Convert.ToInt32(dataTable2.Rows[index2]["LotteryId"].ToString()) == 2)
                    {
                        Decimal num3 = Convert.ToDecimal(dataTable2.Rows[index2]["MaxBonus"].ToString());
                        Decimal num4 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus"].ToString());
                        Decimal num5 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus2"].ToString());
                        Decimal num6 = (num3 - num4) / new Decimal(260);
                        if (num5 == new Decimal(0))
                        {
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                num12 = new Decimal(0);
                                Decimal num7 = !(Convert.ToDecimal(str1) > new Decimal(130)) ? Convert.ToDecimal(str1) : new Decimal(130);
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + (object)num7 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num8 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num9 = num4 + Convert.ToDecimal(num8) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num9).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(num7) - num8) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        str2 = str2.Substring(0, str2.Length - 1) + "]}";
                        if (index2 == dataTable2.Rows.Count - 1)
                            stringBuilder.Append(str2);
                        else
                            stringBuilder.Append(str2 + ",");
                    }
                    #endregion

                    #region LotteryId = 3
                    if (Convert.ToInt32(dataTable2.Rows[index2]["LotteryId"].ToString()) == 3)
                    {
                        Decimal num3 = Convert.ToDecimal(dataTable2.Rows[index2]["MaxBonus"].ToString());
                        Decimal num4 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus"].ToString());
                        Decimal num5 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus2"].ToString());
                        Decimal num6 = (num3 - num4) / new Decimal(220);
                        if (num5 == new Decimal(0))
                        {
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                num12 = new Decimal(0);
                                Decimal num7 = !(Convert.ToDecimal(str1) > new Decimal(110)) ? Convert.ToDecimal(str1) : new Decimal(110);
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + (object)num7 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num8 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num9 = num4 + Convert.ToDecimal(num8) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num9).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(num7) - num8) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        else
                        {
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z3_L");
                            object[] fields1 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num7 = (Convert.ToDecimal(fields1[0]) - Convert.ToDecimal(fields1[1])) / new Decimal(220);
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z6_L");
                            object[] fields2 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num8 = (Convert.ToDecimal(fields2[0]) - Convert.ToDecimal(fields2[1])) / new Decimal(220);
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num9 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num10 = num4 + Convert.ToDecimal(num9) * new Decimal(2) * num7;
                                    Decimal num11 = num5 + Convert.ToDecimal(num9) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num10).ToString("0.00") + "/" + Convert.ToDouble(num11).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                num12 = new Decimal(0);
                                Decimal num9 = !(Convert.ToDecimal(str1) > new Decimal(110)) ? Convert.ToDecimal(str1) : new Decimal(110);
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + (object)num9 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num10 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num11 = num4 + Convert.ToDecimal(num10) * new Decimal(2) * num7;
                                    Decimal num13 = num5 + Convert.ToDecimal(num10) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num11).ToString("0.00") + "/" + Convert.ToDouble(num13).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(num9) - num10) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        str2 = str2.Substring(0, str2.Length - 1) + "]}";
                        if (index2 == dataTable2.Rows.Count - 1)
                            stringBuilder.Append(str2);
                        else
                            stringBuilder.Append(str2 + ",");
                    }
                    #endregion

                    #region LotteryId = 4
                    if (Convert.ToInt32(dataTable2.Rows[index2]["LotteryId"].ToString()) == 4)
                    {
                        Decimal num3 = Convert.ToDecimal(dataTable2.Rows[index2]["MaxBonus"].ToString());
                        Decimal num4 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus"].ToString());
                        Decimal num5 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus2"].ToString());
                        Decimal num6 = (num3 - num4) / new Decimal(260);
                        if (num5 == new Decimal(0))
                        {
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                num12 = new Decimal(0);
                                Decimal num7 = !(Convert.ToDecimal(str1) > new Decimal(130)) ? Convert.ToDecimal(str1) : new Decimal(130);
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + (object)num7 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num8 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num9 = num4 + Convert.ToDecimal(num8) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num9).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(num7) - num8) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        string str3 = str2.Substring(0, str2.Length - 1) + "]}";
                        if (index2 == dataTable2.Rows.Count - 1)
                            stringBuilder.Append(str3);
                        else
                            stringBuilder.Append(str3 + ",");
                    }
                    #endregion

                    #region LotteryId = 5
                    if (Convert.ToInt32(dataTable2.Rows[index2]["LotteryId"].ToString()) == 5)
                    {
                        Decimal num3 = Convert.ToDecimal(dataTable2.Rows[index2]["MaxBonus"].ToString());
                        Decimal num4 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus"].ToString());
                        Decimal num5 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus2"].ToString());
                        Decimal num6 = (num3 - num4) / new Decimal(260);
                        if (num5 == new Decimal(0))
                        {
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + str1 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(str1) - num7) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        else
                        {
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z3_L");
                            object[] fields1 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num7 = (Convert.ToDecimal(fields1[0]) - Convert.ToDecimal(fields1[1])) / new Decimal(260);
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z6_L");
                            object[] fields2 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num8 = (Convert.ToDecimal(fields2[0]) - Convert.ToDecimal(fields2[1])) / new Decimal(260);
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num9 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num10 = num4 + Convert.ToDecimal(num9) * new Decimal(2) * num7;
                                    Decimal num11 = num5 + Convert.ToDecimal(num9) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num10).ToString("0.00") + "/" + Convert.ToDouble(num11).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + str1 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num9 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num10 = num4 + Convert.ToDecimal(num9) * new Decimal(2) * num7;
                                    Decimal num11 = num5 + Convert.ToDecimal(num9) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num10).ToString("0.00") + "/" + Convert.ToDouble(num11).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(str1) - num9) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        str2 = str2.Substring(0, str2.Length - 1) + "]}";
                        if (index2 == dataTable2.Rows.Count - 1)
                            stringBuilder.Append(str2);
                        else
                            stringBuilder.Append(str2 + ",");
                    }
                    #endregion

                    #region LotteryId = 6
                    if (Convert.ToInt32(dataTable2.Rows[index2]["LotteryId"].ToString()) == 6)
                    {
                        Decimal num3 = Convert.ToDecimal(dataTable2.Rows[index2]["MaxBonus"].ToString());
                        Decimal num4 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus"].ToString());
                        Decimal num5 = Convert.ToDecimal(dataTable2.Rows[index2]["MinBonus2"].ToString());
                        Decimal num6 = (num3 - num4) / new Decimal(260);
                        if (num5 == new Decimal(0))
                        {
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + str1 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    Decimal num7 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num8 = num4 + Convert.ToDecimal(num7) * new Decimal(2) * num6;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num8).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(str1) - num7) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        else
                        {
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z3_L");
                            object[] fields1 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num7 = (Convert.ToDecimal(fields1[0]) - Convert.ToDecimal(fields1[1])) / new Decimal(260);
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Title2=@Title2 and LotteryId=1";
                            this.doh.AddConditionParameter("@Title2", (object)"P_3Z6_L");
                            object[] fields2 = this.doh.GetFields("Sys_PlaySmallType", "MaxBonus,MinBonus");
                            Decimal num8 = (Convert.ToDecimal(fields2[0]) - Convert.ToDecimal(fields2[1])) / new Decimal(260);
                            if (num6 == new Decimal(0))
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num9 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num10 = num4 + Convert.ToDecimal(num9) * new Decimal(2) * num7;
                                    Decimal num11 = num5 + Convert.ToDecimal(num9) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num10).ToString("0.00") + "/" + Convert.ToDouble(num11).ToString("0.00") + "\",\"point\": \"0.00\"},";
                                }
                            }
                            else
                            {
                                this.doh.Reset();
                                this.doh.SqlCmd = "select * from N_UserLevel where (Point=" + str1 + " or Point=0.00) order by Point desc";
                                DataTable dataTable3 = this.doh.GetDataTable();
                                for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
                                {
                                    num1 = new Decimal(0);
                                    num2 = new Decimal(0);
                                    Decimal num9 = Convert.ToDecimal(dataTable3.Rows[index3]["Point"].ToString());
                                    Decimal num10 = num4 + Convert.ToDecimal(num9) * new Decimal(2) * num7;
                                    Decimal num11 = num5 + Convert.ToDecimal(num9) * new Decimal(2) * num8;
                                    str2 = str2 + "{\"no\":" + (object)(index3 + 1) + ",\"bonus\": \"" + Convert.ToDouble(num10).ToString("0.00") + "/" + Convert.ToDouble(num11).ToString("0.00") + "\",\"point\": \"" + Convert.ToDecimal((Convert.ToDecimal(str1) - num9) / new Decimal(10)).ToString("0.00") + "\"},";
                                }
                            }
                        }
                        str2 = str2.Substring(0, str2.Length - 1) + "]}";
                        if (index2 == dataTable2.Rows.Count - 1)
                            stringBuilder.Append(str2);
                        else
                            stringBuilder.Append(str2 + ",");
                    }
                    #endregion                    

                }
                this.SaveJsFile("var PointJsonData={\"result\" :\"1\",\"returnval\" :\"加载完成\",\"recordcount\":1,\"table\": [" + stringBuilder.ToString() + "]}", HttpContext.Current.Server.MapPath("~/statics/json/json_" + str1 + ".js"));
            }
            this.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
