

//检查充值订单
var checkAddFunds = function () {
    var lastDate = getCookie("LastChargeDateAdm");
    lastDate = (lastDate == undefined || lastDate == null) ? "" : lastDate;

    $.ajax({
        type: "get",
        dataType: "json",
        url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxChargeState&date=" + lastDate + "&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown);
        },
        success: function (d) {
            var num = isNaN(d.result) ? 0 : parseInt(d.result);

            if (num > 0 && d.returnval != "") {
                delCookie("LastChargeDateAdm");
                setCookie("LastChargeDateAdm", d.returnval);

                var notice = new Audio("/statics/music/pay.mp3");
                notice.play();
            }
        }
    });
};

var timerA = window.setInterval(function () {
    checkAddFunds();
}, 10000); //轮询支付状态


//检查充值订单
var checkWithdrawFunds = function () {
    var lastDate = getCookie("LastCashDateAdm");
    lastDate = (lastDate == undefined || lastDate == null) ? "" : lastDate;

    $.ajax({
        type: "get",
        dataType: "json",
        url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxCashState&date=" + lastDate + "&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown);
        },
        success: function (d) {
            var num = isNaN(d.result) ? 0 : parseInt(d.result);

            if (num > 0 && d.returnval != "") {
                delCookie("LastCashDateAdm");
                setCookie("LastCashDateAdm", d.returnval);

                var notice = new Audio("/statics/music/withdraw.mp3");
                notice.play();
            }
        }
    });
};

setTimeout(function () {
    //轮询提现状态
    var timerW = window.setInterval(function () {
        checkWithdrawFunds();
    }, 10000);
}, 5000);