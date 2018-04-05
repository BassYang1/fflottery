<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="useradd.aspx.cs" Inherits="Lottery.IPhone.user.useradd" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>立博国际娱乐</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=no">
    <meta name="format-detection" content="telephone=no,email=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/global.css" />
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/style.css" />
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script src="/statics/formValidator.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
                emAlert(msg);
            },
                onsuccess: function () { return true; }
            });
            $("#txtUserName").formValidator({ tipid: "tipUserName", onshow: "请输入会员账号", onfocus: "由0-9，z-a,A-Z组成的6-16位的字符" }).InputValidator({ min: 5, max: 20, onerror: "会员账号为5-20个字符" }).RegexValidator({ regexp: "username", datatype: "enum", onerror: "汉字或字母开头,不支持符号" });
            $("#txtAdminPass1").formValidator({ tipid: "tipAdminPass1", onshow: "请输入登陆密码", onfocus: "密码由字母组成的6-14个字符，且包含数字和字母" }).InputValidator({ min: 6, max: 14, onerror: "密码6-14位,请确认" });
            $("#txtAdminPass2").formValidator({ tipid: "tipAdminPass2", onshow: "请输入重复密码", onfocus: "由字母组成的6-14个字符，且包含数字和字母" }).InputValidator({ min: 6, max: 14, onerror: "密码6-14位,请确认" });
        });

        function ajaxRegsiter() {
            if (site.RegIsOpen == "1") {
                emAlert("注册已关闭，请联系管理员！");
                return false;
            }
            if ($.formValidator.PageIsValid('1')) {
                var type = $("#ddlType").val();
                var uName = $("#txtUserName").val();
                var oPass = $('#txtAdminPass1').val();
                var point = $("#ddlPoint").val();

                var index = emLoading();
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: "/ajax/ajaxUser.aspx?oper=ajaxRegiter&clienttime=" + Math.random(),
                    data: "type=" + type + "&name=" + encodeURIComponent(uName) + "&pwd=" + encodeURIComponent(oPass) + "&point=" + encodeURIComponent(point),
                    error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                    success: function (d) {
                        switch (d[0].result) {
                            case '0':
                                emAlert(d[0].message);
                                break;
                            case '1':
                                emAlert(d[0].message);
                                window.location.href = '/user/userlist.html';
                                $("#txtUserName").val("");
                                $('#txtAdminPass1').val("");
                                $('#txtAdminPass2').val("");
                                break;
                        }
                        closeload(index);
                    }
                });
            }
        }
    </script>
</head>
<body>
    <div class="container">
        <header id="header">
        <h1 class="title">开户中心</h1>
        <a href="javascript:history.go(-1);" class="back"></a>
    </header>
        <main id="main">
        <div class="change-password">
            <form id="form1" runat="server" class="lt-form change-password-form">
            <div class="form-item">
                    <div class="item-title">
                        <label class="lab">会员级别</label>
                    </div>
                    <div class="item-content">
                          <select id="ddlType">
                        <option value="0">代理</option>
                        <option value="1">会员</option>
                    </select>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">选择返点</label>
                    </div>
                    <div class="item-content">
                          <asp:DropDownList ID="ddlPoint" runat="server">
                    </asp:DropDownList>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">会员账号</label>
                    </div>
                    <div class="item-content">
                        <input id="txtUserName" type="text" value="" class="ipt" placeholder="请输入会员账号" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">登陆密码</label>
                    </div>
                    <div class="item-content">
                       <input id="txtAdminPass1" value="" class="ipt" onfocus="this.type='password'" autocomplete="off"
                        placeholder="请输入密码" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">确认密码</label>
                    </div>
                    <div class="item-content">
                        <input id="txtAdminPass2" value="" class="ipt" onfocus="this.type='password'" autocomplete="off"
                        placeholder="请输入密码" />
                    </div>
                </div>
                <div class="form-btns">
                    <input type="button" value="确认添加" class="btn primary-btn" onclick="ajaxRegsiter()" />
                </div>
            </form>
        </div>
    </main>
    </div>
</body>
</html>
