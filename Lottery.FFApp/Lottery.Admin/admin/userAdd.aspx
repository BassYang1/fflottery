<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userAdd.aspx.cs" Inherits="Lottery.Admin.useradd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>添加会员</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onError: function (msg) { emAlert(msg); } });
            $("#txtAdminName").formValidator({ tipid: "tipAdminName", onshow: "请输入登录名", onfocus: "6-14个字符" }).InputValidator({ min: 6, max: 14, onerror: "6-14个字符" }).RegexValidator({ regexp: "username", datatype: "enum", onerror: "汉字或字母开头,不支持符号" })
		    .AjaxValidator({
		        type: "get",
		        data: "",
		        url: "ajaxUser.aspx?oper=ajaxCheckUserName&clienttime=" + Math.random(),
		        datatype: "json",
		        success: function (d) {
		            if (d.result == "1")
		                return true;
		            else
		                return false;
		        },
		        buttons: $("#btnSave"),
		        error: function () { emAlert('服务器繁忙，请稍后再试'); },
		        onerror: "该会员账号已经存在",
		        onwait: "正在校验登录名..."
		    });
            $("#txtAdminPass1").formValidator({ tipid: "tipAdminPass1", onshow: "留空默认123456", onfocus: "留空默认123456" });
            $("#txtAdminPass2").formValidator({ tipid: "tipAdminPass2", onshow: "请输入重复密码", onfocus: "密码必须一致" }).CompareValidator({ desID: "txtAdminPass1", operateor: "=", onerror: "两次密码不一致" });
            $("#txtAdminQuota").formValidator({ tipid: "tipAdminQuota", onshow: "请输入默认配额数量", onfocus: "请输入默认配额数量" }).InputValidator({ min: 1, max: 4, onerror: "1-4个字符" });
            $("#txtAdminQuota2").formValidator({ tipid: "tipAdminQuota2", onshow: "请输入默认平级配额", onfocus: "请输入默认平级配额" }).InputValidator({ min: 1, max: 4, onerror: "1-4个字符" });
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
                用户类型：</label>
            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="sel">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label class="lab">
                返点设置：</label>
            <asp:DropDownList ID="ddlPoint" runat="server" CssClass="sel">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label class="lab">
                会员账号：</label>
            <asp:TextBox ID="txtAdminName" runat="server" MaxLength="20" EnableViewState="false"
                CssClass="ipt"></asp:TextBox>
            <div class="tips">
                <span id="tipAdminName"></span>
            </div>
            <asp:TextBox ID="txtId" runat="server" MaxLength="20" CssClass="ipt" Visible="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="lab">
                密码：</label>
            <asp:TextBox ID="txtAdminPass1" runat="server" TextMode="Password" EnableViewState="false"
                MaxLength="20" CssClass="ipt"></asp:TextBox>
            <div class="tips">
                <span id="tipAdminPass1"></span>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                确认密码：</label>
            <asp:TextBox ID="txtAdminPass2" runat="server" TextMode="Password" EnableViewState="false"
                MaxLength="20" CssClass="ipt"></asp:TextBox>
            <div class="tips">
                <span id="tipAdminPass2"></span>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                默认配额数量：</label>
            <asp:TextBox ID="txtAdminQuota" runat="server" MaxLength="20" CssClass="ipt" onkeyup="chkPrice(this)">100</asp:TextBox>
            <div class="tips">
                <span id="tipAdminQuota"></span>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                默认平级配额：</label>
            <asp:TextBox ID="txtAdminQuota2" runat="server" MaxLength="20" CssClass="ipt" onkeyup="chkPrice(this)">0</asp:TextBox>
            <div class="tips">
                <span id="tipAdminQuota2"></span>
            </div>
        </div>
        <div class="popup-actions">
            <asp:Button ID="Button1" runat="server" Text="确认添加" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
