using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottery.FFApp
{
    public partial class TestSignkey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Text = 4.0.ToString("F4");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var md5Key = MD5Cryptology.GetMD5(string.Format("{0}&{1}&{2}&{3}", txtMerch.Text, txtUserName.Text, txtTime.Text, txtKey.Text).ToLower(), "gb2312");
            Label1.Text = md5Key;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var signKey = MD5Cryptology.GetMD5(string.Format("{0}&{1}&{2}&{3}&{4}", txtOrderNo2.Text, txtMerch2.Text,txtUserName2.Text,txtAmount2.Text, txtKey2.Text).ToLower(), "gb2312");
            Label2.Text = signKey;
        }
    }
}