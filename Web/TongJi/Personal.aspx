<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="Personal.aspx.cs" Inherits="EyouSoft.Web.TongJi.Personal" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="form1" method="get">
        <input type="hidden" size="8" name="sl" value="<%=Utils.GetQueryStringValue("sl") %>" />
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    销售员：
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="选择销售员" />
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateS"
                        name="LeaveDateS" value="<%=Utils.GetQueryStringValue("LeaveDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateE"
                        name="LeaveDateE" value="<%=Utils.GetQueryStringValue("LeaveDateE") %>" />
                    核算时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtCheckDateS"
                        name="CheckDateS" value="<%=(Utils.GetQueryStringValue("CheckDateS"))==""? UtilsCommons.GetDateString(Utils.GetFristDayOfMonth(),this.ProviderToDate) :Utils.GetQueryStringValue("CheckDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtCheckDateE"
                        name="CheckDateE" value="<%=(Utils.GetQueryStringValue("CheckDateE"))==""?UtilsCommons.GetDateString(Utils.GetLastDayOfMonth(),this.ProviderToDate):Utils.GetQueryStringValue("CheckDateE") %>" />
                    分公司：
                    <select class="inputselect" name="DepartId" id="DepartId">
                        <asp:Literal ID="ltrDepartHtml" runat="server"></asp:Literal>
                    </select>
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="toolbar_Personal_Div1">
            <ul class="fixed">
                <li><s class="dayin"></s><a href="javascript:void(0);" onclick="PrintPage('liststyle'); return false;"
                    hidefocus="true" class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" onclick="toXls1(); return false;" hidefocus="true"
                    class="toolbar_daochu"><span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="ckbAll" id="ckbAll" />
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        订单数
                    </th>
                    <th align="right" class="th-line">
                        总收入
                    </th>
                    <th align="right" class="th-line">
                        总支出
                    </th>
                    <th align="right" class="th-line">
                        毛利
                    </th>
                    <th align="center" class="th-line">
                        毛利率
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptPersonal">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="ckbXh" id="ckbXH_<%# Container.ItemIndex + 1 %>" value="<%# Eval("SellerId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <%# Eval("PeopleNum")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="tj_ck" data-sellerid="<%# Eval("SellerId") %>"
                                    data-class="orderNum">
                                    <%# Eval("OrderNum")%></a>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%# UtilsCommons.GetMoneyString(Eval("TotalIncome"), this.ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%# UtilsCommons.GetMoneyString(Eval("TotalOutlay"), this.ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# UtilsCommons.GetMoneyString(Eval("GrossProfit"), this.ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%# GetBfbString(Eval("GrossProfitRate"), 0)%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;" id="toolbar_Personal_Div2">
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({
                objectName: "个人业绩统计"
            });
            //初始化选择的分公司
            //$("#DepartId").val('<%= Utils.GetQueryStringValue("DepartId") %>');
            //初始化底部的操作栏
            $("#toolbar_Personal_Div2").html($("#toolbar_Personal_Div1").clone(true));

            var data = { sl: '<%= Utils.GetQueryStringValue("sl") %>', type: '<%= Utils.GetQueryStringValue("type") %>' };
            data.LeaveDateS = $("#txtLeaveDateS").val();
            data.LeaveDateE = $("#txtLeaveDateE").val();
            data.CheckDateS = $("#txtCheckDateS").val();
            data.CheckDateE = $("#txtCheckDateE").val();
            data.SunCompanyId = $("#DepartId").val();
            //订单数查看
            $("#liststyle").find("a[data-class='orderNum']").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/TongJi/PersonalOrder.aspx?sellerId=" + $(this).attr("data-sellerid") + "&" + $.param(data),
                    title: "查看订单",
                    modal: true,
                    width: "925px",
                    height: "485px"
                });
                return false;
            })
        });
    </script>

</asp:Content>
