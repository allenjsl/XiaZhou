<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectedOrder.aspx.cs"
    Inherits="Web.CommonPage.SelectedOrder" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background:#e9f4f9;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                style="margin: 0 auto" id="liststyle" >
                <tr>
                    <td width="30" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT" >
                        &nbsp;
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        订单号
                    </td>
                    <td align="left" bgcolor="#b7e0f3" class="alertboxTableT">
                        客户单位
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        人数
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        订单金额
                    </td>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input name="orderID" type="radio" value="<%# Eval("OrderId") %>" style="border: none" />
                            </td>
                            <td height="28" align="left">
                                <%# Eval("OrderCode")%>
                            </td>
                            <td align="left">
                                <%# Eval("BuyCompanyName")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.Utils.GetInt(Eval("Adults").ToString()) + EyouSoft.Common.Utils.GetInt(Eval("Childs").ToString()) + EyouSoft.Common.Utils.GetInt(Eval("Others").ToString())%>
                            </td>
                            <td align="right">
                                <b class="fontred"><%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td height="23" colspan="5" align="right" class="alertboxTableT">
                        <div style="position: relative; height: 32px;">
                            <div class="pages">
                               <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="alertbox-btn">
                <a href="javascript:" hidefocus="true" data-class="selected"><s class="xuanzhe"></s>
                    选 择</a> <a href="javascript:" hidefocus="true" onclick="window.parent.Boxy.getIframeDialog('<%= Request.QueryString["iframeId"] %>').hide()"><s class="chongzhi">
                    </s>关 闭</a></div>
        </div>
    </form>
    <script type="text/javascript">
        var selOrderPage = {
            selectValue: "",
            selectTxt: "",
            selectBuyCompanyName: "",
            parentWindow: null,
            iframeId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>',
            pIframeID: '<%=Request.QueryString["pIframeId"]%>',
            //选用a标签id
            aid: '<%=Request.QueryString["id"] %>',
            SetValue: function() {
                var valueArray = new Array();
                var txtArray = new Array();
                var buyCompanyName = new Array();
                $("#liststyle").find("input[type='radio'][name='orderID']:checked").each(function() {
                    valueArray.push($.trim($(this).val()));
                    txtArray.push($.trim($(this).parent().next().html()));
                    buyCompanyName.push($.trim($(this).parent().next().next().html()));
                });
                selOrderPage.selectValue = valueArray.join(',');
                selOrderPage.selectTxt = txtArray.join(',');
                selOrderPage.selectBuyCompanyName = buyCompanyName.join(',');
            },
            InitSetSelect: function() {
                var oldValue = selOrderPage.parentWindow.$('#' + selOrderPage.aid).parent().find("input[type='hidden']");
                if (oldValue) {
                    $("#liststyle").find("input[type='radio'][name='orderID'][value='" + oldValue + "']").attr("checked", "true");
                }

                var hidIdArr = '<%=EyouSoft.Common.Utils.GetQueryStringValue("orderIdList") %>';
                if (hidIdArr != "") {
                    if (hidIdArr.split(',').length > 0) {
                        for (var i = 0; i < hidIdArr.split(',').length; i++) {
                            $("#liststyle").find("input[type='radio'][name='orderID']").each(function() {
                                if ($(this).val() == hidIdArr.split(',')[i]) {
                                    $("#liststyle").find("input[type='radio'][name='orderID'][value='" + hidIdArr.split(',')[i] + "']").attr("disabled", "disabled");
                                }
                            });
                        }
                    }
                }
            }
        }
        $(function() {
            $("[data-class='selected']").unbind("click").click(function() {
                selOrderPage.SetValue();
                var backData = { id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', value: selOrderPage.selectValue, text: selOrderPage.selectTxt, buyCompanyName: selOrderPage.selectBuyCompanyName };
                //回调方法
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>';
                if (callBackFun) {
                    if (callBackFun.indexOf('.') == -1) {
                        selOrderPage.parentWindow[callBackFun](backData);
                    } else {
                        selOrderPage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](backData);
                    }
                }

                //关闭页面
                window.parent.Boxy.getIframeDialog(selOrderPage.iframeID).hide();
                return false;
            });

            //获得需要赋值页面的window 对象
            if (selOrderPage.pIframeID) {
                selOrderPage.parentWindow = window.parent.Boxy.getIframeWindowByID(selOrderPage.pIframeID);
            }
            else {
                selOrderPage.parentWindow = parent.window;
            }

            selOrderPage.InitSetSelect();
        });
    </script>
</body>
</html>
