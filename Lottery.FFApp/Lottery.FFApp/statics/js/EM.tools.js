function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}

function ToFrame(src, height) {
    //$i("workmain").style.height = height + "px";
    $i("workmain").src = src;
}

function ajaxCheckLotteryToUrl(src, code) {
    if (src == "/no") {
        emAlert("正在完善中，敬请期待！");
        return;
    }
    else {
        self.location.href = src;
    }
}

function ajaxCheckLoginToUrl(src) {
    if (src == "/no") {
        emAlert("正在完善中，敬请期待！");
        return;
    }
    else {
        self.location.href = src;
    }
}

function ajaxInit(src) {
    $i("workspace").src = src;
    //$("#workspace").style.height = (_jcms_GetViewportHeight() - 165) + "px";
}

function ajaxActive(src, height) {
    $i("workspace").src = src;
    $i("workspace").style.height = height + "px";
}

function ajaxConFrame(src) {
    if (src != "undefined") {
        $("#conFrame").load(src, function (response, status, xhr) {
            $('#conFrame').html(response);
        });
    }
}

function Format(fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function GetDateStr(AddDayCount) {
    var dd = new Date();
    dd.setDate(dd.getDate() + AddDayCount);

    var h = dd.getHours();
    if (h >= 0 && h < 3) {
        var y = dd.getFullYear();
        var m = dd.getMonth() + 1;
        if (m < 10)
            m = "0" + m;
        var d = dd.getDate();
        if (d < 10)
            d = "0" + d;
        return y + "-" + m + "-" + d;
    }
    else {
        var y = dd.getFullYear();
        var m = dd.getMonth() + 1;
        if (m < 10)
            m = "0" + m;
        var d = dd.getDate();
        if (d < 10)
            d = "0" + d;
        return y + "-" + m + "-" + d;
    }
}

function GetDateStrTs(AddDayCount) {
    var dd = new Date();
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1;
    var d = dd.getDate();
    if (AddDayCount == 30) {
        d = "1";
    }
    if (AddDayCount == 90) {
        m = dd.getMonth() - 2;
        d = "1";
    }
    if (AddDayCount == 360) {
        m = "1";
        d = "1";
    }
    if (m < 10)
        m = "0" + m;
    if (d < 10)
        d = "0" + d;
    return y + "-" + m + "-" + d + " 00:00:00";
}

function Show(str) {
    $('#text').text(str);
    $('#loadingDiv').css('display', 'block');
    $('#popup').slideDown();
    setTimeout(function () {
        Close();
    }, 5000);
}

function Close() {
    $('#loadingDiv').css('display', 'none');
    $('#popup').slideUp();
}

function emLoading() {
    return parent.layer.load(1);
    setTimeout(function () {
        parent.layer.close(index);
    }, 1000);
}

function emLoadingSubmit() {
    //加载层-风格4
    return parent.layer.msg('数据提交中，请稍后...', {
        icon: 16,
        shade: 0.2
    });
}
function closeload(index) {
    //   layer.closeAll();
    setTimeout(function () {
        parent.layer.close(index);
    }, 100);
}

function LayerPop(title, width, height, url) {
    parent.layer.open({
        type: 2,
        title: title,
        shadeClose: false,
        //        scrollbar: false,
        shade: 0.2,
        area: [width, height],
        content: url
    });
}

function emAlertMsg(strtitle) {
    var index = parent.layer.msg(strtitle);

    setTimeout(function () {
        parent.layer.close(index);
    }, 3000);
}

function emAlert(strtitle) {
    var con = '<div class="operate-message">';
    con += '<i class="icon-msg icon-warn"></i>';
    con += '<h4>' + strtitle + '</h4>';
    con += '<p>弹出窗口3秒后自动关闭！</p>';
    con += '</div>';

    var index = parent.layer.open({
        title: '温馨提示',
        shade: 0.2,
        shift: 6,
        //scrollbar: false,
        time: 3000, //3秒后自动关闭
        area: ['500px', ''],
        content: con
    });
    layer.closeAll('loading');
}

function GetRandomNum(Min, Max) {
    var Range = Max - Min;
    var Rand = Math.random();
    return (Min + Math.round(Rand * Range));
}

function emAlertSuccess(strtitle) {
    var con = '<div class="operate-message">';
    con += '<i class="icon-msg icon-success"></i>';
    con += '<h4>' + strtitle + '</h4>';
    con += '<p>3秒后自动关闭！</p>';
    con += '</div>';
    var rad = GetRandomNum(1, 5);
    var index = parent.layer.open({
        title: '温馨提示',
        shade: 0.2,
        shift: 6,
        //scrollbar: false,
        time: 3000, //3秒后自动关闭
        area: ['500px', ''],
        content: con
    });
}

function ajaxPagePop(strtitle, width, height, strsrc) {
    layer.open({
        type: 2,
        title: strtitle,
        shadeClose: true,
        shade: false,
        maxmin: false,
        area: [width, height],
        content: strsrc
    });
}

function ajaxPagePop2(strtitle, width, height, strsrc) {
    layer.open({
        type: 2,
        title: strtitle,
        shadeClose: true,
        shade: false,
        maxmin: false,
        area: [width, height],
        offset: '80px',
        content: strsrc
    });
}

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

// 客服js
$('.kf').click(function () {
    kf_width = $(this).width();
    if (kf_width > 61) {
        $(this).width(61);
    } else {
        $(this).width(200);
    }
});

function _jcms_GetViewportHeight() {
    if (window.innerHeight != window.undefined)//FF
    {
        return window.innerHeight;
    }
    if (document.compatMode == 'CSS1Compat')//IE
    {
        return document.documentElement.clientHeight;
    }
    if (document.body)//other
    {
        return document.body.clientHeight;
    }
    return window.undefined;
}

function _jcms_GetViewportWidth() {
    var offset = 17;
    var width = null;
    if (window.innerWidth != window.undefined)//FF
    {
        //return window.innerWidth-offset;
        if (window.innerWidth < 1366)
            return 1366;
        else
            return window.innerWidth;
    }
    if (document.compatMode == 'CSS1Compat')//IE
    {
        if (document.documentElement.clientWidth < 1366)
            return 1366;
        else
            return document.documentElement.clientWidth;
    }
    if (document.body)//other
    {
        if (document.body.clientWidth < 1366)
            return 1366;
        else
            return document.body.clientWidth;
    }
    return window.undefined;
}

function setCookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + " ;path=/";
}

