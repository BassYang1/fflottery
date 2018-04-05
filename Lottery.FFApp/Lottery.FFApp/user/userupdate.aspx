<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userupdate.aspx.cs" Inherits="Lottery.WebApp.user.userupdate" %>

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
    <script src="/statics/global.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckFormSubmit() {
            Lottery.Loading.show("正在处理，请等待...");
            return true;
        }
    </script>
</head>
<body>
    <div class="tto-popup">
        <div class="popup-body">
            <form id="form1" runat="server" onsubmit="return CheckFormSubmit()" class="tto-form popup-form">
            <div class="input-tips">
                <i class="icon icon-warn"></i>请选择返点，进行修改，只能升点，不能降点。
             
                <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300"></asp:Label>
            </div>
            <div class="input-group">
                <label class="lab">
                    会员账号：</label>
                <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" CssClass="ipt" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="txtUserId" runat="server" MaxLength="20" ReadOnly="True" Visible="False"></asp:TextBox>
            </div>
            <div class="input-group">
                <label class="lab">
                    返点设置：</label>
                <asp:DropDownList ID="ddlPoint" runat="server">
                </asp:DropDownList>
            </div>
            <div class="btn-group">
                <asp:Button ID="Button1" runat="server" Text="确认" CssClass="btn btn-bg btn-primary"
                    OnClick="btnSave_Click" />
            </div>
            </form>
        </div>
    </div>
</body>
</html>
