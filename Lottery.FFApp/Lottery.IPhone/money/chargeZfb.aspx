<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chargeZfb.aspx.cs" Inherits="Lottery.Web.money.chargeZfb" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>立博国际娱乐</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=no">
    <meta name="format-detection" content="telephone=no,email=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/global.css" />
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/style.css" />
    <link href="/statics/sytle/css/flipclock.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script src="/statics/formValidator.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#info").hide();
            $("#add").show();
            _jcms_GetRefreshCode('imgCode');
            $i("mincharge").innerHTML = site.MinCharge;
            $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
                emAlert(msg);
            },
                onsuccess: function () { return true; }
            });
            $("#txtPayName").formValidator({ tipid: "tipPayName", onshow: "请输入支付宝姓名", onfocus: "请输入支付宝姓名", defaultvalue: "" }).InputValidator({ min: 1, max: 10, onerror: "请输入支付宝姓名" });
            $("#txtPayMoney").formValidator({ tipid: "tipPayMoney", onshow: "请输入充值金额", onfocus: "请输入充值金额", defaultvalue: "" }).InputValidator({ min: 1, max: 10, onerror: "请输入充值金额" }).RegexValidator({ regexp: "^([1-9]{1}[0-9]{0,8})$", onerror: "请输入整数" });
            $("#txtUserCode").formValidator({ tipid: "tipUserCode", onshow: "请输入验证码", onfocus: "验证码必须填写" }).InputValidator({ min: 4, max: 4, onerror: "验证码必须为4位" });
            //金额大写
            $("#txtPayMoney").delegate('', 'keyup', function (event) {
                chkPrice(this);
                $i('moneyUpper').innerHTML = atoc($('#txtPayMoney').val());
            });
            ajaxList();
        });

        function ajaxList() {
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "clienttime=" + Math.random(),
                url: "/ajax/ajaxBank.aspx?oper=ajaxGetChargeSetList&id=1007",
                error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                success: function (d) {
                    switch (d.result) {
                        case '-1':
                            top.window.location = '/login.html';
                            break;
                        case '1':
                            if (d.recordcount > 0) {
                                if (d.table[0].isused == "0") {
                                    $i('time1').innerHTML = d.table[0].starttime;
                                    $i('time2').innerHTML = d.table[0].endtime;
                                    $i('mincharge').innerHTML = d.table[0].mincharge;
                                    $i('maxcharge').innerHTML = d.table[0].maxcharge;
                                    $i('lblPayName').innerHTML = d.table[0].merkey;
                                    $i('lblPayAccount').innerHTML = d.table[0].mercode;
                                }
                                else {
                                    $("#add").html("支付宝充值已关闭，请等待开放！");
                                    $("#form1").hide();
                                }
                            }
                            break;
                    }
                    closeload(index);
                }
            });
        }

        function chkPost() {
            var uName = $('#txtPayName').val();
            var uMoney = $("#txtPayMoney").val();
            var uCode = $("#txtUserCode").val();
            if ($.formValidator.PageIsValid('1')) {
                if (eval(uMoney) < eval($i("mincharge").innerHTML)) {
                    emAlert('充值金额不能小于最小充值金额');
                    return;
                }
                else if (eval(uMoney) > eval($i("maxcharge").innerHTML)) {
                    emAlert('充值金额不能大于最大充值金额');
                    return;
                }
                else {
                    var index = emLoading();
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        data: "name=" + encodeURIComponent(uName) + "&money=" + encodeURIComponent(uMoney) + "&code=" + encodeURIComponent(uCode),
                        url: "/ajax/ajaxMoney.aspx?oper=ajaxCharge&clienttime=" + Math.random(),
                        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                        success: function (d) {
                            if (d.result == "1") {
                                $("#add").hide();
                                $("#info").show();
                                $i('lblPayMoney').innerHTML = parseFloat($("#txtPayMoney").val()).toFixed(2);
                                $i('lblPayMoney2').innerHTML = parseFloat($("#txtPayMoney").val()).toFixed(2);
                                $i('lblRemark').innerHTML = d.returnval;
                            }
                            else {
                                $("#info").hide();
                                $("#add").show();
                                emAlert(d.returnval);
                                $("#txtPayMoney").val("");
                                $("#txtUserCode").val("");
                                ajaxList();
                                _jcms_GetRefreshCode('imgCode', 28);
                            }
                            closeload(index);
                        }
                    });
                }
            }
        }

        function chkLoginZfb() {
            var url = "https://auth.alipay.com/login/index.htm?needTransfer=true&goto=http://mobile.alipay.com/index.htm";
            openwin(url);
        }

        function goTo(url) {
            var targetWndName = "MyWindow";
            var wnd = window.open("", targetWndName);
            var link = document.getElementById("link");
            link.target = targetWndName;
            link.href = url;
            link.click();
        }

        function openwin(url) {
            var a = document.createElement("a");
            a.setAttribute("href", url);
            a.setAttribute("target", "_blank");
            a.setAttribute("id", "openwin");
            document.body.appendChild(a);
            a.click();
        }
    </script>
