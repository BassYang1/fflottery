<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="Lottery.WebApp.list" %>

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
    <script type="text/javascript" src="/statics/js/jquery_json.js"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
    <script src="/statics/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/statics/base/lotBase.js" type="text/javascript"></script>
    <script src="/statics/base/lotFun.js" type="text/javascript"></script>
    <script src="/statics/base/lotData.js" type="text/javascript"></script>
    <script type="text/javascript">
        var itemid = joinValue('itemid');
        var id = joinValue('id');
        var nav = GetQueryString("nav");
        var pagesize = 10;
        var page = thispage();
        var TableTemplate = GetPage(nav);
        var pagesize = TableTemplate.PageSize;

        $(document).ready(function () {
            var s = '<div class="input-group">';
            for (var i = 0, len = TableTemplate.Query.length; i < len; i++) {
                var tInput = TableTemplate.Query[i];
                if (tInput.InputType == "select") {
                    s += '<select id=' + tInput.InputId;
                    if (tInput.OnChange != null)
                        s += ' onchange="' + tInput.OnChange + '()"';
                    s += '>';
                    if (tInput.OptUrl != null) {
                        s += '<Option value="">' + tInput.InputTitle + '</Option>';
                        $.ajax({
                            type: "get",
                            dataType: "json",
                            async: false,
                            data: "clienttime=" + Math.random(),
                            url: tInput.OptUrl,
                            error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
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
                }

                if (tInput.InputType == "DateTime") {
                    s += '<div class="query-date"><input id="' + tInput.InputId + '" type="text" class="ipt" value="' + tInput.Value + '" placeholder="' + tInput.InputTitle + '"/><i class="icon icon-date" onclick="WdatePicker({el:\'' + tInput.InputId + '\'})"></i></div>';
                }

                if (tInput.InputType == "Input") {
                    if (tInput.Keyup != null)
                        s += '<input id="' + tInput.InputId + '" class="ipt" type="text" placeholder="' + tInput.InputTitle + '" onkeypress="chkPrice(this)" onkeyup="chkPrice(this)";/>';
                    else
                        s += '<input id="' + tInput.InputId + '" class="ipt" type="text" placeholder="' + tInput.InputTitle + '" />';
                }
            }

            for (var i = 0, len = TableTemplate.Botton.length; i < len; i++) {
                var tBotton = TableTemplate.Botton[i];
                if (tBotton.Link != null) {
                    s += '<a href="' + tBotton.Link + '" class="btn btn-query2">' + tBotton.Title + '</a>';
                }
                else {
                    s += '<input type="button" class="btn btn-query" value="' + tBotton.Title + '" onclick="' + tBotton.Function + ';" />';
                }
            }
            s += '</div>';
            $("#ajaxInput").html(s);

            switch (nav) {
                case "ContractUserList":
                case "ContractUserListFH":
                case "ContractUserListGZ":
                case "UserProListSub":
                case "ContractGZRecord":
                case "ContractFHRecord":
                case "ContractFHLog":
                case "ContractGZLog":
                    ajaxSearch();
                    break;
                default:
                    break;
            }
        });

        function CreateTable(d, list) {
            var s = '';
            if (d.table.length > 0) {
                s += '<table class="query-table">';
                s += '<thead><tr>';
//                s += '<th class="shape"><i class="icon icon-shape"></i></th>';
                //s += '<th style="width:30px;" align="center">序号</th>';
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
                    //                    s += '<td><i class="icon icon-shape"></i></td>';
                    //s += '<td>' + (i + 1) + '</td>';
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
                                if (tlist.Function != null) {
                                    s += '<a href="javascript:;;" onclick="' + tlist.Function + '(' + t.id + ')">';
                                }
                                if (tlist.FunctionUserParent != null) {
                                    s += '<a href="javascript:;;" onclick="' + tlist.FunctionUserParent + '(' + t.id + ',' + t.parentid + ')">';
                                }
                                if (tlist.FunctionUserId != null) {
                                    s += '<a href="javascript:;;" onclick="' + tlist.FunctionUserId + '(' + t.userid + ')">';
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
                                        if (tlist.Default[0][t[tlist.Filed]] == "未开奖") {
                                            s += tlist.Default[0][t[tlist.Filed]];
                                        }
                                        else if (tlist.Default[0][t[tlist.Filed]] == "已撤单") {
                                            s += '<font color="#ccc">' + tlist.Default[0][t[tlist.Filed]] + '</font>';
                                        }
                                        else if (tlist.Default[0][t[tlist.Filed]] == "未中奖") {
                                            s += '<font color="green">' + tlist.Default[0][t[tlist.Filed]] + '</font>';
                                        }
                                        else if (tlist.Default[0][t[tlist.Filed]] == "已中奖") {
                                            s += '<font color="Red">' + tlist.Default[0][t[tlist.Filed]] + '</font>';
                                        }
                                        else if (tlist.Default[0][t[tlist.Filed]] == "在线") {
                                            s += '<font color="Red">' + tlist.Default[0][t[tlist.Filed]] + '</font>';
                                        }
                                        else {
                                            s += tlist.Default[0][t[tlist.Filed]];
                                        }
                                    }
                                }
                                else if (tlist.IsPop != null) {
                                    s += '<a href="#" onmouseover="mOver(this,\'' + t[tlist.Filed] + '\')" onmouseout="mOut()">查看备注</a>';
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
                                if (tlist.FunctionUserId != null) {
                                    s += '</a>';
                                }
                                if (tlist.FunctionUserParent != null) {
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

                                for (var k = 0; k < fo.length; k++) {
                                    if (t[fo[k].F1] == t[fo[k].F2]) {
                                        if (fo[k].Type != "Link") {
                                            if (nav == "ContractUserList") { //加载契约会员
                                                if (fo[k].Page && fo[k].Page == "ContractGZ") {
                                                    if (t.contractgz && t.contractgz == "1") {
                                                        s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link" style="color:red;">[已签订工资]</a>'
                                                    }
                                                    else {
                                                        s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link">' + fo[k].Title + '</a>'
                                                    }
                                                }
                                                else if (fo[k].Page && fo[k].Page == "ContractFH") {
                                                    if (t.contractfh && t.contractfh == "1") {
                                                        s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link" style="color:red;">[已签订分红]</a>'
                                                    }
                                                    else {
                                                        s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link">' + fo[k].Title + '</a>'
                                                    }
                                                }
                                                else {
                                                    s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link">' + fo[k].Title + '</a>'
                                                }
                                            }
                                            else if (nav == "ContractUserListFH") { //加载分红契约会员
                                                //-1-未签定契约
                                                //0-契约待接受
                                                //1-契约已签订
                                                //2-契约已拒绝，可重新分配
                                                //3-契约撤销，等待会员同意！
                                                //4-会员同意撤销，请您修改契约！
                                                var title = fo[k].Title;
                                                if (t.contractfh && t.contractfh == "0") title = "<font color='red'>等待确认</fond>";
                                                if (t.contractfh && t.contractfh == "1") title = "<font color='red'>查看契约</fond>";
                                                if (t.contractfh && t.contractfh == "3") title = "<font color='red'>等待确认</fond>";

                                                s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link">' + title + '</a>';
                                            }
                                            else if (nav == "ContractUserListGZ") { //加载工资契约会员
                                                //-1-未签定契约
                                                //0-契约待接受
                                                //1-契约已签订
                                                //2-契约已拒绝，可重新分配
                                                //3-契约撤销，等待会员同意！
                                                //4-会员同意撤销，请您修改契约！
                                                var title = fo[k].Title;
                                                if (t.contractgz && t.contractgz == "0") title = "<font color='red'>等待确认</fond>";
                                                if (t.contractgz && t.contractgz == "1") title = "<font color='red'>查看契约</fond>";
                                                if (t.contractgz && t.contractgz == "3") title = "<font color='red'>等待确认</fond>";

                                                s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link">' + title + '</a>';
                                            }
                                            else {
                                                s += '<a href="javascript:;;" onclick="' + fo[k].Function.replace("@@", t.id).replace("@UserId@", t.userid) + '" class="link">' + fo[k].Title + '</a>';
                                            }
                                        }
                                        else {

                                            if (nav == "ContractGZLog" || nav == "ContractFHLog") {
                                                if (t.allowed == "True") {
                                                    s += '<a href="' + fo[k].Function.replace("@@", t.id).replace("@url@", t.url).replace("@Code@", t.code).replace("@UserId@", t.userid) + '" class="link">' + fo[k].Title + '</a>';
                                                }
                                            }
                                            else if (nav == "UserProListSub") {
                                                if (t.subcount != "0") {
                                                    s += '<a href="' + fo[k].Function.replace("@@", t.id).replace("@url@", t.url).replace("@Code@", t.code).replace("@UserId@", t.userid) + '" class="link">' + fo[k].Title + '</a>';
                                                }
                                            }
                                            else {
                                                s += '<a href="' + fo[k].Function.replace("@@", t.id).replace("@url@", t.url).replace("@Code@", t.code).replace("@UserId@", t.userid) + '" class="link">' + fo[k].Title + '</a>';
                                            }
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
                s += '<table class="query-table">';
                s += '<thead><tr><th></th></tr></thead>';
                s += '<tbody><tr><td><div class="query-empty">当前条件没有记录</div></td></tr></tbody>';
                s += '</table>';
                $(list).html(s);
                $("#ajaxPageBar").html("");
            }
        }

        function ajaxList(currentpage) {
            if (currentpage != null)
                page = currentpage;
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: TableTemplate.Url + itemid + id,
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    CreateTable(d, "#ajaxList");
                    closeload(index);
                }
            });
        }
        function ajaxSearch() {
            var s = "";
            var selects = ajaxInput.getElementsByTagName("select");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
            }
            var inputs = ajaxInput.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                s += "&" + inputs[i].id + "=" + $('#' + inputs[i].id).val();
            }
            itemid = s;
            ajaxList(1);

            //if (nav == "UserProListSub") {
            //    var $table = $("#ajaxList>.query-table>");

            //    if ($table.find("tr").size() == 2) {
            //        $table.find("tr:first").hide();
            //    }
            //}
        }

        function ajaxSearchById(obj) {
            var s = "&Id=" + obj;
            var selects = ajaxInput.getElementsByTagName("select");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
            }
            var selects = ajaxInput.getElementsByTagName("input");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
            }
            itemid = s;
            ajaxList(1);
        }

        function ajaxSearchByParent(obj, objParent) {
            setCookie("userParent", objParent);
            var s = "&Id=" + obj;
            var selects = ajaxInput.getElementsByTagName("select");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
            }
            var selects = ajaxInput.getElementsByTagName("input");
            for (var i = 0; i < selects.length; i++) {
                s += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
            }
            itemid = s;
            ajaxList(1);
        }

        function ajaxToParent() {
            if (getCookie("userParent") != null) {
                var s = "&Id=" + getCookie("userParent");
                var selects = ajaxInput.getElementsByTagName("select");
                for (var i = 0; i < selects.length; i++) {
                    s += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
                }
                var selects = ajaxInput.getElementsByTagName("input");
                for (var i = 0; i < selects.length; i++) {
                    s += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
                }
                itemid = s;
                ajaxList(1);
            }
            else {
                emAlert("已经是最顶级了，不能返回！");
            }
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

        function ajaxRegStrAll() {
            $.ajax({
                type: "post",
                dataType: "json",
                data: "",
                url: "/ajax/ajaxUser.aspx?oper=ajaxRegStrAll&clienttime=" + Math.random(),
                success: function (data) {
                    if (data.result == "1") {
                        ajaxList(page);
                        emAlert(data.returnval);
                    }
                }
            });
        }

        function ajaxGZReissue(id) {
            $.ajax({
                type: "get",
                dataType: "json",
                data: "",
                url: "/ajax/ajaxContractGZ.aspx?oper=ajaxGZReissue&id=" + id + "&clienttime=" + Math.random(),
                success: function (data) {
                    if (data.result == "0") {
                        ajaxList(page);
                        emAlert(data.returnval);
                    }
                    else {
                        emAlert(data.returnval);
                    }
                }
            });
        }

        function ajaxFHReissue(id) {
            $.ajax({
                type: "get",
                dataType: "json",
                data: "",
                url: "/ajax/ajaxContractFH.aspx?oper=ajaxFHReissue&id=" + id + "&clienttime=" + Math.random(),
                success: function (data) {
                    if (data.result == "0") {
                        ajaxList(page);
                        emAlert(data.returnval);
                    }
                    else {
                        emAlert(data.returnval);
                    }
                }
            });
        }

        function mOver(obj, str) {
            layer.tips(str, obj, {
                tips: [3, '#0FA6D8'] //还可配置颜色
            });
        }

        function mOut() {
            layer.closeAll('tips');
        }
    </script>
</head>
<body>
    <div class="query-area">
        <div class="query-form">
            <form id="ajaxInput" action="" method="post">
            </form>
        </div>
        <div id="ajaxList" class="query-result" style="width: 100%">
            <table class="query-table">
                <thead>
                    <tr>
                        <th>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <div class="query-empty">
                                请点击<font color="red">查询</font>按钮显示数据！</div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="ajaxPageBar" class="pagination">
        </div>
    </div>
</body>
</html>
