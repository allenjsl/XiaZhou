<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="EyouSoft.Web.FinanceManage.DaiShou.List"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%--财务管理-代收管理--%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/GysXuanYong.ascx" TagName="GysXuanYong" TagPrefix="uc1" %>

<asp:Content ID="MainBodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form action="List.aspx" method="get">
            <input type="hidden" name="sl" id="sl" value="<%=SL %>" />
            <span class="searchT">
                <p>
                    订单号：<input type="text" name="txtOrderCode" class="inputtext formsize100" value="<%=Utils.GetQueryStringValue("txtOrderCode") %>" />
                    供应商：<uc1:GysXuanYong runat="server" ID="txtGys" />
                    代收状态：<select class="inputselect" name="txtStatus">
                        <%= EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.FinStructure.DaiShouStatus),new string[]{"2"}), EyouSoft.Common.Utils.GetQueryStringValue("txtStatus"),"","请选择")%>
                    </select>                    
                    <button type="submit" class="search-btn">
                        搜索</button>
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                
            </ul>
            <div class="pages">
                <!--paging-->
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        订单号
                    </th>
                    <th align="center" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        下单人
                    </th>
                    <th align="center" class="th-line">
                        供应商
                    </th>
                    <th align="center" class="th-line">
                        代收金额
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                    <th align="center" class="th-line">
                        代收时间
                    </th>
                    <th align="center" class="th-line">
                        代收备注
                    </th>                    
                    <th style="text-align:left;" class="th-line" style="width: 100px">
                        操作
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr i_tourid="<%#Eval("TourId") %>" i_daishouid="<%#Eval("DaiShouId") %>">
                            <td><input type="checkbox" name="checkbox" value="<%#Eval("OrderId") %>" /></td>
                            <td align="center">
                                <%#Eval("OrderCode") %>
                            </td>
                            <td align="center">
                                <%#Eval("CrmName") %>
                            </td>
                            <td align="center">
                                <%#Eval("OrderSellerName") %>
                            </td>
                            <td align="center">
                                <%#Eval("OrderXiaDanRenName") %>
                            </td>
                            <td align="center">
                                <%#Eval("GysName") %>
                            </td>
                            <td align="center" class="<%#(int)Eval("Status")==1?"":"fontred" %>">
                                <%#Eval("JinE","{0:C2}") %>
                            </td>
                            <td align="center" class="<%#(int)Eval("Status")==1?"":"fontred" %>">
                                <%#Eval("Status") %>
                            </td>
                            <td align="center">
                                <%#Eval("Time","{0:yyyy-MM-dd}") %>
                            </td>
                            <td align="center">
                                <%#Eval("BeiZhu") %>
                            </td>                            
                            <td>
                                <%#GetCaoZuoHtml(Eval("Status"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="16" width="100%" align="center">
                            暂无数据。
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            chaKan: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _title = "代收登记-查看";
                var _data = { sl: '<%=SL %>', tourid: _$tr.attr("i_tourid"), daishouid: _$tr.attr("i_daishouid") };
                Boxy.iframeDialog({ iframeUrl: "/financemanage/daishou/edit.aspx", title: _title, modal: true, width: "800px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
            },
            shenPi: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _title = "代收登记-审批";
                var _data = { sl: '<%=SL %>', tourid: _$tr.attr("i_tourid"), daishouid: _$tr.attr("i_daishouid"), isshenpi: "1" };
                Boxy.iframeDialog({ iframeUrl: "/financemanage/daishou/edit.aspx", title: _title, modal: true, width: "800px", height: "500px", data: _data, afterHide: function() { iPage.reload(); } });
            }
        };

        $(document).ready(function() {
            tableToolbar.init({});
            $(".i_chakan").click(function() { iPage.chaKan(this); });
            $(".i_shenpi").click(function() { iPage.shenPi(this); });
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");

            window["<%=txtGys.ClientID %>"].setValue({ gysId: '<%=EyouSoft.Common.Utils.GetQueryStringValue(txtGys.GysIdClientID) %>', gysName: '<%=EyouSoft.Common.Utils.GetQueryStringValue(txtGys.GysNameClientID) %>' });
        });
    </script>

</asp:Content>
