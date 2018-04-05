var UserCurMoney = 0;
var audioElementHovertree;
$(document).ready(function () {
    lookLot();
    ajaxUserInfo();
    ajaxPopInfo();
    $('.sc_ylnav a').click(function () {
        $(this).parent().find('a').removeClass();
        $(this).addClass('current');
        var src = $(this).attr("nmb");
        ajaxCheckLoginToUrl(src);
    });
    audioElementHovertree = document.createElement('audio');
    audioElementHovertree.setAttribute('autoplay', 'autoplay');
});

var flag = 0;
$(".lottery-rec").delegate('', 'click', function (event) {
    if (flag == 1) {
        $(".lottery-games").hide();
        flag = 0;
    } else {
        $(".lottery-games").show();
        flag = 1;
    }
});

var lookLot = function () {
    var $lotNav = $('#lottery-nav');
    $('#look-lot').on('click', function (e) {
        $lotNav.html(GetLottery());
        e.stopPropagation();
        var offset = $(this).offset();
        $lotNav.css({
            left: offset.left
        });
        $lotNav.show();
    });
    $lotNav.on('click', function (e) {
        e.stopPropagation();
    });
    $(document).on('click', function () {
        $lotNav.hide();
    });

    //平台管理员直接单线往下分配置
    //到[会员]不能签约下级
    //$.ajax({
    //    type: "get",
    //    dataType: "json",
    //    data: "clienttime=" + Math.random(),
    //    url: "/ajax/ajaxContractFH.aspx?oper=IsContract3",
    //    error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
    //    success: function (d) {
    //        if (d.result == 1) {
    //            $("#qy").show();
    //            $("#gz").show();
    //        }
    //        else {
    //            $("#qy").hide();
    //            $("#gz").hide();
    //        }
    //    }
    //});
};

//加载采种
function GetLottery() {
    var str1 = "", str2 = "", str3 = "", str4 = "", str5 = "", str6 = "";
    for (i = 0; i < lotteryJsonData.table.length; i++) {
        var id = lotteryJsonData.table[i].id;
        var title = lotteryJsonData.table[i].title;
        var code = lotteryJsonData.table[i].code;
        var type = lotteryJsonData.table[i].ltype;
        var indextype = lotteryJsonData.table[i].indextype;
        if (indextype == 1) {
            str1 += "<li>";
            str1 += "<a href='/"+code+"'>" + title;
            if (id == 1010 ||id == 1015) {
                str1 += "<i class='icon icon-h'></i>";
            }
            if (id == 1017) {
                str1 += "<i class='icon icon-n'></i>";
            }
            str1 += "</a></li>";
        }
        if (indextype == 2) {
            str2 += "<li>";
            str2 += "<a  href='/" + code + "'>" + title;
            if (id == 1001 || id == 1005) {
                str2 += "<i class='icon icon-h'></i>";
            }
            if (id == 1004) {
                str2 += "<i class='icon icon-n'></i>";
            }
            str2 += "</a></li>";
        }
        if (indextype == 3) {
            str3 += "<li><a  href='/" + code + "'>" + title;
            if (id == 1018) {
                str3 += "<i class='icon icon-h'></i>";
            }
            if (id == 1019) {
                str3 += "<i class='icon icon-n'></i>";
            }
            str3 += "</a></li>";
        }
        if (indextype == 4) {
            str4 += "<li>";
            str4 += "<a  href='/" + code + "'>" + title;
            if (id == 2002) {
                str4 += "<i class='icon icon-h'></i>";
            }
            str4 += "</a></li>";
        }
        if (indextype == 5) {
            str5 += "<li><a  href='/" + code + "'>" + title;
            if (id == 3002) {
                str5 += "<i class='icon icon-h'></i>";
            }
            if (id == 3005) {
                str5 += "<i class='icon icon-n'></i>";
            }
            str5 += "</a></li>";
        }
        if (indextype == 6) {
            str6 += "<li><a  href='/" + code + "'>" + title;
            if (id == 4001) {
                str6 += "<i class='icon icon-h'></i>";
            }
            if (id == 4002) {
                str6 += "<i class='icon icon-n'></i>";
            }
            str6 += "</a></li>";
        }
    }

    var gamesHtml = "";
    if (str1 != "") {
        gamesHtml += "<dl><dt>推荐彩种</dt><dd><ul>";
        gamesHtml += str1;
        gamesHtml += "</ul></dd></dl>";
    }
    if (str2 != "") {
        gamesHtml += "<dl><dt>时时彩</dt><dd><ul>";
        gamesHtml += str2;
        gamesHtml += "</ul></dd></dl>";
    }
    if (str3 != "") {
        gamesHtml += "<dl><dt>高频彩票</dt><dd><ul>";
        gamesHtml += str3;
        gamesHtml += "</ul></dd></dl>";
    }
    if (str4 != "") {
        gamesHtml += "<dl><dt>十一选五</dt><dd><ul>";
        gamesHtml += str4;
        gamesHtml += "</ul></dd></dl>";
    }
    if (str5 != "") {
        gamesHtml += "<dl><dt>福彩/体彩</dt><dd><ul>";
        gamesHtml += str5;
        gamesHtml += "</ul></dd></dl>";
    }
    if (str6 != "") {
        gamesHtml += "<dl><dt>北京赛车</dt><dd><ul>";
        gamesHtml += str6;
        gamesHtml += "</ul></dd></dl>";
    }
    return gamesHtml;
}

