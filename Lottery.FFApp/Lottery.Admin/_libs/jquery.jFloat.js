(function($) { 
	$.fn.jFloat = function(o) {
		o = $.extend({
			imgpath:'/_libs/jquery.jFloat/',
			top:60,  //广告距页面顶部距离
			left:0,//广告左侧距离
			right:0,//广告右侧距离
			width:100,  //广告容器的宽度
			height:360, //广告容器的高度
			minScreenW:1024,//出现广告的最小屏幕宽度，当屏幕分辨率小于此，将不出现对联广告
			position:"left", //对联广告的位置left-在左侧出现,right-在右侧出现
			allowClose:true //是否允许关闭 
		}, o || {});
		var h=o.height;
		var showAd=true;
		var fDiv=$(this);
		if(o.minScreenW>=$(window).width()){
			fDiv.hide();
			showAd=false;
		}
		else{
			fDiv.css("display","block");
			var closeHtml='<div align="right" style="z-index:2000;cursor:pointer;border-bottom:1px solid #f1f1f1; height:16px;background-color: #fff;" class="closeFloat"><img src="'+o.imgpath+'close.gif" border="0" /></div>';
			switch(o.position){
			case "left":
				if(o.allowClose){
					//fDiv.prepend(closeHtml);
					fDiv.append(closeHtml);
					$(".closeFloat",fDiv).click(function(){$(this).hide();fDiv.hide();showAd=false;})
					h+=20;
				}
				fDiv.css({position:"absolute",left:o.left+"px",top:o.top+"px",width:o.width+"px",height:h+"px",overflow:"hidden"});
				break;
			case "right":
				if(o.allowClose){
					//fDiv.prepend(closeHtml);
					fDiv.append(closeHtml);
					$(".closeFloat",fDiv).click(function(){$(this).hide();fDiv.hide();showAd=false;})
					h+=20;
				}
				fDiv.css({position:"absolute",left:"auto",right:o.right+"px",top:o.top+"px",width:o.width+"px",height:h+"px",overflow:"hidden"});
				break;
			};
		};
		function ylFloat(){
			if(!showAd){return}
			var windowTop=$(window).scrollTop();
			if(fDiv.css("display")!="none")
				fDiv.css("top",o.top+windowTop+"px");
		};

		$(window).scroll(ylFloat);
		$(document).ready(ylFloat);     
       
	}; 
})(jQuery);