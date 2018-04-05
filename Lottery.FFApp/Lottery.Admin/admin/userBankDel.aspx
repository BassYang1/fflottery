<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userBankDel.aspx.cs" Inherits="Lottery.Admin.userBankDel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>删除会员银行</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        /*最后的表单验证*/
        function CheckFormSubmit() {
            Lottery.Loading.show("正在处理，请等待...");
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
    <table class="formtable">
        <tr>
            <th>
                会员账号
            </th>
            <td>
                <asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="300" CssClass="ipt" Enabled="false"></asp:TextBox>
                <asp:TextBox ID="txtId" runat="server" CssClass="ipt" Visible="False"
                    ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="txtUserId" runat="server" CssClass="ipt" Visible="False"
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                安全问题
            </th>
            <td>
                <asp:TextBox ID="txtQuestion" runat="server" MaxLength="50" Width="300" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                安全答案</th>
            <td>
                <asp:TextBox ID="txtAnswer" runat="server" MaxLength="50" Width="300" CssClass="ipt"></asp:TextBox>
                请输入安全答案，验证通过后才能删除！
            </td>
        </tr>
    </table>
    <div class="popup-actions">
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
        <asp:Button ID="btnSave" runat="server" Text="删除银行" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    </form>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
