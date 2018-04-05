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
                        <div class="tto-block index-block index-banner">
                            <div class="focus-slide" id="focus-slide">
                                <div class="slide-content">
                                    <div class="slide-panel">
                                        <a href="#">
                                            <img src="/statics/img/focus_banner1.png" alt=""></a></div>
                                    <div class="slide-panel">
                                        <a href="#">
                                            <img src="/statics/img/focus_banner2.png" alt=""></a></div>
                                    <div class="slide-panel">
                                        <a href="#">
                                            <img src="/statics/img/focus_banner3.png" alt=""></a></div>
                                    <div class="slide-panel">
                                        <a href="#">
                                            <img src="/statics/img/focus_banner4.png" alt=""></a></div>
                                    <div class="slide-panel">
                                        <a href="#">
                                            <img src="/statics/img/focus_banner5.png" alt=""></a></div>
                                    <div class="slide-panel">
                                        <a href="#">
                                            <img src="/statics/img/focus_banner6.png" alt=""></a></div>
                                    <div class="slide-panel">
                                        <a href="#">
                                            <img src="/statics/img/focus_banner7.png" alt=""></a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="index-section">
                        <div class="tto-block index-block index-member">
                            <div class="i-account">
                                <div class="i-avatar">
                                    <img src="/statics/img/acc_avatar.png" alt="" /></div>
                                <div class="username">
                                    欢迎您，<em id="username2"></em></div>
                                <div id="bindinfo" class="account-auth">
                                </div>
                                <a href="/vip.c" class="to-center">完善个人安全资料</a>
                            </div>
                            <div class="account-info">
                                <div class="account-balance">
                                    余额：<span id="money2" class="amount">0.00</span>元<a href="javascript:;" onclick="ajaxUserBindInfo()"
                                        class="btn-refresh"></a>
                                </div>
                                <div class="account-acts">
                                    <a href="/vip.m" class="btn btn-act btn-recharge">充&nbsp;值</a> 
                                    <a href="/vip.m1" class="btn btn-act btn-withdraw">提&nbsp;现</a>
                                </div>
                            </div>
                            <a href="/vip.c" class="btn btn-center">进入我的账户</a>
                        </div>
                    </div>
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
                        <div class="tto-block has-title block-sm index-block index-notice">
                            <div class="block-heading">
                                <h2 class="block-title">
                                    <i class="icon-title icon-notice"></i>网站公告</h2>
                                <a href="javascript:;" onclick="LayerPop('系统公告', '800px', '550px', '/news/newsindex.html');" class="block-more">&gt; 更多</a>
                            </div>
                            <div class="block-content">
                                <ul id="list-notice" class="notice-list">
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="index-section">
                        <div class="tto-block has-title block-sm index-block index-safety">
                            <div class="block-heading">
                                <h2 class="block-title">
                                    <i class="icon-title icon-safety"></i>安全中心</h2>
                            </div>
                            <div class="block-content">
                                <div class="safety-center">
                                    <div class="security-level">
                                        <h4>
                                            安全等级：</h4>
                                        <div class="security-bar">
                                        </div>
                                        <span id="userLevel" class="level">中</span> <a href="/vip.c" class="btn btn-safety">
                                            提升安全等级</a>
                                    </div>
                                    <div class="history-login">
                                        <h3>
                                            <i class="icon icon-loc"></i>最近登录信息</h3>
                                        <ul id="UserLoginlist">
                                        </ul>
                                    </div>
                                </div>
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
