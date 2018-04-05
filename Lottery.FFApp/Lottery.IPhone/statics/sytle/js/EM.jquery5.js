function OpenZhBet() {
    if (checkBetTime() == false) {
        return false;
    }

    $("#add").hide();
    $("#info").hide();
    $("#zhuihao").show();
    var html = "";
    html += '<div class="form-item">';
    html += '<div class="item-title"><label class="lab">起始倍数</label></div><div class="item-content"><input id="txtStartTimes" type="text" value="1" class="ipt" />';
    html += '</div></div>';
    html += '<div class="form-item">';
    html += '<div class="item-title"><label class="lab">追号期数</label></div><div class="item-content"><input id="txtNums" type="text" value="5" class="ipt" /><span class="v">期</span>';
    html += '</div></div>';
    $i("zhuihaoQuery").innerHTML = html;
    $('.lottery-append-tabs .tabs-nav li').click(function () {
        $(this).parent().find('li').removeClass();
        $(this).addClass('active');
        var nmb = $(this).attr("nmb");
        NmbZH = nmb;
        if (nmb == 1) {
            var html = "";
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">起始倍数</label></div><div class="item-content"><input id="txtStartTimes" type="text" value="1" class="ipt" />';
            html += '</div></div>';
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">追号期数</label></div><div class="item-content"><input id="txtNums" type="text" value="5" class="ipt" /><span class="v">期</span>';
            html += '</div></div>';
            $i("zhuihaoQuery").innerHTML = html;
        }
        if (nmb == 2) {
            var html = "";
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">隔</label></div><div class="item-content"><input id="txtJiange" type="text" value="1" class="ipt"/>期';
            html += '</div></div>';
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">倍×</label></div><div class="item-content"><input id="txtTimes" type="text" value="2" class="ipt"/>';
            html += '</div></div>';
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">追号期数</label></div><div class="item-content"><input id="txtNums" type="text" value="5" class="ipt"/>期';
            html += '</div></div>';
            $i("zhuihaoQuery").innerHTML = html;
        }
        if (nmb == 3) {
            var html = "";
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">起始倍数</label></div><div class="item-content"><input id="txtStartTimes" type="text" value="1" class="ipt"/>';
            html += '</div></div>';
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">最低收益率</label></div><div class="item-content"><input id="txtPer" type="text" value="50" class="ipt"/>%';
            html += '</div></div>';
            html += '<div class="form-item">';
            html += '<div class="item-title"><label class="lab">追号期数</label></div><div class="item-content"><input id="txtNums" type="text" value="5" class="ipt"/>期';
            html += '</div></div>';
            $i("zhuihaoQuery").innerHTML = html;
        }
        for (var i = 0; i < ZhSumCount; i++) {
            if ($("#issues_" + i).parent().hasClass("checkbox-selected")) {
                $("#issues_" + i).parent().removeClass("checkbox-selected");
                $("#issues_" + i).attr("checked", false);
                $i("times_" + i).value = 0;
                $i("money_" + i).innerHTML = (PriceTotal * 0).toFixed(2);
            }
        }
    });
    ajaxIssueNum(0);
}

//隐藏追号
function CloseZhBet() {
    $("#add").hide();
    $("#info").show();
    $("#zhuihao").hide();
}

//加载期号
var sunNum = 0;
function ajaxIssueNum(obj) {
    var u = "flag=0&clienttime=" + Math.random();
    if (obj == 1) {
        u = "flag=1&clienttime=" + Math.random();
    }
    $.ajax({
        type: "get",
        dataType: "json",
        data: u,
        url: "/ajax/ajaxBet.aspx?oper=ajaxZHIssueNum" + lid,
        success: function (data) {
            if (data.result == "1") {
                ZhSumCount = data.totalcount;
                var html = "";
                for (i = 0; i < data.table.length; i++) {
                    html += '<li>';
                    html += '<input onclick="chkChange(' + i + ')" type="checkbox" id="issues_' + i + '" class="chk"/>';
                    html += '<div>';
                     html += '<span class="issue">第 <span id="sn_' + i + '">' + data.table[i].sn + '</span> 期</span>';
                    html += '<span class="times"><input onkeyup="TimesChange(' + i + ')" onkeypress="chkPrice(this)" maxlength="5" size="5" id="times_' + i + '" value="' + data.table[i].count + '" class="ipt n"/>倍</span>';
                    html += '</div>';
                    html += '<div>';
                    html += '<span class="money">金额：<em id="money_' + i + '">' + data.table[i].price + '</em></span>';
                    html += '<span class="time">截止：<span id="stime_' + i + '">' + data.table[i].stime + '</span></span>';
                    html += '</div>';
                    html += '</li>';
                }
                $i("zhlist").innerHTML = html;
            }
        }
    });
    CalculateZh();

    SumZhCount = 0;
    SumZhTotal = 0;
}

