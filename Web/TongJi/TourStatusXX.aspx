<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourStatusXX.aspx.cs" Inherits="EyouSoft.Web.TongJi.TourStatusXX"
    MasterPageFile="~/MasterPage/Boxy.Master" %>
    
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <div style="width: 99%; margin: 0px auto;">
            <div style="line-height:24px; margin-bottom:5px;"><b>[团号：<asp:Literal runat="server" ID="ltrTourCode"></asp:Literal>]状态明细</b></div>
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">                
                <tr style="font-weight:bold;">
                    <td style="height:30px">操作时间</td>
                    <td>操作人</td>
                    <td>状态</td>                    
                    <td>备注</td>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                <tr>
                    <td style="height:26px;"><%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}") %></td>                    
                    <td><%#Eval("Operator") %></td>
                    <td><%#GetTourStatus(Eval("TourStatus")) %></td>
                    <td><%#Eval("YuanYin") %></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                    <td style="height:28px" colspan="4">暂无状态信息</td>
                </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="alertbox-btn"></div>
    </div>
</asp:Content>
