<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BianGeng.aspx.cs" Inherits="EyouSoft.Web.m.BianGeng" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../UserControl/Mobile/MobileHead.ascx" TagName="MobileHead" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>手机版-变更</title>
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
        a
        {
            color: #137cbf;
            text-decoration: none;
        }
    </style>
</head>
<body style="font-size:12px">
    <uc1:MobileHead ID="MobileHead1" runat="server" />
    <form method="post" runat="Server">
    
    <div id="wapper">
       <div id="Daoyou" runat="Server">
          <ul>
              <li>
                  <label>人数：</label>
                  <input type="text" class="text" id="txtpeople" style="width:80px;" runat="server" />
              </li>
              <li>
                  <label>费用：</label>
                  <input type="text" class="text" id="txtfeiyong" style="width:80px;" runat="server"/>                    </li>
              <li>
                  <label>备<s></s>注：</label>
                  <textarea class="text" id="txtbeizhu" style="width:150px; height:55px;" runat="server"></textarea> 
              </li>
         </ul>
           <%if (Utils.GetQueryStringValue("type") == "add")
             {%>
           <input type="submit" value=" 增加 " style="width: 65px; height: 23px; margin-left: 55px; margin-top: 5px;" />
           <%}
             else
             { %>
           <input type="submit" value=" 减少 " style="width: 65px; height: 23px; margin-left: 55px; margin-top: 5px;"/>
             <%} %>
           <asp:Label ID="lblMsg" ForeColor="red" runat="server" Text=""></asp:Label>
        </div>
        <div style="margin-top: 3px; margin-left: 5px;">
        <a href="/m/Index.aspx?sl=<%=Request.QueryString["sl"] %>">首页</a>
        <a href="/m/BaoZhang.aspx?sl=<%=Request.QueryString["sl"] %>&tourId=<%=Request.QueryString["tourId"] %>">上一页</a></div>
      
    </div>
    </form>
</body>
</html>
