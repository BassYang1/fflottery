var bzTime = "";
var curTime = "";

$(document).ready(function () {
    ajaxCurTime();
    ajaxCurBzTime();
    setInterval("ajaxCurTime()", 20000);
    setInterval("ajaxCurBzTime()", 20000);
});

function ajaxCurTime() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "ajax.aspx?oper=ajaxGetCurTime",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") {  } },
        success: function (d) {
            $i("curTime").innerHTML = d.table[0].time; 
        }
    });
}

function ajaxCurBzTime() {
    $.ajax({
        type: 'get',
        async: false,
        url: 'http://api.k780.com:88/?app=life.time&appkey=21881&sign=6248da051e9db5fb45c4854a7902553d&format=json&jsoncallback=data',
        dataType: 'jsonp',
        jsonp: 'callback',
        jsonpCallback: 'data',
        success: function (data) {
            if (data.success != '1') {
                $i("bzTime").innerHTML = data.msgid + ' ' + data.msg;
            }
            //bzTime = (new Date(oldTime)).getTime(); //得到毫秒数
            $i("bzTime").innerHTML = data.result["datetime_1"];
        },
        error: function () {
            $i("bzTime").innerHTML = 'fail';
        }
    });
}