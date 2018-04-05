<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="conListpop.aspx.cs" Inherits="Lottery.Admin.conListpop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>EM-CLUB后台管理系统</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script src="/_libs/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript" src="/_libs/My97DatePicker/WdatePicker.js"></script>
    <script src="/statics/admin/js/lotBase.js" type="text/javascript"></script>
    <script src="/statics/admin/js/lotFun.js" type="text/javascript"></script>
    <script src="/statics/admin/js/lotData.js" type="text/javascript"></script>
    <script src="/statics/json/playBigdate.js" type="text/javascript"></script>
    <script src="/statics/json/LotAndSmalldata.js" type="text/javascript"></script>
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script src="/_libs/Jquery.tableExport/jquery.table2excel.js" type="text/javascript"></script>
    <script src="/_libs/Jquery.tableExport/TimeObjectUtil.js" type="text/javascript"></script>
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/statics/global.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var itemid = joinValue('itemid');
        var id = joinValue('id');
        var TableTemplate = GetPage(GetQueryString("page"));
        var pagesize = TableTemplate.PageSize;
        var page = thispage();
        $(document).ready(function () {
            ajaxSearch();
        });

        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        }

        function CreateTable(d, list) {
            var s = '';
            if (d.table.length > 0) {
                s += '<table class="query-table">';
                s += '<thead><tr>';
                s += '<th class="shape"><i class="icon icon-shape"></i></th>';
                s += '<th style="width:30px;" align="center">序号</th>';
                for (var i = 0, len = TableTemplate.List.length; i < len; i++) {
                    var tlist = TableTemplate.List[i];
                    if (tlist.Filed == 'checkbox') {
                        s += '<th align="center" scope="col" style="width:' + tlist.Width + ';"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全选/反选" /></th>';
                    }
                    else {
                        s += '<th style="width:' + tlist.Width + ';" align="' + tlist.Align + '">' + tlist.Header + '</th>';
                    }
                }
                s += '</tr></thead><tbody>';
                for (var i = 0; i < d.table.length; i++) {
                    var t = d.table[i];
                    s += '<tr>';
                    s += '<td><i class="icon icon-shape"></i></td>';
                    s += '<td>' + (i + 1) + '</td>';
                    for (var j = 0; j < TableTemplate.List.length; j++) {
                        var tlist = TableTemplate.List[j];
                        if (tlist.Filed != null) {
                            if (tlist.Filed == 'checkbox') {
                                s += '<td align="' + tlist.Align + '"><input class="checkbox" name="selectID" type="checkbox" value=' + t.id + ' /></td>';
                            }
                            else {
                                s += '<td align="' + tlist.Align + '">';
                                if (tlist.Color != null) {
                                    s += '<font color="' + tlist.Color + '">';
                                }
                                if (tlist.Link != null) {
                                    var ssid = t[tlist.Filed].toString();
                                    if (ssid.substr(0, 2) == 'B_') {
                                        var url = "/admin/conList.aspx?page=BetList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'Z_') {
                                        var url = "/admin/conList.aspx?page=BetZhList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'C_') {
                                        var url = "/admin/conList.aspx?page=ChargeList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'L_') {
                                        var url = "/admin/conList.aspx?page=TranAccList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'G_') {
                                        var url = "/admin/conList.aspx?page=CashList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'A_') {
                                        var url = "/admin/conList.aspx?page=ActiveRecord&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                }
                                if (tlist.Function != null) {
                                    s += '<a href="javascript:void(0);" onclick="' + tlist.Function + '(' + t.id + ')">';
                                }
                                if (tlist.Default != null) {
                                    if (tlist.Function != null) {
                                        if (t[tlist.Filed] == "0") {
                                            s += '<font color="Red">' + tlist.Default[0][t[tlist.Filed]] + '</font>';
                                        }
                                        else {
                                            s += '<font color="Blue">' + tlist.Default[0][t[tlist.Filed]] + '</font>';
                                        }
                                    }
                                    else {
                                        s += tlist.Default[0][t[tlist.Filed]];
                                    }
                                }
                                else {
                                    if (tlist.TwoColor != null) {
                                        if (t[tlist.Filed] > 0) {
                                            s += '<font color="red">' + t[tlist.Filed] + '</font>';
                                        }
                                        else if (t[tlist.Filed] < 0) {
                                            s += '<font color="green">' + t[tlist.Filed] + '</font>';
                                        }
                                        else {
                                            s += t[tlist.Filed];
                                        }
                                    }
                                    else {
                                        s += t[tlist.Filed];
                                    }
                                }
                                if (tlist.Filed2 != null) {
                                    s += '<font color="red">(' + t[tlist.Filed2] + ')</font>';
                                }
                                if (tlist.Function != null) {
                                    s += '</a>';
                                }
                                if (tlist.Link != null) {
                                    s += '</a>';
                                }
                                if (tlist.Color != null) {
                                    s += '</font>';
                                }
                                s += '</td>';
                            }
                        }
                        else {
                            if (tlist.Info.length > 0) {
                                s += '<td align="' + tlist.Align + '">'
                                var fo = tlist.Info;
                                if (fo.length > 1) {
                                    s += '<div class="topnav">';
                                    s += '<div class="topmenu"><a href="javascript:void(0);" class="btn btn-action"><span>功能操作</span></a>';
                                    s += '<ul class="sub">';
                                    for (var k = 0; k < fo.length; k++) {
                                        if (fo[k].Type != "Link") {
                                            s += '<li><a href="javascript:void(0);" onclick="' + fo[k].Function.replace("@@", t.id) + '">' + fo[k].Title + '</a></li>';
                                        }
                                        else {
                                            s += '<li><a href="' + fo[k].Function.replace("@@", t.id).replace("@url@", t.url).replace("@Code@", t.code).replace("@UserId@", t.userid) + '">' + fo[k].Title + '</a></li>';
                                        }
                                    }
                                    s += '</ul>';
                                    s += '</div>';
                                }
                                else {
                                    if (fo[0].Filed != null) {
                                        if (t[fo[0].Filed] == 0) {
                                            if (fo[0].Type != "Link") {
                                                s += '<li><a href="javascript:void(0);" onclick="' + fo[0].Function.replace("@@", t.id) + '" class="btn btn-action">' + fo[0].Title + '</a></li>';
                                            }
                                            else {
                                                s += '<li><a href="' + fo[0].Function.replace("@@", t.id).replace("@url@", t.url).replace("@Code@", t.code).replace("@UserId@", t.userid) + '" class="btn btn-action">' + fo[0].Title + '</a></li>';
                                            }
                                        }
                                    }
                                    else {
                                        if (fo[0].Type != "Link") {
                                            s += '<li><a href="javascript:void(0);" onclick="' + fo[0].Function.replace("@@", t.id) + '" class="btn btn-action">' + fo[0].Title + '</a></li>';
                                        }
                                        else {
                                            s += '<li><a href="' + fo[0].Function.replace("@@", t.id).replace("@url@", t.url).replace("@Code@", t.code).replace("@UserId@", t.userid) + '" class="btn btn-action">' + fo[0].Title + '</a></li>';
                                        }
                                    }
                                }
                                s += '</td>';
                            }
                        }
                    }
                    s += '</tr>';
                }
                s += '</tbody></table>';
                $(list).html(s);
            }
            else {
                s += '<div class="query-empty">暂时还没有任何数据</div>';
                $(list).html(s);
            }
        }

        function ajaxList(currentpage) {
            if (currentpage != null) page = currentpage;
            top.Lottery.Loading.show("正在努力加载，请稍后...");
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: TableTemplate.Url + itemid + id,
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
                            CreateTable(d, "#ajaxList");
                            top.Lottery.Message(d.returnval, "1");
                            break;
                    }
                }
            });
        }
        function ajaxSearch() {
            ajaxList(1);
        }
    </script>
</head>
<body>
    <div id="ajaxQuery" class="query-bar">
        <div id="ajaxButton" class="query-actions">
        </div>
        <div class="query-conditions">
            <form id="ajaxInput" class="query-form">
            <input type="button" class="btn btn-primary" value="查询" onclick="ajaxSearch()" />
            </form>
        </div>
    </div>
    <div id="ajaxList" class="query-result">
    </div>
</body>
</html>
