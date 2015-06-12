<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tourMoneyStatements.aspx.cs"
    Inherits="Web.CommonPage.tourMoneyStatements" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/ValiDatorForm.js"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

</head>
<body style="background: #e9f4f9;">
    <form id="form1" runat="server">
    <div>
        <div class="alertbox-outbox">
            <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        出团时间：
                    </td>
                    <td width="10%" align="left">
                        <asp:Literal ID="litLDate" runat="server"></asp:Literal>
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        线路名称：
                    </td>
                    <td width="30%">
                        <asp:Literal ID="litRouteName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        订单号：
                    </td>
                    <td align="left">
                        <asp:Literal ID="litorderCode" runat="server"></asp:Literal>
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        客户单位：
                    </td>
                    <td align="left">
                        <asp:Literal ID="litCompanyName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        联系人：
                    </td>
                    <td align="left">
                        <asp:Literal ID="litContectName" runat="server"></asp:Literal>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        联系电话：
                    </td>
                    <td align="left">
                        <asp:Literal ID="litContectPhone" runat="server"></asp:Literal>
                    </td>
                </tr>
                
                <!--团队价格体系 S-->
                <asp:PlaceHolder ID="TourQuoteView" runat="server" Visible="false">
                    <tr>
                        <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            整团：
                        </td>
                        <td colspan="3" align="left">
                            <asp:Literal ID="litServerStandard" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="TourQuoteView1" runat="server" Visible="false">
                    <tr>
                        <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            分项：
                        </td>
                        <td colspan="3" align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 3px;">
                                <tr bgcolor="#B7E0F3">
                                    <td height="25" align="center">
                                        项目
                                    </td>
                                    <td align="center">
                                        单项报价
                                    </td>
                                    <td align="center">
                                        服务标准
                                    </td>
                                </tr>
                                <asp:Repeater ID="repForeignQuotelist" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td height="25" align="center">
                                                <%# Eval("ServiceType").ToString() %>
                                            </td>
                                            <td align="center">
                                                <%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("Quote").ToString()))%>元/<%#Eval("Unit")%>
                                            </td>
                                            <td align="left">
                                                <%# Eval("ServiceStandard")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </asp:PlaceHolder>                
                <!--团队价格体系 E-->
                
                <!--价格明细 S-->                
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        价格：
                    </td>
                    <td colspan="3" align="left">
                        <asp:Literal runat="server" ID="ltrJiaGe"></asp:Literal>
                    </td>
                </tr>
                <!--价格明细 E-->
                
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        增加费用：
                    </td>
                    <td width="10%" align="left">
                        <asp:Literal ID="litAddMoney" runat="server"></asp:Literal>
                        元
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注：
                    </td>
                    <td width="30%">
                        <asp:Literal ID="litAddRemark" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        减少费用：
                    </td>
                    <td width="10%" align="left">
                        <asp:Literal ID="litlessenMoney" runat="server"></asp:Literal>
                        元
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注：
                    </td>
                    <td width="30%">
                        <asp:Literal ID="litlessenRemark" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        合计金额：
                    </td>
                    <td colspan="3" align="left">
                        <b>
                            <asp:HiddenField ID="hidAccountPrices" runat="server" />
                            <asp:Literal ID="litAccountPrices" runat="server"></asp:Literal></b> 元
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        订单备注：
                    </td>
                    <td colspan="3" align="left">
                        <asp:Literal runat="server" ID="ltrDingDanBeiZhu"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        内部信息：
                    </td>
                    <td colspan="3" align="left">
                        <asp:Literal runat="server" ID="ltrNeiBuXinXi"></asp:Literal>
                    </td>
                </tr>
                <asp:PlaceHolder runat="server" ID="phTuiKuan">
                    <tr>
                        <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            退款信息：
                        </td>
                        <td colspan="3" align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin: 3px;">
                                <tr bgcolor="#B7E0F3">
                                    <td width="30" height="25" align="center">
                                        序号
                                    </td>
                                    <td align="center">
                                        退款时间
                                    </td>
                                    <td align="center">
                                        退款金额
                                    </td>
                                    <td align="center">
                                        退款方式
                                    </td>
                                    <td align="center">
                                        备注
                                    </td>
                                </tr>
                                <asp:Repeater ID="repTourOrderSalesList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td height="25" align="center">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td align="center">
                                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("CollectionRefundDate"), ProviderToDate)%>
                                            </td>
                                            <td align="center">
                                                <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("CollectionRefundAmount"), ProviderToMoney)%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("CollectionRefundModeName")%>
                                            </td>
                                            <td align="center">
                                                <%# Eval("Memo")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        变更增加：
                    </td>
                    <td width="10%" align="left">
                        <asp:TextBox ID="txtChangeAddMoney" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        元
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注：
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="txtChangeRemark" runat="server" CssClass="inputtext formsize350"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        变更减少：
                    </td>
                    <td width="10%" align="left">
                        <asp:TextBox ID="txtChangelessonMoney" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        元
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注：
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="txtChangeRemarks" runat="server" CssClass="inputtext formsize350"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        订单确认金额：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtComfirmMoney" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        元
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        金额变更说明：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtchangeEsplain" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                            Style="height: 120px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        已支付金额：
                    </td>
                    <td align="left">
                        <asp:Literal ID="litpayMoney" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hidPayMoney" runat="server" />
                        元
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        尚欠款金额：
                    </td>
                    <td>
                        <asp:TextBox ID="txtDebtMoney" runat="server" CssClass="inputtext formsize40" Enabled="false"></asp:TextBox>
                        元
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        结算人：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtSettlementName" runat="server" CssClass="inputtext formsize80"
                            Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="alertbox-btn">
                <asp:Literal runat="server" id="ltrCaoZuoTiShi"></asp:Literal>
                <asp:Panel ID="pan_Save" runat="server" Style="display: inline" Visible="false">                    
                    <a href="javascript:void(0);" hidefocus="true" id="btnBaoCun"><s class="baochun"></s>保存</a>
                    <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>确认</a>
                </asp:Panel>
                
                <asp:PlaceHolder ID="ph_QuXiaoQueRen" runat="server" Visible="false">
                    <a href="javascript:void(0);"
                    hidefocus="true" id="i_a_quxiaoqueren">取消确认</a>
                </asp:PlaceHolder>
                
                
                <a href="javascript:" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();">
                    <s class="chongzhi"></s>关 闭</a>
            </div>
        </div>
        <asp:HiddenField ID="hidComfirmMoney" runat="server" />
    </div>
    </form>

    <script type="text/javascript">
        var StatementsPage = {
            _Save: function() {
                var tourId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>';
                var OrderId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderId") %>';
                $.newAjax({
                    type: "POST",
                    url: '/CommonPage/tourMoneyStatements.aspx?action=save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&tourId=' + tourId + "&OrderId=" + OrderId + "&confirm=1",
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                parent.window.location.reload();
                            });
                        } else {
                            parent.tableToolbar._showMsg(data.msg);
                            StatementsPage._BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        StatementsPage._BindBtn();
                    }
                });
            },
            _TotalPrices: function() {
                //合计金额 增加费用 减少费用  确认金额
                var accountPrices = $("#<%=hidAccountPrices.ClientID %>").val();
                var addMoney = $("input[type='text'][name='txtChangeAddMoney']").val();
                var lessenMoney = $("input[type='text'][name='txtChangelessonMoney']").val();
                $("#<%=txtComfirmMoney.ClientID %>").val(tableToolbar.calculate(tableToolbar.calculate(accountPrices, addMoney, "+"), lessenMoney, "-"));
                $("#<%=hidComfirmMoney.ClientID %>").val(tableToolbar.calculate(tableToolbar.calculate(accountPrices, addMoney, "+"), lessenMoney, "-"))
                var ComfirmMoney = $("#<%=txtComfirmMoney.ClientID %>").val();
                var hidPayMoney = $("#<%=hidPayMoney.ClientID %>").val();
                $("#<%=txtDebtMoney.ClientID %>").val(tableToolbar.calculate(ComfirmMoney, hidPayMoney, "-"));
            },
            _BindBtn: function() {
                $("input[type='text'][name='txtChangeAddMoney']").unbind("change").change(function() {
                    StatementsPage._TotalPrices();
                });
                $("input[type='text'][name='txtChangelessonMoney']").unbind("change").change(function() {
                    StatementsPage._TotalPrices();
                });
                $("#<%=txtComfirmMoney.ClientID %>").unbind("change").change(function() {
                    var hidPayMoney = $("#<%=hidPayMoney.ClientID %>").val();
                    $("#<%=txtDebtMoney.ClientID %>").val(tableToolbar.calculate($("input[type='text'][name='txtComfirmMoney']").val(), hidPayMoney, "-"));
                    $("#<%=hidComfirmMoney.ClientID %>").val($("#<%=txtComfirmMoney.ClientID %>").val());
                });
                $("#btnSave").text("确 认").unbind("click").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    parent.tableToolbar.ShowConfirmMsg("合同金额确认后将不能修改确认金额，你确定要确认吗？", function() {
                        $("#btnSave").text("处理中...").unbind("click").css("background-position", "0 -55px");
                        StatementsPage._Save();
                    });
                    return false;
                });

            },
            _PageInit: function() {
                this._BindBtn();
            }, baoCun: function(obj) {
                if (!confirm("此操作不会最终确认合同金额，仅保存信息，你确定要保存吗？")) return;
                var tourId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>';
                var OrderId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderId") %>';
                $(obj).text("保存中...").unbind("click");
                $.newAjax({
                    type: "POST", cache: false, data: $("#btnSave").closest("form").serialize(), dataType: "json",
                    url: '/CommonPage/tourMoneyStatements.aspx?action=save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&tourId=' + tourId + "&OrderId=" + OrderId + "&confirm=0",
                    success: function(data) {
                        if (data.result == "1") {
                            alert("保存成功!");
                            window.location.href = window.location.href;
                        } else {
                            alert("保存失败!");
                            $("#btnBaoCun").text("保存").click(function() { StatementsPage.baoCun(this); });
                        }
                    }
                });
            }, quXiaoQueRenHeTongJinE: function(obj) {
                if (!confirm("取消确认合同金额后，相应计划的状态将会回滚至销售未结算。\n你确定要取消确认合同金额吗？")) return;
                var _orderId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderId") %>';
                var _sl = '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';

                $(obj).text("处理中...").unbind("click");
                $.newAjax({
                    type: "POST", cache: false, dataType: "json",
                    url: '/CommonPage/tourMoneyStatements.aspx?action=QuXiaoQueRenHeTongJinE&sl=' + _sl + "&orderid=" + _orderId,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            window.location.href = window.location.href;
                        } else {
                            alert(response.msg);
                            $("#i_a_quxiaoqueren").text("取消确认").click(function() { StatementsPage.quXiaoQueRenHeTongJinE(this); });
                        }
                    }
                });
            }
        }
        $(document).ready(function() {
            StatementsPage._PageInit();
            $("#btnBaoCun").click(function() { StatementsPage.baoCun(this); });
            $("#i_a_quxiaoqueren").click(function() { StatementsPage.quXiaoQueRenHeTongJinE(this); });
        });
    </script>
</body>
</html>
