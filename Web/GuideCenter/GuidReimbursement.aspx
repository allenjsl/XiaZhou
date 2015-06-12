<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuidReimbursement.aspx.cs"
    Inherits="Web.GuideCenter.GuidReimbursement" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register TagName="sellers" TagPrefix="uc1" Src="~/UserControl/SellsSelect.ascx" %>
<%@ Register TagName="guid" TagPrefix="uc2" Src="~/UserControl/selectGuid.ascx" %>
<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div class="mainbox">
        <form id="formSearch" method="get" action="/GuideCenter/GuidReimbursement.aspx">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=Request.QueryString["sl"]%>" />
                    团号：<input type="text" class="inputtext" size="formsize80" name="txtTourCode" value="<%=Request.QueryString["txtTourCode"] %>" />
                    线路名称：<input type="text" class="inputtext" size="formsize80" name="txtRouteName" value="<%=Request.QueryString["txtRouteName"] %>" />
                    出团日期：
                    <input type="text" class="inputtext" style="width: 60px;" name="txtStarTime" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["txtStarTime"] %>" />
                    至
                    <input type="text" class="inputtext" style="width: 60px;" name="txtStarEnd" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["txtStarEnd"] %>" />
                    团队状态：<%=TourStatusHtml.ToString()%>
                    导游：
                    <uc2:guid ID="guid1" runat="server" />
                    销售员：<uc1:sellers ID="sellers1" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>" SelectFrist="false" />
                    <br />计调员：<uc1:sellers ID="txtJiDiaoYuan" runat="server"  SelectFrist="false" />
                    <button type="submit" class="search-btn" id="submit">
                        搜索</button></p>
            </span>
        </div>
        <input type="hidden" name="isDealt" id="isDealt" value="<%=Request.QueryString["isDealt"]??"-1" %>" />
        </form>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="-1"
                    href="javascript:void(0);"><span>未报账团队</span></a></li><li class="line"></li>
            </ul>
            <ul class="fixed">
                <li><s class="xiaoshou-bz"></s><a href="javascript:void(0);" hidefocus="true" data-class="a_isDealt"
                    data-value="1"><span>已报账团队</span></a> </li>
                <li class="line"></li>
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
                                <a href='<%=PrintUrl %>?tourId=<%# Eval("TourId") %>' target="_blank">
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
                                <%#GetOperaterList(Eval("MPlanerInfo"))%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%#Eval("TourStatus").ToString()%>
                            </td>
                            <td align="center">
                            <%if (EyouSoft.Common.Utils.GetQueryStringValue("isDealt") == "1")
                              { %>
                              <a href="/CommonPage/SalesAccountedFor.aspx?source=1&sl=<%=Request.QueryString["sl"]%>&tourId=<%# Eval("TourId") %>&tourType=<%#EyouSoft.Common.Utils.GetInt(Eval("TourType").ToString()) %>"class="check-btn" title="查看"></a>
                                    <%}
                              else
                              { %>
                              <a href="/CommonPage/SalesAccountedFor.aspx?source=1&sl=<%=Request.QueryString["sl"]%>&tourId=<%# Eval("TourId") %>&tourType=<%#EyouSoft.Common.Utils.GetInt(Eval("TourType").ToString()) %>">
                                    <img alt="" src="/images/baozhang-cy.gif" />导游报账</a>
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
            </ul>
            <ul class="fixed">
                <li><s class="xiaoshou-bz"></s><a href="javascript:void(0);" hidefocus="true" data-class="a_isDealt"
                    data-value="1"><span>已报账团队</span></a> </li>
                <li class="line"></li>
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
            })
            var isdefaul='<%=Request.QueryString["isDealt"]??"-1" %>';
            $("a[data-class='a_isDealt']").each(function(){
                if($(this).attr("data-value")==isdefaul){
                    $(this).addClass("ztorderform");
                }
                else{
                    $(this).removeClass("ztorderform");
                }
            })
        });
    </script>

</asp:Content>
