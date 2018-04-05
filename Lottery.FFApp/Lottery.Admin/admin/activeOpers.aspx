<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activeOpers.aspx.cs" Inherits="Lottery.Admin.activeOpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>活动补发</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script src="/statics/admin/js/lotData.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            for (var obj in ActiveBFJsonData) {
                $('#loid').append('<Option value="' + ActiveBFJsonData[obj]["key"] + '">' + ActiveBFJsonData[obj]["value"] + '</Option>');
            }
        });

        function ConfirmCancel() {
            top.top.Lottery.Confirm("确定要补发该活动吗?", "getCurrentIframe().AutoActive()");
        }

        function AutoActive() {
            top.Lottery.Loading.show("正在努力派发，请稍后...");
            $.ajax({
                type: "post",
                dataType: "json",
                data: "flag=" + $('#loid').val() + "&clienttime=" + Math.random(),
                url: "ajaxActive.aspx?oper=ajaxAutoActive",
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    top.Lottery.Alert(d.returnval, "1");
                }
            });
        }
    </script>
</head>
<body>
    <div class="uc-box">
        <div class="box-hd">
            <h3 class="title">
                活动补发</h3>
            <a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a> <i class="icon-t icon-del">
            </i>
        </div>
        <div class="box-bd">
            <form action="" method="post" class="uc-form">
            <div class="form-group">
                <label class="lab">
                    选择活动：</label>
                <select class="sel" id="loid">
                </select>
                <div class="tips">
                    请选择活动.</div>
            </div>
             <div class="form-group">
                <label class="lab">
                    功能范围：</label>
                <div class="tips2">
                    只针对于系统自动派发，没有派发到的活动！</div>
            </div>
            <div class="btn-group">
                <div class="fl note">
                    请注意：所有活动都是自动派发，如果未派发，可以通过此功能来补发，补发不会重复派发！</div>
                <button type="button" class="btn btn-primary" onclick="ConfirmCancel();">
                    <i class="icon icon-del"></i>一键补发</button>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
