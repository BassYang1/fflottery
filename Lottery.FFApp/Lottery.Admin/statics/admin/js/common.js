function setCookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
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

function checkAll() { 
	if ($("#checkedAll").is(":checked")) { // 全选 
		$("input[name='selectID']").each(function() { 
			$(this).attr("checked", true); 
		}); 
	} else { // 取消全选 
		$("input[name='selectID']").each(function() { 
			$(this).attr("checked", false); 
		}); 
	}
}

function checkAllLine() { 
	if ($("#checkedAll").is(":checked")) {  // 全选 
	    $('.query-table tbody tr').each(
			function() {
				$(this).addClass('selected');
				$(this).find('input[type=checkbox]').attr('checked','checked');
			}
		);
	} else { // 取消全选 
    $('.query-table tbody tr').each(
			function() {
				$(this).removeClass('selected');
				$(this).find('input[type=checkbox]').removeAttr('checked');
			}
		);
	}
}

//验证方法
function chkPrice(obj) {
    obj.value = obj.value.replace(/[^\d\.]/g, "");
    //必须保证第一位为数字而不是. 
    obj.value = obj.value.replace(/^\./g, "");
    //保证只有出现一个.而没有多个. 
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //保证.只出现一次，而不能出现两次以上 
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
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

function GetDateShort(AddDayCount) {
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