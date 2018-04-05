<%@ Page Language="C#" AutoEventWireup="true" Inherits="Lottery.WebApp.contract.MyAgentfhlist"
    CodeBehind="MyAgentfhlist.aspx.cs" %>

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
</head>
<body>
    <div class="acting-dividends">
        <table class="acting-table">
            <colgroup>
                <col class="w120" />
                <col class="w160" />
                <col class="w120" />
                <col class="w160" />
                <col class="w120" />
                <col class="w160" />
            </colgroup>
            <tr>
                <td class="type w120">
                    本期开始：
                </td>
                <td>
                    <%=d1 %>
                </td>
                <td class="type w120">
                    本期截止：
                </td>
                <td>
                    <%=d2 %>
                </td>
                <td class="type w120">
                    累计方式：
                </td>
                <td>
                    不累计
                </td>
            </tr>
            <tr>
                <td class="type w120">
                    活跃人数：
                </td>
                <td>
                    <em>
                        <%=HyNum %></em>
                </td>
                <td class="type w120">
                    您的分红比率：
                </td>
                <td>
                    <em>
                        <%=Per %>%</em>
                </td>
                 <td class="type w120">
                </td>
                <td>
                    <em>
                        </em>
                </td>
            </tr>
            <tr>
                <td class="type w120">
                    累计销量：
                </td>
                <td>
                    <em>
                        <%=Bet %></em>
                </td>
                <td class="type w120">
                    盈亏情况：
                </td>
                <td>
                    <em>
                        <%=Loss %></em>
                </td>
                <td class="type w120">
                    分红金额：
                </td>
                <td>
                    <em>
                        <%=Money %></em>
                </td>
            </tr>
        </table>
        <div class="query-form">
            <iframe id="workspace" src="/aspx/list.aspx?nav=AgentFHRecord" scrolling="no" width="100%"
                height="500px" frameborder="0" marginheight="0" marginwidth="0" style="background-color: #fff;">
            </iframe>
        </div>
    </div>
</body>
</html>
