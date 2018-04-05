//组合
function RedZH(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        var num = 5.00;
        for (var i = 0; i < strArray2.length; i++) {
            var num2 = strArray2[i].length;
            num *= num2;
        }
        return num;
    }
    return 0;
}
//组120
function RedZu120(balls) {
    if (balls != "") {
        var n = balls.length;
        return (Pareto(n, 5) / Pareto(5, 5));
    }
    return 0;
}
//组60
function RedZu60(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length > 1) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var num = 0;
            var danhao = "";
            for (var i = 0; i < strArray12.length; i++) {
                for (var j = i; j < strArray12.length; j++) {
                    for (var k = j; k < strArray12.length; k++) {
                        if (strArray12[i] != strArray12[j] && strArray12[j] != strArray12[k] && strArray12[k] != strArray12[i])
                            danhao += strArray12[i] + "" + strArray12[j] + "" + strArray12[k] + ",";
                    }
                }
            }
            danhao = danhao.substring(0, danhao.length - 1);
            var strArray = danhao.split(',');

            for (var i = 0; i < strArray.length; i++) {
                for (var j = 0; j < strArray11.length; j++) {
                    var str1 = strArray[i];
                    if (str1.indexOf(strArray11[j]) == -1) {
                        num++;
                    }
                }
            }
            return num;
        }
        else {
            return 0;
        }
    }
    return 0;
}
//组30
function RedZu30(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length > 1) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var num = 0;
            var danhao = "";
            for (var i = 0; i < strArray11.length; i++) {
                for (var j = i; j < strArray11.length; j++) {
                    if (strArray11[i] != strArray11[j])
                        danhao += strArray11[i] + "" + strArray11[j] + ",";
                }
            }
            danhao = danhao.substring(0, danhao.length - 1);
            var strArray = danhao.split(',');

            for (var i = 0; i < strArray.length; i++) {
                for (var j = 0; j < strArray12.length; j++) {
                    var str1 = strArray[i];
                    if (str1.indexOf(strArray12[j]) == -1) {
                        num++;
                    }
                }
            }
            return num;
        }
        else {
            return 0;
        }
    }
    return 0;
}
//组20
function RedZu20(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length > 1) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var num = 0;
            var danhao = "";
            for (var i = 0; i < strArray12.length; i++) {
                for (var j = i; j < strArray12.length; j++) {
                    if (strArray12[i] != strArray12[j])
                        danhao += strArray12[i] + "" + strArray12[j] + ",";
                }
            }
            danhao = danhao.substring(0, danhao.length - 1);
            var strArray = danhao.split(',');

            for (var i = 0; i < strArray.length; i++) {
                for (var j = 0; j < strArray11.length; j++) {
                    var str1 = strArray[i];
                    if (str1.indexOf(strArray11[j]) == -1) {
                        num++;
                    }
                }
            }
            return num;
        }
        else {
            return 0;
        }
    }
    return 0;
}
//组10
function RedZu10(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length > 1) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var num = 0;
            var danhao = "";
            for (var i = 0; i < strArray12.length; i++) {
                danhao += strArray12[i] + ",";
            }
            danhao = danhao.substring(0, danhao.length - 1);
            var strArray = danhao.split(',');

            for (var i = 0; i < strArray.length; i++) {
                for (var j = 0; j < strArray11.length; j++) {
                    var str1 = strArray[i];
                    if (str1.indexOf(strArray11[j]) == -1) {
                        num++;
                    }
                }
            }
            return num;
        }
        else {
            return 0;
        }
    }
    return 0;
}
//组5
function RedZu5(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length > 1) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var num = 0;
            var danhao = "";
            for (var i = 0; i < strArray12.length; i++) {
                danhao += strArray12[i] + ",";
            }
            danhao = danhao.substring(0, danhao.length - 1);
            var strArray = danhao.split(',');

            for (var i = 0; i < strArray.length; i++) {
                for (var j = 0; j < strArray11.length; j++) {
                    var str1 = strArray[i];
                    if (str1.indexOf(strArray11[j]) == -1) {
                        num++;
                    }
                }
            }
            return num;
        }
        else {
            return 0;
        }
    }
    return 0;
}
//特殊
function RedTS(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        var num = 0.00;
        for (var i = 0; i < strArray2.length; i++) {
            var num2 = strArray2[i].length;
            num += num2;
        }
        return num;
    }
    return 0;
}
//组24
function RedZu24(balls) {
    if (balls != "") {
        var n = balls.length;
        return (Pareto(n, 4) / Pareto(4, 4));
    }
    return 0;
}
//组12
function RedZu12(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length > 1) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var num = 0;
            var danhao = "";
            for (var i = 0; i < strArray12.length; i++) {
                for (var j = i; j < strArray12.length; j++) {
                    if (strArray12[i] != strArray12[j])
                        danhao += strArray12[i] + "" + strArray12[j] + ",";
                }
            }
            danhao = danhao.substring(0, danhao.length - 1);
            var strArray = danhao.split(',');

            for (var i = 0; i < strArray.length; i++) {
                for (var j = 0; j < strArray11.length; j++) {
                    var str1 = strArray[i];
                    if (str1.indexOf(strArray11[j]) == -1) {
                        num++;
                    }
                }
            }
            return num;
        }
        else {
            return 0;
        }
    }
    return 0;
}
//组6
function RedZu61(balls) {
    if (balls != "") {
        var n = balls.length;
        return (Pareto(n, 2) / Pareto(2, 2));
    }
    return 0;
}
//组4
function RedZu4(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length > 1) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var num = 0;
            var danhao = "";
            for (var i = 0; i < strArray12.length; i++) {
                danhao += strArray12[i] + ",";
            }
            danhao = danhao.substring(0, danhao.length - 1);
            var strArray = danhao.split(',');

            for (var i = 0; i < strArray.length; i++) {
                for (var j = 0; j < strArray11.length; j++) {
                    var str1 = strArray[i];
                    if (str1.indexOf(strArray11[j]) == -1) {
                        num++;
                    }
                }
            }
            return num;
        }
        else {
            return 0;
        }
    }
    return 0;
}

