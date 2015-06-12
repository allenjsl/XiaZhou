<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomUseInfo.aspx.cs" Inherits="Web.UserCenter.WorkAwake.Preview.RoomUseInfo" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <% if (sueType == (int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.车辆 || sueType == (int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.酒店 || sueType == (int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.景点 || sueType == (int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.其他)
               {%>
            <table width="99%" cellspacing="0" cellpadding="0" border="0" style="margin: 0 auto;">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            团号
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            线路名称
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            导游
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            导游电话
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            使用数量
                        </td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td height="29" align="center">
                                    <%#Eval("TourCode")%>
                                </td>
                                <td align="center">
                                    <%#Eval("RouteName")%>
                                </td>
                                <td align="center">
                                    <%#GetdyInfo(Eval("GuideList"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Count")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <%} %>
            <% if (sueType == (int)EyouSoft.Model.EnumType.SourceStructure.SourceControlCategory.游轮)
               {%>
            <table width="99%" cellspacing="0" cellpadding="0" border="0" style="margin: 0 auto;">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            团号
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            线路名称
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            游客
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            游客电话
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            使用数量
                        </td>
                    </tr>
                    <asp:Repeater ID="rptListShip" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td height="29" align="center">
                                    <%#Eval("TourCode")%>
                                </td>
                                <td align="center">
                                    <%#Eval("RouteName")%>
                                </td>
                                <td align="center">
                                    <%# GetykInfo(Eval("TravellerList"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Count")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <%} %>
            <div style="width: 100%; text-align: center;">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div style="border: 0 none;">
        <div class="pages">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
