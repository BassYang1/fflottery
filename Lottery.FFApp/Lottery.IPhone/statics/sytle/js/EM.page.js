$(document).ready(function () {
    ajaxCheckLogin();
});

function ajaxCheckLogin() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxCheckLogin",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { emAlert("亲！页面过期,请刷新页面!"); } },
        success: function (d) {
            if (d.result != "1") {
                top.location.href = '/login.html';
            }
        }
    });
}