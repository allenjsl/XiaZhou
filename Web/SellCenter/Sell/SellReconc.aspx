<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SellReconc.aspx.cs" Inherits="Web.SellCenter.Sell.SellReconc" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a class="toolbar_dayin" hidefocus="true" href="#"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="excel"></s><a class="toolbar_excel" hidefocus="true" href="#"><span>导出Excel</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="104" align="center" class="th-line">
                            团号
                        </th>
                        <th width="123" align="left" class="th-line">
                            线路名称
                        </th>
                        <th width="62" align="center" class="th-line">
                            订单号
                        </th>
                        <th width="222" align="center" class="th-line">
                            客户单位/游客姓名
                        </th>
                        <th align="center" class="th-line" colspan="2">
                            人数
                        </th>
                        <th width="89" align="center" class="th-line">
                            销售员
                        </th>
                        <th width="108" align="center" class="th-line">
                            收款金额
                        </th>
                        <th width="91" align="center" class="th-line">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="odd">
                                <td align="center">
                                    <%#Eval("TourCode")%>
                                </td>
                                <td align="left">
                                    <a href="#">
                                        <%#Eval("RouteName")%></a>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderCode")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Customer")%>
                                </td>
                                <td align="center">
                                    <img alt="成人" src="/images/chengren.gif">
                                    <b class="fontblue">
                                        <%#Eval("Adults")%></b>
                                </td>
                                <td align="center">
                                    <img alt="儿童" src="/images/child.gif">
                                    <b class="fontgreen">
                                        <%#Eval("Childs")%></b>
                                </td>
                                <td align="center">
                                    <%#Eval("Salesman")%>
                                </td>
                                <td align="center">
                                    <b class="fontbsize12">
                                        <%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal( Eval("ReceivableAmount")))%></b>
                                </td>
                                <td align="center">
                                    <%#Eval("Status")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="border-top: 0 none;" class="tablehead">
            <ul class="fixed">
                <li><s class="dayin"></s><a class="toolbar_dayin" hidefocus="true" href="#"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="excel"></s><a class="toolbar_excel" hidefocus="true" href="#"><span>导出Excel</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
