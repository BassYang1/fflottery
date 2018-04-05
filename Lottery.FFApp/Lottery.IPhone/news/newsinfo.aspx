<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsinfo.aspx.cs" Inherits="Lottery.IPhone.news.newsinfo" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>立博国际娱乐</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=no">
    <meta name="format-detection" content="telephone=no,email=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/global.css" />
    <link rel="stylesheet" type="text/css" href="/statics/sytle/css/style.css" />
</head>
<body class="whitebg">
<div class="container">
    <header id="header">
        <h1 class="title">公   告</h1>
        <a href="javascript:history.go(-1);" class="back"></a>
    </header>
    <main id="main">
        <article class="notice-detail">
        	<header class="notice-info">
            	<div class="date">
                    <span class="month"><%=L_Month %></span>
                    <span class="day"><%=L_Day %></span>
                </div>
                <div class="name">
                    <em>【网站公告】</em><%=L_Title %>
                </div>
            </header>
            <div class="notice-content">
            	 <%=L_Detail%>
                 <p>立博国际娱乐<br/> <%=L_Time%></p>
            </div>
        </article>
    </main>
</div>
</body>
</html>
