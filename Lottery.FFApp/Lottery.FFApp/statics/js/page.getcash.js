$(document).ready(function () {
    $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
        emAlert(msg);
    },
        onsuccess: function () { return true; }
    });
    $("#txtPayMoney").formValidator({ tipid: "tipPayMoney", onshow: "请输入提现金额  ", onfocus: "请输入提现金额", defaultvalue: "" }).InputValidator({ min: 0, onerror: "提现金额不能为空" }).RegexValidator({ regexp: "^([1-9]{1}[0-9]{0,4})$", onerror: "提现金额请输入整数" });
    $("#txtNewPass1").formValidator({ tipid: "tipNewPass1", onshow: "请输入资金密码  ", onfocus: "资金密码6-14位" }).InputValidator({ min: 6, max: 14, onerror: "资金密码为6-14位" });

    //金额大写
    $("#txtPayMoney").delegate('', 'keyup', function (event) {
        chkPrice(this);
        $('#moneyUpper').val(atoc($('#txtPayMoney').val()));
    });
    ajaxIsTrueName();
});

function ajaxIsTrueName() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxCenter.aspx?oper=ajaxGetUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
        success: function (d) {
            if (d.result == "1") {
                var userlevel = 0;
                if (d.table.length > 0) {
                    var t = d.table[0];
                    if (t.istruename == "1") {
                        ajaxList();
                    }
                    else {
                        parent.layer.confirm('您还没有绑定真实姓名？', {
                            icon: 3,
                            title: '温馨提示',
                            btn: ['立即绑定'],
                            shade: 0.2
                        }, function () {
							LayerPop("绑定真实姓名", "650px", "400px", "/money/trueName.html");
                        });
                    }
                }
            }
            else {
                window.location.href = '/login';
            }
        }
    });
}

function ajaxBankPop() {
    LayerPop("绑定银行卡", "650px", "550px", "/center/bankInfo.aspx");
}

var strjson = "";
var maxgetcash = 0;
function ajaxList() {
    var index = emLoading();
    $.ajax({
        type: "get",
        dataType: "json",
        data: "page=1&pagesize=1&clienttime=" + Math.random(),
        url: "/ajax/ajaxBank.aspx?oper=ajaxGetList",
        error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
        success: function (d) {
            strjson = d;
            var html = "";
            if (d.result == "1") {
                if (d.table.length > 0) {
                    parent.layer.closeAll();
                    for (var i = 0; i < d.table.length; i++) {
                        var t = d.table[i];
                        if (i == 0) {
                            html += '<li class="selected" id="' + t.id + '" bankid="' + t.paymethod + '" userid="' + t.id + '">';
                            $("#lblBankId").val(d.table[0].paymethod);
                            $("#lblUserBankId").val(d.table[0].id);
                        }
                        else {
                            html += '<li id="' + t.id + '" bankid="' + t.paymethod + '" userid="' + t.id + '">';
                        }
                        html += '<span class="bank-logo"><img src="/statics/img/bank_36_icbc.png" alt=""/></span>';
                        html += '<p><span class="bank-name">' + t.paybank + '</span><span class="bank-user">' + t.payname.substr(0, 1) + '**</span></p>';
                        html += '<p>尾号' + t.payaccount.substr(t.payaccount.length - 4, 4) + '</p>';
                        html += '</li>';
                    }

                    $i("PriceTime1").innerHTML = d.table[0].starttime;
                    $i("PriceTime2").innerHTML = d.table[0].endtime;
                    $i("PriceOut").innerHTML = parseFloat(d.table[0].mincharge).toFixed(2);
                    $i("PriceOut2").innerHTML = parseFloat(d.table[0].maxcharge).toFixed(2);

                    $i("money").innerHTML = parseFloat(d.table[0].money).toFixed(2);
                    $i("txcs").innerHTML = parseFloat(d.table[0].txcs).toFixed(2);
                    $i("txje").innerHTML = parseFloat(d.table[0].txje).toFixed(2);
                    // parseInt(d.table[0].maxgetcash) - parseInt(d.table[0].txcs);
                    if(parseInt(d.table[0].txcs)<=5)
                        $i("synum").innerHTML = 5 - parseInt(d.table[0].txcs);
                    else
                        $i("synum").innerHTML = 0;
                    maxgetcash = d.table[0].maxgetcash;
                }
                else {
                    parent.layer.confirm('您还没有绑定银行信息？', {
                        icon: 3,
                        title: '温馨提示',
                        btn: ['立即绑定'],
                        closeBtn: 0,
                        shade: 0.2
                    }, function () {
                        //top.location.href = "/vip.c";
                        LayerPop("绑定银行卡","650px","550px","/center/bankInfo.aspx");
                    });
                }
            }
            html += "<li class='add-card'><a href='javascript:;' onclick='LayerPop(\"绑定银行卡\",\"650px\",\"550px\",\"/center/bankInfo.aspx\")'>";
            html += "<i class='icon icon-add'></i>绑定银行卡</a> </li>";
            $("#userBank").html(html);
            closeload(index);
        }
    });

     $(".bankcard-list").delegate('li', 'click', function (event) {
        $(this).parent().find('li').removeClass();
        $(this).addClass('selected');
        var id = $(this).attr("id");
        var bankid = $(this).attr("bankid");
        var userid = $(this).attr("userid");
        $("#lblBankId").val(bankid);
        $("#lblUserBankId").val(userid);
        for (var k = 0; k < strjson.table.length; k++) {
            var t = strjson.table[k];
            if (t.id == id) {
                $i("PriceTime1").innerHTML = t.starttime;
                $i("PriceTime2").innerHTML = t.endtime;
                $i("PriceOut").innerHTML = parseFloat(t.mincharge).toFixed(2);
                $i("PriceOut2").innerHTML = parseFloat(t.maxcharge).toFixed(2);

                $i("money").innerHTML = parseFloat(t.money).toFixed(2);
                $i("txcs").innerHTML = parseFloat(t.txcs).toFixed(2);
                $i("txje").innerHTML = parseFloat(t.txje).toFixed(2);
                // parseInt(d.table[0].maxgetcash) - parseInt(d.table[0].txcs);
                if (parseInt(t.txcs) <= 5)
                    $i("synum").innerHTML = 5 - parseInt(t.txcs);
                else
                    $i("synum").innerHTML = 0;
                maxgetcash = t.maxgetcash;
            }
        }
    });
}

function chkPost() {
    var bankid = $("#lblBankId").val();
    var userid = $("#lblUserBankId").val();
    var uMoney = $("#txtPayMoney").val();
    var uPass = $('#txtNewPass1').val();
    if ($.formValidator.PageIsValid('1')) {
        if (eval(bankid) == 0 || eval(userid) == 0) {
            emAlert('您还未绑定银行信息，请先绑定');
            return;
        }
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
        else if (eval($i("txcs").innerHTML) >= eval(maxgetcash)) {
            emAlert('今日提现已得到最大提现次数');
            return;
        }
        else {
            var index = emLoading();
            $.ajax({
                type: "post",
                dataType: "json",
                data: "money=" + encodeURIComponent(uMoney) + "&pass=" + encodeURIComponent(uPass) + "&bankId=" + bankid + "&userBankId=" + userid,
                url: "/ajax/ajaxMoney.aspx?oper=ajaxCash&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                success: function (d) {
                    emAlertSuccess(d.returnval);
                    $("#txtPayMoney").val("");
                    $("#txtNewPass1").val("");
                    $i('moneyUpper').innerHTML = "";
//                    ajaxList();
                    closeload(index);
                }
            });
        }
    }
}