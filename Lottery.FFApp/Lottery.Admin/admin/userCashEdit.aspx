<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userCashEdit.aspx.cs" Inherits="Lottery.Admin.usercashedit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>提现审核</title>
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
                提现银行：</label>
            <asp:Label ID="lblBank" runat="server" CssClass="ipt"></asp:Label>
            <asp:Label ID="lblBankCode" runat="server" Text="" Visible="false"></asp:Label>
            <div class="tips">
                <a href="<%=url %>" target="_blank" style="color: Red;">进入银行</a></div>
        </div>
        <div class="form-group">
            <label class="lab">
                会员账号：</label>
            <asp:Label ID="lblUserName" runat="server" CssClass="ipt"></asp:Label>
        </div>
        <div class="form-group">
            <label class="lab">
                提现户名：</label>
            <asp:Label ID="txtPayName" runat="server" CssClass="ipt"></asp:Label>
            <div class="tips">
                <input id="a1" type="button" value="复制" />
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                提现卡号：</label>
            <asp:Label ID="txtPayAccount" runat="server" CssClass="ipt"></asp:Label>
            <div class="tips">
                <input id="a2" type="button" value="复制" />
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                提现金额：</label>
            <asp:Label ID="txtMoney" runat="server" CssClass="ipt"></asp:Label>
            <div class="tips">
                <input id="Button2" type="button" value="复制" />
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                审核选项：</label>
            <div class="tips">
                <asp:RadioButton ID="rbo2" runat="server" GroupName="a1" Checked="True" />手动提现
                <asp:RadioButton ID="rbo3" runat="server" GroupName="a1" />拒绝提现
                <asp:RadioButton ID="rbo1" runat="server" GroupName="a1" />秒卡通支付
               <%-- <asp:RadioButton ID="rbo4" runat="server" GroupName="a1" />久付微信
                <asp:RadioButton ID="rbo5" runat="server" GroupName="a1" />久付网银
                <asp:RadioButton ID="rbo12" runat="server" GroupName="a1" />国盛通微信
                <asp:RadioButton ID="rbo13" runat="server" GroupName="a1" />国盛通网银
                <asp:RadioButton ID="rbo14" runat="server" GroupName="a1" />国盛通支付宝--%>
            </div>
        </div>
        <div class="form-group">
            <label class="lab">
                审核说明：</label>
            <asp:TextBox ID="txtSeason" runat="server" Width="300px" CssClass="ipt" Height="60px"
                TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="popup-actions">
            <asp:TextBox ID="txtId" runat="server" Width="120px" MaxLength="10" CssClass="ipt"
                Visible="false"></asp:TextBox>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <span id="tipMsg" style="color: Red;"></span>
            <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <input type="hidden" runat="server" id="orderNo" name="orderNo" />
            <input type="hidden" runat="server" id="tradeDate" name="tradeDate" />
            <input type="hidden" runat="server" id="Amt" name="Amt" />
            <input type="hidden" runat="server" id="bankAccName" name="bankAccName" />
            <input type="hidden" runat="server" id="bankName" name="bankName" />
            <input type="hidden" runat="server" id="bankCode" name="bankCode" />
            <input type="hidden" runat="server" id="bankAccNo" name="bankAccNo" />
        </div>
        </form>
    </div>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#a1").zclip({
                path: "/_libs/Jquery.zclip/ZeroClipboard.swf",
                copy: function () {
                    return $("#txtPayName").text();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                    alert("复制成功，请用Ctrl+V粘贴到所需要的地方!");
                }
            });
            $("#a2").zclip({
                path: "/_libs/Jquery.zclip/ZeroClipboard.swf",
                copy: function () {
                    return $("#txtPayAccount").text();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                    alert("复制成功，请用Ctrl+V粘贴到所需要的地方!");
                }
            });
            $("#a3").zclip({
                path: "/_libs/Jquery.zclip/ZeroClipboard.swf",
                copy: function () {
                    return $("#txtMoney").text();
                },
                afterCopy: function () {/* 复制成功后的操作 */
                    alert("复制成功，请用Ctrl+V粘贴到所需要的地方!");
                }
            });
        });
    </script>
</body>
</html>
