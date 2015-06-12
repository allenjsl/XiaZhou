<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Print.Master"
    ValidateRequest="false" CodeBehind="cheliangyukongquerendan.aspx.cs" Inherits="EyouSoft.Web.PrintPage.xz.cheliangyukongquerendan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2">
        <tr>
            <td height="40" align="center">
                <b class="font24">车辆-最终确认单</b>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder runat="server" ID="ph_tuandui">
        <asp:Repeater runat="server" ID="rpt_tuandui">
            <HeaderTemplate>
                <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="0"
                    class="list_2">
                    <tr>
                        <th width="163" align="center">
                            团号
                        </th>
                        <th width="229" align="left">
                            线路名称
                        </th>
                        <th width="74" align="center">
                            导游
                        </th>
                        <th width="149" align="center">
                            导游电话
                        </th>
                        <th width="81" align="center">
                            使用数量
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%#Eval("TourCode") %>
                    </td>
                    <td align="left">
                        <%#Eval("RouteName")%>
                    </td>
                    <td align="center">
                        <%#GetGuideOrTouristStr(Eval("GuideList"))%>
                    </td>
                    <td align="center">
                        <%#Eval("Count")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:PlaceHolder>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="0"
        class="list_2">
        <tr>
            <th width="100" align="right">
                车队名称
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbCarName">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                车型
            </th>
            <td>
                <asp:Label runat="server" ID="lbCarType">
                </asp:Label>
            </td>
            <th width="100" align="right">
                控车数量
            </th>
            <td>
                <asp:Label runat="server" ID="lbCarNum">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                预控日期
            </th>
            <td>
                <asp:Label runat="server" ID="lbPreDate">
                </asp:Label>
            </td>
            <th align="right">
                最后保留日期
            </th>
            <td>
                <p>
                    <asp:Label runat="server" ID="lbLastDate">
                    </asp:Label></p>
            </td>
        </tr>
        <tr>
            <th align="right">
                单价
            </th>
            <td>
                <asp:Label runat="server" ID="lbCarPrice">
                </asp:Label>
            </td>
            <th align="right">
                总价
            </th>
            <td>
                <asp:Label runat="server" ID="lbTotalPrice">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <th align="right">
                备注
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbRemark">
                </asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
