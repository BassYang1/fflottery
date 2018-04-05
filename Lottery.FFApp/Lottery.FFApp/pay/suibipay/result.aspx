<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="result.aspx.cs" Inherits="Lottery.FFApp.SBF.result" %>

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
    <h1>支付成功。<span id="sec">10</span>秒后自动关闭页面...</h1>
    <script>
        $(function(){            
            var seconds = 10;
            var timer = window.setInterval(function () {
                seconds--;

                if (seconds <= 0) {
                    window.clearInterval(timer);
                    window.opener = null;
                    window.open('', '_self');
                    window.close();
                }
                else {
                    $("#sec").html(seconds);
                }
            }, 1000);
        });
    </script>
    <% 
        }
        else
        {
    %>
    <h1><%=this.Message %></h1>
    <% 
        }
    %>
</body>
</html>
