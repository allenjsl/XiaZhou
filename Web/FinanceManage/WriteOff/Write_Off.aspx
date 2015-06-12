<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Write_Off.aspx.cs" Inherits="Web.FinanceManage.WriteOff.Write_Off" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批量销帐-销帐-</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <div style="margin: 0 auto; width: 99%;">
            <table width="99%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" id="liststyle"
                style="margin: 0 auto;">
                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        销账金额
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        订单号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        线路名称
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        出团日期
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        客户单位
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        应收金额
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        未收金额
                    </td>
                </tr>
                <tr>
                    <td height="30" align="center">
                        <input type="checkbox" name="checkbox2" id="checkbox2" />
                    </td>
                    <td align="center">
                        <input name="textfield" type="text" id="textfield" value="2580" class="xzjeInput" />
                    </td>
                    <td height="29" align="center">
                        nnbh201108120815009
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="center">
                        海景蜈支洲、南山祈福二日
                    </td>
                    <td align="center">
                        2006-04-05
                    </td>
                    <td align="center">
                        湖北峡州国际旅行社
                    </td>
                    <td align="right">
                        <b class="fontred">8000</b>
                    </td>
                    <td align="right">
                        <b class="fontred">4000</b>
                    </td>
                </tr>
                <tr>
                    <td height="30" align="center">
                        <input type="checkbox" name="checkbox2" id="checkbox2" />
                    </td>
                    <td align="center">
                        <input name="textfield" type="text" class="xzjeInput" id="textfield" value="300" />
                    </td>
                    <td height="29" align="center">
                        nnbh201108120815009
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="center">
                        海景蜈支洲、南山祈福二日
                    </td>
                    <td align="center">
                        2006-04-05
                    </td>
                    <td align="center">
                        湖北峡州国际旅行社
                    </td>
                    <td align="right">
                        <b class="fontred">8000</b>
                    </td>
                    <td align="right">
                        <b class="fontred">4000</b>
                    </td>
                </tr>
                <tr>
                    <td height="30" align="center">
                        <input type="checkbox" name="checkbox2" id="checkbox2" />
                    </td>
                    <td align="center">
                        <input name="textfield" type="text" class="xzjeInput" id="textfield" value="1260" />
                    </td>
                    <td height="29" align="center">
                        nnbh201108120815009
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="center">
                        海景蜈支洲、南山祈福二日
                    </td>
                    <td align="center">
                        2006-04-05
                    </td>
                    <td align="center">
                        湖北峡州国际旅行社
                    </td>
                    <td align="right">
                        <b class="fontred">8000</b>
                    </td>
                    <td align="right">
                        <b class="fontred">4000</b>
                    </td>
                </tr>
                <tr>
                    <td height="30" align="center">
                        <input type="checkbox" name="checkbox2" id="checkbox2" />
                    </td>
                    <td align="center">
                        <input name="textfield" type="text" class="xzjeInput" id="textfield" value="10300" />
                    </td>
                    <td height="29" align="center">
                        nnbh201108120815009
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="center">
                        海景蜈支洲、南山祈福二日
                    </td>
                    <td align="center">
                        2006-04-05
                    </td>
                    <td align="center">
                        湖北峡州国际旅行社
                    </td>
                    <td align="right">
                        <b class="fontred">8000</b>
                    </td>
                    <td align="right">
                        <b class="fontred">4000</b>
                    </td>
                </tr>
                <tr>
                    <td height="30" align="center">
                        <input type="checkbox" name="checkbox2" id="checkbox2" />
                    </td>
                    <td align="center">
                        <input name="textfield" type="text" id="textfield" class="xzjeInput" value="3000" />
                    </td>
                    <td height="29" align="center">
                        nnbh201108120815009
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="center">
                        海景蜈支洲、南山祈福二日
                    </td>
                    <td align="center">
                        2006-04-05
                    </td>
                    <td align="center">
                        湖北峡州国际旅行社
                    </td>
                    <td align="right">
                        <b class="fontred">8000</b>
                    </td>
                    <td align="right">
                        <b class="fontred">4000</b>
                    </td>
                </tr>
                <tr>
                    <td height="29" colspan="9" align="center">
                        <div style="position: relative; height: 20px;">
                            <div class="pages">
                                <span class="current">首页</span><span class="disabled">上页</span><a href="#">下一页</a><a
                                    href="#">尾页</a><select name=""><option>1/8</option>
                                    </select></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="#" hidefocus="true"><s class="baochun"></s>保 存</a><a href="#" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a></div>
    </div>
     <script type="text/javascript">

         $(function() {
             //绑定功能按钮
             BindBtn();
         })

         //绑定功能按钮
         function BindBtn() {
             tableToolbar.init({
                 tableContainerSelector: "#liststyle", //表格选择器
                 objectName: "记录"
             })
         }
    </script>
</body>

</html>
