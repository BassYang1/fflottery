function GetPage(action) {
    var TableTemplate = "";
    switch (action) {

        //游戏设置                 
        //#region                  
        case "LotteryList":
            TableTemplate = {
                Title: "游戏列表",
                PageSize: 16,
                Url: "/admin/ajaxLottery.aspx?oper=ajaxGetLotteryList",
                StateUrl: "/admin/ajaxLottery.aspx?oper=ajaxStates",
                State2Url: "/admin/ajaxLottery.aspx?oper=ajaxAutoStates",
                Query: [],
                Botton: [],
                List: [
                        { Header: "彩票名称", Filed: "title", Width: "*", Align: "center" },
                        { Header: "下级玩法(个)", Filed: "childcount", Width: "*", Align: "center" },
                        { Header: "最小倍数", Filed: "mintimes", Width: "*", Align: "center" },
                        { Header: "最大倍数", Filed: "maxtimes", Width: "*", Align: "center" },
                        { Header: "封单时间", Filed: "closetime", Width: "*", Color: "Red", Align: "center" },
                        { Header: "是否启用", Filed: "isopen", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "开奖方式", Filed: "isauto", Width: "*", Align: "center", Default: [{ 0: "自动采集", 1: "手动开奖"}] },
                        { Header: "采集源", Filed: "autoname", Width: "*", Align: "center" },
                        { Header: "排序", Filed: "sort", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑游戏", Function: "PagePop('/admin/PageEditPop.aspx?page=LotterySave&id=@@')" },
                                    { Type: "Popup", Title: "玩法权限", Function: "top.Lottery.Popup.show('/admin/LotteryNoPlay.aspx?id=@@',1000,600,true)" },
                                ]
                        }]
            };
            break;
        case "LotteryPlayType":
            TableTemplate = {
                Title: "游戏分类",
                PageSize: 16,
                Url: "/admin/ajaxLottery.aspx?oper=ajaxGetPlayBigList",
                StateUrl: "/admin/ajaxLottery.aspx?oper=ajaxPlayBigStates",
                Query: [{ InputType: "select", InputTitle: "彩票名称", InputId: "type", InputClass: "sel sel-md", Width: "100px", Options: LotteryTypeJsonData}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "彩票名称", Filed: "typeid", Width: "*", Align: "center", Default: [{ 1: "时时彩类", 2: "11选5类", 3: "3DP3类", 4: "北京PK10"}] },
                        { Header: "游戏分类", Filed: "title", Width: "*", Align: "center" },
                        { Header: "下级玩法(个)", Filed: "childcount", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isopen", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "排序", Filed: "sort", Width: "*", Align: "center" },
                        { Header: "添加时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                     { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=LotteryTypeSave&id=@@')" }
                                ]
                        }]
            };
            break;
        case "LotteryPlay":
            TableTemplate = {
                Title: "游戏玩法",
                PageSize: 16,
                Url: "/admin/ajaxLottery.aspx?oper=ajaxGetPlaySmallList",
                StateUrl: "/admin/ajaxLottery.aspx?oper=ajaxPlaySmallStates",
                Query: [{ InputType: "select", InputTitle: "彩票名称", InputId: "type", InputClass: "sel sel-md", Width: "100px", OnChange: "LotteryPlayChange", Options: LotteryTypeJsonData },
                             { InputType: "select", InputTitle: "游戏分类", InputId: "play", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有分类"}]}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "游戏分类", Filed: "radioname", Width: "*", Align: "center" },
                        { Header: "游戏玩法", Filed: "title", Width: "*", Align: "center" },
                        { Header: "最大奖金", Filed: "maxbonus", Width: "*", Align: "center" },
                        { Header: "最小奖金", Filed: "minbonus", Width: "*", Align: "center" },
                        { Header: "混选组六奖金", Filed: "minbonus2", Width: "*", Align: "center" },
                        { Header: "风控注数", Filed: "minnum", Width: "*", Align: "center" },
                        { Header: "最大注数", Filed: "maxnum", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isopen", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "排序", Filed: "sort", Width: "*", Align: "center" },
                        { Header: "添加时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                     { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=LotteryPlaySave&id=@@')" }
                                ]
                        }]
            };
            break;
        case "LotteryTime":
            TableTemplate = {
                Title: "开奖时间",
                PageSize: 16,
                Url: "/admin/ajaxLottery.aspx?oper=ajaxGetTimeList",
                Query: [{ InputType: "select", InputTitle: "彩票名称", InputId: "type", InputClass: "sel sel-md", Width: "100px", Options: LotteryJsonData}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "彩票名称", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "sn", Width: "*", Align: "center" },
                        { Header: "开奖时间", Filed: "time", Width: "*", Align: "center" },
                        { Header: "添加时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                     { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=LotteryTimeSave&id=@@')" }
                                ]
                        }]
            };
            break;
        case "LotteryUrl":
            TableTemplate = {
                Title: "开奖地址",
                PageSize: 16,
                Url: "/admin/ajaxLottery.aspx?oper=ajaxGetAutoUrlList",
                Query: [{ InputType: "select", InputTitle: "彩票名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", Options: LotteryJsonData}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "彩票名称", Filed: "lname", Width: "*", Align: "center" },
                        { Header: "采集网站", Filed: "title", Width: "*", Align: "center" },
                        { Header: "采集地址", Filed: "url", Width: "*", Align: "left" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                     { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=LotteryUrlSave&id=@@')" }
                                ]
                        }]
            };
            break;
        case "LotteryDataList":
            TableTemplate = {
                Title: "开奖数据",
                PageSize: 16,
                Url: "/admin/ajaxLotterydata.aspx?oper=ajaxGetList",
                DelUrl: '/admin/ajaxLotterydata.aspx?oper=ajaxDel',
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "类型", InputId: "flag", InputClass: "sel sel-md", Width: "100px", Options: Lottery2JsonData },
                             { InputType: "select", InputTitle: "派奖情况", InputId: "sort", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部" }, { key: "0", value: "完成" }, { key: "1", value: "未完成"}] },
                             { InputType: "Input", InputTitle: "期号", InputId: "u", InputClass: "sel sel-md", Width: "70px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }
                            , { Title: "手动补单", Function: "PagePopUrl('lotteryDataAdd.aspx',800,365)", InputClass: "btn btn-primary", Parm: "flag" }
                            , { Title: "一键补单", Function: "LotteryDataGetNum()", InputClass: "btn btn-primary" }
                            , { Title: "全部派奖", Function: "LotteryDataPaiJiang()", InputClass: "btn btn-primary" }
                            , { Title: "选中派奖", Function: "LotteryDataPaiJiangTitle()", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },

                        { Header: "采种类别", Filed: "typename", Width: "*", Align: "center" },
                        { Header: "开奖期号", Filed: "title", Width: "*", Align: "center" },
                        { Header: "开奖号码", Filed: "number", Width: "*", Align: "center" },
                        { Header: "和值", Filed: "total", Width: "*", Align: "center" },
                        { Header: "原始号码", Filed: "numberall", Width: "*", Align: "center" },
                        { Header: "开奖时间", Filed: "opentime", Width: "*", Align: "center" },
                        { Header: "采集时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "未派奖记录", Filed: "flag", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                        }]
            };
            break;
            case "LotteryDataListNo":
            TableTemplate = {
                Title: "开奖数据",
                PageSize: 16,
                Url: "/admin/ajaxLotterydata.aspx?oper=ajaxGetListNo",
                DelUrl: '/admin/ajaxLotterydata.aspx?oper=ajaxDel',
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "期号", InputId: "u", InputClass: "sel sel-md", Width: "70px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }
                            , { Title: "选中派奖", Function: "LotteryDataPaiJiangTitle()", InputClass: "btn btn-primary"}],
                List: [ { Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "采种类别", Filed: "typename", Width: "*", Align: "center" },
                        { Header: "开奖期号", Filed: "title", Width: "*", Align: "center" },
                        { Header: "开奖号码", Filed: "number", Width: "*", Align: "center" },
                        { Header: "和值", Filed: "total", Width: "*", Align: "center" },
                        { Header: "原始号码", Filed: "numberall", Width: "*", Align: "center" },
                        { Header: "开奖时间", Filed: "opentime", Width: "*", Align: "center" },
                        { Header: "采集时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "未派奖记录", Filed: "flag", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                        }]
            };
            break;
        case "DnDataList":
            TableTemplate = {
                Title: "开奖数据",
                PageSize: 100,
                Url: "/admin/ajaxLotterydata.aspx?oper=ajaxGetDNList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "类型", InputId: "flag", InputClass: "sel sel-md", Width: "100px", Options: DNJsonData },
                             { InputType: "Input", InputTitle: "期号", InputId: "u", InputClass: "sel sel-md", Width: "70px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('斗牛数据')", InputClass: "btn btn-primary"}],
                List: [{ Header: "采种类别", Filed: "typename", Width: "*", Align: "center" },
                        { Header: "开奖期号", Filed: "title", Width: "*", Align: "center" },
                        { Header: "庄家", Filed: "number", Width: "*", Align: "center" },
                        { Header: "玩家+1", Filed: "number1", Width: "*", Align: "center" },
                        { Header: "玩家+2", Filed: "number2", Width: "*", Align: "center" },
                        { Header: "玩家+3", Filed: "number3", Width: "*", Align: "center" },
                        { Header: "玩家+4", Filed: "number4", Width: "*", Align: "center" },
                        { Header: "玩家+5", Filed: "number5", Width: "*", Align: "center" },
                        { Header: "中奖+1", Filed: "win1", Width: "*", Align: "center" },
                        { Header: "中奖+2", Filed: "win2", Width: "*", Align: "center" },
                        { Header: "中奖+3", Filed: "win3", Width: "*", Align: "center" },
                        { Header: "中奖+4", Filed: "win4", Width: "*", Align: "center" },
                        { Header: "中奖+5", Filed: "win5", Width: "*", Align: "center" },
                        { Header: "全买盈亏", Filed: "total", Width: "*", Align: "center", TwoColor: true },
                        { Header: "开奖时间", Filed: "opentime", Width: "*", Align: "center"}]
            };
            break;
          case "LotteryCheckList":
            TableTemplate = {
                Title: "自主彩设置",
                PageSize: 16,
                Url: "/admin/ajaxLottery.aspx?oper=ajaxGetLotteryCheckList",
                Query: [],
                Botton: [],
                List: [
                        { Header: "游戏名称", Filed: "name", Width: "*", Align: "center" },
                        { Header: "杀数个数", Filed: "checknum", Width: "*", Align: "center" },
                        { Header: "自动开启杀数比例（%）", Filed: "checkper", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=LotteryCheckSave&id=@@')" }
                                ]
                        }]
            };
            break;

        //#endregion                  
        //日志管理                 
        //#region                 
        case "LogException":
            TableTemplate = {
                Title: "异常日志",
                PageSize: 16,
                Url: "/admin/ajaxLogs.aspx?oper=ajaxGetExceptionList",
                ClearUrl: "/admin/ajaxLogs.aspx?oper=ajaxExceptionClear",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "类型", InputId: "sel2", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "程序异常", value: "程序异常" }, { key: "采集异常", value: "采集异常" }, { key: "派奖异常", value: "派奖异常"}] },
                             { InputType: "Input", InputTitle: "日志内容", InputId: "u", InputClass: "sel sel-md", Width: "100px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "清空记录", Function: "Confirmclear()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('异常日志')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "日志类型", Filed: "title", Width: "150", Align: "center" },
                        { Header: "日志内容", Filed: "content", Width: "*", Align: "left" },
                        { Header: "日志时间", Filed: "stime", Width: "150", Align: "center"}]
            };
            break;
        case "LogUserLogin":
            TableTemplate = {
                Title: "登录日志",
                PageSize: 16,
                Url: "/admin/ajaxLogs.aspx?oper=ajaxGetUserLoginList",
                ClearUrl: "/admin/ajaxLogs.aspx?oper=ajaxUserLoginClear",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                            { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                            { InputType: "select", InputTitle: "类型", InputId: "sel2", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部类型" }, { key: "1", value: "用户名" }, { key: "2", value: "登陆IP" }, { key: "3", value: "浏览器"}] },
                            { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "100px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "清空记录", Function: "Confirmclear()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('登录日志')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "用户名", Filed: "username", Width: "150", Align: "center" },
                        { Header: "登陆IP", Filed: "ip", Width: "100", Align: "center" },
                        { Header: "登陆详细地址", Filed: "address", Width: "*", Align: "left" },
                        { Header: "操作系统", Filed: "system", Width: "100", Align: "center" },
                        { Header: "浏览器", Filed: "browser", Width: "100", Align: "center" },
                        { Header: "登陆时间", Filed: "logintime", Width: "150", Align: "center" }
                       ]
            };
            break;
        case "LogSys":
            TableTemplate = {
                Title: "系统日志",
                PageSize: 16,
                Url: "/admin/ajaxLogs.aspx?oper=ajaxGetList",
                ClearUrl: "/admin/ajaxLogs.aspx?oper=clear",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "类型", InputId: "sel2", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部" }, { key: "系统活动", value: "系统活动" }, { key: "会员购彩", value: "会员购彩" }
                             , { key: "游戏派奖", value: "游戏派奖" }, { key: "游戏采集", value: "游戏采集" }, { key: "系统自动", value: "系统自动" }, { key: "会员管理", value: "会员管理"}]
                             }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "清空记录", Function: "Confirmclear()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('系统日志')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "日志类型", Filed: "title", Width: "150", Align: "center" },
                        { Header: "日志内容", Filed: "content", Width: "*", Align: "left" },
                        { Header: "日志时间", Filed: "stime", Width: "150", Align: "center" },
                       ]
            };
            break;
        case "LogAdmin":
            TableTemplate = {
                Title: "管理日志",
                PageSize: 16,
                Url: "/admin/ajaxLogs.aspx?oper=ajaxGetAdminList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                        { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                        { InputType: "select", InputTitle: "操作类型", InputId: "sel2", InputClass: "sel sel-md", Width: "100px", Options: AdminOperLog},
                        { InputType: "Input", InputTitle: "管理员", InputId: "admin", InputClass: "sel sel-md", Width: "100px"},
                        { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "100px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "清空记录", Function: "Confirmclear()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('管理日志')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "管理员", Filed: "adminname", Width: "*", Align: "center" },
                        { Header: "操作类型", Filed: "opertitle", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "操作内容", Filed: "operinfo", Width: "*", Align: "left" },
                        { Header: "操作IP", Filed: "operip", Width: "*", Align: "center" },
                        { Header: "操作时间", Filed: "opertime", Width: "*", Align: "center" }
                       ]
            };
            break;
        //#endregion                  
        //活动管理                 
        //#region                 
        //case "ActiveSet":
        //    TableTemplate = {
        //        Title: "活动配置",
        //        PageSize: 16,
        //        Url: "/admin/ajaxActive.aspx?oper=ajaxGetList",
        //        StateUrl: "/admin/ajaxActive.aspx?oper=ajaxStates",
        //        Query: [],
        //        Botton: [],
        //        List: [
        //                { Header: "活动名称", Filed: "name", Width: "*", Align: "center" },
        //                { Header: "开始时间", Filed: "starttime", Width: "*", Align: "center" },
        //                { Header: "截止时间", Filed: "endtime", Width: "*", Align: "center" },
        //                { Header: "派发方式", Filed: "typecode", Width: "*", Align: "center" },
        //                { Header: "是否启用", Filed: "isuse", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
        //                { Header: "", Width: "*", Align: "center"
        //                , Info: [
        //                            { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActiveSetSave&id=@@')" },
        //                             { Type: "Link", Title: "设置规则", Function: "/admin/conList.aspx?page=@Code@" },
        //                            { Type: "Popup", Title: "设置公告", Function: "PagePop('/admin/ActiveNewsedit.aspx?id=@@')" }
        //                        ]
        //                }]
        //    };
        //    break;
         case "ActDay15Fenhong":
            TableTemplate = {
                Title: "直属分红",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_Day15FHSet",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_Day15FHSet",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员级别", Filed: "groupname", Width: "*", Align: "center" },
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日平均量(万)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "直属分红(%)", Filed: "group3", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActDay15Fenhong&id=@@')" }
                                ]
                        }]
            };
            break;
          case "ActDayGongZi":
            TableTemplate = {
                Title: "日结工资",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_DayGzSet",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_DayGzSet",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员级别", Filed: "groupname", Width: "*", Align: "center" },
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日销量下限(万)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "日销量上限(万)", Filed: "maxmoney", Width: "*", Align: "center" },
                        { Header: "活跃人数(>=)", Filed: "minusers", Width: "*", Align: "center" },
                        { Header: "工资比例(%)", Filed: "money", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActDayGongZi&id=@@')" }
                                ]
                        }]
            };
            break;
          case "ActDayYongJin":
            TableTemplate = {
                Title: "亏损活动",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_DayYJSet",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_DayYJSet",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日销量(元)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "特权直属佣金(元)", Filed: "group3", Width: "*", Align: "center" },
                        { Header: "直属佣金(元)", Filed: "group2", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActDayYongJin&id=@@')" }
                                ]
                        }]
            };
            break;
        case "ActYongJin":
            TableTemplate = {
                Title: "亏损活动",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_SetYJDetail",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_SetYJDetail",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日销量(元)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "直属佣金(元)", Filed: "group3", Width: "*", Align: "center" },
                        { Header: "总代佣金(元)", Filed: "group2", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActYongJin&id=@@')" }
                                ]
                        }]
            };
            break;
        case "ActYongJin2":
            TableTemplate = {
                Title: "亏损活动",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_SetYJDetail2",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_SetYJDetail2",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日销量(元)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "上级(元)", Filed: "money", Width: "*", Align: "center" },
                        { Header: "上上级(元)", Filed: "money2", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActYongJin2&id=@@')" }
                                ]
                        }]
            };
            break;
        case "ActGongZi":
            TableTemplate = {
                Title: "日结工资",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_SetGZDetail",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_SetGZDetail",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日销量下限(万)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "日销量上限(万)", Filed: "maxmoney", Width: "*", Align: "center" },
                        { Header: "活跃人数(>=)", Filed: "minusers", Width: "*", Align: "center" },
                        { Header: "工资比例(%)", Filed: "money", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActGongZi&id=@@')" }
                                ]
                        }]
            };
            break;
        case "ActGongZi2":
            TableTemplate = {
                Title: "日结工资",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_SetGZDetail2",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_SetGZDetail2",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日销量下限(万)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "日销量上限(万)", Filed: "maxmoney", Width: "*", Align: "center" },
                        { Header: "活跃人数(>=)", Filed: "minusers", Width: "*", Align: "center" },
                        { Header: "工资比例(%)", Filed: "money", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActGongZi2&id=@@')" }
                                ]
                        }]
            };
            break;
        case "ActFenHong":
            TableTemplate = {
                Title: "直属分红",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActDetail&table=Act_SetFHDetail",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxActStates&table=Act_SetFHDetail",
                Query: [],
                Botton: [{ Title: "返回配置", Link: "/admin/conList.aspx?page=ActiveSet", InputClass: "btn btn-primary" }, { Title: "刷新数据", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "活动说明", Filed: "name", Width: "*", Align: "center" },
                        { Header: "日平均量(万)", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "直属分红(%)", Filed: "group3", Width: "*", Align: "center" },
                        { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/PageEditPop.aspx?page=ActFenHong&id=@@')" }
                                ]
                        }]
            };
            break;
        case "ActiveNewsList":
            TableTemplate = {
                Title: "活动公告",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetList",
                StateUrl: "/admin/ajaxActive.aspx?oper=ajaxIndexStates",
                Query: [],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "所属活动", Filed: "name", Width: "150", Align: "center" },
                        { Header: "公告标题", Filed: "title", Width: "*", Align: "center" },
                        { Header: "发布时间", Filed: "stime", Width: "150", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑属性", Function: "PagePop('/admin/ActiveNewsedit.aspx?id=@@')" }
                                ]
                        }]
            };
            break;
        case "ActiveRecord":
            TableTemplate = {
                Title: "活动记录",
                PageSize: 16,
                Url: "/admin/ajaxActive.aspx?oper=ajaxGetActiveRecord",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "类型", InputId: "type", InputClass: "sel sel-md", Width: "100px", Options: ActiveJsonData },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('活动记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "150", Align: "center" },
                        { Header: "活动名称", Filed: "activename", Width: "*", Align: "center" },
                        { Header: "消费/亏损", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "领取金额", Filed: "inmoney", Width: "150", Align: "center" },
                        { Header: "领取时间", Filed: "stime", Width: "150", Align: "center" },
                        { Header: "领取者IP", Filed: "checkip", Width: "150", Align: "center" },
                        { Header: "机器码", Filed: "checkmachine", Width: "150", Align: "center" },
                        { Header: "备注", Filed: "remark", Width: "150", Align: "center" },
                       ]
            };
            break;
        case "AgentFHRecord":
            TableTemplate = {
                Title: "分红记录",
                PageSize: 16,
                Url: "/admin/ajaxAgentFH.aspx?oper=ajaxGetAgentFHRecord",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                            { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                            { InputType: "select", InputTitle: "分红类型", InputId: "code", InputClass: "sel sel-md", Width: "100px", Options: AgentFHTypeJsonData },
                            { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('分红记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "代理账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "分红类型", Filed: "agentid", Width: "*", Align: "center", Default: [{ 3: "特权分红", 4: "招商分红", 99: "契约分红", 0: ""}] },
                        { Header: "开始时间", Filed: "starttime", Width: "150", Align: "center" },
                        { Header: "结束时间", Filed: "endtime", Width: "150", Align: "center" },
                        { Header: "投注", Filed: "bet", Width: "150", Align: "center" },
                        { Header: "亏损", Filed: "total", Width: "150", Align: "center" },
                        { Header: "分红比例(%)", Filed: "per", Width: "150", Align: "center" },
                        { Header: "分红金额", Filed: "inmoney", Width: "150", Align: "center" },
                        { Header: "分红时间", Filed: "stime", Width: "150", Align: "center" },
                         { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Filed: "agentid", FiledValue:"99", Title: "契约详情", Function: "LayerPop('契约','/admin/conList.aspx?page=UserContractDetail2&id=@UserId@')" }
                                ]
                         }
                       ]
            };
            break;
        case "ChargeActList":
            TableTemplate = {
                Title: "首充活动",
                PageSize: 16,
                Url: "/admin/ajaxCharge.aspx?oper=ajaxGetActChargeList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "充值类型", InputId: "bank", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", OptUrl: "/admin/ajaxSave.aspx?oper=OptionsInfo&t=Sys_ChargeSet&n='MerName'" },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('充值记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "200px", Align: "center" },
                        { Header: "充值类型", Filed: "bankname", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "充值金额", Filed: "inmoney", Width: "*", Align: "center" },
                        { Header: "充值状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "充值时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "checkcode", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Filed: "actstate", FiledValue:"0", Title: "审核", Function: "top.Lottery.Popup.show('/admin/userChargeAct.aspx?id=@@',600,300,true)" }
                                  ]
                        }
                      ]
            };
            break;
        //#endregion                  
        //管理账号                 
        //#region                 
        case "AdminRolelist":
            TableTemplate = {
                Title: "角色管理",
                PageSize: 16,
                Url: "/admin/ajaxAdmin.aspx?oper=ajaxGetRoleList",
                DelUrl: "/admin/ajaxAdmin.aspx?oper=ajaxRoleDel",
                StateUrl: "/admin/ajaxAdmin.aspx?oper=ajaxRoleStates",
                Query: [],
                Botton: [{ Title: "添加角色", Function: "PagePop('/admin/PageSavePop.aspx?page=RoleSave')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "角色名称", Filed: "name", Width: "*", Align: "center" },
                        { Header: "排序", Filed: "sort", Width: "*", Align: "center" },
                        { Header: "启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "禁用"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "分配权限", Function: "top.Lottery.Popup.show('/admin/adminPower.aspx?id=@@',1000,600,true)" },
                                    { Type: "Popup", Title: "编辑", Function: "top.Lottery.Popup.show('/admin/PageEditPop.aspx?page=RoleSave&id=@@',600,350,true)" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                        }]
            };
            break;
        case "AdminList":
            TableTemplate = {
                Title: "管理员列表",
                PageSize: 16,
                Url: "/admin/ajaxAdmin.aspx?oper=ajaxGetList",
                StateUrl: "/admin/ajaxAdmin.aspx?oper=ajaxStates",
                DelUrl: "/admin/ajaxAdmin.aspx?oper=ajaxDel",
                Query: [],
                Botton: [{ Title: "添加管理员", Function: "PagePop('/admin/PageSavePop.aspx?page=AdminSave')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "角色", Filed: "rolename", Width: "*", Align: "center" },
                        { Header: "最后登录IP", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后登录时间", Filed: "logintime", Width: "*", Align: "center" },
                        { Header: "在线", Filed: "isonline", Width: "*", Align: "center", Default: [{ 1: "在线", 0: "离线"}] },
                        { Header: "启用", Filed: "flag", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "禁用"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center", FiledSuper: "IsSuper"
                        , Info: [
                                    { Type: "Popup", Title: "重置密码", Function: "PagePop('/admin/PageEditPop.aspx?page=AdminEdit&id=@@')" },
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=AdminSave&id=@@')" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                        }]
            };
            break;
        case "AdminGashList":
            TableTemplate = {
                Title: "财务账号列表",
                PageSize: 16,
                Url: "/admin/ajaxAdmin.aspx?oper=ajaxGetList2",
                DelUrl: "/admin/ajaxAdmin.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxAdmin.aspx?oper=ajaxStates",
                Query: [],
                Botton: [{ Title: "添加管理员", Function: "PagePop('/admin/PageSavePop.aspx?page=AdminGashSave')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "角色", Filed: "rolename", Width: "*", Align: "center" },
                        { Header: "最后登录IP", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "最后登录时间", Filed: "logintime", Width: "*", Align: "center" },
                        { Header: "最小取款", Filed: "mincash", Width: "*", Align: "center" },
                        { Header: "最大取款", Filed: "maxcash", Width: "*", Align: "center" },
                        { Header: "在线", Filed: "isonline", Width: "*", Align: "center", Default: [{ 1: "在线", 0: "离线"}] },
                        { Header: "启用", Filed: "flag", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "禁用"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "重置密码", Function: "PagePop('/admin/PageEditPop.aspx?page=AdminEdit&id=@@',600,350,true)" },
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=AdminGashSave&id=@@')" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                        }]
            };
            break;
        //#endregion                  
        //银行信息                 
        //#region                 
        case "ChargeSet":
            TableTemplate = {
                Title: "充值配置",
                PageSize: 16,
                Url: "/admin/ajaxCharge.aspx?oper=ajaxChargeSetList",
                StateUrl: "/admin/ajaxCharge.aspx?oper=ajaxChargeSetStates",
                Query: [],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "第三方名称", Filed: "name", Width: "*", Align: "center" },
                        { Header: "前台名称", Filed: "mername", Width: "*", Align: "center" },
                        { Header: "商户ID", Filed: "mercode", Width: "*", Align: "center" },
                        { Header: "最小充值", Filed: "mincharge", Width: "*", Align: "center" },
                        { Header: "最大充值", Filed: "maxcharge", Width: "*", Align: "center" },
                        { Header: "充值开始时间", Filed: "starttime", Width: "*", Align: "center" },
                        { Header: "充值结束时间", Filed: "endtime", Width: "*", Align: "center" },
                        { Header: "每日限额", Filed: "total", Width: "*", Align: "center" },
                        { Header: "充值开关", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "排序", Filed: "sort", Width: "*", Align: "center" },
                        { Header: "添加时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=ChargeSetSave&id=@@')" }
                                  ]
                        }]
            };
            break;
        case "ChargeBankList":
            TableTemplate = {
                Title: "银行配置",
                PageSize: 16,
                Url: "/admin/ajaxSysBank.aspx?oper=ajaxGetList&flag=0",
                StateUrl: "/admin/ajaxSysBank.aspx?oper=ajaxStates",
                State2Url: "/admin/ajaxSysBank.aspx?oper=ajaxStates2",
                Query: [],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "银行", Filed: "bank", Width: "*", Align: "center" },
                        { Header: "姓名", Filed: "name", Width: "*", Align: "center" },
                        { Header: "账号", Filed: "account", Width: "*", Align: "left" },
                        { Header: "最小充值", Filed: "mincharge", Width: "*", Align: "center" },
                        { Header: "最大充值", Filed: "maxcharge", Width: "*", Align: "center" },
                        { Header: "充值开始时间", Filed: "starttime", Width: "*", Align: "center" },
                        { Header: "充值结束时间", Filed: "endtime", Width: "*", Align: "center" },
                        { Header: "充值开关", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=SysBankSave&id=@@')" }
                                  ]
                        }]
            };
            break;
        case "GetCashBankList":
            TableTemplate = {
                Title: "银行配置",
                PageSize: 16,
                Url: "/admin/ajaxSysBank.aspx?oper=ajaxGetList&flag=1",
                StateUrl: "/admin/ajaxSysBank.aspx?oper=ajaxStates",
                State2Url: "/admin/ajaxSysBank.aspx?oper=ajaxStates2",
                Query: [],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "银行", Filed: "bank", Width: "*", Align: "center" },
                        { Header: "最小取款金额", Filed: "mincharge", Width: "*", Align: "center" },
                        { Header: "最大取款金额", Filed: "maxcharge", Width: "*", Align: "center" },
                        { Header: "每天取款次数", Filed: "maxgetcash", Width: "*", Align: "center" },
                        { Header: " 取款消费限制(%)", Filed: "betpercheck", Width: "*", Align: "center" },
                        { Header: "取款开始时间", Filed: "starttime", Width: "*", Align: "center" },
                        { Header: "取款结束时间", Filed: "endtime", Width: "*", Align: "center" },
                        { Header: "绑定多久可取款(小时)", Filed: "bindtime", Width: "*", Align: "center" },
                        { Header: "取款开关", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "启用", 1: "关闭"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=SysGetCashBankSave&id=@@')" }
                                  ]
                        }]
            };
            break;
        case "ChargeList":
            TableTemplate = {
                Title: "充值记录",
                PageSize: 16,
                Url: "/admin/ajaxCharge.aspx?oper=ajaxGetList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "充值类型", InputId: "bank", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", OptUrl: "/admin/ajaxSave.aspx?oper=OptionsInfo&t=Sys_ChargeSet&n='MerName'" },
                              { InputType: "select", InputTitle: "充值状态", InputId: "state", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "待支付" }, { key: "1", value: "已完成"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "ssid", value: "订单编号"}, { key: "checkcode", value: "备注信息"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                              { InputType: "Input", InputTitle: "金额大于", InputId: "money", InputClass: "sel sel-md", Width: "80px", Keyup: "on" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('充值记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "200px", Align: "center" },
                        { Header: "充值类型", Filed: "bankname", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "充值金额", Filed: "inmoney", Width: "*", Align: "center" },
                        { Header: "到账金额", Filed: "dzmoney", Width: "*", Align: "center" },
                        { Header: "充值状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "充值时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "checkcode", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Filed: "state", FiledValue:"0",Title: "补单", Function: "top.Lottery.Popup.show('/admin/userchargeedit.aspx?id=@@',600,300,true)" }
                                  ]
                        }
                      ]
            };
            break;
            case "ChargeListNo":
            TableTemplate = {
                Title: "充值问题记录",
                PageSize: 16,
                Url: "/admin/ajaxCharge.aspx?oper=ajaxGetListOfNo",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "充值类型", InputId: "bank", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", OptUrl: "/admin/ajaxSave.aspx?oper=OptionsInfo&t=Sys_ChargeSet&n='MerName'" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('充值问题记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "200px", Align: "center" },
                        { Header: "充值类型", Filed: "bankname", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "充值金额", Filed: "inmoney", Width: "*", Align: "center" },
                        { Header: "充值状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "充值时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "checkcode", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Filed: "state", FiledValue:"0", Title: "补单", Function: "top.Lottery.Popup.show('/admin/userchargeedit.aspx?id=@@',600,300,true)" }
                                  ]
                        }
                      ]
            };
            break;
        case "TranAccList":
            TableTemplate = {
                Title: "转账记录",
                PageSize: 16,
                Url: "/admin/ajaxCharge.aspx?oper=ajaxGetTranAccList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "UserId", value: "转出账号" }, { key: "ToUserId", value: "转入账号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                              { InputType: "Input", InputTitle: "金额大于", InputId: "money", InputClass: "sel sel-md", Width: "80px", Keyup: "on" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('转账记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "200px", Align: "center" },
                        { Header: "转出账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "转入账号", Filed: "tousername", Width: "*", Align: "center" },
                        { Header: "转账金额", Filed: "moneychange", Width: "*", Align: "center" },
                        { Header: "转账时间", Filed: "stime", Width: "*", Align: "center" },
                      ]
            };
            break;
        case "CashCheck":
            TableTemplate = {
                Title: "取款审核",
                PageSize: 16,
                Url: "/admin/ajaxCharge.aspx?oper=ajaxGetCashCheck",
                StateUrl: "/admin/ajaxCharge.aspx?oper=ajaxStates",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "ssid", value: "订单编号" }, { key: "PayName", value: "取款姓名" }, { key: "PayAccount", value: "取款卡号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "money", value: "取款金额大于" }, { key: "bet", value: "当天销量大于"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u2", InputClass: "sel sel-md", Width: "80px", Keyup: "on" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('取款申请')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "200px", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                         { Header: "取款金额", Filed: "cashmoney", Width: "*", Align: "center" },
                        { Header: "银行", Filed: "paybank", Width: "*", Align: "center" },
                        { Header: "户名", Filed: "payname", Width: "*", Align: "center" },
                        { Header: "账号", Filed: "payaccount", Width: "*", Align: "center" },
                           { Header: "当天销量", Filed: "bet", Width: "*", Align: "center" },
                        { Header: "申请时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "忽略提示", Filed: "state", Width: "*", Align: "center", Default: [{ 99: "已忽略", 0: "忽略提示"}], Function: "ajaxStates" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "处理取款", Function: "top.Lottery.Popup.show('/admin/usercashedit.aspx?id=@@',800,500,true)" },
                                       { Type: "Popup", Title: "账变信息", Function: "LayerPop('账变信息','/admin/conList.aspx?page=UserHistoryPop&id=@@')" },
                                       { Type: "Popup", Title: "卡号变动", Function: "LayerPop('卡号变动','/admin/conList.aspx?page=UserBankPop&id=@UserId@')" }
                                  ]
                        }
                       ]
            };
            break;
        case "CashList":
            TableTemplate = {
                Title: "取款记录",
                PageSize: 16,
                Url: "/admin/ajaxCharge.aspx?oper=ajaxGetCashList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "ssid", value: "订单编号" }, { key: "PayName", value: "取款姓名" }, { key: "PayAccount", value: "取款卡号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "取款状态", InputId: "issoft", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "未处理" }, { key: "1", value: "已经放款" }, { key: "2", value: "拒绝放款"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "money", value: "取款金额大于" }, { key: "bet", value: "当天销量大于"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u2", InputClass: "sel sel-md", Width: "80px", Keyup: "on" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('取款记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "200px", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "取款金额", Filed: "cashmoney", Width: "*", Align: "center" },
                        { Header: "可用金额", Filed: "money", Width: "*", Align: "center" },
                        { Header: "当天销量", Filed: "bet", Width: "*", Align: "center" },
                        { Header: "银行", Filed: "paybank", Width: "*", Align: "center" },
                        { Header: "户名", Filed: "payname", Width: "*", Align: "center" },
                        { Header: "账号", Filed: "payaccount", Width: "*", Align: "center" },
                        { Header: "申请时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "处理时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "msg", Width: "*", Align: "center" }
                       ]
            };
            break;
        case "IpsList":
            TableTemplate = {
                Title: "第三方流水",
                PageSize: 16,
                Url: "/admin/ajaxPayRecord.aspx?oper=ajaxGetIpsList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "第三方名称", InputId: "code", InputClass: "sel sel-md", Width: "120px", Options: [{ key: "", value: "全部" }, { key: "1001", value: "充值线路1" }, { key: "1002", value: "充值线路2" }, { key: "1003", value: "充值线路3" }, { key: "1004", value: "充值线路4"}] },
                             { InputType: "Input", InputTitle: "平台订单号", InputId: "payrequestid", InputClass: "sel sel-md", Width: "135px" },
                             { InputType: "Input", InputTitle: "支付流水号", InputId: "ipsrequestid", InputClass: "sel sel-md", Width: "135px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('第三方流水')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "第三方名称", Filed: "payname", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "平台订单号", Filed: "payrequestid", Width: "*", Align: "center" },
                        { Header: "支付流水号", Filed: "ipsrequestid", Width: "*", Align: "center" },
                        { Header: "金额", Filed: "payamount", Width: "*", Align: "left" },
                        { Header: "状态", Filed: "paystate", Width: "*", Align: "center" },
                        { Header: "订单时间", Filed: "ipscompletestime", Width: "*", Align: "center" },
                        { Header: "充值时间", Filed: "paystime", Width: "*", Align: "center" }
                       ]
            };
            break;
        //#endregion                  
        //报表管理                 
        //#region                 
        case "MoneyStatAll":
            TableTemplate = {
                Title: "全局报表",
                PageSize: 16,
                Page: false,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetList",
                Url2: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListTop10",
                Url3: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetAllInfo",
                Query: [],
                Botton: [],
                List: [
                        { Header: "统计时间", Filed: "name", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}],
                  List2: [
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true },
                        { Header: "账户余额", Filed: "usermoney", Width: "*", Align: "right", TwoColor: true },
                        { Header: "中奖次数", Filed: "winnum", Width: "*", Align: "center", TwoColor: true}],
                     List3: [
                        { Header: "统计时间", Filed: "date", Width: "*", Align: "center" },
                        { Header: "今日注册", Filed: "regtoday", Width: "*", Align: "center" },
                        { Header: "昨日注册", Filed: "regyesterday", Width: "*", Align: "center" },
                        { Header: "本月注册", Filed: "regmonth", Width: "*", Align: "center" },
                        { Header: "总共人数", Filed: "sum", Width: "*", Align: "center" },
                        { Header: "在线人数", Filed: "sumonline", Width: "*", Align: "center" },
                        { Header: "手机在线", Filed: "sumiosonline", Width: "*", Align: "center" },
                        { Header: "电脑在线", Filed: "sumpconline", Width: "*", Align: "center" },
                        { Header: "目前余额", Filed: "money", Width: "*", Align: "center" }],
            };
            break;
        case "MoneyStatOfDay":
            TableTemplate = {
                Title: "每日报表",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListDay",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(-30) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "统计方式", InputId: "code", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "0", value: "个人" }, { key: "1", value: "团队" }] },
                             { InputType: "Input", InputTitle: "会员账号(输入查具体会员)", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "100px", Options: MoneyStatOrderData }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('每日报表')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "统计时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        case "MoneyStatOfRank":
            TableTemplate = {
                Title: "排行报表",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListRank",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "100px", Options: MoneyStatOrderData },
                             { InputType: "select", InputTitle: "排序方式", InputId: "orderby", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "asc", value: "升序" }, { key: "desc", value: "降序"}] }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('排行报表')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "撤单", Filed: "cancellation", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true },
                        { Header: "账户余额", Filed: "usermoney", Width: "*", Align: "right", TwoColor: true },
                        { Header: "中奖次数", Filed: "winnum", Width: "*", Align: "right", TwoColor: true}],
                SumList: [
                        { Header: "会员账号", Filed: "", Title: "全部合计", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true },
                        { Header: "账户余额", Filed: "usermoney", Width: "*", Align: "right", TwoColor: true },
                        { Header: "中奖次数", Filed: "winnum", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        case "MoneyStatOfLottery":
            TableTemplate = {
                Title: "彩种报表",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListLottery",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1)}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('彩种报表')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "游戏名称", Filed: "title", Width: "*", Align: "center" },
                        { Header: "投注金额", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "投注注数", Filed: "num", Width: "*", Align: "right" },
                        { Header: "中奖金额", Filed: "win", Width: "*", Align: "right" },
                        { Header: "中奖注数", Filed: "winnum", Width: "*", Align: "right" },
                        { Header: "中奖率(%)", Filed: "per", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "实际盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true }]
            };
            break;
        case "MoneyStatOfIuss":
            TableTemplate = {
                Title: "每期报表",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListIuss",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "彩票名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", Options: LotteryJsonData },
                             { InputType: "Input", InputTitle: "游戏期号", InputId: "u", InputClass: "sel sel-md", Width: "100px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('每期报表')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "游戏玩法", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "游戏期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "投注金额", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "投注注数", Filed: "num", Width: "*", Align: "right" },
                        { Header: "中奖金额", Filed: "win", Width: "*", Align: "right" },
                        { Header: "中奖注数", Filed: "winnum", Width: "*", Align: "right" },
                        { Header: "中奖率(%)", Filed: "per", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "实际盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true }]
            };
            break;
        case "MoneyStatOfPlay":
            TableTemplate = {
                Title: "玩法报表",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListPlay",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "彩票名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", Options: LotteryJsonData }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                         { Title: "导出", Function: "Table2Excel('玩法报表')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "游戏名称", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "游戏玩法", Filed: "title", Width: "*", Align: "center" },
                        { Header: "投注金额", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "投注注数", Filed: "num", Width: "*", Align: "right" },
                        { Header: "中奖金额", Filed: "win", Width: "*", Align: "right" },
                        { Header: "中奖注数", Filed: "winnum", Width: "*", Align: "right" },
                        { Header: "中奖率(%)", Filed: "per", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "实际盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true }],
            };
            break;
        case "UserProfitloss":
            TableTemplate = {
                Title: "个人统计",
                PageSize: 16,
                Url: "/admin/ajaxProfitloss.aspx?oper=ajaxGetProList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('个人统计')", InputClass: "btn btn-primary"}],
                List: [{ Header: "会员账号", Filed: "username", Filed2: "chindcount", Width: "*", Align: "left", Function: "ajaxSearchById" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        case "UserTeamProfitloss":
            TableTemplate = {
                Title: "团队统计",
                PageSize: 16,
                Url: "/admin/ajaxProfitloss.aspx?oper=ajaxGetProListTeam",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('团队统计')", InputClass: "btn btn-primary"}],
                List: [{ Header: "会员账号", Filed: "username", Filed2: "chindcount", Width: "*", Align: "left", Function: "ajaxSearchById" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        case "MoneyStatOfTeamSale":
            TableTemplate = {
                Title: "团队销量",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListTeamSale",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('团队销量')", InputClass: "btn btn-primary"}],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "返点级别", Filed: "userpoint", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "center" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "center" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "center" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "center" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "center" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "center" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "center", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "center", TwoColor: true}]
            };
            break;
      case "UserListCheck":
            TableTemplate = {
                Title: "代理审核",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListCheck",
                Query: [
                        { InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                        { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                        { InputType: "select", InputTitle: "类型", InputId: "group", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                        { InputType: "Input", InputTitle: "返点", InputId: "point", InputClass: "ipt", Width: "80px", Keyup: "on" },
                        { InputType: "select", InputTitle: "", InputId: "sel1", InputClass: "sel sel-md", Width: "120px", Options: [{ key: "0", value: "销量小于" },{ key: "1", value: "销量大于" }] },
                        { InputType: "Input", InputTitle: "", InputId: "bet", InputClass: "ipt", Width: "80px", Keyup: "on" },
                        { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "ipt", Width: "80px" }
                        ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                { Title: "导出", Function: "Table2Excel('代理审核')", InputClass: "btn btn-primary"},
                { Title: "批量编辑", Function: "ajaxAllUpdatePoint()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "会员Id", Filed: "id", Width: "*", Align: "center"},
                        { Header: "会员账号", Filed: "username", Width: "*",Color: "Red", Align: "center" , Function: "ajaxSearchById" },
                        { Header: "会员类型", Filed: "usergroupname", Width: "*", Align: "center"},
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "消费量", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "注册时间", Filed: "regtime", Width: "*", Align: "center" },
                        { Header: "开始时间", Filed: "starttime", Width: "*", Align: "center" },
                        { Header: "截止时间", Filed: "endtime", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑返点", Function: "top.Lottery.Popup.show('userUpdatePoint.aspx?id=@@',600,400,true)" }
                                ]
                        }]
            };
            break;
        case "UserHistory":
            TableTemplate = {
                Title: "资金流水",
                PageSize: 16,
                Url: "/admin/ajaxHistory.aspx?oper=ajaxGetList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "流水类别", InputId: "tid", InputClass: "sel sel-md", Width: "100px", Options: HistoryJsonData },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Uname", value: "会员账号" }, { key: "ssid", value: "流水号"}, { key: "Remark", value: "备注信息"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "200px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('资金流水')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "流水号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "uname", Width: "*", Align: "center" },
                        { Header: "流水时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "流水类型", Filed: "codename", Width: "*", Align: "center" },
                        { Header: "游戏", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "singlemoney", Width: "*", Align: "center" },
                        { Header: "收入/支出", Filed: "moneychange", Width: "*", Align: "right", TwoColor: true },
                        { Header: "余额", Filed: "moneyafter", Width: "*", Align: "right" },
                        { Header: "备注", Filed: "remark", Width: "*", Align: "center"}]
            };
            break;
        case "UserHistoryRG":
            TableTemplate = {
                Title: "资金流水",
                PageSize: 16,
                Url: "/admin/ajaxHistory.aspx?oper=ajaxGetListRG",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Uname", value: "会员账号" }, { key: "ssid", value: "流水号"}, { key: "Remark", value: "备注信息"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "200px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('资金流水')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "流水号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "uname", Width: "*", Align: "center" },
                        { Header: "流水时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "流水类型", Filed: "codename", Width: "*", Align: "center" },
                        { Header: "游戏", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "singlemoney", Width: "*", Align: "center" },
                        { Header: "收入/支出", Filed: "moneychange", Width: "*", Align: "right", TwoColor: true },
                        { Header: "余额", Filed: "moneyafter", Width: "*", Align: "right" },
                        { Header: "备注", Filed: "remark", Width: "*", Align: "center"}]
            };
            break;
        case "UserHistoryPop":
            TableTemplate = {
                Title: "资金流水",
                PageSize: 16,
                Url: "/admin/ajaxHistory.aspx?oper=ajaxGetList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "流水类别", InputId: "tid", InputClass: "sel sel-md", Width: "100px", Options: HistoryJsonData}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员账号", Filed: "uname", Width: "*", Align: "center" },
                        { Header: "流水时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "流水类型", Filed: "codename", Width: "*", Align: "center" },
                        { Header: "游戏", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "singlemoney", Width: "*", Align: "center" },
                        { Header: "收入/支出", Filed: "moneychange", Width: "*", Align: "right", TwoColor: true },
                        { Header: "余额", Filed: "moneyafter", Width: "*", Align: "right" },
                        { Header: "备注", Filed: "remark", Width: "*", Align: "center"}]
            };
            break;
            case "MoneyStatOfMonth":
            TableTemplate = {
                Title: "每月报表",
                PageSize: 16,
                Url: "/admin/ajaxMoneyStatAll.aspx?oper=ajaxGetListMonth",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(-600) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "统计方式", InputId: "code", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "0", value: "个人" }, { key: "1", value: "团队" }] },
                             { InputType: "Input", InputTitle: "会员账号(输入查具体会员)", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "100px", Options: MoneyStatOrderData }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('每日报表')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "统计时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "分红", Filed: "agentfh", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        //#endregion                       
        //报警信息                  
        //#region 
        case "UserRepeatHistory":
            TableTemplate = {
                Title: "重复记录",
                PageSize: 16,
                Url: "/admin/ajaxHistory.aspx?oper=ajaxGetRepeatList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "重复类别", InputId: "tid", InputClass: "sel sel-md", Width: "100px", Options: HistoryRepeatJsonData }],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('重复记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "流水号", Filed: "ssid", Width: "*", Align: "center", Link: "ssid" },
                        { Header: "会员账号", Filed: "uname", Width: "*", Align: "center" },
                        { Header: "流水时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "流水类型", Filed: "codename", Width: "*", Align: "center" },
                        { Header: "游戏", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "singlemoney", Width: "*", Align: "center" },
                        { Header: "收入/支出", Filed: "moneychange", Width: "*", Align: "right", TwoColor: true },
                        { Header: "余额", Filed: "moneyafter", Width: "*", Align: "right" },
                        { Header: "备注", Filed: "remark", Width: "*", Align: "center"}]
            };
            break;                 
        case "BetOfWinWarn":
            TableTemplate = {
                Title: "中奖报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxBetOfWinWarn",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                              { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "stime2", value: "投注时间" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('中奖报警')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },

                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "采种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "left" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "单注", Filed: "singlemoney", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "代购", Filed: "total", Width: "*", Align: "center" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center", Color: "Red" },
                        { Header: "实际盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,525,true)" }
                                  ]
                        }]
            };
            break;
        case "BetOfPointWarn":
            TableTemplate = {
                Title: "返点报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxBetOfPointWarn",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                              { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "stime2", value: "投注时间" }, { key: "PointMoney", value: "返点金额" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('返点报警')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },

                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "采种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "left" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "单注", Filed: "singlemoney", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "代购", Filed: "total", Width: "*", Align: "center" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "center", Color: "Red" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "实际盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,525,true)" }
                                  ]
                        }]
            };
            break;
        case "StatOfRealWarn":
            TableTemplate = {
                Title: "盈利报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxStatOfRealWarn",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('盈利报警')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "报警日期", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        case "StatOfActiveWarn":
            TableTemplate = {
                Title: "活动报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxStatOfActiveWarn",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('活动报警')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "报警日期", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        case "StatOfFhWarn":
            TableTemplate = {
                Title: "分红报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxStatOfFhWarn",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('分红报警')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "报警日期", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        case "BetOfYLLWarn":
            TableTemplate = {
                Title: "盈利率报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxBetOfYLLWarn",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "stime2", value: "投注时间" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('盈利率报警')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },

                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "采种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "left" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "单注", Filed: "singlemoney", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "代购", Filed: "total", Width: "*", Align: "center" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center", Color: "Red" },
                        { Header: "实际盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,525,true)" }
                                  ]
                        }]
            };
            break;
        case "UserOfIpWarn":
            TableTemplate = {
                Title: "同IP报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxUserOfIpWarn",
                Query: [{ InputType: "Input", InputTitle: "会员账号", InputId: "uname", InputClass: "ipt", Width: "100px" },
                             { InputType: "Input", InputTitle: "IP", InputId: "ip", InputClass: "ipt", Width: "100px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('同IP报警')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                         { Header: "类型", Filed: "usergroupname", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Filed2: "childnum", Width: "*", Align: "center", Function: "ajaxSearchById" },
                        { Header: "上级账号", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Color: "Red", Align: "right" },
                        { Header: "账号积分", Filed: "score", Width: "*", Align: "right" },
                        { Header: "最后访问", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "账号状态", Filed: "isenable", Width: "*", Align: "center", Default: [{ 0: "正常", 1: "锁定"}], Function: "ajaxStates" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线"}] },
                        { Header: "绑定银行", Filed: "bank", Width: "*", Align: "center", Default: [{ 0: "未绑定", 1: "绑定"}] },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "top.Lottery.Popup.show('useredit.aspx?id=@@',1000,600,true)" }
                                ]
                        }]
            };
            break;
        case "GetCashWarnTotal":
            TableTemplate = {
                Title: "取款报警记录",
                PageSize: 16,
                Url: "/admin/ajaxWarn.aspx?oper=ajaxGetCashWarn",
                StateUrl: "/admin/ajaxCharge.aspx?oper=ajaxStates",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "ssid", value: "订单编号" }, { key: "PayName", value: "取款姓名" }, { key: "PayAccount", value: "取款卡号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "money", value: "取款金额大于" }, { key: "bet", value: "当天销量大于"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u2", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('取款报警')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                         { Header: "取款金额", Filed: "cashmoney", Width: "*", Align: "center" },
                        { Header: "可用金额", Filed: "money", Width: "*", Align: "center" },
                        { Header: "当天销量", Filed: "bet", Width: "*", Align: "center" },
                        { Header: "银行", Filed: "paybank", Width: "*", Align: "center" },
                        { Header: "户名", Filed: "payname", Width: "*", Align: "center" },
                        { Header: "账号", Filed: "payaccount", Width: "*", Align: "center" },
                        { Header: "申请时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "忽略提示", Filed: "state", Width: "*", Align: "center", Default: [{ 99: "已忽略", 0: "忽略提示"}], Function: "ajaxStates"}]
            };
            break;
        //#endregion                       
        //游戏记录                  
        //#region                  
        case "BetListOfNO":
            TableTemplate = {
                Title: "未开奖记录",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetList&state=0&IsCheat=0",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "IssueNum", value: "购买期号"}, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                         { Title: "加入改单", Function: "ajaxBetCheat()", InputClass: "btn btn-primary" },
                         { Title: "选中派奖", Function: "ajaxPaiJiangBetId()", InputClass: "btn btn-primary" },
                         { Title: "选中撤单", Function: "ajaxBetCancel()", InputClass: "btn btn-primary" },
                         { Title: "导出", Function: "Table2Excel('未开奖记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "投注", Filed: "total", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "异常状态", Filed: "warnstate", Width: "*", Align: "center" },
                        { Header: "来源", Filed: "source", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,600,true)" }
                                  ]
                        }]
            };
            break;
        case "BetListOfUpdate":
            TableTemplate = {
                Title: "改单记录",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetList&IsCheat=1&State=0",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "IssueNum", value: "购买期号"}, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Id", value: "投注时间" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                         { Title: "选中派奖", Function: "ajaxPaiJiangBetId()", InputClass: "btn btn-primary" },
                         { Title: "选中撤单", Function: "ajaxBetCancel()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('改单记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "投注", Filed: "total", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "异常状态", Filed: "warnstate", Width: "*", Align: "center" },
                        { Header: "来源", Filed: "source", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "编辑", Function: "top.Lottery.Popup.show('/admin/betUpdate.aspx?id=@@',800,600,true)" }
                                  ]
                        }]
            };
            break;
        case "BetListOfWin":
            TableTemplate = {
                Title: "中奖记录",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetList&state=3",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "IssueNum", value: "购买期号"}, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Id", value: "投注时间" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('中奖记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "投注", Filed: "total", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "异常状态", Filed: "warnstate", Width: "*", Align: "center" },
                        { Header: "来源", Filed: "source", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,600,true)" }
                                  ]
                        }]
            };
            break;
        case "BetListOfCancel":
            TableTemplate = {
                Title: "已撤单记录",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetList&state=1",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "IssueNum", value: "购买期号"}, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Id", value: "投注时间" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('已撤单记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "投注", Filed: "total", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "异常状态", Filed: "warnstate", Width: "*", Align: "center" },
                        { Header: "来源", Filed: "source", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,600,true)" }
                                  ]
                        }]
            };
            break;
        case "BetList":
            TableTemplate = {
                Title: "全部投注记录",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "IssueNum", value: "购买期号"}, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Id", value: "投注时间" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('投注记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "投注", Filed: "total", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 99: "", 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "异常状态", Filed: "warnstate", Width: "*", Align: "center" },
                        { Header: "来源", Filed: "source", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,600,true)" }
                                  ]
                        }]
            };
            break;
            case "BetListOfYC":
            TableTemplate = {
                Title: "异常投注",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetList&yc=1",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", OnChange: "selectlottery", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "玩法", InputId: "pid", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "所有玩法"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "IssueNum", value: "购买期号"}, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             { InputType: "select", InputTitle: "排序", InputId: "order", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "Id", value: "投注时间" }, { key: "realget", value: "实际盈亏" }, { key: "winbonus", value: "派奖" }, { key: "bet", value: "代购" }, { key: "times", value: "投注倍数" }, { key: "num", value: "投注注数" }, ] },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('投注记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "投注", Filed: "total", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "异常状态", Filed: "warnstate", Width: "*", Align: "center" },
                        { Header: "来源", Filed: "source", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,600,true)" }
                                  ]
                        }]
            };
            break;
        case "BetListOfMissing":
            TableTemplate = {
                Title: "往期漏派订单",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetListOfMissing",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "选中派奖", Function: "ajaxPaiJiangBetId()", InputClass: "btn btn-primary" },
                              { Title: "选中已撤单", Function: "ajaxBetCancel()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('漏派记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "彩种", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "投注", Filed: "total", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "winbonus", Width: "*", Align: "center" },
                        { Header: "盈亏", Filed: "realget", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "state", Width: "*", Align: "center", Default: [{ 0: "未开奖", 1: "已撤单", 2: "未中奖", 3: "已中奖"}] },
                        { Header: "异常状态", Filed: "warnstate", Width: "*", Align: "center" },
                        { Header: "来源", Filed: "source", Width: "*", Align: "center" },
                        { Header: "时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betinfo.aspx?id=@@',800,600,true)" }
                                  ]
                        }]
            };
            break;
        case "BetZhList":
            TableTemplate = {
                Title: "全部追号记录",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetZHList",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "select", InputTitle: "游戏名称", InputId: "lid", InputClass: "sel sel-md", Width: "100px", Options: LotteryJsonData },
                             { InputType: "select", InputTitle: "", InputId: "sel", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "username", value: "会员账号" }, { key: "IssueNum", value: "购买期号"}, { key: "ssid", value: "订单编号"}] },
                             { InputType: "Input", InputTitle: "", InputId: "u", InputClass: "sel sel-md", Width: "80px" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('追号记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },

                        { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "游戏名称", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "开始期号", Filed: "startissuenum", Width: "*", Align: "center" },
                        { Header: "总金额", Filed: "alltotal", Width: "*", Align: "center" },
                        { Header: "完成金额", Filed: "finishtotal", Width: "*", Align: "center" },
                        { Header: "取消金额", Filed: "canceltotal", Width: "*", Align: "center" },
                        { Header: "中奖后停止", Filed: "isstopname", Width: "*", Align: "center" },
                        { Header: "追号进度", Filed: "statename", Width: "*", Color: "Red", Align: "center" },
                        { Header: "追号时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Link", Title: "详情", Function: "/admin/conList.aspx?page=BetInfoList&id=@@" }
                                  ]
                        }]
            };
            break;
        case "BetInfoList":
            TableTemplate = {
                Title: "追号详细列表",
                PageSize: 16,
                Url: "/admin/ajaxBet.aspx?oper=ajaxGetZHInfo",
                Query: [],
                Botton: [{ Title: "终止追号", Function: "operater()", InputClass: "btn btn-primary" }, { Title: "返回追号", Link: "/admin/conList.aspx?page=BetZhList", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },

                         { Header: "订单号", Filed: "ssid", Width: "*", Align: "center" },
                        { Header: "用户帐号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "投注时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "彩票", Filed: "lotteryname", Width: "*", Align: "center" },
                        { Header: "玩法", Filed: "playname", Width: "*", Align: "center" },
                        { Header: "期号", Filed: "issuenum", Width: "*", Align: "center" },
                        { Header: "模式", Filed: "moshi", Width: "*", Align: "center" },
                        { Header: "注数", Filed: "num", Width: "*", Align: "center" },
                        { Header: "倍数", Filed: "times", Width: "*", Align: "center" },
                        { Header: "总金额", Filed: "total", Width: "*", Align: "center" },
                        { Header: "赢亏", Filed: "realget", Width: "*", Align: "center" },
                         { Header: "开奖号码", Filed: "number", Width: "*", Align: "center" },
                        { Header: "状态", Filed: "statename", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "详情", Function: "top.Lottery.Popup.show('/admin/betzhinfo.aspx?id=@@',800,525,true)" }
                                  ]
                        }]
            };
            break;
        //#endregion                       
        //系统设置                  
        //#region                  
        case "AutoRankList":
            TableTemplate = {
                Title: "排行设置",
                PageSize: 16,
                Url: "/admin/ajaxAutoRank.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxAutoRank.aspx?oper=ajaxDel",
                Query: [],
                Botton: [{ Title: "添加排行", Function: "PagePop('/admin/PageSavePop.aspx?page=AutoRankSave')", InputClass: "btn btn-primary"}],
                List: [
                         { Header: "用户名", Filed: "username", Width: "*", Align: "center" },
                         { Header: "奖金", Filed: "win", Width: "*", Align: "center" },
                         { Header: "添加时间", Filed: "stime", Width: "*", Align: "center" },
                         { Header: "启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 1: "启用", 0: "禁用"}], Function: "ajaxStates" },
                         { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "top.Lottery.Popup.show('/admin/PageEditPop.aspx?page=AutoRankSave&id=@@',600,350,true)" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                         }]
            };
            break;
        case "LoginChecLlist":
            TableTemplate = {
                Title: "限制登录",
                PageSize: 16,
                Url: "/admin/ajaxLoginCheck.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxLoginCheck.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxLoginCheck.aspx?oper=ajaxStates",
                Query: [{ InputType: "select", InputTitle: "类别", InputId: "type", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "0", value: "所有限制" }, { key: "1", value: "限制IP登录" }, { key: "2", value: "限制会员登录"}]}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }, { Title: "添加限制", Function: "PagePop('/admin/PageSavePop.aspx?page=LoginCheckSave')", InputClass: "btn btn-primary"}],
                List: [
                         { Header: "限制类别", Filed: "checktype", Width: "*", Align: "center", Default: [{ 2: "限制会员登陆", 1: "限制IP登陆"}] },
                         { Header: "限制内容", Filed: "checktitle", Width: "*", Align: "center" },
                         { Header: "添加时间", Filed: "stime", Width: "*", Align: "center" },
                         { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 1: "启用", 0: "禁用"}], Function: "ajaxStates" },
                         { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "top.Lottery.Popup.show('/admin/PageEditPop.aspx?page=LoginCheckSave&id=@@',600,350,true)" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                         }]
            };
            break;
       case "AdminLoginChecLlist":
            TableTemplate = {
                Title: "后台登录白名单",
                PageSize: 16,
                Url: "/admin/ajaxAdminLoginCheck.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxAdminLoginCheck.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxAdminLoginCheck.aspx?oper=ajaxStates",
                Query: [],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }, { Title: "添加白名单", Function: "PagePop('/admin/PageSavePop.aspx?page=AdminLoginCheckSave')", InputClass: "btn btn-primary"}],
                List: [
                         { Header: "限制类别", Filed: "checktype", Width: "*", Align: "center", Default: [{ 0: "后台白名单"}] },
                         { Header: "限制内容", Filed: "checktitle", Width: "*", Align: "center" },
                         { Header: "添加时间", Filed: "stime", Width: "*", Align: "center" },
                         { Header: "是否启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 1: "启用", 0: "禁用"}], Function: "ajaxStates" },
                         { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "top.Lottery.Popup.show('/admin/PageEditPop.aspx?page=AdminLoginCheckSave&id=@@',600,350,true)" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                         }]
            };
            break;
        case "Newslist":
            TableTemplate = {
                Title: "系统公告",
                PageSize: 16,
                Url: "/admin/ajaxSysNews.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxSysNews.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxSysNews.aspx?oper=ajaxStates",
                State2Url: "/admin/ajaxSysNews.aspx?oper=ajaxIndexStates",
                Query: [],
                Botton: [{ Title: "添加公告", Function: "PagePop('/admin/sysNewsadd.aspx')", InputClass: "btn btn-primary"}],
                List: [
                         { Header: "标题", Filed: "title", Width: "*", Align: "left" },
                         { Header: "发布时间", Filed: "stime", Width: "*", Align: "center" },
                         { Header: "启用", Filed: "isused", Width: "*", Align: "center", Default: [{ 1: "启用", 0: "禁用"}], Function: "ajaxStates" },
                         { Header: "首页弹出显示", Filed: "isindex", Width: "*", Align: "center", Default: [{ 0: "不显示", 1: "显示"}], Function: "ajaxStates2" },
                         { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "top.Lottery.Popup.show('/admin/sysNewsedit.aspx?id=@@',800,500,true)" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                         }]
            };
            break;
        case "Tasklist":
            TableTemplate = {
                Title: "定时任务",
                PageSize: 16,
                Url: "/admin/ajaxTask.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxTask.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxTask.aspx?oper=ajaxStates",
                Query: [],
                Botton: [{ Title: "添加任务", Function: "PagePop('/admin/PageSavePop.aspx?page=TaskSetSave')", InputClass: "btn btn-primary"}],
                List: [
                         { Header: "任务名称", Filed: "title", Width: "*", Align: "center" },
                         { Header: "队列顺序", Filed: "sort", Width: "*", Align: "center" },
                         { Header: "开始时间", Filed: "starttime", Width: "*", Align: "center" },
                         { Header: "截止时间", Filed: "endtime", Width: "*", Align: "center" },
                         { Header: "任务状态", Filed: "isused", Width: "*", Align: "center", Default: [{ 1: "启用", 0: "禁用"}], Function: "ajaxStates" },
                         { Header: "上次执行", Filed: "beforetime", Width: "*", Align: "center" },
                         { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=TaskSetSave&id=@@')" },
                                    { Type: "Fun", Title: "删除", Function: "ConfirmDel(@@)" }
                                ]
                         }]
            };
            break;
        case "MessageList":
            TableTemplate = {
                Title: "即时信息",
                PageSize: 16,
                Url: "/admin/ajaxMessage.aspx?oper=ajaxGetList",
                ClearUrl: "/admin/ajaxMessage.aspx?oper=ajaxClear",
                Query: [],
                Botton: [{ Title: "发送信息", Function: "PagePop('/admin/SendMessage.aspx')", InputClass: "btn btn-primary" }, { Title: "清空记录", Function: "Confirmclear()", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "150", Align: "center" },
                        { Header: "发送内容", Filed: "msg", Width: "*", Align: "left" },
                        { Header: "发布时间", Filed: "stime", Width: "150", Align: "center" }
                       ]
            };
            break;
        //#endregion                         
        //会员管理       { Type: "Popup", Title: "会员切线", Function: "top.Lottery.Popup.show('userUpdateParent.aspx?id=@@',600,250,true)" },             
        //#region                   
        case "UserList":
            TableTemplate = {
                Title: "用户列表",
                PageSize: 16,
                Url: "/admin/ajaxUser.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxUser.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxUser.aspx?oper=ajaxStates",
                Query: [{ InputType: "select", InputTitle: "类型", InputId: "group", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                             { InputType: "select", InputTitle: "在线状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线"}] },
                             { InputType: "select", InputTitle: "账号状态", InputId: "isenable", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "正常" }, { key: "1", value: "锁定"}, { key: "2", value: "卡掉线"}] },
                             { InputType: "Input", InputTitle: "账号余额", InputId: "money1", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "", InputId: "money2", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "未登录", InputId: "nologin", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "1", value: "会员账号" }, { key: "2", value: "会员编号" }, { key: "3", value: "会员返点" }, { key: "4", value: "银行卡号" }, { key: "5", value: "银行姓名"}] },
                             { InputType: "Input", InputTitle: "", InputId: "uname", InputClass: "ipt", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                             { Title: "相同IP", Function: "ajaxSearchIp();", InputClass: "btn btn-primary" },
                             { Title: "选中删除", Function: "ConfirmAllDel();", InputClass: "btn btn-primary" },
                             { Title: "选中下线", Function: "ajaxAllOnline();", InputClass: "btn btn-primary" },
                             { Title: "新增会员", Function: "PagePop('useradd.aspx');", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "会员类型", Filed: "usergroupname", Filed2: "childnum", Width: "*", Align: "center", Function: "ajaxSearchById" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "上级账号", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "账号积分", Filed: "score", Width: "*", Align: "right" },
                        { Header: "最后访问", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "enableseason", Width: "*", Align: "center" },
                        { Header: "账号状态", Filed: "isenable", Width: "*", Align: "center", Default: [{ 0: "正常", 1: "锁定", 2: "卡掉线"}], Function: "ajaxStates" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线"}] },
                        { Header: "绑定银行", Filed: "bank", Width: "*", Align: "center", Default: [{ 0: "未绑定", 1: "绑定1张卡", 2: "绑定2张卡", 3: "绑定3张卡", 4: "绑定4张卡", 5: "绑定5张卡", 6: "绑定6张卡", 7: "绑定7张卡", 8: "绑定8张卡", 9: "绑定9张卡", 10: "绑定10张卡"}] },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "资金操作", Function: "PagePop('usercharge.aspx?id=@@')" },
                                    { Type: "Link", Title: "银行信息", Function: "/admin/conList.aspx?page=UserBank&id=@@" },
                                    { Type: "Popup", Title: "编辑账户", Function: "top.Lottery.Popup.show('useredit.aspx?id=@@',1000,720,true)" },
                                    { Type: "Fun", Title: "删除账户", Function: "ConfirmDel(@@)" },
                                    { Type: "Popup", Title: "玩法权限", Function: "top.Lottery.Popup.show('/admin/userNoPlay.aspx?id=@@',1000,600,true)" },
                                    { Type: "Popup", Title: "修改姓名", Function: "PagePop('/admin/userTrueNameEdit.aspx?id=@@')" }
                                ]
                        }]
            };
            break;
      case "UserUpdateList":
            TableTemplate = {
                Title: "会员切线",
                PageSize: 16,
                Url: "/admin/ajaxUser.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxUser.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxUser.aspx?oper=ajaxStates",
                 Query: [{ InputType: "select", InputTitle: "类型", InputId: "group", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                             { InputType: "select", InputTitle: "在线状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线"}] },
                             { InputType: "select", InputTitle: "账号状态", InputId: "isenable", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "正常" }, { key: "1", value: "锁定"}, { key: "2", value: "卡掉线"}] },
                             { InputType: "Input", InputTitle: "账号余额", InputId: "money1", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "", InputId: "money2", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "未登录", InputId: "nologin", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "1", value: "会员账号" }, { key: "2", value: "会员编号" }, { key: "3", value: "会员返点" }, { key: "4", value: "银行卡号" }, { key: "5", value: "银行姓名"}] },
                             { InputType: "Input", InputTitle: "", InputId: "uname", InputClass: "ipt", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                             { Title: "相同IP", Function: "ajaxSearchIp();", InputClass: "btn btn-primary" },
                             { Title: "选中删除", Function: "ConfirmAllDel();", InputClass: "btn btn-primary" },
                             { Title: "选中下线", Function: "ajaxAllOnline();", InputClass: "btn btn-primary" },
                             { Title: "新增会员", Function: "PagePop('useradd.aspx');", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "会员类型", Filed: "usergroupname", Filed2: "childnum", Width: "*", Align: "center", Function: "ajaxSearchById" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "上级账号", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "账号积分", Filed: "score", Width: "*", Align: "right" },
                        { Header: "最后访问", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "enableseason", Width: "*", Align: "center" },
                        { Header: "账号状态", Filed: "isenable", Width: "*", Align: "center", Default: [{ 0: "正常", 1: "锁定", 2: "卡掉线"}], Function: "ajaxStates" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线"}] },
                        { Header: "绑定银行", Filed: "bank", Width: "*", Align: "center", Default: [{ 0: "未绑定", 1: "绑定1张卡", 2: "绑定2张卡", 3: "绑定3张卡", 4: "绑定4张卡", 5: "绑定5张卡", 6: "绑定6张卡"}] },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "会员切线", Function: "top.Lottery.Popup.show('userUpdateParent.aspx?id=@@',600,250,true)" }
                                ]
                        }]
            };
            break;
        case "UserEditList":
            TableTemplate = {
                Title: "用户列表",
                PageSize: 16,
                Url: "/admin/ajaxUser.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxUser.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxUser.aspx?oper=ajaxStates",
                Query: [{ InputType: "select", InputTitle: "类型", InputId: "group", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                             { InputType: "select", InputTitle: "在线状态", InputId: "online", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "离线" }, { key: "1", value: "在线"}] },
                             { InputType: "select", InputTitle: "账号状态", InputId: "isenable", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "正常" }, { key: "1", value: "锁定"}, { key: "2", value: "卡掉线"}] },
                             { InputType: "Input", InputTitle: "账号余额", InputId: "money1", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "", InputId: "money2", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "未登录", InputId: "nologin", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "1", value: "会员账号" }, { key: "2", value: "会员编号" }, { key: "3", value: "会员返点" }, { key: "4", value: "银行卡号" }, { key: "5", value: "银行姓名"}] },
                             { InputType: "Input", InputTitle: "", InputId: "uname", InputClass: "ipt", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                             { Title: "相同IP", Function: "ajaxSearchIp();", InputClass: "btn btn-primary" },
                             { Title: "选中删除", Function: "ConfirmAllDel();", InputClass: "btn btn-primary" },
                             { Title: "选中下线", Function: "ajaxAllOnline();", InputClass: "btn btn-primary" },
                             { Title: "新增会员", Function: "PagePop('useradd.aspx');", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "会员类型", Filed: "usergroupname", Filed2: "childnum", Width: "*", Align: "center", Function: "ajaxSearchById" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "上级账号", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "账号积分", Filed: "score", Width: "*", Align: "right" },
                        { Header: "最后访问", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "备注", Filed: "enableseason", Width: "*", Align: "center" },
                        { Header: "账号状态", Filed: "isenable", Width: "*", Align: "center", Default: [{ 0: "正常", 1: "锁定", 2: "卡掉线"}], Function: "ajaxStates" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线"}] },
                        { Header: "绑定银行", Filed: "bank", Width: "*", Align: "center", Default: [{ 0: "未绑定", 1: "绑定1张卡", 2: "绑定2张卡", 3: "绑定3张卡", 4: "绑定4张卡", 5: "绑定5张卡", 6: "绑定6张卡"}] },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Link", Title: "银行信息", Function: "/admin/conList.aspx?page=UserBank&id=@@" },
                                    { Type: "Popup", Title: "编辑账户", Function: "top.Lottery.Popup.show('useredit.aspx?id=@@',1000,720,true)" },
                                ]
                        }]
            };
            break;
        case "UserDetail":
            TableTemplate = {
                Title: "代理详情",
                PageSize: 16,
                Url: "/admin/ajaxProfitloss.aspx?oper=ajaxUserDetail",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary"}],
                List: [{ Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "返点级别", Filed: "userpoint", Width: "*", Align: "center" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "center" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "center" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "center" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "center" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "center" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "center" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "center" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "center", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "center", TwoColor: true },
                        { Header: "团队在线", Filed: "onlinenum", Width: "*", Align: "center" },
                        { Header: "今日有效投注", Filed: "yxnum", Width: "*", Align: "center" },
                        { Header: "配额12.8(个)", Filed: "point128", Width: "*", Align: "center" },
                        { Header: "配额12.7(个)", Filed: "point127", Width: "*", Align: "center"}]
            };
            break;
        case "UserListDel":
            TableTemplate = {
                Title: "账号回收站",
                PageSize: 16,
                Url: "/admin/ajaxUser.aspx?oper=ajaxGetDelList",
                DelUrl: "/admin/ajaxUser.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxUser.aspx?oper=ajaxStates",
                Query: [{ InputType: "Input", InputTitle: "会员账号", InputId: "uname", InputClass: "ipt", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                             { Title: "彻底删除", Function: "ConfirmAllDel2();", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                         { Header: "类型", Filed: "usergroup", Width: "*", Align: "center", Default: [{ 0: "代理", 1: "会员"}] },
                        { Header: "会员账号", Filed: "username", Filed2: "childnum", Width: "*", Align: "center", Function: "ajaxSearchById" },
                        { Header: "上级账号", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Color: "Red", Align: "right" },
                        { Header: "账号积分", Filed: "score", Width: "*", Align: "right" },
                        { Header: "最后访问", Filed: "ip", Width: "*", Align: "center" },
                        { Header: "账号状态", Filed: "isenable", Width: "*", Align: "center", Default: [{ 0: "正常", 1: "锁定"}], Function: "ajaxStates" },
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线"}] },
                        { Header: "绑定银行", Filed: "bank", Width: "*", Align: "center", Default: [{ 0: "未绑定", 1: "绑定"}] },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Fun", Title: "恢复会员", Function: "ConfirmHF(@@)" },
                                    { Type: "Fun", Title: "彻底删除", Function: "ConfirmDel2(@@)" }
                                ]
                        }]
            };
            break;
      case "UserGroupQuotaLlist":
            TableTemplate = {
                Title: "设置类型配额",
                PageSize: 16,
                Url: "/admin/ajaxUserlevel.aspx?oper=ajaxGetUserGroupQuotaList",
                Query: [{ InputType: "select", InputTitle: "会员类型", InputId: "group", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] }
                            ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                { Title: "新增配额", Function: "PagePop('/admin/PageSavePop.aspx?page=UserGroupQuotaSave');", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员类型", Filed: "groupname", Width: "*", Align: "center" },
                        { Header: "可开户类型", Filed: "togroupname", Width: "*", Align: "center" },
                        { Header: "可开户数量", Filed: "childnums", Width: "*", Color: "Red", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=UserGroupQuotaSave&id=@@')" }
                                ]
                        }]
            };
            break;
        case "UserPointQuotaLlist":
            TableTemplate = {
                Title: "设置平级配额",
                PageSize: 16,
                Url: "/admin/ajaxUserlevel.aspx?oper=ajaxGetUserPointQuotaList",
                Query: [{ InputType: "Input", InputTitle: "会员返点", InputId: "point", InputClass: "ipt", Width: "80px" }
                            ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                { Title: "新增配额", Function: "PagePop('/admin/PageSavePop.aspx?page=UserPointQuotaSave');", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员返点", Filed: "point", Width: "*", Align: "center" },
                        { Header: "可开平级数量", Filed: "childnums", Width: "*", Color: "Red", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=UserPointQuotaSave&id=@@')" }
                                ]
                        }]
            };
            break;
        case "UserQuotaLlist":
            TableTemplate = {
                Title: "会员配额",
                PageSize: 16,
                Url: "/admin/ajaxUserlevel.aspx?oper=ajaxGetQuotaList",
                DelUrl: "/admin/ajaxUser.aspx?oper=ajaxDel",
                StateUrl: "/admin/ajaxUser.aspx?oper=ajaxStates",
                Query: [{ InputType: "Input", InputTitle: "会员账号", InputId: "uname", InputClass: "ipt", Width: "80px" },
                            { InputType: "Input", InputTitle: "返点", InputId: "ulevel", InputClass: "ipt", Width: "100px", Keyup: "on" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                             { Title: "生成配额", Function: "PagePop('userQuotaadd.aspx');", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center" },
                        { Header: "返点", Filed: "userlevel", Width: "*", Align: "center" },
                        { Header: "配额数量", Filed: "childnums", Width: "*", Align: "center" },
                        { Header: "已用配额", Filed: "usenums", Width: "*", Color: "Red", Align: "center" },
                        { Header: "下级配额", Filed: "usenums2", Width: "*", Align: "center" },
                        { Header: "未使用配额", Filed: "nousernums", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/PageEditPop.aspx?page=UserQuotaSave&id=@@')" }
                                ]
                        }]
            };
            break;
        case "UserBank":
            TableTemplate = {
                Title: "绑定银行列表",
                PageSize: 16,
                Url: "/admin/ajaxSysBank.aspx?oper=ajaxGetUserBankList",
                DelUrl: "/admin/ajaxSysBank.aspx?oper=ajaxDel",
                Query: [],
                Botton: [{ Title: "返回会员列表", Link: "/admin/conList.aspx?page=UserList", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "银行名称", Filed: "paybank", Width: "*", Align: "center" },
                        { Header: "银行卡号", Filed: "payaccount", Width: "*", Align: "center" },
                        { Header: "开户姓名", Filed: "payname", Width: "*", Align: "center" },
                        { Header: "开户地址", Filed: "paybankaddress", Width: "*", Align: "center" },
                        { Header: "绑定时间", Filed: "addtime", Width: "*", Align: "center" },
                        { Header: "绑定状态", Filed: "islock", Width: "*", Color: "Red", Align: "center", Default: [{ 1: "已绑定", 0: "未绑定"}] },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                   { Type: "Popup", Title: "解绑", Function: "PagePop('/admin/userBankUnLock.aspx?id=@@')" },
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/userBankEdit.aspx?id=@@')" },
                                    { Type: "Popup", Title: "删除", Function: "PagePop('/admin/userBankDel.aspx?id=@@')" }
                                ]
                        }]
            };
            break;
        case "UserBankPop":
            TableTemplate = {
                Title: "绑定银行列表",
                PageSize: 16,
                Url: "/admin/ajaxSysBank.aspx?oper=ajaxGetUserBankLog",
                Query: [],
                Botton: [],
                List: [
                        { Header: "操作名称", Filed: "type", Width: "*", Align: "center" },
                        { Header: "银行名称", Filed: "paybank", Width: "*", Align: "center" },
                        { Header: "银行卡号", Filed: "payaccount", Width: "*", Align: "center" },
                        { Header: "开户姓名", Filed: "payname", Width: "*", Align: "center" },
                        { Header: "开户地址", Filed: "paybankaddress", Width: "*", Align: "center" },
                        { Header: "操作时间", Filed: "addtime", Width: "*", Align: "center" },
                        { Header: "绑定状态", Filed: "islock", Width: "*", Color: "Red", Align: "center", Default: [{ 1: "已绑定", 0: "未绑定"}] }
                        ]
            };
            break;
        case "UserBankAll":
            TableTemplate = {
                Title: "绑定银行列表",
                PageSize: 16,
                Url: "/admin/ajaxSysBank.aspx?oper=ajaxGetUserBankAllList",
                DelUrl: "/admin/ajaxSysBank.aspx?oper=ajaxDel",
                Query: [{ InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "ipt", Width: "80px" },
                             { InputType: "Input", InputTitle: "银行户名", InputId: "payname", InputClass: "ipt", Width: "100px" },
                             { InputType: "Input", InputTitle: "银行卡号", InputId: "payaccount", InputClass: "ipt", Width: "200px"}],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('银行记录')", InputClass: "btn btn-primary"}],
                List: [
                        { Header: "会员名称", Filed: "username", Width: "*", Align: "center" },
                        { Header: "银行名称", Filed: "paybank", Width: "*", Align: "center" },
                        { Header: "银行卡号", Filed: "payaccount", Width: "*", Align: "center" },
                        { Header: "开户姓名", Filed: "payname", Width: "*", Align: "center" },
                        { Header: "开户地址", Filed: "paybankaddress", Width: "*", Align: "center" },
                        { Header: "绑定时间", Filed: "addtime", Width: "*", Align: "center" },
                        { Header: "绑定状态", Filed: "islock", Width: "*", Color: "Red", Align: "center", Default: [{ 1: "已绑定", 0: "未绑定"}] },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                   { Type: "Popup", Title: "解绑", Function: "PagePop('/admin/userBankUnLock.aspx?id=@@')" },
                                    { Type: "Popup", Title: "编辑", Function: "PagePop('/admin/userBankEdit.aspx?id=@@')" },
                                    { Type: "Popup", Title: "删除", Function: "PagePop('/admin/userBankDel.aspx?id=@@')" }
                                ]
                        }]
            };
            break;

            case "UserContractList":
            TableTemplate = {
                Title: "契约信息",
                PageSize: 16,
                Url: "/admin/ajaxContract.aspx?oper=ajaxGetList",
                DelUrl: "/admin/ajaxContract.aspx?oper=ajaxDel",
                Query: [{ InputType: "select", InputTitle: "契约类型", InputId: "type", InputClass: "sel sel-md", Width: "100px", Options: [{ key: "", value: "全部" }, { key: "1", value: "分红契约" }, { key: "2", value: "日结契约" }] },
                          { InputType: "Input", InputTitle: "父级会员", InputId: "p", InputClass: "sel sel-md", Width: "100px" },
                        { InputType: "Input", InputTitle: "契约会员", InputId: "u", InputClass: "sel sel-md", Width: "100px" },
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('追号记录')", InputClass: "btn btn-primary"}],
                List: [{ Header: "checkbox", Filed: "checkbox", Width: "30", Align: "center" },
                        { Header: "编号", Filed: "id", Width: "*", Align: "center" },
                        { Header: "契约类型", Filed: "type", Width: "*", Align: "center" , Default: [{ 1: "分红契约", 2: "日结契约"}]},
                        { Header: "父级会员", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "契约会员", Filed: "username", Width: "*", Align: "center" },
                        { Header: "签订状态", Filed: "isused", Width: "*", Align: "center", Default: [{ 0: "已分配，待确认", 1: "已成功签订", 2: "会员拒绝签订", 3: "撤销等待确认", 4: "撤销已确认，请重新分配"}] },
                        { Header: "分配时间", Filed: "stime", Width: "*", Align: "center" },
                        { Header: "签订时间", Filed: "stime2", Width: "*", Align: "center" },
                        { Header: "", Width: "*", Align: "center"
                        , Info: [
                                       { Type: "Popup", Title: "契约详情", Function: "LayerPop('契约','/admin/conList.aspx?page=UserContractDetail&id=@@')" },
                                       { Type: "Fun", Title: "删除契约", Function: "ConfirmDel(@@)" },
                                ]
                        }]
            };
            break;
        case "UserContractDetail":
            TableTemplate = {
                Title: "契约详细信息",
                PageSize: 16,
                Url: "/admin/ajaxContract.aspx?oper=ajaxGetDetail",
                Query: [],
                Botton: [],
                List: [
                        { Header: "半月销量", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "分红比例", Filed: "money", Width: "*", Align: "center" }
                        ]
            };

         case "UserContractDetail2":
            TableTemplate = {
                Title: "契约详细信息",
                PageSize: 16,
                Url: "/admin/ajaxContract.aspx?oper=ajaxGetDetailByUserId",
                Query: [],
                Botton: [],
                List: [
                        { Header: "半月销量", Filed: "minmoney", Width: "*", Align: "center" },
                        { Header: "分红比例", Filed: "money", Width: "*", Align: "center" }
                        ]
            };
            break;
        //#endregion          
        
        //#endregion                         
        //风控管理                   
        //#region                   
        case "UserListFK":
            TableTemplate = {
                Title: "用户列表",
                PageSize: 16,
                Url: "/admin/ajaxUser.aspx?oper=ajaxGetFKList",
                Query: [{ InputType: "select", InputTitle: "类型", InputId: "group", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                             { InputType: "select", InputTitle: "", InputId: "sel1", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "1", value: "账号余额" }, { key: "2", value: "账号积分" }, { key: "3", value: "未登录天数"}] },
                             { InputType: "Input", InputTitle: "", InputId: "money1", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "", InputId: "money2", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "120px", Options: [{ key: "1", value: "请输入会员账号" }] },
                             { InputType: "Input", InputTitle: "", InputId: "uname", InputClass: "ipt", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "会员类型", Filed: "usergroupname", Width: "*", Align: "center"},
                        { Header: "会员账号", Filed: "username", Filed2: "childnum",Width: "*",Color: "Red", Align: "center" , Function: "ajaxSearchById" },
                        { Header: "上级账号", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "账号积分", Filed: "score", Width: "*", Align: "right" },
                        { Header: "最后访问", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "账号状态", Filed: "isenable", Width: "*", Align: "center", Default: [{ 0: "正常", 1: "锁定"}]},
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线"}] },
                        { Header: "绑定银行", Filed: "bank", Width: "*", Align: "center", Default: [{ 0: "未绑定", 1: "绑定"}] }]
            };
            break;
            case "UserListOnlineFK":
            TableTemplate = {
                Title: "用户列表",
                PageSize: 16,
                Url: "/admin/ajaxUser.aspx?oper=ajaxGetFKListOnLine",
                Query: [{ InputType: "select", InputTitle: "类型", InputId: "group", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "", value: "全部" }, { key: "0", value: "会员" }, { key: "1", value: "代理" }, { key: "2", value: "直属" }, { key: "3", value: "特权直属" }, { key: "4", value: "招商" }, { key: "5", value: "主管"}] },
                            { InputType: "select", InputTitle: "", InputId: "sel1", InputClass: "sel sel-md", Width: "80px", Options: [{ key: "1", value: "账号余额" }, { key: "2", value: "账号积分" }, { key: "3", value: "未登录天数"}] },
                             { InputType: "Input", InputTitle: "", InputId: "money1", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "Input", InputTitle: "", InputId: "money2", InputClass: "ipt", Width: "60px", Keyup: "on" },
                             { InputType: "select", InputTitle: "", InputId: "sel2", InputClass: "sel sel-md", Width: "120px", Options: [{ key: "1", value: "请输入会员账号" }] },
                             { InputType: "Input", InputTitle: "", InputId: "uname", InputClass: "ipt", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" }],
                List: [
                        { Header: "会员类型", Filed: "usergroupname", Width: "*", Align: "center"},
                        { Header: "会员账号", Filed: "username", Filed2: "childnum",Width: "*",Color: "Red", Align: "center" , Function: "ajaxSearchById" },
                        { Header: "上级账号", Filed: "parentname", Width: "*", Align: "center" },
                        { Header: "返点/奖金", Filed: "point", Width: "*", Align: "right" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "账号积分", Filed: "score", Width: "*", Align: "right" },
                        { Header: "最后访问", Filed: "lasttime", Width: "*", Align: "center" },
                        { Header: "账号状态", Filed: "isenable", Width: "*", Align: "center", Default: [{ 0: "正常", 1: "锁定"}]},
                        { Header: "在线状态", Filed: "isonline", Width: "*", Align: "center", Default: [{ 0: "离线", 1: "在线"}] },
                        { Header: "绑定银行", Filed: "bank", Width: "*", Align: "center", Default: [{ 0: "未绑定", 1: "绑定"}] }]
            };
            break;
            case "UserProListSubFK":
            TableTemplate = {
                Title: "团队统计",
                PageSize: 16,
                Url: "/admin/ajaxUser.aspx?oper=ajaxGetFKProListSub",
                Query: [{ InputType: "DateTime", InputTitle: "开始时间", InputId: "d1", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(0) },
                             { InputType: "DateTime", InputTitle: "截止时间", InputId: "d2", InputClass: "sel sel-md", Width: "135px", Value: GetDateStr(1) },
                             { InputType: "Input", InputTitle: "会员账号", InputId: "u", InputClass: "sel sel-md", Width: "80px" }
                             ],
                Botton: [{ Title: "查询", Function: "ajaxSearch()", InputClass: "btn btn-primary" },
                              { Title: "导出", Function: "Table2Excel('团队统计')", InputClass: "btn btn-primary"}],
                List: [ { Header: "账号返点", Filed: "userpoint", Width: "*", Align: "center" },
                        { Header: "会员账号", Filed: "username", Width: "*", Align: "center", FunctionUserId: "ajaxSearchById" },
                        { Header: "账号余额", Filed: "money", Width: "*", Align: "right" },
                        { Header: "存款", Filed: "charge", Width: "*", Align: "right" },
                        { Header: "取款", Filed: "getcash", Width: "*", Align: "right" },
                        { Header: "代购", Filed: "bet", Width: "*", Align: "right" },
                        { Header: "派奖", Filed: "win", Width: "*", Align: "right" },
                        { Header: "返点", Filed: "point", Width: "*", Align: "right" },
                        { Header: "转入", Filed: "tranaccin", Width: "*", Align: "right" },
                        { Header: "转出", Filed: "tranaccout", Width: "*", Align: "right" },
                        { Header: "活动", Filed: "give", Width: "*", Align: "right" },
                        { Header: "其他", Filed: "other", Width: "*", Align: "right" },
                        { Header: "报表盈亏", Filed: "total", Width: "*", Align: "right", TwoColor: true },
                        { Header: "实际盈亏", Filed: "moneytotal", Width: "*", Align: "right", TwoColor: true}]
            };
            break;
        //#endregion               
        default:
            break;
    }
    return TableTemplate;
}