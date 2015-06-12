<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderExpenceInfo.aspx.cs"
    Inherits="Web.OperaterCenter.OrderExpenceInfo" %>

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
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单号：
                </td>
                <td width="20%" align="left">
                    <asp:Literal ID="litOrderCode" runat="server"></asp:Literal>
                </td>
                <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="fontred">*</span>客源单位：
                </td>
                <td width="20%">
                    <asp:Literal ID="litBuyCompany" runat="server"></asp:Literal>
                </td>
                <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    联系人：
                </td>
                <td width="19%">
                    <asp:Literal ID="litContectName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>订单销售员：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:Literal ID="litOrderSellers" runat="server"></asp:Literal>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    下单人：
                </td>
                <td height="28" colspan="3" bgcolor="#e0e9ef">
                    <asp:Literal ID="litOperator" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>价格组成：
                </td>
                <td colspan="5">
                    成人单价 <strong>
                        <asp:Literal ID="litAdultPrices" runat="server"></asp:Literal></strong> * 成人数
                    <strong>
                        <asp:Literal ID="litAdultNums" runat="server"></asp:Literal></strong> + 儿童单价
                    <strong>
                        <asp:Literal ID="litChildPrices" runat="server"></asp:Literal></strong> * 儿童数
                    <strong>
                        <asp:Literal ID="litChildNums" runat="server"></asp:Literal></strong>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    增加费用：
                </td>
                <td colspan="5" bgcolor="#e0e9ef">
                    <asp:Literal ID="litAddMoney" runat="server"></asp:Literal>
                    <span style="margin-left: 80px;">备注:<asp:Literal ID="litAddRemark" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    减少费用：
                </td>
                <td colspan="5">
                    <asp:Literal ID="litLessMoney" runat="server"></asp:Literal><span style="margin-left: 80px;">备注：
                        <asp:Literal ID="litLessRemark" runat="server"></asp:Literal>
                    </span>
                </td>
            </tr>
            <tr bgcolor="#E0E9EF">
                <td width="11%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="fontred">*</span>合计金额：
                </td>
                <td>
                    <asp:Literal ID="litSumMoney" runat="server"></asp:Literal>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    销售应收：
                </td>
                <td>
                    <asp:Literal ID="litSellerRe" runat="server"></asp:Literal>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    导游现收：
                </td>
                <td>
                    <asp:Literal ID="litGuidRe" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单备注：
                </td>
                <td colspan="5">
                    <asp:Literal ID="litOrderRemark" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">游客信息</span>
            <table width="99%" border="0" cellspacing="0" cellpadding="0" style="height: auto;
                zoom: 1; overflow: hidden;">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        姓名
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        类型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        证件类型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        证件号码
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        性别
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        联系方式
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        保险
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        短信通知
                    </td>
                </tr>
                <asp:Repeater ID="repCusomerList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center">
                                <%# Eval("CnName")%>
                            </td>
                            <td align="center">
                                <%# Eval("VisitorType").ToString()%>
                            </td>
                            <td align="center">
                                <%# Eval("CardType").ToString()%>
                            </td>
                            <td align="center">
                                <%# Eval("CardNumber")%>
                            </td>
                            <td align="center">
                                <%# Eval("Gender").ToString()%>
                            </td>
                            <td align="center">
                                <%# Eval("Contact")%>
                            </td>
                            <td align="center">
                                <%# (bool)Eval("IsInsurance") == true ? "<img src=\"/images/y-duihao.gif\" width=\"13\" height=\"9\" />":"<img src=\"/images/y-cuohao.gif\" />" %>
                            </td>
                            <td align="center">
                                <%# Eval("Remark")%>
                            </td>
                            <td align="center">
                                <%# (bool)Eval("LNotice")==true?"出团通知":""%>&nbsp:&nsbp:<%# (bool)Eval("RNotice")==true?"回团通知":""%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <span class="formtableT formtableT02">订单结算信息</span>
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单结算价：
                </td>
                <td width="79%" bgcolor="#e0e9ef">
                    成人单价
                    <asp:TextBox ID="txtAdultPrices" runat="server" CssClass="formsize50"></asp:TextBox>
                    * 成人数
                    <asp:TextBox ID="txtAdultNums" runat="server" CssClass="formsize40"></asp:TextBox>
                    + 儿童单价
                    <asp:TextBox ID="txtChildPrices" runat="server" CssClass="formsize50"></asp:TextBox>
                    * 儿童数
                    <asp:TextBox ID="txtChildNums" runat="server" CssClass="formsize40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    增加费用：
                </td>
                <td width="79%" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtAddMoney" runat="server" CssClass="formsize80"></asp:TextBox>
                    <span style="margin-left: 80px;">备注：
                        <asp:TextBox ID="txtAddMoneyRemark" runat="server" CssClass="formsize600"></asp:TextBox>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    减少费用：
                </td>
                <td>
                    <asp:TextBox ID="txtLessMoney" runat="server" CssClass="formsize80"></asp:TextBox>
                    <span style="margin-left: 80px;">备注：
                        <asp:TextBox ID="txtLessMoneyRemark" runat="server" CssClass="formsize600"></asp:TextBox>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    结算金额：
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmMoney" runat="server" CssClass="formsize80"></asp:TextBox>
                    元&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; 订单利润：
                    <asp:TextBox ID="txtOrderProfit" runat="server" CssClass="formsize80"></asp:TextBox>
                    元
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" id="ul_action_list">
            <a href="javascript:" hidefocus="true"><s class="baochun"></s>保 存</a> 
            <a href="javascript:" hidefocus="true" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();">
            <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var OrderPage = {
            _BindBtn: function() {
                $("#ul_action_list").find("a").eq(0).unbind("click");
                $("#ul_action_list").find("a").eq(0).click(function() {
                    var orderId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderId") %>';
                    var tourId = '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>';
                    var sl = '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                    $.newAjax({
                        type: "POST",
                        url: '/OperaterCenter/OrderExpenceInfo.aspx?type=save&sl=' + sl,
                        cache: false,
                        data: { tourId: tourId, OrderId: orderId },
                        dataType: "json",
                        success: function(data) {
                            if (data.result == "1") {
                                tableToolbar._showMsg(data.msg);
                                window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                //window.location.href = window.location.href;
                            } else {
                                tableToolbar._showMsg(data.msg);
                            }
                        },
                        error: function() {
                            tableToolbar._showMsg("服务器繁忙，请稍后在试!");
                            window.location.href = window.location.href;
                            return false;
                        }
                    });
                    return false;
                });
            },
            _PageInit: function() {
                OrderPage._BindBtn();
            }
        }
        $(document).ready(function() {
            OrderPage._PageInit();
        });
    </script>

</body>
</html>
