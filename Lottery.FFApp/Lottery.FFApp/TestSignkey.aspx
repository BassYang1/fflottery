<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSignkey.aspx.cs" Inherits="Lottery.FFApp.TestSignkey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>验证登录注册签名</h1>
        <div>{商户id}&{用户名}&{商户Key}</div>
        <div>
            商户id<asp:TextBox ID="txtMerch" runat="server"></asp:TextBox></div>
        <div>用户名<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></div>
        <div>时间<asp:TextBox ID="txtTime" runat="server"></asp:TextBox></div>
        <div>商户Key<asp:TextBox ID="txtKey" runat="server"></asp:TextBox></div>
        <div><asp:Button ID="Button1" runat="server" Text="加密" OnClick="Button1_Click" /></div>
        <div><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></div>
    </div>
        
    <div>
        <h1>验证充值提现签名</h1>
        <div>签名字符串, 按顺序(充值/提现订单号&商户Id&会员用户名&充值金额(4位小数)&商户安全码)MD5加密串</div>
        <div>
            订单号<asp:TextBox ID="txtOrderNo2" runat="server"></asp:TextBox></div>
            商户id<asp:TextBox ID="txtMerch2" runat="server"></asp:TextBox></div>
        <div>用户名<asp:TextBox ID="txtUserName2" runat="server"></asp:TextBox></div>
        <div>充值金额<asp:TextBox ID="txtAmount2" runat="server"></asp:TextBox></div>
        <div>商户Key<asp:TextBox ID="txtKey2" runat="server"></asp:TextBox></div>
        <div><asp:Button ID="Button2" runat="server" Text="加密" OnClick="Button2_Click" /></div>
        <div><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></div>
        <div><asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></div>
    </div>
    </form>
</body>
</html>
