<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoShouShouKuan.aspx.cs"
    Inherits="Web.MarketCenter.XiaoShouShouKuan" MasterPageFile="~/MasterPage/Front.Master" %>

<%--销售中心-销售收款--%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc3" %>

<asp:Content ID="MainBodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form action="XiaoShouShouKuan.aspx" method="get">
            <input type="hidden" name="sl" id="sl" value="<%=SL %>" />
            <span class="searchT">
                <p>
                    出团日期：<input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtLSDate"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLSDate") %>" />-<input type="text"
                            onfocus="WdatePicker()" class="inputtext formsize80" name="txtLEDate" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtLEDate") %>" />
                    订单号：<input type="text" name="txtOrderCode" class="inputtext formsize100" value="<%=Utils.GetQueryStringValue("txtOrderCode") %>" />
                    客户单位：<uc1:CustomerUnitSelect ID="txtKeHuDanWei" runat="server" SelectFrist="false" />
                    销售员：<uc2:SellsSelect ID="txtXiaoShouYuan" runat="server" SelectFrist="false" />
                    下单人:<uc2:SellsSelect ID="txtXiaDanRen" runat="server" SetTitle="下单人" SelectFrist="false" />
                    <br />                    
                    欠款：<uc3:caiwushaixuan id="txtQianKuan" runat="server" />
                    已收待审：<uc3:caiwushaixuan id="txtYiShouDaiShen" runat="server" />   
                    合同金额确认状态：<select name="txtHeTongJinEQueRenStatus" id="txtHeTongJinEQueRenStatus"
                        class="inputselect">
                        <option selected="selected" value="">请选择</option>
                        <option value="1">已确认</option>
                        <option value="0">未确认</option>
                    </select>                    
                    收款人:<uc2:SellsSelect ID="txtShouKuanRen" runat="server" SetTitle="收款人" SelectFrist="false" />
                    收款状态：<select name="txtShouKuanStatus" id="txtShouKuanStatus" class="inputselect">
                        <option selected="selected" value="">请选择</option>
                        <option value="1">已结清</option>
                        <option value="0">未结清</option>
                    </select><br />
                    收款时间：<input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtShouKuanSTime"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtShouKuanSTime") %>" />-<input
                            type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtShouKuanETime"
                            value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtShouKuanETime") %>" />   
                    线路名称：
                    <input type="text" class="inputtext formsize120" name="txtRouteName" size="28" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>" />
                    计调员:
                    <uc2:SellsSelect ID="txtJiDiaoYuan" runat="server" SetTitle="计调员" SelectFrist="false" />                 
                    <button type="submit" class="search-btn">
                        搜索</button>
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="phdPLSK" Visible="true">
                    <li><s class="shoukuan-pl"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_plshoukuan"
                        id="selector_toolbar_PiLiangShouKuan"><span>批量收款</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phdPLKP" Visible="true">
                    <li><s class="kaipiao-pl"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_plkaipiao"
                        id="selector_toolbar_PiLiangKaiPiao"><span>批量开票</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phdDRDZD" Visible="true">
                    <li><s class="duizhang"></s><a href="DangRiShouKuan.aspx?sl=<%=SL %>"
                        hidefocus="true"><span>当日收款对账</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="dayin"></s><a href="javascript:void(0);" id="a_print" class="toolbar_dayin">
                    <span>打印列表</span> </a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="###" hidefocus="true" class="toolbar_daochu" id="selector_toolbar_toXls">
                    <span>导出列表</span></a></li>
                <li><s class="daochu"></s><a href="javascript:void(0)" hidefocus="true" id="i_a_daishenshoukuanxx"><span>待审明细</span></a></li>
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
                    <th rowspan="2" class="thinputbg">
                        <input type="checkbox" name="checkbox" />
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        团号-订单号
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        线路名称
                    </th>
                    <th rowspan="2" align="center" class="th-line">
                        人数
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
                    <th rowspan="2" align="center" class="th-line">
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
                    <th align="center" class="th-line nojiacu h20">
                        金额
                    </th>
                    <th align="center" class="th-line nojiacu h20" style="width: 40px;">
                        状态
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr data-customerid="<%#Eval("CustomerId") %>" data-customer="<%#Eval("Customer") %>"
                            data-unreceived="<%#Eval("UnReceived") %>" data-unchecked="<%#Eval("UnChecked") %>"
                            data-received="<%#Eval("Received") %>" data-tourid='<%#Eval("TourId") %>'>
                            <td align="center">
                                <input type="checkbox" name="checkbox" value="<%#Eval("OrderId") %>" />
                                <input type="hidden" name="ItemUserID" value="" />
                            </td>
                            <td align="center">
                                <%#Eval("OrderCode")%>
                            </td>
                            <td align="left">
                                <a href='<%#PrintPages %>?tourid=<%#Eval("TourId") %>' target="_blank">
                                    <%#Eval("RouteName") %></a>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%#Eval("Adults")%></b><sup class="fontred">+<%#Eval("Childs")%></sup>
                            </td>
                            <td align="left">
                                <a href="###" id="example2" class="a_info" href="javascript:void(0);">
                                    <%#Eval("Customer")%></a> <span style="display: none;"><b>
                                        <%#Eval("Customer")%></b><br />
                                        联系人：<%#Eval("Contact")%><br />
                                        联系方式：<%#Eval("Phone")%>
                                    </span>
                            </td>
                            <td align="center">
                                <input type="hidden" value="<%#Eval("SalesmanId")%>" name="hideSalesId" />
                                <span data-name="spanSalesMan">
                                    <%#Eval("Salesman")%></span>
                            </td>
                            <td style="text-align:center">
                                <%#Eval("OperatorName")%>
                            </td>
                            <td align="right">
                                <!--确认金额-->
                                <b class="<%#(bool)Eval("IsConfirmed")?"":"fontred" %>">
                                    <%#UtilsCommons.GetMoneyString(Eval("Receivable"),ProviderToMoney)%></b>
                            </td>
                            <td align="center" class="fonthei">
                                <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_确认合同金额))
                                  { %>
                                <% } %>
                                <a href="javascript:void(0);" data-title="<%#(bool)Eval("IsConfirmed")?"已确认":"未确认"%><%#Eval("TourType")%>"
                                    class="<%#Eval("TourType").ToString()!="单项服务"?"selector_trtoolbar_QueRen":"i_danxianghetongquerenjine" %>"
                                    data-class="a_confirmation" data-tourtype="<%#(int)Eval("TourType") %>" data-tourid="<%# Eval("TourId") %>">
                                    <span class="<%#(bool)Eval("IsConfirmed")?"":"fontred" %>">
                                        <%#(bool)Eval("IsConfirmed")?"已确认":"未确认"%></span> </a>
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
                            <%--<td align="right">
                                <!--已收金额-->
                                <b>
                                    <%#UtilsCommons.GetMoneyString(Eval("Received"),ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <!--已收待审金额-->
                                <b class="fontred">
                                    <%#UtilsCommons.GetMoneyString(Eval("UnChecked"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <!--欠款金额-->
                                <b>
                                    <%#UtilsCommons.GetMoneyString((decimal)Eval("Receivable") - (decimal)Eval("Received"),ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <!--已退金额-->
                                <b>
                                    <%#UtilsCommons.GetMoneyString(Eval("Returned"),ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <!--已退待审金额-->
                                <b class="fontred ">
                                    <%#UtilsCommons.GetMoneyString(Eval("UnChkRtn"),ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <!--开票金额-->
                                <b class="fontgreen">
                                    <%#UtilsCommons.GetMoneyString(Eval("Bill"), ProviderToMoney)%></b>
                            </td>--%>
                            <td align="center">
                                <%--<%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_收款登记))
                                  { %><a href="###" class="selector_trtoolbar_ShouKuanDengJi">收款</a> |
                                <% } %>
                                
                                <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_退款登记))
                                  { %><a href="###" class="<%#(decimal)Eval("UnChkRtn")!=0?"fontred":"" %> selector_trtoolbar_TuiKuanDengJi">
                                      退款</a> |
                                <% } %>--%>
                                <a href="###" class="selector_trtoolbar_ShouKuanDengJi">收款</a> | <a href="###" class="<%#(decimal)Eval("UnChkRtn")!=0?"fontred":"" %> selector_trtoolbar_TuiKuanDengJi">
                                    退款</a>
                                <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_开票登记))
                                  { %>
                                | <a href="###" class="selector_trtoolbar_KaiPiaoDengJi">开票</a><% } %>
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

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>
    <script type="text/javascript">
        var xiaoShouShouKuan = {
            BindClose: function() {/*重写收款退款弹窗右上角 关闭按钮*/
                $("a[data-class='a_close']").unbind().click(function() {
                    window.location.reload();

                    return false;
                })
            },
            winParams: {}
            //初始化查询
            , initSearch: function() {
                //收款状态
                var _txtShouKuanStatus = '<%=Utils.GetQueryStringValue("txtShouKuanStatus") %>';
                $("#txtShouKuanStatus").val(_txtShouKuanStatus);

                //销售员
                $("#<%=txtXiaoShouYuan.SellsIDClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient) %>');
                $("#<%=txtXiaoShouYuan.SellsNameClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient) %>');

                //客户单位
                window["<%=txtKeHuDanWei.ClientID %>"].SetVal({ "CustomerUnitName": '<%=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHMC) %>', "CustomerUnitId": "<%=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHBH) %>", "CustomerUnitType": "<%=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHLX) %>" });

                //下单人
                $("#<%=txtXiaDanRen.SellsIDClient %>").val('<%=Utils.GetQueryStringValue(txtXiaDanRen.SellsIDClient) %>');
                $("#<%=txtXiaDanRen.SellsNameClient %>").val('<%=Utils.GetQueryStringValue(txtXiaDanRen.SellsNameClient) %>');

                //收款人
                $("#<%=txtShouKuanRen.SellsIDClient %>").val('<%=Utils.GetQueryStringValue(txtShouKuanRen.SellsIDClient) %>');
                $("#<%=txtShouKuanRen.SellsNameClient %>").val('<%=Utils.GetQueryStringValue(txtShouKuanRen.SellsNameClient) %>');

                var caiWuShaiXuan = new wuc.caiWuShaiXuan(window['<%=txtQianKuan.ClientID%>']);
                caiWuShaiXuan.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(txtQianKuan.ClientUniqueIDOperator) %>');
                caiWuShaiXuan.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(txtQianKuan.ClientUniqueIDOperatorNumber) %>');
                caiWuShaiXuan = new wuc.caiWuShaiXuan(window['<%=txtYiShouDaiShen.ClientID%>']);
                caiWuShaiXuan.setOperator('<%=EyouSoft.Common.Utils.GetQueryStringValue(txtYiShouDaiShen.ClientUniqueIDOperator) %>');
                caiWuShaiXuan.setOperatorNumber('<%=EyouSoft.Common.Utils.GetQueryStringValue(txtYiShouDaiShen.ClientUniqueIDOperatorNumber) %>');

                $("#txtHeTongJinEQueRenStatus").val('<%=Utils.GetQueryStringValue("txtHeTongJinEQueRenStatus") %>');
            }
            //初始化工具栏
            , initToolbar: function() {
                var _options = { otherButtons: [] }
                var _self = this;

                var _buttonPiLiangShouKuan = { button_selector: '#selector_toolbar_PiLiangShouKuan'
                    , sucessRulr: 2
                    , msg: '未选中任何需要批量收款登记的数据'
                    , buttonCallBack: function(objs) {
                        var _params = { sl: "<%=SL %>", OrderId: [] };
                        $(objs).each(function() {
                            _params.OrderId.push($(this).find(":checkbox").val());
                        });
                        Boxy.iframeDialog({ iframeUrl: Boxy.createUri("/FinanceManage/Common/QuantitySetMoney.aspx", _params)
                            , title: "批量收款登记"
                            , modal: true
                            , width: "900px"
                            , height: "350px"
                        });
                        return false;
                    }
                };

                var _buttonPiLiangKaiPiao = { button_selector: '#selector_toolbar_PiLiangKaiPiao'
                    , sucessRulr: 2
                    , msg: '未选中任何需要批量开票登记的数据'
                    , buttonCallBack: function(objs) {
                        var arrOrderid = [], floatReceived = 0/*已收*/, floatUnchecked = 0/*已收待审*/, floatUnreceived = 0/*欠款*/;
                        $(objs).each(function() {
                            arrOrderid.push($(this).find(":checkbox").val());
                            floatReceived += (parseFloat($(this).attr("data-received")) || 0);
                            floatUnchecked += (parseFloat($(this).attr("data-unchecked")) || 0);
                            floatUnreceived += (parseFloat($(this).attr("data-unreceived")) || 0);
                        });
                        var _params = { sl: "<%=SL %>", OrderId: arrOrderid.join(','), sum: floatReceived + "|" + floatUnchecked + "|" + floatUnreceived };

                        Boxy.iframeDialog({ iframeUrl: Boxy.createUri("/FinanceManage/Common/QuantityOpenInvoice.aspx", _params)
                            , title: "批量开票登记"
                            , modal: true
                            , width: "960px"
                            , height: "350px"
                        });
                    }
                };

                var _buttonDaiShenShouKuanXX = { button_selector: '#i_a_daishenshoukuanxx'
                    , sucessRulr: 2
                    , msg: '未选中任何待审收款信息'
                    , buttonCallBack: function(objs) {
                        var _params = { sl: "<%=SL %>", OrderId: [] };
                        $(objs).each(function() {
                            _params.OrderId.push($(this).find(":checkbox").val());
                        });
                        Boxy.iframeDialog({ iframeUrl: Boxy.createUri("DaiShenShouKuanXX.aspx", _params)
                            , title: "待审核收款明细"
                            , modal: true
                            , width: "900px"
                            , height: "350px"
                        });
                        return false;
                    }
                };

                _options.otherButtons.push(_buttonPiLiangShouKuan);
                _options.otherButtons.push(_buttonPiLiangKaiPiao);
                _options.otherButtons.push(_buttonDaiShenShouKuanXX);

                tableToolbar.init(_options);

                //init toxls
                toXls.init({ "selector": "#selector_toolbar_toXls" });

            }
            //init list tr toolbar
            , initTrToolbar: function() {
                var _self = this;
                $(".selector_trtoolbar_QueRen").bind("click", function() {
                    var _params = { sl: "<%=SL %>", OrderId: $(this).parents("tr").find(":checkbox").val(), tourType: $(this).attr("data-tourtype"), action: 1, tourId: $(this).attr("data-tourId") }; Boxy.iframeDialog({ iframeUrl: Boxy.createUri("/CommonPage/tourMoneyStatements.aspx", _params)
                        , title: $(this).attr("data-title")
                        , modal: true
                        , width: "820px"
                        , height: "715px"
                    });
                });

                $(".selector_trtoolbar_ShouKuanDengJi").bind("click", function() {
                    var _params = { sl: "<%=SL %>", orderid: $(this).parents("tr").find(":checkbox").val(), ReturnOrSet: 1, ParentType: 2 };
                    Boxy.iframeDialog({ iframeUrl: Boxy.createUri("/FinanceManage/Common/SetMoney.aspx", _params)
                        , title: "收款登记"
                        , modal: true
                        , width: "900px"
                        , height: "350px"
                    });
                    _self.BindClose();
                });

                $(".selector_trtoolbar_TuiKuanDengJi").bind("click", function() {
                    var _params = { sl: "<%=SL %>", orderid: $(this).parents("tr").find(":checkbox").val(), ReturnOrSet: 2, ParentType: 2 };
                    Boxy.iframeDialog({ iframeUrl: Boxy.createUri("/FinanceManage/Common/SetMoney.aspx", _params)
                        , title: "退款登记"
                        , modal: true
                        , width: "900px"
                        , height: "350px"
                    });
                    _self.BindClose();
                });

                $(".selector_trtoolbar_KaiPiaoDengJi").bind("click", function() {
                    var oTr = $(this).closest("tr");
                    var _params = {
                        sl: "<%=SL %>",
                        TourId: oTr.attr("data-tourid"),
                        orderid: oTr.find(":checkbox").val(),
                        CustomerId: oTr.attr("data-customerid"),
                        Customer: encodeURIComponent(oTr.attr("data-customer")),
                        SellerId: oTr.find("input[name='hideSalesId']").val(),
                        SellerName: encodeURIComponent($.trim(oTr.find("span[data-name='spanSalesMan']").html())),
                        ParentType: 2
                    };
                    Boxy.iframeDialog({ iframeUrl: Boxy.createUri("/FinanceManage/Common/OpenInvoice.aspx", _params)
                        , title: "开票登记"
                        , modal: true
                        , width: "900px"
                        , height: "350px"
                    });
                });

                $(".i_danxianghetongquerenjine").each(function() {
                    var _$obj = $(this);

                    var _url = "<%=PrintPage_DanXiangYeWuYouKeQueRenDan %>";
                    if (_url.length > 0 && _url != "javascript:void(0)") {
                        _$obj.attr("target", "_blank");
                        _$obj.attr("href", "<%=PrintPage_DanXiangYeWuYouKeQueRenDan %>?tourid=" + _$obj.attr("data-tourid"));
                    }
                });
            }
            , init: function() {
                this.winParams = Boxy.getUrlParams();
            }
        };

        $(document).ready(function() {
            xiaoShouShouKuan.init();
            //init search
            xiaoShouShouKuan.initSearch();
            //init toolbar
            xiaoShouShouKuan.initToolbar();
            //init initTrToolbar
            xiaoShouShouKuan.initTrToolbar();
            //clone toolbar、paging
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            $('.a_info').bt({
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
            //print
            $("#a_print").click(function() {
                PrintPage("#a_print");
                return false;
            })
        });
    </script>

</asp:Content>
