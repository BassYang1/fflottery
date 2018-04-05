<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userlist.aspx.cs" Inherits="Lottery.Web.report.profitloss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <title>立博国际娱乐</title>
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.min.js"></script>
    <script type="text/javascript" src="/statics/json/LotAndSmalldata.js"></script>
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    <script type="text/javascript">
        var itemid = joinValue('itemid');
        var pagesize = 12;
        var page = thispage();
        $(document).ready(function () {
            $i('d1').value = GetDateStr(0);
            $i('d2').value = GetDateStr(1);
            ajaxList(page);
            $(".selectarea ul").delegate('li', 'click', function (event) {
                $(this).parent().find('li').removeClass();
                $(this).addClass('on');
            });
        });

        //个人盈亏
        function ajaxList(currentpage) {
            var u = "/ajax/ajaxProfitloss.aspx?oper=ajaxGetProList";
            if (currentpage != null)
                page = currentpage;
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: u + itemid,
                error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                success: function (d) {
                    switch (d.result) {
                        case '1':
                            $("#ajaxList").setTemplateElement("tplList", null, { filter_data: true });
                            $("#ajaxList").processTemplate(d);
                            $("#ajaxPageBar").html(d.pagebar);
                            break;
                    }
                    closeload(index);
                }
            });
        }

        function ajaxSearch() {
            itemid = "&type=" + $('#type').val() + "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&keys=" + encodeURIComponent($('#username').val());
            ajaxList(page);
        }

        function ajaxQuickSearch(obj) {
            if (obj == 1) {
                $i('d1').value = GetDateStr(0);
                $i('d2').value = GetDateStr(1);
            }
            if (obj == 2) {
                $i('d1').value = GetDateStr(-1);
                $i('d2').value = GetDateStr(0);
            }
            if (obj == 3) {
                $i('d1').value = GetDateStr(-6);
                $i('d2').value = GetDateStr(1);
            }
            if (obj == 4) {
                $i('d1').value = GetDateStrTs(30);
                $i('d2').value = GetDateStr(1);
            }
            if (obj == 5) {
                $i('d1').value = GetDateStrTs(90);
                $i('d2').value = GetDateStr(1);
            }
            if (obj == 6) {
                $i('d1').value = GetDateStrTs(360);
                $i('d2').value = GetDateStr(1);
            }

            itemid = "&type=" + $('#type').val() + "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&type=" + $('#type').val() + "&keys=" + encodeURIComponent($('#username').val());
            ajaxList(page);
        }

        function ajaxSearchById(obj) {
            itemid = "&type=" + $('#type').val() + "&Id=" + obj + "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&keys=" + encodeURIComponent($('#username').val().replace(/(^\s*)|(\s*$)/g, ''));
            ajaxList(1);
        }
    </script>
</head>
<body>
    <div class='selectmain'>
        <div class='selectarea'>
            <span>查询时间：</span>
            <input id="d1" type="text" onclick="WdatePicker({el:'d1'})" class="input-1" />
            <span>至</span>
            <input id="d2" type="text" onclick="WdatePicker({el:'d2'})" class="input-1" />
            <span>范围：</span>
            <select id="type" class="select-1">
                <option value="0">只查本级</option>
                <option value="1">包含下级</option>
            </select>
            <span>会员账号：</span>
            <input type='text' id='username' class="input-1" />
            <input type="button" class="btn-1" value="查询" onclick="ajaxSearch();" />
        </div>
        <div class='selectarea'>
            <ul>
                <span>快捷时间：</span>
                <li class="on" onclick="ajaxQuickSearch(1)">今日</li>
                <li onclick="ajaxQuickSearch(2)">昨日</li>
                <li onclick="ajaxQuickSearch(3)">七天</li>
                <li onclick="ajaxQuickSearch(4)">本月</li>
                <li onclick="ajaxQuickSearch(5)">三月</li>
                <li onclick="ajaxQuickSearch(6)">本年</li>
            </ul>
        </div>
    </div>
    <div>
        <div id="ajaxList" style="width: 100%; margin: 0; padding: 0">
        </div>
        <div id="ajaxList2" style="width: 100%; margin: 0; padding: 0">
        </div>
        <div id="ajaxPageBar">
        </div>
    </div>
    <textarea class="template" id="tplList" rows="1" cols="1" style="display: none">
<table width="100%" class="table-1" border="0" cellpadding="0" cellspacing="0">
<tr>
<th width="10"></th>
<th width="*"><div align="left">会员账号</div></th>
<th width="*"><div align="right">账号余额</div></th>
<th width="*"><div align="right">账号充值</div></th>
<th width="*"><div align="right">账号提现</div></th>
<th width="*"><div align="right">投注扣款</div></th>
<th width="*"><div align="right">奖金派送 </div></th>
<th width="*"><div align="right">游戏返点 </div></th>
<th width="*"><div align="right">活动礼金</div></th>
<th width="*"><div align="right">其他</div></th>
<%--<th width="*"><div align="right">积分兑换</div></th>--%>
<th width="*"><div align="right">实际盈亏</div></th>
<th width="10"></th>
</tr>
{#foreach $T.table as record}
<tr>
<td></td>
<td><div align="left">
{#if $T.record.id==<%=AdminId %>}
<font color="Red">[{$T.record.childnum}]</font>{$T.record.username}
{#else}
{#if $T.record.id == "0"}
{$T.record.username}
{#else}
{#if $T.record.childnum < 1}
<font color="Red">[{$T.record.childnum}]</font>{$T.record.username}
{#else}
<font color="Red">[{$T.record.childnum}]</font><a href="javascript:void(0)" onclick="ajaxSearchById({$T.record.id});">{$T.record.username}</a>
{#/if} 
{#/if} 
{#/if}
</div></td>
<td><div align="right">{$T.record.money}</div></td>
<td><div align="right">{$T.record.charge}</div></td>
<td><div align="right">{$T.record.getcash}</div></td>
<td><div align="right">{$T.record.bet}</div></td>
<td><div align="right">{$T.record.win}</div></td>
<td><div align="right">{$T.record.point}</div></td>
<td><div align="right">{$T.record.give}</div></td>
<td><div align="right">{$T.record.agentfh}</div></td>
<%--<td><div align="right">{$T.record.change}</div></td>--%>
<td><div align="right">
{#if $T.record.total > 0}
<font color="red">{$T.record.total}</font>
{#/if}
{#if $T.record.total == 0}
{$T.record.total}
{#/if}
{#if $T.record.total < 0}
<font color="green">{$T.record.total}</font>
{#/if}
</div></td>
<td></td>
</tr>
{#/for}
</table>
</textarea>
</body>
</html>
