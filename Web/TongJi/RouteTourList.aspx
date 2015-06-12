<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteTourList.aspx.cs"
    Inherits="EyouSoft.Web.TongJi.RouteTourList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>统计分析-线路流量统计-团队数量查看明细页</title>
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
                <td width="44%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td width="15%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    出团时间
                </td>
                <td width="11%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    天数
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptTourList">
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
                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                        </td>
                        <td align="center">
                            <%# Eval("Days")%>
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
                    出团时间
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    天数
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
