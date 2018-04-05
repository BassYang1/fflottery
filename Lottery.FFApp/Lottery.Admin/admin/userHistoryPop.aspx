<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userHistoryPop.aspx.cs"
    Inherits="Lottery.Web.report.userHistoryPop" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>账变信息</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript" src="/_libs/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        var id = joinValue('id');
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
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: "ajaxHistory.aspx?oper=ajaxGetListById" + itemid + id,
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    switch (d.result) {
                        case '1':
                            $("#ajaxList").setTemplateElement("tplList", null, { filter_data: true });
                            $("#ajaxList").processTemplate(d);
                            $("#ajaxPageBar").html(d.pagebar);
                            break;
                    }
                }
            });
        }

        function ajaxSearch() {
            itemid = "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&tid=" + $('#ddlType').val();
            ajaxList(1);
        }
    </script>
</head>
<body>
    <div class="topnav">
        <ul id="topnavbar" style="width: 1200px;">
            <li class="topmenu">
                <span>时间:</span>
                <input id="d1" class="ipt" type="text" style="width: 150px;" onclick="WdatePicker({el:'d1'})" />
                <span>至</span><input id="d2" class="ipt" type="text" style="width: 150px;" onclick="WdatePicker({el:'d2'})" />
                <span>类型:</span>
                 <select id="ddlType" class="ipt2">
                    <option value="" selected>所有类型</option>
                    <option value="1">账号充值</option>
                    <option value="2">账号提款</option>
                    <option value="3">提现失败</option>
                    <option value="4">投注扣款</option>
                    <option value="5">追号扣款</option>
                    <option value="6">追号返款</option>
                    <option value="7">游戏返点</option>
                    <option value="8">奖金派送</option>
                    <option value="9">撤单返款</option>
                    <option value="10">充值扣费</option>
                    <option value="11">上级充值</option>
                    <option value="12">活动礼金</option>
                    <option value="13">代理分红</option>
                    <option value="14">管理员减扣</option>
                    <option value="15">积分兑换</option>
                </select>
                <input type="button" class="bt" value="查询" onclick="ajaxSearch();" />
            </li>
        </ul>
    </div>
    <textarea class="template" id="tplList" rows="1" cols="1" style="display: none">
<table class="query-table">
<thead>
<tr>
<th scope="col" width="50">编号</th>
<th scope="col" width="100">会员名</th>
<th scope="col" width="150">时间</th>
<th scope="col" width="80">类型</th>
<th scope="col" width="80">游戏</th>
<th scope="col" width="80">玩法</th>
<th scope="col" width="110">期号</th>
<th scope="col" width="50">模式</th>
<th scope="col" width="100">收入</th>
<th scope="col" width="100">支出</th>
<th scope="col" width="120">余额</th>
<th scope="col" width="*">备注</th>
</tr>
</thead>
<tbody>
{#foreach $T.table as record}
<tr>
<td align="center"><div align="center">{$T.record.id}</div></td>
<td align="center"><div align="center">{$T.record.uname}</div></td>
<td align="center"><div align="center">{$T.record.stime}</div></td>
<td align="center"><div align="center">{$T.record.codename}</div></td>
<td align="center"><div align="center">{$T.record.lotteryname}</div></td>
<td align="center"><div align="center">{$T.record.playname}</div></td>
<td align="center"><div align="center">{$T.record.issuenum}</div></td>
<td align="center"><div align="center">{$T.record.singlemoney}</div></td>
<td align="center">
{#if $T.record.moneychange > 0}
<div align="right"><font color="green">+{$T.record.moneychange}</font></div>
{#/if}
{#if $T.record.moneychange < 0}
<div align="right">---</div>
{#/if}
</td>
<td align="center">
{#if $T.record.moneychange > 0}
<div align="right">---</div>
{#/if}
{#if $T.record.moneychange < 0}
<div align="right"><font color="red">{$T.record.moneychange}</font></div>
{#/if}
</td>
<td align="center"><div align="right">{$T.record.moneyafter}</div></td>
<td align="center">
<div align="center">{$T.record.remark}</div>
</td>
</tr>
{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList" style="width: 100%; margin: 0; padding: 0" class="mrg10T">
    </div>
    <div id="ajaxPageBar" class="pages">
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
