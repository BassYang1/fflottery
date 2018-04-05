<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Lottery.WebApp.center.index" %>

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
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.js"></script>
    <script type="text/javascript" src="/statics/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/laytpl.js"></script>
    <script type="text/javascript">
        var isBindBank = false;
        $(document).ready(function () {
            ajaxLoadUserInfo();
        });

        function ajaxLoadUserInfo() {
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "clienttime=" + Math.random(),
                url: "/ajax/ajaxCenter.aspx?oper=ajaxGetUserInfo",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    if (d.result == "1") {
                        var userlevel = 0;
                        if (d.table.length > 0) {
                            var t = d.table[0];
                            $("#userName").html(t.username);
                            $("#loginInfo").html('<h4>上次登录IP：</h4><p>' + t.ip + ' [' + t.address + "]</p><h4>上次登录时间：</h4><p>" + t.logintime + '</p>');

                            $("#point").html(t.point);
                            $("#money2").html(t.money);
                            $("#money3").html(t.money);
                            $("#money4").html(t.money);
                            $("#money5").html(t.money);

                            if (t.istruename == "1") {
                                isBindBank = true;
                                userlevel++;
                                $("#isTrueName").removeClass().addClass("certified");
                                $("#isTrueName2").removeClass().addClass("bound");
                                $("#isTrueOper").html("<a href='javascript:;' class='btn btn-set btn-bound'>已绑定</a>");
//                                $("#isTrueOper").html("<a href='javascript:;' onclick='LayerPop(\"修改真实姓名\",\"650px\",\"450px\",\"/center/trueNameEdit.aspx\")' class='btn btn-set btn-bound'>已绑定</a>");
                            }
                            else {
                                isBindBank = false;
                                $("#isTrueName").removeClass();
                                $("#isTrueName2").removeClass();
                                $("#isTrueOper").html("<a href='javascript:;' onclick='LayerPop(\"绑定真实姓名\",\"650px\",\"400px\",\"/center/trueName.html\")' class='btn btn-set btn-bind'>立即绑定</a>");
                            }
                            if (t.isemail == "1") {
                                userlevel++;
                                $("#isEmail").removeClass().addClass("certified");
                                $("#isEmail2").removeClass().addClass("bound");
                                $("#isEmailOper").html("<a href='javascript:;' class='btn btn-set btn-bound'>已绑定</a>");
                            }
                            else {
                                $("#isEmail").removeClass();
                                $("#isEmail2").removeClass();
                                $("#isEmailOper").html("<a href='javascript:;' onclick='LayerPop(\"绑定邮箱地址\",\"650px\",\"400px\",\"/center/email.html\")' class='btn btn-set btn-bind'>立即绑定</a>");
                            }
                            if (t.ismobile == "1") {
                                userlevel++;
                                $("#isMobile").removeClass().addClass("certified");
                                $("#isMobile2").removeClass().addClass("bound");
                                $("#isMolbileOper").html("<a href='javascript:;' class='btn btn-set btn-bound'>已绑定</a>");
                            }
                            else {
                                $("#isMobile").removeClass();
                                $("#isMobile2").removeClass();
                                $("#isMolbileOper").html("<a href='javascript:;' onclick='LayerPop(\"绑定手机号\",\"650px\",\"400px\",\"/center/mobile.html\")' class='btn btn-set btn-bind'>立即绑定</a>");
                            }
                            if (t.isanswer == "1") {
                                userlevel++;
                                $("#isAnswer").removeClass().addClass("bound");
                                $("#isAnswerOper").html("<a href='javascript:;' class='btn btn-set btn-bound'>已设置</a>");
                            }
                            else {
                                $("#isAnswer").removeClass();
                                $("#isAnswerOper").html("<a href='javascript:;' onclick='LayerPop(\"设置密保问题\",\"650px\",\"400px\",\"/center/userverify.html\")' class='btn btn-set btn-bind'>立即设置</a>");
                            }

                            if (userlevel == 0) {
                                $("#userLevel").html("低");
                            }
                            if (userlevel == 1) {
                                $("#userLevel").html("低");
                            }
                            if (userlevel == 2) {
                                $("#userLevel").html("中");
                            }
                            if (userlevel == 3) {
                                $("#userLevel").html("中");
                            }
                            if (userlevel == 4) {
                                $("#userLevel").html("高");
                            }
                        }
                        setTimeout('ajaxList()', 1000);
                    }
                    else {
                        window.location.href = '/login';
                    }
                    closeload(index);
                }
            });
        }

        function ajaxList() {
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=1&pagesize=1&clienttime=" + Math.random(),
                url: "/ajax/ajaxBank.aspx?oper=ajaxGetList",
                error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                success: function (d) {
                    var html = "";
                    if (d.result == "1") {
                        if (d.table.length > 0) {
                            for (var i = 0; i < d.table.length; i++) {
                                var t = d.table[i];
                                html += "<li><span class='bank-logo'><img src='/statics/img/bank_36_icbc.png' alt='' /></span>";
                                html += "<p><span class='bank-name'>" + t.paybank + "</span><span class='bank-user'>" + t.payname + "</span></p>";
                                html += "<p>尾号" + t.payaccount.substr(t.payaccount.length - 4, 4) + "</p>";
                                html += "</li>";
                            }
                        }
                    }
					html += "<li class='add-card'><a href='javascript:;' onclick='BankLayerPop()'>";
                    html += "<i class='icon icon-add'></i>添加银行卡</a> </li>";
                    $("#userBank").html(html);
                }
            });
        }

        function BankLayerPop() {
            if (isBindBank)
                LayerPop("添加银行卡", "650px", "550px", "/center/bankInfo.aspx");
            else {
                parent.layer.confirm('您还没有绑定真实姓名？', {
                    icon: 3,
                    title: '温馨提示',
                    btn: ['立即绑定'],
                    shade: 0.2
                }, function () {
                    LayerPop("绑定真实姓名", "650px", "400px", "/center/trueName.html");
                });
            }  
        }
    </script>
