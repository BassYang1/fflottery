var lotteryAdd = function () {
    ReplaceBalls();
    if (site.BetIsOpen == "1") {
        emAlert('系统正在维护不能投注！');
        return false;
    }
    if (eval($("#txtUserPoint").val()) >= eval(site.MaxLevel) * 10) {
        emAlert('系统设定，返点大于 ' + site.MaxLevel + ' 的会员不能投注！');
        return false;
    }
    if (playPoints < 0 || eval($("#txtUserPoint").val()) < playPoints) {
        emAlert('返点错误，请重新选择！');
        return false;
    }
    if (PriceTimes <= 0) {
        emAlert('倍数不正确，请从新选择倍数！');
        return false;
    }
    if (parseFloat(PriceTimes) < parseFloat(LotteryMinTimes) || parseFloat(PriceTimes) > parseFloat(LotteryMaxTimes)) {
        emAlert('倍数必须大于' + parseInt(LotteryMinTimes) + '，且小于' + parseInt(LotteryMaxTimes));
        $('#fromTimes').val(parseInt(LotteryMinTimes));
        PriceTimes = parseInt(LotteryMinTimes);
        fromTimesChange();
        return false;
    }
    if (SingleTotal == 0) {
        emAlert('请选择投注号码!');
        return false;
    }
    if (Price == "" || Price == "0") {
        emAlert('圆角分错误，请从新选择圆角分！');
        return false;
    }
    if (SingleOrderItem == "") {
        emAlert('投注号码有错或为空！');
        return false;
    }
    if (PlayCode != "PK10_DD1_5" && PlayCode != "PK10_DD6_10" && PlayCode != "P_DD" && PlayCode != "P11_DD") {
        if (parseFloat(PlayMaxNum) < parseFloat(SingleCount)) {
            emAlert('注数不能大于' + PlayMaxNum + '注');
            return false;
        }
    }
    var json = {
        "LotteryId": LotteryId,
        "PlayId": PlayId,
        "Price": Price * 1,
        "PriceName": PriceName,
        "times": PriceTimes,
        "Num": SingleCount,
        "singelBouns": playBouns,
        "Point": playPoints,
        "balls": SingleOrderItem,
        "strPos": PlayPos,
        "PlayName": PlayName,
        "alltotal": Price * SingleCount * PriceTimes * PriceModel
    };
    ArrayOrder.push(json);

    var $lottery = $("#lottery"),
		$lotteryPG = $(".lottery-playgroup", $lottery),
		$lotteryPGItems = $lotteryPG.children("li"),
        $lotteryPanels = $(".lottery-panel", $lottery),
        selected = "selected";
    var $bettingMode = $("#betting-mode"),
		$bettingList = $("#betting-list"),
		$container = $("#lottery-container");

    $bettingList.delegate(".del", "click", function () {
        $(this).parents("li").remove();
    });
    var $lotteryPGItem = $lotteryPGItems.filter("." + selected),
		pg = $lotteryPGItem.text(),
		pgI = $lotteryPGItems.index($lotteryPGItem),
		$lotteryPanel = $lotteryPanels.eq(pgI),
		$lotteryP = $lotteryPanel.find(".lottery-play"),
		$lotteryPItems = $lotteryP.find("dd"),
		$lotteryPItem = $lotteryPItems.filter("." + selected),
		p = $lotteryPItem.text(),
		pI = $lotteryPItems.index($lotteryPItem),
		$lotterySubPanel = $lotteryPanel.find(".lottery-subpanel").eq(pI),
		$selectedBalls = $lotterySubPanel.find("li:not(.lottery-pos) .lottery-balls").find(".ball." + selected),
		bettingMode = $bettingMode.find("." + selected).text(),
		bettingStr = '',
		$body = $("body");
    $selectedBalls.each(function (i) {
        var $selectedBall = $(this),
						offset = $selectedBall.offset(),
						targetOffset = $bettingList.offset(),
						$moveBall = $("<span class='" + $container.attr("data-cls") + "'></span>").append($selectedBall.clone());
        $moveBall.css({ left: offset.left, top: offset.top, position: 'absolute', zIndex: 1999 });

        $body.append($moveBall);
        setTimeout(function () {
            $moveBall.animate({ left: targetOffset.left, top: targetOffset.top, opacity: 0.2 }, 300, function () {
                $moveBall.remove();
            });
        }, 0);
    });

    CreateList();
    ajaxAddAfterClear();
}

var lotteryEmpty = function () {
    var $bettingList = $("#betting-list");
    $bettingList.empty();
    ArrayOrder.splice(0, ArrayOrder.length);
    CreateList();
}

function DelRow(i) {
    ArrayOrder.splice(i, 1);
    CreateList();
}

function ClearRow() {
    ArrayOrder.splice(0, ArrayOrder.length);
    CreateList();
}

