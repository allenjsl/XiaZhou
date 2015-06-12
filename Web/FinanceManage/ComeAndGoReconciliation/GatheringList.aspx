<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="GatheringList.aspx.cs" Inherits="Web.FinanceManage.ComeAndGoReconciliation.GatheringList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc1" %>
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
                <li><s class="orderformicon"></s><a href="javascript:void(0);" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>今日收款</span></a></li>
                <li><s class="orderformicon"></s><a href="ReturnMoneyList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>今日付款</span></a></li>
                <li><s class="orderformicon"></s><a href="ShouldGathering.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>今日应收</span></a></li>
                <li><s class="orderformicon"></s><a href="ShouldReturnMoney.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>今日应付</span></a></li>
            </ul>
        </div>
        <form id="SelectFrom" action="GatheringList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    审核时间：
                    <input value="<%=Request.QueryString["SDate"] %>" name="SDate" type="text" onfocus="WdatePicker();"
                        class="inputtext formsize80" />
                    -
                    <input type="text" value="<%=Request.QueryString["EDate"] %>" name="EDate" onfocus="WdatePicker();"
                        class="inputtext formsize80" />
                    收款金额：
                    <uc1:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
                    客户单位：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" SelectFrist="false" />
                    <br />
                    销售员：<uc1:SellsSelect ID="txt_Seller" runat="server" SelectFrist="false" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <!--列表表格-->
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a id="a_print" href="javascript:void();" hidefocus="true" class="toolbar_dayin"><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" hidefocus="true" id="ToXls"
                    class="toolbar_daochu"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        订单号
                    </th>
                    <th align="left" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="right" class="th-line">
                        收款金额
                    </th>
                    <th align="center" class="th-line">
                        财务人
                    </th>
                    <th align="center" class="th-line">
                        审核时间
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox1" />
                            </td>
                            <td align="center">
                                <%#Eval("OrderCode")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);" data-class="a_bt">
                                    <%#Eval("Crm")%></a> <span style="display: none"><b>
                                        <%#Eval("Crm")%></b><br>
                                        联系人：<%#Eval("Contact")%><br>
                                        联系方式：<%#Eval("Phone")%></span>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="left">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="right">
                                <strong class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Amount"), ProviderToMoney)%></strong>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), ProviderToDate)%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="8">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server" Visible="false">
                    <tr>
                        <td colspan="5" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <strong class="fontred">
                                <asp:Label ID="lbl_sum" runat="server" Text="0"></asp:Label></strong>
                        </td>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="tablehead" style="border-top: 0 none;">
           <script type="text/javascript">
               document.write(document.getElementById("tablehead").innerHTML);
           </script>
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
                var paidwuc = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientUniqueID %>']);
                paidwuc.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator)%>')
                paidwuc.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber)%>')
            }
        }
        $(function() {
            PageJsDataObj.PageInit();
        })
    </script>

</asp:Content>
