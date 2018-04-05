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
                error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert(XmlHttpRequest.responseText); },
                success: function (d) {
                    switch (d.result) {
                        case '-1':
                            top.window.location = '/login.aspx';
                            break;
                        case '1':
                            var userGroup = d.table[0].usergroup;
                            if (isNaN(userGroup)) {
                                emAlert("亲！页面过期,请刷新页面!");
                                top.window.location = '/login.aspx';
                            }
                            
                            userGroup = parseInt(userGroup);

                            //20180207只有[会员]和[代理]两个级别
                            //到[会员]不能签约下级
                            //20180209所有会员都能分配置契约
                            //if (userGroup <= 0) {
                            //    $("#g1").hide();
                            //    $("#g2").hide();
                            //    $("#g3").hide();
                            //}
                            //else {
                            //    $("#g1").show();
                            //    $("#g2").show();
                            //    $("#g3").show();
                            //}
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
                    <h2 class="block-title"><i class="icon-title icon-gift"></i>分红契约</h2>
                </div>
                    <div class="block-content">
                       <div class="block-subnav">
                            <a href="javascript:;" nmb="/contract/contractfh.aspx" class="current">我的分红契约</a>
                            <a href="javascript:;" nmb="/contract/MyAgentfhlist.aspx">我的分红记录</a>
                            <a id="g1" href="javascript:;" nmb="/aspx/List.aspx?nav=ContractUserListFH">分配契约</a>
                            <a id="g2" href="javascript:;" nmb="/aspx/List.aspx?nav=UserContractList">已分配契约</a>
                            <a id="g3" href="javascript:;" nmb="/aspx/List.aspx?nav=ContractFHRecord">下级分红记录</a>
                            <a id="g4" href="javascript:;" nmb="/aspx/List.aspx?nav=ContractFHLog">日志记录</a>
                        </div>
						<div class="block-panel">
                        <iframe id="workspace" name="workspace" src="/contract/contractfh.aspx" scrolling="no"
                            width="100%" height="620px" frameborder="0" marginheight="0" marginwidth="0"
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
