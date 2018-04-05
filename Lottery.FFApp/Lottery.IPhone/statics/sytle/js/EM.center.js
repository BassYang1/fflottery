$(document).ready(function () {
    ajaxUserInfo();
});

//加载基本信息
function ajaxUserInfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { emAlert("亲！页面过期,请刷新页面!"); } },
        success: function (d) {
            if (d.result == "1") {
                $i('name').innerHTML = d.AdminName;
                $i('money').innerHTML = d.AdminMoney;
                UserMoney = d.AdminMoney;
            }
            else {
                top.location.href = '/login.html';
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
        url: "/ajax/ajax.aspx?oper=ajaxPopInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { emAlert("亲！页面过期,请刷新页面!"); } },
        success: function (d) {
            if (d.result == "1") {
                var titlesN3 = [];
                var contentsN3 = [];
                for (i = 0; i < d.table.length; i++) {
                    titlesN3.push(d.table[i].title);
                    contentsN3.push(d.table[i].msg);
                }
                MessagePop(titlesN3, contentsN3);
            }
            setTimeout('ajaxPopInfo()', 10000)
        }
    });
}

//手动刷新
function ajaxRefresh() {
    var index = emLoading();
    ajaxUserInfo();
    closeload(index);
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
                top.location.href = '/login.html'
        }
    });
}