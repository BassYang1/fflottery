<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regMerchant.aspx.cs" Inherits="Lottery.Admin.admin.regMerchant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>注册商户</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/_libs/Jquery.zclip/jquery.js"></script>
    <script type="text/javascript" src="/_libs/Jquery.zclip/jquery.zclip.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        function CheckFormSubmit() {
            Lottery.Loading.show("正在处理，请等待...");
            return true;
        }
    </script>
</head>
<body>
    <div class="popup-body">
        <form id="form1" runat="server" class="uc-form popup-form" onsubmit="return CheckFormSubmit()">
        <div class="form-group">
            <label class="lab">
                商户号：</label>
            <asp:TextBox ID="merchantId" runat="server" Width="120px" MaxLength="10" CssClass="ipt"
                Visible="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label class="lab">
                商户名称：</label>
            <asp:TextBox ID="merchantName" runat="server" Width="120px" MaxLength="10" CssClass="ipt"
                Visible="false"></asp:TextBox>
        </div>
        <div class="popup-actions">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <span id="tipMsg" style="color: Red;"></span>
            <asp:Button ID="btnSave" runat="server" Text="注册商户" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
