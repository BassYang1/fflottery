<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info2.aspx.cs" Inherits="Lottery.WebApp.email.Info2" %>

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
    <script type="text/javascript" src="/statics/js/jquery_json.js"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
</head>
<body>
    <div class="tto-popup tto-popup-md">
        <div class="popup-body">
            <form action="" method="post" class="tto-form popup-form">
            <div class="input-group">
                <label class="lab">
                    标题：</label>
                <span class="ipt">
                    <%=L_Title %></span>
            </div>
            <div class="input-group">
                <label class="lab">
                    收件人：</label>
                <span class="ipt">
                    <%=L_ReceiveName%></span>
            </div>
            <div class="input-group">
                <label class="lab">
                    时间：</label>
                <span class="ipt">
                    <%=L_Time%></span>
            </div>
            <div class="input-group">
                <label class="lab">
                    邮件内容：</label>
                <textarea id="txtContents" style="width: 600px; height: 280px;" class="ipt" placeholder="请输入邮件内容"><%=L_Contents%></textarea>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
