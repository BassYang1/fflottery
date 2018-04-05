<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="betUpdate.aspx.cs" Inherits="Lottery.Admin.betUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>投注改单</title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="formtable">
        <tr>
            <th>
                帐号:
            </th>
            <td>
                &nbsp;<%=UserName %>
            </td>
            <th>
                彩票:
            </th>
            <td>
                &nbsp;<%=L_Lottery %>
            </td>
        </tr>
        <tr>
            <th>
                彩票玩法:
            </th>
            <td>
                &nbsp;<%=L_PlayType %>
            </td>
            <th>
                期号:
            </th>
            <td>
                &nbsp;<%=L_IssueNumber %>
            </td>
        </tr>
        <tr>
            <th>
                单注奖金:
            </th>
            <td>
                &nbsp;<%=L_SingleMoney%>
            </td>
            <th>
                投注注数:
            </th>
            <td>
                &nbsp;<%=L_Num %>
                &nbsp;倍数:&nbsp;<%=L_Times%>
            </td>
        </tr>
        <tr>
            <th>
                返点:
            </th>
            <td>
                &nbsp;<%=L_Point %>
            </td>
            <th>
                返点金额:
            </th>
            <td>
                &nbsp;<%=L_PointMoney %>
            </td>
        </tr>
        <tr>
            <th>
                投注金额:
            </th>
            <td>
                &nbsp;<%=L_Total %>
                元
            </td>
            <th>
                中奖金额:
            </th>
            <td>
                &nbsp;<%=L_Bonus %>
            </td>
        </tr>
        <tr>
            <th>
                彩票奖态:
            </th>
            <td>
                &nbsp;<%=L_State %>
            </td>
            <th>
                投注时间:
            </th>
            <td>
                &nbsp;<%=L_STime2%>
            </td>
        </tr>
        <tr id="NumberShow" runat="server">
            <th>
                开奖号码:
            </th>
            <td>
                &nbsp;<%=L_Number %>
            </td>
            <th>
                实际盈亏:
            </th>
            <td>
                &nbsp;<%=L_RealGet%>
            </td>
        </tr>
        <tr>
            <th>
                投注号码:
            </th>
            <td colspan="3" style="padding: 5px;">
                <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Width="635px" Height="210px"></asp:TextBox>
                <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td colspan="3">
                <asp:Button ID="btnSave" runat="server" Text="确认添加" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">        _jcms_SetDialogTitle();</script>
</body>
</html>