//复试
function RedFS(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        var num = 1.00;
        for (var i = 0; i < strArray2.length; i++) {
            var num2 = strArray2[i].length;
            num *= num2;
        }
        return num;
    }
    return 0;
}
//单式
function RedDS(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        return strArray2.length;
    }
    return 0;
}
//组六
function RedZu6(balls) {
    if (balls != "") {
        var n = balls.length;
        return (Pareto(n, 3) / Pareto(3, 3));
    }
    return 0;
}
//组三
function RedZu3(balls) {
    if (balls != "") {
        var n = balls.length;
        return Pareto(n, 2);
    }
    return 0;
}
//组二
function RedZu2(balls) {
    if (balls != "") {
        var num = balls.length;
        return num * (num - 1) / 2;
    }
    return 0;
}
//定位胆
function RedDD(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        var num = 0;
        for (var i = 0; i < strArray2.length; i++) {
            var num2 = strArray2[i].length;
            num += num2;
        }
        return num;
    }
    return 0;
}
//任选复试
function RedFS_R(balls, p) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        var num = 1.00;
        for (var i = 0; i < strArray2.length; i++) {
            var num2 = strArray2[i].length;
            num *= num2;
        }
        var n = (p.split('1')).length - 1;
        return num * Combine(n, PlayWzNum);
    }
    return 0;
}
//任选单式
function RedDS_R(balls, p) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        var n = (p.split('1')).length - 1;
        return strArray2.length * 1 * Combine(n, PlayWzNum);
    }
    return 0;
}
//任选组六
function RedZu6_R(balls, p) {
    if (balls != "") {
        var num = balls.length;
        var n = (p.split('1')).length - 1;
        return (Pareto(num, 3) / Pareto(3, 3)) * Combine(n, PlayWzNum);
    }
    return 0;
}
//任选组三
function RedZu3_R(balls, p) {
    if (balls != "") {
        var num = balls.length;
        var n = (p.split('1')).length - 1;
        return Pareto(num, 2) * Combine(n, PlayWzNum);
    }
    return 0;
}
//任选组二
function RedZu2_R(balls, p) {
    if (balls != "") {
        var num = balls.length;
        var n = (p.split('1')).length - 1;
        return num * (num - 1) / 2 * Combine(n, PlayWzNum);
    }
    return 0;
}
//三星和值
function RedHE3(balls) {
    var num = 0;
    if (balls != "") {
        var b = balls.split("_");
        for (var i = 0; i != b.length; i++) {
            if (b[i] == 0) {
                num += 1;
            }
            if (b[i] == 1) {
                num += 3;
            }
            if (b[i] == 2) {
                num += 6;
            }
            if (b[i] == 3) {
                num += 10;
            }
            if (b[i] == 4) {
                num += 15;
            }
            if (b[i] == 5) {
                num += 21;
            }
            if (b[i] == 6) {
                num += 28;
            }
            if (b[i] == 7) {
                num += 36;
            }
            if (b[i] == 8) {
                num += 45;
            }
            if (b[i] == 9) {
                num += 55;
            }
            if (b[i] == 10) {
                num += 63;
            }
            if (b[i] == 11) {
                num += 69;
            }
            if (b[i] == 12) {
                num += 73;
            }
            if (b[i] == 13) {
                num += 75;
            }
            if (b[i] == 14) {
                num += 75;
            }
            if (b[i] == 15) {
                num += 73;
            }
            if (b[i] == 16) {
                num += 69;
            }
            if (b[i] == 17) {
                num += 63;
            }
            if (b[i] == 18) {
                num += 55;
            }
            if (b[i] == 19) {
                num += 45;
            }
            if (b[i] == 20) {
                num += 36;
            }
            if (b[i] == 21) {
                num += 28;
            }
            if (b[i] == 22) {
                num += 21;
            }
            if (b[i] == 23) {
                num += 15;
            }
            if (b[i] == 24) {
                num += 10;
            }
            if (b[i] == 25) {
                num += 6;
            }
            if (b[i] == 26) {
                num += 3;
            }
            if (b[i] == 27) {
                num += 1;
            }
        }
    }
    return num;
}
//二星和值
function RedHE2(balls) {
    var num = 0;
    if (balls != "") {
        var b = balls.split("_");
        for (var i = 0; i != b.length; i++) {
            if (b[i] == 0) {
                num += 1;
            }
            if (b[i] == 1) {
                num += 2;
            }
            if (b[i] == 2) {
                num += 3;
            }
            if (b[i] == 3) {
                num += 4;
            }
            if (b[i] == 4) {
                num += 5;
            }
            if (b[i] == 5) {
                num += 6;
            }
            if (b[i] == 6) {
                num += 7;
            }
            if (b[i] == 7) {
                num += 8;
            }
            if (b[i] == 8) {
                num += 9;
            }
            if (b[i] == 9) {
                num += 10;
            }
            if (b[i] == 10) {
                num += 9;
            }
            if (b[i] == 11) {
                num += 8;
            }
            if (b[i] == 12) {
                num += 7;
            }
            if (b[i] == 13) {
                num += 6;
            }
            if (b[i] == 14) {
                num += 5;
            }
            if (b[i] == 15) {
                num += 4;
            }
            if (b[i] == 16) {
                num += 3;
            }
            if (b[i] == 17) {
                num += 2;
            }
            if (b[i] == 18) {
                num += 1;
            }
        }
    }
    return num;
}

