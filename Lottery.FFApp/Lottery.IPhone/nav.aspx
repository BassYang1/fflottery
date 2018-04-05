<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav.aspx.cs" Inherits="Lottery.Web.nav" %>

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
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.min.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/jquery_json.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.header.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.navinit.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.lottery.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.operBet.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.input.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.lotteryTime.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.nav.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.jquery2.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.jquery3.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.jquery4.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.jquery5.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/json/json_<%=AdminPoint %>.js"></script>
    <script src="/statics/sytle/js/EM.center.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/json/Lottery_Json.js"></script>
    <script type="text/javascript" src="/statics/json/BigAndSmalldata.js"></script>
    <script src="/statics/sytle/js/sharedata.js" type="text/javascript"></script>
</head>
<body>
    <div class="container">
        <header id="header">
        <h1 class="title">购彩大厅</h1>
        <a href="game.html" class="back"></a>
        <a href="#" class="help"></a>
    </header>
        <main id="main">
        <div id="add">
    	<div id="mainLottery" class="lottery-dropdown J_LotteryDropdown">
            <ul id="bigtype" class="lottery-pg J_LotteryPG">
            </ul>
            <ul class="lottery-p J_LotteryP" id="smalltype">
            </ul>
        </div>
        <div class="lt-lottery lottery-ssc" id="J_Lottery">
             <div class="lottery-recent J_LotteryRecent">
                <div class="lottery-name J_LotteryName" id="lotteryname"></div>
                <div class="lottery-last">
                    <div class="issue">第<em id="cursn"></em>期</div>
                    <div id="strnumber" class="number">
                       
                    </div>
                </div>
                <ul id="ajaxListNav" class="lottery-records J_LotteryRecords">
                   
                </ul>
            </div>
            <div class="lottery-hd">
            	<span id="playTypeName" class="selected-pg J_SelectedPG"></span>
               	<span id="playName" class="selected-p J_SelectedP"></span>
                <a href="javascript:;" class="p-toggle J_LotteryPToggle"></a>
            </div>
            <div class="lottery-bd">
            	<div class="lottery-top">
                    <span class="endtime">
                        <i class="icon-clock"></i>距<span id="nestsn"></span><span id="states"></span>截止：<span id="lotterytime" class="countdown J_Countdown" data-ms="300000"></span>
                    </span>
                    <span onclick="CreateRandom()" class="shake"><i class="icon-shake" onclick="CreateRandom()"></i>摇一摇机选</span>
                </div>
                <div class="lottery-main J_LotteryMain">
                	<div class="lottery-panel J_LotteryPanel">
                   		<div id="projectxuehao" class="lottery-subpanel J_LotterySubPanel">

                        </div>
                    </div>
                </div>
            </div>
            <div class="confirm-betting">
            <div class="betting-fixed">
                            	<div class="betting-settings">
                	<div class="multi">
                        <div class="betting-amount">
                            <a href="javascript:;" class="minus"><i class="icon-minus"></i></a>
                            <input id="fromTimes" type="text" value="1" maxlength="4" class="number"/>
                            <a href="javascript:;" class="plus"><i class="icon-plus"></i></a>
                        </div>
                        <span class="s">倍</span>
                    </div>
                    <select id="model" onchange="SelectModel()" class="unit">
                    <option value="1">元</option>
                    <option value="0.1">角</option>
                    <option value="0.01">分</option>
                    <option value="0.001">厘</option>
                    </select>
                    <select id="points" onchange="SelectPoints()" class="point">
                    </select>
                </div>

                <div class="betting-bottom">
                	<div class="lottery-ft">
            
            	<i class="icon-list"></i>
                <span class="betting-info">
                    <span class="money">已选<em id="fromBuyNumberCount">0</em>注 共<em id="fromBuyPriceTotal">0.00</em>元</span>
                    <span class="tip">添加到投注单</span>
                </span>
                <a href="javascript:;" onclick="ajaxQuickBetView()" class="btn primary-btn betting-btn" style="width: 90px; padding: 0 3px; margin: 0 1px;"><i class="icon-doc" style="margin-right:1px;"></i>一键下注</a>
                <a href="javascript:;" onclick="AddRow()" class="btn primary-btn betting-btn" style="width: 60px; padding: 0 2px; margin: 0 1px;"><i class="icon-doc" style="margin-right:1px;"></i>添加</a>
                <input type="hidden" id="txtLoid" value="<%=loId %>" />
                <input type="hidden" id="txtTid" value="<%=tId %>" />
                <input type="hidden" id="MinTimes" value="<%=MinTimes %>" />
                <input type="hidden" id="MaxTimes" value="<%=MaxTimes %>" />
                <input type="hidden" id="txtUserPoint" value="<%=AdminPoint %>" />
                <span id="name" style="display: none;"></span>
                <span id="money" style="display: none;"></span>
            </div>
                </div>
            </div>
            </div>
            
        </div>
        </div>

        <div id="info" class="confirm-betting" style="display: none;">
        	<div class="betting-info">
            	<span class="name">时时彩</span>&nbsp;第<span id="nestsn2"></span>&nbsp;<span id="states2"></span>截止：<span id="lotterytime2" class="countdown">00:01:15</span>
            </div>
            <div class="betting-actions">
            	<a href="javascript:;" onclick="SelectRow()" class="btn primary-btn"><i class="icon-cart"></i>自选号码</a>
                <a href="javascript:;" onclick="CreateRandomRow()" class="btn primary-btn"><i class="icon-phone"></i>机选一注</a>
                <a href="javascript:;" onclick="ClearRow()" class="btn primary-btn"><i class="icon-clear"></i>清空号码</a>
            </div>

            <div class="betting-content">
            	<div class="betting-lottery">
                	<ul id="ajaxList" class="betting-list">
                    	
                    </ul>
                </div>
            </div>
            <div class="betting-fixed">
                <div class="betting-bottom">
                	<a href="javascript:;" onclick="OpenZhBet()" class="btn append-btn">我要追号</a>
                    <span class="info">
                        <em id="fromBuyPriceSumTotal" class="money">0.00</em>元<br/>
                        <em id="fromBuyNumberSumCount" class="n">0</em>注<em id="sumOrder" class="n">1</em>单
                    </span>
                    <a href="javascript:;" onclick="ajaxBetView()" class="btn primary-btn">确认投注</a>
                </div>
            </div>
        </div>

        <div id="zhuihao" class="my-append" style="display: none;">
        	<div class="lottery-append-tabs">
            	<ul class="tabs-nav">
                    <li class="active" nmb="1"><a href="javascript:;">同倍追号</a></li>
                    <li nmb="2"><a href="javascript:;">翻倍追号</a></li>
                    <li nmb="3"><a href="javascript:;">利润率追号</a></li>
                </ul>
                <div class="tabs-panels">
                	<div class="tabs-panel">
                    	<form action="" method="post" class="lt-form lottery-append-form">
                            <div class="form-item">
                                <div class="item-title"><label class="lab">中奖后操作</label></div>
                                <div class="item-content">
                                <select id="isStop">
                                    <option value="1">停止追号</option>
                                    <option value="0">继续追号</option>
                                </select>
                                </div>
                            </div>
                            <div class="form-item">
                               <div class="item-title"><label class="lab">追号期数</label></div>
                                <div class="item-content">
                                <select id="cbkQs" onchange="change()">
                                    <option value="5">5期</option>
                                    <option value="10">10期</option>
                                    <option value="20">20期</option>
                                    <option value="30">30期</option>
                                    <option value="50">50期</option>
                                </select>
                                </div>
                            </div>
                            <div class="form-item">
                               <div class="item-title"><label class="lab">追号倍数</label></div>
                                <div class="item-content">
                                <span id="spanSumQS" class="ipt"></span>倍
                                </div>
                            </div>
                            <div class="form-item">
                               <div class="item-title"><label class="lab">追号金额</label></div>
                                <div class="item-content">
                               <span id="spanSumTotal" class="ipt">1.00</span>元
                                </div>
                            </div>
                        <div id="zhuihaoQuery">
                        </div>
                        </form>
                    </div>
                 
                </div>
            </div>
            <div class="lottery-append">
            	<ul id="zhlist" class="append-list">
                	
                </ul>
            </div>
            <div class="append-fixed">
              <a href="javascript:;" onclick="CloseZhBet()" class="btn cancel-btn">取消</a> <a href="javascript:;"
                                    onclick="generateZH()" class="btn primary-btn create-btn">生成追号单</a>&nbsp;<a href="javascript:;"
                                onclick="ajaxZHBetView()" class="btn primary-btn create-btn">确认追号</a>
            </div>
            </div>
    </main>
    </div>
    <textarea class="template" id="tplList" rows="1" cols="1" style="display: none">
{#foreach $T.table as record}
<li>
<div class="lottery-infomav">
<span class="issue">&nbsp;{$T.record.title}</span>&nbsp;<span class="lottery-number">
{#if $T.record.type == "4001"}{$T.record.number}{#/if}
{#if $T.record.type != "4001"}{GetSytle($T.record.number)}{#/if}&nbsp;</span></div>
</li>
{#/for}
</textarea>
</body>
<script type="text/javascript" src="/statics/sytle/js/swiper.min.js"></script>
<script type="text/javascript" src="/statics/sytle/js/script.js"></script>
</html>
