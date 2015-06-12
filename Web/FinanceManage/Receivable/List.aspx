<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.FinanceManage.Receivable.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    出团日期：
                <input value="<%= Request.QueryString["sDate"] %>" name="sDate" type="text" class="inputtext formsize80"
                    onfocus="WdatePicker();" />
                -
                <input type="text" value="<%= Request.QueryString["eDate"] %>" name="eDate" class="inputtext formsize80"
                    onfocus="WdatePicker();" />订单号：<input type="text" name="orderId" class="inputtext formsize120" value="<%=Request.QueryString["orderId"] %>" />
                    客户单位：<uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" selectfrist="false" />
                    销售员：<uc1:SellsSelect ID="txt_Seller" runat="server" selectfrist="false" />
                    下单人:<uc1:SellsSelect ID="txtXiaDanRen" runat="server" SetTitle="下单人" SelectFrist="false" /><br />
                    欠款：<uc3:CaiWuShaiXuan ID="CaiWuShaiXuan1" runat="server" />
                    已收待审：<uc3:CaiWuShaiXuan ID="CaiWuShaiXuan2" runat="server" />
                    合同金额确认状态：<select name="txtHeTongJinEQueRenStatus" id="txtHeTongJinEQueRenStatus"
                        class="inputselect">
                        <option selected="selected" value="">请选择</option>
                        <option value="1">已确认</option>
                        <option value="0">未确认</option>
                    </select>
                    收款人:<uc1:SellsSelect ID="txtShouKuanRen" runat="server" SetTitle="收款人" SelectFrist="false" /><br />
                    收款时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtShouKuanSTime"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtShouKuanSTime") %>" />-<input
                            type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtShouKuanETime"
                            value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtShouKuanETime") %>" />
                    线路名称：
                    <input type="text" class="inputtext formsize120" name="txtRouteName" size="28" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>" />
                    计调员:
                    <uc1:SellsSelect ID="txtJiDiaoYuan" runat="server" SetTitle="计调员" SelectFrist="false" />     
                    <input type="submit" id="submit_Select" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="isReceived" id="hd_isReceived" value="<%=Request.QueryString["isReceived"] %>" />
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div class="tablehead" style="background: none #f6f6f6;">
            <ul class="fixed">
                <li data-class="li_isReceived" data-type="-1"><s class="orderformicon"></s><a href="javascript:void(0);"
                    hidefocus="true" class="ztorderform"><span>应收账款</span></a></li>
                <li data-class="li_isReceived" data-type="1"><s class="orderformicon"></s><a href="javascript:void(0);"
                    hidefocus="true" class="ztorderform"><span>已结清账款</span></a></li>
            </ul>
        </div>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <asp:PlaceHolder ID="pan_plshoukuan" runat="server">
                    <li id="li_quantitySetMoney"><s class="shoukuan-pl"></s><a href="javascript:void(0);"
                        hidefocus="true" class="toolbar_plshoukuan"><span>批量收款</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_plshenhe" runat="server">
                    <li id="li_quantityExamineV"><s class="shenhe"></s><a href="#" hidefocus="true" class="toolbar_plshenhe">
                        <span>批量审核</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_plkaipiao" runat="server">
                    <li id="li_quantityOpenInvoice"><s class="kaipiao-pl"></s><a href="javascript:void(0);"
                        hidefocus="true" class="toolbar_plkaipiao"><span>批量开票</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="pan_DRSK" Visible="true">
                    <li><s class="duizhang"></s><a href="/MarketCenter/DangRiShouKuan.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"
                        hidefocus="true"><span>当日收款对账</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="dayin"></s><a href="javascript:void(0);" hidefocus="true" id="a_print"
                    class="toolbar_dayin"><span>打印列表</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0);" hidefocus="true" id="toolbar_daochu"
                    class="toolbar_daochu"><span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
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
                        订单号
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        线路名称
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        客源单位
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        销售员
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        下单人
                    </th>
                    <th colspan="2" align="center" class="th-line h20">
                        合同金额
                    </th>
                    <th rowspan="2" align="right" class="th-line">
                        已收
                    </th>
                    <th rowspan="2" align="right" class="th-line">
                        已收待审
                    </th>
                    <th rowspan="2" align="right" class="th-line">
                        欠款
                    </th>
                    <th rowspan="2" align="right" class="th-line">
                        已退
                    </th>
                    <th rowspan="2" align="right" class="th-line">
                        已退待审
                    </th>
                    <th rowspan="2" align="right" class="th-line">
                        开票金额
                    </th>
                    <th rowspan="2" align="center" class="th-line" style="width: 100px">
                        操作
                    </th>
                </tr>
                <tr>
                    <th align="right" class="th-line nojiacu h20">
                        金额
                    </th>
                    <th align="center" class="th-line nojiacu h20" style="width: 40px">
                        状态
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-orderid="<%#Eval("OrderId") %>" data-customerid="<%#Eval("CustomerId") %>"
                            data-customer="<%#Eval("Customer") %>" data-unreceived="<%#Eval("UnReceived") %>"
                            data-unchecked="<%#Eval("UnChecked") %>" data-received="<%#Eval("Received") %>"
                            data-receivable="<%#Eval("Receivable") %>" data-bill="<%#Eval("Bill") %>" data-tourid="<%#Eval("TourId") %>"
                            data-sellerid="<%#Eval("SalesmanId") %>" data-sellername="<%#Eval("Salesman") %>"
                            data-contact="<%#Eval("Contact")%>" data-phone="<%#Eval("Phone")%>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("OrderCode")%>
                            </td>
                            <td align="left">
                                <%#Eval("RouteName")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);" data-class="a_popo">
                                    <%#Eval("Customer")%></a><span style="display: none"><b><%#Eval("Customer")%></b><br />
                                        联系人：<%#Eval("Contact")%><br />
                                        联系方式：<%#Eval("Phone")%></span>
                            </td>
                            <td align="center">
                                <%#Eval("Salesman") %>
                            </td>
                            <td align="center">
                                <%#Eval("OperatorName") %>
                            </td>
                            <td align="right">
                                <b class="<%#((bool?)Eval("IsConfirmed")) ==true?"":"fontred" %>">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Receivable"), ProviderToMoney)%></b>
                            </td>
                            <td align="center" class="fonthei">
                                <a href="javascript:void(0);" data-tourid="<%# Eval("TourId") %>" data-class="<%#((int)Eval("TourType")).ToString()!="6"?"i_hetongquerenjine":"i_danxianghetongquerenjine" %>"
                                    data-tourtype="<%# (int)Eval("TourType") %>"><span class="<%#(bool)Eval("IsConfirmed")?"":"fontred" %>">
                                    <%#((bool?)Eval("IsConfirmed")) == true ? "已确认" : "未确认"%></span></a>
                            </td>
                            <td align="right" data-class="td_sum">
                                <b class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Received"), ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="right" data-class="td_sum">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("UnChecked"), ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="right" data-class="td_sum">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("UnReceived"), ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Returned"), ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="right">
                                <b class="fontbsize12 ">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("UnChkRtn"), ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Bill"), ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="center">
                                <a data-class="a_setMoney" href="javascript:void(0);">收款</a> | <a data-class="a_returnMoney"
                                    href="javascript:void(0);" class="<%#(decimal)Eval("UnChkRtn")>0?"fontred":"" %>">
                                    退款</a> 
                                <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_开票登记))
                                  { %>| <a data-class="a_openInvoice" href="javascript:void(0);">开票</a><%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="15">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server" Visible="false">
                    <tr class="odd">
                        <td align="right" colspan="6">
                            <strong>金额汇总：</strong>
                        </td>
                        <td align="right">
                            <b class="fontbsize12">
                                <asp:Label ID="lbl_totalSumPrice" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Label ID="lbl_totalReceived" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_totalUnchecked" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontbsize12 ">
                                <asp:Label ID="lbl_totalUnReceived" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_totalReturned" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontbsize12 ">
                                <asp:Label ID="lbl_totalUnChkReturn" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_totalBill" runat="server" Text="0"></asp:Label></b>
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var Receivable = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height,
                    draggable: true
                });
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/FinanceManage/Common",
                    title: "应收管理",
                    width: "800px",
                    height: "350px"
                }
            },
            ShowQuantitySetMoney: function(arrTr) {/*批量收款*/
                var data = this.DataBoxy();
                var arrOrderid = [];
                $(arrTr).each(function() {
                    arrOrderid.push($(this).attr("data-orderid"));
                })
                data.title += ("批量" + EnglishToChanges.Ping("SetMoney"));
                data.url += "/QuantitySetMoney.aspx?";
                data.url += $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', OrderId: arrOrderid.join(',') })
                data.width = "960px";
                this.ShowBoxy(data);
                return false;
            },
            ShowQuantityExamineV: function(arrTr) {/*批量审核*/
                var data = this.DataBoxy();
                var arrOrderid = [], floatReceived = 0/*已收*/, floatUnchecked = 0/*已收待审*/, floatUnreceived = 0/*欠款*/;
                $(arrTr).each(function() {
                    var obj = $(this);
                    floatReceived += (parseFloat(obj.attr("data-received")) || 0);
                    floatUnchecked += (parseFloat(obj.attr("data-unchecked")) || 0);
                    floatUnreceived += (parseFloat(obj.attr("data-unreceived")) || 0);
                    arrOrderid.push(obj.attr("data-orderid"));
                })
                data.title += ("批量" + EnglishToChanges.Ping("ExamineV"));
                data.url = "/FinanceManage/Receivable/QuantityExamineV.aspx?";
                data.url += $.param({
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    OrderId: arrOrderid.join(','),
                    sum: floatReceived + "|" + floatUnchecked + "|" + floatUnreceived
                })
                data.width = "960px";
                this.ShowBoxy(data);
                return false;
            },
            ShowQuantityOpenInvoice: function(arrTr) {/*批量开票*/
                var data = this.DataBoxy();
                var arrOrderid = [], floatReceived = 0/*已收*/, floatUnchecked = 0/*已收待审*/, floatUnreceived = 0/*欠款*/;
                $(arrTr).each(function() {
                    var obj = $(this);
                    floatReceived += (parseFloat(obj.attr("data-received")) || 0);
                    floatUnchecked += (parseFloat(obj.attr("data-unchecked")) || 0);
                    floatUnreceived += (parseFloat(obj.attr("data-unreceived")) || 0);
                    arrOrderid.push(obj.attr("data-orderid"));
                })
                data.title += ("批量" + EnglishToChanges.Ping("OpenInvoice"));
                data.url = "/FinanceManage/Common/QuantityOpenInvoice.aspx?";
                data.url += $.param({
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    OrderId: arrOrderid.join(','),
                    sum: floatReceived + "|" + floatUnchecked + "|" + floatUnreceived
                })
                data.width = "960px";
                this.ShowBoxy(data);
                return false;

            },
            Confirmed: function(thiss) {/*团队状态弹窗*/
                var data = this.DataBoxy();
                data.title += "--" + $(thiss).text();
                data.url = "/CommonPage/SelectTeamConfirmed.aspx";
                this.ShowBoxy(data);
                return false;
            },
            NoConfirmed: function(thiss) {/*散客状态弹窗*/
                var data = this.DataBoxy();
                data.title += "--" + $(thiss).text();
                data.url = "/CommonPage/SelectSection.aspx";
                this.ShowBoxy(data);
                return false;
            },
            BindClose: function() {/*重写收款退款弹窗右上角 关闭按钮*/
                $("a[data-class='a_close']").unbind().click(function() {
                    window.location = window.location;
                    return false;
                })
            },
            BindBtn: function() {/*绑定功能按钮*/
                var that = this;
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "订单",
                    otherButtons: [
                    {/*批量收款*/
                        button_selector: '#li_quantitySetMoney', //按钮选择器
                        sucessRulr: 2, //验证选择记录---1表示单选，2表示多选
                        msg: '未选中任何 订单 ', //验证未通过提示文本
                        buttonCallBack: function(arrTr) {//验证通过执行函数
                            that.ShowQuantitySetMoney(arrTr);
                        }
                    },
                    {/*批量审核*/
                        button_selector: '#li_quantityExamineV',
                        sucessRulr: 2,
                        msg: '未选中任何 订单 ',
                        buttonCallBack: function(arrTr) {
                            that.ShowQuantityExamineV(arrTr);
                        }
                    },
                    {/*批量开票*/
                        button_selector: '#li_quantityOpenInvoice',
                        sucessRulr: 2,
                        msg: '未选中任何 订单 ',
                        buttonCallBack: function(arrTr) {
                            that.ShowQuantityOpenInvoice(arrTr);
                        }
                    }
                    ]
                })
                var tab = $("#liststyle")
                //确认单
                tab.find("a[data-class='i_hetongquerenjine']").click(function() {
                    var obj = $(this);
                    var _tourType = obj.attr("data-tourtype");
                    var data = that.DataBoxy();
                    data.title = obj.html();
                    data.width = "820px";
                    data.height = "715px";
                    data.url = "/CommonPage/tourMoneyStatements.aspx?";
                    data.url += $.param({
                        tourType: _tourType,
                        OrderId: obj.closest("tr").attr("data-orderid"),
                        tourId: obj.attr("data-tourId"),
                        action: 3,
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                    });

                    that.ShowBoxy(data);
                    return false;
                });

                tab.find("a[data-class='i_danxianghetongquerenjine']").click(function() {
                    var _$obj = $(this);
                    var _url = "<%=PrintPage_DanXiangYeWuYouKeQueRenDan %>";
                    if (_url.length > 0 && _url != "javascript:void(0)") {
                        _$obj.attr("target", "_blank");
                        _$obj.attr("href", "<%=PrintPage_DanXiangYeWuYouKeQueRenDan %>?tourid=" + _$obj.attr("data-tourId"));
                    }
                });

                //收款
                tab.find("a[data-class='a_setMoney']").click(function() {
                    var tr = $(this).closest("tr");
                    //                    var arrSum = [];
                    //                    arrSum.push(parseFloat(tr.attr("data-received")) || 0);
                    //                    arrSum.push(parseFloat(tr.attr("data-unchecked")) || 0);
                    //                    arrSum.push(parseFloat(tr.attr("data-unreceived")) || 0);
                    var data = that.DataBoxy();
                    data.title += EnglishToChanges.Ping("SetMoney");
                    data.url += "/SetMoney.aspx?" + $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        OrderId: tr.attr("data-orderid"),
                        ReturnOrSet: 1,
                        ParentType: 1,
                        //                        Sum: arrSum.join('|'),
                        DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.订单收款%>',
                        tourId: tr.attr("data-tourid")
                    });
                    that.ShowBoxy(data);
                    that.BindClose();
                    return false;
                })
                //退款
                tab.find("a[data-class='a_returnMoney']").click(function() {
                    var tr = $(this).closest("tr");
                    //                    var arrSum = [];
                    //                    arrSum.push(parseFloat(tr.attr("data-received")) || 0);
                    //                    arrSum.push(parseFloat(tr.attr("data-unchecked")) || 0);
                    //                    arrSum.push(parseFloat(tr.attr("data-unreceived")) || 0);
                    var data = that.DataBoxy();
                    data.title += EnglishToChanges.Ping("ReturnMoney");
                    data.url += "/SetMoney.aspx?" + $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        OrderId: $(this).closest("tr").attr("data-orderid"),
                        ReturnOrSet: 2,
                        ParentType: 1,
                        //                        Sum: arrSum.join('|'),
                        DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.订单收款%>',
                        tourId: tr.attr("data-tourid")
                    });
                    that.ShowBoxy(data);
                    that.BindClose();
                    return false;
                })
                //开票
                tab.find("a[data-class='a_openInvoice']").click(function() {
                    var data = that.DataBoxy();
                    var tr = $(this).closest("tr");
                    data.title += EnglishToChanges.Ping("OpenInvoice");
                    data.url += "/OpenInvoice.aspx?" + $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        TourId: tr.attr("data-tourid"),
                        OrderId: tr.attr("data-orderid"),
                        ParentType: 1,
                        CustomerId: tr.attr("data-customerid"),
                        Customer: tr.attr("data-customer"),
                        SellerId: tr.attr("data-sellerId"),
                        SellerName: tr.attr("data-sellerName"),
                        Contact: tr.attr("data-contact"),
                        Phone: tr.attr("data-phone")
                    });
                    that.ShowBoxy(data);
                    return false;
                })
                //应收账款/已结清账款
                $("li[data-class='li_isReceived']").click(function() {
                    $("#hd_isReceived").val($.trim($(this).attr("data-type")));
                    $("#submit_Select").click();
                    return false;
                })
                $("#toolbar_daochu").click(function() {
                    toXls1();
                    return false;
                })
            },
            PageInit: function() {
                this.BindBtn();
                $("#a_print").click(function() {
                    PrintPage("#a_print");
                    return false;
                })
                $("a[data-class='a_popo']").bt({
                    contentSelector: function() {
                        return $(this).next("span");
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
                $("li[data-type='" + (Boxy.queryString("isReceived") || "-1") + "'] a").addClass("de-ztorderform")
                var caiWuShaiXuan = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan1.ClientID%>']);
                caiWuShaiXuan.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator) %>');
                caiWuShaiXuan.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber) %>');
                caiWuShaiXuan = new wuc.caiWuShaiXuan(window['<%=CaiWuShaiXuan2.ClientID%>']);
                caiWuShaiXuan.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator) %>');
                caiWuShaiXuan.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber) %>');
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            Receivable.PageInit();

            $("#txtHeTongJinEQueRenStatus").val('<%=EyouSoft.Common.Utils.GetQueryStringValue("txtHeTongJinEQueRenStatus") %>');
        })
    </script>

</asp:Content>
