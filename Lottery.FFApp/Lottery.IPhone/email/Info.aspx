<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="Lottery.IPhone.email.Info" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>立博国际娱乐</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=no">
    <meta name="format-detection" content="telephone=no,email=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/style.css" />
</head>
<body>
    <div class="zj-page">
        <header class="header">
                <h1 class="title">邮件详情</h1>
                 <a href="javascript:history.go(-1);" class="back"></a>
            </header>
        <div class="content">
            <article class="email">
                    <header class="email-header">
                        <h1>
                            <%=L_Title %>
                        </h1>
                    </header>
                    <div class="email-body">
                        <%=L_Contents%>
                        <br/><p>来自于：<%=L_SendName %><br/><%=L_Time%></p>
                    </div> 
                    <div class="btn-item">
                <a href="Reply.aspx?id=<%=L_Id %>">
                    <input type="button" value="回复" class="btn primary-btn" /></a>
            </div>
                </article>
        </div>
    </div>
</body>
</html>
