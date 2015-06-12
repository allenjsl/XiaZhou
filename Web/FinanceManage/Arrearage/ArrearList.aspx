<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArrearList.aspx.cs" Inherits="Web.FinanceManage.Arrearage.ArrearList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>拖欠金额</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div class="tanchuT" style="text-align: left">
            <a href="javascript:void(0);" id="a_print" class="toolbar_dayin">
                <img src="/images/dayin1-cy.gif" border="0" /></a>&nbsp;<a href="javascript:void(0);" id="a_daochu" class="toolbar_daochu"><img
                    src="/images/daochu-excle.gif" border="0" /></a></div>
        <div class="tanchuT">
            <asp:Label ID="lbl_Title" runat="server" Text="Label"></asp:Label></div>
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="15" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
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
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    出团时间
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    下单时间
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    应付金额
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    已付金额
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    未付金额
                </td>
            </tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="left">
                            <%#Container.ItemIndex + 1 %>
                        </td>
                        <td height="28" align="center">
                            <%#Eval("TourCode")%>
                        </td>
                        <td height="28" align="left">
                            <%#Eval("RouteName")%>
                        </td>
                        <td height="28" align="center">
                            <%#Eval("AdultNum")%>+<%#Eval("ChildNum")%>
                        </td>
                        <td align="center">
                            <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                        </td>
                        <td align="center">
                            <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("OrderTime"), ProviderToDate)%>
                        </td>
                        <td height="28" align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Receivable"), ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Received"), ProviderToMoney)%></b>
                        </td>
                        <td height="28" align="right">
                            <b class="fontred">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Unreceivable"), ProviderToMoney)%></b>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr bgcolor="#e9f4f9">
                <td height="23" colspan="6" align="right" class="alertboxTableT">
                    合计：
                </td>
                <td align="right" class="alertboxTableT">
                    <b>
                        <asp:Label ID="lbl_receivable" runat="server" Text=""></asp:Label></b>
                </td>
                <td align="right" class="alertboxTableT">
                    <b class="fontgreen">
                        <asp:Label ID="lbl_received" runat="server" Text=""></asp:Label></b>
                </td>
                <td align="right" class="alertboxTableT">
                    <b class="fontred">
                        <asp:Label ID="lbl_unreceivable" runat="server" Text=""></asp:Label></b>
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
        $("#a_print").click(function() {
            PrintPage("#a_print");
            return false;
        })
        $("#a_daochu").click(function() {
            toXls1();
            return false;
        })
    </script>

</body>
</html>
