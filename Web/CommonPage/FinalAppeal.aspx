<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="FinalAppeal.aspx.cs" Inherits="Web.CommonPage.FinalAppeal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                <tr>
                    <td width="10%" class="addtableT">
                        线路名称：
                    </td>
                    <td width="30%" class="kuang2">
                        <asp:Label ID="lbl_routeName" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="10%" class="addtableT">
                        出团时间：
                    </td>
                    <td width="20%" class="kuang2">
                        <asp:Label ID="lbl_lDate" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="10%" class="addtableT">
                        团号：
                    </td>
                    <td width="20%" class="kuang2">
                        <asp:Label ID="lbl_tourCode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        天数：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_tourDays" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="addtableT">
                        人数：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_number" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="addtableT">
                        销售员：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_saleInfoName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        计调员：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_tourPlaner" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="addtableT">
                        导游：
                    </td>
                    <td colspan="3" class="kuang2">
                        <asp:Label ID="lbl_mGuidInfoName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--团队收入-->
        <div class="addContent-box">
            <span class="formtableT">团款收入</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="16%">
                        订单号
                    </th>
                    <th width="25%" align="left">
                        客源单位
                    </th>
                    <th>
                        下单人
                    </th>
                    <th>销售员</th>
                    <th align="right">
                        合同金额
                    </th>
                    <th align="right">
                        结算金额
                    </th>
                    <th align="right" title="导游实收金额">
                        导游实收
                    </th>
                    <th align="right" title="合同金额-导游实收金额">
                        财务应收
                    </th>
                    <th align="right">
                        订单利润
                    </th>
                    <th align="right" title="已登记审核金额(含报销完成后导游实收金额)">
                        已收金额
                    </th>
                    <th align="right" title="已收待审：已登记收款未审核金额(含报销未完成导游实收金额)">
                        待审金额
                    </th>
                    <th align="right" title="合同金额-已收金额">
                        未收金额
                    </th>                    
                </tr>
                <asp:Repeater ID="rpt_tourMoneyIn" runat="server">
                    <ItemTemplate>
                        <tr i_orderid="<%#Eval("OrderId") %>">
                            <td align="center">
                                <a href="<%#PringPageJSD %>?OrderId=<%#Eval("OrderId") %>&tourType=<%#(int)Eval("TourType") %>&ykxc=1"
                                    target="_blank"><%#Eval("OrderCode")%></a>
                            </td>
                            <td align="left">
                                <%#Eval("BuyCompanyName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                            </td>
                            <td align="center"><%#Eval("SellerName")%></td>
                            <td align="right">
                                <b class="<%#(bool)Eval("ConfirmMoneyStatus")?"":"fontred" %>">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmSettlementMoney"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GuideRealIncome"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b><%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("ConfirmMoney") - (decimal)Eval("GuideRealIncome"), ProviderToMoney)%></b>
                            </td> 
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"), ProviderToMoney)%></b>
                            </td>                           
                            <td align="right">
                                 <b><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("CheckMoney"), ProviderToMoney)%></b>
                            </td>   
                            <td align="right">
                                 <b><%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("ReceivedMoney") - (decimal)Eval("CheckMoney"), ProviderToMoney)%></b>
                            </td>         
                            <th align="right">
                                <b><a href="javascript:void(0)" class="i_shoukuan_win"><%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("ConfirmMoney") - (decimal)Eval("CheckMoney"), ProviderToMoney)%></a></b>
                            </th>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_tourMoneyInMsg" runat="server">
                    <tr>
                        <td align="center" colspan="12">
                            暂无团款收入信息
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_tourMoneyInSum" runat="server">
                    <tr>
                        <td colspan="4" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b><asp:Label ID="lbl_sumPrice" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b><asp:Label ID="lbl_confirmSettlementMoney" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b><asp:Label ID="lbl_guideRealIncome" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b><asp:Label ID="lbl_checkMoney" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontred"><asp:Label ID="lbl_profit" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b><asp:Literal runat="server" ID="ltrYiShouJinEHeJi"></asp:Literal></b>
                        </td>
                        <td align="right">
                            <b><asp:Literal runat="server" ID="ltrDaiShenJinE"></asp:Literal></b>
                        </td>
                        <td align="right">
                            <b><asp:Literal runat="server" ID="ltrWeiShouJinE"></asp:Literal></b>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">其它收入</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="16%">
                        收入类型
                    </th>
                    <th width="25%" align="left">
                        付款单位
                    </th>
                    <th width="8%" align="right">
                        金额
                    </th>
                    <th width="51%">
                        备注
                    </th>
                </tr>
                <asp:Repeater ID="rpt_restsMoneyIn" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("FeeItem")%>
                            </td>
                            <td align="left">
                                <%#Eval("Crm")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="left">
                                <%#Eval("Remark")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Label ID="lbl_restsMoneyInMsg" runat="server" Visible="false" Text="暂无其他收入信息"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">团队支出</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="16%">
                        类别
                    </th>
                    <th align="left" width="19%">
                        供应商
                    </th>
                    <th align="left" width="23%">
                        明细
                    </th>
                    <th align="center" width="11%">
                        支付方式
                    </th>
                    <th align="center" width="7%">
                        数量
                    </th>
                    <th align="right" width="8%">
                        结算金额
                    </th>
                    <th align="right" width="8%">
                        已付金额
                    </th>
                    <th align="right" width="8%">
                        未付金额
                    </th>
                </tr>
                <asp:Repeater ID="rpt_tourMoneyOut" runat="server">
                    <ItemTemplate>
                        <tr i_planid="<%#Eval("PlanId") %>">
                            <td align="center">
                                <%#Eval("Type")%>
                            </td>
                            <td  align="left">
                                <%#Eval("SourceName")%>
                            </td>
                            <td  align="left">
                                <%#Eval("CostDetail")%>
                            </td>
                            <td  align="center">
                                <%#Eval("PaymentType")%>
                            </td>
                            <td  align="center" data-class="td_Num">
                                <%#GetZhiChuShuLiang(Eval("Type"), Eval("Num"),Eval("DNum"))%>
                            </td>
                            <td  align="right" data-confirmation="<%#Eval("Confirmation") %>">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"), ProviderToMoney)%></b>
                            </td>
                            <td align="right" data-confirmation="<%#Eval("Confirmation") %>">                                
                                <b><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Prepaid"), ProviderToMoney)%></b>
                            </td>
                            <td align="right" data-confirmation="<%#Eval("Confirmation") %>">                                
                                <b><a href="javascript:void(0)" class="i_fukuan_win"><%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("Confirmation") - (decimal)Eval("Prepaid"), ProviderToMoney)%></a></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_tourMoneyOutMsg" runat="server">
                    <tr>
                        <td align="center" colspan="8">
                            暂无团队支出信息
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_tourMoneyOut" runat="server">
                    <tr>
                        <td colspan="5" align="right">
                            合计：
                        </td>
                        <td align="right">
                            <b class="fontred" data-calss="b_Confirmation"><asp:Label ID="lbl_tourMoneyOutSumConfirmation" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b><asp:Literal runat="server" ID="ltrYiFuJinE"></asp:Literal></b>
                        </td>                        
                        <td align="right">
                            <b><asp:Literal runat="server" ID="ltrWeiFuJinE"></asp:Literal></b>
                        </td>       
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <asp:Panel ID="pan_moneyAllot" runat="server">
            <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">利润分配</span><span>
                    <asp:PlaceHolder ID="pan_AddMongyAllot" runat="server"><a href="javascript:void(0);"
                        id="a_AddMongyAllot">
                        <img src="/images/addimg.gif" /></a></asp:PlaceHolder>
                </span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                    <tr>
                        <th height="31">
                            订单号/团号
                        </th>
                        <th align="center">
                            人员
                        </th>
                        <th align="right">
                            分配金额
                        </th>
                        <th align="right">
                            毛利
                        </th>
                        <th align="right">
                            净利
                        </th>
                        <th align="left">
                            备注
                        </th>
                        <th width="115" align="center">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_mongyAllot" runat="server">
                        <ItemTemplate>
                            <tr data-id="<%#Eval("Id") %>">
                                <td width="16%" align="center">
                                    <%#Eval("TourCode")%>/<%#Eval("OrderCode")%>
                                </td>
                                <td width="8%" align="center">
                                    <%#Eval("Staff")%>
                                </td>
                                <td width="8%" align="right">
                                    <b class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Amount"), ProviderToMoney)%></b>
                                </td>
                                <td width="9%" align="right">
                                    <b class="fontgreen">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Gross"), ProviderToMoney)%></b>
                                </td>
                                <td width="8%" align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(((decimal)Eval("Gross"))- ((decimal)Eval("Amount")), ProviderToMoney)%></b>
                                </td>
                                <td align="left">
                                    <%#Eval("Remark")%>
                                </td>
                                <td align="center">
                                    <a data-class="a_UpdateMongyAllot" data-editbutton="edit" href="javascript:void(0);">
                                        <img src="/images/y-delupdateicon.gif" border="0" />
                                        修改 </a><a data-class="a_DelMongyAllot" data-editbutton="edit" href="javascript:void(0);">
                                            <img src="/images/y-delicon.gif" />
                                            删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_mongyAllotMsg" runat="server">
                        <tr>
                            <td align="center" colspan="7">
                                暂无利润分配信息
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </div>
        </asp:Panel>
        <div class="hr_10">
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
            <div class="hr_5">
            </div>
            <div class="tablelist-box" style="width: 98.5%">
                <span class="formtableT">供应商代收</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                    <tr>
                        <th style="text-align: left;">
                            订单号
                        </th>
                        <th style="text-align: left;">
                            客户单位
                        </th>
                        <th style="text-align: left;">
                            供应商
                        </th>
                        <th style="text-align: left;">
                            代收时间
                        </th>
                        <th style="text-align: right;">
                            代收金额&nbsp;
                        </th>
                        <th style="text-align: left;">
                            状态
                        </th>
                        <th style="text-align: left;">
                            代收备注
                        </th>
                        <th style="text-align: left;">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptDaiShou">
                        <ItemTemplate>
                            <tr i_daishouid="<%#Eval("DaiShouId") %>">
                                <td>
                                    <%#Eval("OrderCode") %>
                                </td>
                                <td>
                                    <%#Eval("CrmName") %>
                                </td>
                                <td>
                                    <%#Eval("GysName") %>
                                </td>
                                <td>
                                    <%#Eval("Time","{0:yyyy-MM-dd}") %>
                                </td>
                                <td style="text-align: right;" class="<%#(int)Eval("Status")==1?"":"fontred" %>">
                                    <%#Eval("JinE","{0:C2}") %>&nbsp;
                                </td>
                                <td class="<%#(int)Eval("Status")==1?"":"fontred" %>">
                                    <%#Eval("Status") %>
                                </td>
                                <td>
                                    <%#Eval("BeiZhu") %>
                                </td>
                                <td>
                                    <%#GetDaiShouCaoZuoHtml(Eval("Status")) %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phEmptyDaiShou" Visible="false">
                        <tr>
                            <td colspan="8">
                                暂无代收登记信息
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
            </div>
        </asp:PlaceHolder>
        
        <div class="hr_5">
        </div>
        <div id="div_btn" class="mainbox cunline fixed" style="width: 450px;">
            <ul>
                <asp:PlaceHolder ID="pan_returnOperater" runat="server">
                    <li id="li_returnOperater" class="cun-cy"><a href="javascript:void(0);">退回计调</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_returnCourtFinal" runat="server" >
                    <li id="li_returnCourtFinal" class="cun-cy"><a href="javascript:void(0);">退回终审</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_submitFinance" runat="server" >
                    <li id="li_submitFinance" class="cun-cy"><a href="javascript:void(0);">提交财务</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_sealTour" runat="server" >
                    <li id="li_sealTour" class="cun-cy">
                        <input type="hidden" runat="server" id="TourSettlement" name="TourSettlement" />
                        <input type="hidden" runat="server" id="TourPay" name="TourPay" />
                        <input type="hidden" runat="server" id="TourProfit" name="TourProfit" />
                        <input type="hidden" runat="server" id="DisOrderProfit" name="DisOrderProfit" />
                        <input type="hidden" runat="server" id="DisTourProfit" name="DisTourProfit" />
                        <input type="hidden" runat="server" id="TourIncome" name="TourIncome" />
                        <input type="hidden" runat="server" id="TourOtherIncome" name="TourOtherIncome" />
                        <a href="javascript:void(0);">核算结束</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_InAccount" runat="server">
                    <li class="cun-cy"><a id="a_InAccount" href="javascript:void(0);"><%#GetFinIn()%></a></li>
                </asp:PlaceHolder>
                <li class="quxiao-cy"><a id="a_return" href="javascript:void(0);">返回</a></li>
                <li class="cun-cy"><a href="<%=PrintPageHSD %>" target="_blank">报账单</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var CommPage = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/FinanceManage/Common/AddDistributeProfit.aspx?",
                    title: "添加利润分配",
                    width: "530px",
                    height: "240px"
                }
            },
            UnBind: function() {
                var obj = $("#div_btn li.btn a");
                obj.css({ "background-position": "0 -62px" });
                obj.unbind("click");
            },
            Add: function() {/*添加 利润分配*/
                var data = this.DataBoxy();
                data.url += $.param({
                    tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                    Price: '<%=Price %>',
                    TourCode: '<%=TourCode %>',
                    sl: '<%=SL %>'
                });
                this.ShowBoxy(data);
            },
            Updata: function(obj) {/*修改 利润分配*/
                var data = this.DataBoxy();
                data.url += $.param({
                    tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                    Price: '<%=Price %>',
                    id: $(obj).closest("tr").attr("data-id"),
                    TourCode: '<%=TourCode %>',
                    sl: '<%=SL %>'
                });
                data.title = "修改利润分配";
                this.ShowBoxy(data);
            },
            BindButtBtn: function() {/*页面底部按钮*/
                var that = this;
                var obj = $("#li_returnOperater")
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {/*退回计调*/
                    that.UnBind();
                    var data = {
                        sl: '<%=SL %>',
                        tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                        type: "ReturnOperater"
                    }
                    that.GoAjax(data);
                })
                obj = $("#li_returnCourtFinal")
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {/*退回终审*/
                    that.UnBind();
                    var data = {
                        sl: '<%=SL %>',
                        tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                        type: "ReturnLastInstace"
                    }

                    that.GoAjax(data);
                })
                obj = $("#li_submitFinance")
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {/*提交财务*/
                    that.UnBind();
                    var data = {
                        sl: '<%=SL %>',
                        tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                        type: "SubmitFinanceManage"
                    }
                    that.GoAjax(data);
                })
                obj = $("#li_sealTour")
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {/*核算结束*/
                    that.UnBind();
                    var data = {
                        sl: '<%=SL %>',
                        tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                        type: "AppealEnd"
                    }
                    that.GoAjax($.param(data) + "&" + $(this).closest("li").find(":hidden").serialize());
                })
                var returnURLArr = [
                "/OperaterCenter/OperaterLastInstace.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调终审%>",
                "/FinanceManage/OneTeamAdjustAccounts/List.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_单团核算%>",
                "/FinanceManage/Apply/List.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_报销报账%>"];
                obj = $("#a_return")
                obj.unbind("click").click(function() {
                    var index = parseInt('<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("source")) %>')
                    if (index) {
                        window.location.href = returnURLArr[index - 1];
                    }
                    return false;
                })
                obj = $("#a_InAccount");
                obj.css({ "background-position": "0 0px" });
            	obj.html("<%=this.GetFinIn() %>");
                obj.unbind("click").click(function() {
                    Boxy.iframeDialog({
                        iframeUrl: "/FinanceManage/Common/InAccount.aspx?" + $.param({
                            sl: '<%=SL %>',
                            KeyId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourID") %>',
                            DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.单团核算 %>',
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
            BindListBtn: function() {/*绑定利润分配列表上的按钮*/
                //已核算
                if ('<%=flag %>' == "1") {
                    //控制利润分配编辑按钮(已核算的时候不显示修改和删除按钮)
                    $("a[data-editbutton='edit']").hide();
                }
                var that = this;
                $("#a_AddMongyAllot").click(function() {
                    that.Add();
                    return false;
                })
                $("#<%=pan_moneyAllot.ClientID %> a[data-class='a_UpdateMongyAllot']").click(function() {
                    that.Updata(this);
                    return false;
                })
                $("#<%=pan_moneyAllot.ClientID %> a[data-class='a_DelMongyAllot']").click(function() {
                    var obj = this;
                    tableToolbar.ShowConfirmMsg("确定删除利润分配？", function() {
                        var data = {
                            type: "Del",
                            sl: '<%=SL %>',
                            id: $(obj).closest("tr").attr("data-id")
                        };
                        CommPage.GoAjax(data);
                    })
                    return false;
                })

            },
            GoAjax: function(data) {/*通用Ajax请求*/
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/CommonPage/FinalAppeal.aspx",
                    data: data,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg("提交成功!", function() {
                                window.location = window.location;
                            });

                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg("服务器忙!");
                    }
                });
            },
            PageInit: function() {
                this.BindListBtn();
                this.BindButtBtn();
            }
        }

        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            daiShouChaKan: function(obj) {
                var _$obj = $(obj);
                var _$tr = _$obj.closest("tr");
                var _data = { sl: '<%=SL %>', tourid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>', daishouid: _$tr.attr("i_daishouid"), isshenpi: "0" };

                var _title = "代收登记-查看";
                if (_$obj.attr("i_shenpi") == "1") {
                    _data.isshenpi = "1";
                    _title = "代收登记-审批";
                }

                Boxy.iframeDialog({ iframeUrl: "/financemanage/daishou/edit.aspx", title: _title, modal: true, width: "800px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
            },
            openShouKuanWin: function(obj) {
                var _title = "财务管理-收款";
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "47", OrderId: _$tr.attr("i_orderid"), TourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>', ReturnOrSet: "1", DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.订单收款%>',ParentType:"1" };

                Boxy.iframeDialog({ iframeUrl: "/FinanceManage/Common/SetMoney.aspx", title: _title, modal: true, width: "800px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
            },
            openFuKuanWin: function(obj) {
                var _title = "财务管理-付款";
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "49", PlanId: _$tr.attr("i_planid"), TourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>'};

                Boxy.iframeDialog({ iframeUrl: "/FinanceManage/Payable/Register.aspx", title: _title, modal: true, width: "800px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
            }
        };

        $(function() {
            CommPage.PageInit();
            $(".i_daishouchakan").click(function() { iPage.daiShouChaKan(this); });
            $(".i_shoukuan_win").click(function() { iPage.openShouKuanWin(this); });
            $(".i_fukuan_win").click(function() { iPage.openFuKuanWin(this); });
        })
    </script>

</asp:Content>
