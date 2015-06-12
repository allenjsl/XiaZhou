<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourCountlist.aspx.cs"
    Inherits="Web.ResourceManage.Path.Transactions" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/JS/jquery-1.4.4.js"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
            id="liststyle" style="margin: 0 auto">
            <tbody>
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;" class="odd">
                    <td height="23" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        序号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        团号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        团队名称
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        出团时间
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        天数
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        人数
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        收入
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        支出
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        毛利
                    </td>
                </tr>
                <asp:Repeater ID="RptList" runat="server">
                    <ItemTemplate>
                        <tr class="">
                            <td align="center">
                                <%#Container.ItemIndex + 1 %>
                            </td>
                            <td height="28" align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                    <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartOutDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("DayCount")%>
                            </td>
                            <td height="28" align="center">
                                <%#((EyouSoft.Model.SourceStructure.MPeopleCountModel)Eval("PeopleCount")).AdultCount%>+<%#((EyouSoft.Model.SourceStructure.MPeopleCountModel)Eval("PeopleCount")).ChildrenCount%>+<%#((EyouSoft.Model.SourceStructure.MPeopleCountModel)Eval("PeopleCount")).OtherCount%>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("IncomeMoney")), ProviderToMoney)%></b>
                            </td>
                            <td height="28" align="right">
                                <b class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("PayMoney")),ProviderToMoney)%></b>
                            </td>
                            <td height="28" align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("MaoriMoney")),ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;" class="odd">
                    <td height="23" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        序号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        团号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        团队名称
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        出团时间
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        天数
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        人数
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        收入
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        支出
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        毛利
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="position: relative; height: 32px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" onclick="GoustList.close()">关 闭</a>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
   var GoustList={
        close:function(){
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
        }
    }
</script>
