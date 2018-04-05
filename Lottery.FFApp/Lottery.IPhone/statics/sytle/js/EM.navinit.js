$(function () {
    //圆角分
    $(".pa").delegate('li', 'click', function (event) {
        Price = $(this).attr("number");
        $(".pa li").each(function (i, n) {
            $(this).removeClass().addClass("n" + $(n).attr("nmb"));
        });
        $(this).removeClass().addClass("on");
        PriceName = name;
        fromTimesChange();
    });
    //示例弹出
    $(".tz_style_div10").delegate('#example', 'click', function (event) {
        layer.tips(PlayExample, this, {
            tips: [1, '#3595CC'],
            maxWidth: 300,
            time: 5000,
            closeBtn: [0, true]
        });
    });
    //帮助弹出
    $(".tz_style_div10").delegate('#help', 'click', function (event) {
        layer.tips(PlayHelp, this, {
            tips: [1, '#3595CC'],
            maxWidth: 300,
            time: 5000,
            closeBtn: [0, true]
        });
    });
    //倍数加
    $(".plus").delegate('', 'click', function (event) {
        values = $('#fromTimes').val();
        values++;
        PriceTimes = values;
        $('#fromTimes').val(values);
        fromTimesChange();
    });
    //倍数减
    $(".minus").delegate('', 'click', function (event) {
        values = $('#fromTimes').val();
        values--;
        if (values < 1) {
            values = 1;
        }
        PriceTimes = values;
        $('#fromTimes').val(values);
        fromTimesChange();
    });
    //倍数变化事件
    $("#fromTimes").delegate('', 'keyup', function (event) {
        chkPrice(this);
        if ($(this).attr("value") == "" || $(this).attr("value") == "0") {
            return;
        }
        PriceTimes = $('#fromTimes').val();
        fromTimesChange();
    });
    //追号点击
    $("#chkadd").delegate('', 'click', function (event) {
        var obj = document.getElementsByName("chkstop");
        $('.c-5').hide();
        if (this.checked) {
            if (SumCount == 0) {
                $('.c-5').eq(0).show();
                emAlert('请选择投注号码后再进行追号');
                return false;
            }
            else {
                obj[0].checked = true;
                $('.c-5').eq(1).show();
                lid = "&lid=" + LotteryId;
                ajaxIssueNum(0);
            }
        } else {
            $('.c-5').eq(0).show();
        }
    });
    //追号方式切换
    $(".abc li").delegate('a', 'click', function (event) {
        NmbZH = $(this).attr("nmbzz");
        $(".abc li a").each(function (i, n) {
            $(this).removeClass("on").addClass("n" + $(n).attr("nmbzz"));
        });
        $(this).removeClass("on").addClass("on fl");

        var tb = document.getElementById("divtb");
        var fb = document.getElementById("divfb");
        if (NmbZH == 1) {
            tb.style.display = "block";
            fb.style.display = "none";
        }
        if (NmbZH == 2) {
            tb.style.display = "none";
            fb.style.display = "block";
        }
    });

    //追号倍数变化事件
    $("#history_income_list").delegate('input', 'keyup', function (event) {
        chkPrice(this);
        var sum = SumZhTotal * 1;
        var obj3 = document.getElementsByName($(this).parents("tr").find("span").eq(2).attr("id"));
        sum = sum - obj3[0].innerHTML * 1;
        obj3[0].innerHTML = (PriceTotal * $(this).attr("value")).toFixed(2);
        sum = sum + obj3[0].innerHTML * 1;
        $i("spanSumTotal").innerHTML = SumZhTotal = (sum).toFixed(2);
    });
    //倍数验证
    $("#history_income_list").delegate('input', 'keypress', function (event) {
        chkPrice(this);
    });
    $("#history_income_list").delegate('input', 'onblur', function (event) {
        chkPrice(this);
    });

    $("#history_div").hover(function () {
        $(this).find('.history_list').addClass('show_solid_div_menu');
        $(this).find('.history_list').show();
    }, function () {
        $(this).find('.history_list').removeClass('show_solid_div_menu');
        $(this).find('.history_list').hide();
    });

    $(".history_list").hover(function () {
        $(this).addClass('show_solid_div_menu');
        $(this).show();
    }, function () {
        $(this).removeClass('show_solid_div_menu');
        $(this).hide();
    });
});