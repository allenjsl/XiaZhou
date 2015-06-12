<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterCheckend.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterCheckend" MasterPageFile="~/MasterPage/Front.Master" %>

<asp:Content ContentPlaceHolderID="head" ID="head1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content1" runat="server">
    <form id="Form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                <tr>
                    <td width="10%" class="addtableT">
                        线路名称：
                    </td>
                    <td width="30%" class="kuang2">
                        <asp:Literal ID="litRouteName" runat="server"></asp:Literal>
                    </td>
                    <td width="10%" class="addtableT">
                        出团时间：
                    </td>
                    <td width="20%" class="kuang2">
                        <asp:Literal ID="litStartTime" runat="server"></asp:Literal>
                    </td>
                    <td width="10%" class="addtableT">
                        团号：
                    </td>
                    <td width="20%" class="kuang2">
                        <asp:Literal ID="litTourCode" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        天数：
                    </td>
                    <td class="kuang2">
                        <asp:Literal ID="litDays" runat="server"></asp:Literal>
                    </td>
                    <td class="addtableT">
                        人数：
                    </td>
                    <td class="kuang2">
                        <b class="fontblue">
                            <asp:Literal ID="litAdults" runat="server"></asp:Literal></b><sup class="fontred">+<asp:Literal
                                ID="litChilds" runat="server"></asp:Literal></sup>
                    </td>
                    <td class="addtableT">
                        销售员：
                    </td>
                    <td class="kuang2">
                        <asp:Literal ID="litSellersName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        计调员：
                    </td>
                    <td class="kuang2">
                        <asp:Literal ID="litOperaterName" runat="server"></asp:Literal>
                    </td>
                    <td class="addtableT">
                        导游：
                    </td>
                    <td colspan="3" class="kuang2">
                        <asp:Literal ID="litGuidName" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="addContent-box">
            <span class="formtableT">团款收入</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tabOrderList">
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
                    <th>
                        结算金额
                    </th>
                    <th align="right">
                        导游收款
                    </th>
                    <th align="right">
                        财务收款
                    </th>
                    <th align="right">
                        订单利润
                    </th>
                </tr>
                <asp:Repeater ID="repTourIncomList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%# Eval("OrderCode")%>
                            </td>
                            <td align="left">
                                <%# Eval("BuyCompanyName")%>
                            </td>
                            <td align="center">
                                <%# Eval("Operator")%>
                            </td>
                            <td align="center"><%# Eval("SellerName")%></td>
                            <td align="right" class="fonthei">
                                 <%#GetShouRuHeTongJinELinkHtml(Eval("OrderId"), Eval("ConfirmMoney"), Eval("ConfirmMoneyStatus"))%>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmSettlementMoney"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b>
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GuideRealIncome"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b><%# EyouSoft.Common.UtilsCommons.GetMoneyString(EyouSoft.Common.Utils.GetDecimal(Eval("ConfirmMoney").ToString()) - EyouSoft.Common.Utils.GetDecimal(Eval("GuideRealIncome").ToString()), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred"><%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"), ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="4" align="right">
                        <strong>合计：</strong>
                    </td>
                    <td align="right">
                        <b class="fontblue">
                            <asp:Literal ID="litConfirmMoneyCount" runat="server"></asp:Literal></b>
                    </td>
                    <td align="right">
                        <b class="fontgreen">
                            <asp:Literal ID="litConfirmSettlementMoneyCount" runat="server"></asp:Literal></b>
                    </td>
                    <td align="right">
                        <b>
                            <asp:Literal ID="litSalerIncomeCount" runat="server"></asp:Literal></b>
                    </td>
                    <td align="right">
                        <b>
                            <asp:Literal ID="litCheckMoneyCount" runat="server"></asp:Literal></b>
                    </td>
                    <td align="right">
                        <b class="fontred">
                            <asp:Literal ID="litProfitCount" runat="server"></asp:Literal></b>
                    </td>
                </tr>
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
                <asp:Repeater ID="repOtherIncomList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("FeeItem")%>
                            </td>
                            <td align="left">
                                <%# Eval("Crm")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"),ProviderToMoney)%></b>
                            </td>
                            <td align="left">
                                <%# Eval("Remark")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="hr_10">
            </div>
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">团队支出</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th>
                        类别
                    </th>
                    <th align="left">
                        供应商
                    </th>
                    <th align="left">
                        明细
                    </th>
                    <th align="center">
                        支付方式
                    </th>
                    <th align="center">
                        数量
                    </th>
                    <th align="right">
                        结算金额
                    </th>
                </tr>
                <asp:Repeater ID="RepTourSeationExpList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td width="16%" align="center">
                                <%# Eval("Type").ToString()%>
                            </td>
                            <td width="25%" align="left">
                                <%# Eval("SourceName")%>
                            </td>
                            <td width="31%" align="left">
                                <%# Eval("CostDetail")%>
                            </td>
                            <td width="11%" align="center">
                                <%# Eval("PaymentType").ToString() %>
                            </td>
                            <td width="7%" align="center">
                                <%#GetZhiChuShuLiang(Eval("Type"), Eval("Num"),Eval("DNum"))%>
                            </td>
                            <td width="10%" align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="5" align="right">
                        合计：
                    </td>
                    <td align="right">
                        <b class="fontred">
                            <asp:Literal ID="litConfirmaCount" runat="server"></asp:Literal></b>
                    </td>
                </tr>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">报账汇总</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
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
                            <asp:Literal ID="litGuideIncomeCount" runat="server"></asp:Literal></b>
                    </td>
                    <td width="13%" align="right">
                        <b class="fontred">
                            <asp:Literal ID="litGuideBorrowCount" runat="server"></asp:Literal></b>
                    </td>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Literal ID="litGuideOutlayCount" runat="server"></asp:Literal></b>
                    </td>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Literal ID="litGuideMoneyRtnCount" runat="server"></asp:Literal></b>
                    </td>
                    <td width="15%" align="center">
                        <asp:Literal ID="litGuideRelSignCount" runat="server"></asp:Literal>
                    </td>
                    <td width="15%" align="center">
                        <asp:Literal ID="litGuideUsedCount" runat="server"></asp:Literal>
                    </td>
                    <td width="15%" align="center">
                        <asp:Literal ID="litGuideSignRtnCount" runat="server"></asp:Literal>
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
                            <asp:Literal ID="litTourIncomCount" runat="server"></asp:Literal>
                        </b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Literal ID="litTourExpenceCount" runat="server"></asp:Literal>
                        </b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Literal ID="litProfit1Count" runat="server"></asp:Literal>
                        </b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Literal ID="litProfitLCount" runat="server"></asp:Literal>%</b>
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
                                    <a href='javascript:void(0)' class='i_daishouchakan' >查看</a>
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
        <div class="mainbox cunline fixed">
            <ul id="ul_action_list">
                <asp:PlaceHolder ID="panView" runat="server">
                    <asp:PlaceHolder ID="panViewReturnOp" runat="server">
                        <li class="cun-cy"><a href="javascript:" data-class="returnOperater">退回计调</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="panViewSubmitFin" runat="server">
                        <li class="cun-cy"><a href="javascript:" data-class="ConfirmAccount">提交财务</a></li></asp:PlaceHolder>
                </asp:PlaceHolder>
                <li class="cun-cy"><a href="<%=PrintPageHSD %>" target="_blank">报账单</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var OperaterCheckPage = {
            _tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
            _sl: '<%=SL%>',
            _Save: function(actionType) {
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterCheckend.aspx?type=' + actionType + '&sl=' + OperaterCheckPage._sl + "&tourId=" + OperaterCheckPage._tourId,
                    cache: false,
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            tableToolbar._showMsg(data.msg, function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg(data.msg, function() {
                                if (actionType == "saveReturn") {
                                    $("#ul_action_list").find("a[data-class='returnOperater']").text("退回计调");
                                    $("#ul_action_list").find("a[data-class='returnOperater']").bind("click");
                                    $("#ul_action_list").find("a[data-class='returnOperater']").css("background-position", "0 0px");
                                    $("#ul_action_list").find("a[data-class='returnOperater']").click(function() {
                                        $(this).text("退回计调中...");
                                        $(this).unbind("click");
                                        $(this).css("background-position", "0 -62px");
                                        OperaterCheckPage._Save(actionType);
                                    });
                                } else {
                                    $("#ul_action_list").find("a[data-class='ConfirmAccount']").text("提交财务");
                                    $("#ul_action_list").find("a[data-class='ConfirmAccount']").bind("click");
                                    $("#ul_action_list").find("a[data-class='ConfirmAccount']").css("background-position", "0 0px");
                                    $("#ul_action_list").find("a[data-class='ConfirmAccount']").click(function() {
                                        $(this).text("提交财务中...");
                                        $(this).css("background-position", "0 -62px");
                                        $(this).unbind("click");
                                        OperaterCheckPage._Save(actionType);
                                    });
                                }
                                return false;
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
            },
            _BindBtn: function() {
                //结算单
                $(".i_hetongquerenjine").click(function() {
                    var _$obj = $(this);
                    var _data = { tourType: _$obj.attr("i_tourtype"), tourId: _$obj.attr("i_tourid"), OrderId: _$obj.attr("i_orderid"), sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' };

                    parent.Boxy.iframeDialog({ title: "团款确认单", iframeUrl: "/CommonPage/tourMoneyStatements.aspx", data: _data, width: "715px", height: "820px", draggable: true });
                    return false;
                });

                //订单报账 
                $("#tabOrderList").find("a[data-class='orderProfit']").unbind("click");
                $("#tabOrderList").find("a[data-class='orderProfit']").click(function() {
                    var orderId = $(this).attr("data-orderid");
                    var tourId = $(this).attr("data-tourid");
                    parent.Boxy.iframeDialog({
                        title: "订单报账信息",
                        iframeUrl: "/CommonPage/OrderBaoZhang.aspx?" +
                            $.param({
                                OrderId: orderId,
                                sl: OperaterCheckPage._sl,
                                tourId: tourId
                            }),
                        width: "970px",
                        height: "600px",
                        draggable: true
                    });
                    return false;
                });

                //退回计调 
                $("#ul_action_list").find("a[data-class='returnOperater']").unbind("click");
                $("#ul_action_list").find("a[data-class='returnOperater']").click(function() {
                    $(this).text("退回计调中...");
                    $(this).unbind("click");
                    $(this).css("background-position", "0 -62px");
                    OperaterCheckPage._Save("saveReturn", OperaterCheckPage._sl, OperaterCheckPage._tourId);
                    return false;
                });

                //提交财务
                $("#ul_action_list").find("a[data-class='ConfirmAccount']").unbind("click");
                $("#ul_action_list").find("a[data-class='ConfirmAccount']").click(function() {
                    $(this).text("提交财务中...");
                    $(this).unbind("click");
                    $(this).css("background-position", "0 -62px");
                    OperaterCheckPage._Save("saveConfirm", OperaterCheckPage._sl, OperaterCheckPage._tourId);
                    return false;
                });

            },
            _PageInit: function() {
                OperaterCheckPage._BindBtn();
            }
        }

        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            daiShouChaKan: function(obj) {
                var _$obj = $(obj);
                var _$tr = _$obj.closest("tr");
                var _title = "代收登记-查看";
                var _data = { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', tourid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>', daishouid: _$tr.attr("i_daishouid") };
                Boxy.iframeDialog({ iframeUrl: "/financemanage/daishou/edit.aspx", title: _title, modal: true, width: "800px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
            }
        };

        $(document).ready(function() {
            OperaterCheckPage._PageInit();
            $(".i_daishouchakan").click(function() { iPage.daiShouChaKan(this); });
        })
        
    </script>

</asp:Content>
