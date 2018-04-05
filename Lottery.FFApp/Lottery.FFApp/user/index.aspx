<%@ Page Language="C#" %>

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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.block-subnav a').click(function () {
                $(this).parent().find('a').removeClass();
                $(this).addClass('current');
                var src = $(this).attr("nmb");
                ajaxInit(src);
            });
            ajaxList();
        });

        function ajaxList() {
            $.ajax({
                type: "get",
                dataType: "json",
                data: "clienttime=" + Math.random(),
                url: "/ajax/ajaxActive.aspx?oper=ajaxGetList",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    if (d.table[0].usergroup != "0") { //会员不允许注册平级会员
                        $("#ad").show();
                        $("#fh").show();
                    }
                }
            });
        }
    </script>
</head>
<body style="background: url(/statics/img/bg.jpg) no-repeat repeat #23293f;">
    <!-- 容器 start -->
    <div class="tto-container">
        <!-- #include file="/statics/include/header.html" -->
        <!-- #include file="/statics/include/lefter.html" -->
        <!-- 内容 start -->

        <div class="tto-content">
            <div class="tto-layout">
                <div class="tto-block">
                    <div class="block-heading">
                        <div class="block-nav">
                             <!-- #include file="/statics/include/nav.html" -->
                        </div>
                    </div>
                    <div class="block-content">
                        <div class="block-subnav">
                               <a href="javascript:;" nmb="/user/userindex.aspx" class="current">代理首页</a>
                               <a id="ad" href="javascript:;" nmb="/user/useradd.aspx" style="display:none;">开户中心</a>
                               <a href="javascript:;" nmb="/aspx/list.aspx?nav=UserList">用户管理</a>
                               <a href="javascript:;" nmb="/aspx/list.aspx?nav=UserListOnline">在线会员</a>
                               <a href="javascript:;" nmb="/aspx/list.aspx?nav=UserProListSub">团队报表</a>
                               <a href="javascript:;" nmb="/aspx/list.aspx?nav=UserChargeCashHistory">充提记录</a>
                               <a href="javascript:;" nmb="/aspx/list.aspx?nav=betlist_User">游戏记录</a>
                               <a href="javascript:;" nmb="/aspx/list.aspx?nav=UserHistory_User">账变记录</a>
							   <a id="fh" href="javascript:;" nmb="/user/userAgentFH.aspx" style="display:none;">代理分红</a>
                        </div>
                        <div class="block-panel">
                            <iframe id="workspace" src="/user/userindex.aspx" scrolling="no" width="100%" height="620px" frameborder="0"
                                marginheight="0" marginwidth="0" style="background-color: #fff;"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 内容 end -->
    </div>
    <!-- 容器 end -->
</body>
</html>
