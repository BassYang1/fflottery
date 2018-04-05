<%@ Page Language="C#" CodeBehind="getcash.aspx.cs" Inherits="Lottery.WebApp.money.getcash" %>

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
    <script src="/statics/js/page.getcash.js" type="text/javascript"></script>
</head>
<body>
    <div class="account-withdraw" id="account-withdraw">
        <form method="post" class="tto-form withdraw-form">
        <div class="withdraw-process">
            <div class="withdraw-step">
                <div class="step-heading">
                    <i class="icon icon-no icon-n1"></i>选择银行卡或取款账户
                </div>
                <div class="step-content">
                    <div class="account-bank">
                        <ul id="userBank" class="bankcard-list">

                        </ul>
                    </div>
                </div>
            </div>
            <div class="withdraw-step">
                <div class="step-heading">
                    <i class="icon icon-no icon-n2"></i>请输入提款金额
                </div>
                <div class="step-content">
                    <div class="input-group">
                        <label class="lab">
                            提款金额：</label>
                        <input id="txtPayMoney" type="text" value="" class="ipt" onkeypress="chkPrice(this)"
                            placeholder="请输入取款金额" />
                        <span class="unit">元</span>
                    </div>
                    <div class="input-group">
                        <label class="lab">
                            金额大写：</label>
                        <input id="moneyUpper" type="text" value="" class="ipt" />
                        <span class="unit">元</span>
                    </div>
                    <div class="input-group">
                        <label class="lab">
                            温馨提示：</label>
                        <div class="form-info">
                            您今天还有<font id="synum"></font>次提款免手续费特权</div>
                    </div>
                    <div class="withdraw-help">
                        <table class="withdraw-table">
                            <colgroup>
                                <col class="w150" />
                                <col class="w150" />
                                <col class="w150" />
                                <col class="w130" />
                            </colgroup>
                            <tr>
                                <td class="name">
                                    可提金额
                                </td>
                                <td>
                                    <font id="money"></font>元
                                </td>
                                <td class="name">
                                    彩票所需消费量
                                </td>
                                <td>
                                    0元
                                </td>
                            </tr>
                            <tr>
                                <td class="name">
                                    今日已经提现
                                </td>
                                <td>
                                    <font id="txcs"></font>次
                                </td>
                                <td class="name">
                                    今日成功提现
                                </td>
                                <td>
                                    <font id="txje"></font>元
                                </td>
                            </tr>
                            <tr>
                                <td class="name">
                                    取款时间
                                </td>
                                <td>
                                    <span id="PriceTime1"></span>-<span id="PriceTime2"></span>
                                </td>
                                <td class="name">
                                    单笔限额
                                </td>
                                <td>
                                    <font id="PriceOut"></font>- <font id="PriceOut2"></font>元
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="name">
                                    手续费说明
                                </td>
                                <td colspan="3">
                                单笔提款金额1%，最小手续费1元，最高25元。
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="withdraw-step">
                <div class="step-heading">
                    <i class="icon icon-no icon-n3"></i>输入资金密码
                </div>
                <div class="step-content">
                    <div class="input-group">
                        <label class="lab">
                            资金密码：</label>
                        <input id="txtNewPass1" value="" class="ipt" onfocus="this.type='password'" autocomplete="off"
                            placeholder="请输入资金密码" />
                    </div>
                    <div class="btn-group">
                        <input type="button" value="确定取款" onclick="chkPost();" class="btn btn-bg btn-primary" />
                        <input type="hidden" value="0" id="lblBankId" />
                        <input type="hidden" value="0" id="lblUserBankId" />
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
