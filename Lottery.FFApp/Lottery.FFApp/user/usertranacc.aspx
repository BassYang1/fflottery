
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usertranacc.aspx.cs" Inherits="Lottery.WebApp.user.usertranacc" %>

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
            ajaxList(page);
            $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
                emAlert(msg);
            },
                onsuccess: function () { return true; }
            });
            $("#txtMoney").formValidator({ tipid: "tipUserName", onshow: "请输入会员账号", onfocus: "由0-9，z-a,A-Z组成的6-16位的字符" }).InputValidator({ min: 5, max: 20, onerror: "会员账号为5-20个字符" }).RegexValidator({ regexp: "username", datatype: "enum", onerror: "汉字或字母开头,不支持符号" });
            $("#txtNewPass1").formValidator({ tipid: "tipPoint", onshow: "请输入返点", onfocus: "请输入返点" }).InputValidator({ min: 1, max: 5, onerror: "请输入返点" });
        });

        /*最后的表单验证*/
        function CheckFormSubmit() {
            var money = $("#txtMoney").val();
            var passwoed = $("#txtNewPass1").val();
            if (money.length < 1) {
                emAlert("请输入转账金额");
                return false;
            }
            if (passwoed.length < 1) {
                emAlert("请输入资金民吗");
                return false;
            }
            Lottery.Loading.show("正在处理，请等待...");
            return true;
        }
    </script>
</head>
<body>
    <div class="tto-popup">
        <div class="popup-body">
            <form id="form1" runat="server" onsubmit="return CheckFormSubmit()" class="tto-form popup-form">
            <div class="input-tips">
                <i class="icon icon-warn"></i><asp:Label ID="lblMsg" runat="server" 
                    Text="活动转账计算盈亏，其他转账不计算盈亏" ForeColor="Red"></asp:Label>
            </div>
            <div class="input-group">
                <label class="lab">
                    您的余额：</label>
                <span class="ipt">
                    <%=strUserMoney%></span>
                <asp:TextBox ID="txtId" runat="server" MaxLength="20" ReadOnly="True" Visible="false"></asp:TextBox>
            </div>
            <div class="input-group">
                <label class="lab">
                    转入会员：</label>
                <span class="ipt">
                    <%=strUserName%></span>
            </div>
            <div class="input-group">
                <label class="lab">
                    转账金额：
                </label>
                &nbsp;<asp:TextBox ID="txtMoney" runat="server" MaxLength="8" CssClass="ipt" placeholder="请输转账金额"></asp:TextBox>
                <span id="tipMoney"></span>
            </div>
            <div class="input-group">
                <label class="lab">
                    转账类型：</label>
                <label class="radio">
                    <asp:RadioButton ID="rdo1" runat="server" GroupName="type" Checked="true" />活动</label>
                <label class="radio">
                    <asp:RadioButton ID="rdo2" runat="server" GroupName="type" />其他</label>
            </div>
            <div class="input-group">
                <label class="lab">
                    资金密码：</label>
                <asp:TextBox ID="txtNewPass1" runat="server" MaxLength="14" CssClass="ipt" TextMode="Password"
                    placeholder="请输资金密码"></asp:TextBox>
                <span id="tipNewPass1"></span>
            </div>
            <div class="btn-group">
                <asp:Button ID="Button1" runat="server" Text="确认" CssClass="btn btn-bg btn-primary"
                    OnClick="btnSave_Click" />
            </div>
            </form>
        </div>
    </div>
</body>
</html>
