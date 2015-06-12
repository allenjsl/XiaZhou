<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourPlanInfo.ascx.cs"
    Inherits="Web.UserControl.TourPlanInfo" %>
<div class="addContent-box jd-addContent-box" style="padding-bottom: 0;">
    <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
        <tr>
            <td class="addtableT">
                线路名称：
            </td>
            <td class="kuang2">
                <asp:Literal ID="litRouteName" runat="server"></asp:Literal>
            </td>
            <td class="addtableT">
                团号：
            </td>
            <td class="kuang2">
                <asp:Literal ID="litTourId" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="117" height="14" class="addtableT">
               出发时间：
            </td>
            <td class="kuang2">
                <asp:Literal ID="litTourendInfo" runat="server"></asp:Literal>
            </td>
            <td class="addtableT">
               出发交通：
            </td>
            <td class="kuang2">
                <asp:Literal ID="litTourStartInfo" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td height="14" class="addtableT">
                销售员：
            </td>
            <td class="kuang2">
                <asp:Literal ID="LitSellerName" runat="server"></asp:Literal>
            </td>
            <td class="addtableT">
                人数：
            </td>
            <td class="kuang2">
                <asp:Literal ID="LitPeople" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td height="14" class="addtableT">
                内部信息：
            </td>
            <td colspan="3" align="left" class="kuang2">
                <p>
                   <asp:Literal ID="LitInterInfo" runat="server"></asp:Literal>
                </p>
            </td>
        </tr>
        <tr>
            <td height="14" class="addtableT">
                计调需知：
            </td>
            <td colspan="3" class="kuang2">
                <asp:Literal ID="LitOperNeet" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</div>
