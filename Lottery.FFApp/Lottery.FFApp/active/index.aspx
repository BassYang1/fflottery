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
                var height = $(this).attr("nmb2");
                ajaxActive(src, height);
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
                    switch (d.result) {
                        case '-1':
                            top.window.location = '/login.aspx';
                            break;
                        case '1':
                            if (d.table[0].usergroup == "2") {
                                $("#g2").show();
							}
                            else if (d.table[0].usergroup == "3") {
                                $("#g3").show();
                            }
                            else if (d.table[0].usergroup == "4") {
                                $("#g4").show();
                            }
                            break;
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
            <div class="tto-block has-title">
                <div class="block-heading">
                    <h2 class="block-title"><i class="icon-title icon-gift"></i>优惠活动</h2>
                </div>
                    <div class="block-content">
                       <div id="menulist" class="block-subnav">
                            <a href="javascript:;" nmb="/active/actReg.html" nmb2="860" class="current">会员注册有礼</a>
                            <a id="g2" href="javascript:;" nmb="/active/actGroup2.html" nmb2="1090" style="display:none;">日奖励活动</a>
							<a id="g3" href="javascript:;" nmb="/active/actGroup3.html" nmb2="950" style="display:none;">日奖励活动</a>
                            <a id="g4" href="javascript:;" nmb="/active/actGroup4.html" nmb2="900" style="display:none;">日奖励活动</a>
							<a href="javascript:;" nmb="/active/actYongjin.html" nmb2="900">亏损佣金</a>
                        </div>
						<div class="block-panel">
                        <iframe id="workspace" name="workspace" src="/active/actReg.html" scrolling="no"
                            width="100%" height="860px" frameborder="0" marginheight="0" marginwidth="0"
                            style="background-color: #fff;"></iframe>
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
