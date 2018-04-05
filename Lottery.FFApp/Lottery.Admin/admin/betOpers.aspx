<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="betOpers.aspx.cs" Inherits="Lottery.Admin.betOpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>批量撤单操作</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script src="/statics/admin/js/lotData.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            for (var obj in Lottery2JsonData) {
                $('#loid').append('<Option value="' + Lottery2JsonData[obj]["key"] + '">' + Lottery2JsonData[obj]["value"] + '</Option>');
                $('#loid2').append('<Option value="' + Lottery2JsonData[obj]["key"] + '">' + Lottery2JsonData[obj]["value"] + '</Option>');
                $('#loid3').append('<Option value="' + Lottery2JsonData[obj]["key"] + '">' + Lottery2JsonData[obj]["value"] + '</Option>');
            }
        });

        function ConfirmCancel() {
            if ($("#Issue").val() == "") {
                top.Lottery.Alert("请输入期号", "0");
                return false;
            }
            top.top.Lottery.Confirm("确定要整期撤单吗?", "getCurrentIframe().ajaxCancel(1)");
        }

        function ConfirmCancel2() {
            if ($("#Issue2").val() == "") {
                top.Lottery.Alert("请输入期号", "0");
                return false;
            }
            top.top.Lottery.Confirm("确定要整期撤单吗?", "getCurrentIframe().ajaxCancel(2)");
        }

        function ConfirmCancel3() {
            if ($("#Issue3").val() == "") {
                top.Lottery.Alert("请输入期号", "0");
                return false;
            }
            top.top.Lottery.Confirm("确定要整期撤回到未开奖吗?", "getCurrentIframe().ajaxCancel(3)");
        }

        function ajaxCancel(flag) {
            var u = "";
            if (flag == "1")
                u = "loid=" + $('#loid').val() + "&Issue=" + $('#Issue').val() + "&flag=1";
            if (flag == "2")
                u = "loid=" + $('#loid2').val() + "&Issue=" + $('#Issue2').val() + "&flag=2";
            if (flag == "3")
                u = "loid=" + $('#loid3').val() + "&Issue=" + $('#Issue3').val() + "&flag=3";
            top.Lottery.Loading.show("正在努力撤单，请稍后...");
            $.ajax({
                type: "post",
                dataType: "json",
                data: u,
                url: "/admin/ajaxBet.aspx?oper=ajaxBetOpers&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    top.Lottery.Alert(d.returnval, d.result);
                }
            });
        }
    </script>
</head>
<body>
    <div class="uc-box">
        <div class="box-hd">
            <h3 class="title">
                未开奖整期撤单</h3>
            <a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a> <i class="icon-t icon-del">
            </i>
        </div>
        <div class="box-bd">
            <form action="" method="post" class="uc-form">
            <div class="form-group">
                <label class="lab">
                    选择游戏：</label>
                <select class="sel" id="loid">
                </select>
                <div class="tips">
                    请选择游戏.</div>
            </div>
            <div class="form-group">
                <label class="lab">
                    撤单期号：</label>
                <input id="Issue" type="text" value="" class="ipt" />
                <div class="tips">
                    请输入撤单期号，请注意各游戏期号格式.</div>
            </div>
            <div class="btn-group">
                <div class="fl note">
                    请注意：所有撤单操作均不可恢复，撤单需谨慎！</div>
                <button type="button" class="btn btn-primary" onclick="ConfirmCancel();">
                    <i class="icon icon-del"></i>一键撤单</button>
            </div>
            </form>
        </div>
    </div>
    <div class="uc-box">
        <div class="box-hd">
            <h3 class="title">
                已开奖整期撤单</h3>
            <a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a> <i class="icon-t icon-del">
            </i>
        </div>
        <div class="box-bd">
            <form action="" method="post" class="uc-form">
            <div class="form-group">
                <label class="lab">
                    选择游戏：</label>
                <select class="sel" id="loid2">
                </select>
                <div class="tips">
                    请选择游戏.</div>
            </div>
            <div class="form-group">
                <label class="lab">
                    撤单期号：</label>
                <input id="Issue2" type="text" value="" class="ipt" />
                <div class="tips">
                    请输入撤单期号，请注意各游戏期号格式.</div>
            </div>
            <div class="btn-group">
                <div class="fl note">
                    请注意：所有撤单操作均不可恢复，撤单需谨慎！</div>
                <button type="button" class="btn btn-primary" onclick="ConfirmCancel2();">
                    <i class="icon icon-del"></i>一键撤单</button>
            </div>
            </form>
        </div>
    </div>
    <div class="uc-box">
        <div class="box-hd">
            <h3 class="title">
                已开奖整期撤单到未开奖</h3>
            <a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a> <i class="icon-t icon-del">
            </i>
        </div>
        <div class="box-bd">
            <form action="" method="post" class="uc-form">
            <div class="form-group">
                <label class="lab">
                    选择游戏：</label>
                <select class="sel" id="loid3">
                </select>
                <div class="tips">
                    请选择游戏.</div>
            </div>
            <div class="form-group">
                <label class="lab">
                    撤单期号：</label>
                <input id="Issue3" type="text" value="" class="ipt" />
                <div class="tips">
                    请输入撤单期号，请注意各游戏期号格式.</div>
            </div>
            <div class="btn-group">
                <div class="fl note">
                    请注意：所有撤单操作均不可恢复，撤单需谨慎！</div>
                <button type="button" class="btn btn-primary" onclick="ConfirmCancel3();">
                    <i class="icon icon-del"></i>一键撤单</button>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
