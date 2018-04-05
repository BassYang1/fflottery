<%@ Page Language="C#" AutoEventWireup="true" Inherits="Lottery.WebApp.user.userindex"
    CodeBehind="userindex.aspx.cs" %>

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
    <script type="text/javascript" src="/statics/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/statics/echarts.min.js"></script>
    <script type="text/javascript">
        var itemid = joinValue('itemid');
        $(document).ready(function () {
            ajaxList();
            ajaxGetTeamType(1);
            $(".optgroup").delegate('li', 'click', function (event) {
                $(this).parent().find('li').removeClass().addClass('option');
                $(this).addClass('option selected');
            });
        });

        function ajaxList() {
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=1&pagesize=1&clienttime=" + Math.random(),
                url: "/ajax/ajaxUser.aspx?oper=ajaxGetTeamTotalList" + itemid,
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    switch (d.result) {
                        case '1':
                            if (d.recordcount > 0) {
                                $("#charge").html(parseFloat(d.table[0].charge).toFixed(2));
                                $("#getcash").html(parseFloat(d.table[0].getcash).toFixed(2));
                                $("#bet").html(parseFloat(d.table[0].bet).toFixed(2));
                                $("#win").html(parseFloat(d.table[0].win).toFixed(2));
                                $("#point").html(parseFloat(d.table[0].point).toFixed(2));
                            }
                            break;
                    }
                    closeload(index);
                }
            });
        }
        function ajaxGetTeamType(flag) {
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=1&pagesize=31&clienttime=" + Math.random(),
                url: "/ajax/ajaxUser.aspx?oper=ajaxGetTeamType",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    var title = [];
                    var datas = [];
                    var name = "";
                    switch (d.result) {
                        case '1':
                            if (d.recordcount > 0) {
                                if (flag == 1) {
                                    for (i = 0; i < d.table.length; i++) {
                                        title.push(d.table[i].stime.substr(5, 2) + "月" + d.table[i].stime.substr(8) + "日");
                                        datas.push(d.table[i].charge);
                                    }
                                    name = "充值量";
                                }
                                if (flag == 2) {
                                    for (i = 0; i < d.table.length; i++) {
                                        title.push(d.table[i].stime.substr(5, 2) + "月" + d.table[i].stime.substr(8) + "日");
                                        datas.push(d.table[i].getcash);
                                    }
                                    name = "提现量";
                                }
                                if (flag == 3) {
                                    for (i = 0; i < d.table.length; i++) {
                                        title.push(d.table[i].stime.substr(5, 2) + "月" + d.table[i].stime.substr(8) + "日");
                                        datas.push(d.table[i].bet);
                                    }
                                    name = "投注量";
                                }
                                if (flag == 4) {
                                    for (i = 0; i < d.table.length; i++) {
                                        title.push(d.table[i].stime.substr(5, 2) + "月" + d.table[i].stime.substr(8) + "日");
                                        datas.push(d.table[i].win);
                                    }
                                    name = "奖金量";
                                }
                                if (flag == 5) {
                                    for (i = 0; i < d.table.length; i++) {
                                        title.push(d.table[i].stime.substr(5, 2) + "月" + d.table[i].stime.substr(8) + "日");
                                        datas.push(d.table[i].point);
                                    }
                                    name = "返点量";
                                }

                                var myChart = echarts.init(document.getElementById('placeholder'));
                                option = {
                                    title: {
                                        text: name+'本月走势',
                                    },
                                    tooltip: {
                                        trigger: 'axis'
                                    },
                                    legend: {
                                        data: name
                                    },
                                    toolbox: {
                                        show: true,
                                        feature: {
                                            dataZoom: {
                                                yAxisIndex: 'none'
                                            },
                                            dataView: { readOnly: false },
                                            magicType: { type: ['line'] },
                                            restore: {},
                                            saveAsImage: {}
                                        }
                                    },
                                    xAxis: {
                                        type: 'category',
                                        boundaryGap: false,
                                        data: title
                                    },
                                    yAxis: {
                                        type: 'value',
                                        axisLabel: {
                                            formatter: '{value} 元'
                                        }
                                    },
                                    series: [
                                                {
                                                    name: name,
                                                    type: 'line',
                                                    smooth: true,
                                                    symbolSize: 10,
                                                    data: datas,
                                                    markPoint: {
                                                        data: [
                                                            { type: 'max', name: '最大值' },
                                                            { type: 'min', name: '最小值' }
                                                        ]
                                                    }
                                                }
                                            ]
                                };
                                myChart.setOption(option);
                            }
                            break;
                    }
                }
            });
        }

        function ajaxSearch(flag) {
            if (flag == "1") {
                $('#d1').val(GetDateStr(-3));
                $('#d2').val(GetDateStr(0));
            }
            if (flag == "2") {
                $('#d1').val(GetDateStr(-7));
                $('#d2').val(GetDateStr(0));
            }
            if (flag == "3") {
                $('#d1').val(GetDateStr(-30));
                $('#d2').val(GetDateStr(0));
            }
            itemid = "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val();
            ajaxList();
        }

    </script>
