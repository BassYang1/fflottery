function GetPage(action) {
    var TableTemplate = "";
    switch (action) {
        case "betlist":
            TableTemplate = {
                Title: "投注记录",
                PageSize: 12,
                Url: "/ajax/ajaxBet.aspx?oper=ajaxGetList&type=1",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "select", InputTitle: "彩票类别", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "LotteryPlayChange", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "订单状态", InputId: "state", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部状态" }, { key: "0", value: "未开奖" }, { key: "1", value: "已撤单" }, { key: "2", value: "未中奖" }, { key: "3", value: "已中奖" }] },
                             { InputType: "select", InputTitle: "全部内容", InputId: "sel", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部内容" }, { key: "UserName", value: "会员账号" }, { key: "ssid", value: "订单编号" }] },
                             { InputType: "Input", InputTitle: "输入内容", InputId: "u", InputClass: "sel sel-md", Width: "100px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "moshi", Width: "*", Align: "center" },
                        { Header: "金额", Filed: "total", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center", TwoColor: true },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖" }] },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "详情", Function: "LayerPop('投注详情','800px','615px','/bet/betInfo.html?id=@@')" }
                        ]
                        }
                ]
            };
            break;
        case "betzhlist":
            TableTemplate = {
                Title: "追号跟踪",
                PageSize: 12,
                Url: "/ajax/ajaxBet.aspx?oper=ajaxGetZHList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "select", InputTitle: "彩票类别", InputId: "type", InputClass: "sel sel-md", Width: "100px", OnChange: "LotteryPlayChange", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "游戏分类", InputId: "play", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有分类" }] },
                             { InputType: "Input", InputTitle: "订单编号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "开始期号", Filed: "startissuenum", Width: "*", Align: "center" },
                        { Header: "订单彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "完成金额", Filed: "finishname", Width: "*", Align: "center" },
                        { Header: "完成状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "中奖后停止", Filed: "isstopname", Width: "*", Align: "center" },
                        { Header: "订单时间", Filed: "stime", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Link", Title: "详情", Function: "/aspx/list.aspx?nav=BetInfoList&id=@@" }
                        ]
                        }
                ]
            };
            break;
        case "BetInfoList":
            TableTemplate = {
                Title: "追号详细列表",
                PageSize: 12,
                Url: "/ajax/ajaxBet.aspx?oper=ajaxGetZHInfo",
                Query: [],
                Botton: [{ Title: "终止", Function: "operater()", InputClass: "btn btn-primary" },
                        { Title: "返回", Link: "/aspx/list.aspx?nav=betzhlist", InputClass: "btn btn-primary" },
                        { Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "moshi", Width: "*", Align: "center" },
                        { Header: "金额", Filed: "total", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "奖金", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖" }] },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "详情", Function: "LayerPop('投注详情','800px','615px','/bet/betInfo.html?id=@@')" }
                        ]
                        }
                ]
            };
            break;
        case "BetZhInfoIndex":
            TableTemplate = {
                Title: "追号详细列表",
                PageSize: 12,
                Url: "/ajax/ajaxBet.aspx?oper=ajaxGetZHInfo",
                Query: [],
                Botton: [{ Title: "终止", Function: "operater()", InputClass: "btn btn-primary" },
                        { Title: "返回", Link: "/aspx/list.aspx?nav=betzhlist", InputClass: "btn btn-primary" },
                        { Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "moshi", Width: "*", Align: "center" },
                        { Header: "金额", Filed: "total", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "奖金", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖" }] },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "详情", Function: "LayerPop('投注详情','800px','615px','/bet/betInfo.html?id=@@')" }
                        ]
                        }
                ]
            };
            break;
        case "UserHistory":
            TableTemplate = {
                Title: "资金流水",
                PageSize: 12,
                Url: "/ajax/ajaxHistory.aspx?oper=ajaxGetList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "流水类别", InputId: "tid", InputClass: "sel sel-md", Width: "100px", Options: HistoryJsonData },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Uname", value: "会员账号" }, { key: "ssid", value: "流水号" }] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "200px" }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "流水号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "uname", Width: "*", Align: "center" },
                        { Header: "流水类型", Filed: "codename", Width: "*", Align: "center", Color: "blue" },
                        { Header: "操作前余额", Filed: "moneyago", Width: "*", Align: "center" },
                        { Header: "收入/支出", Filed: "moneychange", Width: "*", Align: "center", TwoColor: true },
                        { Header: "操作后余额", Filed: "moneyafter", Width: "*", Align: "center" },
                        { Header: "流水时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "remark", Width: "*", Align: "center", IsPop: "true" }]
            };
            break;
        case "MoneyStatOfDay":
            TableTemplate = {
                Title: "每日报表",
                PageSize: 12,
                Url: "/ajax/ajaxHistory.aspx?oper=ajaxGetListDay",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(-30) },
                        { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true }],
                SumList: [
                        { Header: "时间", Filed: "", Title: "当前合计", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true }]
            };
            break;
        case "ChargeList":
            TableTemplate = {
                Title: "充值记录",
                PageSize: 12,
                Url: "/ajax/ajaxMoney.aspx?oper=ajaxGetChargeList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "充值状态", InputId: "state", InputClass: "sel sel-md", Width: "100px", Options: ChargeJsonData }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "交易单号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "交易类型", Filed: "bankname", Width: "*", Align: "center" },
                        { Header: "充值金额", Filed: "inmoney", Width: "*", Align: "center", TwoColor: true },
                        { Header: "到账金额", Filed: "dzmoney", Width: "*", Align: "center", TwoColor: true },
                        { Header: "充值状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "充值时间", Filed: "stime", Width: "*", Align: "center" }]
            };
            break;
        case "GetCashList":
            TableTemplate = {
                Title: "取款记录",
                PageSize: 12,
                Url: "/ajax/ajaxMoney.aspx?oper=ajaxGetCashList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "取款状态", InputId: "state", InputClass: "sel sel-md", Width: "100px", Options: GetCashJsonData }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "交易单号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "提款银行", Filed: "paybank", Width: "*", Align: "center" },
                        { Header: "取款金额", Filed: "cashmoney", Width: "*", Align: "center", TwoColor: true },
                        { Header: "取款状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "取款时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "msg", Width: "*", Align: "center", IsPop: "true" }]
            };
            break;
        case "GetTranAccList":
            TableTemplate = {
                Title: "转账记录",
                PageSize: 16,
                Url: "/ajax/ajaxMoney.aspx?oper=ajaxGetTranAccList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "转账类型", InputId: "state", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部转账" }, { key: "0", value: "活动转账" }, { key: "1", value: "其他转账" }] }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "200px", Align: "center" },
                        { Header: "转账类型", Filed: "type", Width: "200px", Align: "center", Default: [{ 0: "活动转账", 1: "其他转账" }] },
                        { Header: "转出账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "转入账号", Filed: "tousername", Width: "*", Align: "center" },
                        { Header: "转账金额", Filed: "moneychange", Width: "*", Align: "center" },
                        { Header: "转账时间", Filed: "stime", Width: "*", Align: "center" }
                ]
            };
            break;
            //代理中心     
        case "UserLinkList":
            TableTemplate = {
                Title: "开户链接",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetRegStrList",
                Query: [],
                Botton: [{ Title: "一键生成链接", Function: "ajaxRegStrAll()", InputClass: "btn btn-primary" },
                { Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "返点", Filed: "point", Width: "*", Align: "center", Link: "ssid" },
                       { Header: "链接地址", Filed: "url", Width: "*", Align: "left" },
                       { Header: "创建时间", Filed: "stime", Width: "*", Align: "center" }]
            };
            break;
        case "UserList":
            TableTemplate = {
                Title: "用户列表",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "select", InputTitle: "状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线" }] },
                             { InputType: "Input", InputTitle: "金额小于", InputId: "money1", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "金额大于", InputId: "money2", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                { Title: "返回上级", Function: "ajaxToParent()", InputClass: "btn btn-primary" }],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center", FunctionUserParent: "ajaxSearchByParent" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "最后登录Ip", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后时间", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线" }] },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                            { Type: "Popup", Title: "转账", Function: "LayerPop('在线转账','650px','500px','/user/usertranacc.aspx?id=@@')" },
                            { Type: "Popup", Title: "编辑", Function: "LayerPop('编辑返点','650px','400px','/user/userupdate.aspx?id=@@')" }
                        ]
                        }]
            };
            break;
        case "UserListOnline":
            TableTemplate = {
                Title: "用户列表",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetFKListOnLine",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "Input", InputTitle: "金额小于", InputId: "money1", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "金额大于", InputId: "money2", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center", Function: "ajaxSearchById" },
                        { Header: "会员关系", Filed: "usercodes", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "最后登录时间", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线" }] }]
            };
            break;
        case "UserProListSub":
            TableTemplate = {
                Title: "团队统计",
                PageSize: 10,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetFKProListSub",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "有无账变", InputId: "tid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "显示全部会员" }, { key: "1", value: "只显示有账变" }] },
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "返点", Filed: "userpoint", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                            { Type: "Link", Title: "团队报表", Function: "javascript:ajaxSearchById(@UserId@);" }
                        ]
                        },

                ]
            }
            break;
        case "UserChargeCashHistory":
            TableTemplate = {
                Title: "充提记录",
                PageSize: 12,
                Url: "/ajax/ajaxHistory.aspx?oper=ajaxGetChargeCashList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "200px" }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "流水号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "uname", Width: "*", Align: "center" },
                        { Header: "流水类型", Filed: "codename", Width: "*", Align: "center" },
                        { Header: "操作前余额", Filed: "moneyago", Width: "*", Align: "center" },
                        { Header: "收入/支出", Filed: "moneychange", Width: "*", Align: "center", TwoColor: true },
                        { Header: "操作后余额", Filed: "moneyafter", Width: "*", Align: "center" },
                        { Header: "流水时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "remark", Width: "*", Align: "center", IsPop: "true" }]
            };
            break;
        case "betlist_User":
            TableTemplate = {
                Title: "投注记录",
                PageSize: 12,
                Url: "/ajax/ajaxBet.aspx?oper=ajaxGetList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "select", InputTitle: "彩票类别", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "LotteryPlayChange", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "订单状态", InputId: "state", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部状态" }, { key: "0", value: "未开奖" }, { key: "1", value: "已撤单" }, { key: "2", value: "未中奖" }, { key: "3", value: "已中奖" }] },
                             { InputType: "select", InputTitle: "查询范围", InputId: "type", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部记录" }, { key: "0", value: "仅查自己" }, { key: "1", value: "直属下级" }, { key: "2", value: "所有下级" }] },
                             { InputType: "select", InputTitle: "全部内容", InputId: "sel", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部内容" }, { key: "UserName", value: "会员账号" }, { key: "ssid", value: "订单编号" }] },
                             { InputType: "Input", InputTitle: "输入内容", InputId: "u", InputClass: "sel sel-md", Width: "100px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "moshi", Width: "*", Align: "center" },
                        { Header: "金额", Filed: "total", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center", TwoColor: true },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖" }] },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "详情", Function: "LayerPop('投注详情','800px','615px','/bet/betInfo.html?id=@@')" }
                        ]
                        }
                ]
            };
            break;
        case "UserHistory_User":
            TableTemplate = {
                Title: "资金流水",
                PageSize: 12,
                Url: "/ajax/ajaxHistory.aspx?oper=ajaxGetList_User",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "流水类别", InputId: "tid", InputClass: "sel sel-md", Width: "100px", Options: HistoryJsonData },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Uname", value: "会员账号" }, { key: "ssid", value: "流水号" }] },
                             { InputType: "Input", InputTitle: "请输入内容", InputId: "u", InputClass: "sel sel-md", Width: "200px" }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "流水号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "uname", Width: "*", Align: "center" },
                        { Header: "流水类型", Filed: "codename", Width: "*", Align: "center" },
                        { Header: "操作前余额", Filed: "moneyago", Width: "*", Align: "center" },
                        { Header: "收入/支出", Filed: "moneychange", Width: "*", Align: "center", TwoColor: true },
                        { Header: "操作后余额", Filed: "moneyafter", Width: "*", Align: "center" },
                        { Header: "流水时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "remark", Width: "*", Align: "center", IsPop: "true" }]
            };
            break;
        case "Receivelist":
            TableTemplate = {
                Title: "收件箱",
                PageSize: 12,
                Url: "/ajax/ajaxEmail.aspx?oper=ajaxGetReceiveList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "发件人", Filed: "sendname", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "标题", Filed: "title", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "详情", Function: "LayerPop('邮件详情','800px','615px','/email/info.aspx?id=@@')" }
                        ]
                        }]
            };
            break;
        case "Sendlist":
            TableTemplate = {
                Title: "发件箱",
                PageSize: 12,
                Url: "/ajax/ajaxEmail.aspx?oper=ajaxGetSendList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "收件人", Filed: "receivename", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "标题", Filed: "title", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "详情", Function: "LayerPop('邮件详情','800px','615px','/email/info2.aspx?id=@@')" }
                        ]
                        }
                ]
            };
            break;

        case "AgentFHRecord":
            TableTemplate = {
                Title: "我的分红记录",
                PageSize: 10,
                Url: "/ajax/ajaxContractFH.aspx?oper=ajaxGetAgentFHRecord",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                            { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "开始时间", Filed: "starttime", Width: "150", Align: "center" },
                        { Header: "结束时间", Filed: "endtime", Width: "150", Align: "center" },
                        { Header: "投注", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "亏损", Filed: "total", Width: "150", Align: "center" },
                        { Header: "分红比例(%)", Filed: "per", Width: "100", Align: "center" },
                        { Header: "分红金额", Filed: "inmoney", Width: "150", Align: "center" },
                        { Header: "契约分红(个)", Filed: "contractcount", Width: "100", Align: "center" },
                        { Header: "分红时间", Filed: "stime", Width: "150", Align: "center" }
                ]
            };
            break;

        case "UserContractList":
            TableTemplate = {
                Title: "分红契约信息",
                PageSize: 12,
                Url: "/ajax/ajaxContractFH.aspx?oper=ajaxGetList",
                Query: [{ InputType: "Input", InputTitle: "契约会员", InputId: "u", InputClass: "sel sel-md", Width: "100px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "编号", Filed: "rowid", Width: "*", Align: "center" },
                        { Header: "契约类型", Filed: "type", Width: "*", Align: "center", Default: [{ 1: "分红契约", 2: "日结契约" }] },
                        { Header: "契约会员", Filed: "username", Width: "*", Align: "center" },
                        { Header: "契约状态", Filed: "isused", Width: "*", Align: "center", Default: [{ 99: "未分配，请分配", 0: "已分配，待确认", 1: "已成功签订", 2: "会员拒绝签订", 3: "撤销等待确认", 4: "撤销已确认，请重新分配" }] },
                        { Header: "分配时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "签订时间", Filed: "stime2", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                                     { Type: "Popup", Title: "分配/查看", Function: "LayerPop('契约详情','800px','550px','/contract/ContractfhPop.aspx?id=@@')" },
									 { Type: "Popup", Title: "本期详情", Function: "LayerPop('本期详情','800px','550px','/contract/ContractfhOper.aspx?id=@@')" }
                        ]
                        }]
            };
            break;

        case "ContractFHRecord":
            TableTemplate = {
                Title: "下级分红记录",
                PageSize: 10,
                Url: "/ajax/ajaxContractFH.aspx?oper=ajaxGetContractFHRecord",
                Query: [
                        { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" },
                        { InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                        { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "开始时间", Filed: "starttime", Width: "150", Align: "center" },
                        { Header: "结束时间", Filed: "endtime", Width: "150", Align: "center" },
                        { Header: "投注", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "亏损", Filed: "total", Width: "150", Align: "center" },
                        { Header: "分红比例(%)", Filed: "per", Width: "150", Align: "center" },
                        { Header: "分红金额", Filed: "inmoney", Width: "150", Align: "center" },
                        { Header: "分红时间", Filed: "stime", Width: "150", Align: "center" }
                ]
            };
            break;

        case "ContractFHLog":
            TableTemplate = {
                Title: "日志记录",
                PageSize: 10,
                Url: "/ajax/ajaxContractFH.aspx?oper=ajaxGetContractFHLog",
                Query: [
                        //{ InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" },
                        { InputType: "select", InputTitle: "发放状态", InputId: "state", InputClass: "sel sel-md", Width: "100px", Options: ContractLogData },
                        { InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(-6) },
                        { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "销量", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "亏损", Filed: "loss", Width: "150", Align: "center" },
                        { Header: "分红金额", Filed: "money", Width: "150", Align: "center" },
                        { Header: "分红时间", Filed: "opertime", Width: "150", Align: "center" },
                        { Header: "备注", Filed: "remark", Width: "150", Align: "center" },
                        { Header: "", Width: "*", Align: "center", Info: [{ Type: "Link", Title: "补发分红", Function: "javascript:ajaxFHReissue(@@);" }] }
                ]
            };
            break;
        case "AgentGZRecord":
            TableTemplate = {
                Title: "我的日结记录",
                PageSize: 10,
                Url: "/ajax/ajaxContractGZ.aspx?oper=ajaxGetAgentGZRecord",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                            { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "活动单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "销量", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "工资比例(%)", Filed: "per", Width: "100", Align: "center" },
                        { Header: "工资金额", Filed: "inmoney", Width: "150", Align: "center" },
                        { Header: "日结契约(个)", Filed: "contractcount", Width: "100", Align: "center" },
                        { Header: "工资时间", Filed: "stime", Width: "150", Align: "center" },
                         { Header: "备注", Filed: "remark", Width: "150", Align: "center" }
                ]
            };
            break;

        case "UserContractGZList":
            TableTemplate = {
                Title: "日结契约信息",
                PageSize: 12,
                Url: "/ajax/ajaxContractGZ.aspx?oper=ajaxGetList",
                Query: [{ InputType: "Input", InputTitle: "契约会员", InputId: "u", InputClass: "sel sel-md", Width: "100px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "编号", Filed: "rowid", Width: "*", Align: "center" },
                        { Header: "契约类型", Filed: "type", Width: "*", Align: "center", Default: [{ 1: "分红契约", 2: "日结契约" }] },
                        { Header: "契约会员", Filed: "username", Width: "*", Align: "center" },
                        { Header: "契约状态", Filed: "isused", Width: "*", Align: "center", Default: [{ 99: "未分配，请分配", 0: "已分配，待确认", 1: "已成功签订", 2: "会员拒绝签订", 3: "撤销等待确认", 4: "撤销已确认，请重新分配" }] },
                        { Header: "分配时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "签订时间", Filed: "stime2", Width: "*", Align: "center" },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
									 { Type: "Popup", Title: "本期详情", Function: "LayerPop('本期详情','800px','550px','/contract/ContractgzOper.aspx?id=@@')" }
                        ]
                        }]
            };
            break;

        case "ContractGZRecord":
            TableTemplate = {
                Title: "下级日结记录",
                PageSize: 10,
                Url: "/ajax/ajaxContractGZ.aspx?oper=ajaxGetContractGZRecord",
                Query: [
                        //{ InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" },
                        { InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(-6) },
                        { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "活动单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "销量", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "工资金额", Filed: "inmoney", Width: "150", Align: "center" },
                        { Header: "工资时间", Filed: "stime", Width: "150", Align: "center" },
                         { Header: "备注", Filed: "remark", Width: "150", Align: "center" }
                ]
            };
            break;
        case "ContractGZLog":
            TableTemplate = {
                Title: "日志记录",
                PageSize: 10,
                Url: "/ajax/ajaxContractGZ.aspx?oper=ajaxGetContractGZLog",
                Query: [
                        //{ InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" },
                        { InputType: "select", InputTitle: "发放状态", InputId: "state", InputClass: "sel sel-md", Width: "100px", Options: ContractLogData },
                        { InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(-6) },
                        { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "销量", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "工资金额", Filed: "money", Width: "150", Align: "center" },
                        { Header: "工资时间", Filed: "opertime", Width: "150", Align: "center" },
                        { Header: "备注", Filed: "remark", Width: "150", Align: "center" },
                        { Header: "", Width: "*", Align: "center", Info: [{ Type: "Link", Title: "补发工资", Function: "javascript:ajaxGZReissue(@@);" }] }
                ]
            };
            break;
        case "UserListFH":
            TableTemplate = {
                Title: "分配契约",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetList&group=3",
                Query: [
                             { InputType: "select", InputTitle: "状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线" }] },
                             { InputType: "Input", InputTitle: "金额小于", InputId: "money1", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "金额大于", InputId: "money2", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                { Title: "返回上级", Function: "ajaxToParent()", InputClass: "btn btn-primary" }],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center", FunctionUserParent: "ajaxSearchByParent" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "最后登录Ip", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后时间", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线" }] },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                            { Type: "Popup", Title: "分配/查看", Function: "LayerPop('契约详情','800px','550px','/contract/ContractfhPop.aspx?id=@@')" },
                        ]
                        }]
            };
            break;
        case "UserListGZ":
            TableTemplate = {
                Title: "分配契约",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetList&group=2",
                Query: [
                             { InputType: "select", InputTitle: "状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线" }] },
                             { InputType: "Input", InputTitle: "金额小于", InputId: "money1", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "金额大于", InputId: "money2", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                { Title: "返回上级", Function: "ajaxToParent()", InputClass: "btn btn-primary" }],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center", FunctionUserParent: "ajaxSearchByParent" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "最后登录Ip", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后时间", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线" }] },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                            { Type: "Popup", Title: "分配/查看", Function: "LayerPop('契约详情','800px','550px','/contract/ContractgzPop.aspx?id=@@')" },
                        ]
                        }]
            };
            break;
        case "ContractUserList":
            TableTemplate = {
                Title: "契约下级",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetContractUserList",
                Query: [
                             { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "级别", Filed: "usergroupname", Width: "*", Align: "right" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "最后登录Ip", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后时间", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线" }] },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                            //{ Type: "Link", Title: "[报表]", Function: "/aspx/list.aspx?nav=BetInfoList&id=@@" },
                            { Type: "Popup", Title: "[签订工资]", Page: "ContractGZ", Function: "LayerPop('签订工资契约','800px','550px','/contract2/ContractgzPop.aspx?id=@@')" },
                            { Type: "Popup", Title: "[签订分红]", Page: "ContractFH", Function: "LayerPop('签订分红契约','800px','550px','/contract2/ContractfhPop.aspx?id=@@')" },
                        ]
                        }]
            };
            break;
        case "ContractUserListFH":
            TableTemplate = {
                Title: "分配分红契约",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetContractUserList",
                Query: [
                             { InputType: "select", InputTitle: "状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线" }] },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "级别", Filed: "usergroupname", Width: "*", Align: "right" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "最后登录Ip", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后时间", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线" }] },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                            { Type: "Popup", Title: "签订契约", Function: "LayerPop('契约详情','800px','550px','/contract/ContractfhPop.aspx?id=@@')" },
                        ]
                        }]
            };
            break;
        case "ContractUserListGZ":
            TableTemplate = {
                Title: "分配工资契约",
                PageSize: 12,
                Url: "/ajax/ajaxUser.aspx?oper=ajaxGetContractUserList",
                Query: [
                             { InputType: "select", InputTitle: "状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线" }] },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "username", InputClass: "ipt", Width: "80px" }
                ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "级别", Filed: "usergroupname", Width: "*", Align: "right" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "最后登录Ip", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后时间", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线" }] },
                        {
                            Header: "", Width: "*", Align: "center"
                        , Info: [
                            { Type: "Popup", Title: "签订契约", Function: "LayerPop('契约详情','800px','550px','/contract/ContractgzPop.aspx?id=@@')" },
                        ]
                        }]
            };
            break;
        default:

            break;
    }
    return TableTemplate;
}