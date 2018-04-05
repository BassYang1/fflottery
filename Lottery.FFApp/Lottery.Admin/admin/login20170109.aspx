<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="login20170109.aspx.cs" Inherits="Lottery.Admin.login20170109" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>EM-CLUB后台管理系统</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script src="/statics/admin/js/admin.js" type="text/javascript"></script>
    <script type="text/javascript">        if (top != self) top.location = self.location;</script>
    <link rel="stylesheet" type="text/css" href="/statics/admin/css/style.css" />
    <script type="text/javascript">
<!--
        function ajaxChkLogin() {
            var uName = $("#txtUserName").val();
            var oPass = $('#txtUserPass').val();
            $.ajax({
                type: "post",
                dataType: "html",
                url: "ajax.aspx?oper=login&clienttime=" + Math.random(),
                data: "name=" + uName + "&pass=" + encodeURIComponent(oPass) + "&type=7",
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    if (d == "ok")
                        top.location.href = 'default.aspx';
                    else
                        alert(d);
                }
            });
        }
    </script>
</head>
<body scroll="no" id="body">
  <input id="txtUserName" type="text" value="" class="ipt" placeholder="账号" />
                <input id="txtUserPass" type="password" value="" class="ipt" placeholder="密码" />
                <input type="button" value="登录" onclick="ajaxChkLogin()" class="btn btn-login" />
</body>
</html>
