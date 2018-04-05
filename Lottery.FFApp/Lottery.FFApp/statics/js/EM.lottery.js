//加载玩法大类
function ajaxBigType() {
    var str = "";
    for (var i = 0; i < lotteryData.table.length; i++) {
        if (lotteryData.table[i].typeid == Nmbtype) {
            var id = lotteryData.table[i].id;
            var title = lotteryData.table[i].title;
            if (id == 1001 || id == 2001 || id == 3001 || id == 4001) {
                PlayBigId = id;
                PlayBigName = title;
                str += "<li class='selected' nmb='" + id + "' nmbname='" + title + "'>" + title + "</li>";
                ajaxSmallType(PlayBigId);
            }
            else {
                str += "<li nmb='" + id + "' nmbname='" + title + "'>" + title + "</li>";
            }
        }
    }
    $("#bigtype").html(str);

    //大类玩法切换
    $(".lottery-playgroup").delegate('li', 'click', function (event) {
        //delCookie("PlayId");
        PlayBigId = $(this).attr("nmb");
        PlayBigName = $(this).attr("nmbname");
        $(".lottery-playgroup").find("li").removeClass();
        $(this).addClass("selected");
        ajaxAddAfterClear();
        CloseZhBet();
        playPoints = 0;
        playBouns = 0;
        Betpoint = 0;
        PlayPos = "";

        //加载玩法小类
        ajaxSmallType(PlayBigId);
    });
}

//加载玩法小类
function ajaxSmallType(bigid) {
    var zhix = "";
        zux = "";
        tsx = "";
        zhixnum = 0;
        zuxnum = 0;
        tsxnum = 0;
    for (var k = 0; k < lotteryData.table.length; k++) {
        if (lotteryData.table[k].id == bigid) {
            var str2 = lotteryData.table[k].table2;
            str2 = str2.sort(sortBy('id', false, parseInt));
            for (var i = 0; i < str2.length; i++) {
                var id = str2[i].id; type = str2[i].type; title = str2[i].title;
                if (type == "1") {
                    if (i == 0) {
                        PlayId = str2[i].id;
                        PlayName = str2[i].title;
                        PlayRandoms = str2[i].randoms;
                        PlayWzNum = str2[i].wznum;
                        PlayCode = str2[i].title2;
                        PlayMaxNum = str2[i].maxnum;
                        PlayMaxBonus = str2[i].maxbonus;
                        PlayMinBonus = str2[i].minbonus;
                        PlayMinBonus2 = str2[i].minbonus2;
                        PlayExample = str2[i].example;
                        PlayHelp = str2[i].help;
                        PlayRemark = str2[i].remark;
                        zhix += "<dd class='selected' nmb='" + id + "'>" + title + "</dd>";
                    }
                    else {
                        zhix += "<dd nmb='" + id + "'>" + title + "</dd>";
                    }
                    zhixnum++;
                }
                if (type == "2") {
                    if (i == 0) {
                        PlayId = str2[i].id;
                        PlayName = str2[i].title;
                        PlayRandoms = str2[i].randoms;
                        PlayWzNum = str2[i].wznum;
                        PlayCode = str2[i].title2;
                        PlayMaxNum = str2[i].maxnum;
                        PlayMaxBonus = str2[i].maxbonus;
                        PlayMinBonus = str2[i].minbonus;
                        PlayMinBonus2 = str2[i].minbonus2;
                        PlayExample = str2[i].example;
                        PlayHelp = str2[i].help;
                        PlayRemark = str2[i].remark;
                        zux += "<dd class='selected' nmb='" + id + "'>" + title + "</dd>";
                    }
                    else {
                        zux += "<dd nmb='" + id + "'>" + title + "</dd>";
                    }
                    zuxnum++;
                }
                if (type == "3") {
                    if (i == 0) {
                        PlayId = str2[i].id;
                        PlayName = str2[i].title;
                        PlayRandoms = str2[i].randoms;
                        PlayWzNum = str2[i].wznum;
                        PlayCode = str2[i].title2;
                        PlayMaxNum = str2[i].maxnum;
                        PlayMaxBonus = str2[i].maxbonus;
                        PlayMinBonus = str2[i].minbonus;
                        PlayMinBonus2 = str2[i].minbonus2;
                        PlayExample = str2[i].example;
                        PlayHelp = str2[i].help;
                        PlayRemark = str2[i].remark;
                        tsx += "<dd class='selected' nmb='" + id + "'>" + title + "</dd>";
                    }
                    else {
                        tsx += "<dd nmb='" + id + "'>" + title + "</dd>";
                    }
                    tsxnum++;
                }
            }
            if (zhixnum > 0) {
                zhix = "<dl><dt>" + lotteryData.table[k].title + "直选：</dt>" + zhix + "</dl>";
            }
            if (zuxnum > 0) {
                zux = "<dl><dt>" + lotteryData.table[k].title + "组选：</dt>" + zux + "</dl>";
            }
            if (tsxnum > 0) {
                tsx = "<dl><dt>" + lotteryData.table[k].title + "特殊：</dt>" + tsx + "</dl>";
            }
        }
    }

    $i("smalltype").innerHTML = zhix + zux + tsx;
    CreateNumber();
    bettingAdjust();
    //小类玩法切换
    $(".lottery-play dl").delegate('dd', 'click', function (event) {
        $(this).parents().find("dd").removeClass();
        $(this).addClass("selected");

        var nmb = $(this).attr("nmb");

        for (k = 0; k < lotteryData.table.length; k++) {
            var str2 = lotteryData.table[k].table2;
            for (i = 0; i < str2.length; i++) {
                var type = str2[i].type;
                if (str2[i].id == nmb) {
                    PlayId = str2[i].id;
                    PlayName = str2[i].title;
                    PlayRandoms = str2[i].randoms;
                    PlayWzNum = str2[i].wznum;
                    PlayCode = str2[i].title2;
                    PlayMaxNum = str2[i].maxnum;
                    PlayMaxBonus = str2[i].maxbonus;
                    PlayMinBonus = str2[i].minbonus;
                    PlayMinBonus2 = str2[i].minbonus2;
                    PlayExample = str2[i].example;
                    PlayHelp = str2[i].help;
                    PlayRemark = str2[i].remark;
                }
            }
        }
        playPoints = 0;
        playBouns = 0;
        Betpoint = 0;
        PlayPos = "";
        ajaxAddAfterClear();
        CloseZhBet();
        CreateNumber();
        bettingAdjust();
    });
}

