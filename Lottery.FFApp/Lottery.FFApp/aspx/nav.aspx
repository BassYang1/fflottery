<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav.aspx.cs" Inherits="Lottery.Web.nav" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>立博国际娱乐</title>
    <link rel="stylesheet" type="text/css" href="/statics/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/statics/css/lottery.css" />
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/statics/global.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
    <script src="/statics/laytpl.js" type="text/javascript"></script>
    <script src="/statics/json/json_<%=AdminPoint %>.js" type="text/javascript"></script>
    <script src="/statics/json/<%=loId %>.js" type="text/javascript"></script>
    <script src="/statics/json/BigAndSmalldata.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/jquery_json.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.init.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.nav.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.lotTime.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.lottery.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.jquery2.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.jquery3.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.jquery4.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.jquery5.js?v=201704" type="text/javascript"></script>
    <script src="/statics/js/EM.betoper.js?v=201704" type="text/javascript"></script>
</head>
<body style="background: url(/statics/img/bg.jpg) no-repeat repeat #23293f;">
    <div class="tto-container">
        <!-- #include file="/statics/include/header.html" -->
         <!-- #include file="/statics/include/lefter.html" -->
        <div class="tto-content">
            <div class="tto-layout">
                <div class="lottery-container lottery-ssc" data-cls="lottery-ssc" id="lottery-container">
                    <!-- 彩票头部 start -->
                    <div id="lottery-header" class="lottery-header">
                    </div>
                    <!-- 彩票头部 end -->
                    <!-- 更多彩种 start -->
                    <a href="javascript:;" class="lottery-more" id="lottery-more"><i class="icon-8"></i>
                        更多彩种</a>
                    <div class="lottery-games" id="lottery-games">
                    </div>
                    <!-- 更多彩种 end -->
                    <!-- 彩票主内容 start -->
                    <div class="lottery-content">
                        <!-- 选号区 start -->
                        <div class="lottery-area" id="lottery" style="min-height: 430px;">
                            <ul id="bigtype" class="lottery-playgroup">
                            </ul>
                            <div class="lottery-panel">
                                <div id="smalltype" class="lottery-play">
                                </div>
                                <div class="lottery-subpanel">
                                    <div class="lottery-playinfo">
                                        <div id="remark" class="play-info">
                                        </div>
                                        <div class="lottery-example">
                                            <i class="icon icon-i"></i>
                                            <div id="example" class="play-example">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="lottery-bonus">
                                        <i class="icon icon-cny"></i>当前玩法奖金：<span id="bonus" class="money">0.0000</span>元
                                    </div>
                                    <div id="spchoose">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- 选号区 end -->
                        <!-- 投注区 start -->
                        <div class="betting-area">
                            <div class="betting-settings">
                                <a href="javascript:;" onclick="lotteryAdd()" class="btn betting-add" id="betting-add">
                                    <i class="icon icon-add"></i>添加号码</a>
                                <div class="betting-number">
                                    已选<span id="fromBuyNumberCount" class="amount">0</span>注
                                </div>
                                <div class="betting-subtotal">
                                    共<span id="fromBuyPriceTotal" class="money">0.00</span>元
                                </div>
                                <div class="betting-multi">
                                    <span class="lab">倍数</span>
                                    <div class="betting-amount" id="betting-amount">
                                        <a href="javascript:;" class="minus"></a>
                                        <input id="fromTimes" type="text" value="1" maxlength="4" class="ipt number" />
                                        <a href="javascript:;" class="plus"></a>
                                    </div>
                                </div>
                                <div class="betting-mode">
                                    <span class="lab">模式</span>
                                    <ul class="optgroup" id="betting-mode">
                                        <li number="1" class="option selected">元</li>
                                        <li number="0.1" class="option">角</li>
                                        <li number="0.01" class="option">分</li>
                                        <li number="0.001" class="option">厘</li>
                                    </ul>
                                </div>
                                <div class="betting-bonus">
                                    奖金<span id="bonus2" class="money">0.00</span>元
                                </div>
                                <div class="betting-adjust" id="betting-adjust">
                                    <span class="lab">调节</span>
                                    <div class="betting-slider">
                                        <span class="cover"></span>
                                    </div>
                                    <span id="bonusInfo" class="ratio">0% | 0</span>
                                </div>
                            </div>
                            <div class="lottery-tabs" id="lottery-tabs">
                                <ul class="tabs-nav">
                                    <li nmb="1" class="ui-state-active">我的投注</li>
                                    <li nmb="2">我的追号</li>
                                </ul>
                                <div class="tabs-panel" id="mybetting">
                                </div>
                            </div>
                            <div class="lottery-basket">
                                <div class="betting-actions">
                                    <a href="javascript:;" onclick="ajaxQuickBetView()" class="btn btn-betting"><i class="icon icon-bet">
                                    </i>一键下注</a> <a href="javascript:;" onclick="lotteryEmpty()" class="btn betting-clear">
                                        <i class="icon icon-clear"></i>清空投注</a> <a href="javascript:;" onclick="OpenZhBet()"
                                            class="btn"><i class="icon icon-append"></i>我要追号</a>
                                    <input type="hidden" id="txtLoid" value="<%=loId %>" />
                                    <input type="hidden" id="txtTid" value="<%=tId %>" />
                                    <input type="hidden" id="txtUserPoint" value="<%=AdminPoint %>" />
                                    <input type="hidden" id="lotTime" value="0" />
                                    <input type="hidden" id="lotBonus" value="0" />
                                    <input type="hidden" id="lotBonus2" value="0" />
                                </div>
                                <div class="betting-lottery">
                                    <ul class="betting-list" id="betting-list">
                                    </ul>
                                </div>
                            </div>
                            <div class="betting-bottom">
                                <div class="betting-total">
                                    总计：<em id="sumOrder" class="total-amount">0</em>单， 共<em id="fromBuyNumberSumCount"
                                        class="total-money">0</em>注， 总计<em id="fromBuyPriceSumTotal" class="balance">0.00</em>元
                                </div>
                                <a href="javascript:;" onclick="ajaxBetView()" class="btn betting-confirm" id="betting-confirm">
                                    <span class="text">确认投注</span> <i class="icon icon-clock"></i>投注截止<span class="confirm-countdown"
                                        id="confirm-countdown"></span> </a>
                            </div>
                        </div>
                        <!-- 投注区 end -->
                    </div>
                    <!-- 彩票主内容 end -->
                </div>
            </div>
        </div>
        <div class="tto-popup tto-popup-md" style="left: 50%; top: 50%; margin: -295px 0 0 -400px;
            display: none;" id="zhuihao">
            <div class="popup-header">
                <h3 class="popup-title">
                    <i class="icon-title icon-doc"></i>我要追号</h3>
                <span class="popup-close" onclick="CloseZhBet()"><i class="icon-close"></i></span>
            </div>
            <div class="popup-body">
                <div class="popup-body">
                    <div class="myappend">
                        <label class="append-stop">
                            <input type="checkbox" value="" checked="checked" name="isStop" />中奖后停止追号</label>
                        <div class="tto-tabs">
                            <ul class="tabs-nav">
                                <li id="t1" class="first ui-state-active" nmb="1">同倍追号</li>
                                <li nmb="2">翻倍追号</li>
                                <li class="last" nmb="3">利润率追号</li>
                            </ul>
                            <div class="tabs-panel" id="tp-tb">
                                <form action="" method="post" class="append-form">
                                <div class="input-group">
                                    <span class="input-wrap">
                                        <label class="lab">
                                            追号期数：</label>
                                        <select id="cbkQs" onchange="change()">
                                            <option value="5" selected>5期</option>
                                            <option value="10">10期</option>
                                            <option value="20">20期</option>
                                            <option value="30">30期</option>
                                            <option value="50">50期</option>
                                        </select>
                                    </span><span class="kv"><span class="k">总倍数：</span> <span id="spanSumQS" class="v">0</span>
                                    </span><span class="kv"><span class="k">总金额：</span> <span id="spanSumTotal" class="v">
                                        0.00 元</span> </span>
                                </div>
                                <div class="input-group" id="zhuihaoQuery">
                                </div>
                                <input type="button" onclick="generateZH()" value="生成追号单" class="btn btn-primary btn-append" />
                                </form>
                            </div>
                        </div>
                        <div class="append-table">
                            <div class="table-head">
                                <span class="chk">选择</span> <span class="issue">期号</span> <span class="multi">倍数</span>
                                <span class="money">金额</span> <span class="date">代购截止时间</span>
                            </div>
                            <div class="table-body">
                                <ul class="append-list" id="zhlist">
                                </ul>
                            </div>
                        </div>
                        <div class="append-actions">
                            <a href="javascript:;" onclick="CloseZhBet()" class="btn btn-default btn-cancel">取消</a>
                            <a href="javascript:;" onclick="ajaxZHBetView()" class="btn btn-primary">确认追号</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="tto-shade" id="zhbg" style="display: none;">
        </div>
        <script type="text/javascript" src="/statics/js/jquery-ui.min.js"></script>
        <script type="text/javascript" src="/statics/js/global.js"></script>
        <script type="text/javascript" src="/statics/js/lottery.js"></script>
</body>
</html>
