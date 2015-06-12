<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestAmount.aspx.cs" Inherits="EyouSoft.Web.TongJi.RestAmount" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>统计分析--收入对账单-未收查看</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div class="tanchuT" style="text-align: left">
            <a class="toolbar_daochu" href="javascript:void(0);" onclick="PrintPage('liststyle');">
                <img src="/images/dayin1-cy.gif" width="57" height="19" /></a> <a class="toolbar_daochu"
                    href="javascript:void(0);" onclick="toXls1(); return false">
                    <img src="/images/daochu-cy.gif" border="0"></a>
        </div>
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    出团时间
                </td>
                <td align="left" bgcolor="#b7e0f3" class="alertboxTableT">
                    客户单位
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    人数
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    应收款
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    已收款
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    未收款
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptRestAmount">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# GetXh(Container.ItemIndex + 1)%>
                        </td>
                        <td height="28" align="center">
                            <%# Eval("OrderCode")%>
                        </td>
                        <td align="left">
                            <%# Eval("RouteName")%>
                        </td>
                        <td align="center">
                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                        </td>
                        <td align="left">
                            <%# Eval("BuyCompanyName")%>
                        </td>
                        <td align="center">
                            <%# Eval("PeopleNum")%>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TotalAmount"), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("InAmount"), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <span style="padding-left: 20px; font-size: 14px; font-weight: bold; padding-bottom: 5px;
                                height: 20px;"><b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("RestAmount"), ProviderToMoney)%></b></span>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    出团时间
                </td>
                <td align="left" bgcolor="#b7e0f3" class="alertboxTableT">
                    客户单位
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    人数
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    应收款
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    已收款
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    未收款
                </td>
            </tr>
            <tr>
                <td height="28" colspan="6" align="right">
                    合计：
                </td>
                <td align="right">
                    <b class="fontblue">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.TotalAmount, ProviderToMoney)%></b>
                </td>
                <td align="right">
                    <b class="fontgreen">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.InAmount, ProviderToMoney)%></b>
                </td>
                <td align="right">
                    <span style="padding-left: 20px; font-size: 14px; font-weight: bold; padding-bottom: 5px;
                        height: 20px;"><b class="fontred">
                            <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.RestAmount, ProviderToMoney)%></b></span>
                </td>
            </tr>
            <tr>
                <td height="23" colspan="9" align="right" class="alertboxTableT">
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
