function CalcSingleOrderItem() {
    var rowsLen = $(".numbers").length;
    var orderItem = '';
    var selectCount = 0;

    for (var i = 0; i < rowsLen; i++) {
        selectCount = 0;

        $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
            orderItem += $(dom).attr("number") + "_"; //当成多注来处理
            selectCount++;
        });
        orderItem = orderItem.substring(0, orderItem.length - 1);
    }

    return orderItem;
}

function CalcSingleOrderItem2() {
    var rowsLen = $(".numbers").length;
    var orderItem = '';
    var selectCount = 0;

    for (var i = 0; i < rowsLen; i++) {
        selectCount = 0;

        $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
            orderItem += $(dom).attr("number") + "_";
            selectCount++;
        });

        orderItem = orderItem.substring(0, orderItem.length - 1);

        if (i != rowsLen - 1) {
            orderItem += ',';
        }

        if(selectCount == 0){
            return "";
        }
    }

    return orderItem;
}

function CalcSingleOrderItem3() {
    var rowsLen = $(".numbers").length;
    var orderItem = '';
    var selectCount = 0;

    for (var i = 0; i < rowsLen; i++) {
        selectCount = 0;

        $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
            selectCount++;
            orderItem += $(dom).attr("number") + "_";
        });

        if (selectCount != 0) {
            orderItem = orderItem.substring(0, orderItem.length - 1);
        }

        if (i != rowsLen - 1) {
            orderItem += ',';
        }
    }

    return orderItem;
}

