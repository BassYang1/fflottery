var typeId = "1";
var line = "1001";
var bank = "ICBC";
var chrMoney = 0;
var ChargeSetJson = "";
$(document).ready(function () {
    ajaxGetChartSetList();
    $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
        emAlert(msg);
    },
        onsuccess: function () { return true; }
    });
    $("#recharge-money").formValidator({ tipid: "tipPayMoney", onshow: "请输入充值金额", onfocus: "请输入充值金额", defaultvalue: "" }).InputValidator({ min: 1, max: 10, onerror: "请输入充值金额" }).RegexValidator({ regexp: "^([1-9]{1}[0-9]{0,8})$", onerror: "请输入整数" });
});

var paymentCountdown = function () {
    var $countdown = $("#payment-countdown");
    if ($countdown.length == 0) return;
    var ms = parseInt($countdown.attr("data-ms"));
    Util.countdown(ms, false, true, true, function (time) {
        time = time.split(":");
        var str = '';
        for (var i = 0; i < time.length; i++) {
            str += '<span>' + time[i] + '</span>'
        }
        $countdown.html(str);
    });
}

function ajaxGetChartSetList() {
    var index = emLoading();
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxBank.aspx?oper=ajaxGetChargeSet",
        error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
        success: function (d) {
            ChargeSetJson = d;
            var html = "";
            if (d.result == "1") {
                if (d.table.length > 0) {
                    for (var i = 0; i < d.table.length; i++) {
                        var t = d.table[i];
                        if (i == 0) {
                            if (d.table.length == 1) {
                                html += "<li class='current' tnmb='" + t.type + "' nmb='" + t.id + "'><a href='javascript:;'>" + t.mername + "</a></li>";
                            }
                            else {
                                html += "<li class='first current' tnmb='" + t.type + "' nmb='" + t.id + "'><a href='javascript:;'>" + t.mername + "</a></li>";
                            }
                            line = t.id;
                            typeId = t.type;
                            InItInfo(t.type, t.id);
                        }
                        else if (i == d.table.length - 1)
                            html += "<li class='last' tnmb='" + t.type + "' nmb='" + t.id + "'><a href='javascript:;'>" + t.mername + "</a></li>";
                        else
                            html += "<li tnmb='" + t.type + "' nmb='" + t.id + "'><a href='javascript:;'>" + t.mername + "</a></li>";
                    }
                }
            }
            $("#chargeSet").html(html);
            $(".recharge-way").delegate('li', 'click', function (event) {
                typeId = $(this).attr("tnmb");
                var setId = $(this).attr("nmb");
                line = setId;
                $(this).parents().find("li").removeClass("current");
                $(this).addClass("current");
                InItInfo(typeId, setId);
                step1Post();
            });
            closeload(index);
        }
    });
}

var merCode = "";
var merKey = "";

