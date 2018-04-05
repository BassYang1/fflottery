<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="userChargeUpList.aspx.cs"
    Inherits="Lottery.Admin.userChargeUpList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/_libs/My97DatePicker/WdatePicker.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        var itemid = joinValue('itemid');
        var pagesize = 20;
        var page = thispage();
        $(document).ready(function () {
            $i('d1').value = GetDateStr(0);
            $i('d2').value = GetDateStr(1);
            ajaxList(page);
        });
        function ajaxList(currentpage) {
            if (currentpage != null) page = currentpage;
            top.Lottery.Loading.show("正在努力加载，请稍后...");
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: "ajaxCharge.aspx?oper=ajaxGetUpList" + itemid,
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert("网络堵塞,稍后再试！"); },
                success: function (d) {
                    switch (d.result) {
                        case '-1':
                            top.Lottery.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                            break;
                        case '0':
                            top.Lottery.Alert(d.returnval, "0");
                            break;
                        case '1':
                            top.Lottery.Message(d.returnval, "1");
                            $("#ajaxList").setTemplateElement("tplList", null, { filter_data: true });
                            $("#ajaxList").processTemplate(d);
                            Activequery-table();
                            $("#ajaxPageBar").html(d.pagebar);
                            break;
                    }
                }
            });
        }
        function ajaxSearch() {
            itemid = "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&u=" + encodeURIComponent($('#username').val().replace(/(^\s*)|(\s*$)/g, '')) + "&u2=" + encodeURIComponent($('#username2').val().replace(/(^\s*)|(\s*$)/g, '')) + "&money=" + $('#money').val();
            ajaxList(1);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="topnav">
        <ul id="topnavbar">
            <li class="topmenu"><span>开始时间:</span><input id="d1" type="text" class="ipt" style="width: 150px;"
                onclick="WdatePicker({el:'d1'})" />
                <span>截止时间:</span>
                <input id="d2" type="text" class="ipt" style="width: 150px;" onclick="WdatePicker({el:'d2'})" />
                <span>上级帐号:</span>
                <input class="ipt" type="text" id="username" />
                <span>下级帐号:</span>
                <input class="ipt" type="text" id="username2" />
                <span>金额大于:</span>
                <input id="money" type="text" class="ipt" maxlength="10" onkeypress="chkPrice(this)"
                    style="width: 60px;" />
                <input type="button" class="bt" value="查询" onclick="ajaxSearch();" />
            </li>
        </ul>
    </div>
    </form>
    <textarea class="template" id="tplList" style="display: none">
<table class="query-table">
<thead>
<tr>
<th scope="col" style="width:50px;">编号</th>
<th scope="col" style="width:200px;">订单号</th>
<th scope="col" style="width:150px;">上级帐号</th>
<th scope="col" style="width:150px;">下级帐号</th>
<th scope="col" style="width:150px;">充值金额</th>
<th scope="col" style="width:150px;">充值状态</th>
<th scope="col" style="width:200px;">充值时间</th>
<th scope="col" style="width:*;"></th>
</tr>
</thead>
<tbody>
{#foreach $T.table as record}
<tr>
<td align="center">{$T.record.rowid}</td>
<td align="center">{$T.record.orderno}</td>
<td align="center">{$T.record.username}</td>
<td align="center">{$T.record.tousername}</td>
<td align="right">{formatCurrency($T.record.moneychange)}</td>
<td align="center"><font color="red">成功</font></td>
<td align="center">{$T.record.stime}</td>
<td align="center">

</td>
</tr>
{#/for}
</table>
</textarea>
    <div id="ajaxList" style="width: 99.5%; margin: 0; padding: 0" class="mrg10T">
    </div>
    <div id="ajaxPageBar" class="pages">
    </div>
</body>
</html>
