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