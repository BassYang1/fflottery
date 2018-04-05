var flag = joinValue('u');
$(document).ready(function () {
    $("#txtUserName").focus();
    _jcms_GetRefreshCode('imgCode', 36);
});

function ajaxLogin() {
    var uName = $("#txtUserName").val();
    var oPass = $('#txtAdminPass1').val();
    var oPass2 = $('#txtAdminPass2').val();
    var uCode = $("#txtUserCode").val();
    if (uName == "") {
        emAlert("请您输入用户名！");
        return false;
    }
    if (uName.length < 5 || uName.length > 20) {
        emAlert("用户名为5-20位！");
        return false;
    }
    if (oPass == "") {
        emAlert("请您输入密码！");
        return false;
    }
    if (oPass2 == "") {
        emAlert("请您输入确认密码！");
        return false;
    }
    if (oPass != oPass2) {
        emAlert("两次密码输入不一致！");
        return false;
    }
    if (uCode == "") {
        emAlert("请您输入验证码！");
        return false;
    }
    uPass = Lottery.MD5(oPass);

    $.ajax({
        type: "post",
        dataType: "json",
        url: "/ajax/ajax.aspx?oper=ajaxRegister&clienttime=" + Math.random(),
        data: "name=" + encodeURIComponent(uName) + "&pass=" + encodeURIComponent(uPass) + "&code=" + encodeURIComponent(uCode) + flag,
        error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
        success: function (d) {
            if (d.result == "1") {
                emAlertSuccess("恭喜您注册成功");
                $('#txtUserName').val("");
                $('#txtAdminPass1').val("");
                $('#txtAdminPass2').val("");
                $("#txtUserCode").val("");
            }
            else {
                emAlert(d.returnval);
            }
            _jcms_GetRefreshCode('imgCode', 36);
        }
    });
}

function ajaxClear() {
    $('#txtUserName').val("");
    $('#txtAdminPass1').val("");
    $('#txtAdminPass2').val("");
    $("#txtUserCode").val("");
}