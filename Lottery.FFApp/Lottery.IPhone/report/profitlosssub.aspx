<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profitlosssub.aspx.cs" Inherits="Lottery.Web.report.profitlosssub" %>

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
        var nmb = 1;
        var pagesize = 11;
        var page = thispage();
        $(document).ready(function () {
            $i('d1').value = GetDateStr(0);
            $i('d2').value = GetDateStr(1);
            ajaxList(page);
        });

        //个人盈亏
        function ajaxList(currentpage) {
            var u = "/ajax/ajaxProfitloss.aspx?oper=ajaxGetProListId";
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
        var nmb = 0;
        function ajaxSearch() {
            itemid = "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&keys=" + encodeURIComponent($('#username').val().replace(/(^\s*)|(\s*$)/g, ''));
            ajaxList(page);
        }
        function ajaxSearchById(obj) {
            itemid = "&Id=" + obj + "&d1=" + $('#d1').val() + "&d2=" + $('#d2').val() + "&keys=" + encodeURIComponent($('#username').val().replace(/(^\s*)|(\s*$)/g, ''));
            ajaxList(1);
        }
    </script>
</head>
<body>
    <div class='user_main_top_a'>
        <div class='user_main_top_a_div'>
            <span class="userList_style_span">查询时间：</span> <span>
                <input id="d1" type="text" onclick="WdatePicker({el:'d1'})" class="user_main_top_a_div_input" />
            </span><span>至</span> <span>
                <input id="d2" type="text" onclick="WdatePicker({el:'d2'})" class="user_main_top_a_div_input" /></span>
            <span class="userList_style_span">
                <input type="button" class="btn_allcss width80 btntop5" value="查询" onclick="ajaxSearch();" />
            </span>
            <div class='clear'>
            </div>
        </div>
    </div>
    <div>
        <div id="ajaxList" style="width: 100%; margin: 0; padding: 0">
        </div>
        <div id="ajaxPageBar">
        </div>
    </div>
    <textarea class="template" id="tplList" rows="1" cols="1" style="display: none">
<table width="100%" class="table-1" border="0" cellpadding="0" cellspacing="0">
<tr>
<th width="*"><div align="center">会员账号</div></th>
<th width="*"><div align="right">账号余额</div></th>
<th width="*"><div align="right">账号充值</div></th>
<th width="*"><div align="right">账号提现</div></th>
<th width="*"><div align="right">投注扣款</div></th>
<th width="*"><div align="right">奖金派送 </div></th>
<th width="*"><div align="right">游戏返点 </div></th>
<th width="*"><div align="right">活动礼金</div></th>
<th width="*"><div align="right">代理分红</div></th>
<th width="*"><div align="right">积分兑换</div></th>
<th width="*"><div align="right">实际盈亏</div></th>
<th width="10"></th>
</tr>
{#foreach $T.table as record}
<tr>
<td><div align="center">
{#if $T.record.id == "0"}
{$T.record.username}
{#else}
<a href="javascript:void(0)" onclick="ajaxSearchById({$T.record.id});">{$T.record.username}<font color="Red">[{$T.record.childnum}]</font></a>
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
<td><div align="right">{$T.record.change}</div></td>
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
