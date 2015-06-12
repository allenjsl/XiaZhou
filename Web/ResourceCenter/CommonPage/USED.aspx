<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="USED.aspx.cs" Inherits="Web.ResourceCenter.CommonPage.USED" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源预控-已使用数量</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/jquery.boxy.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        线路名称
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <span data-calss="sp_title"></span>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <span data-calss="sp_title"></span>电话
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        使用数量
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="29" align="center" style="padding: 3px 3px">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#GetGuideOrTouristStr(Eval("GuideList"), Eval("TravellerList"))%>
                            </td>
                            <td align="center">
                                <%#Eval("Count")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="14" style="padding: 3px 3px">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div style="position: relative; height: 32px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var USED = {
            PageInit: function() {
                var titleObj = { "3": "游客" };
                $("span[data-calss='sp_title']").html(titleObj[Boxy.queryString("sourceControlCategory") || "1"] || "导游");
            }
        }
        $(function() {
            USED.PageInit();
        })
    </script>

</body>
</html>