function CreateList() {
    SumOrder = 0;
    SumCount = 0;
    SumTotal = 0;
    PriceTotal = 0;
    var bettingLine = '';
    for (k = 0; k < ArrayOrder.length; k++) {
        SumOrder += 1;
        SumCount += parseInt(ArrayOrder[k].Num);
        SumTotal += parseFloat(PriceTimes * parseInt(ArrayOrder[k].Num) * parseFloat(ArrayOrder[k].Price)) * PriceModel;
        PriceTotal += parseFloat(parseInt(ArrayOrder[k].Num) * parseFloat(ArrayOrder[k].Price)) * PriceModel;
        var strballs = ArrayOrder[k].balls;
        if (strballs.length > 20)
            strballs = strballs.substr(0, 20) + '...';
        bettingLine += "<li>";
        bettingLine += "<span class='number'>[" + PlayBigName + "-" + ArrayOrder[k].PlayName + "] " + strballs + "</span>";
        bettingLine += "<span class='zhu'>" + ArrayOrder[k].Num + " 注</span>";
        bettingLine += "<span class='multi'>" + ArrayOrder[k].times + " 倍</span>";
        bettingLine += "<span class='mode'>" + ArrayOrder[k].PriceName + "</span>";
        bettingLine += "<span class='money'><em>" + (ArrayOrder[k].alltotal).toFixed(2) + "</em> 元</span>";
        bettingLine += "<span class='oper'><a href='javascript:;' onclick='DelRow(" + k + ")' class='del'><i class='icon icon-del'></i></a></span>";
        bettingLine += "</li>";
    }
    var $bettingList = $("#betting-list");
    $bettingList.html(bettingLine);
    $("#sumOrder").html(SumOrder);
    $("#fromBuyNumberSumCount").html(SumCount);
    $("#fromBuyPriceSumTotal").html(SumTotal.toFixed(4));
}

//投注预览
function ajaxBetView() {
    if (checkBetTime() == false) {
        return false;
    }

    if (site.BetIsOpen == "1") {
        emAlert('系统正在维护不能投注！');
        return false;
    }
    if (SumCount == 0) {
        emAlert('请选择投注号码！');
        return false;
    }
    if (parseFloat(UserCurMoney) < parseFloat(SumTotal)) {
        emAlert('金额不足，不能投注！');
        return false;
    }
    var str = "";
    var strArray = PlayPos.split(',');
    for (var i = 0; i < strArray.length; i++) {
        if (strArray[i] == 1) {
            str = str + " " + i;
        }
    }
    var ws = str.replace("0", "万位").replace("1", "千位").replace("2", "百位").replace("3", "十位").replace("4", "个位");
    if (ws != "") {
        ws = "，任选位数：" + ws;
    }
    var strjson = "";
    var tzorderItems = "";
    for (var index in ArrayOrder) {
        if (ArrayOrder[index].PlayName != "undefined" && ArrayOrder[index].balls != "undefined") {
            tzorderItems += ArrayOrder[index].PlayName + ":" + ArrayOrder[index].balls + "<br/>";
            //strjson += JSON.stringify(ArrayOrder[index]) + ",";
        }
    }
    //strjson = stringformat("[" + strjson.substr(0, strjson.length - 1) + "]");
    strjson = JSON.stringify(ArrayOrder);
    if (tzorderItems.length > 50)
        tzorderItems = tzorderItems.substr(0, 50) + '...';
    var strmsg = "<table align='center' style='width:550px'><tr><td align='center' style='height:30px; font-size:14px;'>您确认要投注" + $i('nestsn').innerHTML + "吗？</td></tr>";
    strmsg += "<tr><td align='center' style='height:30px; font-size:14px;'>总注数：" + $i("fromBuyNumberSumCount").innerHTML + "注，总金额：" + $i("fromBuyPriceSumTotal").innerHTML + "元" + ws + "</td></tr>";
    strmsg += "<tr><td valign='top' style='font-size:14px;height:150px;'><hr/>" + tzorderItems + "</td></tr>"
    strmsg += "<tr><td align='left' style='font-size:14px;color:Red;height:30px;'>温馨提示：本平台每单最高奖金20万元，每期最高奖金40万元，请会员谨慎投注！</td></tr></table>"
    layer.open({
        title: '投注确认',
        content: strmsg
        , area: ['600px']
        , btn: ['确认投注', '取消投注']
        , yes: function (index, layero) {
            var index = emLoadingSubmit();
            setTimeout("Betting('" + strjson + "')", 100);
        }
        , cancel: function (index, layero) {
            ajaxAddAfterClear();
            ajaxBetAfterClear();
            CloseZhBet();
            PlayPos = "";
        }
    });
}

//一键投注
function ajaxQuickBetView() {
    if (checkBetTime() == false) {
        return false;
    }

    ReplaceBalls();
    if (site.BetIsOpen == "1") {
        emAlert('系统正在维护不能投注！');
        return false;
    }
    if (eval($("#txtUserPoint").val()) >= eval(site.MaxLevel) * 10) {
        closeload(index);
        emAlert('系统设定，返点大于 ' + site.MaxLevel + ' 的会员不能投注！');
        return false;
    }
    if (playPoints < 0 || eval($("#txtUserPoint").val()) < playPoints) {
        closeload(index);
        emAlert('返点错误，请重新选择！');
        return false;
    }
    if (PriceTimes <= 0) {
        closeload(index);
        emAlert('倍数不正确，请从新选择倍数！');
        return false;
    }
    if (parseFloat(PriceTimes) < parseFloat(LotteryMinTimes) || parseFloat(PriceTimes) > parseFloat(LotteryMaxTimes)) {
        closeload(index);
        emAlert('倍数必须大于' + parseInt(LotteryMinTimes) + '，且小于' + parseInt(LotteryMaxTimes));
        $('#fromTimes').val(parseInt(LotteryMinTimes));
        PriceTimes = parseInt(LotteryMinTimes);
        fromTimesChange();
        return false;
    }
    if (SingleTotal == 0) {
        closeload(index);
        emAlert('请选择投注号码!');
        return false;
    }
    if (Price == "" || Price == "0") {
        closeload(index);
        emAlert('圆角分错误，请从新选择圆角分！');
        return false;
    }
    if (SingleOrderItem == "") {
        closeload(index);
        emAlert('投注号码有错或为空！');
        return false;
    }
    if (PlayCode != "PK10_DD1_5" && PlayCode != "PK10_DD6_10" && PlayCode != "P_DD" && PlayCode != "P11_DD") {
        if (parseFloat(PlayMaxNum) < parseFloat(SingleCount)) {
            closeload(index);
            emAlert('注数不能大于' + PlayMaxNum + '注');
            return false;
        } 
    }
    if (parseFloat(UserCurMoney) < parseFloat(Price * SingleCount * PriceTimes * PriceModel)) {
        closeload(index);
        emAlert('金额不足，不能投注！');
        return false;
    }
    var index = emLoadingSubmit();
    BetDataOper();
    strjson = JSON.stringify(ArrayOrder);
    setTimeout("Betting('" + strjson + "')", 100);
}

