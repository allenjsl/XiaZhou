<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartSeller.aspx.cs" Inherits="EyouSoft.Web.TongJi.DepartSeller" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门业绩统计查看部门下销售员业绩</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div class="tanchuT" style="text-align: left">
            <a href="javascript:void(0);" onclick="PrintPage('liststyle');" class="toolbar_daochu">
                <img src="/images/dayin1-cy.gif" width="57" height="19" />
            </a><a class="toolbar_daochu" href="javascript:void(0);" onclick="toXls1(); return false;">
                <img src="/images/daochu-cy.gif" border="0" />
            </a>
        </div>
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    姓名
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单人数
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单数量
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
            </tr>
            <asp:Repeater runat="server" ID="rptSeller">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# GetXh(Container.ItemIndex + 1)%>
                        </td>
                        <td align="center">
                            <%# Eval("SellerName")%>
                        </td>
                        <td height="28" align="center">
                            <%# Eval("PeopleNum")%>
                        </td>
                        <td align="center">
                            <%# Eval("OrderNum")%>
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
                            <%# this.GetBfbString(Eval("GrossProfitRate"), 0)%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    姓名
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单人数
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单数量
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
            </tr>
            <tr>
                <td height="28" colspan="2" align="right" class="alertboxTableT">
                    合计：
                </td>
                <td height="28" align="center" class="alertboxTableT">
                    <strong>
                        <%= SumMoney.PeopleNum %></strong>
                </td>
                <td height="28" align="center" class="alertboxTableT">
                    <strong>
                        <%= SumMoney.OrderNum %></strong>
                </td>
                <td align="right" class="alertboxTableT">
                    <b class="fontblue">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.InCome, ProviderToMoney)%></b>
                </td>
                <td align="right" class="alertboxTableT">
                    <b class="fontgreen">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.Pay, ProviderToMoney)%></b>
                </td>
                <td align="right" class="alertboxTableT">
                    <b class="fontred">
                        <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.GrossProfit, ProviderToMoney)%></b>
                </td>
                <td align="left" class="alertboxTableT">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="23" colspan="8" align="right" class="alertboxTableT">
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
