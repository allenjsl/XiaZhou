<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SellIncome.aspx.cs" Inherits="Web.SellCenter.Sell.SellIncome" %>

<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--内容-->
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <span class="searchT">
                <p>
                    团号：<input type="text" size="28" name="tourNum" value='<%=Request.QueryString["tourNum"]%>' />
                    订单号：
                    <input type="text" size="28" name="orderNum" value='<%=Request.QueryString["orderNum"]%>' />
                    客户单位：
                    <input type="text" size="30" name="customerName" value='<%=Request.QueryString["customerName"]%>' />
                    销售员：
                    <input type="text" size="30" name="salesMan" value='<%=Request.QueryString["salesMan"]%>' />
                    <input type="submit" class="search-btn" value="搜索" />
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <%if (this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_收款))
                  { %>
                <li><s class="shoukuan-pl"></s><a class="toolbar_plshoukuan" hidefocus="true" href="#">
                    <span>批量收款</span></a></li>
                <%} %>
                <li class="line"></li>
                <%if (this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_查看当日收款))
                  { %>
                <li><s class="duizhang"></s><a hidefocus="true" href="/SellCenter/Sell/SellIncome.aspx">
                    <span>当日收款对账</span></a></li>
                <%} %>
                <li class="line"></li>
                <li><s class="dayin"></s><a class="toolbar_dayin" hidefocus="true" href="#"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="toolbar_daochu" hidefocus="true" href="#"><span>
                    导出列表</span></a></li>
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
                        <th class="thinputbg" rowspan="2">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            团号
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            订单号
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            线路名称
                        </th>
                        <th align="center" class="th-line" rowspan="2" colspan="2">
                            人数
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            客源单位
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            销售员
                        </th>
                        <th align="center" class="th-line h20" colspan="2">
                            合同金额
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            已收
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            已收待审核
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            未收
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            操作
                        </th>
                    </tr>
                    <tr class="">
                        <th align="center" class="th-line nojiacu h20">
                            金额
                        </th>
                        <th align="center" class="th-line nojiacu h20">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class='<%# Container.ItemIndex%2==0?"":"odd" %>'>
                                <td align="center">
                                    <input type="checkbox" name="checkbox">
                                </td>
                                <td align="center">
                                    <%#Eval("TourCode")%>
                  
                                </td>
                                <td align="center">
                                    <%#Eval("OrderCode")%>
                                </td>
                                <td align="left">
                                    <a href="#">
                                        <%#Eval("RouteName")%></a>
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
                                <td align="left">
                                    <a class="example2" href="javascript:void(0);">
                                        <%#Eval("Customer")%></a> <span style="display: none">
                                            <%#Eval("Customer")%>
                                            <br />
                                            <%#Eval("Contact")%>
                                            <br />
                                            <%#Eval("Phone")%>
                                        </span>
                                </td>
                                <td align="center">
                                    <%#Eval("Salesman")%>
                                </td>
                                <td align="center">
                                    <b class="fontbsize12">
                                        <%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal( Eval("TotalAmount")))%></b>
                                </td>
                                <td align="center">
                                    <a id="link4" href="caiwu-querenbox.html">
                                        <%#Eval("CheckStatus")%></a>
                                </td>
                                <td align="center">
                                    <b class="fontblue">
                                        <%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("Received")))%></b>
                                </td>
                                <td align="center">
                                    <b class="fontgreen">
                                        <%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("UnChecked")))%></b>
                                </td>
                                <td align="center">
                                    <b class="fontbsize12 STYLE1">
                                        <%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("UnReceived")))%></b>
                                </td>
                                <td align="center">
                                    <%if (this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_收款))
                                      { %>
                                    <a class="linkPrice" href="javascript:void(0)" ref="1" val="<%#Eval("OrderId") %>">收款</a>|
                                    <%} %>
                                    <%if (this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_退款))
                                      { %>
                                    <a href="javascript:void(0);" ref="2" val="<%#Eval("OrderId") %>" class="linkPrice">
                                        退款</a>
                                    <%} %>
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
                <li class="line"></li>
                <%if (this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_收款))
                  { %>
                <li><s class="shoukuan-pl"></s><a class="toolbar_plshoukuan" hidefocus="true" href="#">
                    <span>批量收款</span></a></li>
                <%} %>
                <li class="line"></li>
                <li class="line"></li>
                <%if (this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_查看当日收款))
                  { %>
                <li><s class="duizhang"></s><a hidefocus="true" href="/SellCenter/Sell/SellIncome.aspx">
                    <span>当日收款对账</span></a></li>
                <%} %>
                <li class="line"></li>
                <li><s class="dayin"></s><a class="toolbar_dayin" hidefocus="true" href="#"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="toolbar_daochu" hidefocus="true" href="#"><span>
                    导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            tableToolbar.init({
                objectName: "订单",
                otherButtons: [{
                    button_selector: '.toolbar_plshoukuan',
                    sucessRulr: 2,
                    msg: '未选中任何行!',
                    buttonCallBack: SellIncome.GetID(objArr)
}]
                });

            });
    </script>

    <!--paopao start-->

    <script type="text/javascript">
        $(function() {
            $('.example2').bt({
                contentSelector: function() {
                    return $(this).next().html();
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
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            });
        });

        $("#link3").click(function() {
            var url = $(this).attr("href");
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "订单财务确认",
                modal: true,
                width: "450px",
                height: "207px"
            });
            return false;
        });
        $("#link4").click(function() {
            var url = $(this).attr("href");
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "订单财务确认",
                modal: true,
                width: "450px",
                height: "207px"
            });
            return false;
        });

        $(".linkPrice").click(function() {
            SellIncome.OpenSetMoney($(this).attr("val"), $(this).attr("ref"));
            return false;
        })
    </script>

    <!--paopao end-->

    <script type="text/javascript">

        var SellIncome = {
            OpenSetMoney: function(id, type) {
                var url = "";
                var title = "";
                //收款
                if (type == "1") {
                    url = '/FinanceManage/Common/SetMoney.aspx?sl=<%=Request.QueryString["sl"] %>&orderID=' + id;
                    title = "销售收款";
                } else {
                    //退款
                    url = '/FinanceManage/Common/ReturnMoney.aspx?sl=<%=Request.QueryString["sl"] %>&orderID=' + id;
                    title = "销售退款";
                }
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "",
                    modal: true,
                    width: "880px",
                    height: "323px"
                });
                return false;
            },
            GetID: function(objArr) {
                //ajax执行文件路径,默认为本页面
                var ajaxUrl = "XXXXX.aspx";
                //定义数组对象
                var list = new Array();
                //遍历按钮返回数组对象
                for (var i = 0; i < objArr.length; i++) {
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (objArr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(objArr[i].find("input[type='checkbox']").val());
                    }
                }
                this.OpenSetMoney(list.join(','), "1");
            }
        }
    </script>

</asp:Content>
