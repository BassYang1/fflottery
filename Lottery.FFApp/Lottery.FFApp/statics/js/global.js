var Util = {};
(function($){
Util.zeroPrefix = function(n){
	if(n <= 9){
		return "0" + n;	
	}
	return n;
}

Util.countdown = function(ms,isH,isM,isS,fn){
	var s = 1;
	var m = 60 * s;
	var h = 60 * m;
	var fn = typeof fn === "function" ? fn : function(){};
	var end = false;
	
	//console.info(fn);
	
	var format = function(ms){
		var hour = Math.floor(ms / h);
		var minute = Math.floor(ms % h / m);
		var sec = Math.floor(ms % h % m / s);
		var timeStr = "";
		
		if(isH){
			timeStr += Util.zeroPrefix(hour);
		}
		
		if(isM){
			if(timeStr.lastIndexOf(":") != (timeStr.length - 1)){
				timeStr += ":";
			}
			timeStr += Util.zeroPrefix(minute);
		}
		
		if(isS){
			if(timeStr.lastIndexOf(":") != (timeStr.length - 1)){
				timeStr += ":";
			}
			timeStr += Util.zeroPrefix(sec);
		}
		return timeStr;
	}
	
	var timer;
	
	var start = function(){
		timer = setInterval(function(){
			setTime(--ms);
		},1000);
	}
	
	var stop = function(){
		clearInterval(timer);
	}
	
	var setTime = function(ms){
		if(ms <= 0){
			ms = 0;
			end = true;
			stop();
		}
		fn(format(ms),end);
	}
	
	setTime(ms);
	
	start();
}

NumberRun = function(numberRun,number,fn){
	this.numberRun = numberRun;
	this.number = number;
	this.speed = 500,
	this.fn = fn;
	this.init();
}
NumberRun.prototype.init = function(){
	this.numbers = this.numberRun.children();
	this.height = this.numbers.eq(0).height();
	this.top = this.getPosTop();
}
NumberRun.prototype.getPosTop = function(){
	var pos = 0,
		number = this.number;
	this.numbers.each(function(index){
		var $number = $(this);
		if(number == $number.attr("data-number")){
			pos = index;
			return;	
		}
	});
	return pos * this.height;
}
NumberRun.prototype.run = function(){
	var numberRun = this.numberRun,
		top = this.top,
		speed = this.speed,
		fn = this.fn;
	setTimeout(function(){
		numberRun.animate({top:-top},speed,function(){
			if(typeof fn == "function"){
				fn();
			}
		});
	},0);
}

Marquee = function(holder,opts){
	this.holder = holder;
	this.init(opts);
}
Marquee.prototype.init = function(opts){
	var opts = opts ? opts : {};
	this.offset = opts.offset ? opts.offset : 0;
	this.direction = opts.direction ? opts.direction : 0;
	this.speed = opts.speed ? opts.speed : 50;
	this.distance = opts.distance ? opts.distance : 1;
	this.timer;
	this.width = this.holder.width();
	this.height = this.holder.height();
	this.left = this.width + this.offset;
	this.top = this.height + this.offset;
	this.content = this.holder.find(".marquee");
	this.container = $("<div style='position:relative;overflow:hidden;'></div>");
	this.marquee = $("<div style='position:absolute;overflow:hidden;'></div>");		
	this.container.height(this.height);
	if(!this.direction){
		this.marquee.css({
			left : this.left,
			width : '99999em',
			height : this.height
		});	
	}else{
		this.marquee.css({
			top : this.top,
			width : this.width
		});
	}
	this.holder.append(this.container);
	this.container.append(this.marquee);
	this.marquee.append(this.content);
	if(!this.direction){
		this.content.css("float","left");
	}
	this.contentWidth = this.content.outerWidth();
	this.contentHeight = this.content.outerHeight();
	this.bindEvent();
	this.start();
}
Marquee.prototype.move = function(){
	var self = this;
	this.timer = setInterval(function(){
		if(!self.direction){
			var left = parseFloat(self.marquee.css("left"));
			if(left < -self.contentWidth){
				left = self.left;
			}
			left -= self.distance;
			self.marquee.css({left : left});
		}else{
			var top = parseFloat(self.marquee.css("top"));
			if(top < -self.contentHeight){
				top = self.top;
			}
			top -= self.distance;
			self.marquee.css({top : top});
		}
	},this.speed);
}
Marquee.prototype.start = function(){
	this.move();
}
Marquee.prototype.stop = function(){
	clearInterval(this.timer);
	this.timer = undefined;
}
Marquee.prototype.reset = function(){
	this.stop();
	if(!this.direction){
		this.marquee.css({left : this.left});
	}else{
		this.marquee.css({top : this.top});
	}
}
Marquee.prototype.resize = function(){
	var started = this.timer === undefined ? false : true; 
	this.stop();
	this.width = this.holder.outerWidth();
	this.height = this.holder.outerHeight();
	this.left = this.width + this.offset;
	this.top = this.height + this.offset;
	if(started){
		this.start();
	}
}
Marquee.prototype.bindEvent = function(){
	var self = this;
	this.marquee.on("mouseenter",function(){
		self.stop();
	}).on("mouseleave",function(){
		self.start();	
	});
	$(window).on("resize",function(){
		self.resize();
	});
}

Scroll = function(holder,opts){
	this.holder = holder;
	this.init(opts);
}
Scroll.prototype.init = function(opts){
	var opts = opts ? opts : {};
	this.delay = opts.delay ? opts.delay : 3000; // 延迟
	this.speed = opts.speed ? opts.speed : 1000; // 速度
	this.vis = opts.vis ? opts.vis : 1; // 可见个数
	var $items = this.holder.children(); // 滚动元素子元素
	this.length = $items.size(); // 个数
	this.height = $items.eq(0).outerHeight(true); // 子元素高度
	this.timer;
	this.bindEvent();
	this.start();
}
Scroll.prototype.run = function(){
	var self = this;
	this.timer = setInterval(function(){
		self.holder.animate({
			'margin-top': -self.height
		}, self.speed, function() {
			self.holder.css('margin-top', 0);
			self.holder.append(self.holder.children(":first"));
		});
	},this.delay);
}
Scroll.prototype.start = function(){
	this.stop();

	if (this.length > this.vis) {
		this.run();
	}
}
Scroll.prototype.stop = function(){
	clearInterval(this.timer);
	this.timer = undefined;
}
Scroll.prototype.bindEvent = function(){
	var self = this;
	this.holder.on("mouseenter",function(){
		self.stop();
	}).on("mouseleave",function(){
		self.start();
	});
}

var initOptgroup = function(){
	$(".optgroup").each(function(){
        var $optgroup = $(this),
			$options = $optgroup.children(".option");
		$options.on("click",function(){
			$options.removeClass("selected");
			$(this).addClass("selected");
		});
    });
}

var initNav = function () {
	var $toggle = $('#sidebar-toggle'),
		$content = $('#sidebar-content'),
		$sideNav = $('#side-nav'),
		$navGame = $('#nav-game'),
        timer;

    $toggle.on('click', function (e) {
    	e.stopPropagation();
    	if ($content.is(':visible')) {
            $content.hide();
		} else {
            $content.show();
		}
    });

    $sideNav.on('mouseenter', '.lot-game', function () {
        $navGame.show();
    }).on('mouseleave', '.lot-game', function () {
        timer = setTimeout(function () {
            $navGame.hide();
        }, 50);
    });

    $navGame.on('mouseenter', function () {
    	clearTimeout(timer);
        $navGame.show();
    }).on('mouseleave', function () {
        $navGame.hide();
    });

    $(document).on('click', function () {
        if ($content.is(':visible')) {
            $content.hide();
        }
    });
}

var lookLot = function () {
	var $lotNav = $('#lottery-nav');

	$('#look-lot').on('click', function (e) {
		e.stopPropagation();

		var offset = $(this).offset();

		$lotNav.css({
			left: offset.left
		});
		$lotNav.show();
	});

	$lotNav.on('click', function (e) {
		e.stopPropagation();
	});

	$(document).on('click', function () {
		$lotNav.hide();
	});
};

var init = function(){
	initOptgroup();
    initNav();
	lookLot();
}

$(function(){
	init();
});	
})(jQuery);