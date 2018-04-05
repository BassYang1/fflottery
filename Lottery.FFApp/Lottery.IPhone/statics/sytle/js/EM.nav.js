$(document).ready(function () {
    Nmbtype = $("#txtTid").val();
    LotteryId = $("#txtLoid").val();
    lid = "&lid=" + LotteryId;
    LotteryMinTimes = $("#MinTimes").val();
    LotteryMaxTimes = $("#MaxTimes").val();
    $("#add").show();
    $("#info").hide();
    $("#zhuihao").hide();
    if (LotteryId >= 1001 && LotteryId <= 1013) {
        $("#J_Lottery").removeClass().addClass("lt-lottery lottery-ssc");
    }
    if (LotteryId >= 2001 && LotteryId <= 2006) {
        $("#J_Lottery").removeClass().addClass("lt-lottery lottery-11x5");
    }
    if (LotteryId >= 3001 && LotteryId <= 3005) {
        $("#J_Lottery").removeClass().addClass("lt-lottery lottery-qtc");
    }
    if (getCookie("price") != null) {
        $("#model").val(getCookie("price"));
    }
    else {
        $("#model").val("1");
    }

    //验证用户
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { emAlert("亲！页面过期,请刷新页面!"); } },
        success: function (d) {
            if (d.result == "1") {
                //显示当前开奖信息，时间等
                ajaxLotteryTime();
                //加载玩法大类
                ajaxBigType();
                if (site.ZHIsOpen == 1) {
                    $(".iszh").hide();
                }
                else {
                    if (LotteryId == 1006) {
                        $(".iszh").hide();
                    }
                    else {
                        $(".iszh").show();
                    }
                }
            }
            else {
                window.location.href = '/login.html';
            }
        }
    });
    //添加后清空
    ajaxAddAfterClear();
    //投注后清空
    ajaxBetAfterClear();

    //设置彩种投注倍数
    setBetTimes(false);
});

//加载前10期开奖信息
function ajaxListNav() {
    var u = "/ajax/ajax.aspx?oper=GetKaiJiangInfoTop10";
    //var index = emLoading();
    $.ajax({
        type: "get",
        dataType: "json",
        data: "&clienttime=" + Math.random(),
        url: u + lid,
        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
        success: function (d) {
            switch (d.result) {
                case '1':
                    if (d.table.length > 0) {
                        var html = '';
//                        html += '<div class="lottery-last">';
//                        html += '<div class="issue">第<em>' + d.table[0].title + '</em>期</div>';
//                        html += '<div class="number">';
//                        html += GetSytle(d.table[0].number);
//                        html += '</div>';
//                        html += '</div>';
//                        html += '<ul class="lottery-records J_LotteryRecords">';
                        for (var i = 0; i < d.table.length; i++) {

                            html += '<li>';
                            html += '<span class="issue">第<em>' + d.table[i].title + '</em>期</span>';
                            html += '<span class="number">' + d.table[i].number + '</span>';
                            html += '</li>';
                        }
//                        html += '</ul>';
                        $("#ajaxListNav").html(html);
                    }
                    break;
            }
            //closeload(index);
        }
    });
}

function ajaxNewsTop() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxNews.aspx?oper=ajaxGetNewsTop1",
        success: function (data) {
            if (data.result == "1") {
                emAlert3(data.table[0].content);
            }
        }
    });
}

function fmoney(s) {
    s = (s + "").replace(/[^\d\.-]/g, "")+ "";
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
    if (PlayCode == "P_3HX_L" || PlayCode == "P_3HX_C" || PlayCode == "P_3HX_R" || PlayCode == "R_3HX") {
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
    else {
        arr = list;
    }

    //去除重复
    var repstr = CheckReplace(arr);
    if (repstr != "") {
        var temp = repstr.length > 10 ? repstr.toString().substring(0, 10) + " ..." : repstr.toString();
        emAlert(temp + "等号码重复，系统自动过滤！");
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
        emAlert(temp + "等号码重复，系统自动过滤！");
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
    $('#inputtext').val("");
}

//投注后清空
function ajaxBetAfterClear() {
    ClearRow();
    $i("fromBuyNumberSumCount").innerHTML = '0';
    $i("fromBuyPriceSumTotal").innerHTML = '0.0000';
    $i("sumOrder").innerHTML = '0';

    SumCount = 0;
    SumTotal = 0;
    PriceTotal = 0;
    SumOrder = 0;
    ArrayOrder.length = 0;
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
    window.open('/' + LotteryId+'/30');
}