function InItInfo(typeId, setId) {
    for (var i = 0; i < ChargeSetJson.table.length; i++) {
        if (ChargeSetJson.table[i].id == setId) {
            $("#minCharge").html(ChargeSetJson.table[i].mincharge);
            $("#maxCharge").html(ChargeSetJson.table[i].maxcharge);
            $("#wrong-amount").html("<i class='icon icon-wrong'></i>充值限额：最低" + ChargeSetJson.table[i].mincharge + "元，最高" + ChargeSetJson.table[i].maxcharge + "元");
            $("#startTime").html(ChargeSetJson.table[i].starttime);
            $("#endTime").html(ChargeSetJson.table[i].endtime);
            merCode = ChargeSetJson.table[i].mercode;
            merKey = ChargeSetJson.table[i].merkey;
        }
    }
    $("#pZfb").html("");
    var bankHtml = "<ul class='cashier-banks'>";
    if (typeId == "1") {
        $("#divName").hide();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='ICBC' title='中国工商银行' class='radio' name='bank' checked/><span class='icon-bank bank-ICBC'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='ABC' title='中国农业银行' class='radio' name='bank'/><span class='icon-bank bank-ABC'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='CMBC' title='中国民生银行' class='radio' name='bank'/><span class='icon-bank bank-CMBC'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='CCB' title='中国建设银行' class='radio' name='bank'/><span class='icon-bank bank-CCB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='CMB' title='招商银行' class='radio' name='bank'/><span class='icon-bank bank-CMB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='BOCOM' title='中国交通银行' class='radio' name='bank'/><span class='icon-bank bank-BOCOM'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='BOC' title='中国银行' class='radio' name='bank'/><span class='icon-bank bank-BOC'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='PSBC' title='中国邮政储蓄银行' class='radio' name='bank'/><span class='icon-bank bank-PSBC'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='GDB' title='广发银行' class='radio' name='bank'/><span class='icon-bank bank-GDB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='CIB' title='兴业银行' class='radio' name='bank'/><span class='icon-bank bank-CIB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='CEB' title='中国光大银行' class='radio' name='bank'/><span class='icon-bank bank-CEB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='SPDB' title='上海浦东发展银行' class='radio' name='bank'/><span class='icon-bank bank-SPDB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='CITIC' title='中信银行' class='radio' name='bank'/><span class='icon-bank bank-CITIC'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='BOS' title='上海银行' class='radio' name='bank'/><span class='icon-bank bank-SHB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='PAB' title='平安银行' class='radio' name='bank'/><span class='icon-bank bank-PAB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='HXBC' title='华夏银行' class='radio' name='bank'/><span class='icon-bank bank-HXB'></span></label></li>";
    }
    if (typeId == "2") {
        $("#divName").hide();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='WX' class='radio' name='bank'/><span class='icon-bank bank-WX'></span></label></li>";
        bank = "WX";
    }
    if (typeId == "3") {
        $("#zfName").html("支付宝");
        $("#divName").show();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='ZFB' class='radio' name='bank'/><span class='icon-bank bank-ZFB'></span></label></li>";
        bank = "ZFB";
    }
    if (typeId == "4") {
        $("#divName").hide();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='ZFB' class='radio' name='bank'/><span class='icon-bank bank-ZFB'></span></label></li>";
        bank = "ZFB";
        var strpZfb = '<p>收费费率：支付宝充值100到账98.8，收费率1.2%</p>';
        $("#pZfb").html(strpZfb);
    }
    if (typeId == "5") {
        $("#zfName").html("银行卡");
        $("#divName").show();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='ICBC' class='radio' name='bank'/><span class='icon-bank bank-ICBC'></span></label></li>";
        bank = "ICBC";
        var strpZfb = '<p>收费费率：支付宝充值100到账98.8，收费率1.2%</p>';
        $("#pZfb").html(strpZfb);
    }
    if (typeId == "6") {
        $("#divName").hide();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='WECHATQR' class='radio' name='bank'/><span class='icon-bank bank-WX'></span></label></li>";
        bank = "WECHATQR";

        var strpZfb = '<p>收费费率：支付宝充值100到账98.8，收费率1.2%</p>';
        strpZfb += '<p>友情提示：微信充值成功率50%，充值失败的会员请选用其他方式充值，给您带来不便，敬请谅解！</p>';
        $("#pZfb").html(strpZfb);
    }
    if (typeId == "7") {
        $("#divName").hide();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='ALIPAYQR' class='radio' name='bank'/><span class='icon-bank bank-ZFB'></span></label></li>";
        bank = "ALIPAYQR";
        var strpZfb = '<p>收费费率：支付宝充值100到账98.8，收费率1.2%</p>';
        $("#pZfb").html(strpZfb);
    }
    if (typeId == "8") { //随笔付
        $("#divName").hide();
        bankHtml += "<li class='cashier-bank selected'><label><input type='radio' value='ZFB' title='支付宝即时到账' class='radio' name='bank' checked/><span class='icon-bank bank-ZFB'></span></label></li>";
        bankHtml += "<li class='cashier-bank'><label><input type='radio' value='WX' title='微信扫码支付' class='radio' name='bank'/><span class='icon-bank bank-WX'></span></label></li>";
        //bankHtml += "<li class='cashier-bank'><label><input type='radio' value='QQ' title='QQ钱包扫码支付' class='radio' name='bank'/><span class='icon-bank bank-QQ'></span></label></li>";
        //bankHtml += "<li class='cashier-bank'><label><input type='radio' value='CFT' title='财付通即时倒账' class='radio' name='bank'/><span class='icon-bank bank-CFT'></span></label></li>";
        bank = "SUIBIPAY";
    }
    bankHtml += "</ul>";
    $("#choose-bank").html(bankHtml);

    $("#choose-bank").each(function () {
        var $chooseBank = $(this);
        var $banks = $chooseBank.find(".cashier-bank");
        var selected = "selected";
        $banks.each(function () {
            var $bank = $(this),
				                $radio = $bank.find(".radio");
            $bank.on("click", function () {
                $banks.removeClass(selected);
                $bank.addClass(selected);
                $radio.prop("checked", true);
            });
        });
    });
}

