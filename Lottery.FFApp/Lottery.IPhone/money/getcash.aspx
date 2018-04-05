<%@ Page Language="C#" CodeBehind="getcash.aspx.cs" Inherits="Lottery.IPhone.money.getcash" %>

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
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script src="/statics/formValidator.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            _jcms_GetRefreshCode('imgCode', 28);
            ajaxList();
            $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
                emAlert(msg);
            },
                onsuccess: function () { return true; }
            });
            $("#txtPayMoney").formValidator({ tipid: "tipPayMoney", onshow: "请输入提现金额  ", onfocus: "请输入提现金额", defaultvalue: "" }).InputValidator({ min: 2, onerror: "提现金额不能少于100元" }).RegexValidator({ regexp: "^([1-9]{1}[0-9]{0,4})$", onerror: "提现金额请输入整数" });
            $("#txtNewPass1").formValidator({ tipid: "tipNewPass1", onshow: "请输入资金密码  ", onfocus: "资金密码6-14位" }).InputValidator({ min: 6, max: 14, onerror: "资金密码为6-14位" });
            $("#txtUserCode").formValidator({ tipid: "tipUserCode", onshow: "请输入验证码", onfocus: "验证码必须填写" }).InputValidator({ min: 4, max: 4, onerror: "验证码4位验证码" });
            //金额大写
            $("#txtPayMoney").delegate('', 'keyup', function (event) {
                chkPrice(this);
                $i('moneyUpper').innerHTML = atoc($('#txtPayMoney').val());
            });
        });

        function ajaxList() {
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=1&pagesize=1&clienttime=" + Math.random(),
                url: "/ajax/ajaxBank.aspx?oper=ajaxGetList",
                error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                success: function (d) {
                    switch (d.result) {
                        case '-1':
                            top.window.location = '/login.html';
                            break;
                        case '1':
                            if (d.recordcount > 0) {
                                $i('lblBank').innerHTML = d.table[0].paybank;
                                $i('lblPayAccount').innerHTML = d.table[0].payaccount.substr(0, 4) + "***" + d.table[0].payaccount.substr(d.table[0].payaccount.length - 4, 4);
                                $i('lblPayName').innerHTML = d.table[0].payname.substr(0, 1) + "***";
                                $i('lblAddress').innerHTML = d.table[0].paybankaddress;

                                $i("PriceTime1").innerHTML = d.table[0].starttime;
                                $i("PriceTime2").innerHTML = d.table[0].endtime;
                                $i("PriceNum").innerHTML = d.table[0].maxgetcash;
                                $i("PriceOut").innerHTML = d.table[0].mincharge;
                                $i("PriceOut2").innerHTML = d.table[0].maxcharge;

                                $i("money").innerHTML = parseFloat(d.table[0].money).toFixed(2);
                                $i("txcs").innerHTML = d.table[0].txcs;
                                $i("txje").innerHTML = d.table[0].txje;

                                $i("lblBankId").innerHTML = d.table[0].paymethod;
                                $i("lblUserBankId").innerHTML = d.table[0].id;

                            }
                            else {
                                window.location.href = '/center/bankInfo.aspx';
                            }
                            break;
                    }
                    closeload(index);
                }
            });
        }

        function chkPost() {
            var uMoney = $("#txtPayMoney").val();
            var uCode = $("#txtUserCode").val();
            var uPass = $('#txtNewPass1').val();
            if ($.formValidator.PageIsValid('1')) {
                if (eval(uMoney) < eval($i("PriceOut").innerHTML)) {
                    emAlert('提现金额不能小于单笔最小金额');
                    return;
                }
                else if (eval(uMoney) > eval($i("PriceOut2").innerHTML)) {
                    emAlert('提现金额不能大于单笔最大金额');
                    return;
                }
                else if (eval(uMoney) > eval($i("money").innerHTML)) {
                    emAlert('您的可用余额不足');
                    return;
                }
                else if (eval($i("txcs").innerHTML) >= eval($i("PriceNum").innerHTML)) {
                    emAlert('今日提现已得到最大提现次数');
                    return;
                }
                else {
                    var index = emLoading();
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        data: "money=" + encodeURIComponent(uMoney) + "&pass=" + encodeURIComponent(uPass) + "&code=" + encodeURIComponent(uCode) + "&bankId=" + $i("lblBankId").innerHTML + "&userBankId=" + $i("lblUserBankId").innerHTML,
                        url: "/ajax/ajaxMoney.aspx?oper=ajaxCash&clienttime=" + Math.random(),
                        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                        success: function (d) {
                            emAlert(d.returnval);
                            $("#txtPayMoney").val("");
                            $("#txtNewPass1").val("");
                            $("#txtUserCode").val("");
                            $i('moneyUpper').innerHTML = "";
                            ajaxList();
                            closeload(index);
                            _jcms_GetRefreshCode('imgCode', 28);
                        }
                    });
                }
            }
        }
    </script>