var bettingAdjust = function () {
    var userPoint = eval($('#txtUserPoint').val()) * 0.1;
    var $bettingAdjust = $("#betting-adjust"),
		$ratio = $bettingAdjust.find(".ratio"),
		$bettingSlider = $bettingAdjust.find(".betting-slider"),
		$cover = $bettingSlider.find(".cover");
    $cover.width("0%");
    $bettingSlider.slider({
        value: 0,
        min: -eval(userPoint),
        max: 0,
        step: 0.1,
        slide: function (event, ui) {
            var point = (-ui.value);
            var bonus = (eval($('#lotBonus').val()) - ((-ui.value) * 20 * eval(PlayPosBonus))).toFixed(2);
            bonus = bonus * PriceModel * 0.5;
            if (eval($('#lotBonus2').val()) == 1) {
                $ratio.html(point + '%&nbsp;|&nbsp;' + bonus + "/" + (bonus / 2).toFixed(2));
                $('#bonus2').html(bonus + "/" + (bonus / 2).toFixed(2));
            }
            else if (eval($('#lotBonus2').val()) == 2) {
                $ratio.html(point + '%');
                $('#bonus2').html((bonus / 4.5).toFixed(4) + "/" + bonus);
            }
            else {
                $ratio.html(point + '%');
                $('#bonus2').html(bonus);
            }
            $cover.width(point * 100 / eval(userPoint) + "%");
            playPoints = point;
            playBouns = bonus;
        }
    });
}

//加载返点
function CreatePoints() {
    //$('#points').empty();
    Betpoint = 0;
    for (i = 0; i <= PointJsonData.table.length - 1; i++) {
        if (PointJsonData.table[i].SmallTypeId == PlayId) {
            for (j = 0; j <= PointJsonData.table[i].points.length - 1; j++) {
                var no = PointJsonData.table[i].points[j].no;
                var bonus = PointJsonData.table[i].points[j].bonus;
                var point2 = PointJsonData.table[i].points[j].point;
                var value2 = point2 + '/' + bonus;
                var aa = PointJsonData.table[i].points[j].no + '/' + PointJsonData.table[i].points[j].no;
                $('#bonus').html(value2);
            }
        }
    }
}

//返点选择
function SelectPoints() {
    var selno = $("#points").find("option:selected").attr("value");
    var temp = "";
    for (i = 0; i <= PointJsonData.table.length - 1; i++) {
        if (PointJsonData.table[i].SmallTypeId == PlayId) {
            for (j = 0; j <= PointJsonData.table[i].points.length - 1; j++) {
                var no = PointJsonData.table[i].points[j].no;
                var bonus = PointJsonData.table[i].points[j].bonus;
                var point2 = PointJsonData.table[i].points[j].point;
                var value2 = point2 + '/' + bonus;
                if (selno == no) {
                    temp = value2;
                }
            }
        }
    }
    if (temp == "") {
        Betpoint = 0;
    }
    else {
        Betpoint = temp;
    }
}

//计算总金额
function fromTimesChange() {
    var strPrice = 1; //getCookie("price");
    if (!isNaN(PriceTimes)) {
        SingleTotal = (eval(SingleCount) * eval(PriceTimes) * eval(Price)).toFixed(4) * PriceModel;
        $("#fromBuyPriceTotal").html(SingleTotal);
    } else {
        emAlert('倍数必须为数字');
        $('#fromTimes').val("1");
        PriceTimes = 1;
        return;
    }
}

function SelectModel() {
    Price = $("#model").find("option:selected").attr("value");
    PriceName = name;
    setCookie("price", Price);
    setCookie("priceName", PriceName);
    fromTimesChange();
}

//json排序
var sortBy = function (filed, rev, primer) {
    rev = (rev) ? -1 : 1;
    return function (a, b) {
        a = a[filed];
        b = b[filed];
        if (typeof (primer) != 'undefined') {
            a = primer(a);
            b = primer(b);
        }
        if (a < b) { return rev * -1; }
        if (a > b) { return rev * 1; }
        return 1;
    }
};