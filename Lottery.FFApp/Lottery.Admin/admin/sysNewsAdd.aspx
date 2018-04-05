<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sysNewsAdd.aspx.cs" Inherits="Lottery.AdminFile.Admin.sysNewsadd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>添加公告</title>
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
            $("#txtTitle").formValidator({ tipid: "tipTitle", onshow: "请输入标题", onfocus: "请输入标题", defaultvalue: "" }).InputValidator({ min: 1, onerror: "标题不能为空" });
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
                标题:
            </th>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="350px" CssClass="ipt"></asp:TextBox>
                <span id="tipTitle"></span>
                <asp:TextBox ID="txtId" runat="server" Width="120px" MaxLength="10" CssClass="ipt"
                    Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                内容:
            </th>
            <td style="height: 200px;">
                <textarea cols="60" id="txtContent" style="width: 600px; height: 300px;" runat="server"
                    readonly="readonly"></textarea>
            </td>
        </tr>
        <tr>
            <th>
                标题颜色:
            </th>
            <td>
                <asp:DropDownList ID="ddlColor" runat="server" Height="35px" Width="120px">
                    <asp:ListItem Value="#6B8E23">olivedrab</asp:ListItem>
                    <asp:ListItem Value="#FFFF00">yellow</asp:ListItem>
                    <asp:ListItem Value="#808080">gray</asp:ListItem>
                    <asp:ListItem Value="#008000">green</asp:ListItem>
                    <asp:ListItem Value="#FFC0CB">pink</asp:ListItem>
                    <asp:ListItem Value="#4682B4">steelblue</asp:ListItem>
                    <asp:ListItem Value="#800080">purple</asp:ListItem>
                    <asp:ListItem Value="#A52A2A">brown</asp:ListItem>
                    <asp:ListItem Value="#800000">maroon</asp:ListItem>
                    <asp:ListItem Value="#FFA500">orange</asp:ListItem>
                    <asp:ListItem Value="#FFFFFF">white</asp:ListItem>
                    <asp:ListItem Value="#800000">maroon</asp:ListItem>
                </asp:DropDownList>
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
