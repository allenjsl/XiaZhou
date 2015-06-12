<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="Income.aspx.cs" Inherits="EyouSoft.Web.TongJi.Income" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="form1" method="get">
        <input type="hidden" size="8" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    部门：
                    <uc1:SelectSection ID="SelectSection1" runat="server" SetTitle="选择部门" />
                    销售员：
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="选择销售员" />
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateS"
                        name="LeaveDateS" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("LeaveDateS") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtLeaveDateE"
                        name="LeaveDateE" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("LeaveDateE") %>" />
                    <!--未收款：
                    <uc1:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />-->
                    分公司：
                    <select class="inputselect" name="SunCompan" id="SunCompan">
                        <asp:Literal ID="ltrSunCompany" runat="server"></asp:Literal>
                    </select>
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="toolbar_Income_Div1">
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
                        部门
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="right" class="th-line">
                        应收款
                    </th>
                    <th align="right" class="th-line">
                        已收款
                    </th>
                    <th align="right" class="th-line">
                        未收
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptIncome">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="ckbXH" id="ckbXH_<%# Container.ItemIndex + 1 %>" value="<%# Eval("SellerId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("DeptName")%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TotalAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("InAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <a href="javascript:void(0);" class="dz_ck" data-sellerid="<%# Eval("SellerId") %>"
                                    data-class="restAmount"><b class="fontred">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("RestAmount"), ProviderToMoney)%></b></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="3" align="right">
                        合计：
                    </td>
                    <td align="right">
                        <b class="fontblue">
                            <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.TotalAmount, ProviderToMoney)%></b>
                    </td>
                    <td align="right">
                        <b class="fontgreen">
                            <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.InAmount, ProviderToMoney)%></b>
                    </td>
                    <td align="right">
                        <a href="javascript:void(0);" class="dz_ck" data-sellerid="" data-class="restAmount">
                            <b class="fontred">
                                <%= EyouSoft.Common.UtilsCommons.GetMoneyString(SumMoney.RestAmount, ProviderToMoney)%></b></a>
                    </td>
                </tr>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;" id="toolbar_Income_Div2">
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({
                objectName: "收入对账单"
            });
            //初始化选择的分公司
            //$("#SunCompan").val('<%= EyouSoft.Common.Utils.GetQueryStringValue("SunCompan") %>');
            //初始化底部的操作栏
            $("#toolbar_Income_Div2").html($("#toolbar_Income_Div1").clone(true));

            var data = { sl: '<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', type: '<%= EyouSoft.Common.Utils.GetQueryStringValue("type") %>' };
            data.LeaveDateS = $("#txtLeaveDateS").val();
            data.LeaveDateE = $("#txtLeaveDateE").val();
            data.SunCompanyId = $("#SunCompan").val();
            data.DepartId = $("#<%= SelectSection1.SelectIDClient %>").val();

            //未收金额
            /*var weiShouWUC = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientUniqueID %>']);
            var weiShouOperator = "<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator) %>";
            var weiShouOperatorNumber = "<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber) %>";

            weiShouWUC.setOperator(weiShouOperator);
            weiShouWUC.setOperatorNumber(weiShouOperatorNumber);
            data.Calculation = weiShouWUC.getOperator(); ;
            data.Money = weiShouWUC.getOperatorNumber();*/
            
            //订单数查看
            $("#liststyle").find("a[data-class='restAmount']").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/TongJi/RestAmount.aspx?sellerId=" + $(this).attr("data-sellerid") + "&" + $.param(data),
                    title: "查看订单",
                    modal: true,
                    width: "925px",
                    height: "485px"
                });
                return false;
            });



        });
    </script>

</asp:Content>
