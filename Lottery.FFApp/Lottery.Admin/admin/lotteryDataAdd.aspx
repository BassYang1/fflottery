<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lotteryDataAdd.aspx.cs" Inherits="Lottery.AdminFile.Admin.lotteryDataAdd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>手动开奖</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script charset="utf-8" src="/_libs/kindeditor-4.1.10/kindeditor-min.js" type="text/javascript" />
    <link rel="stylesheet" href="/_libs/kindeditor-4.1.10/themes/default/default.css" />
    <script charset="utf-8" src="/_libs/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script>
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="txtContent"]', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                items: [
						'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
						'insertunorderedlist', '|', 'emoticons', 'link']
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onError: function (msg) { alert(msg); } });
            $("#txtTitle").formValidator({ tipid: "tipTitle", onshow: "请输入开奖期号", onfocus: "请输入开奖期号", defaultvalue: "" }).InputValidator({ min: 1, onerror: "开奖期号不能为空" });
            $("#txtTitle").formValidator({ tipid: "tipTitle", onshow: "请输入开奖号码", onfocus: "请输入开奖号码", defaultvalue: "" }).InputValidator({ min: 1, onerror: "开奖号码不能为空" });
            $("#txtTitle").formValidator({ tipid: "tipTitle", onshow: "请输入开奖时间", onfocus: "请输入开奖时间", defaultvalue: "" }).InputValidator({ min: 1, onerror: "开奖时间不能为空" });
        });
        /*最后的表单验证*/
        function CheckFormSubmit() {
            if ($.formValidator.PageIsValid('1')) {
                Lottery.Loading.show("正在处理，请等待...");
                return true;
            } else {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
    <table class="formtable">
        <tr>
            <th>
                选择彩种:
            </th>
            <td>
               <asp:DropDownList ID="ddlType" runat="server" Height="35px" Width="200"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                开奖期号:
            </th>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="200" CssClass="ipt"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <th>
                开奖号码:
            </th>
            <td>
                <asp:TextBox ID="txtNumber" runat="server" Width="200" CssClass="ipt"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <th>
                号码说明:
            </th>
            <td>
                韩国，东京，菲律宾等多开奖号码，请输入完整号码，系统名自动计算！
            </td>
        </tr>
         <tr>
            <th>
                开奖时间:
            </th>
            <td>
                <asp:TextBox ID="txtOpenTime" runat="server" Width="200" CssClass="ipt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <input id="btnReset" type="button" value="取消" class="btn btn-primary" onclick="parent.Lottery.Popup.hide();" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
