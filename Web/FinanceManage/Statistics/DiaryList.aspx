<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="DiaryList.aspx.cs" Inherits="Web.FinanceManage.Statistics.DiaryList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="ProfitList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>利润统计</span></a></li>
                <li><s class="orderformicon"></s><a href="BudgetVSSettleList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform "><span>预算/结算对比表</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:;" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>日记账</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="DiaryList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    时间：
                    <input name="LDateS" type="text" class="inputtext formsize80" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["LDateS"] %>" />
                    -
                    <input name="LDateE" type="text" class="inputtext formsize80" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["LDateE"] %>" />
                    摘要：
                    <input name="Summary" type="text" class="inputtext formsize120" value="<%=Request.QueryString["Summary"] %>" />
                    支付方式：
                    <select name="PaymentId" id="sel_PaymentId" class="inputselect">
                        <option value="-1">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.支出)%>
                        <%=EyouSoft.Common.UtilsCommons.GetStrPaymentList(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType.收入)%>
                    </select>
                    金额：<uc2:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
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
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        时间
                    </th>
                    <th rowspan="2" align="left" class="th-line">
                        摘要
                    </th>
                    <th colspan="2" align="center" class="th-line">
                        借方
                    </th>
                    <th colspan="2" align="center" class="th-line">
                        贷方
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line nojiacu">
                        现金
                    </th>
                    <th align="center" class="th-line nojiacu">
                        银行存款
                    </th>
                    <th align="center" class="th-line nojiacu">
                        现金
                    </th>
                    <th align="center" class="th-line nojiacu">
                        银行存款
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="left">
                                <%#Eval("Summary")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DebitCash"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DebitBank"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("LenderCash"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("LenderBank"), ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="7">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server">
                    <tr>
                        <td colspan="3" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <asp:Label ID="lbl_debitCash" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b>
                                <asp:Label ID="lbl_debitBank" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_lenderCash" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b>
                                <asp:Label ID="lbl_lenderBank" runat="server" Text="0"></asp:Label></b>
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
                that.Bt()
                $("#ToXls").click(function() {
                    toXls1();
                    return false;
                })
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                })
                var caiWuShaiXuan = wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientID%>']);
                caiWuShaiXuan.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator) %>');
                caiWuShaiXuan.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber) %>');
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("PaymentId")%>'.length > 0) {
                    $("#sel_PaymentId").val('<%=EyouSoft.Common.Utils.GetQueryStringValue("PaymentId")%>')
                }
            }
        }
        $(function() {
            PageJsDataObj.PageInit();
        })
    </script>

</asp:Content>