function step1Post() {
    $("#step1").show();
    $("#step2").hide();
    $("#step3").hide();
    $("#s1class").removeClass().addClass("current");
    $("#s2class").removeClass();
    $("#s3class").removeClass();
    $("#txtName").val("");
    $("#recharge-money").val("");
}

function step2Post() {
    var bankCss = "";
    var bankName = "";
    var radios = document.getElementsByName("bank");
    for (var i = 0; i < radios.length; i++) {
        if (radios[i].checked == true) {
            bank = radios[i].value;
            bankName = radios[i].title;
        }
    }
    bankCss = "bank-" + bank;
    chrMoney = $("#recharge-money").val();
    var chrDxMoney = atoc(chrMoney);

    if (typeId == 3) {
        if ($("#txtName").val() == "") {
            emAlert('支付宝姓名不能为空');
            return;
        }
    }
    if ($.formValidator.PageIsValid('1')) {
        if (eval(chrMoney) < eval($("#minCharge").html())) {
            emAlert('充值金额不能小于最小充值金额');
            return;
        }
        else if (eval(chrMoney) > eval($("#maxCharge").html())) {
            emAlert('充值金额不能大于最大充值金额');
            return;
        }
        else {
            $("#step1").hide();
            $("#step2").show();
            $("#step3").hide();
            var userid = $("#txtAdminId").val();
            var username = $("#txtAdminName").val();
            if (typeId == 1) {
                var info = '<li><span class="si-name">充值银行：</span> <span class="si-con"><i class="icon-bank ' + bankCss + '"></i></span></li>';
                info += '<li><span class="si-name">需用银行卡：</span> <span class="si-con">可使用任意一张[' + bankName + ']进行汇款</span></li>';
                info += '<li><span class="si-name">会员账号：</span> <span class="si-con">' + username + '</span></li>';
                info += '<li><span class="si-name">充值金额：</span> <span class="si-con"><em>' + chrMoney + '</em>元</span></li>';
                info += '<li><span class="si-name">大写金额：</span> <span class="si-con"><em>' + chrDxMoney + '</em>元</span></li>';
                $("#chargeInfo").html(info);
            }
            if (typeId == 2 || typeId == 6) {
                var info = '<li><span class="si-name">充值银行：</span> <span class="si-con"><i class="icon-bank ' + bankCss + '"></i></span></li>';
                info += '<li><span class="si-name">需用银行卡：</span> <span class="si-con">请使用微信扫描付款</span></li>';
                info += '<li><span class="si-name">会员账号：</span> <span class="si-con">' + username + '</span></li>';
                info += '<li><span class="si-name">充值金额：</span> <span class="si-con"><em>' + chrMoney + '</em>元</span></li>';
                info += '<li><span class="si-name">大写金额：</span> <span class="si-con"><em>' + chrDxMoney + '</em>元</span></li>';
                $("#chargeInfo").html(info);
            }
            if (typeId == 3) {
                var info = '<li><span class="si-name">充值银行：</span> <span class="si-con"><i class="icon-bank ' + bankCss + '"></i></span></li>';
                info += '<li><span class="si-name">收款银行：</span> <span class="si-con">请在支付宝中选择【转到招商银行】进行转账</span></li>';
                info += '<li><span class="si-name">支付宝姓名：</span> <span class="si-con">' + $("#txtName").val() + '</span></li>';
                info += '<li><span class="si-name">充值金额：</span> <span class="si-con"><em>' + chrMoney + '</em>(' + chrDxMoney + ')元</span></li>';
                info += '<li><span class="si-name">收款账户：</span> <span class="si-con">' + merKey + '</span></li>';
                info += '<li><span class="si-name">收款卡号：</span> <span class="si-con">' + merCode + '</span></li>';
                info += '<li><span class="si-name">附言：</span> <span class="si-con">' + userid + '</span></li>';
                $("#chargeInfo").html(info);
            }
            if (typeId == 4 || typeId == 7) {
                var info = '<li><span class="si-name">充值银行：</span> <span class="si-con"><i class="icon-bank ' + bankCss + '"></i></span></li>';
                info += '<li><span class="si-name">需用银行卡：</span> <span class="si-con">请使用支付宝扫描付款</span></li>';
                info += '<li><span class="si-name">会员账号：</span> <span class="si-con">' + username + '</span></li>';
                info += '<li><span class="si-name">充值金额：</span> <span class="si-con"><em>' + chrMoney + '</em>元</span></li>';
                info += '<li><span class="si-name">大写金额：</span> <span class="si-con"><em>' + chrDxMoney + '</em>元</span></li>';
                $("#chargeInfo").html(info);
            }
            if (typeId == 5) {
                var info = '<li><span class="si-name">充值银行：</span> <span class="si-con"><i class="icon-bank ' + bankCss + '"></i></span></li>';
                info += '<li><span class="si-name">收款银行：</span> <span class="si-con">请登录网上银行进行转账</span></li>';
                info += '<li><span class="si-name">支付姓名：</span> <span class="si-con">' + $("#txtName").val() + '</span></li>';
                info += '<li><span class="si-name">充值金额：</span> <span class="si-con"><em>' + chrMoney + '</em>(' + chrDxMoney + ')元</span></li>';
                info += '<li><span class="si-name">收款账户：</span> <span class="si-con">' + merKey + '</span></li>';
                info += '<li><span class="si-name">收款卡号：</span> <span class="si-con">' + merCode + '</span></li>';
                info += '<li><span class="si-name">附言：</span> <span class="si-con">' + userid + '</span></li>';
                $("#chargeInfo").html(info);
            }
            if (typeId == 8) {
                var info = '<li><span class="si-name">充值银行：</span> <span class="si-con"><i class="icon-bank ' + bankCss + '"></i></span></li>';
                info += '<li><span class="si-name">需用银行卡：</span> <span class="si-con">请使用支付宝扫描付款</span></li>';
                info += '<li><span class="si-name">会员账号：</span> <span class="si-con">' + username + '</span></li>';
                info += '<li><span class="si-name">充值金额：</span> <span class="si-con"><em>' + chrMoney + '</em>元</span></li>';
                info += '<li><span class="si-name">大写金额：</span> <span class="si-con"><em>' + chrDxMoney + '</em>元</span></li>';
                $("#chargeInfo").html(info);
            }
            $("#s1class").removeClass();
            $("#s2class").removeClass().addClass("current");
            $("#s3class").removeClass();
            paymentCountdown();
        }
    }
}

