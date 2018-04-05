var LotteryType = 1;
var LotteryId = 1001;
var LotteryMinTimes = 1;
var LotteryMaxTimes = 1000;
var PlayBigId;
var PlayBigName;
var PlayId;
var PlayName;
var PlayCode;
var PlayRandoms;
var PlayWzNum;
var PlayExample;
var PlayHelp;
var PlayRemark;
var playPoints;
var playBouns;
var PlayPos = "";
var Price = 1; //圆角分
var PriceName = "元";
var PlayMaxNum = 100;
var PlayMaxBonus = 0;
var PlayMinBonus = 0;
var PlayMinBonus2 = 0;
var PlayPosBonus = 0;
var PriceModel = 1; //奖金模式

var Nmbtype = 1;
var PriceTimes = 1; //倍数
var SingleCount = 0; //单笔注数
var SingleTotal = 0; //单笔金额
var SumCount = 0; //总注数
var SumTotal = 0; //总金额
var PriceTotal = 0; //不加倍数的金额
var SumOrder = 0; //总单数
var ArrayOrder = new Array(); //投注列表
var SumZhCount = 0;
var SumZhTotal = 0;
var Betpoint = 0;
var lid = joinValue('lid');
var Adminname;
var UserMoney = 0;
var UserScore = 0;
var SingleOrderItem;
var ZhSumCount;
var SelectedData = [];
var StartSn = "";
var NmbZH = 1;

$(document).ready(function () {
    Nmbtype = $("#txtTid").val();
    LotteryId = $("#txtLoid").val();
    lid = "&lid=" + LotteryId;
    InitHeader();
});

