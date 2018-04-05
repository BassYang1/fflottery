<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractfhPop.aspx.cs"
    Inherits="Lottery.WebApp.contract.ContractfhPop" %>

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
    <script src="/statics/js/page.contract.js" type="text/javascript"></script>
    <script type="text/javascript">
        //下级会员信息
        var subUser = "<%=this.SubUserId%>"; //签约下级Id
        var userName = "<%=this.UserName%>"; //签约下级会员名称
        var userGroup = "<%=this.UserGroup%>"; //签约下级用户级别
        var userGroupName = "<%=this.UserGroupName%>"; //签约下级用户级别名称
        var userDesc = "会员[" + userName + "]";
        userDesc = userGroupName == "会员" ? userDesc : "[" + userGroupName + "]" + userDesc;

        //契约
        var count = 0; //签订契约个数，最大10个
        var contracts = null; //签订的契约
        var state = 0;　//契约状态，默认[0-未使用]

        $(function () {
            $("#btnAdd").hide();
            $("#btnSave").hide();
            $("#btnCannel").hide();

            if (checkUser()) {
                getContract();
                showContract();
                checkContractState();

                if (count == 0) {
                    addContract(); //增加一条契约
                }
            }
            else {
                $i("info").innerHTML = "暂时不能与下级签订契约，请联系上级";
            }
        });

        //检查是否有权限分配契约
        function checkUser() {
            var can = false;

            $.ajax({
                type: "get",
                dataType: "json",
                async: false,
                data: "clienttime=" + Math.random(),
                url: "/ajax/ajaxContractFH.aspx?oper=CanContract",
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                success: function (d) {
                    can = d.result == "1";
                }
            });

            return can;
        }

        //获取契约数据
        function getContract() {
            $.ajax({
                type: "get",
                dataType: "json",
                async: false,
                data: "id=" + subUser + "&clienttime=" + Math.random(),
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

                for (var i = 0; i < count; i++) {
                    var t = table[i];
                    var num = i + 1;

                    html += "<div class='input-group' id='contract_" + num + "'>";
                    html += "<input type='hidden' id='id_" + num + "' value='" + t.id + "' />";
                    html += "<input type='hidden' id='state_" + num + "' value='" + t.isused + "' />";
                    html += "<label class='lab'>0" + num + ".周期销量</label>";
                    html += "<input class='ipt' id='money_" + num + "' value='" + t.minmoney + "' onkeypress='doubleNum(this)' onkeydown='doubleNum(this)' />";
                    html += "<label class='lab'>万，周期分红</label>";
                    html += "<input class='ipt' id='per_" + num + "' value='" + t.money + "' onkeypress='doubleNum(this)' onkeydown='doubleNum(this)' />";
                    html += "<label class='lab'>%</label>";
                    html += "<img class='img' id='img_" + num + "' src='/statics/img/icon_lot_del.png' onclick='removeContract()' />";
                    html += "</div>";
                }

                $("#add").html(html);

                $("#add").find("img").hide();
                if (count > 1) {
                    $("#add").find("img:last").show();
                }
            }
        }

        //检查契约状态
        function checkContractState() {
            if (count > 0) { //已签订契约                  
                if (state == 0) {
                    $i("info").innerHTML = "契约待接受";
                    $("#btnAdd").show();
                    $("#btnSave").show();
                }

                if (state == 1) {
                    $i("info").innerHTML = "契约已签订";
                    $("#btnCannel").show();
                    $("#add").find("input").prop("readonly", true);
                    $("#add").find("img").hide();

                    if (count < 10) { //最多增加10条
                        $("#btnAdd").show();
                        $("#btnSave").show();
                    }
                }

                if (state == 2) {
                    $i("info").innerHTML = "契约已拒绝，可重新分配";
                    $("#btnAdd").show();
                    $("#btnSave").show();
                }

                if (state == 3) {
                    $i("info").innerHTML = "契约撤销，等待会员同意！";
                    $("#add").find("input").prop("readonly", true);
                    $("#add").find("img").hide();
                }

                if (state == 4) {
                    $i("info").innerHTML = "会员同意撤销，请您修改契约！";
                    $("#btnAdd").show();
                    $("#btnSave").show();
                }
            }
            else { //未签订契约
                $i("info").innerHTML = "正在给" + userDesc + "分配契约";
                $("#btnAdd").show();
                $("#btnSave").show();
            }
        }

        //增加一条契约
        function addContract() {
            if (count < 10) {
                count++; //增加一条

                //使用createElement创建元素
                var html = "";
                html += "<div class='input-group' id='contract_" + count + "'>";
                html += "<input type='hidden' id='id_" + count + "' value='0' />";
                html += "<input type='hidden' id='state_" + count + "' value='0' />";
                html += "<label class='lab'>0" + count + ".周期销量</label>";
                html += "<input class='ipt' id='money_" + count + "' value='' onkeypress='doubleNum(this)' onkeydown='doubleNum(this)' />";
                html += "<label class='lab'>万，周期分红</label>";
                html += "<input class='ipt' id='per_" + count + "' value='' onkeypress='doubleNum(this)' onkeydown='doubleNum(this)' />";
                html += "<label class='lab'>%</label>";
                html += "<img class='img' id='img_" + count + "' src='/statics/img/icon_lot_del.png' onclick='removeContract()' />";
                html += "</div>";

                $("#add").append(html);

                $("#add").find("img").hide();
                if (count > 1) {
                    $("#add").find("img:last").show();
                }
            }
            else {
                emAlert("最多增加10条契约!");
            }
        }

        //删除Id
        function removeContract() {
            $("#contract_" + count).remove();

            //第一条和已经生效的契约不允许删除
            if (--count != 1 && $("#state_" + count) != "1") {
                $("#add").find("img:last").show();
            }
        }

        var saveData = []; //需要存储的契约数据
        function saveContract() {
            saveData.splice(0, saveData.length); //清空数据

            for (var i = 0; i < count; i++) {
                var money = $("#money_" + (i + 1)).val();
                var per = $("#per_" + (i + 1)).val();
                var id = $("#id_" + (i + 1)).val();

                if (money == undefined || per == undefined) {
                    emAlert("输入的数值无效!");
                    return;
                }

                try {
                    money = parseFloat(money);
                    per = parseFloat(per);

                    if (isNaN(money) || isNaN(per) || money <= 0 || per <= 0) {
                        emAlert("输入的数值无效!");
                        return;
                    }
                }
                catch (e) {
                    emAlert("输入的数值无效!");
                    return;
                }

                if (per > 50) {
                    emAlert("输入的比例不能超过50%!");
                    return;
                }

                var contract = {
                    "userid": subUser,
                    "id": id,
                    "money": money,
                    "per": per
                };

                saveData.push(contract);
            }

            var jsonData = JSON.stringify(saveData);
            $.ajax({
                type: "post",
                dataType: "json",
                data: jsonData,
                async: false,
                url: "/ajax/ajaxContractFH.aspx?oper=ajaxSaveContract&clienttime=" + Math.random(),
                error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                success: function (d) {
                    switch (d.result) {
                        case '0':
                            emAlert(d.returnval);
                            break;
                        case '1':
                            emAlert("契约分配成功");
                            break;
                    }
                }
            });
        }

        //关闭弹窗
        function closePop() {
            var index = layer.getFrameIndex(window.name);
            parent.layer.close(parent.layer.getFrameIndex(window.name));
        }
    </script>
</head>
<body>
    <div class="tto-popup">
        <div class="popup-body">
            <form id="form1" class="tto-form2">
                <div class="input-group">
                    <span id="info" class="info"></span>
                </div>
                <div id="add" style="height: 350px;">
                </div>
                <div class="btn-group">
                    <input id="btnAdd" type="button" value="添加规则" onclick="addContract()" class="btn btn-bg btn-primary" />
                    <input id="btnCannel" type="button" value="撤销契约" onclick="ajaxUpdate(3)" class="btn btn-bg btn-primary" />
                    <input id="btnSave" type="button" value="提交" onclick="saveContract()" class="btn btn-bg btn-primary" />
                    <input type="button" value="取消" onclick="closePop()" class="btn btn-bg btn-default" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
