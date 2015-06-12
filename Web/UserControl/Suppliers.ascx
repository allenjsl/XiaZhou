<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Suppliers.ascx.cs" Inherits="Web.UserControl.Suppliers" %>
<%@ Register Src="~/UserControl/DistributorNotice.ascx" TagName="Notice" TagPrefix="uc2" %>
<!-- 导航开始 -->
<div class="top">
	<div class="menubg">
    	<div style="width:371px; height:64px;">
            <asp:Literal runat="server" ID="ltrLogo"></asp:Literal>
        </div>
      <!-- InstanceBeginEditable name="导航" -->  
      	<ul class="menu fixed">
            <li><a class="indexicon" href="/logout.aspx"><span><s></s>首页</span></a></li>
            <li><a class="<%=ProcductClass %>" href='ProductList.aspx?sl=<%=Request.QueryString["sl"] %>'><span><s></s>产品投放</span></a></li>
            <li><a class="<%=OrderClass %>" href="OrderCenter.aspx?sl=<%=Request.QueryString["sl"] %>"><span><s></s>订单中心</span></a></li>
            <li><a class="<%=FinanceClass %>" href="FinanceList.aspx?sl=<%=Request.QueryString["sl"] %>"><span><s></s>财务管理</span></a></li>
            <li><a class="<%=SystemClass %>" href="PasswordSettings.aspx?sl=<%=Request.QueryString["sl"] %>"><span><s></s>系统设置</span></a></li>
        </ul><!-- InstanceEndEditable -->    </div>
</div>
<!-- 导航结束 -->
<!-- 公告开始 -->
<uc2:Notice ID="Notice1" runat="server" />
<!-- 公告结束 -->
