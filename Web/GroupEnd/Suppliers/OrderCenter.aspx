<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCenter.aspx.cs" Inherits="Web.GroupEnd.Suppliers.OrderCenter" %>

<%@ Register Src="~/UserControl/Suppliers.ascx" TagName="Suppliers" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>供应商订单中心</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />
</head>
<body style="background: 0 none;">
    <uc1:Suppliers ID="Suppliers1" runat="server" OrderClass="default orderformicon" />
    <div class="list-main">
        <div class="list-maincontent">
            <div class="hr_10">
            </div>
            <div class="listsearch">
                <form id="frm" method="get" action='OrderCenter.aspx'>
                <input type="hidden" id="sl" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                订单号：<input type="text" style="width: 90px;" class="searchInput" name="txtOrderCode"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOrderCode") %>' />
                线路名称：<input type="text" style="width: 100px;" class="searchInput" name="txtRouteName"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>'>
                线路区域：
                <select id="ddlArea" name="ddlArea">
                    <%=EyouSoft.Common.UtilsCommons.GetSuppliersArea(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea"),SiteUserInfo.SourceCompanyInfo.CompanyId) %>
                </select>
                下单时间：
                <input type="text" onfocus="WdatePicker()" class="searchInput size68" name="txtBeginIssueTime"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginIssueTime") %>' />
                -
                <input type="text" onfocus="WdatePicker()" class="searchInput size68" name="txtEndIssueTime"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndIssueTime") %>' />
                出团日期：<input type="text" onfocus="WdatePicker()" class="searchInput size68" name="txtBeginLDate"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginLDate") %>' />
                -
                <input type="text" onfocus="WdatePicker()" class="searchInput size68" name="txtEndLDate"
                    value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndLDate") %>' /><br>
                订单状态：
                <select id="ddlOrderStatus" name="ddlOrderStatus">
                    <%=EyouSoft.Common.UtilsCommons.GetGroupEndOrderStatus(EyouSoft.Common.Utils.GetQueryStringValue("ddlOrderStatus"))%>
                </select>
                <a href="javascript:void(0)" id="btnSearch">
                    <img src="/Images/fx-images/searchbg.gif" alt="" /></a>
                </form>
            </div>
            <div class="listtablebox">
                <table width="100%" cellspacing="0" cellpadding="0" border="0" id="liststyle">
                    <tbody>
                        <tr class="odd">
                            <th align="center" rowspan="2">
                                编号
                            </th>
                            <th align="center" rowspan="2">
                                订单号
                            </th>
                            <th align="left" rowspan="2">
                                线路名称
                            </th>
                            <th align="center" rowspan="2">
                                出团时间
                            </th>
                            <th align="center" rowspan="2">
                                下单时间
                            </th>
                            <th align="center" rowspan="2">
                                销售员
                            </th>
                            <th align="center" colspan="2">
                                价格
                            </th>
                            <th align="right" rowspan="2">
                                订单金额
                            </th>
                            <th align="center" rowspan="2">
                                人数
                            </th>
                            <th align="center" rowspan="2">
                                订单状态
                            </th>
                            <th align="center" rowspan="2">
                                操作
                            </th>
                        </tr>
                        <tr>
                            <td bgcolor="#DCEFF3" align="right">
                                成人
                            </td>
                            <td bgcolor="#DCEFF3" align="right">
                                儿童
                            </td>
                        </tr>
                        <asp:Repeater ID="RpOrder" runat="server">
                            <ItemTemplate>
                                <tr class='<%#Container.ItemIndex%2==0?"odd":"" %>'>
                                    <td align="center">
                                        <%#Container.ItemIndex+1+(pageIndex-1)*pageSize %>
                                    </td>
                                    <td align="center">
                                        <%#Eval("OrderCode") %>
                                    </td>
                                    <td align="left">
                                        <a class="lineInfo" href="javascript:void(0)" bt-xtitle="" title="">
                                            <%#Eval("RouteName")%>
                                        </a>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), this.ProviderToDate)%>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), this.ProviderToDate)%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("SellerName")%>
                                    </td>
                                    <td align="right" class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%>
                                    </td>
                                    <td align="right" class="font-orange">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ChildPrice"),this.ProviderToMoney)%>
                                    </td>
                                    <td align="right" class="fontb-red">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), this.ProviderToMoney)%>
                                    </td>
                                    <td align="center">
                                        <b>
                                            <%#EyouSoft.Common.Utils.GetInt(Eval("Adults").ToString(), 0) + EyouSoft.Common.Utils.GetInt(Eval("Childs").ToString(), 0)%></b>
                                    </td>
                                    <td align="center">
                                        <%--<span data='font' data-value="<%#(int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus), Eval("GroupOrderStatus").ToString())%>">
                                            <%#Eval("GroupOrderStatus")%></span>--%>
                                            <%#EyouSoft.Common.UtilsCommons.GetOrderStateForHtml((int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus), Eval("OrderStatus").ToString()))%>
                                    </td>
                                    <td align="center">
                                        <a class="link1" href='/GroupEnd/Suppliers/OrderSee.aspx?sl=<%=Request.QueryString["sl"] %>&OrderId=<%#Eval("OrderId") %>&LDate=<%#Eval("LDate") %>&RDate=<%#Eval("RDate") %>'>
                                            查看</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder runat="server" ID="PhPage">
                            <tr class="odd">
                                <td bgcolor="#f4f4f4" align="center" colspan="14">
                                    <div class="pages">
                                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:Literal ID="litMsg" Visible="false" runat="server" Text="<tr><td align='center' colspan='14'>暂无订单!</td></tr>"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $(".link1").click(function() {
                var url = $(this).attr("href");
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "查看订单详情",
                    modal: true,
                    width: "750px",
                    height: "400px"
                });
                return false;
            });

//            //控制状态的字体颜色
//            $("span[data='font']").each(function() {
//                var value = $(this).attr("data-value");
//                if (value == "<%=(int)EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus.报名未确认 %>" || value == "<%=(int)EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus.预留未确认 %>") {
//                    $(this).attr("class", "fontbsize12");
//                }
//            });


            $("#btnSearch").click(function() {
                $("#frm").submit();
            });


            //Enter搜索
            $("#frm").find(":text").keypress(function(e) {
                if (e.keyCode == 13) {
                    $("#frm").submit();
                    return false;
                }
            });

            //PaoPao
            //            $('.lineInfo').bt({
            //                contentSelector: function() {
            //                    return "负责人：张先生<br />电话：0571-5676987410<br />QQ：5321254987";
            //                },
            //                positions: ['left', 'right', 'bottom'],
            //                fill: '#FFF2B5',
            //                strokeStyle: '#D59228',
            //                noShadowOpts: { strokeStyle: "#D59228" },
            //                spikeLength: 10,
            //                spikeGirth: 15,
            //                width: 170,
            //                overlap: 0,
            //                centerPointY: 1,
            //                cornerRadius: 4,
            //                shadow: true,
            //                shadowColor: 'rgba(0,0,0,.5)',
            //                cssStyles: { color: '#00387E', 'line-height': '180%' }
            //            });
        })
    </script>

</body>
</html>