</head>
<body>
    <div class="container">
        <header id="header">
        <h1 class="title">充&nbsp;&nbsp;&nbsp;值</h1>
        <a href="javascript:history.go(-1);" class="back"></a>
    </header>
        <main id="main">
        <div id="add" class="user-recharge">
        	<div class="account-balance">
            	<i class="icon-money"></i>
                当前余额
                <strong id="money" class="balance">0.00</strong>
                <a href="#" class="refresh"></a>
            </div>
            <form action="" method="post" class="lt-form recharge-form">
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">充值方式</label>
                    </div>
                    <div class="item-content">
                        <div class="item-content-text">支付宝</div>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">支付宝姓名</label>
                    </div>
                    <div class="item-content">
                       <input id="txtPayName" type="text" value="" class="ipt" placeholder="请输入支付宝姓名" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">充值金额</label>
                    </div>
                    <div class="item-content">
                       <input id="txtPayMoney" type="text" value="" class="ipt" onkeypress="chkPrice(this)" placeholder="请输入充值金额" />
                    </div>
                    <div class="item-extra">
                        元
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">金额大写</label>
                    </div>
                    <div class="item-content">
                       <span id="moneyUpper"></span>
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">验证码</label>
                    </div>
                    <div class="item-content">
                        <input id="txtUserCode" type="text" class="ipt" maxlength="4" size="4"  placeholder="请输入验证码" />
                    
                    </div>
                    <img id="imgCode" onclick="_jcms_GetRefreshCode('imgCode');" src="" alt="点击更换" class="code" />
                </div>
                <div class="form-btns">
                     <input type="button" value="下一步" class="btn primary-btn" onclick="chkPost();" />
                    <a id="link" href="javascript:void(0)" style="visibility: hidden; position: absolute;">
                    </a>
                </div>
                 <div class="form-btns">
                开放时间： 每天<font id="time1" style="color: Red;"></font>至<font id="time2" style="color: Red;"></font><br />
                充值限额： 最低<font id="mincharge" style="color: Red;"></font>元，最高<font id="maxcharge" style="color: Red;"></font>元！ 充值不到账请联系客服进行处理！
                </div>
            </form>
        </div>

        <div id="info" class="recharge-apply" style="display:none;">
        	<div class="order-info">
            	<div class="order-money"><i class="icon-money"></i>订单金额<em id="lblPayMoney2" class="money">0.00</em></div>
                <div class="order-memo">
                	<i class="icon-info"></i>
                    <p class="memo">充值附言：<em id="lblRemark"></em></p>
                    <p class="tip">附言区分大小写，请正确输入</p>
                </div>
            </div>
            <div class="account-info">
            	<p><span class="k">充值金额：</span><span class="v" id="lblPayMoney"></span></p>
                <p><span class="k">收款户名：</span><span class="v" id="lblPayName"></span></p>
                <p><span class="k">收款帐号：</span><span class="v" id="lblPayAccount"></span></p>
                <p><span class="k">开户银行：</span><span class="v">招商银行</span></p>
            </div>
            <div class="recharge-countdown">
				<div class="J_RechargeCountdown"></div>
            </div>
            <div class="recharge-tips">
                <p class="p1">请在30分钟内完成汇款，逾期订单将失效<br/>您需重新发起充值申请</p>
                <p class="p2">请继续在网银客户端或银行柜台完成后续操作</p>
            </div>
            <div class="recharge-bottom">
               <input type="button" value="登录支付宝付款" class="btn primary-btn" onclick="chkLoginZfb();" />
            </div>
        </div>
    </main>
    </div>
    <script src="/statics/sytle/js/flipclock.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".J_RechargeCountdown").FlipClock(1800, { clockFace: 'MinuteCounter', countdown: true, callbacks: {
                stop: function () {
                    //倒计时完成后调用
                }
            }
            });
        });
    </script>

</body>
</html>
