<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="betCancel.aspx.cs" Inherits="Lottery.Admin.betCancel" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>已开奖整期撤单</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onError: function (msg) { alert(msg); } });
            $("#txtIssue").formValidator({ tipid: "tipIssue", onshow: "请输入撤单期号", onfocus: "请正确输入撤单期号", defaultvalue: "" }).InputValidator({ min: 1, onerror: "撤单期号不能为空" });
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
                选择采种</th>
            <td>
                <asp:DropDownList ID="ddlLottery" runat="server" Width="200px" Height="35px" CssClass="ipt2">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                撤单期号
            </th>
            <td>
                <asp:TextBox ID="txtIssue" runat="server" Width="200px" MaxLength="50" CssClass="ipt"></asp:TextBox>
                <span id="tipIssue"></span>
            </td>
        </tr>
        <tr>
            <th>
                确认选项
            </th>
            <td>
                <div class="checkboxDiv">
                    <asp:RadioButton ID="rbo1" runat="server" GroupName="a1" CssClass="cp" />确认撤单&nbsp;&nbsp;
                    <asp:RadioButton ID="rbo2" runat="server" GroupName="a1" CssClass="cp" Checked="True" />还没想好</div>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                撤单后不可反投注，请您慎重！
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="确认撤单" CssClass="btnsubmit" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
