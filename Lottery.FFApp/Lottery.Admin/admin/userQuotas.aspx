<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="userQuotas.aspx.cs" Inherits="Lottery.Admin.userQuotas" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        var itemid = joinValue('itemid');
        var gid = joinValue('gid'); //用户组ID
        var keys = joinValue('keys'); //关键字
        var pagesize = 20;
        var page = thispage();
        $(document).ready(function () {
            ajaxList(page);

        });
        function ajaxList(currentpage) {
            if (currentpage != null) page = currentpage;
            top.Lottery.Loading.show("正在努力加载，请稍后...");
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: "ajaxUser.aspx?oper=ajaxGetQuotasList" + itemid,
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
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
        function ConfirmDel(id) {
            top.top.Lottery.Confirm("确定删除该会员及其相关的信息吗?", "getCurrentIframe().ajaxDel(" + id + ")");
        }
        function ajaxDel(id) {
            $.ajax({
                type: "post",
                dataType: "json",
                data: "id=" + id,
                url: "ajaxUser.aspx?oper=ajaxDel&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    switch (d.result) {
                        case '-1':
                            top.Lottery.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                            break;
                        case '0':
                            top.Lottery.Alert(d.returnval, "0");
                            break;
                        case '1':
                            ajaxList(page);
                            break;
                    }
                }
            });
        }
        function ajaxSearch() {
            itemid = "&uname=" + encodeURIComponent($('#username').val().replace(/(^\s*)|(\s*$)/g, ''));
            ajaxList(1);
        }
    </script>
</head>
<body>
    <div class="topnav">
        <ul id="topnavbar">
            <li class="topmenu">
                <span>用户名:</span><input class="ipt" type="text" id="username" />
                <input type="button" class="bt" value="查询" onclick="ajaxSearch();" />
            </li>
        </ul>
        <script>            topnavbarStuHover();</script>
    </div>
<textarea class="template" id="tplList" style="display: none">
<table class="query-table">
<thead>
<tr>
<th scope="col" style="width:60px;">编号</th>
<th scope="col" style="width:120px;">会员名称</th>
<th scope="col" style="width:80px;">返点</th>
<th scope="col" style="width:80px;">下级</th>
<th scope="col" style="width:100px;">账号余额</th>
<th scope="col" style="width:120px;">投注流水</th>
<th scope="col" style="width:150px">最后登录</th>
<th scope="col" style="width:50px">在线</th>
<th scope="col" style="width:150px;">申请时间</th>
<th scope="col" style="width:150px;">处理时间</th>
<th scope="col" style="width:100px;">操作</th>
</tr>
</thead>
<tbody>
{#foreach $T.table as record}
<tr>
<td align="center">{$T.record.id}</td>
<td align="center">{$T.record.username}</td>
<td align="center">{$T.record.point}</td>
<td align="center">{$T.record.childcount}</td>
<td align="center">{$T.record.money}</td>
<td align="center">{$T.record.betmoney}</td>
<td align="center">{$T.record.ontime}</td>
<td align="center" valign="middle">
{#if $T.record.isonline == "1"}
<img src="/statics/admin/images/state_on.jpg" title="在线"/>
{#else}
<img src="/statics/admin/images/state_off.jpg" title="离线"/>
{#/if}
</td>
<td align="center">{$T.record.addtime}</td>
        <td align="center">{$T.record.checktime}</td>
<td align="center" class="oper">
{#if $T.record.state ==0}
<a href="javascript:void(0);" onclick="top.Lottery.Popup.show('userQuotasEdit.aspx?id={$T.record.bid}',600,300,true)">处理申请</a>
{#/if}
{#if $T.record.state ==1}
<font color='red'>回收成功</font>
{#/if}
{#if $T.record.state ==2}
<font color='red'>拒绝回收</font>
{#/if}
</td>
</tr>
{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList" style="width: 99.5%; margin: 0; padding: 0" class="mrg10T">
    </div>
    <div id="ajaxPageBar" class="pages">
    </div>
</body>
</html>
