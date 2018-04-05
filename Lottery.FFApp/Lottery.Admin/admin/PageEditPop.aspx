<%@ Page Language="C#" CodeBehind="PageEditPop.aspx.cs" Inherits="Lottery.Admin.PageEditPop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>编辑操作</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script src="/statics/admin/js/lotInput.js" type="text/javascript"></script>
    <script type="text/javascript" src="/_libs/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        var id = joinValue('id');
        var TableTemplate = GetPage(GetQueryString("page"));
        $(document).ready(function () {
            document.title = TableTemplate.Title;
            $.formValidator.initConfig({ onError: function (msg) { } });
            var s = '<div class="popup-body">';
            $.ajax({
                type: "get",
                dataType: "json",
                async: false,
                data: id,
                url: TableTemplate.InfoUrl + "&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    for (var j = 0, len = TableTemplate.List.length; j < len; j++) {
                        var tInput = TableTemplate.List[j];
                        if (tInput.InputType == "select") {
                            var value = d.table[0][tInput.InputId.toLowerCase()];
                            s += '<div class="form-group">';
                            s += '<label class="lab">' + tInput.InputTitle + ':</label> <select id=' + tInput.InputId + ' class=' + tInput.InputClass + ' style="width: ' + tInput.Width + ';"';
                            if (tInput.OnChange != null)
                                s += ' onchange="' + tInput.OnChange + '()"';
                            s += '>';
                            if (TableTemplate.OptUrl != null) {
                                $.ajax({
                                    type: "get",
                                    dataType: "json",
                                    async: false,
                                    data: "clienttime=" + Math.random(),
                                    url: TableTemplate.OptUrl,
                                    error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                                    success: function (d) {
                                        for (i = 0; i <= d.table.length - 1; i++) {
                                            if (value == d.table[i].id)
                                                s += '<Option value="' + d.table[i].id + '" selected>' + d.table[i].name + '</Option>';
                                            else
                                                s += '<Option value="' + d.table[i].id + '">' + d.table[i].name + '</Option>';
                                        }
                                    }
                                });
                            }
                            else {
                                for (var obj in tInput.Options) {
                                    if (value == tInput.Options[obj]["key"])
                                        s += '<Option value="' + tInput.Options[obj]["key"] + '" selected>' + tInput.Options[obj]["value"] + '</Option>';
                                    else
                                        s += '<Option value="' + tInput.Options[obj]["key"] + '">' + tInput.Options[obj]["value"] + '</Option>';
                                }
                            }
                            s += '</select><div class="tips"><span id="tip' + tInput.InputId + '">请选择' + tInput.InputTitle + '</span></div>';
                            s += '</div>';
                        }
                        if (tInput.InputType == "input") {
                            var value = d.table[0][tInput.InputId.toLowerCase()];
                            s += '<div class="form-group">';
                            if (tInput.InputTitle != "")
                                s += '<label class="lab">' + tInput.InputTitle + ':</label>';
                            s += '<input id="' + tInput.InputId + '" class="' + tInput.InputClass + '" type="text" style="width: ' + tInput.Width + ';" ';
                            if (tInput.ReadOnly != null)
                                s += ' readonly';
                            if (tInput.IsShow == null)
                                s += ' value="' + value + '"';
                            if (tInput.onkeyup != null) 
                                s += ' onkeyup="' + tInput.onkeyup + '"';
                            s += '/>';
                            s += '<div class="tips">';
                           if (tInput.ReadOnly == null)
                                s += '<span id="tip' + tInput.InputId + '">请输入' + tInput.InputTitle + '</span>';
                            s += '</div></div>';
                        }
                        if (tInput.InputType == "textarea") {
                            var value = d.table[0][tInput.InputId.toLowerCase()];
                            s += '<div class="form-group">';
                            if (tInput.InputTitle != "")
                                s += '<label class="lab">' + tInput.InputTitle + ':</label>';
                            s += '<textarea id="' + tInput.InputId + '" class="' + tInput.InputClass + '">' + value + '</textarea>';
                            s += '<div class="tips"><span id="tip' + tInput.InputId + '">请输入' + tInput.InputTitle + '</span></div></div>';
                        }
                        if (tInput.InputType == "DateTime") {
                            var value = d.table[0][tInput.InputId.toLowerCase()];
                            s += '<div class="form-group">';
                            s += '<label class="lab">' + tInput.InputTitle + ':</label>';
                            s += ' <input id="' + tInput.InputId + '" class="' + tInput.InputClass + '" type="text" style="width: ' + tInput.Width + ';" value="' + value + '" onclick="WdatePicker({el:\'' + tInput.InputId + '\'})" />';
                            s += '<div class="tips"><span id="tip' + tInput.InputId + '">请选择' + tInput.InputTitle + '</span></div></div>';
                        }
                    }
                }
            });

            for (var j = 0, len = TableTemplate.Botton.length; j < len; j++) {
                var tBotton = TableTemplate.Botton[j];
                s += '<div class="popup-actions"><span id="tipMsg" style="color:Red;"></span>';
                if (tBotton.Link != null) {
                    s += '<a href="' + tBotton.Link + '" class="' + tBotton.InputClass + '">' + tBotton.Title + '</a>';
                }
                else {
                    s += ' <button type="button" class="' + tBotton.InputClass + '" onclick="' + tBotton.Function + ';">' + tBotton.Title + '</button>';
                }
                s += '</div>';
            }
            s += '</div>';
            $("#main").html(s);
            for (var j = 0, len = TableTemplate.List.length; j < len; j++) {
                var tInput = TableTemplate.List[j];
                if (tInput.InputType == "select") {
                    if (tInput.Function != null)
                        tInput.Function;
                }
                if (tInput.InputType == "input") {
                    if (tInput.ReadOnly == null)
                    $("#" + tInput.InputId + "").formValidator({ tipid: "tip" + tInput.InputId, onshow: "请输入" + tInput.InputTitle, onfocus: "请输入" + tInput.InputTitle, defaultvalue: "" }).InputValidator({ min: 1, onerror: tInput.InputTitle + "不能为空" });
                }
                if (tInput.InputType == "DateTime") {
                    $("#" + tInput.InputId + "").formValidator({ tipid: "tip" + tInput.InputId, onshow: "请输入" + tInput.InputTitle, onfocus: "请输入" + tInput.InputTitle, defaultvalue: "" }).InputValidator({ min: 1, onerror: tInput.InputTitle + "不能为空" });
                }
            }
        });

        function ajaxPost() {
            if ($.formValidator.PageIsValid('1')) {
                var d = "";
                var selects = main.getElementsByTagName("select");
                for (var i = 0; i < selects.length; i++) {
                    d += "&" + selects[i].id + "=" + $('#' + selects[i].id).val();
                }
                var inputs = main.getElementsByTagName("input");
                for (var i = 0; i < inputs.length; i++) {
                    d += "&" + inputs[i].id + "=" + $('#' + inputs[i].id).val();
                }
                $.ajax({
                    type: "post",
                    dataType: "json",
                    data: d,
                    url: TableTemplate.UpdateUrl + "&clienttime=" + Math.random() + id,
                    error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                    success: function (d) {
                        try {
                            parent.ajaxList(parent.page);
                        }
                        catch (e) {
                            top.getCurrentIframe().ajaxList(top.getCurrentIframe().page);
                        }
                        top.Lottery.Alert(d.returnval, "1");
                    }
                });
            }
        }

        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        }
    </script>
</head>
<body>
    <form id="main" class="uc-form popup-form" onsubmit="return CheckFormSubmit()">
    </form>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
