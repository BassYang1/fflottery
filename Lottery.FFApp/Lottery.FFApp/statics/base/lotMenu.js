function GetMenu(action) {
    var TableTemplate = "";
    switch (action) {
        case "b":
            TableTemplate = {
                Title: "报表中心",
                ListCount: 3,
                ListName: [{ key: "游戏记录" }, { key: "追号记录" }, { key: "账变记录"}],
                ListUrl: [{ key: "/aspx/list.aspx?nav=betlist" }, { key: "/aspx/list.aspx?nav=betzhlist" }, { key: "/aspx/list.aspx?nav=UserHistory"}]
            };
            break;
        case "m":
            TableTemplate = {
                Title: "财务中心",
                ListCount: 5,
                ListName: [{ key: "专线充值" }, { key: "存款" }, { key: "取款" }, { key: "存款记录" }, { key: "取款记录" }, { key: "转账记录"}],
                ListUrl: [{ key: "/money/specialcharge.html" }, { key: "/money/charge.aspx" }, { key: "/money/getcash.aspx" }, { key: "/aspx/list.aspx?nav=ChargeList" }, { key: "/aspx/list.aspx?nav=GetCashList" }, { key: "/aspx/list.aspx?nav=GetTranAccList" }]
            };
            break;
        case "u":
            TableTemplate = {
                Title: "代理中心",
                ListCount: 8,
                ListName: [
                { key: "代理首页" },
                { key: "开户中心" },
                { key: "用户管理" },
                { key: "在线会员" },
                { key: "团队报表" },
                { key: "充提记录" },
                { key: "游戏记录" },
                { key: "账变记录" }
                ],
                ListUrl: [
                { key: "/user/userindex.aspx" },
                { key: "/user/useradd.aspx" },
                { key: "/aspx/list.aspx?nav=UserList" },
                { key: "/aspx/list.aspx?nav=UserListOnline" },
                { key: "/aspx/list.aspx?nav=UserProListSub" },
                { key: "/aspx/list.aspx?nav=UserChargeCashHistory" },
                { key: "/aspx/list.aspx?nav=betlist_User" },
                { key: "/aspx/list.aspx?nav=UserHistory_User" }
                ]
            };
            break;
        case "h":
            TableTemplate = {
                Title: "新手帮助",
                ListCount: 3,
                ListName: [
                { key: "时时彩玩法" },
                { key: "11选5玩法" },
                { key: "3dP3玩法" }
                ],
                ListUrl: [
                { key: "/help/ssc.html" },
                { key: "/help/11x5.html" },
                { key: "/help/3d.html" }
                ]
            };
            break;
//        case "a":
//            TableTemplate = {
//                Title: "平台活动",
//                ListCount: 3,
//                ListName: [
//                { key: "百万话费大派送" },
//                { key: "日奖励活动" },
//                { key: "日奖励活动" }
//                ],
//                ListUrl: [
//                { key: "/active/active3.html" },
//                { key: "/active/active4.html" },
//                { key: "/active/active5.html" }
//                ]
//            };
        //            break; 
        case "e":
            TableTemplate = {
                Title: "邮件中心",
                ListCount: 3,
                ListName: [{ key: "收件箱" }, { key: "发件箱" }, { key: "发送信息"}],
                ListUrl: [{ key: "/aspx/list.aspx?nav=Receivelist" }, { key: "/aspx/list.aspx?nav=Sendlist" }, { key: "/email/add.html"}]
            };
            break;
//        case "q":
//            TableTemplate = {
//                Title: "分红契约",
//                ListCount: 5,
//                ListName: [{ key: "我的分红契约" }, { key: "我的分红记录" }, { key: "分配契约" }, { key: "已分配契约" }, { key: "下级分红记录"}],
//                ListUrl: [{ key: "/contract/contractfh.aspx" }, { key: "/contract/MyAgentfhlist.aspx" }, { key: "/aspx/List.aspx?nav=UserListFH" }, { key: "/aspx/List.aspx?nav=UserContractList" }, { key: "/aspx/List.aspx?nav=ContractFHRecord"}]
//            };
//            break;
//        case "g":
//            TableTemplate = {
//                Title: "日结契约",
//                ListCount: 5,
//                ListName: [{ key: "我的日结契约" }, { key: "我的日结记录" }, { key: "分配契约" }, { key: "已分配契约" }, { key: "下级日结记录"}],
//                ListUrl: [{ key: "/contract/Contractgz.aspx" }, { key: "/aspx/List.aspx?nav=AgentGZRecord" }, { key: "/aspx/List.aspx?nav=UserListGZ" }, { key: "/aspx/List.aspx?nav=UserContractGZList" }, { key: "/aspx/List.aspx?nav=ContractGZRecord"}]
//            };
//            break;
        default:

            break;
    }
    return TableTemplate; UserListFH
}