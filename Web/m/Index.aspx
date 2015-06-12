<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EyouSoft.Web.m.Index" %>

<%@ Register Src="../UserControl/Mobile/MobileHead.ascx" TagName="MobileHead" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>手机版-首页</title>
    <style type="text/css">
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
            padding: 2px;
        }
    </style>
</head>
<body>
    <uc1:MobileHead ID="MobileHead1" runat="server" />
    <form action="/m/List.aspx" method="get">
    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"]== null ? ((int)EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目).ToString() : Request.QueryString["sl"].ToString()%>">
    
    
    <div id="wapper">
        <div class="search">
            <ul>
                <li>
                    <label>团<s></s>号：</label>
                    <input type="text" class="text" name="txtTourCode" style="width: 120px;" /></li>
                <li>
                    <label>线路名称：</label>
                    <input type="text" class="text" name="txtRouteName" style="width: 120px;" /></li>
                <li>
                    <label>出团日期：</label>
                    <lable></lable>
                    <input name="txtStarTime" type="text" class="text" style="width: 65px;" />
                    -<input name="txtEndTime" type="text" class="text" style="width: 65px;" /></li>
            </ul>
            <input type="submit" value="搜索" style="width: 65px; height: 23px; margin-left: 55px;
                margin-top: 5px;" />
            <asp:Label ID="lblMsg" ForeColor="red" runat="server" Text=""></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