//加载购彩头部
function InitHeader() {
    var html = "";
    html += '<div class="lottery-header">';
    html += '<div class="lottery-hl">';
    html += '<div id="lottery-logo" class="lottery-logo"></div>';
    html += '</div>';
    html += '<div class="lottery-hm">';
    html += '<h1 id="lotteryname" class="lottery-name"></h1>';
    html += '<div class="lottery-countdown">';
    html += '<div class="lottery-issue">第<span id="nestsn" class="issue"></span>期</div>';
    html += '<div class="betting-countdown" id="betting-countdown" data-ms="300"></div>';
    html += '</div>';
    html += '<div class="lottery-result">';
    html += '<div class="lottery-issue">第<span id="cursn" class="issue"></span>期<br/><span id="numberstate">等待开奖</span>';
    html += '<a href="javascript:;" onclick="LayerPop(\'声音设置\',\'400px\',\'230px\',\'/html/sound.html\')" id="lottery-sound" class="lottery-sound"><i class="icon icon-sound"></i></a></div>';
    html += '<div class="lottery-show">';
    html += '<input type="hidden" id="lotNumber" value="0" />';
//    if (getCookie("lotNumbers" + LotteryId) != null) {
//        var $lotteryNumbers = $("#lottery-numbers");
//        var numberArr = getCookie("lotNumbers" + LotteryId);
//        numberArr = (numberArr + "").split(",");
//        $lotteryNumbers.children("li").each(function (index) {
//            var $item = $(this);
//            var numberRun = new NumberRun($item.find(".number-run"), numberArr[index]);
//            numberRun.run();
//        });
    //    }
    html += '<ul class="lottery-numbers2"  id="kjLoading">';
    html += GetNoOpenInfo("正","在","加","载","中");
    html += '</ul>';
    html += '<ul class="lottery-numbers" id="lottery-numbers" style="display: none;">';
    html += '</ul>';
    html += '</div>';
    html += '</div>';
    html += ' </div>';
    html += '<div class="lottery-hr">';
    html += '<div class="lottery-links">';
    html += '<a href="/chart/index.aspx?id=' + LotteryId + '" target="_blank" class="btn"><i class="icon icon-trend"></i>号码走势</a>';
    html += '<a href="javascript:;" class="btn" id="history-toggle"><i class="icon icon-history"></i>历史开奖</a>';
    html += '</div>';
    html += '</div>';
    html += '<div class="lottery-history" id="lottery-history">';
    html += '<div class="history-heading">';
    html += '<span class="issue">期号</span>';
    html += '<span class="number">开奖号</span>';
    html += '</div>';
    html += '<div class="history-content">';
    html += '<ul id="today-betting" class="history-list">';
    html += '</ul>';
    html += '</div>';
    html += '</div>';
    html += '</div>';

    $("#lottery-header").html(html);

    if (LotteryId == 1010) {
        $("#lottery-web").html("<a href='http://www.knlotto.org' target='_blank'>游戏官网</a>");
    }

    if (LotteryId == 1012) {
        $("#lottery-web").html("<a href='http://www.sglotto.net' target='_blank'>游戏官网</a>");
    }

    if (LotteryId == 1013) {
        $("#lottery-web").html("<a href='http://www.taiwanlottery.com.tw/Lotto/BINGOBINGO/drawing.aspx' target='_blank'>游戏官网</a>");
    }

    if (LotteryId == 1014) {
        $("#lottery-web").html("<a href='http://www.tokyo-keno.jp' target='_blank'>游戏官网</a>");
    }

    if (LotteryId == 1015) {
        $("#lottery-web").html("<a href='http://www.happylottery.ph' target='_blank'>游戏官网</a>");
    }

    if (LotteryId == 1016) {
        $("#lottery-web").html("<a href='http://www.tokyo-keno.cc' target='_blank'>游戏官网</a>");
    }

    if (LotteryId == 1017) {
        $("#lottery-web").html("<a href='http://www.jlotto.kr/keno.aspx?method=kenoWinNoList' target='_blank'>游戏官网</a>");
    }

    if (LotteryId == 1004) {
        $("#lottery-web").html("<a href='http://www.ny-lotto.bet' target='_blank'>游戏官网</a>");
    }

    LotteryMinTimes = lotJson.table[0].mintimes;
    LotteryMaxTimes = lotJson.table[0].maxtimes;
    $("#lotremark").html(lotJson.table[0].iphoneremark);
    $("#lotIssNum").html(lotJson.table[0].issnum);
    $("#lottery-logo").html("<img src='/statics/img/" + LotteryId + ".png' alt='' />");
    if (LotteryId >= 1001 && LotteryId <= 1013) {
        $("#lotName").removeClass().addClass("zj-lottery lottery-ssc");
        $("#lottery-container").removeClass().addClass("lottery-container lottery-ssc");
    }
    if (LotteryId >= 2001 && LotteryId <= 2006) {
        $("#lotName").removeClass().addClass("zj-lottery lottery-11x5");
        $("#lottery-container").removeClass().addClass("lottery-container lottery-11x5");
    }
    if (LotteryId == 3001) {
        $("#lottery-container").removeClass().addClass("lottery-container lottery-ssl");
    }
    if (LotteryId == 3002 || LotteryId == 3004 || LotteryId == 3005) {
        $("#lottery-container").removeClass().addClass("lottery-container lottery-fc3d");
    }
    if (LotteryId == 3003) {
        $("#lottery-container").removeClass().addClass("lottery-container lottery-tcp3");
    }
    if (LotteryId >= 4001) {
        $("#lottery-container").removeClass().addClass("lottery-container lottery-pk10");
    }
    if (getCookie("price") != null) {
        $("#model").val(getCookie("price"));
        Price = getCookie("price");
    }
    else {
        Price = 1;
        $("#model").val("1");
    }

    InitNumber();
    ajaxLotteryTime();
    ajaxBigType();
    ajaxAddAfterClear();
    ajaxBetAfterClear();
    lotteryHistory();
    lotterymore();
}

//gengduo
var lotterymore = function () {
    var $lotNav = $('#lottery-games');
    $('#lottery-more').on('click', function (e) {
        $lotNav.html(GetLottery());
        e.stopPropagation();
        var offset = $(this).offset();
        $lotNav.css({
            left: offset.right
        });
        $lotNav.show();
    });

    $lotNav.on('click', function (e) {
        e.stopPropagation();
    });

    $(document).on('click', function () {
        $lotNav.hide();
    });
};

//清空号码
function ClearNum() {
    SingleCount = 0;
    SingleTotal = 0;
    $i("fromBuyNumberCount").innerHTML = '0';
    $i("fromBuyPriceTotal").innerHTML = '0.0000';
    $('#inputtext').val("");
}

