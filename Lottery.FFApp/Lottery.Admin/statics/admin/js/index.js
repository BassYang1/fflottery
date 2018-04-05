var _height_top = 96;
var _height_bottom = 0;
var _menuid = (q("menuid") != "") ? q("menuid") : "9";
var _menuNum = 0; //初始信息
var _rightWidth = 0;
var _rightHeight = 0;
var tabpanel = null;

$(document).ready(function () {
    ShowLeftMenus();
    setInterval("ajaxPopInfo()", 30000);
    //setInterval("ajaxWarn()", 60000);
    if (getCookie("lock") != null) {
        top.Lottery.Popup.show('adminLock.aspx', 400, 180, false)
    }
});

function resizeHeight() {
    _rightWidth = (_jcms_GetViewportWidth() - $('#side').width() - 1) + "px";
    _rightHeight = (_jcms_GetViewportHeight() - _height_top - 65) + "px";
    $i("workspace").style.width = (_rightWidth - 200) + "px";
    //$i("workspace").style.height = (_jcms_GetViewportHeight() - 65) + "px";
}
function ShowLeftMenus() {
    $('#ajaxMenuBody').html("<div style='width:180px;text-align:center;margin:100px 0px;'><img src='/statics/loading/03.gif' /></div>");
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "/admin/ajax.aspx?oper=leftmenu",
        error: function (XmlHttpRequest, textStatus, errorThrown) {  },
        success: function (d) {
            switch (d.result) {
                case '1':
                    var str = "";
                    for (var i = 0; i < d.table.length; i++) {
                        var t = d.table[i];
                        if (t.sort == "1")
                            str += '<div class="collapsible collapsible-collapsed">';
                        else
                            str += '<div class="collapsible">';
                        str += '<div class="collapsible-toggle"><div class="sn-tit"><i class="icon-n ' + t.css + '"></i>' + t.name + '</div></div>';
                        str += '<div class="collapsible-content"><ul class="sn-list">';
                        for (var j = 0; j < t.table2.length; j++) {
                            var t1 = t.table2[j];
                            str += '<li><a href="javascript:void(0);" onclick="ShowPage(\'' + t.name + '\',\'' + t1.name + '\',\'' + t1.url + '\')"><i class="icon icon-dot"></i>' + t1.name + '</a></li>';
                        }
                        str += '</ul></div></div>';
                    }
                    $("#ajaxMenuBody").html(str);
                    break;
                default:
                    
                    break;
            }
            var CheckboxHolder = function (holder, itemCls, itemAllCls) {
                this.holder = $(holder);
                this.itemCls = itemCls;
                this.itemAllCls = itemAllCls;
                this.init();
            }
            CheckboxHolder.prototype.init = function () {
                this.items = this.holder.find("." + this.itemCls);
                this.itemAll = this.items.filter("." + this.itemAllCls);
                this.total = this.items.size();
                this.size = this.total - this.itemAll.size();
                this.bindEvent();
            }
            CheckboxHolder.prototype.bindEvent = function () {
                var that = this;
                this.items.on("click", function () {
                    that.handle($(this));
                });
            }
            CheckboxHolder.prototype.handle = function (item) {
                if (item.hasClass(this.itemAllCls)) {
                    if (item.prop("checked")) {
                        this.items.prop("checked", true);
                    } else {
                        this.items.prop("checked", false);
                    }
                } else {
                    if (this.items.filter(":checked").not(this.itemAll).size() == this.size) {
                        this.itemAll.prop("checked", true);
                    } else {
                        this.itemAll.prop("checked", false);
                    }
                }
            }
            var Collapsible = function (holder, itemCls, itemToggleCls, itemConCls, itemActiveCls) {
                this.holder = $(holder);
                this.itemCls = itemCls;
                this.itemToggleCls = itemToggleCls;
                this.itemConCls = itemConCls;
                this.itemActiveCls = itemActiveCls;
                this.init();
            }
            Collapsible.prototype.init = function () {
                var that = this;
                this.items = this.holder.find("." + this.itemCls);
                this.items.each(function () {
                    var item = $(this),
			itemToggle = item.find("." + that.itemToggleCls),
			itemCon = item.find("." + that.itemConCls);
                    itemToggle.on("click", function () {
                        that.handle(item);
                    });
                });
            }
            Collapsible.prototype.handle = function (item) {
                var itemActiveCls = this.itemActiveCls;
                if (item.hasClass(itemActiveCls)) {
                    item.removeClass(itemActiveCls);
                } else {
                    this.items.removeClass(itemActiveCls);
                    item.addClass(itemActiveCls);
                }
            }

            var initQueryTable = function () {
                new CheckboxHolder(".query-table", "chkbox", "check-all");
            }

            var initCollapsible = function () {
                new Collapsible(".collapsible-set", "collapsible", "collapsible-toggle", "collapsible-content", "collapsible-collapsed");
            }

            var initBackTop = function () {
                $(".back-top").on("click", function () {
                    $("html,body").animate({ scrollTop: 0 }, 300);
                });
            }

            var initMTree = function () {
                $(".mtree .uname").each(function () {
                    var $uname = $(this),
			$next = $uname.next();
                    $uname.on("click", function () {
                        if ($next.is(":visible")) {
                            $next.hide();
                        } else {
                            $next.show();
                        }
                    });
                });
            }

            var init = function () {
                initQueryTable();
                initCollapsible();
                initBackTop();
                initMTree();
            }

            $(function () {
                init();
            })
        }
    });
}

