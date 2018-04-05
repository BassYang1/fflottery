<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="Lottery.Admin.SendMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>发送即时信息</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.formValidator.initConfig({ onError: function (msg) { alert(msg); } });
            $("#txtMessage").formValidator({ tipid: "tipMessage", onshow: "请输入即时信息内容", onfocus: "请正确输入即时信息内容", defaultvalue: "" }).InputValidator({ min: 6, onerror: "即时信息内容不能为空" });
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
                发送选项：</label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="sel">
                <asp:ListItem Value="1">全部在线会员</asp:ListItem>
                <asp:ListItem Value="2">输入会员账号</asp:ListItem>
            </asp:DropDownList>
        </div>
         <div class="form-group">
            <label class="lab">
                会员账号：</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="ipt">
                </asp:TextBox><div class="tips">选择指定会员的话，请输入会员账号</div>
        </div>
        <div class="form-group">
            <label class="lab">
                信息内容：</label>
            <asp:TextBox ID="txtMessage" runat="server" Width="600px"
                    CssClass="ipt" TextMode="MultiLine" Height="230px"></asp:TextBox>
                                    <div class="tips"><span id="tipMessage" style="display:none;"></span></div>
        </div>
        <div class="popup-actions">
        <asp:Button ID="btnSave" runat="server" Text="确认发送" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        </form>
    </div>
    </body>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</html>