//添加号码后清空
function ajaxAddAfterClear() {
    SingleCount = 0;
    SingleTotal = 0;
    $i("fromBuyNumberCount").innerHTML = '0';
    $i("fromBuyPriceTotal").innerHTML = '0.0000';
    $('.lottery-numbers li span').removeClass('selected');
    $(".lottery-balls").find("span[number]").removeClass().addClass("ball");
    $('#inputtext').val("");
}

//投注后清空
function ajaxBetAfterClear() {
    ClearRow();
    $i("fromBuyNumberSumCount").innerHTML = '0';
    $i("fromBuyPriceSumTotal").innerHTML = '0.0000';
    $i("sumOrder").innerHTML = '0';
    $("#betting-list").empty();
    SumCount = 0;
    SumTotal = 0;
    PriceTotal = 0;
    SumOrder = 0;
    ArrayOrder.length = 0;
}

//正在开奖中
function GetNoOpenInfo(s1,s2,s3,s4,s5) {
    var html = "";
    html += '<li>';
    html += '<div class="inner">';
    html += '<div class="number-run">';
    html += '<span class="number">'+s1+'</span>';
    html += '</div>';
    html += '</div>';
    html += '</li>';
    html += '<li>';
    html += '<div class="inner">';
    html += '<div class="number-run">';
    html += '<span class="number">' + s2 + '</span>';
    html += '</div>';
    html += '</div>';
    html += '</li>';
    html += '<li>';
    html += '<div class="inner">';
    html += '<div class="number-run">';
    html += '<span class="number">' + s3 + '</span>';
    html += '</div>';
    html += '</div>';
    html += '</li>';
    html += '<li>';
    html += '<div class="inner">';
    html += '<div class="number-run">';
    html += '<span class="number">' + s4 + '</span>';
    html += '</div>';
    html += '</div>';
    html += '</li>';
    html += '<li>';
    html += '<div class="inner">';
    html += '<div class="number-run">';
    html += '<span class="number">' + s5 + '</span>';
    html += '</div>';
    html += '</div>';
    html += '</li>';
    return html;
}

//历史开奖
var lotteryHistory = function () {
    var $lotteryHistory = $('#lottery-history');
    $('#history-toggle').on('click', function () {
        if ($lotteryHistory.is(':visible')) {
            $lotteryHistory.hide();
        } else {
            $lotteryHistory.show();
        }
    });
};

//数字提示
function mOver(obj, i) {
    if (numbers.length > 0) {
        layer.tips(numbers[i], obj, {
            tips: [3, '#0FA6D8'] //还可配置颜色
        });
    }
}

function mOut() {
    layer.closeAll('tips');
}

//加载数字滚动
function InitNumber() {
    var html = "";
    if (Nmbtype == 1) {
        html += '<li onmouseover="mOver(this,0)" onmouseout="mOut()">';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li onmouseover="mOver(this,1)" onmouseout="mOut()">';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li onmouseover="mOver(this,2)" onmouseout="mOut()">';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li onmouseover="mOver(this,3)" onmouseout="mOut()">';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li onmouseover="mOver(this,4)" onmouseout="mOut()">';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';
    }
    if (Nmbtype == 2) {
        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';
        html += '<span class="number" data-number="11">11</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';
        html += '<span class="number" data-number="11">11</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';
        html += '<span class="number" data-number="11">11</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';
        html += '<span class="number" data-number="11">11</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';
        html += '<span class="number" data-number="11">11</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';
    }
    if (Nmbtype == 3) {
        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="0">0</span>';
        html += '<span class="number" data-number="1">1</span>';
        html += '<span class="number" data-number="2">2</span>';
        html += '<span class="number" data-number="3">3</span>';
        html += '<span class="number" data-number="4">4</span>';
        html += '<span class="number" data-number="5">5</span>';
        html += '<span class="number" data-number="6">6</span>';
        html += '<span class="number" data-number="7">7</span>';
        html += '<span class="number" data-number="8">8</span>';
        html += '<span class="number" data-number="9">9</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';
    }
    if (Nmbtype == 4) {
        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';

        html += '</div>';
        html += '</div>';
        html += '</li>';

        html += '<li>';
        html += '<div class="inner">';
        html += '<div class="number-run">';
        html += '<span class="number" data-number="01">01</span>';
        html += '<span class="number" data-number="02">02</span>';
        html += '<span class="number" data-number="03">03</span>';
        html += '<span class="number" data-number="04">04</span>';
        html += '<span class="number" data-number="05">05</span>';
        html += '<span class="number" data-number="06">06</span>';
        html += '<span class="number" data-number="07">07</span>';
        html += '<span class="number" data-number="08">08</span>';
        html += '<span class="number" data-number="09">09</span>';
        html += '<span class="number" data-number="10">10</span>';
        html += '</div>';
        html += '</div>';
        html += '</li>';
    }
    $("#lottery-numbers").html(html);
}