function ShowPage(parent, title, url) {
    $i("parent").innerHTML = parent;
    $i("title").innerHTML = title;
    $i("title2").innerHTML = title;

    if (title == "系统设置")
        $i("workspace").style.height = 1565+ "px";
    else
        $i("workspace").style.height = 850 + "px";
    $i("workspace").src = url;
}

/*实时获取当前iframe的ID*/
function getCurrentIframe() {
    return eval('workspace');
}

//获取即时信息
function ajaxPopInfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "ajax.aspx?oper=ajaxPopInfo",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { } }, //alert("网络堵塞,稍后再试！");
        success: function (d) {
            $i('usercount').innerHTML = d.usercount;
            $i('usercount2').innerHTML = d.usercount2;
            $i('cashcount').innerHTML = d.cashcount;
            $i('cashcount2').innerHTML = d.cashcount;
//            if (d.cashcount != "0") {
//                var pop = new Pop(d.title, "#", "您有" + d.cashcount + "笔提现请求，请您及时处理！");
//                //document.getElementById('MUSIC1').play();
//            }
//            else {
//                $('#pop').hide();
//                //document.getElementById('MUSIC1').stop();
//            }
        }
    });
}

function ajaxWarn() {
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: "ajaxWarn.aspx?oper=ajaxWarnCount",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") {  } },
        success: function (d) {
            $i('c1').innerHTML = d.table[0].num;
            $i('c2').innerHTML = d.table[1].num;
            $i('c3').innerHTML = d.table[2].num;
            $i('c4').innerHTML = d.table[3].num;
            $i('c5').innerHTML = d.table[4].num;
            $i('c6').innerHTML = d.table[5].num;
            $i('c7').innerHTML = d.table[6].num;
            $i('c8').innerHTML = d.table[7].num;
            $i('WarnCount').innerHTML = eval(d.table[0].num) + eval(d.table[1].num) + eval(d.table[2].num) + eval(d.table[3].num) + eval(d.table[4].num) + eval(d.table[5].num) + eval(d.table[6].num) + eval(d.table[7].num);
            $i('WarnCount2').innerHTML = eval(d.table[0].num) + eval(d.table[1].num) + eval(d.table[2].num) + eval(d.table[3].num) + eval(d.table[4].num) + eval(d.table[5].num) + eval(d.table[6].num) + eval(d.table[7].num);
        }
    });
}