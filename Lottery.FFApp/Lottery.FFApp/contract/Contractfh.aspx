<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contractfh.aspx.cs"
    Inherits="Lottery.WebApp.contract.Contractfh" %>

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
        //会员信息
        var userId = "<%=this.UserId%>"; //会员Id
        var userName = "<%=this.UserName%>"; //会员名称
        var userGroup = "<%=this.UserGroup%>"; //会员级别
        var userGroupName = "<%=this.UserGroupName%>"; //会员级别名称
        var isAdminUser = "<%=this.IsAdminUser %>"; //是否是管量员账户
        var userDesc = "会员[" + userName + "]";
        userDesc = userGroupName == "会员" ? userDesc : "[" + userGroupName + "]" + userDesc;

        //契约
        var count = 0; //签订契约个数，最大10个
        var contracts = null; //签订的契约
        var state = 0;　//契约状态，默认[0-未使用]

        $(document).ready(function () {
            $("#btnAgree").hide();
            $("#btnRefuse").hide();
            $("#btnRefuseCannel").hide();
            $("#btnAgreeCannel").hide();

            if (isAdminUser != "True") {
                getContract();
                showContract();
                checkContractState();
            }
            else {
                $i("info").innerHTML = "管理账户无需签订契约";
            }
        });

        //获取契约数据
        function getContract() {
            $.ajax({
                type: "get",
                dataType: "json",
                async: false,
                data: "id=" + userId + "&clienttime=" + Math.random(),
                url: "/ajax/ajaxContractFH.aspx?oper=GetContractInfo2",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    contracts = d; //契约

                    if (contracts && contracts.table && contracts.table.length > 0) { //签订契约状态
                        //0-契约待接受或未签定契约
                        //1-契约已签订
                        //2-契约已拒绝，可重新分配
                        //3-契约撤销，等待会员同意！
                        //4-会员同意撤销，请您修改契约！
                        state = contracts.table[0].isused;

                        //已有契约数量
                        count = contracts.table.length;
                    }
                }
            });
        }

        //显示已有契约
        function showContract() {
            if (count > 0) { //已签订契约
                var table = contracts.table;
                var html = "";

                html += '<table class="query-table">';
                html += ' <colgroup>';
                html += '<col class="w50"/>';
                html += '<col class="w150"/>';
                html += '<col class="w150"/>';
                html += '<col class="w500"/>';
                html += '</colgroup>';

                html += '<thead><tr>';
                html += '<th>编号</th>';
                html += '<th>契约条件</th>';
                html += '<th>契约比例</th>';
                html += '<th>&nbsp;</th>';
                html += '</tr></thead>';
                html += '<tbody>';

                for (var i = 0; i < count; i++) {
                    var t = table[i];
                    html += '<tr>';
                    html += '<td>' + (i + 1) + '</td><td><label class="lab">半月周期销量</label><label class="lab">' + t.minmoney + '万</label></td><td><label class="lab">' + t.money + '%</label></td><td>&nbsp;</td>';
                    html += '</tr>';
                }

                html += '</tbody>';
                html += '</table>';

                $("#add").html(html);
            }
        }

        //检查契约状态
        function checkContractState() {
            if (count > 0) { //已签订契约                  
                if (state == 0) {
                    $i("info").innerHTML = "契约待接受，请您确认";
                    $("#btnAgree").show();
                    $("#btnRefuse").show();
                }

                if (state == 1) {
                    $i("info").innerHTML = "契约已签订";
                }

                if (state == 2) {
                    $i("info").innerHTML = "契约已拒绝，请联系上级重新分配";
                }

                if (state == 3) {
                    $i("info").innerHTML = "上级要求撤销契约，请您确认";
                    $("#btnRefuseCannel").show();
                    $("#btnAgreeCannel").show();
                }

                if (state == 4) {
                    $i("info").innerHTML = "您已同意撤销契约，请等待上级重新分配";
                }
            }
            else { //未签订契约
                $i("info").innerHTML = "契约未分配，请联系上级！";
            }
        }

        //更新契约状态
        //0-契约待接受或未签定契约
        //1-契约已签订
        //2-契约已拒绝，可重新分配
        //3-契约撤销，等待会员同意！
        //4-会员同意撤销，请您修改契约！
        function updateContract(state) {
            var index = emLoading();
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/ajax/ajaxContractFH.aspx?oper=UpdateContractState&clienttime=" + Math.random(),
                data: "state=" + state,
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    switch (d.result) {
                        case '0':
                            emAlert(d.message);
                            break;
                        case '1':
                            getContract();
                            showContract();
                            checkContractState();
                            $("#btnAgree").hide();
                            $("#btnRefuse").hide();
                            $("#btnRefuseCannel").hide();
                            $("#btnAgreeCannel").hide();
                            break;
                    }
                    closeload(index);
                }
            });
        }
    </script>
</head>
<body>
 <div class="query-area">
        <div class="query-form">
            <form id="ajaxInput" action="" method="post">
            <div class="query-date"> <span id="info" class="info"></span></div>
			<div class="btn-group" style="float:right; margin-right:-10px; ">
                <input id="btnAgree" type="button" value="同意契约" onclick="updateContract(1)" class="btn btn-bg btn-primary" />
			    <input id="btnRefuse" type="button" value="拒绝契约" onclick="updateContract(2)" class="btn btn-bg btn-primary" />
                <input id="btnRefuseCannel" type="button" value="拒绝撤销" onclick="updateContract(1)" class="btn btn-bg btn-primary" />
			    <input id="btnAgreeCannel" type="button" value="同意撤销" onclick="updateContract(4)" class="btn btn-bg btn-primary" />
            </div>
            </form>
        </div>
        <div id="add" class="query-result" style="width: 100%"></div>
    </div>
</body>
</html>