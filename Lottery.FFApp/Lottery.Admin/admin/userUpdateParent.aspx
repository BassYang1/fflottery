<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userUpdateParent.aspx.cs"
    Inherits="Lottery.Admin.userUpdateParent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>会员切线</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onError: function (msg) { alert(msg); } });
            $("#txtMoney").formValidator({ tipid: "tipMoney", onshow: "请输入充值金额", onfocus: "请输入充值金额" }).InputValidator({ min: 1, max: 10, onerror: "充值金额不能为空" });
        });
        /*最后的表单验证*/
        function CheckFormSubmit() {
            if ($.formValidator.PageIsValid('1')) {
                Lottery.Loading.show("正在处理，请等待...");
                return true;
            } else {
                return false;
            }
        }
    </script>
</head>
<body>
    <div class="popup-body">
        <form id="form1" runat="server" class="uc-form popup-form" onsubmit="return CheckFormSubmit()">
        <div class="form-group">
            <label class="lab">
                会员账号：</label>
            <asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="20" CssClass="ipt"
                Enabled="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="lab">
                会员返点：</label>
            <asp:TextBox ID="txtPoint" runat="server" Width="200px" MaxLength="20" CssClass="ipt"
                Enabled="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="lab">
                会员级别：</label>
            <asp:TextBox ID="txtGroup" runat="server" Width="200px" MaxLength="20" CssClass="ipt"
                Enabled="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="lab">
                迁入账号：</label>
            <asp:TextBox ID="txtToName" runat="server" Width="200px" MaxLength="20" CssClass="ipt"></asp:TextBox>
            <asp:TextBox ID="txtId" runat="server" MaxLength="20" CssClass="ipt" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtCode" runat="server" MaxLength="200" CssClass="ipt" Visible="false"></asp:TextBox>
        </div>
        <div class="popup-actions">
            <asp:Button ID="Button1" runat="server" Text="确认迁移" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
