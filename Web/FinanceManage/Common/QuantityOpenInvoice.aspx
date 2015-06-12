<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuantityOpenInvoice.aspx.cs"
    Inherits="Web.FinanceManage.Common.QuantityOpenInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批量开票</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <div class="hr_5">
            </div>
            <table width="99%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" id="liststyle"
                style="margin: 0 auto;">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        订单号
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        客户单位
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        开票日期
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        未开票金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        开票人
                    </td>
                    <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        开票金额
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-tourcode="<%#Eval("TourCode") %>" data-tourid="<%#Eval("TourId") %>" data-orderid="<%#Eval("OrderId") %>"
                            data-customerid="<%#Eval("CustomerId") %>" data-customer="<%#Eval("Customer") %>"
                            data-sellerid="<%#Eval("SellerId") %>" data-sellername="<%#Eval("SellerName") %>"
                            data-contact="<%#Eval("ContactName") %>" data-phone="<%#Eval("ContactPhone")%>">
                            <td height="30" align="center">
                                <%#Eval("OrderCode")%>
                            </td>
                            <td align="left">
                                <%#Eval("Customer")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("BillTime"),ProviderToDate)%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("BillAmount"),ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%=SiteUserInfo.Name %>
                            </td>
                            <td align="left">
                                <textarea name="txt_Remark" style="height: 25px;" class="inputtext formsize200"></textarea>
                            </td>
                            <td align="right">
                                <input name="txt_GetMoney" data-class="txt_GetMoney" type="text" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("BillAmount").ToString()) %>" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="7">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_sum" runat="server">
                    <tr>
                        <td height="30" colspan="6" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b id="b_amount" class="fontred"></b>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="phdShow" runat="server"><a href="javascript:void(0);" hidefocus="true"
                id="a_Save" style="text-indent: 0px;">批量开票</a> </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var QuantityOpenInvoice = {
            Save: function(obj) {
                var url = '/FinanceManage/Common/QuantityOpenInvoice.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                var that = this, dataArr = [], subDataArr = [];
                var data = {}
                var obj = $(obj);
                obj.unbind("click")
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" })
                obj.html("<s class=baochun></s>&nbsp;&nbsp;&nbsp;开票中...");
                $("#liststyle tr[data-orderid]").each(function() {
                    var obj = $(this);
                    subDataArr.push(obj.attr("data-orderid")); /*订单编号*/
                    subDataArr.push(obj.attr("data-customerid")); /*开票公司编号*/
                    subDataArr.push(obj.attr("data-customer")); /*开票公司*/
                    subDataArr.push(obj.attr("data-sellerid")); /*销售员*/
                    subDataArr.push($.trim(obj.find("td:eq(5) textarea").html()))/*备注*/
                    subDataArr.push($.trim(obj.find("td:eq(6) :text").val()))/*到款金额*/
                    subDataArr.push(obj.attr("data-sellername")); /*销售员名称*/
                    subDataArr.push(obj.attr("data-tourcode")); /*团号*/
                    subDataArr.push(obj.attr("data-tourid")); /*团队编号*/
                    subDataArr.push(obj.attr("data-contact")); /*联系人*/
                    subDataArr.push(obj.attr("data-phone")); /*联系电话*/
                    dataArr.push(subDataArr.join('|'));
                    subDataArr = [];
                })
                data.list = dataArr.join(',');
                data.doType = "Save";
                data.ParentType = parseInt(Boxy.queryString("ParentType")) || 1;
                $.newAjax({
                    type: "post",
                    data: data,
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('批量开票成功!', function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg);
                            that.BindBtn();
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                        that.BindBtn();
                    }
                });

                return false;
            },
            BindBtn: function() {
                var that = this;
                var a_Save = $("#a_Save");
                a_Save.unbind("clcik");
                a_Save.css({ "background-position": "", "text-decoration": "" })
                a_Save.text("批量开票");
                a_Save.click(function() {
                    that.Save(this);
                    return false;
                })
                /*绑定自动计算功能*/
                $(":text[data-class='txt_GetMoney']")
                .blur(function() {
                    var sum = 0;
                    $(":text[data-class='txt_GetMoney']").each(function() {
                        sum = tableToolbar.calculate(sum, parseFloat($(this).val()) || 0, "+");
                    })
                    $("#b_amount").html(sum)
                })
                /*
                .keypress(function() {
                var sumobj = $("#b_amount");
                sumobj.attr("sum", (parseFloat(sumobj.html()) || 0) - (parseFloat($(this).val()) || 0))
                })
                .keyup(function() {
                var sumobj = $("#b_amount");
                sumobj.html((parseFloat(sumobj.attr("sum")) || 0) + (parseFloat($(this).val()) || 0))
                })
                */
            },
            PageInit: function() {
                tableToolbar.init({
                    tableContainerSelector: "#liststyle" //表格选择器
                })
                this.BindBtn();
                $(":text[data-class='txt_GetMoney']:eq(0)").blur();
            }

        }
        $(function() {
            QuantityOpenInvoice.PageInit();
        })
    </script>

</body>
</html>
