(function(){
var paymentCountdown = function(){
	var $countdown = $("#payment-countdown");
	if($countdown.length == 0) return;
	var ms = parseInt($countdown.attr("data-ms"));
    Util.countdown(ms,false,true,true,function(time){
        time = time.split(":");
        var str = '';
        for (var i = 0; i < time.length; i++) {
            str += '<span>' + time[i] + '</span>'
        }
        $countdown.html(str);
    });
}
var accountTransfer = function(){
	var $accountTransfer = $("#account-transfer");
	var $transferAccount = $accountTransfer.find(".transfer-account");
	var $outAccount = $transferAccount.find(".out-account");
	var $outItems = $outAccount.children("li");
	var $intoAccount = $transferAccount.find(".into-account");
	var $intoItems = $intoAccount.children("li");
	var selected = "selected";
	var active = "active";

	$outItems.each(function(){
		var $item = $(this);
		$item.on("click", function(){
			$outItems.removeClass(selected);
			$item.addClass(selected);
			$transferAccount.addClass(active);
		});
	});

	$intoItems.each(function(){
		var $item = $(this);
		$item.on("click", function(){
			$intoItems.removeClass(selected);
			$item.addClass(selected);
		});
	});
}
var chooseMoney = function(){
	$(".choose-money").each(function(){
		var $chooseMoney = $(this);
		var $input = $("#" + $chooseMoney.attr("data-input"));
		$chooseMoney.find("li").each(function(){
			var $item = $(this);
			var money = $item.attr("data-money");
			$item.on("click", function(){
				$input.val(money);
			});
		});

	});
}
var accountWithdraw = function(){
	var $accountWithdraw = $("#account-withdraw");
	var $accountType = $accountWithdraw.find(".account-type");
	var $accountBanks = $accountWithdraw.find(".account-bank");
	$accountType.children().each(function(index){
		var $label = $(this);
		var $radio = $label.find(":radio");
		var type = $radio.attr("data-type");
		var $accountBank = $accountBanks.filter("[data-account=" + type + "]");
		$label.on("click", function(){
			$accountBanks.hide();
			$accountBank.show();
		});
		if (index == 0) {
			$accountBank.show();
		}
	})
}
var chooseBank = function(){
	$("#choose-bank").each(function(){
		var $chooseBank = $(this);
		var $banks = $chooseBank.find(".cashier-bank");
		var selected = "selected";
		$banks.each(function(){
			var $bank = $(this),
				$radio = $bank.find(".radio");

			$bank.on("click", function() {
				$banks.removeClass(selected);
				$bank.addClass(selected);
				$radio.prop("checked", true);
			});
		});
	})
}
var latestWinning = function(){
	$(".sports-winning .marquee-holder").each(function(){
		new Marquee($(this),{
			direction : 1
		});
	});
}
var gameSlide = function(){
//	$('#game-winning').flexslider({
//		selector:".winning-list > li",
//		controlNav: false
//	});
}
var focusSlide = function(){
	$("#focus-slide").flexslider({
		selector:".slide-content > .slide-panel",
		directionNav: false
	});
};

var init = function(){
	paymentCountdown();
	accountTransfer();
	chooseMoney();
	accountWithdraw();
	chooseBank();
	latestWinning();
	gameSlide();
	focusSlide();
}

$(function(){
	init();
});
})();