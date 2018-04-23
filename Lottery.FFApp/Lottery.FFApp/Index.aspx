<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Lottery.WebApp.Index" %>

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
	<script type="text/javascript" src="/statics/js/jquery_json.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.js"></script>
    <script type="text/javascript" src="/statics/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/laytpl.js"></script>
    <script type="text/javascript" src="/statics/json/lottery.js?v=201704"></script>
    <script type="text/javascript" src="/statics/js/page.index.js"></script>
</head>
<body style="background: url(/statics/img/bg.jpg) no-repeat repeat #23293f;">
    <!-- 容器 start -->
    <div class="tto-container">
        <!-- #include file="/statics/include/header.html" -->
        <!-- #include file="/statics/include/lefter.html" -->
        <!-- 内容 start -->
        <div class="tto-content">
            <div class="tto-layout">
                <div class="tto-index">
                    <div class="index-section">
                        <div class="hot-rec">
                            <div class="lot-logo">
                                <img src="/statics/img/1001_hot.png" alt="" /></div>
                            <div id="lotteryname" class="lot-name">
                                重庆时时彩</div>
                            <div id="lotIssue" class="lot-issue">
                                </div>
                            <div id="lotNumber" class="lot-result">
                            </div>
                            <a href="javascript:;" onclick="ajaxCheckLotteryToUrl('/cqssc','cqssc')" class="to-betting">
                                &gt; 立即投注</a>
                        </div>
                    </div>
                    <div class="index-section">
                        <div class="tto-block has-title block-sm index-block index-games">
                            <div class="block-heading">
                                <h2 class="block-title">
                                    <i class="icon-title icon-game"></i>热门游戏</h2>
                            </div>
                            <div class="block-content">
                                <ul id="lotList" class="index-lots">
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="index-section">
                        <div class="tto-block has-title block-sm index-block index-lotcenter">
                            <div class="block-heading">
                                <h2 class="block-title">
                                    <i class="icon-title icon-notice"></i>彩票中心</h2>
                            </div>
                            <div class="block-content lottery-games lot-center">                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 内容 end -->
    </div>
    <!-- 容器 end -->
    <script type="text/javascript" src="/statics/js/jquery.flexslider-min.js"></script>
    <script type="text/javascript" src="/statics/js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/statics/js/global.js"></script>
    <script type="text/javascript" src="/statics/js/member.js"></script>
</body>
</html>
