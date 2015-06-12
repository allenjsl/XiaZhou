<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgencyOperaterList.aspx.cs"
    Inherits="Web.OperaterCenter.AgencyOperaterList" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="Customer" TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
    <div class="mainbox">
        <form id="searchform" action="/OperaterCenter/AgencyOperaterList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=Request.QueryString["sl"]%>" />
                    团号：
                    <input type="text" class="formsize120" name="txtTourCode" value="<%=Request.QueryString["txtTourCode"] %>" />
                    客户单位：
                    <uc2:Customer ID="Customer1" runat="server" />
                    接团日期：
                    <input type="text" style="width: 63px; padding-left: 2px;" id="TxtmeetTeamTime" name="TxtmeetTeamTime"
                        onfocus="WdatePicker();" value="<%=Request.QueryString["TxtmeetTeamTime"] %>" />
                    销售员：
                    <Uc1:Seller ID="Seller1" runat="server" />
                    <input type="submit" id="search" class="search-btn" value="" /></p>
            </span>
        </div>
        </form>
        <!--列表表格-->
        <div class="tablehead">
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
                        团号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        出团时间
                    </th>
                    <th align="center" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        天数
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        计调员
                    </th>
                    <th align="center" class="th-line">
                        价格
                    </th>
                    <th align="center" class="th-line">
                        查看订单
                    </th>
                    <th align="center" class="th-line">
                        查看计调
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="AgOperaterList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox1" value="" />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);"  class="TravelagencyShow">
                                    <%# Eval("TourCode")%></a>
                                <div class="showTravelagency" style="display: none">
                                    <%# GetOperaterInfo(Eval("TourId").ToString())%>
                                </div>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);">
                                    <%# Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%# Eval("LDate","{0:yyyy-MM-DD}")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" id="customershow">
                                    <%#GetCustomerInfo(Eval("CompanyInfo"), "single")%></a>
                                <div class="showcustomer" style="display: none">
                                    <%# GetCustomerInfo(Eval("CompanyInfo"), "info")%>
                                </div>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%# Eval("Adults")%></b>+<b class="fontgreen"><%# Eval("Childs")%></b>+<b class="fontred"><%# Eval("Others")%></b>
                            </td>
                            <td align="center">
                                <%# Eval("TourDays")%>
                            </td>
                            <td align="center">
                                <%# GetSellerInfo(Eval("SaleInfo"))%>
                            </td>
                            <td align="center">
                                <%# GetOperaList(Eval("TourPlaner"))%>
                            </td>
                            <td align="center" class="blue">
                                <%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("AdultPrice")))%>
                            </td>
                            <td align="center">
                                <a href="javascript:" class="showOrder" tourId="<%# Eval("TourId") %>">查看订单</a>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.TourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                            </td>
                            <td align="center">
                                <%# Eval("TourStatus")%>
                            </td>
                            <td align="center">
                                <%# GetOperate(Eval("TourStatus").ToString(),Eval("TourId").ToString())%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="tablehead">
            <div class="pages">
                <asp:Label ID="lab_text" runat="server"></asp:Label>
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>

        <script type="text/javascript">
            $(function() {
                BindBtn();
                //当列表页面出现横向滚动条时使用以下方法
                $('.tablelist-box').moveScroll();
                $("a.TravelagencyShow").bt({
                    contentSelector: function() {
                        return $(this).next(".showTravelagency").html();
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
                    cssStyles: { color: '#00387E', 'line-height': '200%' }
                });
                $("#customershow").bt({
                    contentSelector: function() {
                        return $(this).next(".showcustomer").html();
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
                    cssStyles: { color: '#00387E', 'line-height': '200%' }
                });
                $("img[name='Agencydijie']").bt({
                    contentSelector: function() {
                        return $(this).next(".AgencyShow").html();
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
                    cssStyles: { color: '#00387E', 'line-height': '200%' }
                });

                $("a.showOrder").click(function() {
                     var Id = $(this).attr("tourId");
                     parent.Boxy.iframeDialog({ title: "查看订单", iframeUrl: "/SellCenter/Order/OrderInfo.aspx?tourId=" + Id +"&sl=<%=Request.QueryString["sl"] %>", width: "990px", height: "380px", draggable: true });
                    return false;
                });
                
                 $("a.receiveOp").click(function() {
                    var id = $(this).attr("tourid");
                    $.ajax({
                        type: "POST",
                        url: '/Ashx/ReceiveJob.ashx?sl=<%=Request.QueryString["sl"] %>&type=receive&Id='+id,
                        async: false,
                        dataType: "html",
                        success: function(data) {
                            tableToolbar._showMsg(data);
                            window.location.href = window.location.href;
                        },
                        error: function() {
                            tableToolbar._showMsg("接收失败!");
                        }
                    });
                    return false;
                });
            })

            function BindBtn() {
                tableToolbar.init({
                    tableContainerSelector: "#liststyle",
                    objectName: "aa"
                });
            }  
        </script>

    </div>
</asp:Content>
