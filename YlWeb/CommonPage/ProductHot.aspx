<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductHot.aspx.cs" Inherits="EyouSoft.YlWeb.CommonPage.ProductHot" %>
<%@ OutputCache Duration="3600" VaryByParam="TypeId" %>
<%@ Register src="../UserControl/ProductHot.ascx" tagname="ProductHot" tagprefix="uc1" %>
<uc1:ProductHot ID="ProductHot1" runat="server" />