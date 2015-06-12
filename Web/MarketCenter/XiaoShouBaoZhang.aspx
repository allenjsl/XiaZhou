<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoShouBaoZhang.aspx.cs" Inherits="Web.MarketCenter.XiaoShouBaoZhang" MasterPageFile="~/MasterPage/Front.Master" %>
<%--销售中心-销售报账--%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/selectGuid.ascx" TagName="GuidsSelect" TagPrefix="uc2" %>
<asp:Content ID="PageHeader" ContentPlaceHolderID="head" runat="server">
    <script src="/js/datepicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
<asp:content ID="MainBodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form action="XiaoShouBaoZhang.aspx" method="get">
            <input type="hidden" name="sl" id="sl" value="<%=Utils.GetQueryStringValue("sl") %>" />
            <input type="hidden" name="sltStatus" id="sltStatus" value="<%=(EyouSoft.Common.Utils.GetQueryStringValue("sltStatus")!="1"?"0":"1") %>" />
            <span class="searchT">
                <p>
                    团号：<input type="text" class="inputtext formsize100" name="txtTourCode"  value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>"/>
                    线路名称：<input type="text" class="inputtext formsize100" name="txtRouteName" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>"/>
                    出团日期：<input type="text" class="inputtext formsize80" name="txtLSDate" onfocus="WdatePicker()"  value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLSDate") %>"/>
                     - <input type="text" class="inputtext formsize80" name="txtLEDate" onfocus="WdatePicker()"  value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLEDate") %>"/>
                    导游：<uc2:GuidsSelect id="txtDaoYou" runat="server" />
                    销售员：<uc1:sellsselect id="txtXiaoShouYuan" runat="server" SelectFrist="false" CompanyID="<%=this.SiteUserInfo.CompanyId %>" /><br /><br />
                    团队状态：
                        <select name="sltTourStatus" class="inputselect">
                            <asp:Literal ID="litTourStatus" runat="server"></asp:Literal>
                        </select>
                    发布人:<uc1:SellsSelect ID="txtFaBuRen" runat="server" SetTitle="发布人" SelectFrist="false" />
                    计调员：<uc1:SellsSelect ID="txtJiDiaoYuan" runat="server" SelectFrist="false" />
                    <button type="submit" class="search-btn">搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed" id="btnAction">
        	  <li><s class="xiaoshou-bz"></s><a href="XiaoShouBaoZhang.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&sltStatus=0" hidefocus="true" class="<%=(EyouSoft.Common.Utils.GetQueryStringValue("sltStatus")!="1"?"ztorderform":"") %>"><span>未报账团队</span></a></li><li class="line"></li>
        	  <li><s class="xiaoshou-bz"></s><a href="XiaoShouBaoZhang.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&sltStatus=1" hidefocus="true" class="<%=(EyouSoft.Common.Utils.GetQueryStringValue("sltStatus")=="1"?"ztorderform":"") %>"><span>已报账团队</span></a></li><li class="line"></li>
			</ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <%--<th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>--%>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="center" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        出团日期
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        发布人
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
                        收入
                    </th>
                    <th align="center" class="th-line">
                        支出
                    </th>
                    <th align="center" class="th-line">
                        毛利
                    </th>
                    <th align="center" class="th-line">
                        团队状态
                    </th>
                    <th align="center" class="th-line">
                        销售报账
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr>
                            <%--<td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox"  value="<%# Eval("TourId") %>"/>
                            </td>--%>
                            <td align="center">
                                <%# Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <a href='<%#PrintPages %>?tourid=<%#Eval("TourId") %>' target="_blank">
                                <%#Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <b class="fontblue"><%# Eval("Adults")%></b><sup class="fontred">+<%# Eval("Childs")%></sup>
                            </td>
                            <td style="text-align:center;">
                                <%#Eval("FaBuRenName") %>
                            </td>
                            <td align="center">
                                <%#GetGuidInfo(Eval("MGuidInfo"))%>
                            </td>
                            <td align="center">
                                <%# GetPlanInfo(Eval("MPlanerInfo"))%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="right" class="<%#GetShouRuYanSe(Eval("TourStatus")) %>">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ZongShouRu"), ProviderToMoney)%>
                            </td>
                            <td align="right" <%# (EyouSoft.Model.EnumType.TourStructure.TourStatus)Eval("TourStatus")== EyouSoft.Model.EnumType.TourStructure.TourStatus.封团? "":"class=fontred"  %> >
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TourPay"),ProviderToMoney)%>
                            </td>
                            <td align="right" class="fontblue">
                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"),ProviderToMoney)%> 
                            </td>
                            <td align="center">
                                <%#Eval("TourStatus").ToString()%>
                            </td>
                            <td align="center">
                            <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售报账_销售报账操作))
                              {
                                  if (Utils.GetQueryStringValue("sltStatus") == "1")
                                  {%>
                                  <span class="th-line"> <a href="/CommonPage/SalesAccountedFor.aspx?source=3&sl=<%=Request.QueryString["sl"]%>&tourId=<%# Eval("TourId") %>&tourType=<%#(int)Eval("TourType") %>" class="check-btn" title="查看"></a></span>
                                  <%}
                                  else
                                  {%>
                                <span class="th-line"> <a href="/CommonPage/SalesAccountedFor.aspx?source=3&sl=<%=Request.QueryString["sl"]%>&tourId=<%# Eval("TourId") %>&tourType=<%#(int)Eval("TourType") %>">
                                    <img src="/images/baozhang-cy.gif" />
                                    销售报账</a></span>
                            <% }
                              }%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">
            <ul class="fixed">
        	  <script type="text/javascript">
        	      document.write($("#btnAction").html());
               </script>
			</ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
</asp:content>
