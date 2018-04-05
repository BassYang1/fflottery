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
        return y + "-" + m + "-" + d + " 00:00:00";
    }
    else {
        var y = dd.getFullYear();
        var m = dd.getMonth() + 1;
        if (m < 10)
            m = "0" + m;
        var d = dd.getDate();
        if (d < 10)
            d = "0" + d;
        return y + "-" + m + "-" + d + " 00:00:00";
    }
}

function GetDateStrTs(AddDayCount) {
    var dd = new Date();
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1;
    var d = dd.getDate();
    if(AddDayCount==30)
    {
        d="1";
    }
    if(AddDayCount==90)
    {
        m=dd.getMonth() - 2;
        d="1";
    }
    if(AddDayCount==360)
    {
        m="1";
        d="1";
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
    return layer.load(1);
    setTimeout(function () {
        layer.close(index);
    }, 100);
}

function closeload(index) {
    setTimeout(function () {
        layer.close(index);
    }, 100);
}

function emAlert(strtitle) {
    var index = layer.msg(strtitle);

    setTimeout(function () {
        layer.close(index);
    }, 3000);
}

function emAlert2(strtitle) {
    var index = layer.open({
        title: '立博国际娱乐提示您',
        content: strtitle
    });
    setTimeout(function () {
        layer.close(index);
    }, 5000); 
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

