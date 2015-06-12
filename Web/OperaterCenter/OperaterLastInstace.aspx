<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterLastInstace.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterLastInstace" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/selectGuid.ascx" TagName="seleGuid" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="head" ID="head1" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content1" runat="server">
    <div class="mainbox">
        <form id="formSearch" action="OperaterLastInstace.aspx" method="get">
        <input type="hidden" name="sl" id="sl" value="<%=Request.QueryString["sl"]%>" />
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" class="inputtext formsize100" name="txtTourCode" value="<%=Utils.GetQueryStringValue("txtTourCode") %>" />&nbsp&nbsp
                    线路名称：<input type="text" class="inputtext formsize120" name="txtRouteName" value="<%=Utils.GetQueryStringValue("txtRouteName") %>" />&nbsp&nbsp
                    出团日期：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" name="txtStartTime"
                        value="<%=Utils.GetQueryStringValue("txtStartTime") %>" />
                    至
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" name="txtEndTime"
                        value="<%=Utils.GetQueryStringValue("txtEndTime") %>" /><br />
                    <br />
                    导游：
                    <uc2:seleGuid ID="SelectedGuid" runat="server" />
                    &nbsp&nbsp 销售员：<Uc1:Seller ID="sellers" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>"
                        SetTitle="销售员" SelectFrist="false" />
                    &nbsp&nbsp 计调员：<Uc1:Seller ID="planers" CompanyID="<%=this.SiteUserInfo.CompanyId %>"
                        runat="server" SetTitle="计调员" SelectFrist="false" />
                    &nbsp&nbsp
                    <input type="hidden" value="<%=Utils.GetQueryStringValue("IsDealt") %>" name="IsDealt" />
                    <input type="submit" value="" class="search-btn" />
                </p>
            </span>
        </div>
        </form>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href='/OperaterCenter/OperaterLastInstace.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl")%>&IsDealt=0'
                    hidefocus="true" class='ztorderform <%=EyouSoft.Common.Utils.GetQueryStringValue("IsDealt")=="0" ||EyouSoft.Common.Utils.GetQueryStringValue("IsDealt")==""?"de-ztorderform":"" %>'>
                    <span>未终审</span></a> </li>
                <li><s class="orderformicon"></s><a href='/OperaterCenter/OperaterLastInstace.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl")%>&IsDealt=1'
                    hidefocus="true" class='ztorderform <%=EyouSoft.Common.Utils.GetQueryStringValue("IsDealt")=="1" ? "de-ztorderform":"" %>'>
                    <span>已终审</span></a> </li>
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
                        团号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        出团日期
                    </th>
                    <th align="center" class="th-line">
                        导游
                    </th>
                    <th align="center" class="th-line">
                        计调
                    </th>
                    <th align="center" class="th-line">
                        销售员
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
                <asp:Repeater ID="repOpInstaceList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("TourId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <%# (EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType), Eval("TourType").ToString()) == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务 ? "单项业务" : Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#GetGuidList(Eval("MGuidInfo")) %>
                            </td>
                            <td align="center">
                                <%# GetOperaList(Eval("MPlanerInfo"))%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="right" class="<%#GetShouRuYanSe(Eval("TourStatus")) %>">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ZongShouRu"),ProviderToMoney)%>
                            </td>
                            <td align="right" <%# (EyouSoft.Model.EnumType.TourStructure.TourStatus)Eval("TourStatus")== EyouSoft.Model.EnumType.TourStructure.TourStatus.封团? "":"class=fontred"  %>>
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TourPay"),ProviderToMoney)%>
                            </td>
                            <td align="right" class="fontblue">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"), ProviderToMoney)%>
                            </td>
                            <td align="center">
                            <%#GetCaoZuoHtml(Eval("TourId"),Eval("TourStatus"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
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
        $(function() {
            tableToolbar.init({
                tableContainerSelector: "#liststyle"
            });
        });
    </script>

</asp:Content>
