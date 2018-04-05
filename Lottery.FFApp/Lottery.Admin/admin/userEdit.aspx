<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userEdit.aspx.cs" Inherits="Lottery.Admin.useredit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>编辑会员</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script type="text/javascript">
        /*最后的表单验证*/
        function CheckFormSubmit() {
            Lottery.Loading.show("正在处理，请等待...");
            return true;
        }

        function ajaxLogin() {
            var id = $("#txtId").val();
            var name = $('#txtName').val();
            var cookiess = $("#lblCookie").val();
            var point = $("#ddlPoint").val();

            $.ajax({
                type: "post",
                dataType: "json",
                url: "/admin/ajax.aspx?oper=ajaxAdminToLoginWeb&clienttime=" + Math.random(),
                data: "id=" + encodeURIComponent(id) + "&name=" + encodeURIComponent(name) + "&cookieId=" + encodeURIComponent(cookiess) + "&point=" + encodeURIComponent(point),
                error: function (XmlHttpRequest, textStatus, errorThrown) { },
                success: function (d) {
                    if (d.result == "1") {
                        window.open('http://www.feifan1188.com/aspx/adminTologin.aspx?id=' + id + '&cookiess="' + cookiess + '"');
                    }
                }
            });
        }
  
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
    <table class="formtable">
        <tr>
            <th>
                会员编号
            </th>
            <td>
                <asp:TextBox ID="txtId" runat="server" MaxLength="20" CssClass="ipt" Enabled="False"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <th>
                会员类别
            </th>
            <td>
                <span id="Span1">
                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="sel">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtGroup" runat="server" MaxLength="20" CssClass="ipt" Enabled="False"
                        ReadOnly="True" Visible="false"></asp:TextBox>
                </span>
            </td>
        </tr>
        <tr>
            <th>
                会员账号
            </th>
            <td>
                <asp:TextBox ID="txtName" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
            <th>
                登陆密码
            </th>
            <td>
                <asp:TextBox ID="txtLoginPwd" runat="server" TextMode="Password" MaxLength="20" CssClass="ipt"></asp:TextBox>
                不修改请留空
            </td>
        </tr>
        <tr>
            <th>
                上级会员
            </th>
            <td>
                <span id="tipName">
                    <asp:TextBox ID="txtParent" runat="server" MaxLength="20" CssClass="ipt" Enabled="False"
                        ReadOnly="True"></asp:TextBox>
                </span>
            </td>
            <th>
                提现密码
            </th>
            <td>
                <span id="Span2"></span>
                <asp:TextBox ID="txtBankPwd" runat="server" TextMode="Password" MaxLength="20" CssClass="ipt"></asp:TextBox>
                不修改请留空
            </td>
        </tr>
        <tr>
            <th>
                会员关系代码
            </th>
            <td>
                <span id="tipName0">
                    <asp:TextBox ID="txtCode" runat="server" MaxLength="20" CssClass="ipt" Enabled="False"
                        ReadOnly="True"></asp:TextBox>
                </span>
            </td>
            <th>
                返点设置
            </th>
            <td>
                <span id="Span3"></span>
                <asp:DropDownList ID="ddlPoint" runat="server" CssClass="sel">
                </asp:DropDownList>
                <asp:TextBox ID="txtPoint" runat="server" MaxLength="20" CssClass="ipt" Enabled="False"
                    ReadOnly="True" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                是否在线
            </th>
            <td>
                <span id="Span7">
                    <asp:TextBox ID="txtOnline" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
                </span>
            </td>
            <th>
                可用余额</th>
            <td>
                <asp:TextBox ID="txtMoney" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                最后登录时间
            </th>
            <td>
                <span id="Span6">
                    <asp:TextBox ID="txtOntime" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
                </span>
            </td>
            <th>
                账号积分</th>
            <td>
                <asp:TextBox ID="txtScore" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                登录IP
            </th>
            <td>
                <asp:TextBox ID="txtIp" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
            <th>
                是否禁止登陆
            </th>
            <td>
                <asp:DropDownList ID="ddlIsEnable" runat="server" CssClass="sel">
                    <asp:ListItem Value="0">允许登陆</asp:ListItem>
                    <asp:ListItem Value="1">禁止登陆</asp:ListItem>
                    <asp:ListItem Value="2">卡掉线</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                注册时间
            </th>
            <td>
                <asp:TextBox ID="txtRegtime" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
            <th>
                是否禁止投注
            </th>
            <td>
                <asp:DropDownList ID="ddlIsBet" runat="server" CssClass="sel">
                    <asp:ListItem Value="0">允许投注</asp:ListItem>
                    <asp:ListItem Value="1">禁止投注</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                银行名称
            </th>
            <td>
                <asp:TextBox ID="txtPayBank" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
            <th>
                是否禁止取款</th>
            <td>
                <asp:DropDownList ID="ddlIsGetcash" runat="server" CssClass="sel">
                    <asp:ListItem Value="0">允许取款</asp:ListItem>
                    <asp:ListItem Value="1">禁止取款</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                开户地址
            </th>
            <td>
                <asp:TextBox ID="txtPayBankAddress" runat="server" MaxLength="20" CssClass="ipt"
                    Enabled="false"></asp:TextBox>
            </td>
            <th>
                是否禁止转账</th>
            <td>
                <asp:DropDownList ID="ddlIsTranAcc" runat="server" CssClass="sel">
                    <asp:ListItem Value="0">允许转账</asp:ListItem>
                    <asp:ListItem Value="1">禁止转账</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                银行卡号
            </th>
            <td>
                <asp:TextBox ID="txtPayAccount" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
            <th>
                安全问题
            </th>
            <td>
                <asp:TextBox ID="txtQuestion" runat="server" MaxLength="20" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                真实姓名
            </th>
            <td>
                <asp:TextBox ID="txtPayName" runat="server" MaxLength="20" CssClass="ipt"></asp:TextBox>
                 <asp:TextBox ID="tipPayName" runat="server" MaxLength="20" CssClass="ipt" Visible="false"></asp:TextBox>
            </td>
            <th>
                安全答案</th>
            <td>
                <asp:TextBox ID="txtAnswer" runat="server" MaxLength="20" CssClass="ipt"></asp:TextBox>
                <asp:TextBox ID="tipAnswer" runat="server" MaxLength="20" CssClass="ipt" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                邮箱地址
            </th>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
            <th>
                手机号码
            </th>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" MaxLength="200" CssClass="ipt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                备注信息
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtEnableSeason" runat="server" MaxLength="200" CssClass="ipt" 
                    Width="800px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="popup-actions">
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
        <asp:TextBox ID="lblCookie" runat="server" Width="0px"></asp:TextBox>
        <input type="button" onclick="ajaxLogin()" value="登录前台" class="btn btn-primary" />
        <asp:Button ID="btnSave" runat="server" Text="保存编辑" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    </form>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
