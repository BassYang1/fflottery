<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="charge.aspx.cs" Inherits="Lottery.Web.money.charge" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>立博国际娱乐</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=no">
    <meta name="format-detection" content="telephone=no,email=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/style.css" />
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.min.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/jquery_json.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.center.js"></script>
    <script src="/statics/sytle/js/EM.tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            _jcms_GetRefreshCode('imgCode');
            $i("mincharge").innerHTML = site.MinCharge;
            $.formValidator.initConfig({ onError: function (msg) { emAlert(msg); } });
            $("#txtPayMoney").formValidator({ tipid: "tipPayMoney", onshow: "请输入充值金额", onfocus: "请输入充值金额", defaultvalue: "" }).InputValidator({ min: 1, max: 10, onerror: "请输入充值金额" }).RegexValidator({ regexp: "^([1-9]{1}[0-9]{0,8})$", onerror: "请输入整数" });
            $("#txtUserCode").formValidator({ tipid: "tipUserCode", onshow: "请输入验证码", onfocus: "验证码必须填写" }).InputValidator({ min: 4, max: 4, onerror: "验证码必须为4位" });
            //金额大写
            $("#txtPayMoney").delegate('', 'keyup', function (event) {
                chkPrice(this);
                $i('moneyUpper').innerHTML = atoc($('#txtPayMoney').val());
            });
        });

        function chkPost() {
            var line = $("#ddlLine").val();
            var bank = $("#ddlBank").val();
            var money = $("#txtPayMoney").val();
            var code = $("#txtUserCode").val();

            if ($.formValidator.PageIsValid('1')) {
                if (eval(money) < eval(site.MinCharge)) {
                    emAlert('充值金额不能小于最小充值金额');
                    return;
                }
                else {
                    emAlert('暂不支持手机端支付，请在PC端充值！');
                    return;
                    //var url = "http://pay.youle2016.com/sign" + line + ".aspx?Bank=" + bank + "&Id=" + line + "&Amount=" + money + "&UserId=<%=AdminId %>";
                    //goTo(url);
                }
            }
        }

        function goTo(url) {
            var targetWndName = "MyWindow";
            var wnd = window.open("", targetWndName);
            var link = document.getElementById("link");
            link.target = targetWndName;
            link.href = url;
            link.click();
        }

        function openwin(url) {
            var a = document.createElement("a");
            a.setAttribute("href", url);
            a.setAttribute("target", "_blank");
            a.setAttribute("id", "openwin");
            document.body.appendChild(a);
            a.click();
        }
    </script>
</head>
<body>
    <div class="zj-page">
        <header class="header">
                <h1 class="title">充&nbsp;&nbsp;&nbsp;&nbsp;值</h1>
                <a href="javascript:history.go(-1);" class="back"></a>
            </header>
        <div class="content">
            <div class="zj-recharge">
                <form id="form1" runat="server" class="zj-form recharge-form">
                <div class="balance">
                    <i class="icon-money"></i>当前余额<em id="money">0.00</em>元<span id="name" style="display: none;"></span>
                </div>
                <div class="form-item">
                    <label>
                        充值线路：</label>
                    <asp:DropDownList ID="ddlLine" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-item">
                    <label>
                        选择银行：</label>
                    <asp:DropDownList ID="ddlBank" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-item recharge-amount">
                    <label>
                        充值金额：</label>
                    <input id="txtPayMoney" type="text" value="" class="ipt" onkeypress="chkPrice(this)" placeholder="请输入充值金额" />
                    <span class="unit">元</span>
                </div>
                <div class="form-item">
                    <label>
                        金额大写：</label>
                    <span id="moneyUpper"></span>
                </div>
                <div class="form-item code-item">
                    <label>
                        验证码：</label>
                    <input id="txtUserCode" type="text" class="ipt" maxlength="4" size="4" />
                    <img id="imgCode" onclick="_jcms_GetRefreshCode('imgCode');" src="" alt="点击更换" class="code" placeholder="请输入验证码" />
                </div>
                <div class="btn-item">
                    <input type="button" value="下一步" class="btn primary-btn" onclick="chkPost();" />
                    <a id="link" href="javascript:void(0)" style="visibility: hidden; position: absolute;">
                    </a>
                </div>
                <div class="zj-info account-info">
                        平台最低充值 <font id="mincharge" style="color: Red;"></font>元！ 在线充值为即时到账，超过5分钟没有到账请联系客服进行处理！
                </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
