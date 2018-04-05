<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="login.aspx.cs" Inherits="Lottery.Admin.login" %>

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
        function CheckBrowser() {
            var app = navigator.appName;
            var verStr = navigator.appVersion;
            if (app.indexOf('Netscape') != -1) {
                alert("友情提示:\n    你使用的是Netscape浏览器，可能会导致无法使用后台的部分功能。建议您使用 IE6.0 或以上版本。");
            }
            else if (app.indexOf('Microsoft') != -1) {
                if (verStr.indexOf("MSIE 3.0") != -1 || verStr.indexOf("MSIE 4.0") != -1 || verStr.indexOf("MSIE 5.0") != -1 || verStr.indexOf("MSIE 5.1") != -1)
                    alert("友情提示:\n    您的浏览器版本太低，可能会导致无法使用后台的部分功能。建议您使用 IE6.0 或以上版本。");
            }
        }
        function ajaxChkLogin(uName, uPass, cookieday) {
            $.ajax({
                type: "post",
                dataType: "html",
                url: "ajax.aspx?oper=login&clienttime=" + Math.random(),
                data: "name=" + uName + "&pass=" + encodeURIComponent(uPass) + "&type=" + cookieday,
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    if (d == "ok")
                        top.location.href = 'default.aspx';
                    else
                        alert(d);
                }
            });
        }

        function sendRequest(p) {
            if (p == 0) {
                return;
            }
            $.ajax({
                url: "ajax.aspx?clienttime=" + Math.random(),
                type: "get",
                dataType: "json",
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    if (d.result == "1")
                        top.location.href = 'default.aspx';
                }
            });
        }
//-->
    </script>
</head>
<body scroll="no" id="body">
    <div id="swfDiv">
    </div>
    <script type="text/javascript">
        //CheckBrowser();
        sendRequest(1);

        var flashvars = {};
        flashvars.Copyright = 'Powered By Lottery';
        flashvars.siteid = "1";
        var params = {};
        params.quality = "high";
        params.bgcolor = "#869ca7";
        params.allowScriptAccess = "sameDomain";
        params.allowfullscreen = "true";
        var attributes = {};
        attributes.id = "LoginFlexApp";
        attributes.name = "LoginFlexApp";
        swfobject.embedSWF(site.Dir + "statics/flex3/admin/AdminLogin.swf", "swfDiv", "100%", "100%", "9.0.0", site.Dir + "statics/flex3/expressInstall.swf", flashvars, params, attributes); 
    </script>
</body>
</html>