//三星组选和值
function RedZHE3(balls) {
    var num = 0;
    if (balls != "") {
        var b = balls.split("_");
        for (var i = 0; i != b.length; i++) {
            if (b[i] == 1) {
                num += 1;
            }
            if (b[i] == 2) {
                num += 2;
            }
            if (b[i] == 3) {
                num += 2;
            }
            if (b[i] == 4) {
                num += 4;
            }
            if (b[i] == 5) {
                num += 5;
            }
            if (b[i] == 6) {
                num += 6;
            }
            if (b[i] == 7) {
                num += 8;
            }
            if (b[i] == 8) {
                num += 10;
            }
            if (b[i] == 9) {
                num += 11;
            }
            if (b[i] == 10) {
                num += 13;
            }
            if (b[i] == 11) {
                num += 14;
            }
            if (b[i] == 12) {
                num += 14;
            }
            if (b[i] == 13) {
                num += 15;
            }
            if (b[i] == 14) {
                num += 15;
            }
            if (b[i] == 15) {
                num += 14;
            }
            if (b[i] == 16) {
                num += 14;
            }
            if (b[i] == 17) {
                num += 13;
            }
            if (b[i] == 18) {
                num += 11;
            }
            if (b[i] == 19) {
                num += 10;
            }
            if (b[i] == 20) {
                num += 8;
            }
            if (b[i] == 21) {
                num += 6;
            }
            if (b[i] == 22) {
                num += 5;
            }
            if (b[i] == 23) {
                num += 4;
            }
            if (b[i] == 24) {
                num += 2;
            }
            if (b[i] == 25) {
                num += 2;
            }
            if (b[i] == 26) {
                num += 1;
            }
        }
    }
    return num;
}


