
//加载返点
function CreatePoints() {
    $('#points').empty();
    Betpoint = 0;
    for (i = 0; i <= PointJsonData.table.length - 1; i++) {
        if (PointJsonData.table[i].SmallTypeId == PlayId) {
            for (j = 0; j <= PointJsonData.table[i].points.length - 1; j++) {
                var no = PointJsonData.table[i].points[j].no;
                var bonus = PointJsonData.table[i].points[j].bonus * parseFloat(PricePos);
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

function SelectModel() {
    Price = $("#model").find("option:selected").attr("value");
    PriceName = name;
    setCookie("price", Price);
    setCookie("priceName", PriceName);
    fromTimesChange();
}