//生成追号计划
function generateZH() {
    if (SumCount == 0) {
        emAlert('注数错误，请重新选择号码！');
        return false;
    }
    for (var i = 0; i < ZhSumCount; i++) {
        $i("issues_" + i).checked = false;
        $i("times_" + i).value = "0";
        $i("money_" + i).innerHTML = "0.00";
    }
    if (NmbZH == 1) {
        var qs = $('#txtNums').val() * 1;
        if (qs > ZhSumCount)
            qs = ZhSumCount;
        var total = 0;
        for (var i = 0; i < qs; i++) {
            $("#issues_" + i).parent().addClass("checkbox-selected");
            var el = document.getElementById('issues_' + i)
            el.checked = true;
            $i("times_" + i).value = $('#txtStartTimes').val();
            $i("money_" + i).innerHTML = (PriceTotal * $('#txtStartTimes').val()).toFixed(2);

            total += parseFloat($i("money_" + i).innerHTML * 1);
        }
        CalculateZh();
    }
    if (NmbZH == 2) {
        var fgqs = $('#txtJiange').val() * 1;
        var fbs = $('#txtTimes').val() * 1;
        var fqs = $('#txtNums').val() * 1;

        if (fqs > ZhSumCount)
            fqs = ZhSumCount;
        var total2 = 0;
        var bs = 1;
        var gq = fgqs;
        for (var i = 0; i < fqs; i++) {
            $("#issues_" + i).parent().addClass("checkbox-selected");
            var el = document.getElementById('issues_' + i)
            el.checked = true;
            $i("times_" + i).value = bs;
            $i("money_" + i).innerHTML = (PriceTotal * bs).toFixed(2);

            total2 += PriceTotal * bs * 1;
            gq--;
            if (gq == 0) {
                bs = bs * fbs * 1;
                gq = fgqs;
            }
        }
        CalculateZh();
    }
    if (NmbZH == 3) {
        //起始倍数  =1
        var pstart = Number($('#txtStartTimes').val());
        //最低收益率=100																
        var prate = Number($('#txtPer').val());
        //追号期数  =15
        var pdatecount = Number($('#txtNums').val());
        //单倍金额  =125.00																	
        var singleAmount = Number(ArrayOrder[0].Price);
        //单倍奖金  =970.00							
        var singleBonus = Number(ArrayOrder[0].singelBouns);

        var pmultiple = pstart;
        if ((pstart < 1) || (isNaN(pstart))) {
            emAlert('起始倍数不能小于1！');
            return false;
        }
        if ((pdatecount < 2) || (isNaN(pdatecount))) {
            emAlert('追号期数不能小于2！');
            return false;
        }
        if ((prate < 0) || (isNaN(prate))) {
            emAlert('最低收益率不能小于0！');
            return false;
        }
        if (singleAmount > singleBonus) {
            emAlert('购买金额过多超过奖金值无意义！');
            return false;
        }
        if (prate / 100 >= (singleBonus - singleAmount) / singleAmount) {
            emAlert('收益率有误，请重新填写！');
            return false;
        }
        var totalMultiple = 0;
        for (var i = 0; i < pdatecount; i++) {
            $("#issues_" + i).parent().addClass("checkbox-selected");
            var el = document.getElementById('issues_' + i)
            el.checked = true;
            $i("times_" + i).value = pmultiple;
            $i("money_" + i).innerHTML = (pmultiple * PriceTotal).toFixed(2);

            totalMultiple += pmultiple;
            pmultiple = Math.ceil((totalMultiple * singleAmount * (1 + prate / 100)) / (singleBonus - singleAmount * (1 + prate / 100)));
            if (pstart > pmultiple) { pmultiple = pstart; }
        }
        CalculateZh();

    }
}

function change() {
    $("#txtNums").val($("#cbkQs").val());
}

function CalculateZh() {
    SumZhCount = 0;
    SumZhTotal = 0;
    for (var i = 0; i < ZhSumCount; i++) {
        if ($("#issues_" + i).parent().hasClass("checkbox-selected")) {
            SumZhCount += parseInt($i("times_" + i).value);
        }
        SumZhTotal = parseInt(SumZhCount) * parseFloat(PriceTotal);
    }
    $i("spanSumQS").innerHTML = SumZhCount.toFixed(0);
    $i("spanSumTotal").innerHTML = SumZhTotal.toFixed(2);
}