function checkBetTime() {
    if (LotteryId != "1001") { //只处理重庆时时彩
        return true;
    }

    var allowBet = true;

    $.ajax({
        type: "get",
        dataType: "json",
        async: false,
        url: "/ajax/ajaxBetting.aspx?oper=ajaxCheckBetTime&id=" + LotteryId + "&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
        success: function (d) {
            switch (d.result) {
                case '0':
                    emAlert(d.returnval);
                    allowBet = false;
                    break;
                default:
                    break;
            }
        }
    });

    return allowBet;
}

function ReplaceBalls() {
    switch (PlayCode) {
        case "R_4DS":
        case "R_3DS":
        case "R_2DS":
        case "R_3HX":
        case "P_5DS":
        case "P_4DS_L":
        case "P_4DS_R":
        case "P_3DS_L":
        case "P_3DS_C":
        case "P_3DS_R":
        case "P_2DS_L":
        case "P_2DS_R":
        case "P_3HX_L":
        case "P_3HX_C":
        case "P_3HX_R":
            ReplaceNum();
            break;
        case "P11_RXDS_1":
        case "P11_RXDS_2":
        case "P11_RXDS_3":
        case "P11_RXDS_4":
        case "P11_RXDS_5":
        case "P11_RXDS_6":
        case "P11_RXDS_7":
        case "P11_RXDS_8":
        case "P11_3DS_L":
        case "P11_3ZDS_L":
        case "P11_2DS_L":
        case "P11_2ZDS_L":
            ReplaceNum2();
            break;
    }
}

function BetDataOper() {
    var json = {
        "LotteryId": LotteryId,
        "PlayId": PlayId,
        "Price": Price * 1,
        "PriceName": PriceName,
        "Times": PriceTimes,
        "Num": SingleCount,
        "SingelBouns": playBouns,
        "Point": playPoints,
        "Balls": SingleOrderItem,
        "StrPos": PlayPos,
        "PlayName": PlayName,
        "Alltotal": Price * SingleCount * PriceTimes * PriceModel
    };
    ArrayOrder.push(json);
}

function Betting(obj) {
    if (checkBetTime() == false) {
        return false;
    }

    obj = format(obj, true);
    $.ajax({
        type: "post",
        dataType: "json",
        data: obj,
        async: false,
        url: "/ajax/ajaxBetting.aspx?oper=ajaxBetting&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
        success: function (d) {
            switch (d.result) {
                case '0':
                    emAlert(d.returnval);
                    ajaxAddAfterClear();
                    ajaxBetAfterClear();
                    layer.closeAll('loading');
                    break;
                case '1':
                    ajaxRefresh();
                    emAlertSuccess(d.returnval);
                    ajaxAddAfterClear();
                    ajaxBetAfterClear();
                    PlayPos = "";
                    ajaxList();
                    layer.closeAll('loading');
                    break;
            }
        }
    });
}

function format(txt, compress/*是否为压缩模式*/) {/* 格式化JSON源码(对象转换为JSON文本) */
    var indentChar = '    ';
    if (/^\s*$/.test(txt)) {
        alert('数据为空,无法格式化! ');
        return;
    }
    try { var data = eval('(' + txt + ')'); }
    catch (e) {
        alert('数据源语法错误,格式化失败! 错误信息: ' + e.description, 'err');
        return;
    };
    var draw = [], last = false, This = this, line = compress ? '' : '\n', nodeCount = 0, maxDepth = 0;

    var notify = function (name, value, isLast, indent/*缩进*/, formObj) {
        nodeCount++; /*节点计数*/
        for (var i = 0, tab = ''; i < indent; i++) tab += indentChar; /* 缩进HTML */
        tab = compress ? '' : tab; /*压缩模式忽略缩进*/
        maxDepth = ++indent; /*缩进递增并记录*/
        if (value && value.constructor == Array) {/*处理数组*/
            draw.push(tab + (formObj ? ('"' + name + '":') : '') + '[' + line); /*缩进'[' 然后换行*/
            for (var i = 0; i < value.length; i++)
                notify(i, value[i], i == value.length - 1, indent, false);
            draw.push(tab + ']' + (isLast ? line : (',' + line))); /*缩进']'换行,若非尾元素则添加逗号*/
        } else if (value && typeof value == 'object') {/*处理对象*/
            draw.push(tab + (formObj ? ('"' + name + '":') : '') + '{' + line); /*缩进'{' 然后换行*/
            var len = 0, i = 0;
            for (var key in value) len++;
            for (var key in value) notify(key, value[key], ++i == len, indent, true);
            draw.push(tab + '}' + (isLast ? line : (',' + line))); /*缩进'}'换行,若非尾元素则添加逗号*/
        } else {
            if (typeof value == 'string') value = '"' + value + '"';
            draw.push(tab + (formObj ? ('"' + name + '":') : '') + value + (isLast ? '' : ',') + line);
        };
    };
    var isLast = true, indent = 0;
    notify('', data, isLast, indent, false);
    return draw.join('');
}



function CreateRandomRow() {
    CreateRandom();
    AddRow();
}

