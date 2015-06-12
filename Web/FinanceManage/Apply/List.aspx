<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.FinanceManage.Apply.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/selectGuid.ascx" TagName="selectGuid" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="txt_teamNumber" value="<%= Request.QueryString["txt_teamNumber"]%>"
                        class="inputtext formsize80" />
                    线路名称：<input type="text" name="txt_lineName" value="<%=Request.QueryString["txt_lineName"] %>"
                        class="inputtext formsize120" />
                    出团时间：<input name="txt_SDate" type="text" onclick="WdatePicker()" value="<%=Request.QueryString["txt_SDate"] %>"
                        class="inputtext formsize80" />
                    -
                    <input type="text" name="txt_EDate" onclick="WdatePicker()" value="<%=Request.QueryString["txt_EDate"] %>"
                        class="inputtext formsize80" />
                    <br />
                    导游：<uc2:selectGuid ID="txt_Guide" selectfrist="false" runat="server"></uc2:selectGuid>
                    销售员：<uc1:SellsSelect ID="txt_Seller" selectfrist="false" runat="server" />
                    计调员：
                    <uc1:SellsSelect ID="txt_Plan" runat="server" selectfrist="false" SetTitle="计调员" />
                    <input type="submit" id="submit" class="search-btn" />
                </p>
            </span>
        </div>
        <input type="hidden" name="isDealt" id="isDealt" value="<%=Request.QueryString["isDealt"]%>" />
        <input type="hidden" name="sl" value="<%=SL %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="-1"
                    href="javascript:void(0);"><span>未报账团队</span></a></li><li class="line"></li>
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="1"
                    href="javascript:void(0);"><span>已报账团队</span></a></li><li class="line"></li>
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="-2"
                    href="javascript:void(0);"><span>未报销团队</span></a></li><li class="line"></li>
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="2"
                    href="javascript:void(0);"><span>已报销团队</span></a></li><li class="line"></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="center" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        出团时间
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        销售
                    </th>
                    <th align="center" class="th-line">
                        导游
                    </th>
                    <th align="center" class="th-line">
                        计调
                    </th>
                    <th align="right" class="th-line">
                        收入
                    </th>
                    <th align="right" class="th-line">
                        支出
                    </th>
                    <th align="right" class="th-line">
                        利润
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%#Eval("Adults")%></b><sup class="fontred">+<%#Eval("Childs")%></sup>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%#PingDYName((System.Collections.Generic.IList<EyouSoft.Model.TourStructure.MGuidInfo>)Eval("MGuidInfo"))%>
                            </td>
                            <td align="center">
                                <%#PingJDName((System.Collections.Generic.IList<EyouSoft.Model.TourStructure.MTourPlaner>)Eval("MPlanerInfo"))%>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ZongShouRu"), ProviderToMoney)%></b>
                            </td>
                            <td align="right" class="fontred">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TourPay"), ProviderToMoney)%></b>
                            </td>
                            <td align="right" class="fontred">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("JProfit"), ProviderToMoney)%></b>
                            </td>
                            <td align="center" data-tourid="<%#Eval("TourId") %>">
                                <%=CaoZuoString %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_msg" runat="server">
                    <tr align="center">
                        <td colspan="12">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("tablehead").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            SalesAccountedForURL: "/CommonPage/SalesAccountedFor.aspx?", /*报销页面URL*/
            FinalAppealURL: "/CommonPage/FinalAppeal.aspx?", /*审批页面URL*/
            BindBtn: function() {/*绑定按钮*/
                var that = this;
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "记录"
                });
                $("#liststyle a[data-class='a_ExamineA']").click(function() {
                    window.location.href = that.SalesAccountedForURL + $.param({
                        tourId: $(this).closest("td").attr("data-tourid"),
                        source: 4,
                        sl: '<%=SL %>'
                    })
                    return false;
                });
                $("#liststyle a[data-class='a_Apply']").click(function() {
                    window.location.href = that.FinalAppealURL + $.param({
                        tourId: $(this).closest("td").attr("data-tourid"),
                        source: 3,
                        sl: '<%=SL %>'
                    })
                    return false;
                });
                $("a[data-class='a_isDealt']").click(function() {
                    $("#isDealt").val($(this).attr("data-value"));
                    $("#submit").click();
                })
                $("a[data-class='a_isDealt'][data-value='" + $("#isDealt").val() + "']").addClass("ztorderform");
                var isdefaul = '<%=Request.QueryString["isDealt"]%>';
                isdefaul = isdefaul || "-1";
                $("a[data-class='a_isDealt']").each(function() {
                    if ($(this).attr("data-value") == isdefaul) {
                        $(this).addClass("ztorderform");
                    }
                    else {
                        $(this).removeClass("ztorderform");
                    }
                })
            },
            PageInit: function() {
                //绑定功能按钮
                this.BindBtn();
            }

        }
        $(function() {
            PageJsDataObj.PageInit();
        })




        
    </script>

</asp:Content>