function step3Post() {
    var adminId = $("#txtAdminId").val();
    if ($.formValidator.PageIsValid('1')) {
        if (eval(chrMoney) == 0) {
            emAlert('充值金额不正确！');
            return;
        }
        else {
            $("#step1").hide();
            $("#step2").hide();
            $("#step3").show();
            $("#s1class").removeClass();
            $("#s2class").removeClass();
            $("#s3class").removeClass().addClass("current");
            var url = "";
            if (typeId == 3) {
                $.ajax({
                    type: "post",
                    dataType: "json",
                    data: "setid="+line+"&name=" + encodeURIComponent($("#txtName").val()) + "&money=" + encodeURIComponent(chrMoney),
                    url: "/ajax/ajaxMoney.aspx?oper=ajaxCharge&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                    success: function (d) {
                        if (d.result == "1") {
                            url = "https://www.alipay.com/";
                            goTo(url);
                        }
                        else {
                            emAlert(d.returnval);
                            $("#txtPayMoney").val("");
                            $("#txtUserCode").val("");
                        }
                    }
                });
            }
            else if (typeId == 5) {
                $.ajax({
                    type: "post",
                    dataType: "json",
                    data: "setid=1020&name=" + encodeURIComponent($("#txtName").val()) + "&money=" + encodeURIComponent(chrMoney),
                    url: "/ajax/ajaxMoney.aspx?oper=ajaxCharge&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                    success: function (d) {
                        if (d.result == "1") {
                            url = "http://www.icbc.com.cn/icbc/";
                            goTo(url);
                        }
                        else {
                            emAlert(d.returnval);
                            $("#txtPayMoney").val("");
                            $("#txtUserCode").val("");
                        }
                    }
                });
            }
            else if (typeId == 8) { //随笔付
                var orderId = "";

                //获取订单号
                $.ajax({
                    type: "get",
                    dataType: "json",
                    async: false,
                    url: "/ajax/ajaxMoney.aspx?oper=ajaxChargeOrderId&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                    success: function (d) {
                        if (d.result == "1") {
                            orderId = d.returnval;
                        }
                    }
                });

                if (orderId) {
                    url = getHost() + "/pay/suibipay/pay.aspx?bank=" + bank + "&setId=" + line + "&amount=" + chrMoney + "&userId=" + adminId + "&orderId=" + orderId;
                    //LayerPop('随笔付支付', '1200px', '550px', url);
                    window.open(url);
                    checkState(orderId);
                }
            }
            else {
                url = "http://pay.feifan1010.com/sign" + line + ".aspx?Bank=" + bank + "&Id=" + line + "&Amount=" + chrMoney + "&UserId=" + adminId;
                //url = "http://localhost:30974/sign" + line + ".aspx?Bank=" + bank + "&Id=" + line + "&Amount=" + chrMoney + "&UserId=" + adminId;
                goTo(url);
            }
        }
    }
}

