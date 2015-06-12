<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteTotalOutlay.aspx.cs"
    Inherits="EyouSoft.Web.TongJi.RouteTotalOutlay" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>统计分析-线路流量统计-总支出查看明细页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td width="9%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td width="21%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号
                </td>
                <td width="35%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td width="9%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    人数
                </td>
                <td width="26%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    支出金额
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptOutlay">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# GetXh(Container.ItemIndex + 1)%>
                        </td>
                        <td height="28" align="center">
                            <%# Eval("TourCode")%>
                        </td>
                        <td align="left">
                            <%# Eval("RouteName")%>
                        </td>
                        <td align="center">
                            <%# Eval("PeopleNum")%>
                        </td>
                        <td align="right">
                            <b class="fontgreen"><%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PayMoney"), ProviderToMoney)%></b>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    人数
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    支出金额
                </td>
            </tr>
            <tr>
                <td height="28" colspan="5" align="right" class="alertboxTableT">
                    金额合计：<b class="fontgreen"><%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumIncome, ProviderToMoney)%></b>
                </td>
            </tr>
            <tr>
                <td height="23" colspan="5" align="right" class="alertboxTableT">
                    <div style="position: relative; height: 32px;">
                        <div class="pages">
                            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({});
        })

    </script>

</body>
</html>
