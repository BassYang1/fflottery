<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Temp.aspx.cs" Inherits="Lottery.Admin.Temp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <title></title>
    <script type="text/javascript" src="/_libs/jquery.tools.pack.js"></script>
    <script type="text/javascript" src="/statics/global.js"></script>
    <script type="text/javascript" src="/_libs/My97DatePicker/WdatePicker.js"></script>
    <link type="text/css" rel="stylesheet" href="/statics/global.css" />
    <link href="/statics/admin/css/pop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/statics/admin/js/common.js"></script>
    <script src="/statics/admin/js/temp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowPage(taburl) {
            top.Lottery.Loading.show("正在更新缓存，请稍后...");
            $i("workspace").src = taburl;
            top.Lottery.Message("更新完成", "1");
        }
    </script>
</head>
<body>
    <div class="uc-box">
            <div class="box-hd">
            <h3 class="title">
                服务器时间校对</h3>
            <a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a> <i class="icon-t icon-data">
            </i>
        </div>
        <div class="box-bd">
         <form action="" method="post" class="uc-form">
            <table class="formtable" style="background-color: #fff; width: 100%; margin: 0px;">
                <tr>
                    <th>
                        北京时间
                    </th>
                    <td>
                        <span class="num" id="bzTime">0</span>
                    </td>
                    <th>
                        服务器时间
                    </th>
                    <td>
                        <span class="num" id="curTime">0</span>
                    </td>
                </tr>
            </table>
            <br />
            </form>
        </div>
        <div class="box-hd">
            <h3 class="title">
                数据更新</h3>
            <a href="javascript:;" class="back-top"><i class="icon icon-top"></i>TOP</a> <i class="icon-t icon-data">
            </i>
        </div>
        <div class="box-bd">
            <form action="" method="post" class="uc-form">
            <table class="formtable" style="background-color: #fff; width: 100%; margin: 0px;">
                <tr>
                    <th>
                        游戏采种更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strRootUrl %>/Temp/WebLot.aspx')" />
                    </td>
                    <th>
                        游戏类别更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strRootUrl %>/Temp/WebPlay.aspx')" />
                    </td>
                </tr>
                <tr>
                    <th>
                        游戏玩法更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strRootUrl %>/Temp/WebPlay.aspx')" />
                    </td>
                    <th>
                        基础参数更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strRootUrl %>/Temp/WebSite.aspx')" />
                    </td>
                </tr>
                <tr>
                    <th>
                        手机采种更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strIphoneUrl %>/Temp/WebLot.aspx')" />
                    </td>
                    <th>
                        手机返点更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strIphoneUrl %>/Temp/WebPoint.aspx')" />
                    </td>
                </tr>
                <tr>
                    <th>
                        手机玩法更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strIphoneUrl %>/Temp/WebPlay.aspx')" />
                    </td>
                    <th>
                        手机参数更新
                    </th>
                    <td>
                        <input type="button" value="立即更新" class="btn btn-primary" onclick="ShowPage('<%=strIphoneUrl %>/Temp/WebSite.aspx')" />
                    </td>
                </tr>
            </table>
            <iframe id="workspace" name='workspace' scrolling="auto" src="" width="100%" height="580px"
                frameborder="0" marginheight="0" marginwidth="0"></iframe>
            </form>
        </div>
    </div>
</body>
</html>
