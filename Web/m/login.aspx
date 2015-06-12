<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="EyouSoft.Web.m.login" %>

<%@ Register Src="../UserControl/Mobile/MobileHead.ascx" TagName="MobileHead" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .text
        {
            border: solid 1px #1c9bdd;
            width: 150px;
            height: 20px;
        }
        body
        {
            font-size: 12px;
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body style="font-size: 12px">
    <uc1:MobileHead ID="MobileHead1" runat="server" />
    <form id="form1" runat="server">
    <div style="width: 98%; padding: 5px;">
        帐号:<input type="text" name="u" class="text" /><br />
        <br />
        密码:<input type="password" name="p" class="text" /><br />
        <br />
        <input type="submit" value="登录" style="width: 65px; height: 23px; margin-left:55px;" />
        <br />
        <asp:Label ID="lblMsg" ForeColor="red" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
