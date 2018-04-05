
function ConfirmAllDel() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要删除的会员", "0");
        return;
    }
    top.top.Lottery.Confirm("确定删除选中会员及其相关的信息吗?", "getCurrentIframe().ajaxAllDel()");
}

function ajaxAllDel() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要删除的会员", "0");
        return;
    }
    top.Lottery.Loading.show("正在删除，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxUser.aspx?oper=ajaxAllDel&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Message(d.returnval, "1");
            ajaxList(1);
        }
    });
}

function ajaxSearchIp() {
    itemid += "&ip=1";
    ajaxList(1);
}

function ajaxOnline(id) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: "id=" + id,
        url: "/admin/ajaxUser.aspx?oper=ajaxOnline&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            if (d.result == "1") {
                ajaxList(page);
            }
        }
    });
}

function ajaxAllOnline() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要下线的会员", "0");
        return;
    }
    top.Lottery.Loading.show("正在下线，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxUser.aspx?oper=ajaxAllOnline&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Message(d.returnval, "1");
            ajaxList(page);
        }
    });
}

//彻底删除
function ConfirmAllDel2() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要删除的会员", "0");
        return;
    }
    top.top.Lottery.Confirm("确定删除选中会员及其相关的信息吗?", "getCurrentIframe().ajaxAllDel2()");
}
function ajaxAllDel2() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要删除的会员", "0");
        return;
    }
    top.Lottery.Loading.show("正在删除，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxUser.aspx?oper=ajaxAllDel2&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Message(d.returnval, "1");
            ajaxList(1);
        }
    });
}
function ConfirmDel2(id) {
    top.top.Lottery.Confirm("确定彻底删除该会员及其相关的信息吗?", "getCurrentIframe().ajaxDel2(" + id + ")");
}
function ajaxDel2(id) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: "id=" + id,
        url: "/admin/ajaxUser.aspx?oper=ajaxDel2&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            switch (d.result) {
                case '-1':
                    top.Lottery.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                    break;
                case '0':
                    top.Lottery.Alert(d.returnval, "0");
                    break;
                case '1':
                    ajaxList(page);
                    break;
            }
        }
    });
}

//恢复会员
function ConfirmHF(id) {
    top.top.Lottery.Confirm("确定恢复该会员吗?", "getCurrentIframe().ajaxHF(" + id + ")");
}
function ajaxHF(id) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: "id=" + id,
        url: "/admin/ajaxUser.aspx?oper=ajaxDelStates&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            if (d.result == "1") {
                ajaxList(page);
            }
        }
    });
}

//终止追号
function operater() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要操作的内容", "0");
        return;
    }
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxBet.aspx?oper=ajaxOper&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            ajaxList(1);
        }
    });
}

function selectlottery() {
    var id = $("#lid").find("option:selected").attr("value");
    var ltype = "";
    if (id != "")
        ltype = id.substr(0, 1);
    if (ltype != 0) {
        for (i = 0; i <= PlayData.table.length - 1; i++) {
            if (PlayData.table[i].ltype == ltype) {
                $('#pid').empty();
                $('#pid').append('<option value="" ltype="0" selected>所有玩法</option>');
                for (j = 0; j <= PlayData.table[i].table2.length - 1; j++) {
                    var id = PlayData.table[i].table2[j].id;
                    var title = PlayData.table[i].table2[j].title;
                    if (j == 0) {
                        $('#pid').append('<option value="' + id + '">' + title + '</option>');
                    }
                    else {
                        $('#pid').append('<option value="' + id + '">' + title + '</option>');
                    }
                }

            }
        }
    }
    else {
        $('#pid').empty();
        $('#pid').append('<option value="" ltype="0" selected>所有玩法</option>');
    }
}
function LotteryPlayChange() {
    var ltype = $("#type").find("option:selected").attr("value");
    if (ltype != 0) {
        $('#play').empty();
        $('#play').append('<option value="0" selected>所有分类</option>');
        for (i = 0; i <= PlayTypeData.table.length - 1; i++) {
            if (PlayTypeData.table[i].typeid == ltype) {
                var id = PlayTypeData.table[i].id;
                var title = PlayTypeData.table[i].title;
                if (i == 0) {
                    $('#play').append('<option value="' + id + '">' + title + '</option>');
                }
                else {
                    $('#play').append('<option value="' + id + '">' + title + '</option>');
                }
            }
        }
    }
    else {
        $('#play').empty();
        $('#play').append('<option value="0" selected>所有玩法</option>');
    }
}