function goTo(url) {
    var targetWndName = "MyWindow";
    var wnd = window.open("", targetWndName);
    var link = document.getElementById("link");
    link.target = targetWndName;
    link.href = url;
    link.click();
}

function getHost() {
    var url = location.href;
    var temps = url.split("//");
    if (temps.length > 0) {
        var host = temps[0];
        temps = temps[1].split("/");
        
        if (temps.length > 0) {
            return host + "//" + temps[0];
        }
    }

    return "";
}

//检查订单状态
function checkState(orderId) {
    var adminId = $("#txtAdminId").val();
    var timer;
    var check = function (t) {
        $.ajax({
            type: "get",
            dataType: "json",
            url: "/ajax/ajaxMoney.aspx?oper=ajaxChargeState&userId=" + adminId + "&orderId=" + orderId + "&clienttime=" + Math.random(),
            error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
            success: function (d) {
                if (d.result == "1") {
                    var notice = new Audio("/statics/music/pay.mp3");
                    notice.play();

                    if (t) {
                        window.clearInterval(t);
                    }
                }
                else if (d.result == "-1") {
                    emAlert(d.returnval);

                    if (t) {
                        window.clearInterval(t);
                    }
                }
            }
        });
    };

    timer = window.setInterval(function () {
        check(timer);
    }, 1000); //轮询支付状态
}