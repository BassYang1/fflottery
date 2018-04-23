using Lottery.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.Admin.admin
{
    public partial class regMerchant : AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.merchantId.Text) || string.IsNullOrEmpty(this.merchantName.Text))
            {
                this.FinalMessage("商户信息不完整，不能注册！", "/admin/close.htm", 0);
            }
            else
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT top 1 * FROM [N_Merchant] where MerchantId='" + this.merchantId.Text + "'";
                DataTable dataTable1 = this.doh.GetDataTable();

                if (dataTable1 != null && dataTable1.Rows.Count <= 0)
                {
                    string code = GenerateCode();

                    this.doh.Reset();
                    this.doh.SqlCmd = "INSERT INTO [N_Merchant](MerchantId, Name, Code, State) VALUES('" + this.merchantId.Text + "','" + this.merchantName.Text + "','" + code + "',1)";
                    var num = this.doh.ExecuteSqlNonQuery();

                    if (num > 0)
                    {
                        this.FinalMessage("注册成功", "/admin/close.htm", 0);
                    }
                    else
                        this.FinalMessage("注册失败", "/admin/close.htm", 0);
                }
                else
                    this.FinalMessage("商户号已存在，不能注册！", "/admin/close.htm", 0);
            }
        }

        private Random rnd = new Random();
        private int seed = 0;
        /// <summary>
        /// 生成用户Token
        /// </summary>
        /// <returns></returns>
        private string GenerateCode()
        {
            var rndData = new byte[48];
            rnd.NextBytes(rndData);
            var seedValue = Interlocked.Add(ref seed, 1);
            var seedData = BitConverter.GetBytes(seedValue);
            var tokenData = rndData.Concat(seedData).OrderBy(_ => rnd.Next());

            return Convert.ToBase64String(tokenData.ToArray()).TrimEnd('=');
        }
    }
}