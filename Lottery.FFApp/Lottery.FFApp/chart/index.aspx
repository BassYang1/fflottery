<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.index" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>立博国际娱乐</title>
<style>
html,body,h1,h2,h3,h4,h5,h6,div,dl,dt,dd,ul,ol,li,p,blockquote,pre,hr,figure,table,caption,th,td,form,fieldset,legend,input,button,textarea,menu{margin:0;padding:0;}
html{font-family:sans-serif;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%;}
a{color:#333;text-decoration:none;background-color:transparent;-webkit-text-decoration-skip:objects;}
a:active,a:hover,:focus{outline:0;}
b,strong{font-weight:bold;}
i,em{font-style:normal;}
h1,h2,h3,h4,h5,h6{font-size:100%;}
img{border:0 none;vertical-align:middle;}
body,input,button,select,textarea{font:12px/1.5 "Microsoft YaHei",Arial,\5b8b\4f53,sans-serif;color:#333;}

.query-form{margin:10px;}
.query-form .lab,.query-form .ipt,.query-form select,.query-form .to,.query-form .btn,.query-form .query-date{float:left;margin-right:10px;margin-bottom:10px;}
.query-form .ipt,.query-form select{border:1px solid #e7eaeb;}
.query-form .ipt{width:100px;height:24px;padding:2px 9px;}
.query-form .lab,.query-form .to{height:30px;line-height:30px;overflow:hidden;}
.query-form .query-date{position:relative;}
.query-form .query-date .ipt{margin-right:0;margin-bottom:0;}
.query-form .query-date .icon-date{position:absolute;z-index:9;top:50%;right:7px;width:16px;height:16px;margin-top:-8px;background-image:url(../img/icon_query_date.png);}
.query-form select{width:100px;height:30px;padding-left:5px;}
.query-form .btn-query{width:60px;height:30px;margin-right:5px;border-radius:3px;color:#fff;background-color:#fba026;}
.query-form .btn-query2{width:60px;height:30px;line-height:30px; margin:auto 5px auto 0px;border-radius:3px;color:#fff;background-color:#fba026;}
.query-form .time-range{float:left;}
.query-form .time-range li{float:left;width:88px;height:28px;line-height:28px;margin-right:10px;border:1px solid #e7eaeb;text-align:center;color:#999;cursor:pointer;}
.query-form .time-range .selected{color:#fff;border-color:#fba026;background-color:#fba026;}

.query-table{width:100%;max-width:100%;border-spacing:0;border-collapse:collapse;text-align:center;}
.query-table tr{height:35px;}
.query-table thead tr{color:#fff;background-color:#3b3c46;}
.query-table th,.query-table td{width: 20px;padding:3px;border:1px solid #ddd;}
.query-table .btn{width:38px;height:24px;line-height:24px;margin:0 5px;border:1px solid #e7eaeb;border-radius:2px;background-color:#fff;}
.ball01{color: #FFF;font-size: 10px;background: url(style/ball.png);width: 20px;height: 20px;line-height: 20px;}
</style>
</head>
<body style="width: 100%; margin: 0px;">
   <div class="query-area">
            <div class="query-form">
                <form action="" method="post">
                    <div class="input-group"> 
                    <strong style="color: #f00"><%=LName%>走势图</strong> 
                    <input type="checkbox" name="checkbox2" value="checkbox" id="has_line">显示折线 
                    <input type="checkbox" name="checkbox" value="checkbox" id="no_miss" checked="checked">显示遗漏
                    <a href="/Chart/index.aspx?id=<%=lotteryId %>&n=50">最近50期</a>&nbsp; 
                    <a href="/Chart/index.aspx?id=<%=lotteryId %>&n=100">最近100期</a>&nbsp; 
                    <a href="/Chart/index.aspx?id=<%=lotteryId %>&n=200">最近200期</a>&nbsp;
                    <a href="/Chart/index.aspx?id=<%=lotteryId %>&n=500">最近500期</a>&nbsp;
                    <a href="/Chart/index.aspx?id=<%=lotteryId %>&n=9999">全部</a>
                    </div>
                </form>
            </div>
        <div class="query-result" style="width: 100%">
          <table id="chartsTable" width="100%" border="0" cellpadding="0" cellspacing="1" class="query-table">
                <thead>
                <%=LotteryHeadLines%>
                </thead>
                 <tbody>
                <%=LotteryLines %>
                 </tbody>
        </table>
        </div>
    </div>
</body>
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/chart/style/line.js"></script>
    <script>
        $(function () {
            if ($("#chartsTable").width() > $('body').width()) {
                $('body').width(($("#chartsTable").width() + 30) + "px");
                $('.history_code').css("width", $("#chartsTable").width() + "px");
            }
            Chart.init();
            DrawLine.bind("chartsTable", "has_line");

            var count=<%=count %>;

            DrawLine.color('#FF0000');
            DrawLine.add((parseInt(0) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#00B22D');
            DrawLine.add((parseInt(1) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#FF0000');
            DrawLine.add((parseInt(2) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#00B22D');
            DrawLine.add((parseInt(3) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#FF0000');
            DrawLine.add((parseInt(4) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#00B22D');
            DrawLine.add((parseInt(5) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#FF0000');
            DrawLine.add((parseInt(6) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#00B22D');
            DrawLine.add((parseInt(7) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#FF0000');
            DrawLine.add((parseInt(8) * count + 1 + 1), 2, count, 0);
            DrawLine.color('#00B22D');
            DrawLine.add((parseInt(9) * count + 1 + 1), 2, count, 0);
            DrawLine.draw(Chart.ini.default_has_line);
            resize();

            var show = false;
            var nols = $("div[class^='ball1']");
            $("#no_miss").click(function () {
                show = !show;
                $.each(nols, function (i, n) {
                    if (show == true) {
                        n.style.display = 'none';
                    } else {
                        n.style.display = 'block';
                    }
                });
            });
        });
        function resize() {
            window.onresize = func;
            function func() {
                window.location.href = window.location.href;
            }
        }
        $("tr").live({
            mouseover: function () {
                $(this).css("background-color", "#eeeeee");
            },
            mouseout: function () {
                $(this).css("background-color", "#ffffff");
            }
        }) 
    </script>
</html>