//三星直选跨度
function Red3KD(balls) {
    var num = 0;
    if (balls != "") {
        var b = balls.split("_");
        for (var i = 0; i != b.length; i++) {
            if (b[i] == 0) {
                num += 10;
            }
            if (b[i] == 1) {
                num += 54;
            }
            if (b[i] == 2) {
                num += 96;
            }
            if (b[i] == 3) {
                num += 126;
            }
            if (b[i] == 4) {
                num += 144;
            }
            if (b[i] == 5) {
                num += 150;
            }
            if (b[i] == 6) {
                num += 144;
            }
            if (b[i] == 7) {
                num += 126;
            }
            if (b[i] == 8) {
                num += 96;
            }
            if (b[i] == 9) {
                num += 54;
            }
        }
    }
    return num;
}

//二星组选和值
function RedZHE2(balls) {
    var num = 0;
    if (balls != "") {
        var b = balls.split("_");
        for (var i = 0; i != b.length; i++) {
            if (b[i] == 1) {
                num += 1;
            }
            if (b[i] == 2) {
                num += 1;
            }
            if (b[i] == 3) {
                num += 2;
            }
            if (b[i] == 4) {
                num += 2;
            }
            if (b[i] == 5) {
                num += 3;
            }
            if (b[i] == 6) {
                num += 3;
            }
            if (b[i] == 7) {
                num += 4;
            }
            if (b[i] == 8) {
                num += 4;
            }
            if (b[i] == 9) {
                num += 5;
            }
            if (b[i] == 10) {
                num += 4;
            }
            if (b[i] == 11) {
                num += 4;
            }
            if (b[i] == 12) {
                num += 3;
            }
            if (b[i] == 13) {
                num += 3;
            }
            if (b[i] == 14) {
                num += 2;
            }
            if (b[i] == 15) {
                num += 2;
            }
            if (b[i] == 16) {
                num += 1;
            }
            if (b[i] == 17) {
                num += 1;
            }
        }
    }
    return num;
}

//二星直选跨度
function Red2KD(balls) {
    var num = 0;
    if (balls != "") {
        var b = balls.split("_");
        for (var i = 0; i != b.length; i++) {
            if (b[i] == 0) {
                num += 10;
            }
            if (b[i] == 1) {
                num += 18;
            }
            if (b[i] == 2) {
                num += 16;
            }
            if (b[i] == 3) {
                num += 14;
            }
            if (b[i] == 4) {
                num += 12;
            }
            if (b[i] == 5) {
                num += 10;
            }
            if (b[i] == 6) {
                num += 8;
            }
            if (b[i] == 7) {
                num += 6;
            }
            if (b[i] == 8) {
                num += 4;
            }
            if (b[i] == 9) {
                num += 2;
            }
        }
    }
    return num;
}

//趣味区间
function RedQwQj(balls) {
    if (balls != "") {
        balls = balls.replace(/_/g, "");
        var strArray2 = balls.split(",");
        var num = 1.00;
        if (strArray2.length == 5) {
            num = 1.00;
            for (var i = 2; i < strArray2.length; i++) {
                var num2 = strArray2[i].length;
                num *= num2;
            }
            num = num * strArray2[0].length / 2 * strArray2[1].length / 2;
        }
        if (strArray2.length == 4) {
            num = 1.00;
            for (var j = 1; j < strArray2.length; j++) {
                var num3 = strArray2[j].length;
                num *= num3;
            }
            num = num * strArray2[0].length / 2;
        }
        if (strArray2.length == 3) {
            num = 1.00;
            for (var k = 1; k < strArray2.length; k++) {
                var num4 = strArray2[k].length;
                num *= num4;
            }
            num = num * strArray2[0].length / 2;
        }
        return num;
    }
    return 0;
}

//五星组合玩法
function Red5ZuHe(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length < 5)
            return 0;
        var sum = 0.00;
        var num = 1.00;
        var num2 = 1.00;
        var num3 = 1.00;
        var num4 = 1.00;
        var num5 = 1.00;
        var i = 0;
        for (i = 0; i < strArray2.length; i++) {
            num *= strArray2[i].length;
        }
        for (i = 1; i < strArray2.length; i++) {
            num2 *= strArray2[i].length;
        }
        for (i = 2; i < strArray2.length; i++) {
            num3 *= strArray2[i].length;
        }
        for (i = 3; i < strArray2.length; i++) {
            num4 *= strArray2[i].length;
        }
        for (i = 4; i < strArray2.length; i++) {
            num5 *= strArray2[i].length;
        }
        return num + num2 + num3 + num4 + num5;
    }
    return 0;
}