function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}

function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
function stripscript(s) {
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）—|{}【】‘；：”“'。，、？]")
    var rs = "";
    for (var i = 0; i < s.length; i++) {
        rs = rs + s.substr(i, 1).replace(pattern, '');
    }
    return rs;
}

//辅助方法
function getCheck(str) {
    //为了支持IE8
    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function (elt /*, from*/) {
            var len = this.length >>> 0;

            var from = Number(arguments[1]) || 0;
            from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
            if (from < 0)
                from += len;

            for (; from < len; from++) {
                if (from in this &&
          this[from] === elt)
                    return from;
            }
            return -1;
        };
    }

    var array = new Array();
    var list = str.split(" ");
    for (var i = 0; i < list.length; i++) {
        var s = list[i];
        if (array.indexOf(s) == -1) {
            if (check(str, s)) {
                array.push(i + 1, list[i]);
            }
        }
    }
    return array;
}

function get(str) {
    //为了支持IE8
    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function (elt /*, from*/) {
            var len = this.length >>> 0;

            var from = Number(arguments[1]) || 0;
            from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
            if (from < 0)
                from += len;

            for (; from < len; from++) {
                if (from in this &&
          this[from] === elt)
                    return from;
            }
            return -1;
        };
    }

    var array = new Array();
    var list = str.split(",");
    for (var i = 0; i < list.length; i++) {
        var s = list[i];
        if (array.indexOf(s) == -1) {
            if (check(str, s)) {
                array.push(i + 1, list[i]);
            }
        }
    }
    return array;
}

function get2(str) {
    //为了支持IE8
    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function (elt /*, from*/) {
            var len = this.length >>> 0;

            var from = Number(arguments[1]) || 0;
            from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
            if (from < 0)
                from += len;

            for (; from < len; from++) {
                if (from in this &&
          this[from] === elt)
                    return from;
            }
            return -1;
        };
    }

    var array = new Array();
    var list2 = str.split(",");
    for (var j = 0; j < list2.length; j++) {
        var list = list2[j].split(" ");
        for (var i = 0; i < list.length; i++) {
            var s = list[i];
            if (array.indexOf(s) == -1) {
                if (check(list, s)) {
                    array.push(i + 1, list[i]);
                }
            }
        }
    }
    return array;
}

function check(str, s) {
    var start = str.indexOf(s);
    var n = str.indexOf(s, start + 1);
    if (n > 0) {
        return true;
    }
    return false;
}

//数值格式化函数，Dight要格式化的数字，How要保留的小数位数。
function ForDight(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}

//验证方法
function chkPrice(obj) {
    obj.value = obj.value.replace(/[^\d]/g, "");
    //必须保证第一位为数字而不是. 
    obj.value = obj.value.replace(/^\./g, "");
    //保证只有出现一个.而没有多个. 
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //保证.只出现一次，而不能出现两次以上 
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
}

function chkLast(obj) {
    // 如果出现非法字符就截取掉 
    if (obj.value.substr((obj.value.length - 1), 1) == '.')
        obj.value = obj.value.substr(0, (obj.value.length - 1));
}


