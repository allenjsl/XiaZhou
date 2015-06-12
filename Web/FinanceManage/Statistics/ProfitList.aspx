<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ProfitList.aspx.cs" Inherits="Web.FinanceManage.Statistics.ProfitList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Src="/UserControl/selectGuid.ascx" TagName="selectGuid" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="javascript:;" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>利润统计</span></a></li>
                <li><s class="orderformicon"></s><a href="BudgetVSSettleList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>预算/结算对比表</span></a></li>
                <li><s class="orderformicon"></s><a href="DiaryList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>日记账</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="ProfitList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号/订单号：
                    <input type="text" name="txt_teamNumber" value="<%= Request.QueryString["txt_teamNumber"]%>"
                        class="inputtext formsize120" />
                    线路名称：
                    <input type="text" name="txt_lineName" value="<%=Request.QueryString["txt_lineName"] %>"
                        class="inputtext formsize120" />
                    客户单位：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" selectfrist="false" />
                    出团时间：
                    <input name="txt_SDate" onfocus="WdatePicker();" value="<%=Request.QueryString["txt_SDate"] %>"
                        type="text" class="inputtext formsize80" />
                    -
                    <input type="text" name="txt_EDate" onfocus="WdatePicker();" value="<%=Request.QueryString["txt_EDate"] %>"
                        class="inputtext formsize80" />
                    <br />
                    销售员：<uc1:sellsselect id="txt_Seller" runat="server" selectfrist="false" />
                    计调员：<uc1:sellsselect id="txt_Plan" runat="server" selectfrist="false" />
                    导游：<uc2:selectGuid ID="txt_Guide" runat="server"></uc2:selectGuid>
                    核算日期：
                    <input name="txt_adjustSDate" type="text" onfocus="WdatePicker();" value="<%= Request.QueryString["txt_adjustSDate"]%>"
                        class="inputtext formsize80" />
                    -
                    <input type="text" name="txt_adjustEDate" onfocus="WdatePicker();" value="<%= Request.QueryString["txt_adjustEDate"]%>"
                        class="inputtext formsize80" />
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
                    <th width="30" class="thinputbg">
                        <input type="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号/订单号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="left" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        出团时间
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        计调员
                    </th>
                    <th align="center" class="th-line">
                        导游
                    </th>
                    <th width="126" align="right" class="th-line">
                        收入
                    </th>
                    <th width="126" align="right" class="th-line">
                        支出
                    </th>
                    <th width="111" align="right" class="th-line">
                        毛利
                    </th>
                    <th align="center" class="th-line">
                        核算日期
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("Code")%>
                            </td>
                            <td align="left">
                                <%#Eval("RouteName") %>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);" data-class="a_bt">
                                    <%#Eval("Crm")%></a> <span style="display: none"><b>
                                        <%#Eval("Crm")%></b><br>
                                        联系人：<%#Eval("ContactName")%><br>
                                        联系方式：<%#Eval("ContactTel")%></span>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("PeopleNum")%>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Planer")%>
                            </td>
                            <td align="center">
                                <%#Eval("Guide")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Income"),ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Outlay"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), ProviderToDate)%>
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
                        <td colspan="5" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="center">
                            <strong>
                                <asp:Label ID="lbl_peopleNum" runat="server" Text="0"></asp:Label></strong>
                        </td>
                        <td colspan="3" align="center">
                            &nbsp;
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <asp:Label ID="lbl_income" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_outlay" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Label ID="lbl_profit" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="center">
                            &nbsp;
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
