<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="OrderPaid.aspx.cs" Inherits="Web.SellCenter.OrderPaid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="daochu"></s><a class="toolbar_daochu" id="i_a_toxls" hidefocus="true"
                    href="javascript:void(0)"><span>导&nbsp;&nbsp;出</span></a></li>
            </ul>
        </div>
        <div style="height: 10px;" class="tablehead">
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellspacing="0" border="0" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th valign="middle" align="center" class="th-line h20">
                            订单号(结算单)
                        </th>
                        <th valign="middle" align="center" class="th-line h20">
                            下单人
                        </th>
                        <th valign="middle" align="center" class="th-line h20">
                            销售员
                        </th>
                        <th class="th-line h20" style="text-align:left;">
                            客源单位
                        </th>
                        <%--<th class="th-line h20" style="text-align:center; vertical-align:middle;">
                            标准/等级
                        </th>                        
                        <th valign="middle" align="right" class="th-line h20">
                            销售价
                        </th>
                        <th valign="middle" align="right" class="th-line h20">
                            结算价
                        </th>--%>
                        <th valign="middle" align="center" class="th-line h20">
                            人数
                        </th>
                        <asp:PlaceHolder ID="phUnset" runat="server">
                            <th valign="middle" align="center" class="th-line h20">
                                未分配座位
                            </th>
                        </asp:PlaceHolder>
                        <th valign="middle" align="center" class="th-line h20">
                            合计金额
                        </th>                        
                        <th valign="middle" align="center" class="th-line h20">
                            订单状态
                        </th>
                        <th valign="middle" align="center" class="th-line h20">
                            下单时间
                        </th>
                        <th valign="middle" align="center" class="th-line h20">
                            查看
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class='<%# Container.ItemIndex%2==0?"":"odd" %>'>
                                <td align="center">
                                    <a href="<%# GetPrintJieSuanDan( Eval("OrderId"),Eval("TourType"))%>" target="_blank">
                                        <%#Eval("OrderCode")%></a>
                                </td>
                                <td align="center">
                                    <span id="CommonSalesRoom1_lbljdName">
                                        <%#Eval("Operator")%></span>
                                </td>
                                <td align="center">                                    
                                        <%#Eval("SellerName")%>
                                </td>
                                <td align="left">
                                    <a href="<%# GetPrintYouKeQueRenDan( Eval("OrderId"),Eval("TourType"))%>" target="_blank">
                                        <%#Eval("BuyCompanyName")%></a>
                                </td>
                                <%--<td style="text-align:center;">
                                    <%#Eval("BaoJiaBiaoZhunName")%>/<%#Eval("KeHuLevName")%>
                                </td>                                
                                <td align="right" class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%>
                                </td>
                                <td align="right" class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PeerAdultPrice"), this.ProviderToMoney)%>
                                </td>--%>
                                <td align="center">
                                    <%# Eval("Adults")%>+<%#Eval("Childs")%>
                                </td>
                                <%#this.IsShow(Eval("UnSeat"))%>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), this.ProviderToMoney)%>
                                </td>                               
                                <td align="center">
                                    <%#Eval("OrderStatus").ToString()%>
                                </td>
                                <td align="center">
                                    <%# Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                <td align="center">
                                    <a class="check-btn" onclick="OrderInfo.ToOrderInfoByType('<%#Eval("TourType")%>','<%#Eval("OrderId")%>');return false;"
                                        href="javascript:void(0);"></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" Text="" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none; height: 10px;" class="tablehead">
            
        </div>
    </div>

    <script type="text/javascript">
        var OrderInfo = {
            ToOrderInfoByType: function(tourtype, orderid) {
                var sl = '<%=Request.QueryString["sl"] %>';
                if (tourtype != null) {
                    if (tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼 %>' ||
                   tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼 %>' ||
                   tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼 %>') {
                        window.location.href = "/TeamCenter/SanKeBaoMing.aspx?OrderId=" + orderid + "&sl=" + sl + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
                    }
                    else if (tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.出境团队 %>' ||
                        tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.单项服务 %>' ||
                        tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.地接团队 %>' ||
                        tourtype == '<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团团队 %>') {
                        window.location.href = "/SellCenter/CustomerPlan/OrderInfo.aspx?OrderId=" + orderid + "&sl=" + sl;
                    } else if (tourtype == "<%=EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线 %>") {
                        window.location.href = "/TeamCenter/ShortSanKeBaoMing.aspx?OrderId=" + orderid + "&sl=" + sl + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
                    }
                    else {
                        tableToolbar._showMsg("数据有误");
                    }
                }
            }
        }

        var pagingRecordCount = "<%=RecordCount %>";

        $(document).ready(function() {
            toXls.init({ "selector": "#i_a_toxls" });
        });
    </script>

</asp:Content>
