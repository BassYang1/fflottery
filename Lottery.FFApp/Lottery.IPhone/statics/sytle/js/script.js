;(function(){
$.fn.marquee = function(options){
	var defaults = {
		content:'.marquee',
		distance:1, //距离
		frequency:50, //频率
		offset:0, //开始位置偏移
		direction : 0 //0 水平 1 垂直
	};
	return this.each(function(){
		var vars = $.extend({},defaults,options),
			$container = $(this),
			distance = vars.distance,
			frequency = vars.frequency,
			offset = vars.offset,
			direction = !!vars.direction,
			$marquee = $(vars.content,$container);
		
		$container.css({position:"relative",overflow:"hidden"});
		if(!direction){
			$marquee.css({"float":"left"});
		}
		var width = $container.width(),
			height = $container.height(),
			initLeft = $container.outerWidth() + offset,
			initTop = $container.outerHeight() + offset,
			$marqueeContainer = $("<div style='position:absolute;z-index:9;'></div>");
		if(!direction){
			$marqueeContainer.css({left:initLeft,width:'99999em',height:height});
		}else{
			$marqueeContainer.css({top:initTop,width:width});
		}
		$marqueeContainer.append($marquee);
		$container.append($marqueeContainer);
		
		var marqueeWidth = $marquee.width(),
			marqueeHeight = $marquee.height(),
			timer;
			
		var start = function(){
			timer = setInterval(function(){
				var left = parseFloat($marqueeContainer.css("left"));
				var top = parseFloat($marqueeContainer.css("top"));
				if(!direction){
					if(left < -marqueeWidth){
						left = initLeft;
					}
					left -= distance;
					$marqueeContainer.css({left:left});
				}else{
					if(top < -marqueeHeight){
						top = initTop;
					}
					top -= distance;
					$marqueeContainer.css({top:top});
				}
			},frequency);
		}
		var stop = function(){
			!!timer	&& clearInterval(timer);
			timer = undefined;
		}
		$marquee.on("mouseenter",function(){
			stop();
		}).on("mouseleave",function(){
			start();
		});
		start();
	});
}
$.fn.tabs = function(options){
	var defaults = {
		navCls : 'tabs-nav',
		contentCls : 'tabs-panels',
		triggerType : 'click', // mouse/click
		active : 'active',
		callback : function(){}
	}
	return this.each(function(){
		var vars = $.extend({},defaults,options),
			$this = $(this),
			triggerType = vars.triggerType,
			active = vars.active,
			callback = vars.callback,
			$navItems = $("."+vars.navCls,$this).children(),
			$contentItems = $("."+vars.contentCls,$this).children(),
			initIndex = $this.attr("data-tab-init");
		if(triggerType == "mouse"){
			triggerType = "mouseenter";
		}
		if(isNaN(initIndex)){
			initIndex = 0;	
		}
		$navItems.on(triggerType,function(){
			var $navItem = $(this);
			$navItems.removeClass(active);
			$navItem.addClass(active);
			var i = $navItems.index($navItem);
			$contentItems.hide();
			$contentItems.eq(i).show();
			callback($navItems,$contentItems,i);
		});
		var init = function(){
			$navItems.removeClass(active);
			$navItems.eq(initIndex).addClass(active);
			$contentItems.hide();
			$contentItems.eq(initIndex).show();
			callback($navItems,$contentItems,initIndex);
		}
		init();
	});
}
var countdown = function(ms,isH,isM,isS,fn){
	var s = 1000,
		m = 60 * s,
		h = 60* m,
		finished = false;
	
	var timer = setInterval(function(){
		ms -= 1000;
		if(ms <= 0){
			setTime(0);
			clearInterval(timer);
			return;
		}
		setTime(ms);
	},1000);
	
	var setTime = function(ms){ 
		if(ms <= 0){
			finished = true;
			ms = 0;
		}
		var hour = Math.floor(ms/h),
			minute = Math.floor(ms % h / m),
			sec = Math.floor(ms % h % m / s);
			
		var time = '';
		if(isH){
			time += zeroPrefix(hour);
		}
		if(isM){
			if(time.lastIndexOf(':') != (time.length - 1)){
				time += ':';
			}
			time += zeroPrefix(minute);
		}
		if(isS){
			if(time.lastIndexOf(':') != (time.length - 1)){
				time += ':';
			}
			time += zeroPrefix(sec);
		}
		
		fn(time,finished);
	}
	
	var zeroPrefix = function(n){
		if(n <= 9){
			return "0" + n;	
		}
		return n;
	}
	
	setTime(ms);
}
var LeTian = {
	flipswitch : function(){
		$(".flipswitch").each(function(){
			var $flipswitch = $(this);
			$flipswitch.on("click",function(){
				if($flipswitch.hasClass("flipswitch-active")){
					$flipswitch.removeClass("flipswitch-active");
				}else{
					$flipswitch.addClass("flipswitch-active");
				}
			});
		});		
	},
	countdown : function(){
//		$(".J_Countdown").each(function(){
//			var $countdown = $(this),
//				ms = $countdown.attr("data-ms");
//			countdown(ms,true,true,true,function(time,finished){
//				$countdown.text(time);
//			})
//		});
	},
	bettingAmount : function(){
		$(".betting-amount").each(function(){
			var $amount = $(this),
				$minus = $amount.find(".minus"),
				$plus = $amount.find(".plus"),
				$number = $amount.find(".number");
			$plus.on("click",function(){
				var n = parseInt($number.val());
				$number.val(++n);
				
                PriceTimes = n;
                fromTimesChange();_change(n)
			});
			$minus.on("click",function(){
				if($minus.hasClass("disable")) return;
				var n = parseInt($number.val());
				$number.val(--n);
				_change(n)
			});
			$number.on("blur",function(){
				var n = parseInt($number.val());
				if(isNaN(n)){
					n = 1;	
				}
				n = parseInt(n);
				if(n < 1){
					n = 1;	
				}
				$number.val(n);
				_change(n);
			})
			var _change = function(n){
				if(n > 1){
					$minus.removeClass("disable");	
				}else{
					$minus.addClass("disable");
					if(n < 1){
						$number.val(1);
					}
				}
			}
			var init = function(){
				var n = parseInt($number.val());
				if(isNaN(n)){
					n = 1;
				}
				$number.val(n);
				_change(n);
			}
			init();
		});
	},
	marquee : function(){
		$("#notice .notice-content").marquee();
	},
	tabs : function(){
		$(".lottery-append-tabs").tabs();
	},
	lottery : function(){
		if($("#J_Lottery").size() == 0) return;
		var $lotteryDropdown = $(".J_LotteryDropdown"),
			$lotteryPToggle = $(".J_LotteryPToggle"),
			$selectedPG = $(".J_SelectedPG");
			$selectedP = $(".J_SelectedP");
			$lotteryPG = $(".J_LotteryPG"),
			$lotteryPGItems = $lotteryPG.children();
			$lotteryPs = $(".J_LotteryP"),
			$lotteryPanels = $(".J_LotteryMain .J_LotteryPanel"),
			$lotteryShade = $("<div class='lottery-shade'></div>");
			selected = "selected",
            $lotteryName = $('.J_LotteryName'),
			$lotteryRecords = $('.J_LotteryRecords'),
			$lotteryRecent = $('.J_LotteryRecent');
			
		$lotteryPGItems.each(function(i) {
			var $lotteryPGItem = $(this),
				$pgA = $lotteryPGItem.find("a"),
				$lotteryP = $lotteryPs.eq(i),
				$lotteryPItems = $lotteryP.children(),
				$lotteryPanel = $lotteryPanels.eq(i),
				$lotterySubPanels = $lotteryPanel.children(".J_LotterySubPanel");
			$pgA.on("click",function(){
				$lotteryPGItems.removeClass(selected);
				$lotteryPGItem.addClass(selected);
				$lotteryPs.hide();
				$lotteryP.show();
			});
			$lotteryPItems.each(function(j){
				var $lotteryPItem = $(this),
					$pA = $lotteryPItem.find("a"),
					$lotterySubPanel = $lotterySubPanels.eq(j);
				$pA.on("click",function(){
					$selectedPG.text($pgA.text());
					$selectedP.text($pA.text());
					$lotteryPanels.hide();
					$lotteryPanel.show();
					$lotterySubPanels.hide();
					$lotterySubPanel.show();
					$lotteryDropdown.hide();
					$lotteryShade.hide();
				});
				if(j == 0){
					if(i == 0){
						$selectedP.text($pA.text());
					}
					$lotterySubPanel.show();
				}else{
					$lotterySubPanel.hide();
				}
			});
			if(i == 0){
				$selectedPG.text($pgA.text());
				$lotteryPGItem.addClass(selected);
				$lotteryPanel.show();
				$lotteryP.show();
			}else{
				$lotteryPGItem.removeClass(selected);
				$lotteryPanel.hide();
				$lotteryP.hide();
			}
		});
		
        $lotteryPToggle.on("click",function(){
			$lotteryRecent.css('z-index', 199);
			$lotteryShade.css('z-index', 299);
			if($lotteryDropdown.is(":visible")){
				$lotteryDropdown.hide();
//				$lotteryShade.hide();
			}else{
				$lotteryDropdown.show();
//				$lotteryShade.show();
			}
		});

		
		var chooseLotteryNumbers = function(){
			var _clear = function($balls){
				$balls.removeClass('selected');
			}
			var _all = function($balls){
				$balls.addClass('selected');
			}
			var _big = function($balls){
				_clear($balls);
				var avg = Math.floor($balls.size()/2);
				_all($balls.filter(":gt("+(avg-1)+")"));
			}
			var _small = function($balls){
				_clear($balls);
				var avg = Math.floor($balls.size()/2);
				_all($balls.filter(":lt("+avg+")"));
			}
			var _select = function($ball){
				$ball.addClass('selected');
			}
			var _cancel = function($ball){
				$ball.removeClass('selected');
			}
			var _even = function($balls){
				_clear($balls);
				$balls.each(function(){
					var $ball = $(this),
						number = $ball.attr("data-number");
					if(number % 2 == 0){
						_select($ball);
					}
				});
			}
			var _odd = function($balls){
				_clear($balls);
				$balls.each(function(){
					var $ball = $(this),
						number = $ball.attr("data-number");
					if(number % 2 == 1){
						_select($ball);
					}
				});
			}
			$(".J_LotterySet").each(function(){
				var $lotterySet = $(this);
				$lotterySet.children("li").each(function(){
					var $this = $(this),
						$actionHolder = $this.find(".J_LotteryActionHolder"),
						$ballHolder = $this.find(".J_LotteryBallHolder"),
						$balls = $ballHolder.children();
					$actionHolder.find("[data-action='all']").on("click",function(){
						_all($balls);
					});
					$actionHolder.find("[data-action='big']").on("click",function(){
						_big($balls);
					});
					$actionHolder.find("[data-action='small']").on("click",function(){
						_small($balls);
					});
					$actionHolder.find("[data-action='odd']").on("click",function(){
						_odd($balls);
					});
					$actionHolder.find("[data-action='even']").on("click",function(){
						_even($balls);
					});
					$actionHolder.find("[data-action='clear']").on("click",function(){
						_clear($balls);
					});
					$balls.each(function(){
						var $ball = $(this);
						$ball.on("click",function(){
							if($ball.hasClass(selected)){
								$ball.removeClass(selected);
							}else{
								$ball.addClass(selected);	
							}
						});
					});
				});
			});
		}
		chooseLotteryNumbers();
		$("body").append($lotteryShade);
		$lotteryShade.hide();

        $lotteryName.on('click', function () {
			$lotteryRecent.css('z-index', 599);
			$lotteryShade.css('z-index', 499);
			if ($lotteryRecords.is(':visible')) {
				$lotteryRecords.hide();
//				$lotteryShade.hide();
			} else {
				$lotteryRecords.show();
//				$lotteryShade.show();
			}
		});
	},
	ratioDragbar : function(){
		$(".dragbar").each(function(){
			var $dragbar = $(this),
				$bar = $(".bar",$dragbar),
				$shade = $(".shade",$dragbar),
				$ratio = $dragbar.parent().parent().find(".ratio"),
				$percentage = $(".percentage",$ratio),
				$number = $(".number",$ratio),
				_dw = $dragbar.width(),
				_bw = $bar.outerWidth(),
				_total = 1700,
				_count = 12,
				_aw = _dw / _count,
				_cur = 0,
				startX,
				state;
			var drag = function(e){
				e.preventDefault();
				if("touchstart" == e.type){
					startX = $bar.offset().left + _bw/2; 
					state = true;
				}else if("touchmove" == e.type && state){
					var touch = e.originalEvent.targetTouches[0];
					var moveX = touch.pageX - startX;
					if(Math.abs(moveX) < _aw/2) return;
					var aw = _aw;
					if(moveX < 0){
						aw = -aw;
						_cur -= 1;
					}else{
						_cur += 1;	
					}
					_cur = Math.min(Math.max(_cur,0),_count);
					var left = Math.min(Math.max(parseFloat($bar.css("left")) + aw,0),_dw);
					$shade.width(left);
					$bar.css("left",left);
					startX = $bar.offset().left + _bw/2;
					$percentage.text(_cur+"%");
					$number.text(_total/_count*_cur);
				}else if("touchend" == e.type){
					state = false;
				}
			}
			var init = function(){
				$bar.on("touchstart touchmove touchend",drag);
				$percentage.text(_cur+"%");
				$number.text(_total/_count*_cur);
			}
			init();
        });
	},
	slide : function(){
		new Swiper('.banner-slide',{
			pagination : '.pagination',
			paginationClickable : true,
			loop : true,
			autoplay : 5000,
			autoplayDisableOnInteraction : false,
			useCSS3Transforms : false,
			calculateHeight : true,
			wrapperClass : 'slide-wrapper',
			slideClass : 'slide',
			slideActiveClass : 'active',
			slideVisibleClass : 'visible',
			slideDuplicateClass : 'duplicate',
			paginationElement : 'li',
			paginationElementClass : 'swith',
			paginationActiveClass : 'active',
			paginationVisibleClass : 'visible'
		});
	},
	main : function(){
		this.flipswitch();
//		this.countdown();
//		this.bettingAmount();
		this.marquee();
//		this.tabs();
		this.lottery();
		this.ratioDragbar();
//		this.slide();
	}	
}
$(function(){
	LeTian.main();
});
})();