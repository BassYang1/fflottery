<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Lottery.WebApp.bet.index" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>立博国际娱乐</title>
    <link rel="stylesheet" type="text/css" href="/statics/css/flipclock.css" />
    <link rel="stylesheet" type="text/css" href="/statics/css/global.css" />
    <link rel="stylesheet" type="text/css" href="/statics/css/member.css" />
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.uc-menu a').click(function () {
                $(this).parent().find('a').removeClass();
                $(this).addClass('current');
                var src = $(this).attr("nmb");
                ajaxInit(src);
            });
            $(".sc-nav-new").find('a').removeClass();
            $('#id5').addClass('current');
        });
    </script>
</head>
<body>
    <div class="sc-container">
        <!-- #include file="/statics/include/header.html" -->
        <div class="sc-content">
            <div class="sc-pc">
                <div class="pc-banner" style="background-image: url(/statics/img/pc_banner.jpg);">
                </div>
                <div class="pc-content">
                    <div class="pc-main">
                        <div class="sc-uc">
                            <div class="uc-menu">
                                <a href="javascript:;" nmb="/aspx/list.aspx?nav=Receivelist" class="current">收件箱</a>
                                <a href="javascript:;" nmb="/aspx/list.aspx?nav=Sendlist">发件箱</a> <a href="javascript:;"
                                    nmb="/email/add.html">发送信息</a>
                            </div>
                            <div class="uc-main">
                                <iframe id="workspace" src="/aspx/list.aspx?nav=Receivelist" scrolling="no" width="100%" height="765px"
                                    frameborder="0" marginheight="0" marginwidth="0" style="background-color:#fff;"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- #include file="/statics/include/footer.html" -->
    </div>
</body>
</html>
