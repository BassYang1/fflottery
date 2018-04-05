var LotteryType = 1;
var LotteryId = 1;
var LotteryName = "";
var LotteryMinTimes = 1;
var LotteryMaxTimes = 1000;
var PlayBigId;
var PlayBigName;
var PlayId;
var PlayName;
var PlayCode;
var PlayRandoms;
var PlayWzNum;
var PlayExample;
var PlayHelp;
var playPoints;
var PlayPos = "";
var Price = 1; //圆角分
var PriceName = "元";
var PlayMaxNum = 100;
var Nmbtype = 1;
var PriceTimes = 1; //倍数
var SingleCount = 0; //单笔注数
var SingleTotal = 0; //单笔金额
var SumCount = 0; //总注数
var SumTotal = 0; //总金额
var PriceTotal = 0; //不加倍数的金额
var SumOrder = 0; //总单数
var ArrayOrder = new Array(); //投注列表
var SumZhCount = 0;
var SumZhTotal = 0;
var Betpoint = 0;
var lid = joinValue('lid');
var Adminname;
var UserMoney = 0;
var UserScore = 0;
var SingleOrderItem;
var ZhSumCount;
var SelectedData = [];
var StartSn = "";
var NmbZH = 1;
var PricePos = 0.5;//系数，表示单倍还是双倍 

$(document).ready(function () {
    if (getCookie("price") != null) {
        Price = getCookie("price");
    }
    else {
        Price = 1;
    }
    
});

//加载采种
function ajaxLottery() {
    var str1 = "";
    var str2 = "";
    var str3 = "";
    for (i = 0; i < lotteryJsonData.table.length; i++) {
        var title = lotteryJsonData.table[i].title;
        var code = lotteryJsonData.table[i].code;
        var type = lotteryJsonData.table[i].ltype;
        if(type=="1")
        {
            str1 += "<li><a href='/" + code + "'>" + title + "</a></li>";
        }
         if(type=="2")
        {
            str2 += "<li><a href='/" + code + "'>" + title + "</a></li>";
        }
         if(type=="3")
        {
            str3 += "<li><a href='/" + code + "'>" + title + "</a></li>";
        }
    }
    $i("lotterylist").innerHTML = "<ul>"+str1+"</ul>"+"<ul>"+str2+"</ul>"+"<ul>"+str3+"</ul>";
}

//加载基本信息
function ajaxUserInfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) {},
        success: function (d) {
            if (d.result == "1") {
                Adminname = d.AdminName;
                $i('name').innerHTML = d.AdminName;
                $i('money').innerHTML = UserMoney = d.AdminMoney;
//                $i('score').innerHTML = UserScore = d.AdminScore;
//                $i('email').innerHTML = d.emailcount;
            }
            else {
                var strmsg = "<table align='center' style='width:300px'><tr><td align='center' style='height:35px; font-size:16px;'>" + d.Message + "</td></tr></table>"
                layer.open({
                    title: '立博国际娱乐提示您',
                    content: strmsg,
                    area: ['350px'],
                    btn: ['重新登陆'],
                    closeBtn: false,
                    yes: function (index, layero) {
                        top.location.href = '/login.html';
                    }
                });
            }
        }
    });
    setTimeout('ajaxUserInfo()', 5000);
}

//获取即时信息
function ajaxPopInfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxPopInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { },
        success: function (d) {
            if (d.result == "1") {
                var titlesN3=[];
                var contentsN3=[];
                for (i = 0; i < d.table.length; i++) {
                    titlesN3.push(d.table[i].title);
                    contentsN3.push(d.table[i].msg);
                } 
                MessagePop(titlesN3,contentsN3);
                //var pop = new Pop(d.title, "#", d.content);
            }
        }
    });
    setTimeout('ajaxPopInfo()', 10000);
}

//签到领现
function GetSignInMoney() {
    var index = emLoading();
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajaxActive.aspx?oper=ajaxGetSignInMoney",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { emAlert("亲！页面过期,请刷新页面!"); } },
        success: function (d) {
            closeload(index);
            emAlert(d.returnval);
        }
    });
}

//手动刷新
function ajaxRefresh() {
    $i('money').innerHTML = "加载中...";
    var index = emLoading();
    ajaxUserInfo();
    closeload(index);
}

function chkLogout() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "/ajax/ajax.aspx?oper=logout&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            alert(XmlHttpRequest.responseText)
        },
        success: function (d) {
            if (d.result == "1")
                top.location.href = '/login.html'
        }
    });
};

function ajaxPop() {
    layer.open({
        type: 2,
        title: '客户端安装',
        shadeClose: true,
        shade: false,
        maxmin: false,
        area: ['650px'],
        content: '/SL.aspx'
    });
}

function ShowPage(taburl) {
    $i("workspace").src = taburl;
}
