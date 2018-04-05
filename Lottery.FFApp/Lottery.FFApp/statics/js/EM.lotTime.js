var lotTimes = 0;
//显示开奖信息，时间等
function ajaxLotteryTime() {
    IsKaijiang = false;
    intDiff = 0;
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url:"/ajax/ajax.aspx?oper=ajaxLotteryTime" + lid,
        success: function (data) {
            currsn = data.cursn;
            nestsn = data.nestsn;
            $('#lotteryname').html(data.name);
            $('#nestsn').html(nestsn);
            $('#nestsn2').html(nestsn);
            $('#cursn').html(currsn);
            intDiffClose = parseInt(data.closetime);
            intDiff = parseInt(data.ordertime);
            lotTimes = parseInt(intDiff) - parseInt(intDiffClose);
            diff = parseInt(intDiff) - parseInt(intDiffClose);
//            if (lotTimes < 0) {
//                lotTimes = parseInt(intDiffClose);
//                ajaxLotteryTime();
//            }
            $("#lotOpenNum").html(data.opennum);
            $("#lotNoOpenNum").html(eval($("#lotIssNum").html()) - eval(data.opennum));
            bettingCountdown();
            timer();
        }
    });
}

var bettingCountdown = function () {
    ///console.log("===倒计时时间=== : " + lotTimes);
    bettingConfirmCountdown();
    var $countdown = $("#betting-countdown");
    var ms = parseInt(lotTimes);
    Util.countdown(ms, true, true, true, function (time) {
		//console.info(time);
        time = time.split(":");
        var str = '';
        for (var i = 0; i < time.length; i++) {
            str += '<span>' + time[i] + '</span>'
        }
        $countdown.html(str);
    });
}

var bettingConfirmCountdown = function () {
    var $countdown = $("#confirm-countdown");
    var ms = parseInt(lotTimes);
    Util.countdown(ms, true, true, true, function (time) {
        $countdown.text(time);
    });
}

var lotteryResult = function () {
    var $lotteryNumbers = $("#lottery-numbers");
    if ($lotteryNumbers.size() == 0) return;
    var numberArr = $("#lotNumber").val();
    numberArr = (numberArr + "").split(",");
    $lotteryNumbers.children("li").each(function (index) {
        var $item = $(this);
        var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
        numberRun.run();
    });
}

var numbers = new Array()
var IsKaijiang = false;
var currsn;
var nestsn;
var intDiff = parseInt(0);
var intDiffClose = parseInt(0);
var diff = 0;
var timers;
function timer() {
    clearInterval(timers);
    timers = window.setInterval(function () {
		//console.info("loading"+intDiff);
        if (intDiff == 0) {
            ajaxLotteryTime();
        }
        if (intDiffClose != 0) {
            if (intDiff == intDiffClose) {
                lotTimes = parseInt(intDiffClose);
                bettingCountdown();
                emAlert('第' + nestsn + '期' + "投注已截止");
                audioPlay('fengdan');
            }
        }
        if (intDiff <= intDiffClose) {
            $('#state').html("封单");
            diff = intDiffClose;
        }
        else {
            $('#state').html("投注");
        }

        intDiff--;
        diff--;

        if (!IsKaijiang) {
            if (intDiff % 2 == 0) {
                $.ajax({
                    type: "get",
                    dataType: "json",
                    data: "clienttime=" + Math.random(),
                    url: "/ajax/ajax.aspx?oper=GetLotteryNumber" + lid,
                    success: function (data) {
                        var $lotteryNumbers = $("#lottery-numbers");
                        if (data.table.length > 0) {
                            if (data.table[0].title == $('#cursn').html()) {
                                $('#numberstate').html("开奖结果");
                                $('#kjLoading').hide();
                                $('#lottery-numbers').show();
                                var numberAll = data.table[0].numberall.split(",");
                                var numberArr = data.table[0].number;
                                setCookie("lotNumbers" + LotteryId, numberArr);
                                numberArr = (numberArr + "").split(",");
                                if (numberAll.length >= 20) {
                                    $lotteryNumbers.children("li").each(function (index) {
                                        var nemberSum = (parseInt(numberAll[4 * index])
                                        + parseInt(numberAll[4 * index + 1])
                                        + parseInt(numberAll[4 * index + 2])
                                        + parseInt(numberAll[4 * index + 3])) + "";
                                        numbers[index] = numberAll[4 * index] + "+"
                                        + numberAll[4 * index + 1] + "+"
                                        + numberAll[4 * index + 2] + "+"
                                        + numberAll[4 * index + 3] + "="
                                        + nemberSum.substr(0, nemberSum.length - 1) + "<span style='color:Red'>" + nemberSum.substr(nemberSum.length - 1, 1) + "</span>";
                                        var $item = $(this);
                                        var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
                                        numberRun.run();
                                    });
                                }
                                else {
                                    $lotteryNumbers.children("li").each(function (index) {
                                        var $item = $(this);
                                        var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
                                        numberRun.run();
                                    });
                                }
                                IsKaijiang = true;
                                audioPlay('kaijiang');
                                ajaxList();
                                ajaxUserTotalList();
                            }
                            else {
                                $('#numberstate').html("等待开奖");
                                $('#kjLoading').html(GetNoOpenInfo("正", "在", "开", "奖", "中"));
                                $('#kjLoading').show();
                                $('#lottery-numbers').hide();
                            }

                            ajaxLotteryDataList(data);
                        }
                        else {
                            ajaxLotteryTime();
                            console.log("更新期号");
                            $('#numberstate').html("等待开奖");
                            $('#kjLoading').html(GetNoOpenInfo("正", "在", "开", "奖", "中"));
                            $('#kjLoading').show();
                            $('#lottery-numbers').hide();
                        }
                    }
                });
            }
        }
    }, 1000);
}

function ajaxLotteryDataList(d) {
    var html = "";
    if (d.table.length > 0) {
        for (var i = 0; i < d.table.length; i++) {
            var t = d.table[i];
            html += "<li><span class='issue'>" + t.title + "</span> ";
            if (Nmbtype != 4) {
                html += "<span class='number'>";
                var strArray = t.number.split(',');
                for (var j = 0; j < strArray.length; j++) {
                    html += " <span class='n'>" + strArray[j] + "</span>";
                }
                html += "</span>";
            }
            else {
                html += "<span class='number'>";
                html += t.number;
                html += "</span>";
            }
            html += "</li>";
        }
    }
    $("#today-betting").html(html);
}