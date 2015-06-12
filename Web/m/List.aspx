<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="EyouSoft.Web.m.List" %>

<%@ Register Src="../UserControl/Mobile/MobileHead.ascx" TagName="MobileHead" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>手机版-导游报账</title>
    <style type="text/css">
        a
        {
            color: #137cbf;
            text-decoration: none;
        }
        .text
        {
            border: solid 1px #1c9bdd;
            height: 20px;
        }
        body, ul
        {
            font-size: 12px;
            margin: 0;
            padding: 0;
        }
        li
        {
            list-style-type: none;
            padding: 4px;
        }
    </style>
</head>
<body style="font-size: 12px;">
    <uc1:MobileHead ID="MobileHead1" runat="server" />
    <form id="form1" runat="server">
    <div id="wapper">
        <div class="search">
            <ul style="margin-left: 5px;">
                <li><b>线路名称(团号)</b></li>
                <asp:Repeater ID="replist" runat="server">
                    <ItemTemplate>
                        <li>
                            <div>
                                <%#(Container.ItemIndex+1)+(pageIndex-1)*pageSize %>.&nbsp;<a href='/m/baozhang.aspx?sl=<%#Request.QueryString["sl"] %>&source=1&tourId=<%# Eval("TourId") %>&tourType=<%#EyouSoft.Common.Utils.GetInt(Eval("TourType").ToString()) %>'>
                                    <%#Eval("RouteName")%>(<%# Eval("TourCode")%>)</a></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div style="margin-top: 3px;">
            <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Text=""></asp:Label>
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />
        </div>
        <div style="margin-top: 3px; margin-left: 5px;">
            <a href="/m/index.aspx?sl=<%=Request.QueryString["sl"] %>">首页</a></div>
    </div>
    </form>
</body>
</html>