function ajaxUserTotalList() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxHistory.aspx?oper=GetUserTotalList",
        error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
        success: function (d) {
            var html = "<ul class='betting-overview'>";
            html += "<li><span>日期</span> <span>投注额</span> <span>中奖额</span> </li>";
            if (d.result == "1") {
                if (d.table.length > 0) {
                    for (var i = 0; i < d.table.length; i++) {
                        var t = d.table[i];
                        html += "<li><span>" + t.name + "</span> <span>" + t.bet + "</span> <span>" + t.win + "</span> </li>";
                    }
                }
            }
            html += "</ul>";
            $("#userTotal").html(html);
        }
    });
}

function ajaxList() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxBet.aspx?oper=ajaxGetListIndex",
        error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
        success: function (d) {
            var html = '<a href="/vip.b" class="query-more"><i class="icon icon-more"></i>更多</a>';
            html += '<div class="betting-table">';
            html += '<div class="table-head">';
            html += '<span class="no">编号</span>';
            html += '<span class="state">状态</span>';
            html += '</div>';
            html += '<div class="table-body">';
            html += '<ul class="betting-list">';
            if (d.result == "1") {
                if (d.table.length > 0) {
                    for (var i = 0; i < d.table.length; i++) {
                        var t = d.table[i];
                        var title = '投注详情-' + t.lotteryname + '-' + t.playname;
                        var url = "/bet/betInfo.html?id=" + t.id;
                        html += '<li>';
                        html += '<span class="no"><a href="javascript:;" onclick="LayerPop(\'' + title + '\',\'800px\',\'615px\',\'' + url + '\')">' + t.ssid + '</a></span>';
                        if (t.state == "0")
                            html += "<span class='state'>未开奖</span>";
                        if (t.state == "1")
                            html += "<span class='state' style='color: #ccc;'>已撤单</span>";
                        if (t.state == "2")
                            html += "<span class='state' style='color: green;'>未中奖</span>";
                        if (t.state == "3")
                            html += "<span class='state' style='color: red;'>已中奖</span>";
                        html += '</li>';
                    }
                }
            }
            html += '</ul>';
            html += '</div>';
            html += '</div>';
            $("#mybetting").html(html);
        }
    });
}

function ajaxZhBetList() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxBet.aspx?oper=ajaxGetZHListIndex",
        error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
        success: function (d) {
            var html = '<a href="/vip.b1" class="query-more"><i class="icon icon-more"></i>更多</a>';
            html += '<div class="betting-table">';
            html += '<div class="table-head">';
            html += '<span class="no">编号</span>';
            html += '<span class="state">状态</span>';
            html += '</div>';
            html += '<div class="table-body">';
            html += '<ul class="betting-list">';
            if (d.result == "1") {
                if (d.table.length > 0) {
                    for (var i = 0; i < d.table.length; i++) {
                        var t = d.table[i];
                        var title = '投注详情';
                        var url = "/aspx/list.aspx?nav=BetZhInfoIndex&id=" + t.id;
                        html += '<li>';
                        html += '<span class="no"><a href="javascript:;" onclick="LayerPop(\'' + title + '\',\'900px\',\'615px\',\'' + url + '\')">' + t.ssid + '</a></span>';
                        html += "<span class='state'>" + t.statename + "</span>";
                        html += '</li>';
                    }
                }
            }
            html += '</ul>';
            html += '</div>';
            html += '</div>';
            $("#mybetting").html(html);
        }
    });
}