<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnMoney.aspx.cs" Inherits="Web.FinanceManage.Common.ReturnMoney" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>退款</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: auto; text-align: center; font-size: 12px; color: #FF0000; font-weight: bold;
            padding-bottom: 5px;">
            应收金额：￥2,400.00 已收金额：￥0.00 未收金额：￥2,400.00
        </div>
        <table width="99%" height="79" border="0" align="center" cellpadding="0" cellspacing="0"
            style="margin: 0 auto">
            <tr>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">序号</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">退款时间</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">退款人</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">退款金额</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">退款方式</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    备注
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    操作
                </td>
            </tr>
            <tr>
                <td align="center">
                    1
                </td>
                <td align="center">
                    <label>
                        <input name="textfield" type="text" id="text1" size="10" value="2011-8-11" />
                    </label>
                </td>
                <td align="center">
                    <input name="textfield2" type="text" id="text2" size="8" value="黄芳（小）" />
                    <a href="tuikuanren-xybox.html" class="xuanyong" id="A1">&nbsp;</a>
                </td>
                <td align="center">
                    <input name="textfield3" type="text" id="text3" size="10" value="￥0.00k" />
                </td>
                <td align="center">
                    <label>
                        <select name="select" id="select1" style="width: 80px;">
                            <option selected="selected">财务现收</option>
                        </select>
                    </label>
                </td>
                <td align="center">
                    <textarea name="textarea" id="textarea" style="height: 25px; width: 110px;"></textarea>
                </td>
                <td align="center">
                    <a href="#">审核</a><a href="#"> 修改</a><a href="#"> 删除 </a><a href="#" target="_blank">
                        打印</a>
                </td>
            </tr>
            <tr>
                <td align="center">
                    2
                </td>
                <td align="center">
                    <label>
                        <input name="textfield" type="text" id="textfield" size="10" />
                    </label>
                </td>
                <td align="center">
                    <input name="textfield2" type="text" id="textfield2" size="8" />
                    <a href="tuikuanren-xybox.html" class="xuanyong" id="link1">&nbsp;</a>
                </td>
                <td align="center">
                    <input name="textfield3" type="text" id="textfield3" size="10" />
                </td>
                <td align="center">
                    <label>
                        <select name="select" id="select" style="width: 80px;">
                        </select>
                    </label>
                </td>
                <td align="center">
                    <textarea name="textarea" id="textarea" style="height: 25px; width: 110px;"></textarea>
                </td>
                <td align="center">
                    <a href="#">审核</a><a href="#"> 修改</a><a href="#"> 删除 </a><a href="#" target="_blank">
                        打印</a>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="#" hidefocus="true"><s class="baochun"></s>保 存</a><a href="#" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a></div>
    </div>
</body>
</html>
