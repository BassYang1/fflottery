<%@ Page Language="C#" CodeBehind="sysSetInfo.aspx.cs" Inherits="Lottery.Admin.sysSetInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>常规设置</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script src="/statics/admin/js/lotInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        var TableTemplate = GetPage(GetQueryString("page"));
        $(document).ready(function () {
            ajaxList();
        });

        function ajaxList() {
            top.Lottery.Loading.show("正在努力加载，请稍后...");
            //$("#main").html("");
            var s = "";
            for (var i = 0, len = TableTemplate.ListCount; i < len; i++) {
                s += '<div class="uc-box">';
                s += '<div class="box-hd">';
                s += '<h3 class="title">' + TableTemplate.ListName[i]["key"] + '</h3><a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a>';
                s += '<i class="icon-t"></i>';
                s += '</div>';
                s += '<div class="box-bd">';
                s += '<div class="uc-form">';
                var tList = "";
                if (i == 0)
                    tList = TableTemplate.List1;
                if (i == 1)
                    tList = TableTemplate.List2;
                if (i == 2)
                    tList = TableTemplate.List3;
                if (i == 3)
                    tList = TableTemplate.List4;
                if (i == 4)
                    tList = TableTemplate.List5;
                $.ajax({
                    type: "get",
                    dataType: "json",
                    async: false,
                    data: "",
                    url: "/admin/ajaxSysInfo.aspx?oper=ajaxGetList&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                    success: function (d) {
                        top.Lottery.Message(d.returnval, "1");
                        for (var j = 0, len = tList.length; j < len; j++) {
                            var tInput = tList[j];
                            if (tInput.InputType == "select") {
                                var value = d.table[0][tInput.InputId.toLowerCase()];
                                s += '<div class="form-group">';
                                s += '<label class="lab">' + tInput.InputTitle + ':</label> <select id=' + tInput.InputId + ' class=' + tInput.InputClass + ' style="width: ' + tInput.Width + ';"';
                                if (tInput.OnChange != null)
                                    s += ' onchange="' + tInput.OnChange + '()"';
                                s += '>';
                                for (var obj in tInput.Options) {
                                    if (value == obj)
                                        s += '<Option value="' + tInput.Options[obj]["key"] + '" selected>' + tInput.Options[obj]["value"] + '</Option>';
                                    else
                                        s += '<Option value="' + tInput.Options[obj]["key"] + '">' + tInput.Options[obj]["value"] + '</Option>';
                                }
                                s += '</select><div class="tips"><span id="tip' + tInput.InputId + '">' + tInput.InputTip + '</span></div>';
                                s += '</div>';
                            }
                            if (tInput.InputType == "DateTime") {
                                s += '<div class="form-group">';
                                s += '<label class="lab">' + tInput.InputTitle + ':</label> <input id="' + tInput.InputId + '" class="' + tInput.InputClass + '" type="text" style="width: ' + tInput.Width + ';" value="' + tInput.Value + '" onclick="WdatePicker({el:\'' + tInput.InputId + '\'})" />';
                                s += '</div>';
                            }
                            if (tInput.InputType == "input") {
                                var value = d.table[0][tInput.InputId.toLowerCase()];
                                s += '<div class="form-group">';
                                if (tInput.InputTitle != "")
                                    s += '<label class="lab">' + tInput.InputTitle + ':</label>';
                                s += '<input id="' + tInput.InputId + '" class="' + tInput.InputClass + '" type="text" style="width: ' + tInput.Width + ';" value="' + value + '"';
                                if (tInput.onkeyup != null)
                                    s += ' onkeyup="' + tInput.onkeyup + '"';
                                s += '/>';
                                s += '<div class="tips"><span id="tip' + tInput.InputId + '">' + tInput.InputTip + '</span></div></div>';
                            }
                        }

                        for (var j = 0, len = TableTemplate.Botton.length; j < len; j++) {
                            var tBotton = TableTemplate.Botton[j];
                            s += '<div class="btn-group">';
                            if (tBotton.Link != null) {
                                s += '<a href="' + tBotton.Link + '" class="' + tBotton.InputClass + '">' + tBotton.Title + '</a>';
                            }
                            else {
                                s += ' <button type="button" class="' + tBotton.InputClass + '" onclick="' + tBotton.Function + ';"><i class="icon icon-save"></i>' + tBotton.Title + '</button>';
                            }
                            s += '</div>';
                        }
                    }
                });
                s += '</div>';
                s += '</div>';
                s += '</div>';
            }
            $("#main").html(s);
        }

        function ajaxPost() {
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
                url: "/admin/ajaxSysInfo.aspx?oper=ajaxUpdate&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) {  },
                success: function (d) {
                    top.Lottery.Alert(d.returnval, "1");
                }
            });
        }

        /*最后的表单验证*/
        function CheckFormSubmit() {
            if ($.formValidator.PageIsValid('1')) {
                Lottery.Loading.show("正在处理，请等待...");
                return true;
            } else {
                return false;
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
    <form id="form1">
    <div id="main">
    </div>
    </form>
</body>
</html>
