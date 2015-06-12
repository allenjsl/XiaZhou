<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="BudgetVSSettleList.aspx.cs" Inherits="Web.FinanceManage.Statistics.BudgetVSSettleList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="ProfitList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>利润统计</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:;" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>预算/结算对比表</span></a></li>
                <li><s class="orderformicon"></s><a href="DiaryList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>日记账</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="BudgetVSSettleList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：
                    <input type="text" name="txt_teamNumber" value="<%= Request.QueryString["txt_teamNumber"]%>"
                        class="inputtext formsize120" />
                    线路名称：
                    <input type="text" name="txt_lineName" value="<%=Request.QueryString["txt_lineName"] %>"
                        class="inputtext formsize120" />
                    客户单位：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" selectfrist="false" />
                    出团时间：
                    <input name="txt_SDate" value="<%=Request.QueryString["txt_SDate"] %>" onfocus="WdatePicker();"
                        type="text" class="inputtext formsize80" />
                    -
                    <input type="text" name="txt_EDate" onfocus="WdatePicker();" value="<%=Request.QueryString["txt_EDate"] %>"
                        class="inputtext formsize80" />
                    销售员：<uc1:sellsselect id="txt_Seller" runat="server" selectfrist="false" />
                    <br />
                    计调员：<uc1:sellsselect id="txt_Plan" runat="server" selectfrist="false" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a id="a_print" href="javascript:void(0);" hidefocus="true"
                    class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" id="ToXls" hidefocus="true"
                    class="toolbar_daochu"><span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" rowspan="2" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox1" />
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        团号
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        线路名称
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        客户单位
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        出团时间
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        销售员
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        计调员
                    </th>
                    <th colspan="3" align="center" class="th-line">
                        费用预算
                    </th>
                    <th colspan="3" align="center" class="th-line">
                        结算费用
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line nojiacu">
                        收入
                    </th>
                    <th align="center" class="th-line nojiacu">
                        支出
                    </th>
                    <th align="center" class="th-line nojiacu">
                        毛利
                    </th>
                    <th align="center" class="th-line nojiacu">
                        收入
                    </th>
                    <th align="center" class="th-line nojiacu">
                        支出
                    </th>
                    <th align="center" class="th-line nojiacu">
                        毛利
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);" data-class="a_bt">
                                    <%#Eval("Crm")%></a> <span style="display: none"><b>
                                        <%#Eval("Crm")%></b><br>
                                        联系人：<%#Eval("Contact")%><br>
                                        联系方式：<%#Eval("Phone")%></span>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Planer")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("BudgetIncome"),ProviderToMoney) %></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("BudgetOutgo"),ProviderToMoney) %></b>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("BudgetGProfit"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ClearingIncome"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ClearingOutgo"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ClearingGProfit"), ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="13">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server">
                    <tr>
                        <td colspan="7" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <asp:Label ID="lbl_budgetIncome" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_budgetOutgo" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Label ID="lbl_budgetProfit" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <asp:Label ID="lbl_clearingIncome" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_clearingOutgo" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Label ID="lbl_clearingProfit" runat="server" Text="0"></asp:Label></b>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            Bt: function() {/*泡泡提示*/
                $(".bt-wrapper").html("");
                $("a[data-class='a_bt']").bt({
                    contentSelector: function() {
                        return $(this).next("span").html();
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 200,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });
            },
            PageInit: function() {
                tableToolbar.init({});
                var that = this;
                that.Bt();
                $("#ToXls").click(function() {
                    toXls1();
                    return false;
                })
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                })
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            PageJsDataObj.PageInit();
        })
    </script>

</asp:Content>
