<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activeNewsEdit.aspx.cs"
    Inherits="Lottery.Admin.activeNewsEdit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>编辑活动公告</title>
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
            editor = K.create('textarea[name="txtRemark"]', {
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
            $("#txtContent").formValidator({ tipid: "tipContent", onshow: "请输入内容", onfocus: "请输入内容", defaultvalue: "" }).InputValidator({ min: 1, onerror: "内容不能为空" });
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
    <div class="popup-body">
        <form id="form1" runat="server" class="uc-form popup-form" onsubmit="return CheckFormSubmit()">
        <div class="form-group">
            <label class="lab">
                所属活动：</label>
            <asp:Label ID="lblName" runat="server" CssClass="ipt"></asp:Label>
        </div>
        <div class="form-group">
            <label class="lab">
                活动标题：</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="ipt"></asp:TextBox>
            <div class="tips">
                <span id="tipTitle"></span>
            </div>
            <asp:TextBox ID="txtId" runat="server" Width="120px" MaxLength="10" CssClass="ipt"
                Visible="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="lab">
                活动内容：</label>
            <asp:TextBox ID="txtContent" runat="server" Width="450px" CssClass="ipt"></asp:TextBox>
            <div class="tips">
                    <span id="tipContent"></span>
                </div>
        </div>
        <div class="form-group">
            <label class="lab">
                活动规则：</label>
            <textarea cols="50" id="txtRemark" style="width: 450px; height: 200px;" runat="server"
                readonly="readonly"></textarea>
        </div>
        <div class="popup-actions">
            <asp:Button ID="Button1" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
