$(document).ready(function () {
    ajaxUserBindInfo();
});

function ajaxUserBindInfo() {
    var index = emLoading();
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxCenter.aspx?oper=ajaxGetUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
        success: function (d) {
            if (d.result == "1") {
                var userlevel = 0;
                if (d.table.length > 0) {
                    var t = d.table[0];
                    $("#username2").html(t.username);
                    $("#money2").html(t.money);

                    var info = '';
                    if (t.istruename == "1") {
                        userlevel++;
                        info += '<a href="javascript:;" class="verified"><i class="icon icon-user"></i></a>';
                    }
                    else {
                        info += '<a href="javascript:;"><i class="icon icon-user"></i></a>';
                    }
                    if (t.isemail == "1") {
                        userlevel++;
                        info += '<a href="javascript:;" class="verified"><i class="icon icon-email"></i></a>';
                    }
                    else {
                        info += '<a href="javascript:;"><i class="icon icon-email"></i></a>';
                    }
                    if (t.ismobile == "1") {
                        userlevel++;
                        info += '<a href="javascript:;" class="verified"><i class="icon icon-phone"></i></a>';
                    }
                    else {
                        info += '<a href="javascript:;"><i class="icon icon-phone"></i></a>';
                    }
                    $("#bindinfo").html(info);

                    if (userlevel == 0) {
                        $("#userLevel").html("低");
                    }
                    if (userlevel == 1) {
                        $("#userLevel").html("低");
                    }
                    if (userlevel == 2) {
                        $("#userLevel").html("中");
                    }
                    if (userlevel == 3) {
                        $("#userLevel").html("中");
                    }
                    if (userlevel == 4) {
                        $("#userLevel").html("高");
                    }
                }
                ajaxList();
                ajaxLotteryTime();
                ajaxNewsList();
                ajaxUserLogin();
                closeload(index);
                LayerPop('系统公告', '800px', '550px', '/news/newsindex.html');
                $.ajax({
                    type: "get",
                    dataType: "json",
                    data: "clienttime=" + Math.random(),
                    url: "/ajax/ajaxContractFH.aspx?oper=IsContractState",
                    error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                    success: function (d) {
                        if (d.result == 1) {
                            BankLayerPop("/vip.q");
                        }
                        else {
                            if (d.result2 == 1) {
                                BankLayerPop("/vip.g");
                            }
                        }
                    }
                });
            }
            else {
                top.location = '/login';
            }
        }
    });
}

function BankLayerPop(str) {
    parent.layer.confirm('您有新的契约需要签订，是否签约？', {
        icon: 3,
        title: '温馨提示',
        btn: ['立即接受'],
        shade: 0.2
    }, function () {
        top.location.href = str;
    });
}

//显示开奖信息，时间等
function ajaxLotteryTime() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxLotteryTimeIndex&lid=1001",
        success: function (data) {
            currsn = data.cursn;
            nestsn = data.nestsn;
            $('#lotteryname').html(data.name);
            $('#lotIssue').html("第 " + currsn + " 期开奖号码");
			var numberArr = data.number;
            numberArr = (numberArr + "").split(",");
            var number = "";
            for (var i = 0; i < numberArr.length; i++) {
                number += '<span class="number">' + numberArr[i] + '</span>';
            }
            $('#lotNumber').html(number);
        }
    });
}

function ajaxList() {
    var s = '';
    for (i = 0; i < lotteryJsonData.table.length; i++) {
        var id = lotteryJsonData.table[i].id;
        var title = lotteryJsonData.table[i].title;
        var code = lotteryJsonData.table[i].code;
        var issnum = lotteryJsonData.table[i].issnum;
        var code2 = '/' + code;
        if (id == 1004 || id == 1005 || id == 1010 || id == 1011 || id == 1018 || id == 1019 || id == 2002 || id == 2006 || id == 3002 || id == 4001) {
            s += '<li>';
            s += '<img src="/statics/img/' + id + '.png" style="width:70px;height:70px;" alt="">';
            s += '<div class="hover">';
            s += '<span class="name">' + title + '</span>';
            s += '<a href="javascript:;" onclick="ajaxCheckLotteryToUrl(\'' + code2 + '\',\'' + code + '\')" class="btn btn-betting">立即投注</a>';
            s += '</div>';
            s += '</li>';
        }
    }
    $("#lotList").html(s);
}

function ajaxNewsList() {
    var u = "/ajax/ajaxNews.aspx?oper=ajaxGetNewsList";
    $.ajax({
        type: "get",
        dataType: "json",
        data: "page=1&pagesize=5&clienttime=" + Math.random(),
        url: u,
        error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
        success: function (d) {
            var gettpl = '';
            if (d.table.length > 0) {
                for (var i = 0; i < d.table.length; i++) {
                    gettpl += '<li class="read">';
                    gettpl += '<a href="javascript:;" onclick="LayerPop(\'系统公告\', \'800px\', \'550px\', \'/news/newsindex.html\');">';
					gettpl += '<span class="dot"></span><span class="time">'+d.table[i].tmonth+'/'+d.table[i].tday+'</span>'+d.table[i].title+'</a>';
                    gettpl += '</li>';
                }
            }
            else {
                gettpl += '无数据';
            }
			$("#list-notice").html(gettpl);
        }
    });
}

function ajaxUserLogin() {
    var u = "/ajax/ajaxCenter.aspx?oper=ajaxUserLoginList";
    $.ajax({
        type: "get",
        dataType: "json",
        data: "page=1&pagesize=3&clienttime=" + Math.random(),
        url: u,
        error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
        success: function (d) {
            var gettpl = '';
            if (d.table.length > 0) {
                for (var i = 0; i < d.table.length; i++) {
                    gettpl += '<li>';
                    gettpl += '<span>' + d.table[i].logintime + '</span>';
                    gettpl += '<span>' + d.table[i].ip + '</span>';
                    gettpl += '<span>' + d.table[i].address + '</span>';
                    gettpl += '</li>';
                }
            }
            else {
                gettpl += '无数据';
            }
            $("#UserLoginlist").html(gettpl);
        }
    });
}