<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="Route.aspx.cs" Inherits="EyouSoft.Web.TongJi.Route" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="form1" method="get">
        <input type="hidden" name="type" value="<%=Utils.GetQueryStringValue("type") %>" />
        <input type="hidden" size="8" name="sl" value="<%=Utils.GetQueryStringValue("sl") %>" />
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    线路区域：<select class="inputselect" id="ddlArea" name="AreaId">
                        <%= UtilsCommons.GetAreaLineForSelect(Utils.GetInt(Utils.GetQueryStringValue("AreaId")), SiteUserInfo.CompanyId)%>
                    </select>
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateS"
                        name="LeaveDateS" value="<%=Utils.GetQueryStringValue("LeaveDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateE"
                        name="LeaveDateE" value="<%=Utils.GetQueryStringValue("LeaveDateE") %>" />
                    核算时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtCheckDateS"
                        name="CheckDateS" value="<%=(Utils.GetQueryStringValue("CheckDateS"))==""? UtilsCommons.GetDateString(Utils.GetFristDayOfMonth(),this.ProviderToDate) :Utils.GetQueryStringValue("CheckDateS")  %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtCheckDateE"
                        name="CheckDateE" value="<%=(Utils.GetQueryStringValue("CheckDateE"))==""?UtilsCommons.GetDateString(Utils.GetLastDayOfMonth(),this.ProviderToDate):Utils.GetQueryStringValue("CheckDateE")%>" />
                    分公司：
                    <select class="inputselect" name="DepartId" id="DepartId">
                        <asp:Literal ID="ltrDepartHtml" runat="server"></asp:Literal>
                    </select>
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="toolbar_Route_Div1">
            <ul class="fixed">
                <li><s class="dayin"></s><a href="javascript:void(0);" onclick="PrintPage('liststyle'); return false;"
                    hidefocus="true" class="toolbar_dayin"><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" onclick="toXls1(); return false;" hidefocus="true"
                    class="toolbar_daochu"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg">
                            <input type="checkbox" name="ckbAll" id="ckbAll" />
                        </th>
                        <th width="158" align="center" class="th-line">
                            线路区域
                        </th>
                        <th width="126" align="center" class="th-line">
                            收客数
                        </th>
                        <th width="158" align="center" class="th-line">
                            团队数量
                        </th>
                        <th width="126" align="right" class="th-line">
                            总收入
                        </th>
                        <th width="126" align="right" class="th-line">
                            总支出
                        </th>
                        <th width="111" align="right" class="th-line">
                            毛利
                        </th>
                        <th width="132" align="center" class="th-line">
                            毛利率
                        </th>
                        <th width="132" align="center" class="th-line">
                            人均毛利
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptRoute">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    <input type="checkbox" name="ckbXH" id="ckbXH_<%# Container.ItemIndex + 1 %>" value="<%# Eval("AreaId") %>">
                                </td>
                                <td align="center">
                                    <%# Eval("AreaName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Adults")%><sup class="fontred">+<%# Eval("Childs")%></sup>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0);" class="tl_ck" data-areaid="<%# Eval("AreaId") %>" data-class="tourCount">
                                        <%# Eval("TourCount")%></a>
                                </td>
                                <td align="right">
                                    <a href="javascript:void(0);" class="shouru_box" data-areaid="<%# Eval("AreaId") %>"
                                        data-class="totalIncome"><b class="fontblue">
                                            <%# UtilsCommons.GetMoneyString(Eval("TotalIncome"), this.ProviderToMoney)%></b></a>
                                </td>
                                <td align="right">
                                    <a href="javascript:void(0);" class="zhchu_box" data-areaid="<%# Eval("AreaId") %>"
                                        data-class="totalOutlay"><b class="fontgreen">
                                            <%# UtilsCommons.GetMoneyString(Eval("TotalOutlay"), this.ProviderToMoney)%></b></a>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%# UtilsCommons.GetMoneyString(Eval("GrossProfit"), this.ProviderToMoney)%></b>
                                </td>
                                <td align="center">
                                    <%# GetBfbString(Eval("GrossProfitRate"), 0)%>
                                </td>
                                <td align="center">
                                <b class="fontred"><%# UtilsCommons.GetMoneyString(Eval("PerGrossProfitRate"), this.ProviderToMoney)%></b>
                                   <%-- <%# GetBfbString(Eval("PerGrossProfitRate"), 0)%>--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;" id="toolbar_Route_Div2">
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            
            //alert($("#txtCheckDateS").val());

            tableToolbar.init({
                objectName: "线路流量统计"
            });

            //初始化选择的分公司
            //$("#DepartId").val('<%= Utils.GetQueryStringValue("DepartId") %>');
            //初始化底部的操作栏
            $("#toolbar_Route_Div2").html($("#toolbar_Route_Div1").clone(true));

            var data = { sl: '<%= Utils.GetQueryStringValue("sl") %>', type: '<%= Utils.GetQueryStringValue("type") %>' };
            data.LeaveDateS = $("#txtLeaveDateS").val();
            data.LeaveDateE = $("#txtLeaveDateE").val();
            data.CheckDateS = $("#txtCheckDateS").val();
            data.CheckDateE = $("#txtCheckDateE").val();
            data.DepartId = $("#DepartId").val();
            //团队数量查看
            $("#liststyle").find("a[data-class='tourCount']").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/TongJi/RouteTourList.aspx?AreaId=" + $(this).attr("data-areaid") + "&" + $.param(data),
                    title: "查看团队数量",
                    modal: true,
                    width: "745px",
                    height: "485px"
                });
                return false;
            });
            //总收入查看
            $("#liststyle").find("a[data-class='totalIncome']").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/TongJi/RouteTotalIncome.aspx?AreaId=" + $(this).attr("data-areaid") + "&" + $.param(data),
                    title: "查看总收入",
                    modal: true,
                    width: "745px",
                    height: "485px"
                });
                return false;
            })
            //总支出查看
            $("#liststyle").find("a[data-class='totalOutlay']").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/TongJi/RouteTotalOutlay.aspx?AreaId=" + $(this).attr("data-areaid") + "&" + $.param(data),
                    title: "查看总支出",
                    modal: true,
                    width: "745px",
                    height: "485px"
                });
                return false;
            })
        });
    </script>

</asp:Content>