function AutoCalcBet() {
    var rowsLen = $(".numbers").length;
    var orderItem = '';
    var selectCount = '';
    var Znum = 0;
    SingleOrderItem = "";
    PlayPos = "";
    var obj = document.getElementsByName("wz");
    for (var ii = 0; ii < obj.length; ii++) {
        if (obj[ii].checked)
            PlayPos += obj[ii].value + ',';
        else {
            PlayPos += '0,';
        }
    }
    PlayPos = PlayPos.substring(0, PlayPos.length - 1);
    switch (PlayCode) {
        case "H_ZMPTX1":
        case "H_ZMPTX2":
        case "H_ZMPTX3":
        case "H_ZMPTX4":
        case "H_ZMPTX5":
            SingleOrderItem = CalcSingleOrderItem();
            Znum = RedCommon4(SingleOrderItem, parseInt(PlayCode.replace("H_ZMPTX", "")));
            break;
        case "H_ZMP1Z1":
        case "H_ZMP2Z2":
        case "H_ZMP3Z2X3":
        case "H_ZMP3Z3":
        case "H_ZMP4Z4":
            var msg = "";
            var balls = 0;
            SingleOrderItem = CalcSingleOrderItem();
            Znum = RedCommon4(SingleOrderItem, balls);
            var selectedNum = SingleOrderItem.split("_").length;
            Znum = 0;

            switch (PlayCode) {
                case "H_ZMP1Z1": balls = 0;
                    balls = 1;
                    break;
                case "H_ZMP2Z2":
                    balls = 2;
                    break;
                case "H_ZMP3Z2X3":
                    balls = 3;
                    break;
                case "H_ZMP3Z3":
                    balls = 3;
                    if (selectedNum > 15) {
                        msg = "注数过多，只允许选择15个球";
                    }
                    break;
                case "H_ZMP4Z4":
                    balls = 4;
                    if (selectedNum > 15) {
                        msg = "注数过多，只允许选择15个球";
                    }
                    break;
                default:
                    break;
            }

            if (msg != "") {
                emAlert(msg);
            }
            else {
                Znum = RedCommon4(SingleOrderItem, balls);
            }

            break;
        case "H_BZ5":
        case "H_BZ6":
        case "H_BZ7":
        case "H_BZ8":
        case "H_BZ9":
        case "H_BZ10":
        case "H_BZ11":
        case "H_BZ12":
        case "H_BZ15":
            var msg = "";
            var balls = parseInt(PlayCode.replace("H_BZ", ""));
            SingleOrderItem = CalcSingleOrderItem();
            var selectedNum = SingleOrderItem.split("_").length;
            Znum = 0;

            switch (balls) {
                case 5:
                case 6:
                case 7:
                    if (selectedNum > 11) {
                        msg = "注数过多，只允许选择11个球";
                    }
                    break;
                case 8:
                case 9:
                    if (selectedNum > 12) {
                        msg = "注数过多，只允许选择12个球";
                    }
                    break;
                case 10:
                    if (selectedNum > 13) {
                        msg = "注数过多，只允许选择13个球";
                    }
                    break;
                case 11:
                    if (selectedNum > 14) {
                        msg = "注数过多，只允许选择14个球";
                    }
                    break;
                case 12:
                    if (selectedNum > 15) {
                        msg = "注数过多，只允许选择15个球";
                    }
                    break;
                case 15:
                    if (selectedNum > 17) {
                        msg = "注数过多，只允许选择17个球";
                    }
                    break;
                default:
                    break;
            }

            if (msg != "") {
                emAlert(msg);
            }
            else {
                Znum = RedCommon4(SingleOrderItem, balls);
            }

            break;
        case "H_ZMDX":
        case "H_ZMDS":
        case "H_ZMHSDX":
        case "H_ZMHSDS":
        case "H_ZMSBH":
        case "H_ZMSBLAN":
        case "H_ZMSBLV":
        case "H_ZMWSDX":
            SingleOrderItem = CalcSingleOrderItem3();
            Znum = RedCommon3(SingleOrderItem);
            break;
        case "H_TMBT":
        case "H_TMBB":
        case "H_TMBBB":
            SingleOrderItem = CalcSingleOrderItem2();
            Znum = RedCommon2(SingleOrderItem);
            break;
        case "H_TM": //六合彩，特码
        case "H_TMDX":
        case "H_TMDS":
        case "H_TMHDX":
        case "H_TMHDS":
        case "H_TMWDX":
        case "H_TMWDS":
        case "H_TMTS":
        case "H_TMWS":
        case "H_TMSB":
        case "H_TMSX":
        case "H_SXZX":
        case "H_SXZXDS":
        case "H_ZHDX":
        case "H_ZHDS":
            SingleOrderItem = CalcSingleOrderItem();
            Znum = RedCommon(SingleOrderItem);
            break;
        case "P_DX_W": //时时彩，猜大小
        case "P_DX_Q":
        case "P_DX_B":
        case "P_DX_S":
        case "P_DX_G":
        case "P_DS_W": //时时彩，猜单双
        case "P_DS_Q":
        case "P_DS_B":
        case "P_DS_S":
        case "P_DS_G":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_"; //当成多注来处理
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = selectCount;
            break;
        case "K_3HZ":
        case "K_3STDX":
        case "K_32TDX":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_"; //当成多注来处理
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedK3HZ(SingleOrderItem);
            break;
        case "K_32BT": //二不同直选
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_"; //当成多注来处理
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedK32BT(SingleOrderItem);
            break;
        case "K_33BT":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_"; //当成多注来处理
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedK33BT(SingleOrderItem);
            break;
        case "K_33LTX": //三连号通选
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_"; //当成多注来处理
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedK33LTX(SingleOrderItem);
            break;
        case "K_3STTX":
        case "K_32TTX":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_"; //当成多注来处理
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = 1;
            break;
        case "P_5ZX120":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu120(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_5ZX60":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu60(SingleOrderItem);
            break;
        case "P_5ZX30":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu30(SingleOrderItem);
            break;
        case "P_5ZX20":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu20(SingleOrderItem);
            break;
        case "P_5ZX10":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu10(SingleOrderItem);
            break;
        case "P_5ZX5":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu5(SingleOrderItem);
            break;
        case "P_5TS1":
        case "P_5TS2":
        case "P_5TS3":
        case "P_5TS4":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedTS(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_4ZX24":
        case "R_4ZX24":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZu24(SingleOrderItem.replace(/_/g, "")) * Combine((PlayPos.split('1')).length - 1, 4);
            else
                Znum = RedZu24(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_4ZX12":
        case "R_4ZX12":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZu12(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 4);
            else
                Znum = RedZu12(SingleOrderItem);
            break;
        case "P_4ZX6":
        case "R_4ZX6":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZu61(SingleOrderItem.replace(/_/g, "")) * Combine((PlayPos.split('1')).length - 1, 4);
            else
                Znum = RedZu61(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_4ZX4":
        case "R_4ZX4":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZu4(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 4);
            else
                Znum = RedZu4(SingleOrderItem);
            break;

        case "P_5FS":
        case "P_4FS_L":
        case "P_4FS_R":
        case "P_3FS_L":
        case "P_3FS_C":
        case "P_3FS_R":
        case "P_2FS_L":
        case "P_2FS_R":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedFS(SingleOrderItem);
            break;
        case "R_4FS":
        case "R_3FS":
        case "R_2FS":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedFS_R(SingleOrderItem, PlayPos);
            break;

        case "P_3Z3_L":
        case "P_3Z3_C":
        case "P_3Z3_R":
        case "R_3Z3":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                    selectCount++;
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZu3(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 3);
            else
                Znum = RedZu3(SingleOrderItem);
            break;
        case "P_3Z6_L":
        case "P_3Z6_C":
        case "P_3Z6_R":
        case "R_3Z6":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                    selectCount++;
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZu6(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 3);
            else
                Znum = RedZu6(SingleOrderItem);
            break;
        case "P_2Z2_L":
        case "P_2Z2_R":
        case "R_2Z2":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                    selectCount++;
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZu2(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 2);
            else
                Znum = RedZu2(SingleOrderItem);
            break;

        case "P_5DS":
        case "P_4DS_L":
        case "P_4DS_R":
        case "P_3DS_L":
        case "P_3DS_C":
        case "P_3DS_R":
        case "P_2DS_L":
        case "P_2DS_R":
        case "P_2ZDS_L":
        case "P_2ZDS_R":
        case "P_3HX_L":
        case "P_3HX_C":
        case "P_3HX_R":
        case "P_3Z3DS_L":
        case "P_3Z6DS_L":
        case "P_3Z3DS_C":
        case "P_3Z6DS_C":
        case "P_3Z3DS_R":
        case "P_3Z6DS_R":
        case "R_4DS":
        case "R_3DS":
        case "R_2DS":
        case "R_3HX":
        case "R_3Z3DS":
        case "R_3Z6DS":
        case "R_2ZDS":
            var orderItem = "";
            $i("message").innerHTML = '';
            var allText = $("#inputtext").val().replace(/(^\s*)|(\s*$)/g, "");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{" + PlayRandoms + "}$/;");
            var cutRegexp = /[\s;\,\，　]+/;
            var numberArray = allText.split(cutRegexp);
            for (var i = 0; i < numberArray.length; i++) {
                numberArray[i] = numberArray[i].replace("\n", '');
                if (numberArray[i] == '') {
                    continue;
                }
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var tmpNumber = numberArray[i].split('');
                    var aNumber = '';
                    for (var j = 1; j <= tmpNumber.length; j++) {
                        if (j == tmpNumber.length)
                            aNumber += tmpNumber[j - 1];
                        else
                            aNumber += tmpNumber[j - 1];
                    }

                    orderItem += aNumber;
                    if (i != numberArray.length - 1) {
                        orderItem += ',';
                    }
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1);
            SingleOrderItem = orderItem.toString();

            if (PlayPos != "")
                Znum = RedDS_R(SingleOrderItem, PlayPos);
            else
                Znum = RedDS(SingleOrderItem);
            //排序去除重复
            //            numberArray = numberArray.sort(function (a, b) {
            //                return a - b;
            //            });
            //            var arr = orderItem.split(",");
            //            var repstr = CheckReplace(orderItem);
            //            if (repstr != "") {
            //                for (var i = 0; arr.length > i; i++) {
            //                    for (var j = i + 1; j < arr.length; j++) {
            //                        if (arr[j] == arr[i]) {
            //                            arr.splice(j, 1);
            //                            j--;
            //                        }
            //                    }
            //                }
            //            }
            break;
        case "P_3HE_L":
        case "P_3HE_C":
        case "P_3HE_R":
        case "R_3HE":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedHE3(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 3);
            else
                Znum = RedHE3(SingleOrderItem);
            break;

        case "P_3ZHE_L":
        case "P_3ZHE_C":
        case "P_3ZHE_R":
        case "R_3ZHE":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZHE3(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 3);
            else
                Znum = RedZHE3(SingleOrderItem);
            break;

        case "P_3KD_L":
        case "P_3KD_C":
        case "P_3KD_R":
        case "R_3KD":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = Red3KD(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 3);
            else
                Znum = Red3KD(SingleOrderItem);
            break;

        case "P_3ZBD_L":
        case "P_3ZBD_C":
        case "P_3ZBD_R":
        case "R_3ZBD":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            if (selectCount > 1) {
                emAlert('最多只能选择一个号码！');
                $(".lottery-numbers").find(".lottery-balls").find("span[number]").removeClass().addClass("ball J_Ball");
            }
            else {
                SingleOrderItem = orderItem;
                if (PlayPos != "")
                    Znum = selectCount * 54 * Combine((PlayPos.split('1')).length - 1, 3);
                else
                    Znum = selectCount * 54;
            }
            break;
        case "P_3QTWS_L":
        case "P_3QTWS_C":
        case "P_3QTWS_R":
        case "R_3QTWS":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedDD(SingleOrderItem.replace(/_/g, "")) * Combine((PlayPos.split('1')).length - 1, 3);
            else
                Znum = RedDD(SingleOrderItem.replace(/_/g, ""));
            break;

        case "P_3QTTS_L":
        case "P_3QTTS_C":
        case "P_3QTTS_R":
        case "R_3QTTS":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedDD(SingleOrderItem.replace(/_/g, "")) * 0.5 * Combine((PlayPos.split('1')).length - 1, 3);
            else
                Znum = RedDD(SingleOrderItem.replace(/_/g, "")) * 0.5;
            break;

        case "P_2HE_L":
        case "P_2HE_R":
        case "R_2HE":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedHE2(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 2);
            else
                Znum = RedHE2(SingleOrderItem);
            break;

        case "P_2KD_L":
        case "P_2KD_R":
        case "R_2KD":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = Red2KD(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 2);
            else
                Znum = Red2KD(SingleOrderItem);
            break;
        case "P_2ZHE_L":
        case "P_2ZHE_R":
        case "R_2ZHE":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            if (PlayPos != "")
                Znum = RedZHE2(SingleOrderItem) * Combine((PlayPos.split('1')).length - 1, 2);
            else
                Znum = RedZHE2(SingleOrderItem);
            break;
        case "P_2ZBD_L":
        case "P_2ZBD_R":
        case "R_2ZBD":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            if (selectCount > 1) {
                emAlert('最多只能选择一个号码！');
                $(".lottery-numbers").find(".lottery-balls").find("span[number]").removeClass().addClass("ball J_Ball");
            }
            else {
                SingleOrderItem = orderItem;
                if (PlayPos != "")
                    Znum = selectCount * 9 * Combine((PlayPos.split('1')).length - 1, 2);
                else
                    Znum = selectCount * 9;
            }
            break;
        case "P_DD":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedDD(SingleOrderItem);
            break;
        case "P_3BDD1_R":
        case "P_3BDD1_L":
        case "P_4BDD1":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                    selectCount++;
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedDD(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_3BDD2_R":
        case "P_3BDD2_L":
        case "P_4BDD2":
        case "P_5BDD2":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                    selectCount++;
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu2(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_5BDD3":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                    selectCount++;
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedZu6(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_2DXDS_L":
        case "P_2DXDS_R":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                if (orderItem.substring(orderItem.length - 1) == "_")
                    orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedFS(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_LHH_WQ":
        case "P_LHH_WB":
        case "P_LHH_WS":
        case "P_LHH_WG":
        case "P_LHH_QB":
        case "P_LHH_QS":
        case "P_LHH_QG":
        case "P_LHH_BS":
        case "P_LHH_BG":
        case "P_LHH_SG":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                if (orderItem.substring(orderItem.length - 1) == "_")
                    orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedFS(SingleOrderItem.replace(/_/g, ""));
            break;
        case "P_5QJ3":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            var arr = orderItem.split(",");
            if (arr.length == 5) {
                SingleOrderItem = orderItem;
                Znum = RedQwQj(SingleOrderItem);
            }
            break;
        case "P_4QJ3":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            var arr = orderItem.split(",");
            if (arr.length == 4) {
                SingleOrderItem = orderItem;
                Znum = RedQwQj(SingleOrderItem);
            }
            break;
        case "P_3QJ2_L":
        case "P_3QJ2_R":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            var arr = orderItem.split(",");
            if (arr.length == 3) {
                SingleOrderItem = orderItem;
                Znum = RedQwQj(SingleOrderItem);
            }
            break;
        case "P_5QW3":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            var arr = orderItem.split(",");
            if (arr.length == 5) {
                SingleOrderItem = orderItem;
                Znum = RedFS(SingleOrderItem.replace(/_/g, ""));
            }
            break;
        case "P_4QW3":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            var arr = orderItem.split(",");
            if (arr.length == 4) {
                SingleOrderItem = orderItem;
                Znum = RedFS(SingleOrderItem.replace(/_/g, ""));
            }
            break;
        case "P_3QW2_L":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            var arr = orderItem.split(",");
            if (arr.length == 3) {
                SingleOrderItem = orderItem;
                Znum = RedFS(SingleOrderItem.replace(/_/g, ""));
            }
            break;
        case "P_3QW2_R":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            var arr = orderItem.split(",");
            if (arr.length == 3) {
                SingleOrderItem = orderItem;
                Znum = RedFS(SingleOrderItem.replace(/_/g, ""));
            }
            break;

        case "P_5ZH": for (var i = 0; i < rowsLen; i++) {
            $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                orderItem += $(dom).attr("number") + "";
            });
            if (i != rowsLen - 1) {
                orderItem += ',';
            }
        }
            SingleOrderItem = orderItem;
            Znum = Red5ZuHe(SingleOrderItem);
            break;
        case "P_4ZH_L":
        case "P_4ZH_R":
            for (var i = 0; i < rowsLen; i++) {
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "";
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = Red4ZuHe(SingleOrderItem);
            break;
        case "P_3ZH_L":
        case "P_3ZH_C":
        case "P_3ZH_R": for (var i = 0; i < rowsLen; i++) {
            $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                orderItem += $(dom).attr("number") + "";
            });
            if (i != rowsLen - 1) {
                orderItem += ',';
            }
        }
            SingleOrderItem = orderItem;
            Znum = Red3ZuHe(SingleOrderItem);
            break;


        //11选5                            
        case "P11_RXDS_1":
            $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXDS_2": $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXDS_3": $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11 && parseInt(temp[2]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXDS_4": $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11 && parseInt(temp[2]) <= 11 && parseInt(temp[3]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXDS_5": $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11 && parseInt(temp[2]) <= 11 && parseInt(temp[3]) <= 11
                    && parseInt(temp[4]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXDS_6": $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11 && parseInt(temp[2]) <= 11 && parseInt(temp[3]) <= 11
                    && parseInt(temp[4]) <= 11 && parseInt(temp[5]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXDS_7": $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();

                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11 && parseInt(temp[2]) <= 11 && parseInt(temp[3]) <= 11
                    && parseInt(temp[4]) <= 11 && parseInt(temp[5]) <= 11 && parseInt(temp[6]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXDS_8": $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();
                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11 && parseInt(temp[2]) <= 11 && parseInt(temp[3]) <= 11
                    && parseInt(temp[4]) <= 11 && parseInt(temp[5]) <= 11 && parseInt(temp[6]) <= 11 && parseInt(temp[7]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_RXFS_1":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 1);
            break;
        case "P11_RXFS_2":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 2);
            break;
        case "P11_RXFS_3":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 3);
            break;
        case "P11_RXFS_4":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 4);
            break;
        case "P11_RXFS_5":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 5);
            break;
        case "P11_RXFS_6":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 6);
            break;
        case "P11_RXFS_7":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 7);
            break;
        case "P11_RXFS_8":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 8);
            break;
        case "P11_3FS_L":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = Red3FS_11(SingleOrderItem);
            break;
        case "P11_3ZFS_L":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = Red3ZFS_11(SingleOrderItem);
            break;

        case "P11_2FS_L":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = Red2FS_11(SingleOrderItem);
            break;

        case "P11_2ZFS_L":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = Red2ZFS_11(SingleOrderItem);
            break;
        case "P11_3DS_L":
        case "P11_3ZDS_L":
            $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            //allText = allText.replace(/(^\s*)|(\s*$)/g, "");
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();

                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码  
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11 && parseInt(temp[2]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_2DS_L":
        case "P11_2ZDS_L":
            $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();

                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 11 && parseInt(temp[1]) <= 11) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;
        case "P11_DD":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                if (orderItem.substring(orderItem.length - 1) == "_")
                    orderItem = orderItem.substring(0, orderItem.length - 1);
                orderItem += ",";
            }
            if (orderItem.split(',').length > 3)
                orderItem = orderItem.substring(0, orderItem.length - 1)
            SingleOrderItem = orderItem;
            Znum = RedDD_11(SingleOrderItem);
            break;
        case "P11_BDD_L":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 1);
            break;
        case "P11_CDS":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 1);
            break;
        case "P11_CZW":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
            }
            SingleOrderItem = orderItem;
            Znum = RedRXFS_11(SingleOrderItem, 1);
            break;
        case "P_DD_3":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number");
                });
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedDD(SingleOrderItem);
            break;

        //PK10             
        case "PK10_One":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = PK10FS_One(SingleOrderItem);
            break;

        case "PK10_TwoFS":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = Red2FS_11(SingleOrderItem);
            break;

        case "PK10_TwoDS":
            $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();

                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}$/;"); //号码    
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 10 && parseInt(temp[1]) <= 10 && parseInt(temp[0]) != parseInt(temp[1])) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;

        case "PK10_ThreeFS":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = Red3FS_11(SingleOrderItem);
            break;

        case "PK10_ThreeDS":
            $i("message").innerHTML = '';
            var allText = $("#inputtext").val();
            //allText = allText.replace(/(^\s*)|(\s*$)/g, "");
            allText = allText.replace(/\,/g, "\n");
            if (allText == "") {
                ajaxAddAfterClear();

                return false;
            }
            eval("var regexp = /^[0-9]{2}[ ][0-9]{2}[ ][0-9]{2}$/;"); //号码  
            var numberArray = allText.split("\n");
            var orderItem = "";
            for (var i = 0; i < numberArray.length; i++) {
                if (numberArray[i] == '') {
                    continue;
                }
                numberArray[i] = numberArray[i].replace("\n", '');
                if (regexp.test(numberArray[i])) {
                    repeatCount = 0;
                    var temp = numberArray[i].split(" ");
                    if (parseInt(temp[0]) <= 10 && parseInt(temp[1]) <= 10 && parseInt(temp[2]) <= 10 && parseInt(temp[0]) != parseInt(temp[1]) && parseInt(temp[0]) != parseInt(temp[2]) && parseInt(temp[1]) != parseInt(temp[2])) {
                        orderItem += numberArray[i];
                        if (i != numberArray.length - 1) {
                            orderItem += ',';
                        }
                    }
                }
                else {
                    if (numberArray[i].length > PlayRandoms) {
                        $i("message").innerHTML = '第' + (i + 1) + '个号码 输入有误';
                    }
                    return false;
                }
            }
            if (orderItem.substring(orderItem.length - 1) == ",")
                orderItem = orderItem.substring(0, orderItem.length - 1)
            var arr = orderItem.split(",");
            var repstr = CheckReplace11(orderItem);
            if (repstr != "") {
                for (var i = 0; arr.length > i; i++) {
                    for (var j = i + 1; j < arr.length; j++) {
                        if (arr[j] == arr[i]) {
                            arr.splice(j, 1);
                            j--;
                        }
                    }
                }
            }
            SingleOrderItem = arr.toString();
            Znum = RedDS(SingleOrderItem);
            break;

        case "PK10_DD1_5":
        case "PK10_DD6_10":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                });
                if (orderItem.substring(orderItem.length - 1) == "_")
                    orderItem = orderItem.substring(0, orderItem.length - 1);
                orderItem += ",";
            }
            if (orderItem.split(',').length > 5)
                orderItem = orderItem.substring(0, orderItem.length - 1)
            SingleOrderItem = orderItem;
            Znum = RedDD_11(SingleOrderItem);
            break;

        case "PK10_DXOne":
        case "PK10_DXTwo":
        case "PK10_DXThree":
        case "PK10_DSOne":
        case "PK10_DSTwo":
        case "PK10_DSThree":
            for (var i = 0; i < rowsLen; i++) {
                var selectCount = 0;
                $($(".numbers").eq(i).find("span[number].selected")).each(function (j, dom) {
                    orderItem += $(dom).attr("number") + "_";
                    selectCount++;
                });
                if (orderItem.substring(orderItem.length - 1) == "_")
                    orderItem = orderItem.substring(0, orderItem.length - 1);
                if (i != rowsLen - 1) {
                    orderItem += ',';
                }
            }
            SingleOrderItem = orderItem;
            Znum = RedFS(SingleOrderItem.replace(/_/g, ""));
            break;
    }
    $i("fromBuyNumberCount").innerHTML = SingleCount = Znum;
    $i("fromBuyPriceTotal").innerHTML = SingleTotal = parseFloat(parseInt(Znum) * PriceTimes * Price).toFixed(4) * PriceModel;
}