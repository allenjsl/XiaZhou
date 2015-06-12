<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoZhang.aspx.cs" Inherits="EyouSoft.Web.m.BaoZhang" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../UserControl/Mobile/MobileHead.ascx" TagName="MobileHead" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        a
        {
            color: #137cbf;
            text-decoration: none;
        }
        .text
        {
            border: solid 1px #1c9bdd;
            height: 17px;
            width: 65px;
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
        span
        {
            color: #1184c9;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <uc1:MobileHead ID="MobileHead1" runat="server" />
    <form method="post" runat="Server">
    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
    <div class="baozh">
        <ul>
            <li><b>【团队支出】</b></li>
            <li style="background-color: #ececec">
                <asp:Literal ID="litFrist" runat="server"></asp:Literal></li>
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <li>
                        <%#(Container.ItemIndex+1) %>.&nbsp;
                        <%#Eval("Type").ToString()%>&nbsp;&nbsp;
                        <%#Eval("SourceName")%>&nbsp;&nbsp;<%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),this.ProviderToMoney) %>&nbsp;&nbsp;
                        <%if (this.PrivsPage=="1")
                          { %>
                        &nbsp;&nbsp;<a href="/m/BianGeng.aspx?sl=<%#Request.QueryString["sl"]%>&type=add&plantype=<%#(int)Eval("Type")%>&tourId=<%#Request.QueryString["tourId"]%>&planid=<%#Eval("PlanId")%>">增</a>&nbsp;&nbsp;
                        <a href="/m/BianGeng.aspx?sl=<%#Request.QueryString["sl"]%>&type=red&plantype=<%#(int)Eval("Type")%>&tourId=<%#Request.QueryString["tourId"]%>&planid=<%#Eval("PlanId")%> ">
                            减</a> 
                            <%}%></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <b></b>
        <ul>
            <li><b>【导游收入】</b></li>
            <li style="background-color: #ececec">
                <asp:Literal ID="litSecond" runat="server"></asp:Literal>
            </li>
            <asp:Repeater ID="repGuidInMoney" runat="server">
                <ItemTemplate>
                    <li>
                        <%#(Container.ItemIndex+1) %>.&nbsp;<%#Eval("BuyCompanyName")%>&nbsp;&nbsp;
                        <input type="hidden" name="hidIncome" value='<%#Eval("GuideIncome").ToString() %>' />
                        <input type="hidden" name="hidOrderId" value="<%#Eval("OrderId")%>" />
                        <input name="txtRealIncome" class="text" type="text" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("GuideRealIncome").ToString()))%>" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <ul>
            <li><b>【导游借款】</b></li>
            <li style="background-color: #ececec">
                <asp:Literal ID="litThird" runat="server"></asp:Literal></li>
            <asp:Repeater ID="rptDebit" runat="server">
                <ItemTemplate>
                    <li>
                        <%#(Container.ItemIndex+1) %>.&nbsp;<%#Eval("Borrower")%>&nbsp;&nbsp;
                        <%# EyouSoft.Common.UtilsCommons.GetDateString( Eval("BorrowTime"),ProviderToDate)%>&nbsp;&nbsp;
                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("RealAmount"),ProviderToMoney)%>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <ul>
            <li><b>【报账汇总】</b></li>
            <li>导游收入:
                <asp:Literal ID="lbl_guidesIncome" runat="server" Text="0"></asp:Literal></li>
            <li>导游支出:<asp:Literal ID="lbl_guidesSpending" runat="server" Text="0"></asp:Literal></li>
            <li>导游借款:<asp:Literal ID="lbl_guidesBorrower" runat="server" Text="0"></asp:Literal></li>
            <li>补领/归还:
                <asp:Literal ID="lbl_replacementOrReturn" runat="server" Text="0"></asp:Literal></li>
        </ul>
    </div>
    <div class="search_btn">
   <%if (this.PrivsPage == "1")
      { %>
        <asp:Button ID="butSave" runat="server" Text="保存" onclick="butSave_Click"/>
        <asp:Button ID="butSellsExamineV" runat="server" Visible="false" Text="提交销售审核"
            onclick="butSellsExamineV_Click" />
         <%} %>
        <asp:Label ID="lblMsg" ForeColor="red" runat="server" Text=""></asp:Label>
    </div>
    <div style="margin-top: 3px; margin-left: 5px;">
        <a href="/m/index.aspx?sl=<%=Request.QueryString["sl"] %>">首页</a>&nbsp;&nbsp;<a href="/m/list.aspx?sl=<%=Request.QueryString["sl"] %>">上一页</a></div>
    </form>
</body>
</html>