//四星组合玩法
function Red4ZuHe(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length < 4)
            return 0;
        var sum = 0.00;
        var num = 1.00;
        var num2 = 1.00;
        var num3 = 1.00;
        var num4 = 1.00;
        var i = 0;
        for (i = 0; i < strArray2.length; i++) {
            num *= strArray2[i].length;
        }
        for (i = 1; i < strArray2.length; i++) {
            num2 *= strArray2[i].length;
        }
        for (i = 2; i < strArray2.length; i++) {
            num3 *= strArray2[i].length;
        }
        for (i = 3; i < strArray2.length; i++) {
            num4 *= strArray2[i].length;
        }
        return num + num2 + num3 + num4;
    }
    return 0;
}

//三星组合玩法
function Red3ZuHe(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length < 3)
            return 0;
        var sum = 0.00;
        var num = 1.00;
        var num2 = 1.00;
        var num3 = 1.00;
        var i = 0;
        for (i = 0; i < strArray2.length; i++) {
            num *= strArray2[i].length;
        }
        for (i = 1; i < strArray2.length; i++) {
            num2 *= strArray2[i].length;
        }
        for (i = 2; i < strArray2.length; i++) {
            num3 *= strArray2[i].length;
        }
        return num + num2 + num3;
    }
    return 0;
}


//11选5

//任选复试
function RedRXFS_11(balls, num) {
    if (balls != "") {
        var strArray2 = balls.split("_");
        return Combine(strArray2.length, num);
    }
    return 0;
}
//前三直选复式
function Red3FS_11(balls) {
    var num = 0;
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length == 3) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');
            var strArray13 = strArray2[2].split('_');

            for (var i = 0; i < strArray11.length; i++) {
                for (var j = 0; j < strArray12.length; j++) {
                    if (strArray11[i] != strArray12[j]) {
                        for (var k = 0; k < strArray13.length; k++) {
                            if (strArray13[k] != strArray11[i] && strArray13[k] != strArray12[j]) {
                                num++;
                            }
                        }
                    }
                }
            }
        }
    }
    return num;
}
//前三组选复式
function Red3ZFS_11(balls) {
    if (balls != "") {
        var strArray2 = balls.split('_');
        return Combine(strArray2.length, 3);
    }
    return 0;
}
//前二直选复式
function Red2FS_11(balls) {
    var num = 0;
    if (balls != "") {
        var strArray2 = balls.split(",");
        if (strArray2.length == 2) {
            var strArray11 = strArray2[0].split('_');
            var strArray12 = strArray2[1].split('_');

            for (var i = 0; i < strArray11.length; i++) {
                for (var j = 0; j < strArray12.length; j++) {
                    if (strArray11[i] != strArray12[j]) {
                        num++;
                    }
                }
            }
        }
    }
    return num;
}
//前二组选复式
function Red2ZFS_11(balls) {
    if (balls != "") {
        var strArray2 = balls.split('_');
        return strArray2.length * (strArray2.length - 1) / 2;
    }
    return 0;
}
//11定位胆
function RedDD_11(balls) {
    if (balls != "") {
        var strArray2 = balls.split(",");
        var num = 0;
        for (var i = 0; i < strArray2.length; i++) {
            if (strArray2[i] != "") {
                var num2 = strArray2[i].split("_").length;
                num += num2;
            }
        }
        return num;
    }
    return 0;
}

//PK10
//冠军复式
function PK10FS_One(balls) {
    if (balls != "") {
        var strArray2 = balls.split('_');
        return Combine(strArray2.length, 1);
    }
    return 0;
}

function Combine(n, r) {
    return (Pareto(n, r) / Pareto(r, r));
}

function Combine2(n, PlayCode) {
    var r = 0;
    switch (PlayCode) {
        case "R_4FS":
        case "R_4DS":
            r = 2;
            break;
        case "R_3FS":
        case "R_3DS":
        case "R_3Z6":
        case "R_3Z3":
        case "R_3HX":
            r = 3;
            break;
        case "R_2FS":
        case "R_2DS":
        case "R_2Z2":
            r = 2;
            break;
    }
    return (Pareto(n, r) / Pareto(r, r));
}

function Pareto(n, r) {
    var num = 1.00;
    for (var i = n; i != (n - r); i--) {
        num *= i;
    }
    return num;
}