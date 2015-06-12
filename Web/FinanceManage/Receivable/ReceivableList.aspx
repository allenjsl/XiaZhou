<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ReceivableList.aspx.cs" Inherits="Web.FinanceManage.Receivable.ReceivableList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="ReceivableList.aspx" accept="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="txt_teamNumber" value="<%=Request.QueryString["txt_teamNumber"] %>"
                        class="formsize120" />
                    订单号：<input type="text" name="txt_orderId" value="<%=Request.QueryString["txt_orderId"] %>"
                        class="formsize120" />
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" />
                    销售员：<uc1:SellsSelect ID="SellsSelect1" runat="server" />
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="javascript:;" hidefocus="true" class="ztorderform de-ztorderform">
                    <span>应收账款</span></a></li>
                <li><s class="orderformicon"></s><a href="SettleAccountsList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>已结清账款</span></a></li>
            </ul>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <%if (CheckGrant(Common.Enum.TravelPermission.账务管理_应收管理_收款登记))
                  {%><li><s class="shoukuan-pl"></s><a href="javascript:;" hidefocus="true" class="toolbar_plshoukuan">
                      <span>批量收款</span></a></li>
                <li class="line"></li>
                <%} %>
                <li><s class="duizhang"></s><a href="../组团团队/drsk-duizhang.html" hidefocus="true"><span>
                    当日收款对账</span></a></li>
                <li class="line"></li>
                <li><s class="dayin"></s><a href="javascript:;" hidefocus="true" class="toolbar_dayin">
                    <span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:;" hidefocus="true" class="toolbar_daochu">
                    <span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th rowspan="2" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        团号
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        订单号
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        线路名称
                    </th>
                    <th colspan="2" rowspan="2" align="center" class="th-line">
                        人数
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        客源单位
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        销售员
                    </th>
                    <th colspan="2" align="center" class="th-line h20">
                        合同金额
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        已收
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        已收待审核
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        未收
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line nojiacu h20">
                        金额
                    </th>
                    <th align="center" class="th-line nojiacu h20">
                        状态
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
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
                                <img src="/images/chengren.gif" alt="成人" />
                                <b class="fontblue">
                                    <%#Eval("Adults")%></b>
                            </td>
                            <td align="center">
                                <img src="/images/child.gif" alt="儿童" />
                                <b class="fontgreen">
                                    <%#Eval("Childs")%></b>
                            </td>
                            <td align="left">
                                <a href="javascript:;" onmouseover="Bt(this)" linkname="<%#Eval("Contact") %>" linktel="<%#Eval("Phone") %>">
                                    <%#Eval("Customer") %></a>
                            </td>
                            <td align="center">
                                <%#Eval("Salesman") %>
                            </td>
                            <td align="center">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TotalAmount").ToString()) %></b>
                            </td>
                            <td align="center" class="<%#((bool?)Eval("IsConfirmed")) ==true?"fontred":"" %>">
                                <a href="#">
                                    <%#((bool?)Eval("IsConfirmed")) == true ? "已确认" : "未确认"%><%#Eval("TourType")%></a>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%#Eval("Received")%></b>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#Eval("UnChecked")%></b>
                            </td>
                            <td align="center">
                                <b class="fontbsize12 STYLE1">
                                    <%#Eval("UnReceived")%></b>
                            </td>
                            <td align="center">
                                <a href="javascript:;" onclick="ShowSetMoney()">收款</a> | <a href="javascript:;" onclick="ShowReturnMoney()">
                                    退款</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        HHL5-0001-0011
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="left">
                        <a href="#">海口进出五天四晚休闲之旅</a>
                    </td>
                    <td align="center">
                        <img src="/images/chengren.gif" alt="成人" />
                        <b class="fontblue">15</b>
                    </td>
                    <td align="center">
                        <img src="/images/child.gif" alt="儿童" />
                        <b class="fontgreen">5</b>
                    </td>
                    <td align="left">
                        <a href="javascript:;" onmouseover="Bt(this)" linkname="某某帅哥" linktel="0577-7758258">
                            湖北峡州国</a>
                    </td>
                    <td align="center">
                        李四
                    </td>
                    <td align="center">
                        <b class="fontbsize12">13520</b>
                    </td>
                    <td align="center" class="fontred">
                        <a href="#">已确认团队</a>
                    </td>
                    <td align="center">
                        <b class="fontblue">8000</b>
                    </td>
                    <td align="center">
                        <b class="fontgreen">2400</b>
                    </td>
                    <td align="center">
                        <b class="fontbsize12 STYLE1">3000</b>
                    </td>
                    <td align="center">
                        <a href="javascript:;" onclick="ShowSetMoney()">收款</a> | <a href="javascript:;" onclick="ShowReturnMoney()">
                            退款</a>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox1" />
                    </td>
                    <td align="center">
                        HHL5-0001-0011
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="left">
                        <a href="#">海口进出五天四晚休闲之旅</a>
                    </td>
                    <td align="center">
                        <img src="/images/chengren.gif" alt="成人" />
                        <b class="fontblue">15</b>
                    </td>
                    <td align="center">
                        <img src="/images/child.gif" alt="儿童" />
                        <b class="fontgreen">5</b>
                    </td>
                    <td align="left">
                        <a href="javascript:;" onmouseover="Bt(this)" linkname="某某帅哥" linktel="0577-7758258">
                            湖北峡州国</a>
                    </td>
                    <td align="center">
                        李四
                    </td>
                    <td align="center">
                        <b class="fontbsize12">13520</b>
                    </td>
                    <td align="center">
                        <a href="#">未确认散客</a>
                    </td>
                    <td align="center">
                        <b class="fontblue">8000</b>
                    </td>
                    <td align="center">
                        <b class="fontgreen">2400</b>
                    </td>
                    <td align="center">
                        <b class="fontbsize12 STYLE1">3000</b>
                    </td>
                    <td align="center">
                        <a href="javascript:;" onclick="ShowSetMoney()">收款</a> | <a href="javascript:;" onclick="ShowReturnMoney()">
                            退款</a>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox2" />
                    </td>
                    <td align="center">
                        HHL5-0001-0011
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="left">
                        <a href="#">海口进出五天四晚休闲之旅</a>
                    </td>
                    <td align="center">
                        <img src="/images/chengren.gif" alt="成人" />
                        <b class="fontblue">15</b>
                    </td>
                    <td align="center">
                        <img src="/images/child.gif" alt="儿童" />
                        <b class="fontgreen">5</b>
                    </td>
                    <td align="left">
                        <a href="javascript:;" onmouseover="Bt(this)" linkname="某某帅哥" linktel="0577-7758258">
                            湖北峡州国</a>
                    </td>
                    <td align="center">
                        李四
                    </td>
                    <td align="center">
                        <b class="fontbsize12">13520</b>
                    </td>
                    <td align="center" class="fontred">
                        <a href="#">已确认散客</a>
                    </td>
                    <td align="center">
                        <b class="fontblue">8000</b>
                    </td>
                    <td align="center">
                        <b class="fontgreen">2400</b>
                    </td>
                    <td align="center">
                        <b class="fontbsize12 STYLE1">3000</b>
                    </td>
                    <td align="center">
                        <a href="javascript:;" onclick="ShowSetMoney()">收款</a> | <a href="javascript:;" onclick="ShowReturnMoney()">
                            退款</a>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox3" />
                    </td>
                    <td align="center">
                        HHL5-0001-0011
                    </td>
                    <td align="center">
                        JUY5000010001-01
                    </td>
                    <td align="left">
                        <a href="#">海口进出五天四晚休闲之旅</a>
                    </td>
                    <td align="center">
                        <img src="/images/chengren.gif" alt="成人" />
                        <b class="fontblue">15</b>
                    </td>
                    <td align="center">
                        <img src="/images/child.gif" alt="儿童" />
                        <b class="fontgreen">5</b>
                    </td>
                    <td align="left">
                        <a href="javascript:;" onmouseover="Bt(this)" linkname="某某帅哥" linktel="0577-7758258">
                            湖北峡州国</a>
                    </td>
                    <td align="center">
                        李四
                    </td>
                    <td align="center">
                        <b class="fontbsize12">13520</b>
                    </td>
                    <td align="center">
                        <a href="#">未确认团队</a>
                    </td>
                    <td align="center">
                        <b class="fontblue">8000</b>
                    </td>
                    <td align="center">
                        <b class="fontgreen">2400</b>
                    </td>
                    <td align="center">
                        <b class="fontbsize12 STYLE1">3000</b>
                    </td>
                    <td align="center">
                        <a href="javascript:;" onclick="ShowSetMoney()">收款</a> | <a href="javascript:;" onclick="ShowReturnMoney()">
                            退款</a>
                    </td>
                </tr>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
            <form id="form1" runat="server" style="text-align: center">
            <asp:Label ID="lbl_msg" runat="server" Text=""></asp:Label>
            </form>
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            //绑定功能按钮
            BindBtn();
            $("")
        })
        //绑定功能按钮
        function BindBtn() {
            tableToolbar.init({
                tableContainerSelector: "#liststyle", //表格选择器
                objectName: "记录"
            })
        }
        //收款
        function ShowSetMoney() {
            var data = new dataBoxy();
            data.title += Ping("SetMoney");
            data.url += "/SetMoney.aspx";
            ShowBoxy(data);
            return false;
        }
        //退款
        function ShowReturnMoney() {
            var data = new dataBoxy();
            data.title += Ping("ReturnMoney");
            data.url += "/ReturnMoney.aspx";
            ShowBoxy(data);
            return false;

        }
        //团队状态弹窗
        function Confirmed(thiss) {
            var data = new dataBoxy();
            data.title += "--" + $(thiss).text();
            data.url = "/CommonPage/SelectTeamConfirmed.aspx";
            ShowBoxy(data);
            return false;
        }
        //散客状态弹窗
        function NoConfirmed(thiss) {
            var data = new dataBoxy();
            data.title += "--" + $(thiss).text();
            data.url = "/CommonPage/SelectSection.aspx";
            ShowBoxy(data);
            return false;

        }
        //泡泡提示
        function Bt(thiss) {
            $(".bt-wrapper").html("");
            //公司名称
            var firmName = $(thiss).text();
            //联系人姓名
            var linkName = $(thiss).attr("linkName");
            //联系电话
            var linkTel = $(thiss).attr("linkTel");
            //拼接提示文本
            var cont = "<b>" + firmName + "</b><br/>";
            cont += "联系人：" + linkName + "<br/>";
            cont += "联系方式：" + linkTel;
            $(thiss).bt({
                contentSelector: function() {
                    return cont;
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
        }
        //显示弹窗
        function ShowBoxy(data) {
            Boxy.iframeDialog({
                iframeUrl: data.url,
                title: data.title,
                modal: true,
                width: data.width,
                height: data.height
            });
        }
        //弹窗默认参数
        function dataBoxy() {
            this.url = "/FinanceManage/Common";
            this.title = "应收管理";
            this.width = "900px";
            this.height = "350px";
        };
    </script>

</asp:Content>
