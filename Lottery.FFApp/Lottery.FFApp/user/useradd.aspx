<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="useradd.aspx.cs" Inherits="Lottery.WebApp.user.useradd" %>

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
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/statics/formValidator.js" type="text/javascript"></script>
    <script src="/statics/global.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        var pagesize = 12;
        var page = thispage();
        $(document).ready(function () {
            $.formValidator.initConfig({ onerror: function (msg, obj, errorlist) {
                emAlert(msg);
            },
                onsuccess: function () { return true; }
            });
            $("#txtUserName").formValidator({ tipid: "tipUserName", onshow: "请输入会员账号", onfocus: "由0-9，z-a,A-Z组成的6-16位的字符" }).InputValidator({ min: 5, max: 20, onerror: "会员账号为5-20个字符" }).RegexValidator({ regexp: "username", datatype: "enum", onerror: "汉字或字母开头,不支持符号" });
            $("#txtPoint").formValidator({ tipid: "tipPoint", onshow: "请输入返点", onfocus: "请输入返点" }).InputValidator({ min: 1, max: 5, onerror: "请输入返点" });
            $(".tto-tabs .tabs-nav").delegate('li', 'click', function (event) {
                var nmb = $(this).attr("nmb");
                $(this).parents().find("li").removeClass("ui-state-active");
                $(this).addClass("ui-state-active");
                if (nmb == 0) {
                    $("#tp-account").show();
                    $("#tp-linkreg").hide();
                    $("#tp-manage").hide();
                    ajaxUserGroupList(1);
                }
                else if (nmb == 1) {
                    $("#tp-account").hide();
                    $("#tp-linkreg").show();
                    $("#tp-manage").hide();
                }
                else {
                    $("#tp-account").hide();
                    $("#tp-linkreg").hide();
                    $("#tp-manage").show();
                    ajaxList(1);
                }
            });

            $("#tp-account").show();
            $("#tp-linkreg").hide();
            $("#tp-manage").hide();
            setTimeout('ajaxUserGroupList(1)', 1000);
        });


        function ajaxList(currentpage) {
            if (currentpage != null)
                page = currentpage;
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=10&clienttime=" + Math.random(),
                url: "/ajax/ajaxUser.aspx?oper=ajaxGetRegStrList",
                error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                success: function (d) {
                    if (d.result == "1") {
                        var html = '';
                        html += '<table class="query-table">';
                        html += '<thead>';
                        html += '<tr>';
                        html += '<th class="w50">序号</th>';
                        html += '<th class="w100">返点</th>';
                        html += '<th class="w100">有效期</th>';
                        html += '<th class="w100">使用次数</th>';
                        html += '<th>链接地址</th>';
                        html += '<th class="w200">创建时间</th>';
                        html += '</tr>';
                        html += '</thead>';
                        html += '<tbody>';
                        if (d.table.length > 0) {
                            for (var i = 0; i < d.table.length; i++) {
                                var t = d.table[i];
                                html += '<tr>';
                                html += '<td>' + t.rowid + '</td>';
                                html += '<td>' + t.point + '</td>';
                                html += '<td>' + t.yxtime + '个月有效</td>';
                                html += '<td>' + t.times + '</td>';
                                html += '<td align="left">' + t.url + '</td>';
                                html += '<td>' + t.stime + '</td>';
                                html += '</tr>';
                            }
                        }
                        html += '</tbody>';
                        html += '</table>';
                        $("#ajaxList").html(html);
                        $("#ajaxPageBar").html(d.pagebar);
                    }
                    closeload(index);
                }
            });
        }

        function ajaxUserGroupList(currentpage) {
            if (currentpage != null)
                page = currentpage;
            var index = emLoading();
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: "/ajax/ajaxUser.aspx?oper=ajaxGetUserGroupList",
                error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                success: function (d) {
                    if (d.recordcount > 0) {
                        var html = '';
                        html += '<table class="query-table">';
                        html += '<tr><th colspan="5" align="left" style="color:Red;">类型配额</th></tr>';
                        html += '</table>';
                        html += '<table class="query-table">';
                        html += '<thead>';
                        html += '<tr>';
                        html += '<th>序号</th>';
                        html += '<th>类型名称</th>';
                        html += '<th>剩余配额</th>';
                        html += '<th>已注册</th>';
                        html += '<th>合计配额</th>';
                        html += '</tr>';
                        html += '</thead>';
                        html += '<tbody>';
                        if (d.table.length > 0) {
                            for (var i = 0; i < d.table.length; i++) {
                                var t = d.table[i];
                                html += '<tr>';
                                html += '<td>' + t.rowid + '</td>';
                                html += '<td>' + t.togroupname + '</td>';
                                html += '<td>' + (t.childnums - t.regnums) + '</td>';
                                html += '<td>' + t.regnums + '</td>';
                                html += '<td>' + t.childnums + '</td>';
                                html += '</tr>';
                            }
                        }
                        html += '</tbody>';
                        html += '</table>';
                        $("#groupInfo").html(html);
                    }
                    closeload(index);
                    ajaxUserPointList(1);
                }
            });
        }

        function ajaxUserPointList(currentpage) {
            if (currentpage != null)
                page = currentpage;
            $.ajax({
                type: "get",
                dataType: "json",
                data: "page=" + currentpage + "&pagesize=" + pagesize + "&clienttime=" + Math.random(),
                url: "/ajax/ajaxUser.aspx?oper=ajaxGetUserPointList",
                error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                success: function (d) {
                    if (d.recordcount > 0) {
                        var html = '';
                        html += '<table class="query-table">';
                        html += '<tr><th colspan="5" align="left" style="color:Red;">平级配额</th></tr>';
                        html += '</table>';
                        html += '<table class="query-table">';
                        html += '<thead>';
                        html += '<tr>';
                        html += '<th>序号</th>';
                        html += '<th>会员返点</th>';
                        html += '<th>剩余配额</th>';
                        html += '<th>已注册</th>';
                        html += '<th>合计配额</th>';
                        html += '</tr>';
                        html += '</thead>';
                        html += '<tbody>';
                        if (d.table.length > 0) {
                            for (var i = 0; i < d.table.length; i++) {
                                var t = d.table[i];
                                html += '<tr>';
                                html += '<td>' + t.rowid + '</td>';
                                html += '<td>' + t.point + '</td>';
                                html += '<td>' + (t.childnums - t.regnums) + '</td>';
                                html += '<td>' + t.regnums + '</td>';
                                html += '<td>' + t.childnums + '</td>';
                                html += '</tr>';
                            }
                        }
                        html += '</tbody>';
                        html += '</table>';
                        $("#pointInfo").html(html);
                    }
                }
            });
        }

        function ajaxRegStr() {
            var point = $("#txtPoint2").val();
            var yxtime = $("#yxtime").val();
            var times = $("#txtTimes").val();
            if (/^\d+(\.\d{1,1})?$/.test(point)) {
                if (parseFloat($("#txtUserPoint").val()) < 13.1) {
                    if (parseFloat(point) <= 0 || parseFloat(point) > parseFloat($("#txtUserPoint2").val())) {
                        emAlert("返点不能小于0且不能大于您的返点！");
                        return false;
                    }
                }
                else {
                    if (parseFloat(point) <= 10 || parseFloat(point) >= parseFloat($("#txtUserPoint2").val())) {
                        emAlert("返点不能小于0且不能大于等于您的返点！");
                        return false;
                    }
                }
                $.ajax({
                    type: "post",
                    dataType: "json",
                    data: "point=" + point + "&yxtime=" + yxtime + "&times=" + times,
                    url: "/ajax/ajaxUser.aspx?oper=ajaxRegStr&clienttime=" + Math.random(),
                    success: function (data) {
                        if (data.result == "1") {
                            emAlertSuccess(data.returnval);
                            $("#tp-account").hide();
                            $("#tp-linkreg").hide();
                            $("#tp-manage").show();
                            ajaxList(1);
                        }
                    }
                });
            }
            else {
                emAlert("返点只能为整数或者一位小数！");
                return;
            }
        }

        function ajaxRegStrAll() {
            $.ajax({
                type: "post",
                dataType: "json",
                data: "",
                url: "/ajax/ajaxUser.aspx?oper=ajaxRegStrAll&clienttime=" + Math.random(),
                success: function (data) {
                    if (data.result == "1") {
                        ajaxList();
                    }
                }
            });
        }


        function ajaxRegsiter() {
            if (site.RegIsOpen == "1") {
                emAlert("注册已关闭，请联系管理员！");
                return false;
            }
            if ($.formValidator.PageIsValid('1')) {
                var type = $("#ddlType").val();
                var uName = $("#txtUserName").val();
                var oPass = "a123456";
                var point = $("#txtPoint").val();
                if (/^\d+(\.\d{1,1})?$/.test(point)) {
                    if (parseFloat($("#txtUserPoint").val()) < 13.1) {
                        if (parseFloat(point) <= 0 || parseFloat(point) > parseFloat($("#txtUserPoint").val())) {
                            emAlert("返点不能小于0且不能大于您的返点！");
                            return false;
                        }
                    }
                    else {
                        if (parseFloat(point) <= 0 || parseFloat(point) >= parseFloat($("#txtUserPoint").val())) {
                            emAlert("返点不能小于0且不能大于等于您的返点！");
                            return false;
                        }
                    }
                    var index = emLoading();
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        url: "/ajax/ajaxUser.aspx?oper=ajaxRegiter&clienttime=" + Math.random(),
                        data: "type=" + type + "&name=" + encodeURIComponent(uName) + "&pwd=" + encodeURIComponent(oPass) + "&point=" + encodeURIComponent(point * 10),
                        error: function (XmlHttpRequest, textStatus, errorThrown) {  alert(XmlHttpRequest.responseText);},
                        success: function (d) {
                            switch (d[0].result) {
                                case '0':
                                    emAlert(d[0].message);
                                    break;
                                case '1':
                                    emAlertSuccess(d[0].message);
                                    $("#txtUserName").val("");
                                    $('#txtPoint').val("");
                                    ajaxUserGroupList(1);
                                    break;
                            }
                            closeload(index);
                        }
                    });
                } else {
                    emAlert("返点只能为整数或者一位小数！");
                    return;
                }
            }
        }
    </script>
