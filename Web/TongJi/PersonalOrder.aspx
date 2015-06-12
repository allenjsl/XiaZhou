<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalOrder.aspx.cs"
    Inherits="EyouSoft.Web.TongJi.PersonalOrder" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>统计分析-个人业绩统计-订单数量查看明细页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/js/table-toolbar.js"></script>
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockuI.js"></script>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="28" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    客户单位
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    出团时间
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    人数
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    收入
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    支出
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    毛利
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    毛利率
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    下单人
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptOrder">
                <ItemTemplate>
                    <tr>
                        <td align="center" style="height:28px;">
                            <%# GetXh(Container.ItemIndex + 1)%>
                        </td>
                        <td align="center">
                            <%# Eval("TourCode")%>
                        </td>
                        <td height="28" align="center">
                            <%# Eval("OrderCode")%>
                        </td>
                        <td align="left">
                            <%# Eval("RouteName")%>
                        </td>
                        <td align="left">
                            <%# Eval("BuyCompanyName")%>
                        </td>
                        <td align="center">
                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                        </td>
                        <td align="center">
                            <%# Eval("PeopleNum")%>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TotalIncome"), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TotalOutlay"), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GrossProfit"), ProviderToMoney)%></b>
                        </td>
                        <td align="center">
                            <%# GetBfbString(Eval("GrossProfitRate"), 2)%>
                        </td>
                        <td align="center">
                            <%# Eval("Operator")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>            
            <tr>
                <td height="28" colspan="6" align="right" class="alertboxTableT">
                    合计：
                </td>
                <td align="center">
                    <%= SumMoney.PeopleNum %>
                </td>
                <td align="right" class="alertboxTableT">
                    <b class="fontblue">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.InCome, ProviderToMoney)%></b>
                </td>
                <td align="right" class="alertboxTableT">
                    <b class="fontgreen">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.Pay, ProviderToMoney)%></b>
                </td>
                <td align="right">
                    <b class="fontred">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.GrossProfit, ProviderToMoney)%>
                    </b>
                </td>
                <td colspan="2" align="left" class="alertboxTableT">
                    &nbsp;
                </td>
            </tr>
        </table>
        
        <div style="position: relative; height: 32px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        
        <div class="alertbox-btn">
            <a href="javascript:void(0)" onclick="toXls1();return false;" style="text-indent: 0px;">导出</a>
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({});
        })

    </script>

</body>
</html>
