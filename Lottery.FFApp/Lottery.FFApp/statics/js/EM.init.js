$(function () {
    var flag = 0;
    //圆角分
    $(".optgroup").delegate('li', 'click', function (event) {
        Price = $(this).attr("number");
        PriceName = name;
        fromTimesChange();
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
    $(".betting-amount").delegate('input', 'keyup', function (event) {
        chkPrice(this);
        if ($(this).attr("value") == "" || $(this).attr("value") == "0") {
            return;
        }
        PriceTimes = $('#fromTimes').val();
        fromTimesChange();
    });
    //倍数验证
    $(".betting-amount").delegate('input', 'keypress', function (event) {
        chkPrice(this);
    });
    $(".betting-amount").delegate('input', 'onblur', function (event) {
        chkPrice(this);
    });

    ajaxList();
    $('.lottery-tabs .tabs-nav li').click(function () {
        $(this).parent().find('li').removeClass();
        $(this).addClass('ui-state-active');
        var nmb = $(this).attr("nmb");
        if (nmb == 1)
            ajaxList();
        else
            ajaxZhBetList();
    });
}); 