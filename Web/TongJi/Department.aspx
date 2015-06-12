<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="Department.aspx.cs" Inherits="EyouSoft.Web.TongJi.Department" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
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
                    部门：
                    <uc1:SelectSection ID="SelectSection1" runat="server" SetTitle="选择部门" />
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateS"
                        name="LeaveDateS" value="<%=Utils.GetQueryStringValue("LeaveDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateE"
                        name="LeaveDateE" value="<%=Utils.GetQueryStringValue("LeaveDateE") %>" />
                    核算时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtCheckDateS"
                        name="CheckDateS" value="<%=(Utils.GetQueryStringValue("CheckDateS"))==""? UtilsCommons.GetDateString(Utils.GetFristDayOfMonth(),this.ProviderToDate) :Utils.GetQueryStringValue("CheckDateS")%>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtCheckDateE"
                        name="CheckDateE" value="<%=(Utils.GetQueryStringValue("CheckDateE"))==""?UtilsCommons.GetDateString(Utils.GetLastDayOfMonth(),this.ProviderToDate):Utils.GetQueryStringValue("CheckDateE") %>" />
                    分公司：
                    <select class="inputselect" name="DepartId" id="DepartId">
                        <asp:Literal ID="ltrDepartHtml" runat="server"></asp:Literal>
                    </select>
                    <input type="submit" class="search-btn" />
                </p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="toolbar_Depart_Div1">
            <ul class="fixed">
                <li><s class="dayin"></s><a href="javascript:void(0);" onclick="PrintPage('liststyle');"
                    hidefocus="true" class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" onclick="toXls1();return false"
                    hidefocus="true" class="toolbar_daochu"><span>导出列表</span></a></li>
                <li class="line"></li>
                <li><s class="tongji"></s><a href="javascript:void(0);" onclick="GoToDepartImg();return false;"
                    hidefocus="true"><span>统计图</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" height="32" class="thinputbg">
                        <input type="checkbox" name="ckbAll" id="ckbAll" />
                    </th>
                    <th align="center" class="th-line">
                        部门
                    </th>
                    <th align="center" class="th-line">
                        员工人数
                    </th>
                    <th align="center" class="th-line">
                        订单数量
                    </th>
                    <th align="center" class="th-line">
                        订单人数
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
                <asp:Repeater runat="server" ID="rptDepart">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="ckbXH" id="ckbXH_<%# Container.ItemIndex + 1 %>" value="<%# Eval("DeptId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("DeptName")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="tl_ck" data-departid="<%# Eval("DeptId") %>"
                                    data-class="departSeller">
                                    <%# Eval("PeopleNum")%></a>
                            </td>
                            <td align="center">
                                <%# Eval("OrderNum")%>
                            </td>
                            <td align="center">
                                <%# Eval("OrderPersonNum")%>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%# UtilsCommons.GetMoneyString(Eval("TotalIncome"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%# UtilsCommons.GetMoneyString(Eval("TotalOutlay"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# UtilsCommons.GetMoneyString(Eval("GrossProfit"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%# GetBfbString(Eval("GrossProfitRate"), 0)%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="3" align="right">
                        <strong>合计：</strong>
                    </td>
                    <td align="center">
                        <strong>
                            <%= SumMoney.OrderNum %></strong>
                    </td>
                    <td align="center">
                        <strong>
                            <%= SumMoney.PeopleNum %></strong>
                    </td>
                    <td align="right">
                        <b class="fontblue">
                            <%= UtilsCommons.GetMoneyString(SumMoney.InCome, ProviderToMoney)%></b>
                    </td>
                    <td align="right">
                        <b class="fontgreen">
                            <%= UtilsCommons.GetMoneyString(SumMoney.Pay, ProviderToMoney)%></b>
                    </td>
                    <td align="right">
                        <b class="fontred">
                            <%= UtilsCommons.GetMoneyString(SumMoney.GrossProfit, ProviderToMoney)%></b>
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;" id="toolbar_Depart_Div2">
        </div>
    </div>

    <script type="text/javascript">

        //跳转到统计图
        function GoToDepartImg() {
            var queryData = { sl: '<%= Utils.GetQueryStringValue("sl") %>' };
            queryData.LeaveDateS = $("#txtLeaveDateS").val();
            queryData.LeaveDateE = $("#txtLeaveDateE").val();
            queryData.CheckDateS = $("#txtCheckDateS").val();
            queryData.CheckDateE = $("#txtCheckDateE").val();
            queryData.SunCompanyId = $("#DepartId").val();
            queryData.DepartId = $("#<%= SelectSection1.SelectIDClient %>").val();

            window.location.href = "/TongJi/DepartImg.aspx?" + $.param(queryData);

        }

        $(function() {
            tableToolbar.init({
                objectName: "部门业绩统计"
            });

            //初始化选择的分公司
            //$("#DepartId").val('<%= Utils.GetQueryStringValue("DepartId") %>');
            //初始化底部的操作栏
            $("#toolbar_Depart_Div2").html($("#toolbar_Depart_Div1").clone(true));

            var data = { sl: '<%= Utils.GetQueryStringValue("sl") %>' };
            data.LeaveDateS = $("#txtLeaveDateS").val();
            data.LeaveDateE = $("#txtLeaveDateE").val();
            data.CheckDateS = $("#txtCheckDateS").val();
            data.CheckDateE = $("#txtCheckDateE").val();
            data.SunCompanyId = $("#DepartId").val();
            //订单数查看
            $("#liststyle").find("a[data-class='departSeller']").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/TongJi/DepartSeller.aspx?departId=" + $(this).attr("data-departid") + "&" + $.param(data),
                    title: "查看部门业绩",
                    modal: true,
                    width: "925px",
                    height: "485px"
                });
                return false;
            })
        });
    </script>

</asp:Content>
