<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userQuotasEdit.aspx.cs"
    Inherits="Lottery.Admin.userQuotasEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>账号回收审核</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
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
                审核选项
            </th>
            <td>
                <div class="checkboxDiv">
                    <asp:RadioButton ID="rbo1" runat="server" GroupName="a1" Checked="True" />同意回收
                    <asp:RadioButton ID="rbo2" runat="server" GroupName="a1" />拒绝回收</div>
                <asp:TextBox ID="txtId" runat="server" Width="120px" MaxLength="10" CssClass="ipt"
                    Visible="false"></asp:TextBox>
            </td>
        </tr>
          <tr>
            <th>
                直属下级转移
            </th>
            <td>
                <div class="checkboxDiv">
                    <asp:RadioButton ID="rbo3" runat="server" GroupName="a2" Checked="True" />转移到直属上级
                    <asp:RadioButton ID="rbo4" runat="server" GroupName="a2" />转为平台一级代理</div>
            </td>
        </tr>
         <tr>
            <th>
                资金处理
            </th>
            <td>
                <div class="checkboxDiv">
                    <asp:RadioButton ID="rbo5" runat="server" GroupName="a3" Checked="True" />转给上级
                    <asp:RadioButton ID="rbo6" runat="server" GroupName="a3" />不考虑资金</div>
            </td>
        </tr>
         <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" OnClick="btnSave_Click" />
                <input id="btnReset" type="button" value="取消" class="btnsubmit" onclick="parent.Lottery.Popup.hide();" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
