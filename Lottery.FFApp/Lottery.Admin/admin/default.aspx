<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="default.aspx.cs" Inherits="Lottery.Admin._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>EM-CLUB后台管理系统</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script src="/statics/admin/js/yanue.pop.js" type="text/javascript"></script>
    <script src="/statics/admin/js/index.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/admin/js/admin.js"></script>
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript" src="/statics/admin/js/adm.notify.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link rel="stylesheet" type="text/css" href="/statics/admin/css/style.css" />
</head>
<body onload="resizeHeight()">
    <div class="container">
        <div class="header">
            <div class="system-name">
                <img src="/statics/admin/img/yl_logo.png" />
            </div>
            <div class="online-number">
                <i class="icon icon-u"></i>当前在线人数：PC端<span class="num" id="usercount">0</span>人，手机端<span class="num" id="usercount2">0</span>人
            </div>
            <div class="top-user">
                <img src="/statics/admin/img/user_photo.png" alt="" class="photo" />
                <div class="username">
                    <%=AdminName%></div>
                <div class="tu-extra">
                    <a href="javascript:void(0);" onclick="top.Lottery.Popup.show('adminPwd.aspx',600,300,true)">
                        <i class="icon icon-pen"></i>修改密码</a> <a href="javascript:void(0);" onclick="top.Lottery.Popup.show('adminLock.aspx',400,180,false)">
                            <i class="icon icon-lock"></i>锁屏</a> <a href="javascript:void(0);" onclick="top.top.Lottery.Confirm('您确定要退出后台管理吗?', 'chkLogout();');">
                                <i class="icon icon-exit"></i>安全退出</a>
                </div>
                <div class="tu-panel">
                    <ul class="tu-list">
                        <li><a href="javascript:void(0);" onclick="top.Lottery.Popup.show('adminPwd.aspx',600,300,true)"
                            class="mr10"><i class="icon icon-pen"></i>修改密码</a></li>
                        <li><a href="javascript:void(0);" onclick="top.Lottery.Popup.show('adminLock.aspx',400,180,false)">
                            <i class="icon icon-lock"></i>系统锁屏</a></li>
                        <li><a href="javascript:void(0);" onclick="top.top.Lottery.Confirm('您确定要退出后台管理吗?', 'chkLogout();');">
                            <i class="icon icon-exit"></i>安全退出</a></li>
                    </ul>
                    <span class="icon icon-arrow"></span>
                </div>
            </div>
            <ul class="top-nav">
                <li class="tn-tk" <%=act0 %>><a href="javascript:void(0);" onclick="ShowPage('银行信息','提现请求','/admin/conList.aspx?page=CashCheck');"
                    class="tn-title"><i class="icon icon-tn"></i><span class="num" id="cashcount">0</span>
                </a>
                    <div class="tn-content">
                        <dl class="tn-dl">
                            <dt>您有<span id="cashcount2">0</span>条提款请求需要处理</dt>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('银行信息','提现审核','/admin/conList.aspx?page=CashCheck');">
                                    <i class="icon icon-r"></i>查看所有提款请求</a></dd>
                        </dl>
                        <span class="icon icon-arrow"></span>
                    </div>
                </li>
                <li class="tn-jg" <%=act1 %>><a href="javascript:void(0);" class="tn-title"><i class="icon icon-tn">
                </i><span id="WarnCount" class="num">0</span> </a>
                    <div class="tn-content">
                        <dl class="tn-dl">
                            <dt>您有 <span id="WarnCount2">0</span> 条报警，请知晓</dt>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','中奖报警','/admin/conList.aspx?page=BetOfWinWarn');">
                                    <i class="icon icon-r"></i>当日中奖报警 <span id="c1">0</span> 条</a></dd>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','返点报警','/admin/conList.aspx?page=BetOfPointWarn');">
                                    <i class="icon icon-r"></i>当日返点报警 <span id="c2">0</span> 条</a></dd>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','盈利率报警','/admin/conList.aspx?page=BetOfYLLWarn');">
                                    <i class="icon icon-r"></i>当日盈利率报警 <span id="c3">0</span> 条</a></dd>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','盈利报警','/admin/conList.aspx?page=StatOfRealWarn');">
                                    <i class="icon icon-r"></i>当日盈利报警 <span id="c4">0</span> 条</a></dd>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','活动报警','/admin/conList.aspx?page=StatOfActiveWarn');">
                                    <i class="icon icon-r"></i>当日活动报警 <span id="c5">0</span> 条</a></dd>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','分红报警','/admin/conList.aspx?page=StatOfFhWarn');">
                                    <i class="icon icon-r"></i>当日分红报警 <span id="c6">0</span> 条</a></dd>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','取款报警','/admin/conList.aspx?page=GetCashWarnTotal');">
                                    <i class="icon icon-r"></i>当日取款报警 <span id="c7">0</span> 条</a></dd>
                            <dd>
                                <a href="javascript:void(0);" onclick="ShowPage('报警信息','同IP报警','/admin/conList.aspx?page=UserOfIpWarn');">
                                    <i class="icon icon-r"></i>同IP报警 <span id="c8">0</span> 条</a></dd>
                        </dl>
                        <span class="icon icon-arrow"></span>
                    </div>
                </li>
                <li class="tn-hd" <%=act2 %>><a href="javascript:void(0);" class="tn-title"><i class="icon icon-tn">
                </i><span class="num">8</span> </a>
                    <div class="tn-content">
                        <dl class="tn-dl">
                            <dt>您有 8 条未处理活动</dt>
                            <dd>
                                <a href="#"><i class="icon icon-r"></i>绑定资料活动 3 条</a></dd>
                            <dd>
                                <a href="#"><i class="icon icon-r"></i>优惠活动 5 条</a></dd>
                        </dl>
                        <span class="icon icon-arrow"></span>
                    </div>
                </li>
            </ul>
        </div>
        <div class="content">
            <div class="side">
                <div id="ajaxMenuBody" class="collapsible-set">
                </div>
            </div>
            <div class="main">
                <div class="title-bar">
                    <h1 id="title" class="main-title">
                        数据更新</h1>
                    <div class="breadcrumb">
                        当前位置：<span id="parent">基础信息</span>&nbsp;&gt;&nbsp;<span class="end" id="title2">数据更新</span></div>
                </div>
                <iframe id="workspace" name="workspace" src="/admin/temp.aspx" scrolling="no" width="100%"
                    height="760" frameborder="0" marginheight="0" marginwidth="0"></iframe>
            </div>
        </div>
        <div class="sbg">
        </div>
        <div id="pop" style="display: none; z-index: 99">
            <div id="popHead">
                <a id="popClose" title="关闭">关闭</a>
                <h2>
                    系统提示您</h2>
            </div>
            <div id="popContent">
                <dl>
                    <dt id="popTitle"></dt>
                    <dd id="popIntro">
                    </dd>
                </dl>
            </div>
        </div>
        <embed id="MUSIC1" name="MUSIC1" src="../statics/admin/images/pop.mp3" loop="true"
            autostart="false" hidden="true" />
    </div>
</body>
</html>
