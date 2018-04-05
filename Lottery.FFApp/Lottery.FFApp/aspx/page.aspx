<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page.aspx.cs" Inherits="Lottery.FFApp.page" %>

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
    <script type="text/javascript" src="/statics/base/lotMenu.js"></script>
    <script type="text/javascript">
        var request = '<%=k %>';//GetQueryString(<%=k %>);
        $(document).ready(function () {
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
                    var userGroup = d.table[0].usergroup;
                    var TableTemplate = GetMenu(request.substr(0, 1));
                    var TableValue = 0;

                    if (request.length > 1) {
                        TableValue = request.substr(1, 1);
                    }

                    $("#menulist").html("");
                    var s = "";
                    for (var i = 0, len = TableTemplate.ListCount; i < len; i++) {
                        var listName = TableTemplate.ListName[i]["key"];
                        var visable = listName != "开户中心" || userGroup != "0" ? true : false;

                        if (visable) {
                            if (i == TableValue) {
                                s += '<a href="javascript:;" nmb="' + TableTemplate.ListUrl[i]["key"] + '" class="current">' + TableTemplate.ListName[i]["key"] + '</a>';
                                ajaxInit(TableTemplate.ListUrl[i]["key"]);
                            } else {
                                s += '<a href="javascript:;" nmb="' + TableTemplate.ListUrl[i]["key"] + '">' + TableTemplate.ListName[i]["key"] + '</a>';
                            }
                        }
                    }
                    $("#menulist").html(s);

                    $('.block-subnav a').click(function () {
                        $(this).parent().find('a').removeClass();
                        $(this).addClass('current');
                        var src = $(this).attr("nmb");
                        ajaxInit(src);
                    });
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
                        <div id="menulist" class="block-subnav">
                        </div>
                        <div class="block-panel">
                            <iframe id="workspace" src="" scrolling="no" width="100%" height="620px" frameborder="0"
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