function CreateRandom() {
    ajaxAddAfterClear();
    var romCount = 0;
    var input = false;
    var input11 = false;
    var inputpk10 = false;
    switch (PlayCode) {
        case "P_5DS":
            romCount = 5;
            input = true;
            break;
        case "P_4DS_L":
        case "P_4DS_R":
        case "R_4DS":
            romCount = 4;
            input = true;
            break;
        case "P_3DS_L":
        case "P_3DS_C":
        case "P_3DS_R":
        case "P_3HX_L":
        case "P_3HX_C":
        case "P_3HX_R":
        case "R_3DS":
        case "R_3HX":
            romCount = 3;
            input = true;
            break;
        case "P_2DS_L":
        case "P_2DS_R":
        case "R_2DS":
            romCount = 2;
            input = true;
            break;
        case "P11_RXDS_1":
            romCount = 1;
            input11 = true;
            break;
        case "P11_RXDS_2":
            romCount = 2;
            input11 = true;
            break;
        case "P11_RXDS_3":
            romCount = 3;
            input11 = true;
            break;
        case "P11_RXDS_4":
            romCount = 4;
            input11 = true;
            break;
        case "P11_RXDS_5":
            romCount = 5;
            input11 = true;
            break;
        case "P11_RXDS_6":
            romCount = 6;
            input11 = true;
            break;
        case "P11_RXDS_7":
            romCount = 7;
            input11 = true;
            break;
        case "P11_RXDS_8":
            romCount = 8;
            input11 = true;
            break;
        case "P11_3DS_L":
        case "P11_3ZDS_L":
            romCount = 3;
            input11 = true;
            break;
        case "P11_2DS_L":
        case "P11_2ZDS_L":
            romCount = 2;
            input11 = true;
            break;
        case "PK10_TwoDS":
            romCount = 2;
            inputpk10 = true;
            break;
        case "PK10_ThreeDS":
            romCount = 3;
            inputpk10 = true;
            break;
    }
    if (input) {
        var balls = "";
        var roms = GetRandomNums(romCount, 0, 9);
        for (i = 0; i < roms.length; i++) {
            balls += roms[i] + "";
        }
        $("#inputtext").val(balls);
    }
    else if (input11) {
        var balls = "";
        var roms = GetRandomNums11(romCount, 1, 11);
        for (i = 0; i < roms.length; i++) {
            balls += roms[i] + " ";
        }
        balls = balls.substring(0, balls.length - 1);
        $("#inputtext").val(balls);
    }
    else if (inputpk10) {
        var balls = "";
        var roms = GetRandomNums11(romCount, 1, 10);
        for (i = 0; i < roms.length; i++) {
            balls += roms[i] + " ";
        }
        balls = balls.substring(0, balls.length - 1);
        $("#inputtext").val(balls);
    }
    else {
        var chars = [];
        var removeStr = GetRandomNum(0, 9);
        $.each($(".lottery-numbers li"), function (flag) {
            if (PlayCode == "P11_CDS") {
                $(this).find("span[number]").removeClass().addClass("big-ball J_Ball");
            }
            else {
                $(this).find("span[number]").removeClass().addClass("ball J_Ball");
            }
            var rom = 0;
            var romStr = "";
            var roms = [];
            if (Nmbtype == 1 || Nmbtype == 3) {
                rom = GetRandomNum(0, 9);
                romStr = rom;
                switch (PlayCode) {
                    case "P_5ZX120":
                        roms = GetRandomNums(5, 0, 9);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P_5ZX60":
                        if (flag == 0) {
                            $(this).find("span[number=" + removeStr + "]").addClass("selected");
                        }
                        if (flag == 1) {
                            roms = GetRandomNumsRemove(3, 0, 9, removeStr);
                            for (i = 0; i < roms.length; i++) {
                                $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                            }
                        }
                        break;
                    case "P_5ZX30":
                        if (flag == 0) {
                            roms = GetRandomNumsRemove(2, 0, 9, removeStr);
                            for (i = 0; i < roms.length; i++) {
                                $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                            }
                        }
                        if (flag == 1) {
                            $(this).find("span[number=" + removeStr + "]").addClass("selected");
                        }
                        break;
                    case "P_5ZX20":
                        if (flag == 0) {
                            $(this).find("span[number=" + removeStr + "]").addClass("selected");
                        }
                        if (flag == 1) {
                            roms = GetRandomNumsRemove(2, 0, 9, removeStr);
                            for (i = 0; i < roms.length; i++) {
                                $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                            }
                        }
                        break;
                    case "P_5ZX10":
                    case "P_5ZX5":
                    case "P_4ZX12":
                    case "P_4ZX4":
                        if (flag == 0) {
                            $(this).find("span[number=" + removeStr + "]").addClass("selected");
                        }
                        if (flag == 1) {
                            roms = GetRandomNumsRemove(1, 0, 9, removeStr);
                            for (i = 0; i < roms.length; i++) {
                                $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                            }
                        }
                        break;
                    case "P_4ZX24":
                        roms = GetRandomNums(4, 0, 9);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P_4ZX6":
                        roms = GetRandomNums(2, 0, 9);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P_3Z3_L":
                    case "P_3Z3_C":
                    case "P_3Z3_R":
                    case "R_3Z3":
                        var roms = GetRandomNums(2, 0, 9);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P_3Z6_L":
                    case "P_3Z6_C":
                    case "P_3Z6_R":
                    case "R_3Z6":
                        var roms = GetRandomNums(3, 0, 9);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P_2Z2_L":
                    case "P_2Z2_R":
                    case "R_2Z2":
                        var roms = GetRandomNums(2, 0, 9);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P_2DXDS_L":
                    case "P_2DXDS_R":
                        var rom = GetRandomNum(0, 3);
                        var strRom = "";
                        if (rom == 0)
                            strRom = "大";
                        if (rom == 1)
                            strRom = "小";
                        if (rom == 2)
                            strRom = "单";
                        if (rom == 3)
                            strRom = "双";
                        $(this).find("span[number=" + strRom + "]").addClass("selected");
                        break;
                    default:
                        $(this).find("span[number=" + romStr + "]").addClass("selected");
                        break;
                }
            }
            if (Nmbtype == 2) {
                switch (PlayCode) {
                    case "P11_CDS":
                        var rom = GetRandomNum(0, 5);
                        var strRom = "";
                        if (rom == 0)
                            strRom = "5单0双";
                        if (rom == 1)
                            strRom = "4单1双";
                        if (rom == 2)
                            strRom = "3单2双";
                        if (rom == 3)
                            strRom = "2单3双";
                        if (rom == 4)
                            strRom = "1单4双";
                        if (rom == 5)
                            strRom = "0单5双";
                        $(this).find("span[number=" + strRom + "]").addClass("selected");
                        break;
                    case "P11_CZW":
                        var rom = GetRandomNum(3, 9);
                        $(this).find("span[number=" + rom + "]").addClass("selected");
                        break;
                    case "P11_RXFS_1":
                        var roms = GetRandomNums11(1, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P11_RXFS_2":
                        var roms = GetRandomNums11(2, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P11_RXFS_3":
                        var roms = GetRandomNums11(3, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P11_RXFS_4":
                        var roms = GetRandomNums11(4, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P11_RXFS_5":
                        var roms = GetRandomNums11(5, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P11_RXFS_6":
                        var roms = GetRandomNums11(6, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P11_RXFS_7":
                        var roms = GetRandomNums11(7, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    case "P11_RXFS_8":
                        var roms = GetRandomNums11(8, 1, 11);
                        for (i = 0; i < roms.length; i++) {
                            $(this).find("span[number=" + roms[i] + "]").addClass("selected");
                        }
                        break;
                    default:
                        rom = GetRandomNumRemove(1, 11, chars);
                        if (rom < 10) {
                            romStr = "0" + rom;
                            chars.push(romStr);
                        }
                        else {
                            romStr = "" + rom;
                            chars.push(romStr);
                        }
                        $(this).find("span[number=" + romStr + "]").addClass("selected");
                        break;
                }
            }
            if (Nmbtype == 4) {
                switch (PlayCode) {
                    case "PK10_DXOne":
                    case "PK10_DXTwo":
                    case "PK10_DXThree":
                        var rom = GetRandomNum(0, 1);
                        var strRom = "";
                        if (rom == 0)
                            strRom = "大";
                        if (rom == 1)
                            strRom = "小";
                        $(this).find("span[number=" + strRom + "]").addClass("selected");
                        break;
                    case "PK10_DSOne":
                    case "PK10_DSTwo":
                    case "PK10_DSThree":
                        var rom = GetRandomNum(0, 1);
                        var strRom = "";
                        if (rom == 0)
                            strRom = "单";
                        if (rom == 1)
                            strRom = "双";
                        $(this).find("span[number=" + strRom + "]").addClass("selected");
                        break;
                    default:
                        rom = GetRandomNumRemove(1, 10, chars);
                        if (rom < 10) {
                            romStr = "0" + rom;
                            chars.push(romStr);
                        }
                        else {
                            romStr = "" + rom;
                            chars.push(romStr);
                        }
                        $(this).find("span[number=" + romStr + "]").addClass("selected");
                        break;
                }
            }
        });
    }
    AutoCalcBet();
}

function GetRandomNums(n, min, max) {
    var chars = [];
    for (var i = min; i < max; i++) {
        chars.push(i);
    }
    var arr = [];
    for (i = 0; i < n; i++) {
        var id = Math.ceil(Math.random() * 10);
        if (id > chars.length - 1)
            id = 0;
        arr.push(chars[id]);
        chars.splice(id, 1);
    }
    return arr;
}

function GetRandomNums11(n, min, max) {
    var chars = [];
    for (var i = min; i < max; i++) {
        if (i < 10)
            chars.push("0" + i);
        else
            chars.push(i);
    }
    var arr = [];
    for (i = 0; i < n; i++) {
        var id = Math.ceil(Math.random() * 10);
        if (id > chars.length - 1)
            id = 0;
        arr.push(chars[id]);
        chars.splice(id, 1);
    }
    return arr;
}

function GetRandomNumsRemove(n, min, max, removeStr) {
    var chars = [];
    for (var i = min; i < max; i++) {
        if (removeStr != i)
            chars.push(i);
    }

    var arr = [];
    for (i = 0; i < n; i++) {
        var id = Math.ceil(Math.random() * 10);
        if (id > chars.length - 1)
            id = 0;
        arr.push(chars[id]);
        chars.splice(id, 1);
    }
    return arr;
}

function GetRandomNum(Min, Max) {
    var Range = Max - Min;
    var Rand = Math.random();
    return (Min + Math.round(Rand * Range));
}

function GetRandomNumRemove(Min, Max, removeArr) {
    var chars = [];
    for (var i = Min; i < Max; i++) {
        if (removeArr.length == 0) {
            chars.push(i);
        }
        else {
            for (var j = 0; j < removeArr.length; j++) {
                if (removeArr[j] != i)
                    chars.push(i);
            }
        }
    }
    var id = Math.ceil(Math.random() * 10);
    if (id > chars.length - 1)
        id = 0;
    return chars[id];
}

function fmoney(s) {
    s = (s + "").replace(/[^\d\.-]/g, "") + "";
    var l = s.split(".")[0].split("").reverse();
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 1 == 0 && (i + 1) != l.length ? "," : "");
    }
    return t.split("").reverse().join("");
}

function fmoney2(s) {
    var l = s;
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 2 == 0 && (i + 1) != l.length ? " " : "");
    }
    return t.split("").reverse().join("");
}

function CheckReplace(arr) {
    var array = new Array();
    var hash = {};
    for (var i in arr) {
        if (hash[arr[i]]) {
            array.push(arr[i]);
        }
        hash[arr[i]] = true;
    }
    return array;
}

//过滤重复号码
function ReplaceNum() {

    var number = $('#inputtext').val();
    var str = number.toString();
    if (PlayCode == "P_3HX_L" || PlayCode == "P_3HX_C" || PlayCode == "P_3HX_R" || PlayCode == "R_3HX"
    || PlayCode == "P_3Z3DS_L" || PlayCode == "P_3Z3DS_C" || PlayCode == "P_3Z3DS_R" || PlayCode == "R_3Z3DS"
    || PlayCode == "P_3Z6DS_L" || PlayCode == "P_3Z6DS_C" || PlayCode == "P_3Z6DS_R" || PlayCode == "R_3Z6DS") {
        str = str.replace(/000/g, "").replace(/111/g, "").replace(/222/g, "").replace(/333/g, "").replace(/444/g, "").replace(/555/g, "").replace(/666/g, "").replace(/777/g, "").replace(/888/g, "").replace(/999/g, "");
    }
    str = str.replace(/(^\s*)(\s*$)/g, "").replace(/,/g, " "); //.replace(/\n/g, "");
    str = str.replace(/\n/g, ",");
    str = str.replace(/ /g, ",");
    //注内排序
    var list = str.split(",");
    var arr = new Array();
    if (PlayCode == "P_3HX_L" || PlayCode == "P_3HX_C" || PlayCode == "P_3HX_R" || PlayCode == "R_3HX") {
        for (var k = 0; list.length > k; k++) {
            if (list[k] != "") {
                var nums = fmoney(list[k]).split(",");
                nums = nums.sort(function (a, b) {
                    return a - b;
                });
                arr.push(nums.toString().replace(/,/g, ""));
            }
        }
    }
    else if (PlayCode == "P_3Z3DS_L" || PlayCode == "P_3Z3DS_C" || PlayCode == "P_3Z3DS_R" || PlayCode == "R_3Z3DS") {
        for (var k = 0; list.length > k; k++) {
            if (list[k] != "") {
                var nums = fmoney(list[k]).split(",");
                nums = nums.sort(function (a, b) {
                    return a - b;
                });
                if (nums[0] == nums[1] || nums[0] == nums[2] || nums[1] == nums[2]) {
                    arr.push(nums.toString().replace(/,/g, ""));
                }
            }
        }
    }
    else if (PlayCode == "P_3Z6DS_L" || PlayCode == "P_3Z6DS_C" || PlayCode == "P_3Z6DS_R" || PlayCode == "R_3Z6DS") {
        for (var k = 0; list.length > k; k++) {
            if (list[k] != "") {
                var nums = fmoney(list[k]).split(",");
                nums = nums.sort(function (a, b) {
                    return a - b;
                });
                if (nums[0] != nums[1] && nums[0] != nums[2] && nums[1] != nums[2]) {
                    arr.push(nums.toString().replace(/,/g, ""));
                }
            }
        }
    }
    else {
        arr = list;
    }

    //去除重复
    var repstr = CheckReplace(arr);
    if (repstr != "") {
        var temp = repstr.length > 10 ? repstr.toString().substring(0, 10) + " ..." : repstr.toString();
        emAlert("重复号码系统自动过滤！");
        arr = arr.sort(function (a, b) {
            return a - b;
        });
        var arr2 = new Array();
        var hash2 = {};
        for (var i in arr) {
            if (!hash2[arr[i]]) {
                if (arr[i] != "") {
                    arr2.push(arr[i]);
                }
            }
            hash2[arr[i]] = true;
        }
        var after = arr2.toString().replace(/(^\s*)(\s*$)/g, "").replace(/,/g, " ").replace(/\n/g, "");
        after = after.replace(/  /g, " ");
        $('#inputtext').val(after);
        Znum = RedDS(after);
        //重新计算
        $i("fromBuyNumberCount").innerHTML = SingleCount = Znum;
        fromTimesChange();
        AutoCalcBet();
    }
    else {
        arr = arr.sort(function (a, b) {
            return a - b;
        });
        for (var k = 0; arr.length > k; k++) {
            if (arr[k] == "") {
                arr.splice(k, 1);
            }
        }
        var after = arr.toString().replace(/(^\s*)(\s*$)/g, "").replace(/,/g, " ").replace(/\n/g, "");
        after = after.replace(/  /g, " ");
        $('#inputtext').val(after);
        Znum = RedDS(after);
        //重新计算
        $i("fromBuyNumberCount").innerHTML = SingleCount = Znum;
        fromTimesChange();
        AutoCalcBet();
    }
}

function CheckReplace11(number) {
    if (number == "") {
        return;
    }
    var str = number.toString().replace(/(^\s*)(\s*$)/g, "");
    //str = str.replace(/ /g, ",");
    //判断重复
    var array = new Array();
    var list = str.split(",");
    for (var k = 0; list.length > k; k++) {
        if (list[k] == "") {
            list.splice(k, 1);
        }
    }
    for (var i = 0; i < list.length; i++) {
        var s = list[i];
        if (array.indexOf(s) == -1) {
            if (check(str, s)) {
                array.push(list[i]);
            }
        }
    }
    return array;
}
function CheckReplace12(number) {
    if (number == "") {
        return;
    }
    var str = number.toString().replace(/(^\s*)(\s*$)/g, "");
    //str = str.replace(/ /g, ",");
    //判断重复
    var array = new Array();
    var list = str.split(" ");
    for (var k = 0; list.length > k; k++) {
        if (list[k] == "") {
            list.splice(k, 1);
        }
    }
    for (var i = 0; i < list.length; i++) {
        var s = list[i];
        if (array.indexOf(s) == -1) {
            if (check(str, s)) {
                array.push(i + 1, list[i]);
            }
        }
    }
    return array;
}
//过滤重复号码
function ReplaceNum2() {
    var number = $('#inputtext').val();
    var str = number.toString().replace(/(^\s*)(\s*$)/g, "");
    str = str.replace(/\n/g, ",");

    //注内排序
    var arr = str.split(",");
    var list = new Array();
    if (PlayCode == "P11_3ZDS_L" || PlayCode == "P11_2ZDS_L" || PlayCode == "P11_RXDS_1" || PlayCode == "P11_RXDS_2" || PlayCode == "P11_RXDS_3"
    || PlayCode == "P11_RXDS_4" || PlayCode == "P11_RXDS_5" || PlayCode == "P11_RXDS_6" || PlayCode == "P11_RXDS_7" || PlayCode == "P11_RXDS_8") {
        for (var k = 0; arr.length > k; k++) {
            if (list[k] != "") {
                var nums = arr[k].split(" ");
                nums = nums.sort(function (a, b) {
                    return a - b;
                });
                var afternums = new Array();
                if (nums.length == 1) {
                    afternums.push(nums[0].toString());
                }
                if (nums.length == 2) {
                    afternums.push(nums[0].toString());
                    afternums.push(" ");
                    afternums.push(nums[1].toString());
                }
                if (nums.length == 3) {
                    afternums.push(nums[0].toString());
                    afternums.push(" ");
                    afternums.push(nums[1].toString());
                    afternums.push(" ");
                    afternums.push(nums[2].toString());
                }
                if (nums.length == 4) {
                    afternums.push(nums[0].toString());
                    afternums.push(" ");
                    afternums.push(nums[1].toString());
                    afternums.push(" ");
                    afternums.push(nums[2].toString());
                    afternums.push(" ");
                    afternums.push(nums[3].toString());
                }
                if (nums.length == 5) {
                    afternums.push(nums[0].toString());
                    afternums.push(" ");
                    afternums.push(nums[1].toString());
                    afternums.push(" ");
                    afternums.push(nums[2].toString());
                    afternums.push(" ");
                    afternums.push(nums[3].toString());
                    afternums.push(" ");
                    afternums.push(nums[4].toString());
                }
                if (nums.length == 6) {
                    afternums.push(nums[0].toString());
                    afternums.push(" ");
                    afternums.push(nums[1].toString());
                    afternums.push(" ");
                    afternums.push(nums[2].toString());
                    afternums.push(" ");
                    afternums.push(nums[3].toString());
                    afternums.push(" ");
                    afternums.push(nums[4].toString());
                    afternums.push(" ");
                    afternums.push(nums[5].toString());
                }
                if (nums.length == 7) {
                    afternums.push(nums[0].toString());
                    afternums.push(" ");
                    afternums.push(nums[1].toString());
                    afternums.push(" ");
                    afternums.push(nums[2].toString());
                    afternums.push(" ");
                    afternums.push(nums[3].toString());
                    afternums.push(" ");
                    afternums.push(nums[4].toString());
                    afternums.push(" ");
                    afternums.push(nums[5].toString());
                    afternums.push(" ");
                    afternums.push(nums[6].toString());
                }
                if (nums.length == 8) {
                    afternums.push(nums[0].toString());
                    afternums.push(" ");
                    afternums.push(nums[1].toString());
                    afternums.push(" ");
                    afternums.push(nums[2].toString());
                    afternums.push(" ");
                    afternums.push(nums[3].toString());
                    afternums.push(" ");
                    afternums.push(nums[4].toString());
                    afternums.push(" ");
                    afternums.push(nums[5].toString());
                    afternums.push(" ");
                    afternums.push(nums[6].toString());
                    afternums.push(" ");
                    afternums.push(nums[7].toString());
                }
                list.push(afternums.toString().replace(/,/g, ""));
            }
        }
    }
    else {
        list = arr;
    }

    //去除重复
    var repstr = CheckReplace11(number);
    if (repstr != "") {
        var temp = repstr.length > 10 ? repstr.substring(0, 10) + " ..." : repstr;
        emAlert("重复号码系统自动过滤！");
        var arr = str.split(",");
        for (var i = 0; arr.length > i; i++) {
            for (var j = i + 1; j < arr.length; j++) {
                if (arr[j] == arr[i]) {
                    arr.splice(j, 1);
                    j--;
                }
            }
        }
        //排序
        var temp;
        for (var i = 0; i < list.length; i++) {
            for (var j = 0; j < i; j++) {
                if (list[j].split(" ").length == 1) {
                    if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                        temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                    }
                }
                if (list[j].split(" ").length == 2) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
                if (list[j].split(" ").length == 3) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) == parseInt(list[i].split(" ")[1])) {
                            if (parseInt(list[j].split(" ")[2]) > parseInt(list[i].split(" ")[2])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                        else {
                            if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
                if (list[j].split(" ").length == 4) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) == parseInt(list[i].split(" ")[1])) {
                            if (parseInt(list[j].split(" ")[2]) == parseInt(list[i].split(" ")[2])) {
                                if (parseInt(list[j].split(" ")[3]) > parseInt(list[i].split(" ")[3])) {
                                    temp = list[j];
                                    list[j] = list[i];
                                    list[i] = temp;
                                }
                            }
                            else {
                                if (parseInt(list[j].split(" ")[2]) > parseInt(list[i].split(" ")[2])) {
                                    temp = list[j];
                                    list[j] = list[i];
                                    list[i] = temp;
                                }
                            }
                        }
                        else {
                            if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
                if (list[j].split(" ").length == 5) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) == parseInt(list[i].split(" ")[1])) {
                            if (parseInt(list[j].split(" ")[2]) == parseInt(list[i].split(" ")[2])) {
                                if (parseInt(list[j].split(" ")[3]) == parseInt(list[i].split(" ")[3])) {
                                    if (parseInt(list[j].split(" ")[4]) > parseInt(list[i].split(" ")[4])) {
                                        temp = list[j];
                                        list[j] = list[i];
                                        list[i] = temp;
                                    }
                                }
                                else {
                                    if (parseInt(list[j].split(" ")[3]) > parseInt(list[i].split(" ")[3])) {
                                        temp = list[j];
                                        list[j] = list[i];
                                        list[i] = temp;
                                    }
                                }
                            }
                            else {
                                if (parseInt(list[j].split(" ")[2]) > parseInt(list[i].split(" ")[2])) {
                                    temp = list[j];
                                    list[j] = list[i];
                                    list[i] = temp;
                                }
                            }
                        }
                        else {
                            if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
            }
            if (CheckReplace12(list[i]) != "") {
                list.splice(i, 1);
            }
        }

        var after = arr.toString().replace(/(^\s*)(\s*$)/g, "");
        after = after.replace(/  /g, " ");
        $('#inputtext').val(after);
        Znum = RedDS(after);
        //重新计算
        $i("fromBuyNumberCount").innerHTML = SingleCount = Znum;
        fromTimesChange();
        AutoCalcBet();
    }
    else {
        //排序
        var temp;
        for (var i = 0; i < list.length; i++) {
            for (var j = 0; j < i; j++) {
                if (list[j].split(" ").length == 1) {
                    if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                        temp = list[j];
                        list[j] = list[i];
                        list[i] = temp;
                    }
                }
                if (list[j].split(" ").length == 2) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
                if (list[j].split(" ").length == 3) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) == parseInt(list[i].split(" ")[1])) {
                            if (parseInt(list[j].split(" ")[2]) > parseInt(list[i].split(" ")[2])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                        else {
                            if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
                if (list[j].split(" ").length == 4) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) == parseInt(list[i].split(" ")[1])) {
                            if (parseInt(list[j].split(" ")[2]) == parseInt(list[i].split(" ")[2])) {
                                if (parseInt(list[j].split(" ")[3]) > parseInt(list[i].split(" ")[3])) {
                                    temp = list[j];
                                    list[j] = list[i];
                                    list[i] = temp;
                                }
                            }
                            else {
                                if (parseInt(list[j].split(" ")[2]) > parseInt(list[i].split(" ")[2])) {
                                    temp = list[j];
                                    list[j] = list[i];
                                    list[i] = temp;
                                }
                            }
                        }
                        else {
                            if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
                if (list[j].split(" ").length == 5) {
                    if (parseInt(list[j].split(" ")[0]) == parseInt(list[i].split(" ")[0])) {
                        if (parseInt(list[j].split(" ")[1]) == parseInt(list[i].split(" ")[1])) {
                            if (parseInt(list[j].split(" ")[2]) == parseInt(list[i].split(" ")[2])) {
                                if (parseInt(list[j].split(" ")[3]) == parseInt(list[i].split(" ")[3])) {
                                    if (parseInt(list[j].split(" ")[4]) > parseInt(list[i].split(" ")[4])) {
                                        temp = list[j];
                                        list[j] = list[i];
                                        list[i] = temp;
                                    }
                                }
                                else {
                                    if (parseInt(list[j].split(" ")[3]) > parseInt(list[i].split(" ")[3])) {
                                        temp = list[j];
                                        list[j] = list[i];
                                        list[i] = temp;
                                    }
                                }
                            }
                            else {
                                if (parseInt(list[j].split(" ")[2]) > parseInt(list[i].split(" ")[2])) {
                                    temp = list[j];
                                    list[j] = list[i];
                                    list[i] = temp;
                                }
                            }
                        }
                        else {
                            if (parseInt(list[j].split(" ")[1]) > parseInt(list[i].split(" ")[1])) {
                                temp = list[j];
                                list[j] = list[i];
                                list[i] = temp;
                            }
                        }
                    }
                    else {
                        if (parseInt(list[j].split(" ")[0]) > parseInt(list[i].split(" ")[0])) {
                            temp = list[j];
                            list[j] = list[i];
                            list[i] = temp;
                        }
                    }
                }
            }
            if (CheckReplace12(list[i]) != "") {
                list.splice(i, 1);
            }
        }

        var after = list.toString().replace(/(^\s*)(\s*$)/g, "");
        after = after.replace(/  /g, " ");
        $('#inputtext').val(after);
        Znum = RedDS(after);
        //重新计算
        $i("fromBuyNumberCount").innerHTML = SingleCount = Znum;
        fromTimesChange();
        AutoCalcBet();
    }
}

//自定义方法showElement()
function showElement(id) {
    document.getElementById(id).style.display = "";
}

//自定义方法hideElement()
function hideElement(id) {
    document.getElementById(id).style.display = "none";
}

//走势图
function ajaxChart() {
    window.open('/' + LotteryId + '/30');
}