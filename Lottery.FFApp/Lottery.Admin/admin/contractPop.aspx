<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contractPop.aspx.cs" Inherits="Lottery.Admin.contractPop" %>

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
        var TableTemplate = GetPage("UserContractDetail");
        var pagesize = TableTemplate.PageSize;
        var page = thispage();
        $(document).ready(function () {
            var s = "";
            for (var i = 0, len = TableTemplate.Query.length; i < len; i++) {
                var tInput = TableTemplate.Query[i];
                if (tInput.InputType == "select") {
                    s += '<div class="form-group">';
                    if (tInput.InputTitle != "") {
                        s += '<label class="lab">' + tInput.InputTitle + ':</label>';
                    }
                    s += '<select id=' + tInput.InputId + ' class=' + tInput.InputClass + ' style="width: ' + tInput.Width + ';"';
                    if (tInput.OnChange != null)
                        s += ' onchange="' + tInput.OnChange + '()"';
                    s += '>';
                    if (tInput.OptUrl != null) {
                        s += '<Option value="">所有类型</Option>';
                        $.ajax({
                            type: "get",
                            dataType: "json",
                            async: false,
                            data: "clienttime=" + Math.random(),
                            url: tInput.OptUrl,
                            error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                            success: function (d) {
                                for (var j = 0; j <= d.table.length - 1; j++) {
                                    s += '<Option value="' + d.table[j].id + '">' + d.table[j].name + '</Option>';
                                }
                            }
                        });
                    }
                    else {
                        for (var obj in tInput.Options) {
                            s += '<Option value="' + tInput.Options[obj]["key"] + '">' + tInput.Options[obj]["value"] + '</Option>';
                        }
                    }
                    s += '</select>';
                    s += '</div>';
                }

                if (tInput.InputType == "DateTime") {
                    s += '<div class="form-group">';
                    s += '<label class="lab">' + tInput.InputTitle + ':</label> <input id="' + tInput.InputId + '" class="ipt" type="text" style="width: ' + tInput.Width + ';" value="' + tInput.Value + '" onclick="WdatePicker({el:\'' + tInput.InputId + '\'})" />';
                    s += '</div>';
                }

                if (tInput.InputType == "Input") {
                    if (tInput.InputTitle != "") {
                        s += '<div class="form-group">';
                        s += '<label class="lab">' + tInput.InputTitle + ':</label>';
                    }
                    else {
                        s += '<div class="form-group2">';
                    }
                    if (tInput.Keyup != null)
                        s += '<input id="' + tInput.InputId + '" class="ipt" type="text" style="width: ' + tInput.Width + '" onkeypress="chkPrice(this)" onkeyup="chkPrice(this)";/>';
                    else
                        s += '<input id="' + tInput.InputId + '" class="ipt" type="text" style="width: ' + tInput.Width + ';" />';
                    s += '</div>';
                }
            }
            $("#ajaxInput").html(s);

            var btn = "";
            for (var i = 0, len = TableTemplate.Botton.length; i < len; i++) {
                var tBotton = TableTemplate.Botton[i];
                if (tBotton.Link != null) {
                    btn += '<a href="' + tBotton.Link + '" class="' + tBotton.InputClass + '">' + tBotton.Title + '</a>';
                }
                else {
                    btn += '<input type="button" class="' + tBotton.InputClass + '" value="' + tBotton.Title + '" onclick="' + tBotton.Function + ';" />';
                }
            }
            $("#ajaxButton").html(btn);

            if (TableTemplate.Botton.length > 0 || TableTemplate.Query.length > 0)
                $("#ajaxQuery").removeClass().addClass("query-bar");
            else
                $("#ajaxQuery").removeClass().addClass("query-bar0");
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
                                        var url = "/admin/conListpop.aspx?page=BetList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'Z_') {
                                        var url = "/admin/conListpop.aspx?page=BetZhList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'C_') {
                                        var url = "/admin/conListpop.aspx?page=ChargeList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'L_') {
                                        var url = "/admin/conListpop.aspx?page=TranAccList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'G_') {
                                        var url = "/admin/conListpop.aspx?page=CashList&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                    if (ssid.substr(0, 2) == 'A_') {
                                        var url = "/admin/conListpop.aspx?page=ActiveRecord&id=" + t[tlist.Filed];
                                        s += '<a href="javascript:void(0);" onclick=LayerPop("' + t[tlist.Filed] + '","' + url + '")>';
                                    }
                                }
                                if (tlist.Function != null) {
                                    s += '<a href="javascript:void(0);" onclick="' + tlist.Function + '(' + t.id + ')">';
                                }
                                if (tlist.FunctionUserId != null) {
                                    s += '<a href="javascript:void(0);" onclick="' + tlist.FunctionUserId + '(' + t.userid + ')">';
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
                                            s += t[tlist.Filed].length > 30 ? t[tlist.Filed].substr(0,30)+'...' : t[tlist.Filed];
                                        }
                                    }
                                    else {
                                        s += t[tlist.Filed].length > 30 ? t[tlist.Filed].substr(0, 30) + '...' : t[tlist.Filed];
                                    }
                                }
                                if (tlist.Filed2 != null) {
                                    s += '<font color="red">(' + t[tlist.Filed2] + ')</font>';
                                }
                                if (tlist.FunctionUserId != null) {
                                    s += '</a>';
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
                                            s += '<li><a href="javascript:void(0);" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '">' + fo[k].Title + '</a></li>';
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
                                                s += '<li><a href="javascript:void(0);" onclick="' + fo[0].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="btn btn-action">' + fo[0].Title + '</a></li>';
                                            }
                                            else {
                                                s += '<li><a href="' + fo[0].Function.replace("@@", t.id).replace("@url@", t.url).replace("@Code@", t.code).replace("@UserId@", t.userid) + '" class="btn btn-action">' + fo[0].Title + '</a></li>';
                                            }
                                        }
                                    }
                                    else {
                                        if (fo[0].Type != "Link") {
                                            s += '<li><a href="javascript:void(0);" onclick="' + fo[0].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="btn btn-action">' + fo[0].Title + '</a></li>';
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

                //合计行
                if (TableTemplate.SumList != null) {
                    s += '<tr>';
                    s += '<td><i class="icon icon-shape"></i></td>';
                    s += '<td></td>';
                    for (var j = 0; j < TableTemplate.SumList.length; j++) {
                        var tlist = TableTemplate.SumList[j];
                        if (tlist.Filed != null) {
                            if (tlist.Filed == '') {
                                if (tlist.Title != null) {
                                    s += '<td align="' + tlist.Align + '">' + tlist.Title + '</td>';
                                }
                                else {
                                    s += '<td align="' + tlist.Align + '">---</td>';
                                }
                            }
                            else {
                                s += '<td align="' + tlist.Align + '">';
                                var sum = 0;
                                for (var i = 0; i < d.table.length; i++) {
                                    var t = d.table[i];
                                    sum += Number(t[tlist.Filed]);
                                }
                                s += sum.toFixed(4);
                                s += '</td>';
                            }
                        }
                    }
                    s += '</tr>';
                }

                s += '</tbody></table>';
                $(list).html(s);
                $("#ajaxPageBar").html(d.pagebar);
                if (TableTemplate.Page != null) {
                    if (TableTemplate.Page != true) {
                        $("#ajaxPageBar").html("");
                    }
                }
            }
            else {
                s += '<div class="query-empty">暂时还没有任何数据</div>';
                $(list).html(s);
                $("#ajaxPageBar").html("");
            }
        }

        function ajaxList(currentpage) {
            if (currentpage != null) page = currentpage;
            top.Lottery.Loading.show("正在努力加载，请稍后...");
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: TableTemplate.Url + "&id=72",
                error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { alert(XmlHttpRequest.responseText); } }, //alert("网络堵塞,稍后再试！"); },
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
            var s = "";
            var selects = ajaxInput.getElementsByTagName("select");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + encodeURIComponent($('#' + selects[i].id).val());
            }
            var inputs = ajaxInput.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                s += "&" + inputs[i].id + "=" + encodeURIComponent($('#' + inputs[i].id).val());
            }
            itemid = s;
            ajaxList(1);
        }

        function ajaxSearchById(obj) {
            var s = "&Id=" + obj;
            var selects = ajaxInput.getElementsByTagName("select");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + encodeURIComponent($('#' + selects[i].id).val());
            }
            var selects = ajaxInput.getElementsByTagName("input");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + encodeURIComponent($('#' + selects[i].id).val());
            }
            itemid = s;
            ajaxList(1);
        }
        function ajaxSearchAll() {
            pagesize = 99999;
            var s = "";
            var selects = ajaxInput.getElementsByTagName("select");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + encodeURIComponent($('#' + selects[i].id).val());
            }
            var inputs = ajaxInput.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                s += "&" + inputs[i].id + "=" + encodeURIComponent($('#' + inputs[i].id).val());
            }
            itemid = s;
            ajaxList(1);
        }
        function Confirmclear() {
            top.Lottery.Confirm("确定要清空吗?", "getCurrentIframe().ajaxClear()");
        }
        function ajaxClear() {
            top.Lottery.Loading.show("正在清空...");
            $.ajax({
                type: "get",
                dataType: "json",
                url: TableTemplate.ClearUrl + "&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) { top.Lottery.Loading.hide();  },
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
                            ajaxList(1);
                            break;
                    }
                }
            });
        }
        function ConfirmDel(id) {
            top.top.Lottery.Confirm("确定要删除此记录吗?", "getCurrentIframe().ajaxDel(" + id + ")");
        }
        function ajaxDel(id) {
            $.ajax({
                type: "post",
                dataType: "json",
                data: "id=" + id,
                url: TableTemplate.DelUrl + "&clienttime=" + Math.random(),
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
        function ajaxStates(id) {
            $.ajax({
                type: "post",
                dataType: "json",
                data: "id=" + id,
                url: TableTemplate.StateUrl + "&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    if (d.result == "1") {
                        ajaxList(page);
                    }
                }
            });
        }

        function ajaxStates2(id) {
            $.ajax({
                type: "post",
                dataType: "json",
                data: "id=" + id,
                url: TableTemplate.State2Url + "&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    if (d.result == "1") {
                        ajaxList(page);
                    }
                }
            });
        }

        function PagePop(url) {
            top.Lottery.Popup.show(url, 800, 500, true);
        }

        function LayerPop(title, url) {
            layer.open({
                type: 2,
                title: title + ' 详情',
                shadeClose: true,
                shade: 0.8,
                area: ['100%', '100%'],
                content: url
            });
        }

        function PagePopUrl(url,width,heigth) {
            top.Lottery.Popup.show(url, width, heigth, true);
        }

        //验证方法
        function chkPrice(obj) {
            obj.value = obj.value.replace(/[^\d]/g, "");
            //必须保证第一位为数字而不是. 
            obj.value = obj.value.replace(/^\./g, "");
            //保证只有出现一个.而没有多个. 
            obj.value = obj.value.replace(/\.{2,}/g, ".");
            //保证.只出现一次，而不能出现两次以上 
            obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
        }

        function Table2Excel(eName) {
            var d1 = $('#d1').val();
            var d2 = $('#d2').val();
            top.Lottery.Loading.show("正在努力导出，请稍后...");
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=0&pagesize=99999&clienttime=" + Math.random(),
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
                            if (d.table.length > 0) {
                                CreateTable(d, "#ajaxListExcel");
                                $(".query-result2").table2excel({
                                    exclude: ".noExl",
                                    name: "Excel Document Name",
                                    filename: eName + "(" + d1 + "---" + d2 + ")",
                                    exclude_img: true,
                                    exclude_links: true,
                                    exclude_inputs: true
                                });
                                top.Lottery.Message(d.returnval, "1");
                            }
                            else {
                                top.Lottery.Alert("暂时还没有任何数据，不能导出！", "0");
                            }
                            break;
                    }
                }
            });
        }
    </script>
</head>
<body>
    <div id="ajaxQuery" class="query-bar">
        <div id="ajaxButton" class="query-actions">
        </div>
        <div class="query-conditions">
            <form id="ajaxInput" class="query-form">
            </form>
        </div>
    </div>
    <div id="ajaxList" class="query-result">
    </div>
    <div id="ajaxList2" class="query-result">
    </div>
    <div id="ajaxList3" class="query-result">
    </div>
    <div id="ajaxListExcel" class="query-result2" style="display: none;">
    </div>
    <div id="ajaxPageBar" class="pagination">
    </div>
</body>
</html>
