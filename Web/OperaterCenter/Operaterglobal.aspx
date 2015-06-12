<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Operaterglobal.aspx.cs"
    Inherits="Web.OperaterCenter.Operaterglobal" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="OperaterMenu" TagPrefix="uc1" Src="~/UserControl/OperaterMenu.ascx" %>
<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="hr_10">
        </div>
        <div class="jd-mainbox fixed">
            <uc1:OperaterMenu ID="OperaterMenu1" runat="server" />
            <div class="jdcz-main" id="tabPlanStatus">
                <div class="hr_10">
                </div>
                <div class="jidiao-r">
                    <h2>
                        <p>
                            团队信息</p>
                    </h2>
                    <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01" id="zhanlai">
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                团号：
                            </th>
                            <td width="40%">
                                <asp:Literal ID="litTourCode" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                线路区域：
                            </th>
                            <td width="30%">
                                <asp:Literal ID="litAreaName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                线路名称：
                            </th>
                            <td>
                                <asp:Literal ID="litRouteName" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                天数：
                            </th>
                            <td>
                                <asp:Literal ID="litDays" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                出团时间：
                            </th>
                            <td>
                                <asp:Literal ID="litStartDate" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                人数：
                            </th>
                            <td>
                                <asp:Literal ID="litPeoples" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                回团时间：
                            </th>
                            <td>
                                <asp:Literal ID="litEndDate" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                带团导游：
                            </th>
                            <td>
                                <asp:Literal ID="litGuidNames" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                本团销售：
                            </th>
                            <td>
                                <asp:Literal ID="litSellers" runat="server"></asp:Literal>
                            </td>
                            <th align="right">
                                本团计调：
                            </th>
                            <td>
                                <asp:Literal ID="litOperaters" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                需安排计调项：
                            </th>
                            <td colspan="3">
                                <asp:Literal ID="litPlanItems" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                <img src="/images/deng.gif" width="18px" height="22px" />
                                内部信息：
                            </th>
                            <td style="" colspan="3" align="left">
                                <asp:Literal ID="ltrNeiBuXinXi" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                成本核算：
                            </th>
                            <td style="" colspan="3" align="left">
                                <asp:Literal ID="ltrChengBenHeSuan" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:PlaceHolder ID="tabAyencyView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排地接</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    地接社名称
                                </th>
                                <th width="9%" align="center">
                                    人数
                                </th>
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repAyencyList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2(Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=dijieshePrintUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="5">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="5">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabGuidView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排导游</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="8%" align="center" class="border-l">
                                    导游姓名
                                </th>
                                <th width="10%" align="center">
                                    导游电话
                                </th>
                                <th width="9%" align="center">
                                    上团时间
                                </th>
                                <th width="9%" align="center">
                                    下团时间
                                </th>
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repGuidList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("ContactPhone")%>
                                        </td>
                                        <td align="center">
                                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%>
                                        </td>
                                        <td align="center">
                                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("EndDate"),ProviderToDate)%>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=daoyouPrintUrl %>?tourId=<%# Eval("TourId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="10">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="10">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabHotelView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排酒店</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center">
                                    酒店名称
                                </th>
                                <th width="9%" align="center">
                                    房间数量
                                </th>
                                <!--<th width="9%" align="center">
                                    免费房数
                                </th>-->
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repHotelList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <!--<td align="center">
                                            <%# Eval("FreeNumber")%>
                                        </td>-->
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=hotelPrintUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="1">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="1">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabCarView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排车队</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center">
                                    车队名称
                                </th>
                                <th width="9%" align="center">
                                    用车时间
                                </th>
                                <th width="9%" align="center">
                                    车型
                                </th>
                                <th width="5%" align="center">
                                    数量
                                </th>
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repCarlist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%#(Convert.ToDateTime(Eval("StartDate")).ToString("M/d")) %>
                                            <%# Eval("StartTime") %>-
                                            <%# (Convert.ToDateTime(Eval("EndDate")).ToString("M/d")) %>
                                            <%# Eval("EndTime") %>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Models")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=carPrintUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="2">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="2">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabAirView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排机票</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    出票点
                                </th>
                                <th width="9%" align="center" class="addtableT">
                                    出票数
                                </th>
                                <th align="left" class="addtableT">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repAirList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=querenAirUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="9_1">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="9_1">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabTrainView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排火车票</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    出票点
                                </th>
                                <th width="9%" align="center">
                                    张数
                                </th>
                                <!--<th align="center">
                                    免费数量
                                </th>-->
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="reptrainList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <!--<td align="center">
                                            <%# Eval("FreeNumber")%>
                                        </td>-->
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=querenTrainUrl %>?planId=<%#Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="9_2">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="9_2">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabBusView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排汽车票</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    出票点
                                </th>
                                <th width="9%" align="center">
                                    张数
                                </th>
                                <th align="left" class="addtableT">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repBusList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=querenBusUrl %>?planId=<%#Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="9_3">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="9_3">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabAttrView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排景点</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center">
                                    景点名称
                                </th>
                                <th width="9%" align="center" class="border-l">
                                    游览时间
                                </th>
                                <th width="6%" align="center">
                                    人数
                                </th>
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repAttrList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%#(Convert.ToDateTime(Eval("StartDate")).ToString("M/d")) %>
                                            <%# Eval("StartTime") %>-
                                            <%# (Convert.ToDateTime(Eval("EndDate")).ToString("M/d")) %>
                                            <%# Eval("EndTime") %>
                                        </td>
                                        <td align="center">
                                            <b class="fontblue">
                                                <%# Eval("AdultNumber")%></b><sup class="fontred">+<%# Eval("ChildNumber")%></sup>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=jingdianPrintUrl %>?planId=<%#Eval("PlanId")%>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="3">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="3">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabForeignShipView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排涉外游轮</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    游船公司
                                </th>
                                <th width="9%" align="left">
                                    船名
                                </th>
                                <th width="6%" align="center">
                                    人数
                                </th>
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repForeignShipList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="left">
                                            <%# Eval("ShipName")%>
                                        </td>
                                        <td align="center">
                                            <b class="fontblue">
                                                <%# Eval("DNum","{0:F2}")%></b>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <a href='<%= querenforeirnUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="4_1">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="4_1">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabShipChinaView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排国内游轮</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    游船公司
                                </th>
                                <th width="9%" align="center">
                                    船名
                                </th>
                                <th width="6%" align="center">
                                    人数
                                </th>
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repChinaShipList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("ShipName")%>
                                        </td>
                                        <td align="center">
                                            <b class="fontblue">
                                                <%# Eval("DNum","{0:F2}")%></b>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%= querenchinaUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="4_2">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="4_2">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabDinView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排用餐</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    餐馆名称
                                </th>
                                <th width="9%" align="center">
                                    用餐时间
                                </th>
                                <th width="6%" align="center">
                                    人数
                                </th>
                                <th align="left">
                                    费用明细
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repDinList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=yongcanPrintUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="6">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="6">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabShopView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排购物</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    购物店名称
                                </th>
                                <th width="9%" align="center">
                                    人头数
                                </th>
                                <th align="left">
                                    返利标准
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repShopList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num") %>
                                        </td>
                                        <td align="left">
                                            <span title="<%# Eval("ServiceStandard") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("ServiceStandard").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %></span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=gouwuPrintUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="7">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="7">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabPickView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排领料</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    领料内容
                                </th>
                                <th width="9%" align="center">
                                    数量
                                </th>
                                <th align="center">
                                    领料人
                                </th>
                                <th align="center">
                                    单价
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repPickList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("ContactName")%>
                                        </td>
                                        <td align="center">
                                            <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("Price"),ProviderToMoney)%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="8">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="8">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="tabOtherView" runat="server" Visible="false">
                    <div class="jidiao-r">
                        <h2>
                            <p>
                                已安排其它</p>
                        </h2>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                            style="border-bottom: 1px solid #A9D7EC;">
                            <tr>
                                <th width="18%" align="center" class="border-l">
                                    供应商名称
                                </th>
                                <th width="9%" align="center">
                                    人数
                                </th>
                                <th align="center">
                                    支出项目
                                </th>
                                <th width="8%" align="center">
                                    支付方式
                                </th>
                                <th width="9%" align="right">
                                    结算费用
                                </th>
                                <th width="6%" align="center">
                                    状态
                                </th>
                                <th width="6%" align="center">
                                    确认单
                                </th>
                                <th width="12%" align="center">
                                    操作
                                </th>
                            </tr>
                            <asp:Repeater ID="repOtherList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("SourceName")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Num")%>
                                        </td>
                                        <td align="center">
                                            <span title="<%# Eval("CostDetail") %>">
                                                <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                        </td>
                                        <td align="center">
                                            <%# Eval("PaymentType").ToString()%>
                                        </td>
                                        <td align="right">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                            <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                                data-soucesname="<%# Eval("SourceName")%>">
                                                <img src="/images/yufu.gif" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <span <%# (EyouSoft.Model.EnumType.PlanStructure.PlanState)Eval("Status") == EyouSoft.Model.EnumType.PlanStructure.PlanState.未落实?"class='fontred' data-class='status'":"" %>>
                                                <%# Eval("Status").ToString() %>
                                            </span>
                                        </td>
                                        <td align="center">
                                            <a href='<%=qitaPrintUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                                <img src="/images/y-kehuqueding.gif" border="0" alt="" /></a>
                                        </td>
                                        <td align="center">
                                            <%if (ListPower)
                                              { %>
                                            <a href="javascript:void(0);" data-class="update" data-pid="11">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                            <a href="javascript:void(0);" data-class="delete">
                                                <img src="/images/y-delicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />删除</a>
                                            <%}
                                              else
                                              { %>
                                            <a href="javascript:void(0);" data-class="show" data-pid="11">
                                                <img src="/images/y-delupdateicon.gif" border="0" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                            <%} %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:PlaceHolder>
                <div class="hr_5">
                </div>
                <asp:PlaceHolder ID="BtnglobalAction" runat="server">
                    <div class="mainbox cunline fixed">
                        <ul>
                            <li class="cun-cy"><a href="javascript:" id="globalBtn">全局配置完毕</a></li>
                        </ul>
                    </div>
                </asp:PlaceHolder>
                <div class="hr_10">
                </div>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var globalPage = {
            tourID: '<%=Utils.GetQueryStringValue("tourId") %>',
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("type") %>',
            tourStatus: '<%=Utils.GetQueryStringValue("tourStatus") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _BindBtn: function() {
                $("#globalBtn").unbind("click");
                $("#globalBtn").click(function() {
                    if ($("#tabPlanStatus").find("[data-class='status']").length > 0) {
                        tableToolbar._showMsg("你还有未落实的计调项！请落实！");
                        return false;
                    } else {
                        $.newAjax({
                            type: "POST",
                            url: '/OperaterCenter/Operaterglobal.aspx?action=config&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID,
                            cache: false,
                            data: $("#tabPlanStatus").closest("form").serialize(),
                            dataType: "json",
                            success: function(data) {
                                if (data.result == "1") {
                                    tableToolbar._showMsg(data.msg, function() {
                                        window.location.href = '/OperaterCenter/Operaterglobal.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID;
                                    });

                                } else {
                                    tableToolbar._showMsg(data.msg);
                                }
                            },
                            error: function() {
                                tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                                    window.location.href = window.location.href;
                                });

                                return false;
                            }
                        });
                    }
                });

                //删除事件
                $("#tabPlanStatus").find("[data-class='delete']").unbind("click");
                $("#tabPlanStatus").find("[data-class='delete']").click(function() {
                    var planID = $(this).find("img").attr("data-Id");
                    if (planID) {
                        $.newAjax({
                            type: "POST",
                            url: '/OperaterCenter/Operaterglobal.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&action=delete&planID=' + planID,
                            cache: false,
                            dataType: 'json',
                            success: function(data) {
                                if (data.result == "1") {
                                    tableToolbar._showMsg(data.msg, function() {
                                        window.location.href = '/OperaterCenter/Operaterglobal.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID;
                                    });

                                }
                            },
                            error: function() {
                                tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                                    window.location.href = window.location.href;
                                });

                                return false;
                            }
                        });
                    }
                });

                //预付申请
                $("#tabPlanStatus").find("a[data-class='Prepaid']").unbind("click");
                $("#tabPlanStatus").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    globalPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });


                //修改事件 
                $("#tabPlanStatus").find("[data-class='update']").unbind("click");
                $("#tabPlanStatus").find("[data-class='update']").click(function() {
                    //计调项标识id
                    var pID = $(this).attr("data-pid");
                    //计调项id
                    var planID = $(this).find("img").attr("data-Id");
                    if (pID) {
                        if (pID == "9_1") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateAir&planID=" + planID;
                        }
                        else if (pID == "9_2") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateTrain&planID=" + planID;
                        }
                        else if (pID == "9_3") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateBus&planID=" + planID;
                        }
                        else if (pID == "4_2") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateC&planID=" + planID;
                        }
                        else {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=update&planID=" + planID;
                        }
                    }
                    return false;
                });

                //查看事件
                $("#tabPlanStatus").find("[data-class='show']").unbind("click");
                $("#tabPlanStatus").find("[data-class='show']").click(function() {
                    //计调项标识id
                    var pID = $(this).attr("data-pid");
                    //计调项id
                    var planID = $(this).find("img").attr("data-Id");
                    if (pID) {
                        if (pID == "9_1") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateAir&planID=" + planID + "&show=1";
                        }
                        else if (pID == "9_2") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateTrain&planID=" + planID + "&show=2";
                        }
                        else if (pID == "9_3") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateBus&planID=" + planID + "&show=3";
                        }
                        else if (pID == "4_2") {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=updateC&planID=" + planID + "&show=2";
                        }
                        else {
                            window.location.href = '/OperaterCenter/OperaterConfigPage.aspx?type=' + globalPage.type + '&sl=' + globalPage.sl + '&tourId=' + globalPage.tourID + '&pID=' + pID + "&action=update&planID=" + planID + "&show=1";
                        }
                    }
                    return false;
                });
            },
            _PageInit: function() {
                this._BindBtn();
            }
        };

        $(document).ready(function() {
            $("#tabPlanStatus").find(".jidiao-r:last").css("border-bottom", "1px solid #46ABDC");
            globalPage._PageInit();
        });
    </script>

</asp:Content>