</head>
<body>
    <div class="container">
        <header id="header">
        <h1 class="title">提&nbsp;&nbsp;&nbsp;现</h1>
        <a href="javascript:history.go(-1);" class="back"></a>
    </header>
        <main id="main">
        <div class="user-withdrawal">
        	<div class="account-balance">
            	<i class="icon-money"></i>
                当前余额
                <strong id="money" class="balance">0.00</strong>
                <a href="#" class="refresh"></a>
            </div>
            <form action="" method="post" class="lt-form withdrawal-form">
            	<div class="withdrawal-info">
                	<h3><i class="icon-info"></i>提款信息</h3>
                    <p>
                    	<span class="k">绑定银行：</span>
                        <span class="v" id="lblBank">&nbsp;</span><span id="lblBankId" style="display:none;"></span><span id="lblUserBankId" style="display:none;"></span>
                    </p>
                    <p>
                    	<span class="k">绑定卡号：</span>
                        <span class="v" id="lblPayAccount"></span>
                    </p>
                    <p>
                    	<span class="k">取款姓名：</span>
                        <span class="v" id="lblPayName"></span>
                    </p>
                    <p>
                    	<span class="k">开户地址：</span>
                        <span class="v" id="lblAddress"></span>
                    </p>
                </div>
            	<div class="form-item">
                    <div class="item-title">
                        <label class="lab">提款金额</label>
                    </div>
                    <div class="item-content">
                        <input id="txtPayMoney" type="text" value="" class="ipt" onkeypress="chkPrice(this)" placeholder="请输入取款金额"/>
                    </div>
                    <div class="item-extra">
                        元
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">安全密码</label>
                    </div>
                    <div class="item-content">
                        <input id="txtNewPass1" value="" class="ipt" onfocus="this.type='password'" autocomplete="off" placeholder="请输入取款密码"/>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">验证码</label>
                    </div>
                    <div class="item-content">
                         <input id="txtUserCode" type="text" value="" class="ipt" placeholder="请输入验证码"/>
                    
                    </div><img id="imgCode" onclick="_jcms_GetRefreshCode('imgCode');" src="" alt="" class="code" />
                </div>
                <div class="form-btns">
                    <input type="button" value="确定取款" onclick="chkPost();" class="btn primary-btn" />
                </div>
                <div class="form-btns">
                <span>提款前5次免手续费！超过5次按1%收取，最高25元！</span>
                </div>
                 <div class="form-btns" style="display:none;">
                <span>提现时间为<span id="PriceTime1" class="balance"></span>--<font id="PriceTime2" class="balance"></font></span><span
                    id="name" style="display: none;"></span>
                <br />
                <span>每天允许<font id="PriceNum" class="balance"></font>次，单笔最小<font id="PriceOut" class="balance"></font>元，单笔最大<font
                    id="PriceOut2" class="balance"></font>元</span>
                <br />
                <span>您今日已申请提现<font id="txcs" class="balance"></font>次，合计<font id="txje" class="balance"></font>元</span>
            </div>
            </form>
        </div>
    </main>
    </div>
</body>
</html>
