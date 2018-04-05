function GetPage(action) {
    var TableTemplate = "";
    switch (action) {
        case "SysSetInfo":
            TableTemplate = {
                Title: "常规设置",
                Url: "/admin/ajaxLottery.aspx?oper=ajaxGetPlaySmallList",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                ListCount: 3,
                ListName: [{ key: "常规设置" }, { key: "数据设置" }, { key: "报警设置"}],
                List1: [
                            { InputType: "select", InputTitle: "平台开关", InputId: "WebIsOpen", InputTip: '平台是否关闭', InputClass: "sel", Options: [{ key: "0", value: "开启" }, { key: "1", value: "关闭"}] },
                            { InputType: "input", InputTitle: "关闭原因", InputId: "WebCloseSeason", InputTip: '平台关闭原因', InputClass: "ipt" },
                            { InputType: "select", InputTitle: "追号开关", InputId: "ZHIsOpen", InputTip: '追号是否关闭', InputClass: "sel", Options: [{ key: "0", value: "开启" }, { key: "1", value: "关闭"}] },
                            { InputType: "select", InputTitle: "注册开关", InputId: "RegIsOpen", InputTip: '注册是否关闭', InputClass: "sel", Options: [{ key: "0", value: "开启" }, { key: "1", value: "关闭"}] },
                            { InputType: "select", InputTitle: "投注开关", InputId: "BetIsOpen", InputTip: '投注是否关闭', InputClass: "sel", Options: [{ key: "0", value: "开启" }, { key: "1", value: "关闭"}] },
                            { InputType: "input", InputTitle: "客服地址", InputId: "CsUrl", InputTip: '第三方的客服地址', InputClass: "ipt" },
                            { InputType: "input", InputTitle: "系统版本", InputId: "ClientVersion", InputTip: '设置系统版本号', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "select", InputTitle: "排行榜设置", InputId: "AutoRanking", InputTip: '排行榜手动还是自动', InputClass: "sel", Options: [{ key: "0", value: "自动" }, { key: "1", value: "手动"}] },
                        ],
                List2: [
                            { InputType: "input", InputTitle: "最大返点", InputId: "MaxLevel", InputTip: '返点大于最大返点不能投注', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "最大投注", InputId: "MaxBet", InputTip: '投注额不能大于此数值', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "最大奖金", InputId: "MaxWin", InputTip: '中奖金额大于此数值按此数值来返奖', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "风控奖金", InputId: "MaxWinFK", InputTip: '可在玩法中设置风控注数，当投注小于风控注数时，按此奖金派奖', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "最小充值金额", InputId: "MinCharge", InputTip: '平台全部充值方式的最小充值金额', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "积分兑换比例", InputId: "Points", InputTip: '多少积分兑换1人民币', InputClass: "ipt", onkeyup: "chkPrice(this)" }
                        ],
                //List3: [
                //{ InputType: "input", InputTitle: "提现限制金额(%)", InputId: "PriceOutCheck", InputTip: '设置必须消费到多少才能提现', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                //{ InputType: "input", InputTitle: "单笔最低金额", InputId: "PriceOut", InputTip: '设置单笔最低金额', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                //{ InputType: "input", InputTitle: "单笔最高金额", InputId: "PriceOut2", InputTip: '设置单笔最高金额', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                //{ InputType: "input", InputTitle: "每天提现次数", InputId: "PriceNum", InputTip: '设置每天提现次数', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                //{ InputType: "input", InputTitle: "允许提现开始时间", InputId: "PriceTime1", InputTip: '设置允许提现开始时间', InputClass: "ipt" },
                //{ InputType: "input", InputTitle: "截止", InputId: "PriceTime2", InputTip: '设置允许提现截止时间', InputClass: "ipt" },
                //{ InputType: "input", InputTitle: "银行卡绑定时间", InputId: "BankTime", InputTip: '设置银行卡绑定多久才能提现', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                //{ InputType: "input", InputTitle: "虚拟排队人数", InputId: "PriceOutPerson", InputTip: '设置虚拟排队人数', InputClass: "ipt", onkeyup: "chkPrice(this)" }
                //],
                List3: [
                            { InputType: "input", InputTitle: "中奖报警", InputId: "WarnTotal", InputTip: '每单中奖金额大于此数值时，自动显示到报警列表中', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "返点报警", InputId: "PointWarnTotal", InputTip: '每单返点金额大于此数值时，自动显示到报警列表中', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "盈利率报警(%)", InputId: "YLLWarnTotal", InputTip: '每单盈利率大于此数值时，自动显示到报警列表中', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "盈利报警", InputId: "RealWarnTotal", InputTip: '每单盈利金额大于此数值时，自动显示到报警列表中', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "活动报警", InputId: "ActiveWarnTotal", InputTip: '活动金额大于此数值时，自动显示到报警列表中', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "分红报警", InputId: "FhWarnTotal", InputTip: '分红金额大于此数值时，自动显示到报警列表中', InputClass: "ipt", onkeyup: "chkPrice(this)" },
                            { InputType: "input", InputTitle: "取款报警", InputId: "GetCashWarnTotal", InputTip: '取款金额大于充值金额乘以此数值时，自动显示到报警列表中', InputClass: "ipt", onkeyup: "chkPrice(this)" }
                        ]
            };
            break;
        case "RoleSave":
            TableTemplate = {
                Title: "新增角色",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_Role",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_Role",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_Role",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "input", InputTitle: "角色名称", InputId: "Name", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "排序", InputId: "Sort", InputClass: "ipt" }
                        ]
            };
            break;
        case "AdminSave":
            TableTemplate = {
                Title: "新增管理员",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_Admin",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_Admin",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_Admin",
                OptUrl: "/admin/ajaxSave.aspx?oper=OptionsInfo&t=Sys_Role&w='IsUsed=0'",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "select", InputTitle: "选择角色", InputId: "RoleId", InputClass: "sel" },
                             { InputType: "input", InputTitle: "管理员账号", InputId: "UserName", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "管理员密码", InputId: "Password", InputClass: "ipt", IsShow: false },
                             { InputType: "hidden", InputTitle: "", InputId: "Flag", Default: "0", InputClass: "ipt" },
                             { InputType: "hidden", InputTitle: "", InputId: "GroupId", Default: "1", InputClass: "ipt" }
                        ]
            };
            break;
        case "AdminGashSave":
            TableTemplate = {
                Title: "新增财务",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_Admin",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_Admin",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_Admin",
                OptUrl: "/admin/ajaxSave.aspx?oper=OptionsInfo&t=Sys_Role&w='IsUsed=0'",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "select", InputTitle: "选择角色", InputId: "RoleId", InputClass: "sel" },
                             { InputType: "input", InputTitle: "财务账号", InputId: "UserName", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "财务密码", InputId: "Password", InputClass: "ipt", IsShow: false },
                             { InputType: "input", InputTitle: "最小提现金额", InputId: "MinCash", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "最大提现金额", InputId: "MaxCash", InputClass: "ipt" },
                             { InputType: "hidden", InputTitle: "", InputId: "Flag", Default: "0", InputClass: "ipt" },
                             { InputType: "hidden", InputTitle: "", InputId: "GroupId", Default: "2", InputClass: "ipt" }
                        ]
            };
            break;
        case "AdminEdit":
            TableTemplate = {
                Title: "重置密码",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_Admin",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_Admin",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_Admin",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "input", InputTitle: "管理员账号", InputId: "UserName", InputClass: "ipt", ReadOnly: true },
                             { InputType: "input", InputTitle: "管理员密码", InputId: "Password", InputClass: "ipt", IsShow: false }
                        ]
            };
            break;
        case "AutoRankSave":
            TableTemplate = {
                Title: "排行榜设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_AutoRank",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_AutoRank",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_AutoRank",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "input", InputTitle: "用户名", InputId: "UserName", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "奖金", InputId: "Win", InputClass: "ipt" },
							 { InputType: "select", InputTitle: "是否启用", InputId: "IsUsed", InputClass: "sel", Options: [{ key: "1", value: "启用" }, { key: "0", value: "禁用"}] },
                      ]
            };
            break;
        case "LoginCheckSave":
            TableTemplate = {
                Title: "排行榜设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_LoginCheck",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_LoginCheck",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_LoginCheck",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "select", InputTitle: "限制类别", InputId: "CheckType", InputClass: "sel", Options: [{ key: "1", value: "限制IP登陆" }, { key: "2", value: "限制用户登陆"}] },
                             { InputType: "input", InputTitle: "限制内容", InputId: "CheckTitle", InputClass: "ipt" },
							 { InputType: "select", InputTitle: "是否启用", InputId: "isused", InputClass: "sel", Options: [{ key: "1", value: "启用" }, { key: "0", value: "禁用"}] }
                      ]
            };
            break;
        case "AdminLoginCheckSave":
            TableTemplate = {
                Title: "排行榜设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_LoginCheck",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_LoginCheck",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_LoginCheck",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "select", InputTitle: "限制类别", InputId: "CheckType", InputClass: "sel", Options: [{ key: "0", value: "后台白名单"}] },
                             { InputType: "input", InputTitle: "限制内容", InputId: "CheckTitle", InputClass: "ipt" },
							 { InputType: "select", InputTitle: "是否启用", InputId: "isused", InputClass: "sel", Options: [{ key: "1", value: "启用" }, { key: "0", value: "禁用"}] }
                      ]
            };
            break;
        case "TaskSetSave":
            TableTemplate = {
                Title: "定时任务设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_TaskSet",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_TaskSet",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_TaskSet",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "任务名称", InputId: "Title", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "队列顺序", InputId: "Sort", InputClass: "ipt" },
                            { InputType: "DateTime", InputTitle: "开始时间", InputId: "StartTime", InputClass: "ipt" },
                            { InputType: "DateTime", InputTitle: "截止时间", InputId: "EndTime", InputClass: "ipt" },
                            { InputType: "textarea", InputTitle: "任务SQL语句", InputId: "StrSql", InputClass: "tea" },
                            { InputType: "select", InputTitle: "是否启用", InputId: "isused", InputClass: "sel", Options: [{ key: "1", value: "启用" }, { key: "0", value: "禁用"}] }
                      ]
            };
            break;
        case "LotterySave":
            TableTemplate = {
                Title: "彩种设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_Lottery",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_Lottery",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_Lottery",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "最小倍数", InputId: "MinTimes", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最大倍数", InputId: "MaxTimes", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "封单时间", InputId: "CloseTime", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "排序", InputId: "Sort", InputClass: "ipt" }
                      ]
            };
            break;
        case "LotteryTypeSave":
            TableTemplate = {
                Title: "分类设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_PlayBigType",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_PlayBigType",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_PlayBigType",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "分类名称", InputId: "Title", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "排序", InputId: "Sort", InputClass: "ipt" }
                      ]
            };
            break;
        case "LotteryPlaySave":
            TableTemplate = {
                Title: "玩法设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_PlaySmallType",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_PlaySmallType",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_PlaySmallType",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "游戏玩法", InputId: "Title", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最大奖金", InputId: "MaxBonus", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最小奖金", InputId: "MinBonus", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "混选组六奖金", InputId: "MinBonus2", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "风控注数", InputId: "MinNum", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最大注数", InputId: "MaxNum", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "排序", InputId: "Sort", InputClass: "ipt" }
                      ]
            };
            break;
        case "LotteryTimeSave":
            TableTemplate = {
                Title: "时间设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_LotteryTime",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_LotteryTime",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_LotteryTime",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "游戏期号", InputId: "Sn", InputClass: "ipt" },
                            { InputType: "DateTime", InputTitle: "开奖时间", InputId: "Time", InputClass: "ipt" }
                      ]
            };
            break;
        case "LotteryUrlSave":
            TableTemplate = {
                Title: "采集地址设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_lotteryUrl",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_lotteryUrl",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_lotteryUrl",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "游戏名称", InputId: "LName", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "采集地址", InputId: "Url", InputClass: "ipt" }
                      ]
            };
            break;
        case "ActiveSetSave":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_ActiveSet",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_ActiveSet",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_ActiveSet",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "活动名称", InputId: "Name", InputClass: "ipt" },
                            { InputType: "DateTime", InputTitle: "开始时间", InputId: "StartTime", InputClass: "ipt" },
                            { InputType: "DateTime", InputTitle: "截止时间", InputId: "EndTime", InputClass: "ipt" }
                      ]
            };
            break;
        case "ActYongJin":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_SetYJDetail",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_SetYJDetail",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_SetYJDetail",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(元)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(元)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "直属佣金(元)", InputId: "Group3", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "总代佣金(元)", InputId: "Group2", InputClass: "ipt" },
                      ]
            };
            break;
        case "ActYongJin2":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_SetYJDetail2",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_SetYJDetail2",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_SetYJDetail2",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(元)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(元)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上级(元)", InputId: "money", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上上级(元)", InputId: "money2", InputClass: "ipt" },
                      ]
            };
            break;
        case "ActGongZi":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_SetGZDetail",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_SetGZDetail",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_SetGZDetail",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(万)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(万)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "活跃人数", InputId: "MinUsers", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "工资比例(%)", InputId: "Money", InputClass: "ipt" },
                      ]
            };
            break;
        case "ActGongZi2":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_SetGZDetail2",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_SetGZDetail2",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_SetGZDetail2",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(万)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(万)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "活跃人数", InputId: "MinUsers", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "工资比例(%)", InputId: "Money", InputClass: "ipt" },
                      ]
            };
            break;
        case "ActDay15Fenhong":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_Day15FHSet",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_Day15FHSet",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_Day15FHSet",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(万)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(万)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "直属分红(%)", InputId: "Group3", InputClass: "ipt" },
                      ]
            };
            break;
        case "ActDayGongZi":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_DayGzSet",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_DayGzSet",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_DayGzSet",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(万)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(万)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "活跃人数", InputId: "MinUsers", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "工资比例(%)", InputId: "Money", InputClass: "ipt" },
                      ]
            };
            break;
        case "ActDayYongJin":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_DayYJSet",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_DayYJSet",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_DayYJSet",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(元)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(元)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "特权直属佣金(元)", InputId: "Group3", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "直属佣金(元)", InputId: "Group2", InputClass: "ipt" },
                      ]
            };
            break;
        case "ActFenHong":
            TableTemplate = {
                Title: "活动设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Act_SetFHDetail",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Act_SetFHDetail",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Act_SetFHDetail",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                        { InputType: "input", InputTitle: "活动说明", InputId: "Name", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "下限设置(万)", InputId: "MinMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "上限设置(万)", InputId: "MaxMoney", InputClass: "ipt" },
                        { InputType: "input", InputTitle: "直属分红(%)", InputId: "Group3", InputClass: "ipt" },
                      ]
            };
            break;
        case "SysBankSave":
            TableTemplate = {
                Title: "银行设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_Bank",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_Bank",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_Bank",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "姓名", InputId: "Name", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "账号", InputId: "Account", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最小充值", InputId: "MinCharge", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最大充值", InputId: "MaxCharge", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "开始时间", InputId: "StartTime", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "结束时间", InputId: "EndTime", InputClass: "ipt" }
                      ]
            };
            break;
        case "SysGetCashBankSave":
            TableTemplate = {
                Title: "银行设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_Bank",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_Bank",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_Bank",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "最小取款", InputId: "MinCharge", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最大取款", InputId: "MaxCharge", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "每天提现次数", InputId: "MaxGetCash", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "取款消费限制(%)", InputId: "BetPerCheck", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "绑定多久可提现(小时)", InputId: "BindTime", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "开始时间", InputId: "StartTime", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "结束时间", InputId: "EndTime", InputClass: "ipt" }
                      ]
            };
            break;
        case "ChargeSetSave":
            TableTemplate = {
                Title: "充值设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_ChargeSet",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_ChargeSet",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_ChargeSet",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "select", InputTitle: "第三方名称", InputId: "Name", InputClass: "sel", Options: ChargeSetData },
                            { InputType: "input", InputTitle: "前台名称", InputId: "MerName", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "商户ID(支付宝账号)", InputId: "MerCode", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "商户Key(支付宝姓名)", InputId: "MerKey", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最小充值", InputId: "MinCharge", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "最大充值", InputId: "MaxCharge", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "开始时间", InputId: "StartTime", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "结束时间", InputId: "EndTime", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "每日限额", InputId: "Total", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "排序", InputId: "Sort", InputClass: "ipt" }
                      ]
            };
            break;
        case "UserBankSave":
            TableTemplate = {
                Title: "绑定银行设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=N_UserBank",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=N_UserBank",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=N_UserBank",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "银行卡号", InputId: "PayAccount", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "银行户名", InputId: "PayName", InputClass: "ipt" },
                            { InputType: "input", InputTitle: "开户地址", InputId: "PayBankAddress", InputClass: "ipt" }
                      ]
            };
            break;
        case "UserQuotaSave":
            TableTemplate = {
                Title: "配额设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=N_UserQuota",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=N_UserQuota",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=N_UserQuota",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                            { InputType: "input", InputTitle: "返点", InputId: "UserLevel", InputClass: "ipt", ReadOnly: false },
                            { InputType: "input", InputTitle: "配额数量", InputId: "ChildNums", InputClass: "ipt" }
                      ]
            };
            break;
        case "LotteryDataSave":
            TableTemplate = {
                Title: "手动开奖",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_LotteryData",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_LotteryData",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_LotteryData",
                OptUrl: "/admin/ajaxSave.aspx?oper=OptionsInfo&t=Sys_Lottery&w='IsOpen=0'&n='Title'",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "select", InputTitle: "选择彩种", InputId: "Type", InputClass: "sel" },
                             { InputType: "input", InputTitle: "开奖期号", InputId: "Title", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "开奖号码", InputId: "Number", InputClass: "ipt" },
                              { InputType: "input", InputTitle: "开奖时间", InputId: "OpenTime", InputClass: "ipt" },
                        ]
            };
            break;
        case "NewsSave":
            TableTemplate = {
                Title: "手动开奖",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_LotteryData",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_LotteryData",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_LotteryData",
                OptUrl: "/admin/ajaxSave.aspx?oper=OptionsInfo&t=Sys_Lottery&w='IsOpen=0'&n='Title'",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "select", InputTitle: "选择彩种", InputId: "Type", InputClass: "sel" },
                             { InputType: "input", InputTitle: "开奖期号", InputId: "Title", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "开奖号码", InputId: "Number", InputClass: "ipt" },
                              { InputType: "input", InputTitle: "开奖时间", InputId: "OpenTime", InputClass: "ipt" },
                        ]
            };
            break;
        case "LotteryCheckSave":
            TableTemplate = {
                Title: "自主彩设置",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=Sys_LotteryCheck",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=Sys_LotteryCheck",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=Sys_LotteryCheck",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "input", InputTitle: "杀数个数", InputId: "CheckNum", InputClass: "ipt", onkeyup: "chkPrice(this)" },
                             { InputType: "input", InputTitle: "自动开启杀数比例（%）", InputId: "CheckPer", InputClass: "ipt", onkeyup: "chkPrice(this)" }
                        ]
            };
            break;
        case "UserGroupQuotaSave":
            TableTemplate = {
                Title: "设置类型配额",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=N_UserGroupQuota",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=N_UserGroupQuota",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=N_UserGroupQuota",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "select", InputTitle: "会员类型", InputId: "Group", InputClass: "sel", Options: [{ key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                             { InputType: "select", InputTitle: "可开户类型", InputId: "ToGroup", InputClass: "sel", Options: [{ key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                             { InputType: "input", InputTitle: "可开户数量", InputId: "ChildNums", InputClass: "ipt" }
                      ]
            };
            break;
        case "UserPointQuotaSave":
            TableTemplate = {
                Title: "设置平级配额",
                InfoUrl: "/admin/ajaxSave.aspx?oper=Info&t=N_UserPointQuota",
                SaveUrl: "/admin/ajaxSave.aspx?oper=Save&t=N_UserPointQuota",
                UpdateUrl: "/admin/ajaxSave.aspx?oper=Update&t=N_UserPointQuota",
                Botton: [{ Title: "保存更改", Function: "ajaxPost()", InputClass: "btn btn-primary"}],
                List: [
                             { InputType: "input", InputTitle: "会员返点", InputId: "Point", InputClass: "ipt" },
                             { InputType: "input", InputTitle: "可开平级数量", InputId: "ChildNums", InputClass: "ipt" }
                      ]
            };
            break;
        default:
            break;
    }
    return TableTemplate;
}

var ChargeSetData = [
    { key: "国付宝", value: "国付宝" },
    { key: "易宝支付", value: "易宝支付" },
    { key: "手动收款", value: "手动收款" },
    { key: "优付支付", value: "优付支付" },
    { key: "久付支付", value: "久付支付" },
    { key: "秒卡通", value: "秒卡通" },
    { key: "多多支付", value: "多多支付" },
    { key: "国盛通", value: "国盛通" },
    { key: "智汇付", value: "智汇付" },
    { key: "易汇金", value: "易汇金" }];