//加载基本信息
function ajaxUserInfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { } },
        success: function (d) {
            if (d.result != "0") {
                Adminname = d.AdminName;
                $i('username').innerHTML = d.AdminName;
                $i('money').innerHTML = UserCurMoney = d.AdminMoney;
            }
            else {
                window.location.href = '/login';
            }
        }
    });
    setTimeout('ajaxUserInfo()', 60000);
}

//获取即时信息
function ajaxPopInfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxUser.aspx?oper=GetUserJson",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { } },
        success: function (d) {
            //d = {
            //    result: "1", table: [
            //    { userid: "1937", title: "4001668748", content: "投注彩种 北京PK10<br/>投注期号 668748<br/>投注金额 100.0000元<br/>中奖金额 0.0000元<br/>本次盈亏 -100.0000元" }]
            //};

            if (d.result == "1") {
                if (d.table.length > 0) {
                    var t = d.table[0];
                    if (getCookie("pop") != null) {
                        if (getCookie("pop") != t.title + "") {
                            setCookie("pop", t.title);
                            PopInfo(t.content.replace(/,/g, "<br/>").replace(/ /g, "："));
                            //setTimeout(function () {
                            //    $('#pop').hide();
                            //}, 3000);
                        }
                    }
                    else {
                        setCookie("pop", t.title);
                        PopInfo(t.content.replace(/,/g, "<br/>").replace(/ /g, "："));
                        //setTimeout(function () {
                        //    $('#pop').hide();
                        //}, 3000);
                    }
                }
            }
        }
    });
    setTimeout('ajaxPopInfo()', 10000);
}

function ajaxNewsTop() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxNews.aspx?oper=ajaxGetNewsTop1",
        success: function (data) {
            if (data.result == "1") {
                $("#newsTop1").html(data.table[0].title);
            }
        }
    });
}

function initInfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { } },
        success: function (d) {
            if (d.result == "1") {
                $("#login").hide();
                $("#Info").show();
                Adminname = d.AdminName;
                $i('username').innerHTML = d.AdminName;
                $i('money').innerHTML = UserCurMoney = d.AdminMoney;
            }
        }
    });
}

//手动刷新
function ajaxRefresh() {
//    var index = emLoading();
    $i('money').innerHTML = "获取中...";
    initInfo();
//    closeload(index);
}

function chkLogout() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "/ajax/ajax.aspx?oper=logout&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            alert(XmlHttpRequest.responseText)
        },
        success: function (d) {
            if (d.result == "1")
                window.location.href = '/login'
        }
    });
};