var qs2 = 0;
var total2 = 0;
//复选框选择事件
function chkChange(obj) {
    if ($("#issues_" + obj).parent().hasClass("checkbox-selected")) {
        $("#issues_" + obj).parent().removeClass("checkbox-selected");
        $("#issues_" + obj).attr("checked", false);
        $i("times_" + obj).value = 0;
        $i("money_" + obj).innerHTML = (PriceTotal * 0).toFixed(2);
    } else {
        $("#issues_" + obj).parent().addClass("checkbox-selected");
        $("#issues_" + obj).attr("checked", true);
        $i("times_" + obj).value = 1;
        $i("money_" + obj).innerHTML = (PriceTotal * 1).toFixed(2)
    }
    CalculateZh();
}

var qs2 = 0;
var total2 = 0;
//倍数事件
function TimesChange(obj) {
    $("#issues_" + obj).parent().addClass("checkbox-selected");
    $("#issues_" + obj).attr("checked", true);
    $i("money_" + obj).innerHTML = (PriceTotal * $i("times_" + obj).value).toFixed(2);
    CalculateZh();
}

//追号投注
function ajaxZHBetView() {
    if (checkBetTime() == false) {
        return false;
    }

    if ($i('spanSumTotal').innerHTML == "" || $i('spanSumTotal').innerHTML == "0.00") {
        emAlert('请选择追号期数，生成追号计划！');
        return false;
    }
    if (parseFloat(UserMoney) < parseFloat(SumZhTotal)) {
        emAlert('金额不足，不能追号投注！');
        return false;
    }
    SelectedData.splice(0, SelectedData.length);
    for (var i = 0; i < ZhSumCount; i++) {
        if ($("#issues_" + i).parent().hasClass("checkbox-selected") && parseFloat($i("times_" + i).value)>0) {
            if (i == 0) {
                StartSn = $i("sn_0").value;
            }
            var strqh = $i("sn_" + i).innerHTML;
            var times = $i("times_" + i).value;
            var total = $i("money_" + i).innerHTML;
            var stime = $i("stime_" + i).innerHTML;
            var JsonZhDetail = {
                "ZHIssueNum": strqh,
                "ZHTimes": times,
                "ZHSTime": stime
            };
            SelectedData.push(JsonZhDetail);
        }
    }

    //追号列表
    var arrzh = "";
    for (var index in SelectedData) {
        arrzh += JSON.stringify(SelectedData[index]) + ",";
    }
    arrzh = arrzh.substr(0, arrzh.length - 1);

    var isstop = $("#isStop").val();

    var zhjson = "";
    for (var index in ArrayOrder) {
        zhjson += "{\"LotteryId\":\"" + ArrayOrder[index].LotteryId
            + "\",\"PlayId\":\"" + ArrayOrder[index].PlayId
            + "\",\"IssueNum\":\"" + SelectedData[0]["ZHIssueNum"]
            + "\",\"Price\":" + ArrayOrder[index].Price
            + ",\"times\":\"" + ArrayOrder[index].times
            + "\",\"Num\":" + ArrayOrder[index].Num
            + ",\"singelBouns\":\"" + ArrayOrder[index].singelBouns
            + "\",\"Point\":\"" + ArrayOrder[index].Point
            + "\",\"balls\":\"" + ArrayOrder[index].balls
            + "\",\"strPos\":\"" + ArrayOrder[index].strPos
            + "\",\"PlayName\":\"" + ArrayOrder[index].PlayName
            + "\",\"IsStop\":\"" + isstop
            + "\",\"ZHNums\":\"" + SumZhCount
            + "\",\"ZHSums\":\"" + SumZhTotal
            + "\",\"table2\": [" + arrzh + "]},";
    }
    zhjson = zhjson.substr(0, zhjson.length - 1);

    var index = emLoading();
    $.ajax({
        type: "post",
        dataType: "json",
        data: zhjson,
        url: "/ajax/ajaxBetting.aspx?oper=ajaxZHBetting&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
        success: function (d) {
            switch (d.result) {
                case '-1':
                    emAlert(d.returnval); top.window.location = '/login.html';
                    break;
                case '0':
                    emAlert(d.returnval);
                    break;
                case '1':
                    emAlert(d.returnval);
                    ajaxAddAfterClear();
                    ajaxBetAfterClear();
                    PlayPos = "";
                    closeload(index);
                    top.window.location = '/statics/html/betSuccess.html?lid=' + LotteryId + '&cur=' + $i("spanSumTotal").innerHTML + '&money=' + (UserMoney - parseFloat($i("spanSumTotal").innerHTML));
                    break;
            }
        }
    });
}