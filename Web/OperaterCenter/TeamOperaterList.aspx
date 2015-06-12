<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamOperaterList.aspx.cs"
    Inherits="Web.OperaterCenter.TourOperaterList" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="Customer" TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div class="mainbox">
        <form id="searchform" action="/OperaterCenter/TeamOperaterList.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=SL %>" />
                    团 &nbsp;&nbsp;&nbsp;号：
                    <input type="text" class="inputtext formsize120" name="txtTourCode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />
                    出团日期：
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtStartTime"
                        name="txtStartTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtStartTime") %>" />
                    -
                    <input type="text" class="inputtext" style="width: 63px; padding-left: 2px;" id="txtEndTime"
                        name="txtEndTime" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndTime") %>" />
                    团队状态：<%=TourStatusHtml.ToString()%>
                    <br />
                    <br />
                    客源单位：
                    <uc2:Customer ID="Customer1" runat="server" BoxyTitle="客源单位" SelectFrist="false" />
                    销售员：
                    <Uc1:Seller ID="Seller1" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>" SelectFrist="false" />
                    计调员：
                    <Uc1:Seller ID="seller2" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>" SelectFrist="false"  SetTitle="计调员"/>
                    <input type="submit" id="search" class="search-btn" value="" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
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
                            天数
                        </th>
                        <th align="center" class="th-line">
                            客户单位
                        </th>
                        <th align="center" class="th-line">
                            销售员
                        </th>
                        <th align="center" class="th-line">
                            计调员
                        </th>
                        <th align="center" class="th-line">
                            人数
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
                    <asp:Repeater ID="TeamOperaterList" runat="server">
                        <ItemTemplate>
                            <tr <%# Container.ItemIndex%2!=0? "class=\"odd\"":"" %>>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("TourId") %>" />
                                </td>
                                <td align="center">
                                    <a href='<%# teamPrintUrl %>?tourid=<%# Eval("TourId") %>' target="_blank" style="text-decoration:none" data-class="TravelagencyShow">
                                        <%# Eval("TourCode")%>
                                    </a>
                                    <%#GetTourPlanIschange(Convert.ToBoolean(Eval("IsChange")), Convert.ToBoolean(Eval("IsSure")),Eval("TourId").ToString())%>
                                    <div style="display: none">
                                        <%# GetOperaterInfo(Eval("TourId").ToString())%>
                                    </div>
                                </td>
                                <td align="left">
                                    <a href='<%# teamPrintUrl %>?tourid=<%# Eval("TourId") %>' target="_blank">
                                        <%# Eval("RouteName") %>
                                    </a>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.UtilsCommons.GetDateString(Convert.ToDateTime(Eval("LDate")), ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%# Eval("TourDays")%>
                                </td>
                                <td align="center">
                                    <asp:PlaceHolder runat="server" Visible='<%# GetTourType((EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType),Eval("TourType").ToString())) %>'>
                                        <a href="javascript:void(0);" data-class="customershow">
                                            <%#GetCustomerInfo(Eval("CompanyInfo"), "single")%>
                                        </a>
                                        <div style="display: none">
                                            <%# GetCustomerInfo(Eval("CompanyInfo"), "info")%>
                                        </div>
                                    </asp:PlaceHolder>
                                </td>
                                <td align="center">
                                    <%# GetSellerInfo(Eval("SaleInfo"))%>
                                </td>
                                <td align="center">
                                    <%# GetOperaList(Eval("TourPlaner"))%>
                                </td>
                                <td align="center">
                                    <b class="fontblue">
                                        <%# Eval("Adults")%></b><sup>+<b class="fontred"><%# Eval("Childs")%></b></sup>
                                </td>
                                <td align="center">
                                    <a href='/SellCenter/CustomerPlan/OrderPaid.aspx?tourID=<%# Eval("TourId") %>&sl=<%#EyouSoft.Common.Utils.GetQueryStringValue("sl")  %>&plan=1'>共<b class="fontred"><%# Eval("OrderNum")%></b>单</a>
                                </td>
                                <td align="center" data-class="GetJiDiaoIcon" data-tourid="<%# Eval("TourId") %>">
                                    <%# EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.TourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                                </td>
                                <td align="center" <%# (EyouSoft.Model.EnumType.TourStructure.TourStatus)Eval("TourStatus") == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 ? "class='fontgray'" : ""%>>
                                    <%# Eval("TourStatus").ToString()%>
                                </td>
                                <td align="center">
                                    <%# GetOperate((EyouSoft.Model.EnumType.TourStructure.TourStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus), Eval("TourStatus").ToString()), Eval("TourId").ToString(), (EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType),Eval("TourType").ToString()), (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourQuoteType), Eval("OutQuoteType").ToString()))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="tablehead">
            <div class="pages">
                <asp:Label ID="lab_Text" runat="server"></asp:Label>
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var TeamOperaterPage = {
            //计调任务接受
            _OperaterReceive: function(comID, tourId) {
                $.newAjax({
                    type: "POST",
                    url: '/Ashx/ReceiveJob.ashx?sl=<%=Utils.GetQueryStringValue("sl") %>&type=receive&com=' + comID + "&Operator=<%=this.SiteUserInfo.Username %>" + "&OperatorID=<%=this.SiteUserInfo.UserId %>" + "&OperatDepID=<%=this.SiteUserInfo.DeptId  %>&tourId=" + tourId,
                    async: false,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result) {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            //click事件
            _BindBtn: function() {
                $("#liststyle").find("[data-class='receiveOp']").unbind("click").click(function() {
                    var ac = $(this).attr("data-teamPlaner");
                    if (ac.toUpperCase() == "FALSE") {
                        tableToolbar._showMsg("不是该计划的计调员,没有接受任务的权限!");
                    } else {
                        var companyID = "<%=this.SiteUserInfo.CompanyId %>";
                        var tourId = $(this).attr("data-tourid");
                        TeamOperaterPage._OperaterReceive(companyID, tourId);
                    }
                    return false;
                });

                //发布人泡泡提示
                $("#liststyle").find("[data-class='TravelagencyShow']").bt({
                    contentSelector: function() {
                        return $(this).siblings("div").html();
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

                //客户单位泡泡
                $("#liststyle").find("[data-class='customershow']").bt({
                    contentSelector: function() {
                        return $(this).siblings("div").html();
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

                //供应商泡泡
                BtFun.InitBindBt("GetJiDiaoIcon");
            },
            _DataInit: function() {
                TeamOperaterPage._BindBtn();
            }
        };


        $(document).ready(function() {
            //初始化
            TeamOperaterPage._DataInit();
            tableToolbar.init({
                tableContainerSelector: "#liststyle"
            });
        });
        
    </script>

</asp:Content>