</head>
<body style="background: url(/statics/img/bg.jpg) no-repeat repeat #23293f;">
    <div class="tto-container">
        <!-- #include file="/statics/include/header.html" -->
        <!-- #include file="/statics/include/lefter.html" -->
        <div class="tto-content">
            <div class="tto-layout">
                <div class="tto-block">
                    <div class="block-heading">
                        <div class="block-nav">
                            <div class="block-nav">
                                <!-- #include file="/statics/include/nav.html" -->
                            </div>
                        </div>
                    </div>
                    <div class="block-content">
                        <div class="personal-center">
                            <div class="i-account">
                                <div class="i-avatar">
                                    <img src="/statics/img/acc_avatar.png" alt="" /></div>
                                <div class="i-info">
                                    <div class="username">
                                        欢迎您，<em id="userName"></em></div>
                                    <div class="account-auth">
                                        <a href="javascript:;" id="isTrueName" class="verified"><i class="icon icon-user"></i>
                                        </a><a href="javascript:;" id="isEmail"><i class="icon icon-email"></i></a><a href="javascript:;"
                                            id="isMobile"><i class="icon icon-phone"></i></a>
                                    </div>
                                </div>
                                <div class="security-level">
                                    <h4>
                                        安全等级：</h4>
                                    <div class="security-bar">
                                        <span class="sec-line medium"></span>
                                    </div>
                                    <span id="userLevel" class="level">中</span>
                                </div>
                                <div id="loginInfo" class="last-login">
                                </div>
                            </div>
                            <div class="i-top">
                                <div class="agent-rebate">
                                    <h3>
                                        <i class="icon icon-rebate"></i>返点明细</h3>
                                    <ul>
                                        <li>彩票：<span id="point" class="pct">0.0%</span></li>
                                        <li>真人：<span class="pct">0.0%</span></li>
                                        <li>电子：<span class="pct">0.0%</span></li>
                                        <li>体育：<span class="pct">0.0%</span></li>
                                        <li>棋牌：<span class="pct">0.0%</span></li>
                                    </ul>
                                </div>
                                <div class="mybankcard">
                                    <h3>
                                        <i class="icon icon-card"></i>银行卡管理</h3>
                                    <ul id="userBank" class="bankcard-list">
                                    </ul>
                                </div>
                            </div>
                            <div class="i-bottom">
                                <div class="account-assets">
                                    <ul>
                                        <li class="assets-total"><i class="icon icon-wallet"></i>
                                            <h3>
                                                账户总额</h3>
                                            <div id="money2" class="amount">
                                                0.0000</div>
                                            <a href="/vip.m" class="btn btn-act">充值</a> <a href="javascript:;" onclick="ajaxRefresh()"
                                                class="btn-refresh"></a></li>
                                        <li class="assets-usable"><i class="icon icon-card"></i>
                                            <h3>
                                                可提金额</h3>
                                            <div id="money3" class="amount">
                                                0.0000</div>
                                            <a href="/vip.m1" class="btn btn-act">提现</a> <a href="javascript:;" onclick="ajaxRefresh()"
                                                class="btn-refresh"></a></li>
                                    </ul>
                                </div>
                                <div class="account-table">
                                    <table>
                                        <tr>
                                            <td>
                                                <span class="name">主账户：</span> <span id="money4" class="amount">0.0000</span>
                                                <a href="#" class="btn btn-out"></a>
                                            </td>
                                            <td>
                                                <span class="name">彩票：</span> <span id="money5" class="amount">0.0000</span>
                                                <a href="#" class="btn btn-out"></a>
                                            </td>
                                            <td>
                                                <span class="name">AG：</span> <span class="not-open">系统暂未开通</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="name">BBIN：</span> <span class="not-open">系统暂未开通</span>
                                            </td>
                                            <td>
                                                <span class="name">OG：</span> <span class="not-open">系统暂未开通</span>
                                            </td>
                                            <td>
                                                <span class="name">棋牌：</span> <span class="not-open">系统暂未开通</span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="security-settings">
                                    <ul>
										<li id="isTrueName2"><i class="icon icon-sec icon-user"></i>
                                            <h3>
                                                开户姓名</h3>
                                            <p>
                                                绑定玩家的开户姓名后，将无法自行修改，可保证资金的绝对安全。</p>
                                            <div id="isTrueOper">
                                            </div>
                                        </li>
                                        <li><i class="icon icon-sec icon-lock"></i>
                                            <h3>
                                                登录密码</h3>
                                            <p>
                                                我们建议玩家不定时的修改登陆密码，可提高账号的安全性。</p>
                                            <a href="javascript:;" onclick="LayerPop('修改登录密码','650px','400px','/center/userpwd.html')"
                                                class="btn btn-set btn-bind">修改</a> </li>
                                        <li><i class="icon icon-sec icon-shield"></i>
                                            <h3>
                                                资金密码</h3>
                                            <p>
                                                我们建议玩家不定时的修改资金密码，可提高账号的安全性。</p>
                                            <a href="javascript:;" onclick="LayerPop('修改资金密码','650px','400px','/center/bankpwd.html')"
                                                class="btn btn-set btn-bind">修改</a> </li>
                                        <li id="isAnswer"><i class="icon icon-sec icon-question"></i>
                                            <h3>
                                                密保保护</h3>
                                            <p>
                                                玩家忘记密码时可以自行通过密保答案找回您的登陆、资金密码。</p>
                                            <div id="isAnswerOper">
                                            </div>
                                        </li>
                                        <li id="isEmail2"><i class="icon icon-sec icon-email"></i>
                                            <h3>
                                                邮箱验证</h3>
                                            <p>
                                                绑定邮箱，增加账户安全性。</p>
                                            <div id="isEmailOper">
                                            </div>
                                        </li>
                                        <li id="isMobile2"><i class="icon icon-sec icon-phone"></i>
                                            <h3>
                                                手机验证</h3>
                                            <p>
                                                绑定手机，增加账户安全性。</p>
                                            <div id="isMolbileOper">
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
