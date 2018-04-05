<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userContractFH.aspx.cs"
    Inherits="Lottery.WebApp.user.userContractFH" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <title>立博国际娱乐</title>
    <link rel="stylesheet" type="text/css" href="/statics/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/statics/css/member.css" />
    <script src="/statics/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="/statics/global.js" type="text/javascript"></script>
    <script src="/statics/layer/layer.js" type="text/javascript"></script>
    <script src="/statics/js/EM.tools.js" type="text/javascript"></script>
    <script type="text/javascript">
        var count = 0;
        $(document).ready(function () {
            ajaxGetList();
        });

        function ajaxGetList()
        {
             $.ajax({
                    type: "get",
                    dataType: "json",
                    data: "id=<%=userId %>&clienttime=" + Math.random(),
                    url: "/ajax/ajaxUserContractFH.aspx?oper=GetContractInfo",
                    error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
                    success: function (d) {
                    var html="";
                     if (d.table.length > 0) {
                        for (var i = 0; i < d.table.length; i++) {
                            var t = d.table[i];
                            html+='<div class="input-group"><label class="lab">0'+(i + 1)+'.半月周期销量</label><label class="lab">'+t.minmoney+'万</label><label class="lab">，分红'+t.money+'%</label></div>';
                        }
                        if (d.table[0].groupid == 4) {
                            $i("info").innerHTML = "该会员是招商会员，直接用平台分红标准";
                            $("#btn").hide();
                            $("#add").html(html);
                        }
                        else if (d.table[0].groupid == 3) {
                            $i("info").innerHTML = "该会员是特权直属会员，直接用平台分红标准";
                            $("#btn").hide();
                            $("#add").html(html);
                        }
                        else {
                        if (d.table[0].groupid >= 5) {
                            $i("info").innerHTML="您的级别不参与契约";
                            $("#btn").hide();
                        }
                        else
                        {
                            if(d.table[0].isused==0)
                            {
                                 $i("info").innerHTML="契约待接受";
                                 $("#btn").hide();
                                 $("#add").html(html);
                            }
                            if(d.table[0].isused==1)
                            {
                                 $i("info").innerHTML="契约已签订";
                                 $("#btn").hide();
                                 $("#add").html(html);
                            }
                            if(d.table[0].isused==2)
                            {
                                 $i("info").innerHTML="契约已拒绝，可重新分配";
                                 $("#btn").show();
                            }
                            }
                        }
                       }
                       else{
                        $i("info").innerHTML = "契约未分配，请您分配！";
						$("#btn").show();
                       }
                    }
                });
        }


        function AddFrom() {
            count++;
            if (count < 10) {
                //使用createElement创建元素
                var div = document.createElement('div');
                div.className = 'input-group';

                var label = document.createElement('label');
                label.className = 'lab';
                label.innerHTML = '0' + count + '.半月周期销量';

                var input = document.createElement('input');
                input.className = 'ipt';
                input.id = "money_" + count;

                var label2 = document.createElement('label');
                label2.className = 'lab';
                label2.innerHTML = '万，分红';

                var select = document.createElement('select');
                select.id = "per_" + count;
                select.className = 'select';
                var img = document.createElement('img');
                img.id = "img_" + count;
                img.src = '/statics/img/icon_lot_del.png';
                img.className = 'img';
                img.onclick = function () {
                    document.getElementById("add").removeChild(div);
                    count--;
                    $("#img_" + (count)).show();
                }

                div.appendChild(label);
                div.appendChild(input);
                div.appendChild(label2);
                div.appendChild(select);
                div.appendChild(img);
                document.getElementById('add').appendChild(div);
                PerBind("per_" + count);
                for (var i = 1; i < count; i++) {
                    $("#img_" + i).hide();
                }
            }
        }

        var SelectedData = [];
        function ajaxView() {
            SelectedData.splice(0, SelectedData.length);
            for (var i = 0; i <= count; i++) {
                var money = $("#money_" + i).val();
                var per = $("#per_" + i).val();
                if (money != "undefined" && parseFloat(money) > 0) {
                    var json1 = {
                        "userid": <%=userId %>,
                        "money": money,
                        "per": per
                    };
                    SelectedData.push(json1);
                }
            }
            var arrzh = JSON.stringify(SelectedData);
            $.ajax({
                    type: "post",
                    dataType: "json",
                    data: arrzh,
                    async: false,
                    url: "/ajax/ajaxUserContractFH.aspx?oper=ajaxSaveContract&clienttime=" + Math.random(),
                    error: function (XmlHttpRequest, textStatus, errorThrown) { emAlert("亲！页面过期,请刷新页面!"); },
                    success: function (d) {
                        switch (d.result) {
                            case '0':
                                emAlert(d.returnval);
                                break;
                            case '1':
                                ajaxGetList();
                                break;
                        }
                    }
                });
		}

        var maxPer=0;
        function PerBind(obj) {
            for (var k = 0; k <= count; k++) {
                var per = $("#per_" + k).val();
                if(per>maxPer)
                    maxPer=per;
            }
            var str = '';
            for(var i=1;i<=(parseInt(maxPer)+15);i++)
            {
                if(i>parseInt(maxPer))
                {
                    str += '<option value="'+i+'">'+i+'%</option>';
                }
            }
//            var str = '<option value="1">1%</option>';
//            str += '<option value="2">2%</option>';
//            str += '<option value="3">3%</option>';
//            str += '<option value="4">4%</option>';
//            str += '<option value="5">5%</option>';
//            str += '<option value="6">6%</option>';
//            str += '<option value="7">7%</option>';
//            str += '<option value="8">8%</option>';
//            str += '<option value="9">9%</option>';
//            str += '<option value="10">10%</option>';
//            str += '<option value="11">11%</option>';
//            str += '<option value="12">12%</option>';
//            str += '<option value="13">13%</option>';
//            str += '<option value="14">14%</option>';
//            str += '<option value="15">15%</option>';
            $('#' + obj).html(str);
        }
    </script>
</head>
<body>
    <div class="tto-popup">
        <div class="popup-body">
        <form id="form1" class="tto-form2">
        <div class="input-group">
           <span id="info" class="info">契约未签订</span>
        </div>
        
        <div id="add" style="height:350px;">
       
        </div>
         <div id="btn" class="btn-group">
            <input type="button" value="添加规则" onclick="AddFrom()" class="btn btn-bg btn-primary" />
            <input type="button" value="确定提交" onclick="ajaxView()" class="btn btn-bg btn-primary" />
        </div>
        </form>
        </div>
    </div>
</body>
</html>
