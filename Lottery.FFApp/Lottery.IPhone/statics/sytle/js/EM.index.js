$(document).ready(function () {
//    initMoney();
    ajaxIndexLottery();
    new Swiper('.banner-slide', {
        pagination: '.pagination',
        paginationClickable: true,
        loop: true,
        autoplay: 5000,
        autoplayDisableOnInteraction: false,
        useCSS3Transforms: false,
        calculateHeight: true,
        wrapperClass: 'slide-wrapper',
        slideClass: 'slide-item',
        slideActiveClass: 'active',
        slideVisibleClass: 'visible',
        slideDuplicateClass: 'duplicate',
        paginationElement: 'li',
        paginationElementClass: 'swith',
        paginationActiveClass: 'active',
        paginationVisibleClass: 'visible'
    });
});

function initMoney() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/ajax/ajax.aspx?oper=ajaxUserInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { emAlert("亲！页面过期,请刷新页面!"); } },
        success: function (d) {
            if (d.result == "1") {
                Adminname = d.AdminName;
                $i('name').innerHTML = d.AdminName;
                $(".J_UserMoney").each(function () {
                    var $digitRunWrap = $(this),
				            $digitRunItems = $digitRunWrap.children(":gt(0)"),
                            ns = (d.Money + "").split('').reverse().join('').replace(/(\d{1})/g, '$1,').replace(/\,$/, '').split('').reverse().join(''),
				            na = ns.split(",");

                    for (var i = 0; i < na.length; i++) {
                        var $digitRunItem = $digitRunItems.eq(i),
					            dr = new DigitRun($digitRunItem.find(".nc"), na[i]);
                        dr.run();
                    }
                });
            }
        }
    });

}

var DigitRun = function (nc, n) {
    this.nc = $(nc);
    this.ns = this.nc.children();
    this.h = this.ns.eq(0).height();
    this.n = n;
    this.d = 500,
	this.top = this.getPosTop();
}

DigitRun.prototype.getPosTop = function () {
    var index = 0,
		n = this.n;
    this.ns.each(function (i) {
        var $n = $(this);
        if (n == $n.attr("data-n")) {
            index = i;
            return;
        }
    });
    return index * this.h;
}

DigitRun.prototype.run = function () {
    var nc = this.nc,
		d = this.d,
		top = this.top;
    setTimeout(function () {
        nc.animate({ top: -top }, d);
    }, 0);
}

function ajaxIndexLottery() {
    var HotArray = new Array();
    if ($.cookie("HotArray") != null)
        HotArray = JSON.parse($.cookie("HotArray")); //从cookie中还原数组
    if (HotArray.length > 0) {
        var html = '';
        for (var i = 0; i < HotArray.length; i++) {
            html += '<li>';
            html += '<a href="nav.aspx?tid=' + HotArray[i].LotId.substr(0, 1) + '&id=' + HotArray[i].LotId + '">';
            html += '<img src="/statics/sytle/images/logo/' + HotArray[i].LotId + '.png" alt="" /></a>';
            html += '</li>';
        }
        $("#ajaxList").html(html);
    }
    else {
        var u = "/ajax/ajax.aspx?oper=ajaxIndexLottery";
        var index = emLoading();
        $.ajax({
            type: "get",
            dataType: "json",
            data: "&clienttime=" + Math.random(),
            url: u,
            error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
            success: function (d) {
                switch (d.result) {
                    case '-1':
                        emAlert(d.returnval); top.window.location = '/login.html';
                        break;
                    case '1':
                        var html = '';
                        for (var i = 0; i < d.table.length; i++) {
                            html += '<li>';
                            html += '<a href="nav.aspx?tid=' + d.table[i].id.substr(0, 1) + '&id=' + d.table[i].id + '">';
                            html += '<img src="/statics/sytle/images/logo/' + d.table[i].id + '.png" alt="" /></a>';
                            html += '</li>';
                        }
                        $("#ajaxList").html(html);
                        break;
                }
                closeload(index);
                ajaxIndexKaiJiangInfo();
            }
        });
    }
}

function ajaxIndexKaiJiangInfo() {
    var u = "/ajax/ajax.aspx?oper=GetKaiJiangInfo&lid=1001";
    $.ajax({
        type: "get",
        dataType: "json",
        data: "&clienttime=" + Math.random(),
        url: u,
        error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
        success: function (d) {
            switch (d.result) {
                case '1':
                    var strArray = d.table[0].number.split(",");
                    var str = "";
                    for (var i = 0; i < strArray.length; i++) {
                        str += "<span class='n'>" + strArray[i] + "</span>";
                    }
                    $("#kjlist").html(str);
                    break;
            }
        }
    });
}

function countdown(ms, isH, isM, isS, fn) {
    var s = 1000,
		m = 60 * s,
		h = 60 * m,
		finished = false;

    var timer = setInterval(function () {
        ms -= 1000;
        if (ms <= 0) {
            setTime(0);
            clearInterval(timer);
            return;
        }
        setTime(ms);
    }, 1000);

    var setTime = function (ms) {
        if (ms <= 0) {
            finished = true;
            ms = 0;
        }
        var hour = Math.floor(ms / h),
			minute = Math.floor(ms % h / m),
			sec = Math.floor(ms % h % m / s);

        var time = '';
        if (isH) {
            time += zeroPrefix(hour);
        }
        if (isM) {
            if (time.lastIndexOf(':') != (time.length - 1)) {
                time += ':';
            }
            time += zeroPrefix(minute);
        }
        if (isS) {
            if (time.lastIndexOf(':') != (time.length - 1)) {
                time += ':';
            }
            time += zeroPrefix(sec);
        }

        fn(time, finished);
    }

    var zeroPrefix = function (n) {
        if (n <= 9) {
            return "0" + n;
        }
        return n;
    }
    setTime(ms);
}