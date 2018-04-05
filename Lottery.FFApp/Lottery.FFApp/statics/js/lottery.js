(function () {
    var lotteryAdd = function () {
        var $lottery = $("#lottery"),
		$lotteryPG = $(".lottery-playgroup", $lottery),
		$lotteryPGItems = $lotteryPG.children("li"),
        $lotteryPanels = $(".lottery-panel", $lottery),
        selected = "selected";
        var $addBtn = $("#betting-add"),
			$bettingMode = $("#betting-mode"),
			$bettingList = $("#betting-list"),
			$container = $("#lottery-container");
        $addBtn.on("click", function () {
            if (site.BetIsOpen == "1") {
                emAlert('系统正在维护不能投注！');
                return false;
            }
            switch (PlayCode) {
                case "R_4DS":
                case "R_3DS":
                case "R_2DS":
                case "R_3HX":
                case "P_5DS":
                case "P_4DS_L":
                case "P_4DS_R":
                case "P_3DS_L":
                case "P_3DS_C":
                case "P_3DS_R":
                case "P_2DS_L":
                case "P_2DS_R":
                case "P_3HX_L":
                case "P_3HX_C":
                case "P_3HX_R":
                case "P_3Z3DS_L":
                case "P_3Z3DS_C":
                case "P_3Z3DS_R":
                case "R_3Z3DS":
                case "P_3Z6DS_L":
                case "P_3Z6DS_C":
                case "P_3Z6DS_R":
                case "R_3Z6DS":
                case "P_2ZDS_L":
                case "P_2ZDS_R":
                case "R_2ZDS":
                    ReplaceNum();
                    break;
                case "P11_RXDS_1":
                case "P11_RXDS_2":
                case "P11_RXDS_3":
                case "P11_RXDS_4":
                case "P11_RXDS_5":
                case "P11_RXDS_6":
                case "P11_RXDS_7":
                case "P11_RXDS_8":
                case "P11_3DS_L":
                case "P11_3ZDS_L":
                case "P11_2DS_L":
                case "P11_2ZDS_L":
                    ReplaceNum2();
                    break;
            }
            if (eval($("#txtUserPoint").val()) >= eval(site.MaxLevel) * 10) {
                emAlert('系统设定，返点大于 ' + site.MaxLevel + ' 的会员不能投注！');
                return false;
            }
            if (playPoints < 0 || eval($("#txtUserPoint").val()) < playPoints) {
                emAlert('返点错误，请重新选择！');
                return false;
            }
            if (PriceTimes <= 0) {
                emAlert('倍数不正确，请从新选择倍数！');
                return false;
            }
            if (parseFloat(PriceTimes) < parseFloat(LotteryMinTimes) || parseFloat(PriceTimes) > parseFloat(LotteryMaxTimes)) {
                emAlert('倍数必须大于' + parseInt(LotteryMinTimes) + '，且小于' + parseInt(LotteryMaxTimes));
                $('#fromTimes').val(parseInt(LotteryMinTimes));
                PriceTimes = parseInt(LotteryMinTimes);
                fromTimesChange();
                return false;
            }
            //            if (SingleTotal == 0) {
            //                emAlert('请选择投注号码!');
            //                return false;
            //            }
            if (Price == "" || Price == "0") {
                emAlert('圆角分错误，请从新选择圆角分！');
                return false;
            }
            if (SingleOrderItem == "") {
                emAlert('投注号码有错或为空！');
                return false;
            }
            if (PlayCode != "PK10_DD1_5" && PlayCode != "PK10_DD6_10" && PlayCode != "P_DD" && PlayCode != "P11_DD") {
                if (parseFloat(PlayMaxNum) < parseFloat(SingleCount)) {
                    emAlert('注数不能大于' + PlayMaxNum + '注');
                    return false;
                }
            }
            var json = {
                "LotteryId": LotteryId,
                "PlayId": PlayId,
                "Price": Price * 1,
                "times": PriceTimes,
                "Num": SingleCount,
                "singelBouns": playBouns,
                "Point": playPoints,
                "balls": SingleOrderItem,
                "strPos": PlayPos,
                "PlayName": PlayName,
                "alltotal": Price * SingleCount * PriceTimes * PriceModel
            };
            ArrayOrder.push(json);


            var $lotteryPGItem = $lotteryPGItems.filter("." + selected),
				pg = $lotteryPGItem.text(),
				pgI = $lotteryPGItems.index($lotteryPGItem),
				$lotteryPanel = $lotteryPanels.eq(pgI),
				$lotteryP = $lotteryPanel.find(".lottery-play"),
				$lotteryPItems = $lotteryP.find("dd"),
				$lotteryPItem = $lotteryPItems.filter("." + selected),
				p = $lotteryPItem.text(),
				pI = $lotteryPItems.index($lotteryPItem),
				$lotterySubPanel = $lotteryPanel.find(".lottery-subpanel").eq(pI),
				$selectedBalls = $lotterySubPanel.find("li:not(.lottery-pos) .lottery-balls").find(".ball." + selected),
				bettingMode = $bettingMode.find("." + selected).text(),
				bettingStr = '',
				$body = $("body");
            $selectedBalls.each(function (i) {
                var $selectedBall = $(this),
						offset = $selectedBall.offset(),
						targetOffset = $bettingList.offset(),
						$moveBall = $("<span class='" + $container.attr("data-cls") + "'></span>").append($selectedBall.clone());
                $moveBall.css({ left: offset.left, top: offset.top, position: 'absolute', zIndex: 1999 });
                if (i > 0) {
                    bettingStr += ',';
                }
                bettingStr += $selectedBall.text();
                $body.append($moveBall);
                setTimeout(function () {
                    $moveBall.animate({ left: targetOffset.left, top: targetOffset.top, opacity: 0.2 }, 300, function () {
                        $moveBall.remove();
                    });
                }, 0);
            });
            var bettingLine = "<li>";
            bettingLine += "<span class='number'>[" + PlayBigName + "-" + PlayName + "] " + SingleOrderItem + "</span>";
            bettingLine += "<span class='zhu'>" + SingleCount + "注</span>";
            bettingLine += "<span class='multi'>" + PriceTimes + "倍</span>";
            bettingLine += "<span class='mode'>" + bettingMode + "</span>";
            bettingLine += "<span class='money'><em>" + (Price * SingleCount * PriceTimes).toFixed(4) + "</em> 元</span>";
            bettingLine += "<span class='oper'><a href='javascript:;' class='del'><i class='icon icon-del'></i></a></span>";
            bettingLine += "</li>";
            $bettingList.append(bettingLine);
            CreateList();
            ajaxAddAfterClear();
        });
    }

    var bettingTabs = function () {
        $("#lottery-tabs").tabs();
    }

    var lotteryBetting = function () {
        var $bettingList = $("#betting-list"),
		$clearBtn = $(".betting-clear");
        $clearBtn.on("click", function () {
            $bettingList.empty();
        });
        $bettingList.delegate(".del", "click", function () {
            $(this).parents("li").remove();
        });
    }

    var init = function () {
        //        lotteryBetting();
        bettingTabs();
        //        lotteryAdd();
    };

    $(function () {
        init();
    });
} ());