﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserCenterNavi.ascx.cs"
    Inherits="Web.UserControl.UserCenterNavi" %>
<ul class="fixed">
    <asp:PlaceHolder ID="phnav_1" runat="server">
        <li><a id="nav_1" runat="server" href="/UserCenter/WorkAwake/OrderRemind.aspx?sl=<%= (int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            订单提醒</a></li></asp:PlaceHolder>
    <asp:PlaceHolder ID="phnav_2" runat="server">
        <li><a id="nav_2" runat="server" href="/UserCenter/WorkAwake/OperaterRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            计调提醒</a></li></asp:PlaceHolder>
    <asp:PlaceHolder ID="phnav_5" runat="server">
        <%--<li><a id="nav_3" runat="server" href="/UserCenter/WorkAwake/LeaveRemind.aspx?sl=1">出团提醒</a></li>
    <li><a id="nav_4" runat="server" href="/UserCenter/WorkAwake/BackRemind.aspx?sl=1">回团提醒</a></li>--%>
        <li><a id="nav_5" runat="server" href="/UserCenter/WorkAwake/CollectionRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            收款提醒</a></li></asp:PlaceHolder>
    <asp:PlaceHolder ID="phnav_7" runat="server">
        <%--<li><a id="nav_6" runat="server" href="/UserCenter/WorkAwake/PaymentRemind.aspx?sl=1">付款提醒</a></li>--%>
        <li><a id="nav_7" runat="server" href="/UserCenter/WorkAwake/ChangeRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            变更提醒</a></li></asp:PlaceHolder>
    <asp:PlaceHolder ID="phnav_8" runat="server">
        <li><a id="nav_8" runat="server" href="/UserCenter/WorkAwake/Preview/PreviewHotel.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            预控到期提醒</a></li></asp:PlaceHolder>
    <asp:PlaceHolder ID="phnav_9" runat="server">
        <li><a id="nav_9" runat="server" href="/UserCenter/WorkAwake/LaborRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            劳动合同到期</a></li></asp:PlaceHolder>
    <asp:PlaceHolder ID="phnav_10" runat="server">
        <li><a id="nav_10" runat="server" href="/UserCenter/WorkAwake/InquiryRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            询价提醒</a></li></asp:PlaceHolder>
    <asp:PlaceHolder ID="phnav_11" runat="server">
        <li><a id="nav_11" runat="server" href="/UserCenter/WorkAwake/BirthdayRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
            生日提醒</a></li>
    </asp:PlaceHolder>
</ul>
