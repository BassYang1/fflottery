<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminPwd.aspx.cs" Inherits="Lottery.Admin.adminPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>修改密码</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onError: function (msg) { alert(msg); } });
            $("#txtOldPass").formValidator({ tipid: "tipOldPass", onshow: "请输入旧密码", onfocus: "请正确输入旧密码", defaultvalue: "" }).InputValidator({ min: 6, onerror: "旧密码不低于6位" });
            $("#txtNewPass1").formValidator({ tipid: "tipNewPass1", onshow: "请输入密码", onfocus: "密码6-14位" }).InputValidator({ min: 6, max: 14, onerror: "密码6-14位" });
            $("#txtNewPass2").formValidator({ tipid: "tipNewPass2", onshow: "请输入重复密码", onfocus: "密码必须一致" }).InputValidator({ min: 6, max: 14, onerror: "密码6-14位,请确认" }).CompareValidator({ desID: "txtNewPass1", operateor: "=", onerror: "两次密码不一致" });
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
                原始密码：</label>
            <asp:TextBox ID="txtOldPass" runat="server" Width="200px" MaxLength="50" CssClass="ipt"
                TextMode="Password"></asp:TextBox>
            <div class="tips">
                <span id="tipOldPass"></span>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                新密码：</label>
            <asp:TextBox ID="txtNewPass1" runat="server" Width="200px" MaxLength="50" CssClass="ipt"
                TextMode="Password"></asp:TextBox>
            <div class="tips">
                <span id="tipNewPass1"></span>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                确认密码：</label>
            <asp:TextBox ID="txtNewPass2" runat="server" Width="200px" MaxLength="50" CssClass="ipt"
                TextMode="Password"></asp:TextBox>
            <div class="tips">
                <span id="tipNewPass2"></span>
            </div>
        </div>
        <div class="popup-actions">
            <asp:Button ID="Button1" runat="server" Text="保存修改" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
