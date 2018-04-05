function SelectRow() {
    $("#add").show();
    $("#info").hide();
    $("#zhuihao").hide();
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

function AddRow() {
    if (checkBetTime() == false) {
        return false;
    }

    if (site.BetIsOpen == "1") {
        emAlert('系统正在维护不能投注！');
        return false;
    }
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
            //ReplaceNum();
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
            //ReplaceNum2();
            break;
    }
    if (eval($("#txtUserPoint").val()) >= eval(site.MaxLevel) * 10) {
        emAlert('系统设定，返点大于 ' + site.MaxLevel + ' 的会员不能投注！');
        return false;
    }
    if (Betpoint == 0) {
        emAlert('返点错误，请重新选择！');
        return false;
    }
    if ($("#fromTimes").attr("value") == "" || $("#fromTimes").attr("value") == "0" || PriceTimes == 0) {
        emAlert('请输入倍数！');
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
    $("#add").hide();
    $("#info").show();
    $("#zhuihao").hide();

    var bonus = Betpoint.split('/');
    var json = {
        "LotteryId": LotteryId,
        "PlayId": PlayId,
        "Price": Price * 1,
        "times": PriceTimes,
        "Num": SingleCount,
        "singelBouns": bonus[1],
        "Point": bonus[0],
        "balls": SingleOrderItem,
        "strPos": PlayPos,
        "PlayName": PlayName,
        "alltotal": Price * SingleCount * PriceTimes * 2 * parseFloat(PricePos)
    };
    ArrayOrder.push(json);
    CreateList();
    ajaxAddAfterClear();
    //设置彩种投注倍数
    setBetTimes(true);
}

//设置彩种投注倍数
function setBetTimes(updated) {
    var lotId = LotteryId;
    var times = $("#fromTimes").val();
    times = times != undefined && times != null && times != "" && !isNaN(times) ? times : "1";

    var cookieId, cookieTimes;
    var cookieVal = getCookie("mbLotFromTimes");
    if (cookieVal != undefined && cookieVal != null && cookieVal != "") {
        var arr = cookieVal.split(":");
        cookieId = arr.length > 0 ? arr[0] : "";
        cookieTimes = arr.length > 1 ? arr[1] : "1";

        if (cookieId != lotId) { //更新彩种
            cookieId = lotId;
            cookieTimes = "1";
        }
        else if(updated == true){
            cookieTimes = times;
        }
    }
    else {
        cookieId = lotId;
        cookieTimes = "1";
    }

    $("#fromTimes").val(cookieTimes);

    //记录投注倍数
    delCookie("mbLotFromTimes");
    setCookie("mbLotFromTimes", cookieId + ":" + cookieTimes);
}

function DelRow(i, number, total) {
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
    var str = "";
    for (k = 0; k < ArrayOrder.length; k++) {
        var balls = ArrayOrder[k].balls.length > 15 ? ArrayOrder[k].balls.substring(0, 15) + "..." : ArrayOrder[k].balls.substring(0, ArrayOrder[k].balls.length);
        str += '<li>';
        str += '<a href="#">';
        str += '<span class="close" onclick="DelRow(' + k + ',' + ArrayOrder[k].Num + ',' + ArrayOrder[k].alltotal + ');"></span>';
        str += '<span class="info">';
        str += '<span class="number">' + balls + '</span>';
        str += '<span class="type">' + ArrayOrder[k].PlayName + '</span>';
        str += '</span>';
        str += '<span class="money">' + ArrayOrder[k].Num + '注,<em>' + ArrayOrder[k].alltotal.toFixed(4) + '</em>元</span>';
        str += '</a>';
        str += '</li>';

        SumOrder += 1;
        SumCount += parseInt(ArrayOrder[k].Num);
        SumTotal += parseFloat(PriceTimes * parseInt(ArrayOrder[k].Num) * Price * 2 * parseFloat(PricePos));
        PriceTotal += parseFloat(parseInt(ArrayOrder[k].Num) * Price * 2 * parseFloat(PricePos));
    }
    $("#ajaxList").html(str);
    $("#sumOrder").html(SumOrder);
    $("#fromBuyNumberSumCount").html(SumCount);
    $("#fromBuyPriceSumTotal").html(SumTotal.toFixed(4));
    //$("#fromBuyTotal").html(PriceTotal);
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
        $.each($(".numbers .lottery-balls"), function (flag) {
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
        emAlert('注数错误，请重新选择号码！');
        return false;
    }

    if (parseFloat(UserMoney) < parseFloat(SumTotal)) {
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
            tzorderItems += ArrayOrder[index].PlayName + ":" + ArrayOrder[index].balls + "\n";
            strjson += JSON.stringify(ArrayOrder[index]) + ",";
        }
    }

    strjson = stringformat("[" + strjson.substr(0, strjson.length - 1) + "]");

    var index = emLoading();
    $.ajax({
        type: "post",
        dataType: "json",
        data: strjson,
        async: false,
        url: "/ajax/ajaxBetting.aspx?oper=ajaxBetting&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
        success: function (d) {
            switch (d.result) {
                case '-1':
                    emAlert(d.returnval);
                    top.window.location = '/login.html';
                    break;
                case '0':
                    emAlert(d.returnval);
                    closeload(index);
                    break;
                case '1':
                    var tzmoney = $("#fromBuyPriceSumTotal").html();
                    ajaxAddAfterClear();
                    ajaxBetAfterClear();
                    PlayPos = "";
                    closeload(index);
                    top.window.location = '/statics/html/betSuccess.html?lid=' + LotteryId + '&cur=' + parseFloat(tzmoney).toFixed(4) + '&money=' + parseFloat(UserMoney - parseFloat(tzmoney)).toFixed(4);
                    break;
            }
        }
    });
}

//一键投注
function ajaxQuickBetView() {
    if (checkBetTime() == false) {
        return false;
    }

    if (site.BetIsOpen == "1") {
        emAlert('系统正在维护不能投注！');
        return false;
    }
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
            //ReplaceNum();
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
            //ReplaceNum2();
            break;
    }
    if (eval($("#txtUserPoint").val()) >= eval(site.MaxLevel) * 10) {
        emAlert('系统设定，返点大于 ' + site.MaxLevel + ' 的会员不能投注！');
        return false;
    }
    if (Betpoint == 0) {
        emAlert('返点错误，请重新选择！');
        return false;
    }
    if ($("#fromTimes").attr("value") == "" || $("#fromTimes").attr("value") == "0" || PriceTimes == 0) {
        emAlert('请输入倍数！');
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

    //组装下注数据
    var bonus = Betpoint.split('/');
    var json = {
        "LotteryId": LotteryId,
        "PlayId": PlayId,
        "Price": Price * 1,
        "times": PriceTimes,
        "Num": SingleCount,
        "singelBouns": bonus[1],
        "Point": bonus[0],
        "balls": SingleOrderItem,
        "strPos": PlayPos,
        "PlayName": PlayName,
        "alltotal": Price * SingleCount * PriceTimes * 2 * parseFloat(PricePos)
    };

    ArrayOrder.push(json);
    CreateList();

    if (checkBetTime() == false) {
        return false;
    }

    if (site.BetIsOpen == "1") {
        emAlert('系统正在维护不能投注！');
        return false;
    }

    if (SumCount == 0) {
        emAlert('注数错误，请重新选择号码！');
        return false;
    }

    if (parseFloat(UserMoney) < parseFloat(SumTotal)) {
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
            tzorderItems += ArrayOrder[index].PlayName + ":" + ArrayOrder[index].balls + "\n";
            strjson += JSON.stringify(ArrayOrder[index]) + ",";
        }
    }

    strjson = stringformat("[" + strjson.substr(0, strjson.length - 1) + "]");

    var index = emLoading();
    $.ajax({
        type: "post",
        dataType: "json",
        data: strjson,
        async: false,
        url: "/ajax/ajaxBetting.aspx?oper=ajaxBetting&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
        success: function (d) {
            switch (d.result) {
                case '-1':
                    emAlert(d.returnval);
                    top.window.location = '/login.html';
                    break;
                case '0':
                    emAlert(d.returnval);
                    closeload(index);
                    break;
                case '1':
                    var tzmoney = $("#fromBuyPriceSumTotal").html();
                    ajaxAddAfterClear();
                    ajaxBetAfterClear();
                    PlayPos = "";
                    closeload(index);
                    emAlert('<font color="white">一键下注成功，请继续下注！</fond>');
                    break;
            }
        }
    });

    ajaxAddAfterClear();
    $(".numbers .lottery-balls").find("span.selected").removeClass("selected");

    //设置彩种投注倍数
    setBetTimes(true);
}