</head>
<body>
    <div class="tto-tabs">
        <ul class="tabs-nav">
            <li class="first ui-state-active" nmb="0">普通开户</li>
            <li nmb="1">链接开户</li>
            <li class="last" nmb="2">链接管理</li>
        </ul>

        <div class="tabs-panel" id="tp-account">
            <form id="form" runat="server" class="tto-form mt30">
            <div class="input-group">
				<label class="lab">开户类别：</label>
                <asp:DropDownList ID="ddlType" runat="server">
                </asp:DropDownList>
            </div>
            <div class="input-group">
                <label class="lab">用户名：</label>
                <input id="txtUserName" type="text" value="" class="ipt" />
                <div class="form-tips">5-20位字母或数字，字母开头</div>
            </div>
            <div class="input-group">
                <label class="lab">彩票返点：</label>
                <input id="txtPoint" type="text" value="" class="ipt" />
                <div class="form-tips" runat="server"><asp:Label ID="lblPoint2" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="btn-group">
                <input type="button" value="添加账户" class="btn btn-primary" onclick="ajaxRegsiter()" />
                <input type="hidden" id="txtUserPoint" value="<%=Convert.ToDecimal(AdminPoint) / 10 %>" />
            </div>
            </form>
                        <div class="kindly-reminder mt10">
                <h4>
                    <i class="icon icon-warn"></i>温馨提示</h4>
                <p>
                    1. 自动注册的会员初始密码为“a123456”。</p>
                <p>
                    2. 为提高服务器效率，系统将自动清理注册一个月没有充值，或两个月未登录，并且金额低于10元的账户。</p>
            </div>
            <div id="groupInfo" class="query-result mt10">
            </div>
            <div id="pointInfo" class="query-result mt10">
            </div>
        </div>

        <div class="tabs-panel" id="tp-linkreg">
            <form class="tto-form mt30">
            <div class="input-group">
                <label class="lab">开户类别：</label>
                <select>
                <option value="代理">代理</option>
                <option value="会员">会员</option>
                </select>
            </div>
            <div class="input-group">
                <label class="lab">链接有限期：</label>
                 <select id="yxtime">
                <option value="1">1个月有效</option>
                <option value="6">6个月有效</option>
                <option value="12">12个月有效</option>
                </select>
            </div>
             <div class="input-group">
                <label class="lab">使用次数：</label>
                <input id="txtTimes" type="text" value="100" class="ipt" />
            </div>
            <div class="input-group">
                <label class="lab">彩票返点：</label>
                <input id="txtPoint2" type="text" value="" class="ipt" />
                <div class="form-tips" runat="server"><asp:Label ID="lblPoint3" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="btn-group">
                <input type="button" value="生成链接" class="btn btn-primary" onclick="ajaxRegStr()" />
                <input type="hidden" id="txtUserPoint2" value="<%=Convert.ToDecimal(AdminPoint) / 10 %>" />
            </div>
			<div class="input-group mt30">
                <label class="lab"></label>
                <div id="pointStr"></div>
            </div>
            </form>
        </div>
        
        <div class="tabs-panel" id="tp-manage">
            <div class="query-area">
                <div id="ajaxList" class="query-result mt30">
                </div>
            </div>
             <div id="ajaxPageBar" class="pagination">
        </div>
        </div>
    </div>
</body>
</html>
