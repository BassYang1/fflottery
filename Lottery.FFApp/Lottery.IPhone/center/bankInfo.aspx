<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bankInfo.aspx.cs" Inherits="Lottery.IPhone.center.bankInfo" %>

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
            ajaxList();
            $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
                emAlert(msg);
            },
                onsuccess: function () { return true; }
            });
            $("#txtPayName").formValidator({ tipid: "message", onshow: "请输入开户姓名", onfocus: "只能输入汉字", defaultvalue: "" }).InputValidator({ min: 1, onerror: "开户姓名不能为空" });
            $("#txtPayPwd").formValidator({ tipid: "message", onshow: "请输入资金密码", onfocus: "请正确输入资金密码", defaultvalue: "" }).InputValidator({ min: 6, onerror: "资金密码不低于6位" });
            $("#txtPayAccount").formValidator({ tipid: "message", onshow: "请输入银行卡号", onfocus: "请正确输入银行卡号", defaultvalue: "" }).InputValidator({ min: 1, onerror: "银行卡号位数不够" });
        });

        function ajaxList() {
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=1&pagesize=1&clienttime=" + Math.random(),
                url: "/ajax/ajaxBank.aspx?oper=ajaxGetList",
                error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                success: function (d) {
                    switch (d.result) {
                        case '-1':
                            top.window.location = '/login.html';
                            break;
                        case '1':
                            if (d.recordcount > 0) {
                                $("#add").hide();
                                $("#info").show();
                                $i('lblBank').innerHTML = d.table[0].paybank;
                                $i('lblPayAccount').innerHTML = d.table[0].payaccount;
                                $i('lblPayName').innerHTML = d.table[0].payname;
                                $i('lblAddress').innerHTML = d.table[0].paybankaddress;
                                $i('lblTime').innerHTML = d.table[0].addtime;
                            }
                            else {
                                $("#info").hide();
                                $("#add").show();
                            }
                            break;
                    }
                    closeload(index);
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
                var PayName = $("#txtPayName").val();
                var PayPwd = $("#txtPayPwd").val();

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
                                emAlert(d[0].message);
                                break;
                            case '1':
                                emAlert(d[0].message);
                                ajaxList();
                                $("#txtAddress").val("");
                                $('#txtPayAccount').val("");
                                $('#txtPayName').val("");
                                $('#txtPayPwd').val("");
                                break;
                        }
                        closeload(index);
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
    <div class="container">
        <header id="header">
        <h1 class="title">绑定银行卡</h1>
<a href="javascript:history.go(-1);" class="back"></a>
    </header>
        <main id="main">
        <div id="add" class="change-password">
            <form id="form1" runat="server" class="lt-form change-password-form">
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">绑定银行</label>
                    </div>
                    <div class="item-content">
                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="bankcard">
                    </asp:DropDownList>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">银行卡号</label>
                    </div>
                    <div class="item-content">
                      <input id="txtPayAccount" type="text" value="" class="ipt" placeholder="请输入银行卡号"
                        onkeyup="chkPrice(this)" />
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">开户姓名</label>
                    </div>
                    <div class="item-content">
                        <input id="txtPayName" type="text" value="" class="ipt" placeholder="请输入开户姓名" />
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">开户地址</label>
                    </div>
                    <div class="item-content">
                        <input id="txtAddress" type="text" value="" class="ipt" placeholder="请输入开户地址" />
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">取款密码</label>
                    </div>
                    <div class="item-content">
                        <input id="txtPayPwd" type="password" value="" class="ipt" placeholder="请输入取款密码" />
                    </div>
                </div>
                <div class="form-msg">
                    新密码包含6-16位字母和数字，不能和原密码相同
                </div>
                <div class="form-btns">
                   <input type="button" onclick="chkBankAdd();" value="设&nbsp;&nbsp;&nbsp;置" class="btn primary-btn" /> 
                </div>
            </form>
        </div>

        <div id="info" class="change-password">
            <form id="form2" class="lt-form change-password-form">
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">绑定银行</label>
                    </div>
                    <div class="item-content">
                      <span id="lblBank" class="ipt"></span>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">银行卡号</label>
                    </div>
                    <div class="item-content">
                      <span id="lblPayAccount" class="ipt"></span>
                    </div>
                </div>
                <div class="form-item">
                    <div class="item-title">
                        <label class="lab">开户姓名</label>
                    </div>
                    <div class="item-content">
                       <span id="lblPayName" class="ipt"></span>
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">开户地址</label>
                    </div>
                    <div class="item-content">
                         <span id="lblAddress" class="ipt"></span>
                    </div>
                </div>
                 <div class="form-item">
                    <div class="item-title">
                        <label class="lab">绑定时间</label>
                    </div>
                    <div class="item-content">
                        <span id="lblTime" class="ipt"></span>
                    </div>
                </div>
            </form>
        </div>
    </main>
    </div>
</body>
</html>
