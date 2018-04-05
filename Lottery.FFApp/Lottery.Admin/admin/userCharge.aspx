<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userCharge.aspx.cs" Inherits="Lottery.Admin.usercharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>资金操作</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onError: function (msg) { alert(msg); } });
            $("#txtMoney").formValidator({ tipid: "tipMoney", onshow: "请输入存款", onfocus: "请输入存款" }).InputValidator({ min: 1, max: 10, onerror: "存款不能为空" });
            $("#txtPwd").formValidator({ tipid: "tipPwd", onshow: "请输入管理员密码", onfocus: "请输入管理员密码" }).InputValidator({ min: 1, max: 20, onerror: "管理员密码不能为空" });
            $("#txtRemark").formValidator({ tipid: "tipRemark", onshow: "请输入操作备注", onfocus: "请输入操作备注" }).InputValidator({ min: 1, max: 500, onerror: "操作备注不能为空" });
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
                充值类型：</label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="sel">
                <asp:ListItem Value="1">存款</asp:ListItem>
                <asp:ListItem Value="2">取款</asp:ListItem>
                <asp:ListItem Value="3">投注</asp:ListItem>
                <asp:ListItem Value="5">派奖</asp:ListItem>
                <asp:ListItem Value="4">返点</asp:ListItem>
                <asp:ListItem Value="9">活动</asp:ListItem>
                <asp:ListItem Value="6">撤单</asp:ListItem>
                <asp:ListItem Value="12">分红</asp:ListItem>
                <asp:ListItem Value="10">其他</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label class="lab">
                会员账号：</label>
            <asp:TextBox ID="txtName" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            <asp:TextBox ID="txtId" runat="server" MaxLength="20" CssClass="ipt" Visible="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="lab">
                操作金额：</label>
            <asp:TextBox ID="txtMoney" runat="server" MaxLength="10" CssClass="ipt">100</asp:TextBox>
            <div class="tips">
                <span id="tipMoney"></span>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                管理员密码：</label>
            <asp:TextBox ID="txtPwd" runat="server" MaxLength="20" TextMode="Password" CssClass="ipt"></asp:TextBox>
            <div class="tips">
                <span id="tipPwd"></span>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                操作备注：</label>
            <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" CssClass="ipt"></asp:TextBox>
            <div class="tips">
                <span id="tipRemark"></span>
            </div>
        </div>
        <div class="popup-actions">
            <asp:Button ID="Button1" runat="server" Text="确认操作" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
