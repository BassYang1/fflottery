<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="trueNameEdit.aspx.cs" Inherits="Lottery.WebApp2.trueNameEdit" %>

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
        /*最后的表单验证*/
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
                <i class="icon icon-warn"></i>绑定玩家的开户姓名后，为保证资金的绝对安全，请提供安全密保来解除绑定。
            </div>
            <div class="input-group">
                <label class="lab">
                    会员账号：</label>
                <asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="300" CssClass="ipt"
                    Enabled="false"></asp:TextBox>
                <asp:TextBox ID="txtId" runat="server" CssClass="ipt" Visible="False" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="input-group">
                <label class="lab">
                    安全问题：</label><asp:TextBox ID="txtQuestion" runat="server" MaxLength="50" Width="300"
                        CssClass="ipt" Enabled="false"></asp:TextBox>
            </div>
            <div class="input-group">
                <label class="lab">
                    安全答案：</label>
                <asp:TextBox ID="txtAnswer" runat="server" MaxLength="50" Width="300" CssClass="ipt"></asp:TextBox>
            </div>
            <div class="input-group">
                <label class="lab">
                    真实姓名：</label><asp:TextBox ID="txtTrueName" runat="server" MaxLength="50" Width="300"
                        CssClass="ipt"></asp:TextBox>
            </div>
            <div class="btn-group">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:Button ID="btnSave" runat="server" Text="确认编辑" CssClass="btn btn-primary"
                    OnClick="btnSave_Click" />
            </div>
            </form>
        </div>
    </div>
</body>
</html>
