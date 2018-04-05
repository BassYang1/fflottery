<%@ Page Language="C#" AutoEventWireup="true" Inherits="Lottery.WebApp.contract.ContractfhOper"
    CodeBehind="ContractfhOper.aspx.cs" %>

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
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.js"></script>
    <script type="text/javascript" src="/statics/js/EM.tools.js"></script>
    <script type="text/javascript" src="/statics/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ajaxGetList();
        });

        function ajaxGetList() {
            $("#userid").val(<%=userId %>);
            $.ajax({
                type: "get",
                dataType: "json",
                data: "id=<%=userId %>&clienttime=" + Math.random(),
                url: "/ajax/ajaxContractFH.aspx?oper=ajaxContractfhOperInfo",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    var html = "";
                        html += '<table class="acting-table">';
                        html += '<colgroup>';
                        html += '<col class="w120" />';
                        html += '<col class="200px" />';
                        html += '<col class="w120" />';
                        html += '<col class="200px" />';
                        html += '</colgroup>';
                        html += '<tr>';
                        html += '<td class="type w120">本期开始：</td>';
                        html += '<td>' + d.starttime + '</td>';
                        html += '<td class="type w120">本期截止：</td>';
                        html += '<td>' + d.endtime + '</td>';
                        html += '</tr>';
                        html += '<tr>';
                        html += '<td class="type w120">累计销量：</td>';
                        html += '<td><em>' + d.bet + '</em></td>';
                        html += '<td class="type w120">盈亏情况：</td>';
                        html += '<td><em>' + d.loss + '</em></td>';
                        html += '</tr>';
                        html += '<tr>';
                        html += '<td class="type w120">您的分红比率：</td>';
                        html += '<td><em>' + d.per + '%</em></td>';
                        html += '<td class="type w120">分红金额：</td>';
                        html += '<td><em>' + d.money + '</em></td>';
                        html += '</tr>';
                        html += '</table>';
                        $("#list").html(html);
                        $("#d1").val(d.starttime);
                        $("#d2").val(d.endtime);
                        $("#bet").val(d.bet);
                        $("#loss").val(d.loss);
                        $("#per").val(d.per);
                        $("#money").val(d.money);
                }
            });
        }

        function PaifaContractFH() {
                var d1 = $("#d1").val();
                var d2 = $("#d2").val();
                var bet = $("#bet").val();
                var loss = $("#loss").val();
                var per = $("#per").val();
                var money = $("#money").val();
                var userid = $("#userid").val();
                var txtpwd = $("#txtPassword").val();
            if (txtpwd=="") {
                emAlert("请您输入资金密码！");
                return false;
            }
            if (parseFloat(money)==0) {
                emAlert("此会员不需要分红！");
                return false;
            }
            var index = emLoading();
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/ajax/ajaxContractFH.aspx?oper=PaifaContractFH&clienttime=" + Math.random(),
				data: "d1=" + d1+"&d2=" + d2+"&bet=" + bet+"&loss=" + loss+"&per=" + per+"&money=" + money+"&userid=" + userid+"&txtpwd=" + txtpwd,
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    switch (d.result) {
                        case '0':
                            emAlert(d.returnval);
                            break;
                        case '1':
                             emAlertSuccess(d.returnval);
                            break;
                    }
                    closeload(index);
                }
            });
        }
    </script>
</head>
<body>
<div class="block-content">
<div class="block-panel">                
<div class="query-area">
<%--<div class="query-form">
<form action="" method="post">
<div class="input-group">
<input id="txtPassword" type="password" value="" class="ipt" placeholder="资金密码"/>
<input id="btnAgreeCannel" type="button" value="确认派发" onclick="PaifaContractFH()" class="btn btn-query"/>
<input id="d1" type="hidden" value=""/>
<input id="d2" type="hidden" value=""/>
<input id="bet" type="hidden" value=""/>
<input id="loss" type="hidden" value=""/>
<input id="per" type="hidden" value=""/>
<input id="money" type="hidden" value=""/>
<input id="userid" type="hidden" value=""/>
</div>
</form>
</div>--%>
<div id="list" class="acting-dividends" style="width: 100%"></div>
</div>
</div>
</div>
</body>
</html>
