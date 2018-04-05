<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userQuotaadd.aspx.cs" Inherits="Lottery.Admin.userQuotaadd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>生成配额</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ajaxCreate() {
            if ($("#userId").val() == "") {
                $('#tip').html('请输入会员Id！');
                return false;
            }
            if ($("#num").val() == "") {
                $('#tip').html('请输入配额数量！');
                return false;
            }
            $.ajax({
                type: "post",
                dataType: "json",
                data: "",
                url: "/admin/ajaxUserlevel.aspx?oper=ajaxCreate&clienttime=" + Math.random() + "&userId=" + $('#userId').val() + "&num=" + $('#num').val(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    try {
                        parent.ajaxList(parent.page);
                    }
                    catch (e) {
                        top.getCurrentIframe().ajaxList(top.getCurrentIframe().page);
                    }
                    top.Lottery.Alert(d.returnval, "1");
                }
            });
        }

        function ajaxCreateAll() {
            if ($("#num2").val() == "") {
                $('#tip2').html('请输入配额数量！');
                return false;
            }
            Lottery.Loading.show("正在处理，请等待...");
            $.ajax({
                type: "post",
                dataType: "json",
                data: "",
                url: "/admin/ajaxUserlevel.aspx?oper=ajaxCreateAll&clienttime=" + Math.random() + "&num2=" + $('#num2').val(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    try {
                        parent.ajaxList(parent.page);
                    }
                    catch (e) {
                        top.getCurrentIframe().ajaxList(top.getCurrentIframe().page);
                    }
                    top.Lottery.Alert(d.returnval, "1");
                }
            });
        }
    </script>
</head>
<body>
    <div class="popup-body">
        <form id="form1" class="uc-form popup-form">
        <div class="form-group">
            <label class="lab">
                会员Id：</label>
            <input class="ipt" type="text" id="userId" onkeyup="chkPrice(this)" />
        </div>
        <div class="form-group">
            <label class="lab">
                配额数量：</label>
            <input class="ipt" type="text" id="num" onkeyup="chkPrice(this)" value="10" />
        </div>
        <div class="popup-actions">
            <span id="tip"></span>
            <input type="button" value="指定会员生成" class="btn btn-primary" onclick="ajaxCreate()" />
        </div>
        <br />
        <div class="form-group">
            <label class="lab">
                配额数量：</label>
            <input class="ipt" type="text" id="num2" onkeyup="chkPrice(this)" value="10" />
        </div>
        <div class="popup-actions">
            <span id="tip2"></span>
            <input type="submit" value="全部生成" class="btn btn-primary" onclick="ajaxCreateAll()" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
