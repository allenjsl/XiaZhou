<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaReimbursement.aspx.cs"
    Inherits="Web.OperaterCenter.OperaReimbursement" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register TagName="sellers" TagPrefix="uc1" Src="~/UserControl/SellsSelect.ascx" %>
<%@ Register TagName="guid" TagPrefix="uc2" Src="~/UserControl/selectGuid.ascx" %>
<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div class="mainbox">
        <form id="formSearch" action="/OperaterCenter/OperaReimbursement.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=SL%>" />
                    团号：<input type="text" class="inputtext formsize120" name="txtTourCode" value="<%=Request.QueryString["txtTourCode"] %>" />
                    线路名称：<input type="text" class="inputtext formsize140" name="txtRouteName" value="<%=Request.QueryString["txtRouteName"] %>" />
                    出团日期：<input type="text" class="inputtext formsize100" name="txtStatTime" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["txtStatTime"] %>" />
                    至
                    <input type="text" class="inputtext formsize100" name="txtTimeEnd" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["txtTimeEnd"] %>" />
                    导游：<uc2:guid ID="guid1" runat="server" />
                    销售员：<uc1:sellers ID="sellers1" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>"
                        SelectFrist="false" />
                    <br />
                    团队状态：<select name="tourState" class="inputselect"><%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus),new string[]{"0","1","2","3","12","13","14","15"}),EyouSoft.Common.Utils.GetQueryStringValue("tourState"),"","请选择") %></select>
                    计调员：<uc1:sellers ID="txtJiDiaoYuan" runat="server" SelectFrist="false" />
                    
                    <button type="submit" class="search-btn" id="submit">
                        搜索</button>
                </p>
            </span>
        </div>
        <input type="hidden" name="isDealt" id="isDealt" value="<%=Request.QueryString["isDealt"]??"-1" %>" />
        </form>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="-1"
                    href="javascript:void(0);"><span>未报账团队</span></a></li><li class="line"></li>
                <li><s class="xiaoshou-bz"></s><a href="javascript:void(0);" hidefocus="true" data-class="a_isDealt"
                    data-value="1"><span>已报账团队</span></a> </li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="i_a_toxls" hidefocus="true" href="###"><span>导出列表</span></a></li>
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
                        人数
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
                        毛利
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="replist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("TourId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <a href='<%=printUrl %>?tourId=<%# Eval("TourId") %>' target="_blank">
                                    <%#Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%# Eval("Adults")%></b><sup class="fontred">+<%# Eval("Childs")%></sup>
                            </td>
                            <td align="center">
                                <%#GetGuidInfoHtml(Eval("MGuidInfo"))%>
                            </td>
                            <td align="center">
                                <%# GetOperaterList(Eval("MPlanerInfo"))%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="right" class="<%#GetShouRuYanSe(Eval("TourStatus")) %>">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ZongShouRu"), ProviderToMoney)%>
                            </td>
                            <td align="right" <%# (EyouSoft.Model.EnumType.TourStructure.TourStatus)Eval("TourStatus")== EyouSoft.Model.EnumType.TourStructure.TourStatus.封团? "":"class=fontred"  %>>
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TourPay"),ProviderToMoney)%>
                            </td>
                            <td align="right" class="fontblue">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"),ProviderToMoney)%>
                            </td>
                            <td align="center">
                                <%#Eval("TourStatus").ToString()%>
                            </td>
                            <td align="center">
                            <%if (EyouSoft.Common.Utils.GetQueryStringValue("isDealt") == "1")
                              { %>
                              <a href="/CommonPage/SalesAccountedFor.aspx?source=2&sl=<%=Request.QueryString["sl"]%>&tourId=<%# Eval("TourId") %>&tourType=<%#(int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType), Eval("TourType").ToString())%>" class="check-btn" title="查看"></a>
                                    <%}
                              else
                              { %>
                              <a href="/CommonPage/SalesAccountedFor.aspx?source=2&sl=<%=Request.QueryString["sl"]%>&tourId=<%# Eval("TourId") %>&tourType=<%#(int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType), Eval("TourType").ToString())%>">
                                    <img alt="" src="/images/baozhang-cy.gif" />计调报账</a>
                              <%} %>
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lab_text" runat="server"></asp:Label>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="-1"
                    href="javascript:void(0);"><span>未报账团队</span></a></li><li class="line"></li>
                <li><s class="xiaoshou-bz"></s><a hidefocus="true" data-class="a_isDealt" href="javascript:void(0);"
                    data-value="1"><span>已报账团队</span></a> </li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="i_a_toxls" hidefocus="true" href="###">
                    <span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({
                tableContainerSelector: "#liststyle"
            });
            $("a[data-class='a_isDealt']").click(function() {
                $("#isDealt").val($(this).attr("data-value"));
                $("#submit").click();
            });
            $("a[data-class='a_isDealt'][data-value='" + $("#isDealt").val() + "']").addClass("ztorderform");
            var isdefaul = '<%=Request.QueryString["isDealt"]??"-1" %>';
            $("a[data-class='a_isDealt']").each(function() {
                if ($(this).attr("data-value") == isdefaul) {
                    $(this).addClass("ztorderform");
                }
                else {
                    $(this).removeClass("ztorderform");
                }
            });

            toXls.init({ "selector": ".i_a_toxls" });
        });
    </script>

</asp:Content>
