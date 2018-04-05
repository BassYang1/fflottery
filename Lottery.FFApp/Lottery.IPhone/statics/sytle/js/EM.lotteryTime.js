
//显示开奖信息，时间等
function ajaxLotteryTime() {
    intDiff = 0;

    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxLotteryTime" + lid,
        success: function (data) {
            currsn = data.cursn;
            nestsn = data.nestsn;
            $('#lotteryname').html(data.name + "<i class='icon-arr'></i>");
            $('#cursn').html(currsn);
            $('#nestsn').html(nestsn + '期');
            $('#nestsn2').html(nestsn + '期');
            intDiffClose = intDiffCloseWarn = parseInt(data.closetime);
            intDiff = parseInt(data.ordertime);
			ordertime = data.ordertime;
            timer();
        }
    });
    
    //加载前10期开奖信息
    ajaxListNav();
}

var ordertime = 0
var IsKaijiang = false;
var currsn;
var nestsn;
var intDiff = parseInt(0);
var intDiffClose = parseInt(0);
var intDiffCloseWarn = parseInt(0);
var diff = 0;
var timers;
function timer() {
    clearInterval(timers);
    timers = window.setInterval(function () {
        if (intDiff == 0) {
            ajaxLotteryTime();
        }
		if (intDiff == 0) {
            ajaxLotteryTime();
        }
        if (intDiff == intDiffClose) {
            $('#states').html("封单");
            $('#states2').html("封单");
            diff = intDiffClose;
            opertime(diff);
            intDiffClose--;
            intDiff--;
        }
        else if (intDiff < intDiffClose) {
            $('#states').html("封单");
            $('#states2').html("封单");
            diff = intDiff;
            opertime(diff);
            intDiff--;
        }
        else {
            $('#states').html("投注");
            $('#states2').html("投注");
            diff = intDiff - intDiffClose;
            opertime(diff);
            intDiff--;
        }
        if (!IsKaijiang) {
            if (intDiff % 2 == 0) {
                $.ajax({
                    type: "get",
                    dataType: "json",
                    data: "clienttime=" + Math.random(),
                    url: "/ajax/ajax.aspx?oper=GetLotteryNumber" + lid,
                    success: function (data) {
                        if (data.table.length > 0) {
                            var numberArr = data.table[0].number;
                            if (data.table[0].title == $('#cursn').html()) {
								
                                $('#strnumber').html(GetSytle(numberArr));
								//$('#cursn').html(data.table[0].title);
								ajaxListNav();
                            }
                            else {
								//$('#cursn').html(nestsn);
                                $('#strnumber').html("正在开奖中");
                                IsKaijiang = false;
                            }
                        }
                        else {
							//$('#cursn').html(nestsn);
                            $('#strnumber').html("正在开奖中");
                            IsKaijiang = false;
                        }
                    }

                });
            }
        }
    }, 1000);
}
function opertime(diff) {
    var day = 0,
		hour = 0,
		minute = 0,
		second = 0;

    if (diff > 0) {
        day = Math.floor(diff / (60 * 60 * 24));
        hour = Math.floor(diff / (60 * 60)) - (day * 24);
        minute = Math.floor(diff / 60) - (day * 24 * 60) - (hour * 60);
        second = Math.floor(diff) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
    }
    var hours = day * 24 + hour;
    if (hours <= 9) hours = '0' + hours;
    if (minute <= 9) minute = '0' + minute;
    if (second <= 9) second = '0' + second;
    //$('#abc').html(hours + ":" + minute + ":" + second.toString().substr(0, 1));
    $('#lotterytime').html(hours + ":" + minute + ":" + second);
    $('#lotterytime2').html(hours + ":" + minute + ":" + second);
}

function GetSytle(balls) {
    if (balls != "") {
        var strArray = balls.split(",");
        var str = "";
        for (var i = 0; i < strArray.length; i++) {
            str += "<span class='n'>" + strArray[i] + "</span>";
        }
        return str;
    }
}
