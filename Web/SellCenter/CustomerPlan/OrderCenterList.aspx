<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="OrderCenterList.aspx.cs" Inherits="Web.SellCenter.OrderCenterList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register src="../../UserControl/SellsSelect.ascx" tagname="SellsSelect" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="form1" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    订单号：<input type="text" class="formsize120" id="txtOrderCode" name="txtOrderCode"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderCode")%>" />
                    线路名称：<input type="text" class="formsize180" id="txtRouteName" name="txtRouteName"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName")%>" />
                    下单时间：<input type="text" onfocus="WdatePicker()" style="width: 65px; padding-left: 2px;"
                        id="txtOrderIssueBeginTime" name="txtOrderIssueBeginTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderIssueBeginTime")%>" />
                    至
                    <input type="text" onfocus="WdatePicker()" style="width: 65px; padding-left: 2px;"
                        id="txtOrderIssueEndTime" name="txtOrderIssueEndTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderIssueEndTime")%>" />
                    出团时间：<input type="text" onfocus="WdatePicker()" style="width: 65px; padding-left: 2px;"
                        id="txtLeaveBeginTime" name="txtLeaveBeginTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLeaveBeginTime")%>" />
                    至
                    <input type="text" onfocus="WdatePicker()" style="width: 65px; padding-left: 2px;"
                        id="txtLeaveEndTime" name="txtLeaveEndTime" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLeaveEndTime")%>" />
                    <br />
                    团 号：<input type="text" style="width: 132px;" id="txtTourCode" name="txtTourCode"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode")%>" />
                    销售员：<uc1:SellsSelect ID="SellsSelect1" runat="server"  SetTitle="销售员"/>
                    <button type="submit" class="search-btn">
                        搜索</button></p>
                <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            </span>
        </div>
        </form>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="OrderCenterList.aspx?OrderTypeBySearch=<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch.全部订单 %>&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目 %>"
                    hidefocus="true" class="ztorderform de-ztorderform"><span>全部订单</span></a></li>
                <li><s class="orderformicon"></s><a href="OrderCenterList.aspx?OrderTypeBySearch=<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch.我销售的订单 %>&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目 %>"
                    hidefocus="true" class="ztorderform"><span>我销售的订单</span></a></li>
                <li><s class="orderformicon"></s><a href="OrderCenterList.aspx?OrderTypeBySearch=<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch.我操作的订单 %>&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_栏目 %>"
                    hidefocus="true" class="ztorderform"><span>我操作的订单</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="Table1">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th width="101" align="center" class="th-line">
                        团号
                    </th>
                    <th width="177" align="left" class="th-line">
                        线路名称
                    </th>
                    <th width="101" align="center" class="th-line">
                        订单号
                    </th>
                    <th width="101" align="center" class="th-line">
                        下单人
                    </th>
                    <th width="101" align="center" class="th-line">
                        客源单位
                    </th>
                    <th width="100" align="center" class="th-line">
                        下单时间
                    </th>
                    <th width="120" align="center" class="th-line">
                        销售价
                    </th>
                    <th width="120" align="center" class="th-line">
                        结算价
                    </th>
                    <th width="59" align="center" class="th-line">
                        人数
                    </th>
                    <th width="68" align="center" class="th-line">
                        合计金额
                    </th>
                    <th width="60" align="center" class="th-line">
                        查看
                    </th>
                    <th width="86" align="center" class="th-line">
                        状态
                    </th>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_本部浏览) && CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_部门浏览))
                          {%>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("OrderId") %>" />
                            </td>
                            <td align="center" class="th-line">
                                <%#Eval("TourCode")%><span style="display: <%#Eval("IsTourChange").ToString()=="True"?"":"none;"%>"
                                    class="<%#(Eval("IsTourChange").ToString()=="True"&&Eval("ChangeState").ToString()=="True")?"fontgreen":"fontred"%>">(变)</span>
                            </td>
                            <td align="left">
                                <a href="#">
                                    <%#Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <a href="../打印/散客行程确认单.html" target="_blank">
                                    <%#Eval("OrderCode")%></a>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                            </td>
                            <td align="center">
                                <%#Eval("BuyCompanyName")%>
                            </td>
                            <td align="center" background="2011-11-15">
                                <%#EyouSoft.Common.Utils.GetDateTime(Eval("IssueTime") == null ? "" : Eval("IssueTime").ToString()).ToShortDateString()%>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#Eval("AdultPrice")%></b>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%#Eval("PeerAdultPrice")%></b>
                            </td>
                            <td align="center">
                                <b><a href="../打印/名单.html" target="_blank">
                                    <%#Eval("Adults")%></a><sup class="fontred">+<%#Eval("Childs")%></sup></b>
                            </td>
                            <td align="center">
                                <b class="fontbsize12">
                                    <%#Eval("SumPrice")%></b>
                            </td>
                            <td align="center">
                                <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_订单中心_内部浏览))
                                  { %>
                                <a href="OrderInfo.aspx" class="check-btn" title="查看"></a>
                                <%} %>
                            </td>
                            <td align="center" class="fontblue">
                                <%# EyouSoft.Common.UtilsCommons.GetOrderStateForHtml(Eval("OrderStatus").ToString())%>
                            </td>
                        </tr>
                        <%} %>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="width: 100%; text-align: center; background-color: #ffffff">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>
            <div style="border-top: 0 none;" class="tablehead">
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
