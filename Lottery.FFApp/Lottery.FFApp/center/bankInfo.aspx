<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bankInfo.aspx.cs" Inherits="Lottery.WebApp.center.bankInfo" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>立博国际娱乐</title>
    <link rel="stylesheet" type="text/css" href="/statics/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/statics/css/member.css" />
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/statics/formValidator.js" type="text/javascript"></script>
    <script src="/statics/global.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ajaxUserInfo();
            $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
                emAlert(msg);
            },
                onsuccess: function () { return true; }
            });
            $("#txtPayPwd").formValidator({ tipid: "message", onshow: "请输入资金密码", onfocus: "请正确输入资金密码", defaultvalue: "" }).InputValidator({ min: 6, onerror: "资金密码不低于6位" });
            $("#txtPayAccount").formValidator({ tipid: "message", onshow: "请输入银行卡号", onfocus: "请正确输入银行卡号", defaultvalue: "" }).InputValidator({ min: 15, onerror: "银行卡号位数不够" });
            $("#txtPayAccount2").formValidator({ tipid: "message", onshow: "请确认银行卡号", onfocus: "请确认银行卡号", defaultvalue: "" }).InputValidator({ min: 15, onerror: "确认银行卡号位数不够" });
        });

        function ajaxUserInfo() {
            $.ajax({
                type: "get",
                dataType: "json",
                data: "clienttime=" + Math.random(),
                url: "/ajax/ajaxCenter.aspx?oper=ajaxGetUserInfo",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    if (d.result == "1") {
                        if (d.table.length > 0) {
                            var t = d.table[0];
                            $("#txtPayName").html(t.truename);
                        }
                        else {
                            $("#txtPayName").html("请先绑定真实姓名");
                        }
                    }
                    else {
                        $("#txtPayName").html("请先绑定真实姓名");
                    }
                }
            });
        }

        function chkBankAdd() {
            if ($.formValidator.PageIsValid('1')) {
                var ddl = document.getElementById("ddlBank")
                var index = ddl.selectedIndex;
                var Bank = ddl.options[index].value;
                var BankName = ddl.options[index].text;
                var Address = $('#txtAddress').val();
                var PayAccount = $("#txtPayAccount").val();
                var PayAccount2 = $("#txtPayAccount2").val();
                var PayName = $("#txtPayName").html();
                var PayPwd = $("#txtPayPwd").val();
                if (PayName=="") {
                    emAlert("请先绑定真实姓名");
                    return;
                }
                if (PayAccount != PayAccount2) {
                    emAlert("两次输入卡号不一致，请重新输入");
                    return;
                }
                var index = emLoading();

                uPass = Lottery.MD5(PayPwd);
                $.ajax({
                    type: "post",
                    dataType: "json",
                    url: "/ajax/ajaxBank.aspx?oper=ajaxAddBank&clienttime=" + Math.random(),
                    data: "Bank=" + Bank + "&BankName=" + BankName + "&Address=" + encodeURIComponent(Address) + "&PayAccount=" + encodeURIComponent(PayAccount) + "&PayName=" + encodeURIComponent(PayName) + "&PayPwd=" + encodeURIComponent(uPass),
                    error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                    success: function (d) {
                        switch (d[0].result) {
                            case '0':
                                closeload(index);
                                emAlert(d[0].message);
                                break;
                            case '1':
                                closeload(index);
                                parent.ajaxList();
                                parent.layer.close(parent.layer.getFrameIndex(window.name));
                                break;
                        }
                    }
                });
            }
        }
        function ischinese(s) {
            var ret = true;
            for (var i = 0; i < s.length; i++)
                ret = ret && (s.charCodeAt(i) >= 10000);
            return ret;
        } 
    </script>
</head>
<body>
    <div class="tto-popup">
        <div class="popup-body">
            <form id="form1" runat="server" class="tto-form popup-form">
            <div class="input-tips">
                <i class="icon icon-warn"></i>为了您的资金安全，您所绑定的所有银行卡都必须在一个持卡人名下
            </div>
            <div class="input-group">
                <label class="lab">
                    持卡人姓名：</label>
                <div id="txtPayName" class="form-info">
                    </div>
            </div>
            <div class="input-group">
                <label class="lab">
                    选择银行：</label>
                <asp:DropDownList ID="ddlBank" runat="server">
                </asp:DropDownList>
            </div>
            <div class="input-group">
                <label class="lab">
                    开户行地址：</label>
                <input id="txtAddress" type="text" value="" class="ipt" placeholder="请输入开户行地址" />
            </div>
            <div class="input-group">
                <label class="lab">
                    银行卡号：</label>
                <input id="txtPayAccount" type="text" value="" class="ipt" placeholder="请输入银行卡号"
                    onkeyup="chkPrice(this)" />
            </div>
            <div class="input-group">
                <label class="lab">
                    确认卡号：</label>
                <input id="txtPayAccount2" type="text" value="" class="ipt" placeholder="请输入银行卡号"
                    onkeyup="chkPrice(this)" />
            </div>
            <div class="input-group">
                <label class="lab">
                    资金密码：</label>
                <input id="txtPayPwd" onfocus="this.type='password'" value="" class="ipt" placeholder="请输入取款密码" />
            </div>
            <div class="btn-group">
                <input type="button" onclick="chkBankAdd();" value="设置" class="btn btn-primary" />
            </div>
            </form>
        </div>
    </div>
</body>
</html>
