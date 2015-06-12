<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationChange.aspx.cs"
    Inherits="Web.OperaterCenter.OperationChange" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">
    <form id="Form1" runat="server">
    <input type="hidden" name="sl" id="sl" value="<%=Request.QueryString["sl"]%>" />
    <div class="mainbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="/OperaterCenter/OperationChange.aspx?type=tour&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl")%>"
                    hidefocus="true" class='ztorderform <%=EyouSoft.Common.Utils.GetQueryStringValue("type")=="tour"|| EyouSoft.Common.Utils.GetQueryStringValue("type")=="" ? "de-ztorderform":""%>'>
                    <span>计划变更</span></a> </li>
                <li><s class="orderformicon"></s><a href="/OperaterCenter/OperationChange.aspx?type=order&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl")%>"
                    hidefocus="true" class="ztorderform <%=EyouSoft.Common.Utils.GetQueryStringValue("type")=="order"?"de-ztorderform":""%>">
                    <span>订单变更</span></a> </li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <asp:PlaceHolder ID="tabTourChangeView" runat="server">
            <div class="tablelist-box">
                <table width="100%" id="liststyleTour">
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
                            销售员
                        </th>
                        <th align="center" class="th-line">
                            计调员
                        </th>
                        <th align="center" class="th-line">
                            导游
                        </th>
                        <th align="center" class="th-line">
                            变更时间
                        </th>
                        <th align="center" class="th-line">
                            变更人
                        </th>
                        <th align="left" class="th-line">
                            变更标题
                        </th>
                        <th align="center" class="th-line">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater ID="repTourChangeList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("TourId") %>" />
                                </td>
                                <td align="center">
                                    <a href='<%# PrintUrl %>?tourid=<%# Eval("TourId") %>' target="_blank" data-class="ContactInfo">
                                        <%# Eval("TourCode")%></a>
                                    <div style="display: none">
                                        <%# GetOperaterInfo(Eval("TourId").ToString())%>
                                    </div>
                                    <span><a target="_blank" <%# (bool)Eval("State") == true ? "class=fontgreen" : "class=fontred"%> href='<%#Eval("TourCode")==""?"javascript:void(0)":PrintUrl %>?tourid=<%# Eval("TourId") %>&type=1'>
                                        <%#Eval("TourCode") == "" ? "" : "(变)"%></a></span>
                                </td>
                                <td align="left">
                                    <a href='<%=PrintUrl %>?tourId=<%# Eval("TourId") %>' target="_blank">
                                        <%# Eval("RouteName")%></a>
                                </td>
                                <td align="center">
                                    <%# Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%# GetOperaterList(Eval("TourPlaner"))%>
                                </td>
                                <td align="center">
                                    <%# GetGuidList(Eval("TourGuide"))%>
                                </td>
                                <td align="center">
                                    <%# Eval("IssueTime")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Operator")%>
                                </td>
                                <td align="left">
                                    <a href="javascript:" data-class="tourChangeInfo" data-id="<%# Eval("Id") %>" data-status="<%# (bool)Eval("State")==true?"0":"1" %>">
                                        <%# Eval("Title")%></a>
                                </td>
                                <td align="center">
                                    <%# (bool)Eval("State") == true ? " <b class=fontgreen>已确认变更</b> " : "<b class=fontred>未确认变更</b> "%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="tabOrderChangeView" runat="server">
            <div class="tablelist-box">
                <table width="100%" id="liststyleOrder">
                    <tr>
                        <th class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox1" />
                        </th>
                        <th align="center" class="th-line">
                            订单号
                        </th>
                        <th align="center" class="th-line">
                            订单销售员
                        </th>
                        <th align="center" class="th-line">
                            变更时间
                        </th>
                        <th align="center" class="th-line">
                            变更人
                        </th>
                        <th align="left" class="th-line">
                            变更内容
                        </th>
                        <th align="center" class="th-line">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater ID="repOrderChangelist" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" value="<%# Eval("Id") %>" />
                                </td>
                                <td align="center">
                                    <%# Eval("OrderCode")%>
                                </td>
                                <td align="center">
                                    <%# Eval("OrderSale")%>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%# Eval("Operator")%>
                                </td>
                                <td align="left">
                                    <a href="javascript:" data-class="orderChangeContect" data-id="<%# Eval("Id") %>">
                                        <%# Eval("Content")%></a>
                                </td>
                                <td align="center">
                                    <%# (bool)Eval("IsSure") == true ? " <b class=fontgreen>已确认变更</b> " : "<b class=fontred>未确认变更</b> "%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:PlaceHolder>
        <div class="tablehead">
            <div class="pages">
                <asp:Label ID="lab_Text" runat="server"></asp:Label>
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var changePage = {
            _Sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
            _type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _Bindbtn: function() {
                //计划变更详细
                $("#liststyleTour [data-class='tourChangeInfo']").unbind("click");
                $("#liststyleTour [data-class='tourChangeInfo']").click(function() {
                    var Id = $(this).attr("data-Id");
                    var status = $(this).attr("data-Status");
                    changePage._OpenBoxy("查看变更内容", '/OperaterCenter/ChangeInfo.aspx?Id=' + Id + '&sl=' + changePage._Sl + '&type=' + changePage._type + "&status=" + status, "750px", "200px", true);
                });

                //订单变更详细  
                $("#liststyleOrder [data-class='orderChangeContect']").click(function() {
                    var OrderId = $(this).attr("data-Id");
                    changePage._OpenBoxy("查看变更内容", '/OperaterCenter/ChangeInfo.aspx?Id=' + OrderId + '&sl=' + changePage._Sl + '&type=' + changePage._type, "750px", "160px", true);
                });
            },
            _PageInit: function() {
                this._Bindbtn();
                if (changePage._type == "order") {
                    $("#<%=tabTourChangeView.ClientID %>").hide();
                    $("#<%=tabOrderChangeView.ClientID %>").show();
                    $(this).parent().find("a").eq(1).addClass("de-ztorderform");
                    $(this).parent().find("a").eq(0).removeClass("de-ztorderform");
                } else {
                    $("#<%=tabTourChangeView.ClientID %>").show();
                    $("#<%=tabOrderChangeView.ClientID %>").hide();
                    $(this).parent().find("a").eq(0).addClass("de-ztorderform");
                    $(this).parent().find("a").eq(1).removeClass("de-ztorderform");
                }
            }

        }

        $(function() {
            tableToolbar.init({
                tableContainerSelector: "#liststyleTour"
            });
            tableToolbar.init({ tableContainerSelector: "#liststyleOrder" });
            $("#liststyleTour [data-class='ContactInfo']").bt({
                contentSelector: function() {
                    return $(this).next("div").html();
                },
                positions: ['left', 'right', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 15,
                width: 200,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '200%' }
            });
            changePage._PageInit();
        });
    </script>

</asp:Content>
