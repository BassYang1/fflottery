
function SwitchBouns(betBalls) {
    if (betBalls == undefined || betBalls == null || betBalls == "") {
        return;
    }

    switch (PlayCode) {
        case "H_SXZX":
        case "H_SXZXDS":
            break;
        default:
            return;
    }

    var done = false;
    var balls = betBalls.split("_");
    var smtype = balls[0];

    if (smtype == "单") {
        smtype = "D";
    }
    else if (smtype == "双") {
        smtype = "S";
    }
    else if (smtype == "大") {
        smtype = "D";
    }
    else if (smtype == "小") {
        smtype = "X";
    }

    var title = PlayCode + smtype;
    var smallTypeId = "0";

    for (k = 0; k < lotteryData.table.length; k++) {
        if (PlayBigId != lotteryData.table[k].id) {
            continue;
        }

        var str2 = lotteryData.table[k].table2;

        for (i = 0; i < str2.length; i++) {
            if (str2[i].title2 == title) {
                smallTypeId = str2[i].id;
                //PlayId = str2[i].id;
                //PlayName = str2[i].title;
                PlayRandoms = str2[i].randoms;
                PlayWzNum = str2[i].wznum;
                //PlayCode = str2[i].title2;
                PlayMaxNum = str2[i].maxnum;
                MinBouns = str2[i].minbonus;

                done = true;
                break;
            }
        }

        if (done) {
            break;
        }
    }

    $('#points').empty();
    Betpoint = 0;
    for (i = 0; i <= PointJsonData.table.length - 1; i++) {
        if (smallTypeId != "0" && PointJsonData.table[i].SmallTypeId == smallTypeId) {
            for (j = 0; j <= PointJsonData.table[i].points.length - 1; j++) {
                var no = PointJsonData.table[i].points[j].no;
                var bonus = PointJsonData.table[i].points[j].bonus * parseFloat(PricePos) * 2;
                var point2 = PointJsonData.table[i].points[j].point;
                var value2 = point2 + '/' + bonus;
                var aa = PointJsonData.table[i].points[j].no + '/' + PointJsonData.table[i].points[j].no;
                if (j == 0) {
                    Betpoint = value2;
                    $('#points').prepend('<option value="' + no + '" selected>' + bonus + '/' + point2 + '%</option>');
                }
                else {
                    $('#points').prepend('<option value="' + no + '">' + bonus + '/' + point2 + '%</option>');
                }
            }
        }
    }
}

//加载返点
function CreatePoints() {
    $('#points').empty();
    Betpoint = 0;
    for (i = 0; i <= PointJsonData.table.length - 1; i++) {
        if (PointJsonData.table[i].SmallTypeId == PlayId) {
            for (j = 0; j <= PointJsonData.table[i].points.length - 1; j++) {
                var no = PointJsonData.table[i].points[j].no;
                var bonus = PointJsonData.table[i].points[j].bonus * parseFloat(PricePos) * 2;
                var point2 = PointJsonData.table[i].points[j].point;
                var value2 = point2 + '/' + bonus;
                var aa = PointJsonData.table[i].points[j].no + '/' + PointJsonData.table[i].points[j].no;
                if (j == 0) {
                    Betpoint = value2;
                    $('#points').prepend('<option value="' + no + '" selected>' + bonus + '/' + point2 + '%</option>');
                }
                else {
                    $('#points').prepend('<option value="' + no + '">' + bonus + '/' + point2 + '%</option>');
                }
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
                var bonus = PointJsonData.table[i].points[j].bonus * parseFloat(PricePos);
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
    var strPrice = getCookie("price");
    if (strPrice == null)
        strPrice = "1";
    if (!isNaN(PriceTimes)) {
        SingleTotal = (eval(SingleCount) * eval(PriceTimes) * eval(strPrice) * 2 * parseFloat(PricePos)).toFixed(4);
        $("#fromBuyPriceTotal").html(SingleTotal);
    } else {
        emAlert('倍数必须为数字');
        $('#fromTimes').val("1");
        PriceTimes = 1;
        return;
    }
}

//显示金额单位
function SelectModel() {
    Price = $("#model").find("option:selected").attr("value");
    PriceName = name;
    setCookie("price", Price);
    setCookie("priceName", PriceName);
    fromTimesChange();
}
