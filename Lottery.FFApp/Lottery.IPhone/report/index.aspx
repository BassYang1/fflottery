<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Lottery.Web.report.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <title>立博国际娱乐</title>
    <script type="text/javascript" src="/statics/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/statics/layer/layer.min.js"></script>
    
    <script type="text/javascript" src="/statics/sytle/js/EM.tools.js"></script>
    
</head>
<body>
    <!-- #include file="/statics/html/header.htm" -->
    <div class='main_tz'>
        <div class='main_tz_top'>
            <img src='/statics/themes/default/images/tz_01.png' width='100%' height='162' />
        </div>
        <div class='main_tz_a'>
            <div class='main_tz_a_cont ny_back_bor myCard_style_div'>
                <!-- #include file="/statics/html/secondtop.htm" -->
                <div class='main_tz_a_contdetails_div main_tz_a_contdetails_div_main'>
                    <img src='/statics/themes/default/images/details_23.png' width="100%" height='31' />
                    <p class='address_cz'>
                        <a>报表管理</a>
                    </p>
                </div>
                <div class='main_tz_a_contdetails_div'>
                    <div class='details_tx'>
                        <ul class="tabsecond">
                            <li>
                            <a href="javascript:void(0)" class="on" url="/report/profitloss.aspx">报表查询</a>
                            <a href="javascript:void(0)" url="/report/history.aspx">资金记录</a>
                            <a href="javascript:void(0)" url="/report/chargecash.aspx">充提记录</a>
                            <a href="javascript:void(0)" url="/report/pointreport.aspx">返点记录</a>
                            <a href="javascript:void(0)" url="/report/activelist.aspx">活动记录</a>
                            <a href="javascript:void(0)" <%=act1 %> url="/report/agentFHRecord.aspx">分红记录</a>
                            </li>
                        </ul>
                        <div class='clear'>
                        </div>
                    </div>

                    <iframe id="workspace" scrolling="no" src="/report/profitloss.aspx" style="width: 998px;
                        height: 790px;" frameborder="0" marginheight="0" marginwidth="0"></iframe>
                </div>
            </div>
        </div>
    </div>
    <!-- #include file="/statics/html/footer.htm" -->
    <script type="text/javascript">
        $('.tabsecond li a').click(function () {
            $(this).parent().find('a').removeClass();
            $(this).addClass('on');
            $i("workspace").src = $(this).attr("url");
        });
    </script>
</body>
</html>