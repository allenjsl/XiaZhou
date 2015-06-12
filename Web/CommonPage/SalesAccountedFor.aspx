<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SalesAccountedFor.aspx.cs" Inherits="Web.CommonPage.SalesAccountedFor" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/UserControl/TourMoneyOut.ascx" TagName="TourMoneyOut" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table class="firsttable" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                    <tr>
                        <td class="addtableT">
                            团号：
                        </td>
                        <td class="kuang2" style="width: 150px">
                            <asp:Label ID="lbl_TourCode" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT" style="width: 80px">
                            线路区域：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_AreaName" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            线路名称：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_RouteName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            天数：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_TourDays" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            出团时间：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_LDate" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            回团时间：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_RDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            带团导游：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_TourGride" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            出发交通：
                        </td>
                        <td colspan="3" class="kuang2">
                            <asp:Label ID="lbl_LTraffic" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            本团销售：
                        </td>
                        <td class="kuang2">
                            <asp:HiddenField ID="hideSaleId" runat="server" />
                            <asp:Label ID="lbl_SaleInfo" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            返程交通：
                        </td>
                        <td colspan="3" class="kuang2">
                            <asp:Label ID="lbl_RTraffic" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="117" class="addtableT">
                            本团计调：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_TourPlaner" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            集合方式：
                        </td>
                        <td colspan="3" class="kuang2">
                            <asp:Label ID="lbl_Gather" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <asp:PlaceHolder ID="phdTourMoneyIn" runat="server" Visible="false">
            <div class="addContent-box">
                <span class="formtableT">团队收入(派团)</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="Table1">
                    <tr>
                        <th>
                            订单号
                        </th>
                        <th>
                            下单人
                        </th>
                        <th>
                            销售员
                        </th>
                        <th>
                            下单时间
                        </th>
                        <th>
                            客源单位
                        </th>
                        <th>
                            确认合同金额
                        </th>
                    </tr>
                    <asp:Repeater ID="repTourMoneyInList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <a href="javascript:void(0);" data-class="orderInfo" data-orderid="<%# Eval("OrderId") %>"
                                        data-tourtype="<%# Convert.ToInt32(Eval("TourType")) %>" data-tourid="<%# Eval("TourId") %>">
                                        <%# Eval("OrderCode")%></a>
                                </td>
                                <td align="center">
                                    <%# Eval("Operator")%>
                                </td>
                                <td align="center">
                                    <%# Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"),ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%# Eval("BuyCompanyName")%>
                                </td>
                                <td align="right">
                                    <b class="<%#(bool)Eval("ConfirmMoneyStatus")?"":"fontred" %>">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), ProviderToMoney)%></b>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr id="trTourCountMsg" runat="server" visible="false">
                        <th colspan="6" align="center">
                            <asp:Label ID="labTourMsg" runat="server"></asp:Label>
                        </th>
                    </tr>
                </table>
            </div>
            <div class="hr_5">
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="orderListView" runat="server" Visible="false">
            <div class="addContent-box">
                <span class="formtableT">团队收入(散拼)</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tabOrderList">
                    <tr>
                        <th>
                            订单号
                        </th>
                        <th>
                            下单人
                        </th>
                        <th>
                            销售员
                        </th>
                        <th>
                            下单时间
                        </th>
                        <th>
                            订单收入
                        </th>
                        <th>
                            订单结算
                        </th>
                        <th>
                            订单利润
                        </th>
                        <th data-class="orderBZ">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="repOrderList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <a href="javascript:void(0);" data-class="orderInfo" data-orderid="<%# Eval("OrderId") %>"
                                        data-tourtype="<%# Convert.ToInt32(Eval("TourType")) %>" data-tourid="<%# Eval("TourId") %>">
                                        <%# Eval("OrderCode")%></a>
                                </td>
                                <td align="center">
                                    <%# Eval("Operator")%>
                                </td>
                                <td align="center">
                                    <%# Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"),ProviderToDate)%>
                                </td>
                                <td align="right">
                                    <b class="<%#(bool)Eval("ConfirmMoneyStatus")?"":"fontred" %>">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"),ProviderToMoney)%></b>
                                </td>
                                <td align="right">
                                    <b class="<%#(bool)Eval("ConfirmMoneyStatus")?"":"fontred" %>">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmSettlementMoney"),ProviderToMoney)%></b>
                                </td>
                                <td align="right">
                                    <b class="<%#(bool)Eval("JieSuanStatus")?"":"fontred" %>">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"),ProviderToMoney)%></b>
                                </td>
                                <td align="center" data-class="orderBZ">
                                    <a href="javascript:void(0);" data-class="orderBZ" data-orderid="<%# Eval("OrderId") %>"
                                        data-tourtype="<%# (int)(Eval("TourType")) %>" data-tourstatus="<%=(int)status %>">
                                        订单报账</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr id="trSanPingOrderCount" runat="server" visible="false">
                        <th colspan="8" align="center">
                            <asp:Label ID="labOrderMsg" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr id="trCountView" runat="server">
                        <td colspan="4" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Literal ID="litConfirmMoneyCount" runat="server"></asp:Literal></b>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Literal ID="litConfirmSettlementMoneyCount" runat="server"></asp:Literal></b>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Literal ID="litProfitCount" runat="server"></asp:Literal></b>
                        </td>
                        <td align="center" data-class="orderBZ">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="hr_5">
            </div>
        </asp:PlaceHolder>
        <uc1:TourMoneyOut ID="TourMoneyOut" runat="server" />
        <div class="hr_5">
        </div>
        <asp:PlaceHolder ID="panMoneyViewGuid" runat="server">
            <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">导游收入</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tabGuidMoneyIn">
                    <tr>
                        <th>
                            订单号
                        </th>
                        <th align="left">
                            客源单位
                        </th>
                        <th align="right">
                            导游应收
                        </th>
                        <th align="right">
                            导游实收
                        </th>
                        <th align="left">
                            备注
                        </th>
                        <th width="12%" align="center" data-calss="td_GuidInMoney">
                            <strong>操作</strong>
                        </th>
                    </tr>
                    <asp:Repeater ID="repGuidInMoney" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td width="16%" align="center" >
                                    <span class="<%#IsBaoCunDaoYouShouRu(Eval("OrderId"))?"":"fontred" %>">
                                        <%#Eval("OrderCode")%></span><input name="txtOrderCode" type="hidden" value="<%#Eval("OrderCode")%>" />
                                    &nbsp;&nbsp;<input type="hidden" name="OrderIdhid" value="<%# Eval("OrderId") %>" />
                                    &nbsp;
                                </td>
                                <td width="18%" align="left">
                                    <%#Eval("BuyCompanyName")%><input name="txtbuyCompany" type="hidden" value="<%#Eval("BuyCompanyName")%>" />
                                    &nbsp;&nbsp;
                                </td>
                                <td width="8%" align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GuideIncome"),ProviderToMoney)%></b>
                                    <input name="txtGuideIncome" type="hidden" value="<%# Eval("GuideIncome") %>" />
                                </td>
                                <td width="8%" align="right">
                                    <input name="txtRealIncome" type="text" class="inputtext formsize120" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("GuideRealIncome").ToString()))%>" />
                                </td>
                                <td width="38%" align="left">
                                    <textarea name="txtConfirmRemark" style="width: 96%; height: 28px;" class="inputtext"><%#Eval("GuideRemark")%></textarea>
                                </td>
                                <td align="center" data-calss="td_GuidInMoney">
                                    <%if (IsSubmit && IsChangeDaoYou)
                                      {%>
                                    <a href="javascript:void(0);" class="addbtn" data-class="updateOrder" data-orderid="<%# Eval("OrderId") %>">
                                        <img src="/images/updateimg.gif" border="0" />
                                    </a>
                                    <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="hr_10">
                </div>
            </div>
            <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">其它收入</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tabFreeItemList">
                    <tr>
                        <th width="13%">
                            收入类型
                        </th>
                        <th width="29%" align="left">
                            付款单位
                        </th>
                        <th width="8%" align="right">
                            金额
                        </th>
                        <th width="10%" align="center">
                            支付方式
                        </th>
                        <th width="28%">
                            备注
                        </th>
                        <th width="12%" align="center">
                            <strong>操作</strong>
                        </th>
                    </tr>
                    <asp:Repeater ID="repOtherMoneyIn" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Eval("FeeItem")%><input type="hidden" name="txtFreeItem" value="<%#Eval("FeeItem")%>" />
                                    &nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <%#Eval("Crm")%>
                                    <input type="hidden" name="hidcrmId" value="<%#Eval("CrmId")%>" />
                                    <input type="hidden" name="hidcrmName" value="<%#Eval("Crm")%>" />
                                </td>
                                <td align="right">
                                    <input name="txtFeeAmount" type="text" class="inputtext formsize50" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("FeeAmount").ToString()))%>" />
                                </td>
                                <td align="center">
                                    <%#GetPayMentStr(Eval("PayType").ToString(),out IsChangeShouRu)%>
                                </td>
                                <td align="left">
                                    <textarea name="txtRemark" style="width: 96%; height: 35px;" class="inputtext"><%#Eval("Remark")%></textarea>
                                </td>
                                <td align="center">
                                    <asp:PlaceHolder runat="server" Visible='<%# GetOutFreeStatus((EyouSoft.Model.EnumType.FinStructure.FinStatus)EyouSoft.Common.Utils.GetInt(Eval("Status").ToString())) %>'>
                                        <a href="javascript:void(0);" style='<%#IsChangeShouRu ? "": "display:none" %>' class="addbtn"
                                            data-class="updateFreeItem" data-id="<%# Eval("Id") %>">
                                            <img src="/images/updateimg.gif" border="0" alt="" />
                                        </a>
                                        <%#(EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus)Eval("IsGuide")!= EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加 ? "<a href='javascript:void(0);' style='" + (IsChangeShouRu ? "" : "display:none") + "' class='addbtn' data-class='deleteFreeItem' data-id='" + Eval("Id") + "'><img src='/images/delimg.gif' border='0' alt='' /></a>" : ""%>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="panMoneyInView" runat="server">
                        <tr>
                            <td align="center">
                                <input name="txtFreeItem" type="text" class="inputtext formsize80" />
                            </td>
                            <td align="left">
                                <uc2:CustomerUnitSelect ID="UnitSelect1" runat="server" IsUniqueness="false" />
                            </td>
                            <td align="right">
                                <input name="txtFeeAmount" type="text" class="inputtext formsize50" value="0" />
                            </td>
                            <td align="center">
                                <%=GetPayMentStr("",out IsChangeShouRu)%>
                            </td>
                            <td align="left">
                                <textarea name="txtRemark" style="width: 96%; height: 35px;" class="inputtext"></textarea>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="addbtn" data-class="addOtherFreeItem">
                                    <img src="/images/addimg.gif" border="0" /></a>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
                <div class="hr_10">
                </div>
            </div>
        </asp:PlaceHolder>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">导游借款</span>
            <table class="add-baojia" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                    <tr>
                        <th width="25%" height="35">
                            姓名
                        </th>
                        <th width="25%">
                            借款时间
                        </th>
                        <th width="25%" align="right">
                            实借金额
                        </th>
                        <th width="25%">
                            领用签单数
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_Debit" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Eval("Borrower")%>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.UtilsCommons.GetDateString( Eval("BorrowTime"),ProviderToDate)%>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("RealAmount"),ProviderToMoney)%></b>
                                </td>
                                <td align="center">
                                    <%#Eval("RelSignNum")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_DebitMsg" runat="server">
                        <tr align="center">
                            <td colspan="4">
                                暂无导游借款信息
                            </td>
                        </tr>
                    </asp:Panel>
                </tbody>
            </table>
            <div class="hr_5">
            </div>
        </div>
        <div class="hr_5">
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">报账汇总</span>
            <table id="tab_RSummary" width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="14%" height="20" align="right">
                        导游收入
                    </th>
                    <th width="13%" align="right">
                        导游借款
                    </th>
                    <th width="14%" align="right">
                        导游支出
                    </th>
                    <th width="14%" align="right">
                        补领/归还
                    </th>
                    <th width="15%" align="center">
                        实领签单数
                    </th>
                    <th width="15%" align="center">
                        已使用签单数
                    </th>
                    <th width="15%" align="center">
                        归还签单数
                    </th>
                </tr>
                <tr>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_guidesIncome" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="13%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_guidesBorrower" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_guidesSpending" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_replacementOrReturn" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="15%" align="center">
                        <asp:Label ID="lbl_RCSN" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="15%" align="center">
                        <asp:Label ID="lbl_HUSN" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="15%" align="center">
                        <asp:Label ID="lbl_RSN" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">团队收支表</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="25%" align="right">
                        团队收入
                    </th>
                    <th width="25%" align="right">
                        团队支出
                    </th>
                    <th width="25%" align="right">
                        团队利润
                    </th>
                    <th width="25%" align="right">
                        利润率
                    </th>
                </tr>
                <tr>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoneyIn" runat="server" Text="0">0</asp:Label></b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoneyOut" runat="server" Text="0">0</asp:Label></b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoney" runat="server" Text="0">0</asp:Label></b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoneyRate" runat="server" Text="0">0</asp:Label></b>
                    </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
        </div>
        
        <asp:PlaceHolder runat="server" ID="phDaiShou">
        <div class="hr_5"></div>
        <div class="tablelist-box" style="width:98.5%">
            <span class="formtableT">供应商代收</span> <a href="javascript:void(0)" id="i_a_daishouinsert">代收登记</a>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th style="text-align:left;">订单号</th>
                    <th style="text-align:left;">客户单位</th>
                    <th style="text-align:left;">供应商</th>
                    <th style="text-align:left;">代收时间</th>
                    <th style="text-align:right;">代收金额&nbsp;</th>
                    <th style="text-align:left;">状态</th>
                    <th style="text-align:left;">代收备注</th>
                    <th style="text-align:left;">操作</th>
                </tr>
                <asp:Repeater runat="server" ID="rptDaiShou">
                <ItemTemplate>
                <tr i_daishouid="<%#Eval("DaiShouId") %>">
                    <td><%#Eval("OrderCode") %></td>
                    <td><%#Eval("CrmName") %></td>
                    <td><%#Eval("GysName") %></td>
                    <td><%#Eval("Time","{0:yyyy-MM-dd}") %></td>
                    <td style="text-align: right;" class="<%#(int)Eval("Status")==1?"":"fontred" %>"><%#Eval("JinE","{0:C2}") %>&nbsp;</td>
                    <td class="<%#(int)Eval("Status")==1?"":"fontred" %>"><%#Eval("Status") %></td>
                    <td><%#Eval("BeiZhu") %></td>
                    <td><%#GetDaiShouCaoZuoHtml(Eval("Status"))%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmptyDaiShou" Visible="false">
                <tr>
                    <td colspan="8">暂无代收登记信息</td>
                </tr>
                </asp:PlaceHolder>
            </table>            
        </div>  
        </asp:PlaceHolder>
       
        <div class="hr_10"></div>
        <div class="mainbox cunline">
            <ul id="ul_btn_list">
                <asp:PlaceHolder ID="pan_OperaterExamineV" runat="server" Visible="false">
                    <li class="cun-cy"><a href="javascript:void(0);" id="a_OperaterExamineV">提交计调审核</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_OperaterCheck" runat="server" Visible="false">
                    <li class="cun-cy"><a href="javascript:void(0);" id="operaterCheck">提交计调终审</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_SellsExamineV" runat="server" Visible="false">
                    <li class="cun-cy"><a id="a_SellsExamineV" href="javascript:void(0);">提交销售审核</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_ApplyOver" runat="server">
                    <li class="cun-cy"><a id="a_ApplyOver" href="javascript:void(0);">报销完成</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_InAccount" runat="server">
                    <li class="cun-cy"><a id="a_InAccount" href="javascript:void(0);">财务入账</a></li>
                </asp:PlaceHolder>
                <li class="quxiao-cy"><a href="javascript:void(0);" id="operaterReturn">返回</a></li>
                <li class="cun-cy"><a href="<%=PrintPageHSD %>" target="_blank">报账单</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var AccountedForPage = {
            _sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
            _tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
            _ajax: function(data) {
                var that = this;
                if (data && data["url"]) {
                    $.newAjax({
                        type: data["getType"] || "POST",
                        url: data["url"],
                        cache: false,
                        data: data["data"] || {},
                        dataType: "json",
                        success: function(ret) {
                            if (data["success"]) {
                                data["success"](ret)
                                return false;
                            }
                            else {
                                if (ret.result == "1") {
                                    tableToolbar._showMsg("提交成功!", function() {
                                        window.location = window.location;
                                    });

                                } else {
                                    tableToolbar._showMsg(ret.msg, function() {
                                        that.BindButtBtn();
                                    });
                                }
                            }

                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                                that.BindButtBtn();
                            });
                            return false;
                        }
                    });
                }
            },
            Save: function(obj, ID) {/*其他收入添加修改*/
                var data = {};
                var tr = obj.closest("tr");
                var crmID = tr.find("td").eq(1).find("input[type='hidden']").eq(0).val();
                if (crmID == "") {
                    tableToolbar._showMsg("请选择付款单位!");
                    tr.find("td").eq(1).find("input[type='text']").focus();
                    return false;
                }
                data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                    type: "saveFreeItem",
                    sl: AccountedForPage._sl,
                    source: '<%=Request.QueryString["source"] %>',
                    ID: ID,
                    tourId: AccountedForPage._tourId,
                    tourCode: $("#<%=lbl_TourCode.ClientID %>").html(),
                    sellerId: $("#<%=hideSaleId.ClientID %>").val(),
                    sellerName: $.trim($("#<%=lbl_SaleInfo.ClientID %>").html())
                });
                data["data"] = tr.find("input,textarea,select").serialize();

                if (ID != "") {
                    data["data"] += "&" + $.param({ crmName: tr.find("td:eq(1)").find("input[type='hidden'][name='hidcrmName']").val(), crmId: crmID, paymentText: tr.find("select[name='other_payment'] :selected").text() });
                }
                else {
                    data["data"] += "&" + $.param({ crmName: tr.find("td").eq(1).find("input[type='text']").val(), crmId: crmID, paymentText: tr.find("select[name='other_payment'] :selected").text() });
                }
                this._ajax(data);
            },
            DeleteFreeItem: function(obj, ID) {/*其他收入删除*/
                var data = {};
                data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                    type: "DeleteFreeItem",
                    sl: AccountedForPage._sl,
                    ID: ID,
                    tourId: AccountedForPage._tourId
                })
                data["data"] = obj.closest("tr").serialize();
                data["success"] = function(ret) {
                    if (ret.result == "1") {
                        tableToolbar._showMsg(ret.msg);
                        obj.parent().parent().css("display", "none");
                    } else {
                        tableToolbar._showMsg(ret.msg);
                    }
                }
                this._ajax(data);
            },
            SaveGuidMoneyIn: function(obj, Id, actionType) {/*导游收入添加修改*/
                var data = {};
                var tr = obj.closest("tr");
                if (tr.find("input[name='txtOrderCode']").val() == "") {
                    tr.find("input[name='txtOrderCode']").focus();
                    tableToolbar._showMsg("请选择订单");
                    return false;
                }
                data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                    type: "saveGuidMoneyIn",
                    sl: AccountedForPage._sl,
                    tourId: AccountedForPage._tourId,
                    OrderId: Id,
                    actionType: actionType
                });
                data["data"] = tr.find("input,textarea").serialize();
                this._ajax(data);
            },
            DeleteGuidMoneyIn: function(obj, Id) {/*导游收入删除*/
                var data = {};
                data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                    type: "delGuidMoneyIn",
                    sl: AccountedForPage._sl,
                    OrderId: Id,
                    tourId: AccountedForPage._tourId
                });
                data["data"] = obj.closest("tr").find("input,textarea").serialize();
                data["success"] = function(ret) {
                    if (ret.result == "1") {
                        tableToolbar._showMsg(ret.msg);
                        obj.parent().parent().css("display", "none");
                    } else {
                        tableToolbar._showMsg(ret.msg);
                    }
                }
                this._ajax(data);
            },
            UnBind: function() {
                var obj = $("#ul_btn_list a");
                obj.css({ "background-position": "0 -62px" });
                obj.unbind("click");
            },
            BindButtBtn: function() {/*绑定底部大按钮*/
                var that = this;
                var obj = $("#operaterCheck");
                obj.css({ "background-position": "0 0px" });
                //计调终审
                obj.unbind("click").click(function() {
                    that.UnBind();
                    var data = {};
                    data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                        type: "save",
                        sl: that._sl,
                        tourId: that._tourId
                    })
                    that._ajax(data);
                    return false;
                });
                obj = $("#a_OperaterExamineV");
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {
                    that.UnBind();
                    var data = {};
                    data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                        type: "OperaterExamineV",
                        sl: that._sl,
                        tourId: that._tourId
                    })
                    that._ajax(data);
                    return false;
                })
                //提交销售审核
                obj = $("#a_SellsExamineV");
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {
                    that.UnBind();
                    var data = {};
                    data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                        type: "SellsExamineV",
                        sl: that._sl,
                        tourId: that._tourId
                    })
                    that._ajax(data);
                    return false;
                })
                //报销完成
                obj = $("#a_ApplyOver");
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {
                    that.UnBind();
                    var data = {};
                    data["getType"] = "get";
                    data["url"] = '/CommonPage/SalesAccountedFor.aspx?' + $.param({
                        type: "ApplyOver",
                        sl: that._sl,
                        tourId: that._tourId
                    })
                    that._ajax(data);
                })
                obj = $("#operaterReturn");
                //返回
                var operaterReturnURLArr = [
                "/GuideCenter/GuidReimbursement.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.导游中心_导游报账%>",
                "/OperaterCenter/OperaReimbursement.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调报账%>",
                "/MarketCenter/XiaoShouBaoZhang.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2. 销售中心_销售报账%>",
                "/FinanceManage/Apply/List.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2. 财务管理_报销报账%>"];

                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {
                    var index = parseInt('<%=Utils.GetInt(Utils.GetQueryStringValue("source")) %>')
                    if (index) {
                        window.location.href = operaterReturnURLArr[index - 1];
                    }
                    return false;
                });
                obj = $("#a_InAccount");
                obj.css({ "background-position": "0 0px" });
            	obj.html("<%=this.GetFinIn() %>");
                obj.unbind("click").click(function() {
                    Boxy.iframeDialog({
                        iframeUrl: "/FinanceManage/Common/InAccount.aspx?" + $.param({
                            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                            KeyId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourID") %>',
                            DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.团未完导游先报账 %>',
                            tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourID") %>'
                        }),
                        title: "财务入账",
                        modal: true,
                        width: "900px",
                        height: "500px",
                        draggable: true
                    });
                })

            },
            _BindBtn: function() {/*绑定页面按钮*/
                var that = this;
                source = '<%=EyouSoft.Common.Utils.GetQueryStringValue("source") %>';
                if (source != '3') {
                    $("#tabOrderList").find("th[data-class='orderBZ'],td[data-class='orderBZ']").css("display", "none");
                }
                //团款确认单                
                $(".addContent-box").find("a[data-class='orderInfo']").unbind("click").click(function() {
                    var tourType = $(this).attr("data-tourType");
                    var orderId = $(this).attr("data-OrderId");
                    parent.Boxy.iframeDialog({
                        title: "团款确认单",
                        iframeUrl: "/CommonPage/tourMoneyStatements.aspx?" +
                            $.param({
                                tourType: tourType,
                                OrderId: orderId,
                                sl: AccountedForPage._sl,
                                action: 2,
                                tourId: AccountedForPage._tourId
                            }),
                        width: "715px",
                        height: "820px",
                        draggable: true
                    });
                    return false;
                });

                //订单报账
                $("#tabOrderList").find("a[data-class='orderBZ']").unbind("click").click(function() {
                    var tourType = $(this).attr("data-tourType");
                    var orderId = $(this).attr("data-OrderId");
                    var status = $(this).attr("data-tourstatus");
                    //                    if (parseInt(status) > 7) {
                    //                        tableToolbar._showMsg("已停止订单报账！");
                    //                        return false;
                    //                    }
                    parent.Boxy.iframeDialog({
                        title: "订单报账",
                        iframeUrl: "/CommonPage/OrderBaoZhang.aspx?" +
                            $.param({
                                tourType: tourType,
                                OrderId: orderId,
                                sl: AccountedForPage._sl
                            }),
                        width: "948px",
                        height: "600px",
                        draggable: true
                    });
                    return false;
                });
                //其它收入 修改
                $("#tabFreeItemList").find("[data-class='updateFreeItem']").unbind("click");
                $("#tabFreeItemList").find("[data-class='updateFreeItem']").click(function() {
                    var id = $(this).attr("data-ID");
                    if (id) {
                        AccountedForPage.Save($(this), id);
                    }
                    return false;
                });

                //其它收入 删除
                $("#tabFreeItemList").find("[data-class='deleteFreeItem']").unbind("click");
                $("#tabFreeItemList").find("[data-class='deleteFreeItem']").click(function() {
                    var id = $(this).attr("data-ID");
                    if (id) {
                        AccountedForPage.DeleteFreeItem($(this), id);
                    }
                    return false;
                });

                //其它收入 添加 
                $("#tabFreeItemList").find("[data-class='addOtherFreeItem']").unbind("click");
                $("#tabFreeItemList").find("[data-class='addOtherFreeItem']").click(function() {
                    AccountedForPage.Save($(this), "");
                    return false;
                });

                //导游收入  修改
                $("#tabGuidMoneyIn").find("[data-class='updateOrder']").unbind("click");
                $("#tabGuidMoneyIn").find("[data-class='updateOrder']").click(function() {
                    var id = $(this).attr("data-OrderID");
                    if (id) {
                        AccountedForPage.SaveGuidMoneyIn($(this), id, "update");
                    }
                    return false;
                });

                that.BindButtBtn();

            },
            CallBackFunOrder: function(obj) {/*订单号选用回调函数*/
                if (obj) {
                    $("input[type='text'][name='txtOrderCode']").val(obj.text);
                    $("input[type='hidden'][name='hidOrderId']").val(obj.value);
                    $("input[type='text'][name='txtbuyCompany']").val(obj.buyCompanyName);
                }
            },
            PageInit: function() {
                AccountedForPage._BindBtn();
                var hidOrderIdLength = $("#tabGuidMoneyIn").find("input[type='hidden'][name='OrderIdhid']").length;
                var hidOrderIdArr = "";
                if (hidOrderIdLength > 0) {
                    for (var i = 0; i < hidOrderIdLength; i++) {
                        hidOrderIdArr += $("#tabGuidMoneyIn").find("input[type='hidden'][name='OrderIdhid']").eq(i).val() + ",";
                    }
                }
                newToobar.init({
                    box: "#span",
                    className: "xuanyong",
                    callBackFun: "AccountedForPage.CallBackFunOrder",
                    para: { tourId: AccountedForPage._tourId, orderIdList: hidOrderIdArr }
                });
            },
            PayMentChange: function(obj) {
                var _self = $(obj);
                if ('<%=IsChangeDaoYou.ToString().ToLower() %>' == 'false') {
                    if (_self.val() == '<%=(int)EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付 %>') {
                        _self.closest("tr").find("a[class='addbtn'").css("display", "none");
                    } else {
                        _self.closest("tr").find("a[class='addbtn'").css("display", "block");
                    }
                }
            }
        }

        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            daiShouInsert: function() {
                var _data = { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', tourid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>' };
                Boxy.iframeDialog({ iframeUrl: "/financemanage/daishou/edit.aspx", title: "代收登记", modal: true, width: "800px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
            },
            daiShouUpdate: function(obj) {
                var _$obj = $(obj);
                var _$tr = _$obj.closest("tr");
                var _title = "代收登记-修改";
                if (_$obj.attr("i_chakan") == "1") _title = "代收登记-查看";
                var _data = { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', tourid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>', daishouid: _$tr.attr("i_daishouid") };
                Boxy.iframeDialog({ iframeUrl: "/financemanage/daishou/edit.aspx", title: _title, modal: true, width: "800px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
            },
            daiShouDelete: function(obj) {
                if (!confirm("代收登记信息删除后不可恢复，你确认要删除吗？")) return;

                var _$obj = $(obj);
                var _$tr = _$obj.closest("tr");
                var _data = { daishouid: _$tr.attr("i_daishouid") };

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=deletedaishou",
                    data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
            AccountedForPage.PageInit();
            $("#i_a_daishouinsert").click(function() { iPage.daiShouInsert(); });
            $(".i_daishouupdate").click(function() { iPage.daiShouUpdate(this); });
            $(".i_daishoudelete").click(function() { iPage.daiShouDelete(this); });
        });
    </script>

</asp:Content>