</head>
<body>
    <div class="agent-home">

        <div class="agent-content">
            <div class="tto-tabs">
                <div class="tabs-panel" id="lottery-game">
       
                    <div class="query-form">
                             
                        <form action="" method="post">
                        <div class="input-group">
                            <ul class="time-range optgroup">
                                <li class="option selected" onclick="ajaxSearch(1)">最近三天</li>
                                <li class="option" onclick="ajaxSearch(2)">最近七天</li>
                                <li class="option" onclick="ajaxSearch(3)">最近一个月</li>
                            </ul>
                            <input id="d1" type="text" onclick="WdatePicker({el:'d1'})" class="ipt" placeholder="开始时间" />
                            <input id="d2" type="text" onclick="WdatePicker({el:'d2'})" class="ipt" placeholder="结束时间" />
                            <input type="button" value="搜索" onclick="ajaxSearch(0)" class="btn btn-query" />
                        </div>
                        </form>
                    </div>
                    <div class="agent-stat">
                        <ul>
                           <li>
                    <h3>
                        团队（人）</h3>
                    <span class="amount">
                        <%=UserSum %></span> </li>
                <li>
                    <h3>
                        在线（人）</h3>
                    <span class="amount">
                        <%=UserOnLineSum%></span> </li>
                <li>
                    <h3>
                        直属会员（人）</h3>
                    <span class="amount">
                        <%=UserZsSum %></span> </li>
                <li>
                    <h3>
                        团队余额（元）</h3>
                    <span class="amount">
                        <%=UserMoneySum %></span> </li>
                        </ul>
                        </div>
                         <div class="agent-stat agent-stat2">
                        <ul>
                            <li><h3>充值量</h3><span class="amount" id="charge">0</span></li>
                            <li><h3>提现量</h3><span class="amount" id="getcash">0</span></li>
                            <li><h3>代购量</h3><span class="amount" id="bet">0</span></li>
                            <li><h3>派奖量</h3><span class="amount" id="win">0</span></li>
                            <li><h3>返点量</h3><span class="amount" id="point">0</span></li>
                        </ul>
                    </div>
                    <div class="agent-look">
                        <label class="radio">
                            <input type="radio" value="" name="type" onclick="ajaxGetTeamType(1)" checked />充值</label>
                        <label class="radio">
                            <input type="radio" value="" name="type" onclick="ajaxGetTeamType(2)" />提现</label>
                        <label class="radio">
                            <input type="radio" value="" name="type" onclick="ajaxGetTeamType(3)" />投注</label>
                        <label class="radio">
                            <input type="radio" value="" name="type" onclick="ajaxGetTeamType(4)" />奖金</label>
                        <label class="radio">
                            <input type="radio" value="" name="type" onclick="ajaxGetTeamType(5)" />返点</label>
                    </div>
                    <div class="agent-chart">
                        <div id="placeholder" style="height: 400px;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