function LotteryDataGetNum(id) {
    top.Lottery.Loading.show("正在采集开奖数据，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "",
        url: "/admin/ajaxLotterydata.aspx?oper=ajaxGetNum&clienttime=" + Math.random() + "&flag=" + $('#flag').val(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            switch (d.result) {
                case '-1':
                    top.Lottery.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                    break;
                case '0':
                    top.Lottery.Alert(d.returnval, "0");
                    break;
                case '1':
                    top.Lottery.Message(d.returnval, "1");
                    ajaxList(page);
                    break;
            }
        }
    });
}
function LotteryDataPaiJiang() {
    top.Lottery.Loading.show("正在派奖，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "",
        url: "/admin/ajaxLotterydata.aspx?oper=ajaxPaiJiang&clienttime=" + Math.random() + "&flag=" + $('#flag').val(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            switch (d.result) {
                case '-1':
                    top.Lottery.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                    break;
                case '0':
                    top.Lottery.Alert(d.returnval, "0");
                    break;
                case '1':
                    top.Lottery.Message(d.returnval, "1");
                    ajaxList(page);
                    break;
            }
        }
    });
}
function LotteryDataPaiJiangTitle() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要派奖的奖期", "0");
        return;
    }
    top.Lottery.Loading.show("正在派奖，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxLotterydata.aspx?oper=ajaxPaiJiangTitle&clienttime=" + Math.random() + "&flag=" + $('#flag').val(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Message(d.returnval, "1");
            ajaxList(page);
        }
    });
}


//投注记录相关
function ajaxPaiJiangBetId() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要派奖的记录", "0");
        return;
    }
    top.Lottery.Loading.show("正在派奖，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxBet.aspx?oper=ajaxPaiJiangBetId&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            switch (d.result) {
                case '0':
                    top.Lottery.Alert(d.returnval, "0");
                    break;
                case '1':
                    top.Lottery.Message(d.returnval, "1");
                    ajaxList(page);
                    break;
            }
        }
    });
}

function ajaxCancelTitle() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要撤单的奖期", "0");
        return;
    }
    top.Lottery.Loading.show("正在撤单，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxLotterydata.aspx?oper=ajaxCancelTitle&clienttime=" + Math.random() + "" + flag,
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Message(d.returnval, "1");
            ajaxList(page);
        }
    });
}

function ajaxCancelTitleOfNo() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要撤单的奖期", "0");
        return;
    }
    top.Lottery.Loading.show("正在撤单，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxLotterydata.aspx?oper=ajaxCancelTitleOfNo&clienttime=" + Math.random() + "" + flag,
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Message(d.returnval, "1");
            ajaxList(page);
        }
    });
}

function ajaxBetCancel() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要撤单的记录", "0");
        return;
    }
    top.Lottery.Loading.show("正在撤单，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxBet.aspx?oper=ajaxBetCanel&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Message(d.returnval, "1");
            ajaxList(page);
        }
    });
}

function ajaxBetCheat() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要改单的记录", "0");
        return;
    }
    top.Lottery.Loading.show("正在加入改单列表，请稍后...");
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/admin/ajaxBet.aspx?oper=ajaxBetCheat&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            top.Lottery.Alert(d.returnval, "0");
            ajaxList(page);
        }
    });
}

//解除绑定
function ConfirmUnLock(id) {
    top.top.Lottery.Confirm("确定要解除绑定吗?", "getCurrentIframe().ajaxUnLock(" + id + ")");
}
function ajaxUnLock(id) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: "id=" + id,
        url: "/admin/ajaxSysBank.aspx?oper=ajaxUnLock&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            switch (d.result) {
                case '-1':
                    top.Lottery.Alert(d.returnval, "0", "top.window.location='login.aspx';");
                    break;
                case '0':
                    top.Lottery.Alert(d.returnval, "0");
                    break;
                case '1':
                    ajaxList(page);
                    break;
            }
        }
    });
}


function ajaxAllUpdatePoint() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        top.Lottery.Alert("请先勾选要编辑的会员", "0");
        return;
    }
    top.Lottery.Popup.show('userUpdatePoints.aspx?id="'+ids+'"', 600, 400, true)
}