//加载玩法大类
function ajaxBigType() {
    if (getCookie("Nmbtype") != null) {
        if (Nmbtype != getCookie("Nmbtype")) {
            delCookie("PlayTypeId");
            delCookie("PlayId");
        }
        setCookie("Nmbtype", Nmbtype);
    }
    else {
        setCookie("Nmbtype", Nmbtype);
    }
    var frist = 0;
    var str = "";
    for (i = 0; i < lotteryData.table.length; i++) {
        if (lotteryData.table[i].typeid == Nmbtype) {
            var id = lotteryData.table[i].id;
            var title = lotteryData.table[i].title;

            if (getCookie("PlayTypeId") != null) {
                if (id == getCookie("PlayTypeId")) {
                    PlayBigId = id;
                    PlayBigName = lotteryData.table[i].title;
                    $i("playTypeName").innerHTML = title;
                    str += "<li class='selected'><a href='javascript:;' nmb='" + id + "' nmbname='" + title + "'>" + title + "</a></li>";
                }
                else {
                    str += "<li><a href='javascript:;' nmb='" + id + "' nmbname='" + title + "'>" + title + "</a></li>";
                }
            }
            else {
                if (id == 1001 || id == 2001 || id == 3001 || id == 4001) {
                    PlayBigId = id;
                    PlayBigName = lotteryData.table[i].title;
                    $i("playTypeName").innerHTML = title;
                    str += "<li class='selected'><a href='javascript:;' nmb='" + id + "' nmbname='" + title + "'>" + title + "</a></li>";
                }
                else {
                    str += "<li><a href='javascript:;' nmb='" + id + "' nmbname='" + title + "'>" + title + "</a></li>";
                }
            }
        }
    }
    $i("bigtype").innerHTML = str;
    if (getCookie("PlayTypeId") != null) {
        frist = getCookie("PlayTypeId");
    }
    else {
        frist = PlayBigId;
        setCookie("PlayTypeId", PlayBigId);
    }
    //delCookie("PlayId");
    ajaxSmallType(frist);

    //大类玩法切换
    $(".lottery-pg li").delegate('a', 'click', function (event) {
        //delCookie("PlayId");
        var nmb = $(this).attr("nmb");
        var nmbname = $(this).attr("nmbname");
        $(".lottery-pg li a").each(function (i, n) {
            $(this).parents("li").removeClass().addClass("n" + $(n).attr("nmb"));
        });
        $(this).parents("li").removeClass().addClass("selected");
        PlayBigName = nmbname;
        $i("playTypeName").innerHTML = nmbname;
        ajaxSmallType(nmb);
        ajaxAddAfterClear();
        //CloseZhBet();
        $('#points').empty();
        Betpoint = 0;
        PlayPos = "";
        CreatePoints();

        setCookie("PlayTypeId", nmb);
    });
}

//加载小类玩法
function ajaxSmallType(bigid) {
    var str = "";

    for (k = 0; k < lotteryData.table.length; k++) {
        if (lotteryData.table[k].id == bigid) {
            var str = ""; str2 = lotteryData.table[k].table2;
            for (i = 0; i < str2.length; i++) {

                var id = str2[i].id; title0 = str2[i].title0; title = str2[i].title; wznum = str2[i].wznum; tplayRandoms = str2[i].randoms; code = str2[i].title2; maxnum = str2[i].maxnum;
                if (getCookie("PlayId") != null) {
                    if (id == getCookie("PlayId")) {
                        PlayId = id;
                        PlayName = title;
                        PlayRandoms = tplayRandoms;
                        PlayWzNum = wznum;
                        PlayCode = code;
                        PlayMaxNum = maxnum;
                        $i("playName").innerHTML = title;
                        str += "<li class='selected'><a href='javascript:void(0);' nmb='" + id + "' nmbname='" + title + "' nmbcode='" + code + "' nmbwz='" + wznum + "' nmbrandoms='" + tplayRandoms + "' maxnum='" + maxnum + "'>" + title + "</a></li>";
                    }
                    else {
                        str += "<li><a href='javascript:void(0);' nmb='" + id + "' nmbname='" + title + "' nmbcode='" + code + "' nmbwz='" + wznum + "' nmbrandoms='" + tplayRandoms + "' maxnum='" + maxnum + "'>" + title + "</a></li>";
                    }
                }
                else {
                    if (i == 0) {
                        setCookie("PlayId", id);
                        PlayId = id;
                        PlayName = title;
                        PlayRandoms = tplayRandoms;
                        PlayWzNum = wznum;
                        PlayCode = code;
                        PlayMaxNum = maxnum;
                        $i("playName").innerHTML = title;
                        str += "<li class='selected'><a href='javascript:void(0);' nmb='" + id + "' nmbname='" + title + "' nmbcode='" + code + "' nmbwz='" + wznum + "' nmbrandoms='" + tplayRandoms + "' maxnum='" + maxnum + "'>" + title + "</a></li>";
                    }
                    else {
                        str += "<li><a href='javascript:void(0);' nmb='" + id + "' nmbname='" + title + "' nmbcode='" + code + "' nmbwz='" + wznum + "' nmbrandoms='" + tplayRandoms + "' maxnum='" + maxnum + "'>" + title + "</a></li>";
                    }
                }
            }
        }
    }

    $i("smalltype").innerHTML = str;
    CreateNumber();
    CreatePoints();

    //小类玩法切换
    $(".lottery-p li").delegate('a', 'click', function (event) {
        $(".lottery-p li a").each(function (i, n) {
            $(this).parents("li").removeClass().addClass("n" + $(n).attr("nmb"));
        });
        $(this).parents("li").removeClass().addClass("selected");
        var nmb = $(this).attr("nmb");
        var nmbname = $(this).attr("nmbname");
        var nmbrandoms = $(this).attr("nmbrandoms");
        var Nmbtype = $(this).attr("Nmbtype");
        var nmbsign = $(this).attr("nmbsign");
        var nmbwz = $(this).attr("nmbwz");
        var nmbcode = $(this).attr("nmbcode");
        var maxnum = $(this).attr("maxnum");
        setCookie("PlayId", nmb);
        $i("playName").innerHTML = nmbname;
        PlayId = nmb;
        PlayName = nmbname;
        PlayCode = nmbcode;
        PlayRandoms = nmbrandoms;
        PlayWzNum = nmbwz;
        PlayMaxNum = maxnum;
        $('#points').empty();
        Betpoint = 0;
        PlayPos = "";
        ajaxAddAfterClear();
        // CloseZhBet();
        CreateNumber();
        CreatePoints();
        $('#inputtext').val("");
        $("#mainLottery").hide();


    });
}

// 客服js
$('.kf').click(function () {
    kf_width = $(this).width();
    if (kf_width > 61) {
        $(this).width(61);
    } else {
        $(this).width(200);
    }
});