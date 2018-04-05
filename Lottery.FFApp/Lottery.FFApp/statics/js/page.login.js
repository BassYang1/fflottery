
var audioElementHovertree;
$(document).ready(function () {
    _jcms_GetRefreshCode('imgCode');
    if (getCookie("username") != null) {
        $("#txtUserName").val(getCookie("username"));
    } 
    audioElementHovertree = document.createElement('audio');
    audioElementHovertree.setAttribute('autoplay', 'autoplay'); 
});

function ajaxLogin() {
    var uName = stripscript($("#txtUserName").val());
    var oPass = $('#txtUserPass').val();
    var typeNum = $("#txtLoginType").val();
    var code = $("#txtUserCode").val();
    if (site.WebIsOpen == "1") {
        emAlert(site.WebCloseSeason);
        return false;
    }
    if (uName == "") {
        emAlert("会员账号不能为空");
        return false;
    }
    if (oPass == "") {
        emAlert("密码不能为空");
        return false;
    }
    if (code == "") {
        emAlert("验证码不能为空");
        return false;
    }
    var pattern = new RegExp("[~'!@#$%^&*()-+_=:]");
    if (uName != "" && uName != null) {
        if (pattern.test(uName)) {
            emAlert("会员账号中包含非法字符！");
            $("#txtUserName").attr("value", "");
            $("#txtUserName").focus();
            return false;
        }
    }
    var index = emLoading();

    uPass = Lottery.MD5(oPass);
    //$('#txtUserPass').val(uPass);
    $.ajax({
        type: "post",
        dataType: "json",
        url: "ajax/ajax.aspx?oper=login&clienttime=" + Math.random(),
        data: "name=" + encodeURIComponent(uName) + "&pass=" + encodeURIComponent(uPass) + "&type=" + typeNum + "&code=" + encodeURIComponent(code),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
        success: function (d) {
            layer.close(index);
            if (d.result == "1") {
                setCookie("username", uName);
                top.location = '/index.aspx';
            }
            else {
                emAlert(d.message);
                return;
            }
            closeload(index);
            $('#txtUserPass').val("");
            $('#txtUserCode').val("");
        }
    });
}

function addfavorite() {
    if (document.all) {
        window.external.addFavorite('http://www.feifan1188.com', '非凡科技');
    }
    else if (window.sidebar) {
        window.sidebar.addPanel('非凡科技', 'http://www.feifan1188.com', "");
    }
} 