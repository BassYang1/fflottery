<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sysDelData.aspx.cs" Inherits="Lottery.Admin.sysDelData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>数据清理</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/_libs/My97DatePicker/WdatePicker.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $i('d1').value = GetDateStr(-7);
            $i('d2').value = GetDateStr(-1);
        });
        function ConfirmDel() {
            top.top.Lottery.Confirm("确定要删除此记录吗?", "getCurrentIframe().ajaxDel()");
        }
        function ajaxDel() {
            var u = "";
            u = "d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&flag=" + $('#flag').val();
            top.Lottery.Loading.show("正在努力删除，请稍后...");
            $.ajax({
                type: "post",
                dataType: "json",
                data: u,
                url: "ajaxsysDelData.aspx?oper=ajaxDel&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    if (d.result == "1") {
                        top.Lottery.Message(d.returnval, "1");
                        ajaxList(page);
                    }
                }
            });
        }
    </script>
</head>
<body>
    <div class="uc-box">
        <div class="box-hd">
            <h3 class="title">
                数据删除工具</h3>
            <a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a> <i class="icon-t icon-del">
            </i>
        </div>
        <div class="box-bd">
            <form action="" method="post" class="uc-form">
            <div class="form-group">
                <label class="lab">
                    删除选项：</label>
                <select class="sel" id="flag">
                    <option value="1">投注记录</option>
                    <option value="2">取款记录</option>
                    <option value="3">帐变记录</option>
                    <option value="4">统计数据</option>
                    <option value="5">开奖数据</option>
                    <option value="6">登录日志</option>
                    <option value="7">系统日志</option>
                    <option value="8">追号记录</option>
                </select>
                <div class="tips">
                    选择一个要被删除数据的表名.</div>
            </div>
            <div class="form-group">
                <label class="lab">
                    开始时间：</label>
                <input id="d1" type="text" value="" class="ipt"  onclick="WdatePicker({el:'d1'})"/>
                <div class="tips">
                    从这个时间开始，这个时间一般是指数据产生的时间.</div>
            </div>
            <div class="form-group">
                <label class="lab">
                    结束时间：</label>
                <input id="d2" type="text" value="" class="ipt"  onclick="WdatePicker({el:'d2'})"/>
                <div class="tips">
                    从这个时间结束，这个时间一般是指数据产生的时间.</div>
            </div>
            <div class="btn-group">
                <div class="fl note">
                    请注意：所有删除操作均不可恢复，删除需谨慎！</div>
                <button type="button" class="btn btn-primary" onclick="ConfirmDel();" >
                    <i class="icon icon-del"></i>一键删除</button>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
