<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="charge.aspx.cs" Inherits="Lottery.Web.money.charge" %>

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
    <script src="/statics/js/page.charge.js" type="text/javascript"></script>
</head>
<body>
    <div class="account-recharge">
        <ul id="chargeSet" class="recharge-way">
        </ul>
        <ol class="process-steps">
            <li id="s1class" class="current"><span class="step-no">01</span> <span class="step-name">
                选择银行填写金额</span></li>
            <li id="s2class"><span class="step-no">02</span> <span class="step-name">确认充值信息</span>
            </li>
            <li id="s3class"><span class="step-no">03</span> <span class="step-name">登录网上银行汇款</span>
            </li>
        </ol>
        <div id="step1" class="recharge-main">
            <form action="" method="post" class="tto-form recharge-form">
            <div class="choose-bank" id="choose-bank">
            </div>
            <div class="recharge-tips">
                <p>充值金额：单笔最低充值金额为<span id="minCharge" class="wrong-amount"></span>元，最高<span id="maxCharge" class="wrong-amount"></span>元</p>
                <p>充值时间：<span id="startTime" class="wrong-amount"></span>至<span id="endTime" class="wrong-amount"></span></p>
                <p id="pZfb"></p>
            </div>
            <div id="divName" class="input-group">
                <label class="lab">
                    <span id="zfName"></span>姓名：</label>
                <input type="text" maxlength="8" class="ipt" id="txtName" />
                <span class="unit"></span>
                <div class="recharge-limit">
                    <span class="wrong-amount"></span>
                </div>
            </div>
            <div class="input-group">
                <label class="lab">
                    充值金额：</label>
                <input type="text" onkeypress="chkPrice(this)" onkeydown="chkPrice(this)" maxlength="8"
                    class="ipt" id="recharge-money" />
                <span class="unit">元</span>
                <div class="recharge-limit">
                    <span class="wrong-amount" id="wrong-amount"></span>
                </div>
                <ul class="choose-money" data-input="recharge-money">
                    <li data-money="500">500</li>
                    <li data-money="1000">1000</li>
                    <li data-money="2000">2000</li>
                    <li data-money="3000">3000</li>
                    <li data-money="5000">5000</li>
                    <li data-money="10000">10000</li>
                </ul>
            </div>
            <div class="btn-group">
                <input type="button" value="下一步" onclick="step2Post()" class="btn btn-primary" />
                <input type="hidden" id="txtAdminId" value="<%=AdminId %>" />
                <input type="hidden" id="txtAdminName" value="<%=AdminName %>" />
            </div>
            </form>
        </div>
        <div id="step2" class="recharge-main" style="display: none;">
            <div class="recharge-part">
                <div class="part-heading">
                    <h3 class="part-title">
                        <span class="no">2</span>收款方信息</h3>
                    <span class="extra">以下信息是确保您充值到账的重要信息</span> <a href="#" class="btn payment-help">付款帮助</a>
                </div>
                <div class="part-content">
                    <div class="payment-tips">
                        <p>
                            为保障充值成功<br />
                            请在15分钟内完成支付
                        </p>
                        <div class="payment-countdown" id="payment-countdown" data-ms="900">
                        </div>
                    </div>
                    <ul id="chargeInfo" class="recharge-info">
                    </ul>
                    <div class="online-payment">
                        <input type="button" value="确认支付" onclick="step3Post()" class="btn btn-primary btn-lg" />
                        <a id="link"></a>
                        <a href="/aspx/list.aspx?nav=ChargeList" target="_parent" class="btn btn-primary btn-lg">查看充值记录</a>
                    </div>
                </div>
            </div>
        </div>
        <div id="step3" class="recharge-main" style="display: none;">
            <div class="recharge-completed">
                <p>
                    请在弹出的网上银行页面完成付款<br />
                    充值遇到问题？ 查看<a href="#">新手帮助</a>
                </p>
                <div class="btn-group">
                    <a href="/money/charge.aspx class="btn btn-primary btn-lg">已完成充值</a> 
                    <a href="/aspx/list.aspx?nav=ChargeList" class="btn btn-primary btn-lg">查看充值记录</a>
                </div>
            </div>
        </div>
    </div>
</body>
    <script type="text/javascript" src="/statics/js/global.js"></script>
    <script type="text/javascript" src="/statics/js/member.js"></script>
</html>