//数字转换成大写金额函数
function atoc(numberValue) {

    var obj = parseFloat(numberValue).toFixed(2);
    var numberValue = new String(Math.round(obj * 100)); // 数字金额
    var chineseValue = ""; // 转换后的汉字金额
    var String1 = "零壹贰叁肆伍陆柒捌玖"; // 汉字数字
    var String2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; // 对应单位
    var len = numberValue.length; // numberValue 的字符串长度
    var Ch1; // 数字的汉语读法
    var Ch2; // 数字位的汉字读法
    var nZero = 0; // 用来计算连续的零值的个数
    var String3; // 指定位置的数值
    if (len > 15) {
        alert("超出计算范围");
        return "";
    }
    if (numberValue == 0) {
        chineseValue = "零元整";
        return chineseValue;
    }

    String2 = String2.substr(String2.length - len, len); // 取出对应位数的STRING2的值
    for (var i = 0; i < len; i++) {
        String3 = parseInt(numberValue.substr(i, 1), 10); // 取出需转换的某一位的值
        if (i != (len - 3) && i != (len - 7) && i != (len - 11) && i != (len - 15)) {
            if (String3 == 0) {
                Ch1 = "";
                Ch2 = "";
                nZero = nZero + 1;
            }
            else if (String3 != 0 && nZero != 0) {
                Ch1 = "零" + String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
            else {
                Ch1 = String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
        }
        else { // 该位是万亿，亿，万，元位等关键位
            if (String3 != 0 && nZero != 0) {
                Ch1 = "零" + String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
            else if (String3 != 0 && nZero == 0) {
                Ch1 = String1.substr(String3, 1);
                Ch2 = String2.substr(i, 1);
                nZero = 0;
            }
            else if (String3 == 0 && nZero >= 3) {
                Ch1 = "";
                Ch2 = "";
                nZero = nZero + 1;
            }
            else {
                Ch1 = "";
                Ch2 = String2.substr(i, 1);
                nZero = nZero + 1;
            }
            if (i == (len - 11) || i == (len - 3)) { // 如果该位是亿位或元位，则必须写上
                Ch2 = String2.substr(i, 1);
            }
        }
        chineseValue = chineseValue + Ch1 + Ch2;
    }
    if (String3 == 0) { // 最后一位（分）为0时，加上“整”
		chineseValue = chineseValue + "整";
    }
    return chineseValue;
}

//弹出右下角弹出
var PopInfo = function (str) {
    var html = '';
    html += '<div id="pop" class="tto-popup" style="width:200px;height:200px;">';
    html += '<div class="popup-header">';
    html += '<h3 class="popup-title">温馨提示</h3>';
    html += '<span id="popClose" class="popup-close"><i class="icon-close"></i></span>';
    html += '</div>';
    html += '<div class="popup-body" id="popContent">';
    html += str;
    html += '</div>';
    html += '</div>';

    $("body").append(html);
    audioPlay('paijiang');
    var pop = new Pop("", "#", str);
    setTimeout(function () {
        $("#pop").remove();
        //$('#pop').hide();
    }, 3000);

    $("#pop").find(".icon-close").click(function () {
        $("#pop").remove();
    });
}


function audioPlay(type) {
    if (type == "fengdan") {
        if (getCookie("soundFD") != null) {
            if (getCookie("soundFD") == "1") {
                audioElementHovertree.setAttribute('src', '/statics/mp3/fengdan.mp3');
                audioElementHovertree.play();
            }
        }
        else {
            audioElementHovertree.setAttribute('src', '/statics/mp3/fengdan.mp3');
            audioElementHovertree.play();
        }
    }
    if (type == "info") {
        if (getCookie("soundInfo") != null) {
            if (getCookie("soundInfo") == "1") {
                audioElementHovertree.setAttribute('src', '/statics/mp3/info.mp3');
                audioElementHovertree.play();
            }
        }
        else {
            audioElementHovertree.setAttribute('src', '/statics/mp3/info.mp3');
            audioElementHovertree.play();
        }
    }
    if (type == "kaijiang") {
        if (getCookie("soundKJ") != null) {
            if (getCookie("soundKJ") == "1") {
                audioElementHovertree.setAttribute('src', '/statics/mp3/kaijiang.mp3');
                audioElementHovertree.play();
            }
        }
        else {
            audioElementHovertree.setAttribute('src', '/statics/mp3/kaijiang.mp3');
            audioElementHovertree.play();
        }
    }
    if (type == "paijiang") {
        audioElementHovertree.setAttribute('src', '/statics/mp3/paijiang.mp3');
    }
}

function audioPause() {
    audioElementHovertree.pause();
}