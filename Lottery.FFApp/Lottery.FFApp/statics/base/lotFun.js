function checkAll() {
    if ($("#checkedAll").is(":checked")) { // 全选 
        $("input[name='selectID']").each(function () {
            $(this).attr("checked", true);
        });
    } else { // 取消全选 
        $("input[name='selectID']").each(function () {
            $(this).attr("checked", false);
        });
    }
}

function checkAllLine() {
    if ($("#checkedAll").is(":checked")) {  // 全选 
        $('.query-table tbody tr').each(
			function () {
			    $(this).addClass('selected');
			    $(this).find('input[type=checkbox]').attr('checked', 'checked');
			}
		);
    } else { // 取消全选 
        $('.query-table tbody tr').each(
			function () {
			    $(this).removeClass('selected');
			    $(this).find('input[type=checkbox]').removeAttr('checked');
			}
		);
    }
}
//终止追号
function operater() {
    var ids = JoinSelect("selectID");
    if (ids == "") {
        emAlert("请先勾选要操作的内容");
        return;
    }
    $.ajax({
        type: "post",
        dataType: "json",
        data: "ids=" + ids,
        url: "/ajax/ajaxBet.aspx?oper=ajaxOper&clienttime=" + Math.random(),
        error: function (XmlHttpRequest, textStatus, errorThrown) { emalert(XmlHttpRequest.responseText); },
        success: function (d) {
            ajaxList(1);
        }
    });
}