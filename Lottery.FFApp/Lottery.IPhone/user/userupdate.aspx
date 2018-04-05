<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userupdate.aspx.cs" Inherits="Lottery.IPhone.user.userupdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>返点设置</title>
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.min.js"></script>
    <script type="text/javascript" src="/statics/json/LotAndSmalldata.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    <link rel="stylesheet" type="text/css" href="/statics/themes/default/css/global.css" />
    <link href="/statics/themes/default/css/secondpop.css" rel="stylesheet" type="text/css" />
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
    <table class="inputtable2 mrg10T">
        <tr>
            <th>
                会员账号：
            </th>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" CssClass="input-2" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="txtUserId" runat="server" MaxLength="20" ReadOnly="True" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                返点设置：
            </th>
            <td>
                <asp:DropDownList ID="ddlPoint" runat="server" CssClass="select-2">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="Button1" runat="server" Text="确定" CssClass="btn-2" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
