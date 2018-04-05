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
    <script type="text/javascript" src="/statics/base/lotMenu.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.uc-menu a').click(function () {
                $(this).parent().find('a').removeClass();
                $(this).addClass('current');
                var src = $(this).attr("nmb");
                ajaxInit(src);
            });
            $(".sc-nav-new").find('a').removeClass();
            $('#id9').addClass('current');
        });

        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
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
                            <ul>
                                <li><a href="/html/service.aspx" target="workspace"><i class="icon icon-clause"></i>
                                    服务条款</a></li>
                                <li><a href="/html/policy.aspx" target="workspace"><i class="icon icon-policy"></i>隐私政策</a></li>
                                <li><a href="/html/duty.aspx" target="workspace"><i class="icon icon-duty"></i>责任博彩</a></li>
                                <li><a href="/html/security.aspx" target="workspace"><i class="icon icon-security"></i>
                                    安全性</a></li>
                                <li><a href="/html/declare.aspx" target="workspace"><i class="icon icon-declare"></i>
                                    免责声明</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="block-content">
                        <iframe id="workspace" name="workspace" src="/html/service.aspx" scrolling="no" width="100%"
                            height="700px" frameborder="0" marginheight="0" marginwidth="0" style="background-color: #fff;">
                        </iframe>
                    </div>
                </div>
            </div>
        </div>
        <!-- 内容 end -->
    </div>
    <!-- 容器 end -->
</body>
</html>
