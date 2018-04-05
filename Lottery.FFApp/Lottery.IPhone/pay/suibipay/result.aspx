<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="result.aspx.cs" Inherits="Lottery.IPhone.SBF.result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>随笔付支付结果</title>
    <script src="../../statics/js/jquery.min.js"></script>
</head>
<body>
    <% 
        if (this.IsSuccess)
        {
    %>
    <h1>支付成功。<span id="sec">3</span>秒后页面直动关闭...</h1>
    
    <% 
        }
        else
        {
    %>
    
    <h1><%=this.Message %>。<span id="sec">3</span>秒后页面直动关闭...</h1>
    <% 
        }
    %>
    <div><a href="/main.html">返回</a></div>

    <script>
        $(function(){            
            var seconds = 3;
            var timer = window.setInterval(function () {
                seconds--;

                if (seconds <= 0) {
                    window.clearInterval(timer);
                    window.opener = null;
                    window.open('', '_self');
                    window.close();
                    <%--if ("<%=this.IsSuccess%>" == "True") {
                        location.href = "/center.html";
                    }
                    else {
                        location.href = "/main.html";
                    }--%>
                }
                else {
                    $("#sec").html(seconds);
                }
            }, 1000);
        });
    </script>
</body>
</html>
