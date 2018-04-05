<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminLock.aspx.cs" Inherits="Lottery.Admin.adminLock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>请输入登陆密码解锁</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setCookie("lock", "lock");
            $.formValidator.initConfig({ onError: function (msg) { } });
            $("#txtOldPass").formValidator({ tipid: "tipOldPass", onshow: "请输入登陆密码", onfocus: "请输入登陆密码", defaultvalue: "" }).InputValidator({ min: 6, onerror: "密码不低于6位" });
        });
        function ajaxPost() {
            if ($.formValidator.PageIsValid('1')) {
                var d = "&p=" + $('#txtOldPass').val();
                $.ajax({
                    type: "post",
                    dataType: "json",
                    data: d,
                    url: "/admin/ajaxAdmin.aspx?oper=ajaxUnLock&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                    success: function (d) {
                        if (d.result == "1") {
                            delCookie("lock");
                            top.Lottery.Alert(d.returnval, "1");
                        }
                        else {
                            $('#error').html(d.returnval);
                        }
                    }
                });
            }
        }
    </script>
</head>
<body>
    <div class="popup-body">
        <form id="form1" runat="server" class="uc-form popup-form">
        <div class="form-group">
            <label class="lab">
                登陆密码：</label>
            <asp:TextBox ID="txtOldPass" runat="server" Width="100px" MaxLength="20" CssClass="ipt"
                TextMode="Password"></asp:TextBox>
            <div class="tips">
                <span id="tipOldPass"></span>
            </div>
        </div>
        <div class="popup-actions">
            <span id="error" style="color:Red;"></span>
            <button type="button" class="btn btn-primary" onclick="ajaxPost();">
                确认解锁</button>
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
