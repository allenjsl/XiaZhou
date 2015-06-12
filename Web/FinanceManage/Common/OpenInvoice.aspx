<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenInvoice.aspx.cs" Inherits="Web.FinanceManage.Common.OpenInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>开票</title>
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
        <form id="form1" runat="server">
        <div style="margin: auto; text-align: center; font-size: 12px; color: #FF0000; font-weight: bold;
            padding-bottom: 5px;">
            <asp:Label ID="lbl_listTitle" runat="server" Text=""></asp:Label>
        </div>
        </form>
        <table id="tab_list" width="99%" border="0" align="center" cellpadding="0" cellspacing="0"
            style="margin: 0 auto">
            <tr>
                <td width="5%" height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">序号</span>
                </td>
                <td width="10%" height="25" align="center" bgcolor="#B7E0F3">
                    <font class="fontbsize12">* </font>开票日期
                </td>
                <td width="10%" height="25" align="right" bgcolor="#B7E0F3">
                    <font class="fontbsize12">* </font>开票金额
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    是否审核
                </td>
                <td height="25" align="left" bgcolor="#B7E0F3">
                    备注
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3" style="width: 150px">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <tr data-id="<%#Eval("Id") %>" data-tourcode="<%#Eval("TourCode") %>" data-tourid="<%#Eval("TourId") %>"
                        data-billno="<%#Eval("BillNo") %>">
                        <td height="28" align="center">
                            <%# Container.ItemIndex+1%>
                        </td>
                        <td align="center">
                            <input id="txt<%# Container.ItemIndex+1%>" name="openInvoiceDate" type="text" data-class="txt_openInvoiceDate"
                                onfocus="WdatePicker();" valid="required" errmsg="请选择开票日期!" class="inputtext formsize80"
                                value="<%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("BillTime"),ProviderToDate) %>" />
                        </td>
                        <td align="right">
                            <input id="txt_<%# Container.ItemIndex+1%>" type="text" data-class="txt_openInvoiceMoney"
                                name="openInvoiceMoney" class="inputtext formsize80" valid="required|isMoney|range"
                                errmsg="请输入开票金额!|开票金额格式不正确!|开票金额必须大于0且最多两位小数!" min="0.01" value=" <%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)(Eval("BillAmount")??0))%>" />
                        </td>
                        <td align="center">
                            <%#(bool)Eval("IsApprove") ? "已审" : "<span class='fontred'>未审</span>"%>
                        </td>
                        <td align="left">
                            <textarea name="openInvoiceRemar" data-class="txt_openInvoiceRemark" class="inputtext formsize250"
                                style="height: 25px;"><%#Eval("Remark")%></textarea>
                        </td>
                        <td align="center">
                            <%#(bool)Eval("IsApprove") ? "" : " <a data-class='a_Updata' href='javascript:void(0);'>修改</a>&nbsp;<a data-class='a_Delete'                                href='javascript:void(0);'> 删除</a>"%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr data-class="tr_Add">
                <td height="28" align="center">
                    -
                </td>
                <td align="center">
                    <input type="text" data-class="txt_openInvoiceDate" onfocus="WdatePicker();" name="openInvoiceDate"
                        valid="required" errmsg="请选择开票日期!" class="inputtext formsize80" value="<%=EyouSoft.Common.UtilsCommons.GetDateString( DateTime.Now,ProviderToDate) %>" />
                </td>
                <td align="right">
                    <input type="text" data-class="txt_openInvoiceMoney" name="openInvoiceMoney" class="inputtext formsize80"
                        valid="required|isMoney|range" errmsg="请输入开票金额!|开票金额格式不正确!|开票金额必须大于0且最多两位小数!" min="0.01" />
                </td>
                <td class="fontred" align="center" id="td_Add">
                    -
                </td>
                <td align="left">
                    <textarea data-class="txt_openInvoiceRemark" name="openInvoiceRemar" class="inputtext formsize250"
                        style="height: 25px;"></textarea>
                </td>
                <td align="center">
                    <a id="a_Add" href="javascript:void(0);">添加</a>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    <script type="text/javascript">
        var OpenInvoice = {
            Save: function(obj, type) {
                var that = this;
                var url = '/FinanceManage/Common/OpenInvoice.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                var obj = $(obj);
                var trobj = obj.closest("tr");
                if (ValiDatorForm.validator(trobj, "parent")) {
                    var txt = obj.text()
                    obj.unbind("click");
                    obj.css("color", "#A9A9A9");
                    obj.text(txt + "中...");
                    var data = {
                        doType: type,
                        ParentPage: parseInt(Boxy.queryString("ParentPage")) || 1,
                        OrderId: '<%=Request.QueryString["OrderId"] %>' || '<%=Request.QueryString["Id"] %>',
                        TourOrderSalesID: trobj.attr("data-id"),
                        CustomerId: Boxy.queryString("CustomerId"),
                        Customer: decodeURI(Boxy.queryString("Customer") || ""),
                        Id: trobj.attr("data-id"),
                        BillNo: trobj.attr("data-billno"),
                        TourCode: trobj.attr("data-tourcode") || "",
                        TourId: trobj.attr("data-tourid") || '<%=EyouSoft.Common.Utils.GetQueryStringValue("TourId")%>',
                        SellerId: '<%=Request.QueryString["SellerId"] %>',
                        SellerName: decodeURIComponent('<%=Request.QueryString["SellerName"] %>'),
                        Contact: '<%=Request.QueryString["Contact"] %>',
                        Phone: '<%=Request.QueryString["Phone"] %>'
                    }
                    $.newAjax({
                        type: "post",
                        data: $.param(data) + "&" + trobj.find("input,textarea,select").serialize(),
                        cache: false,
                        url: url,
                        dataType: "json",
                        success: function(ret) {
                            if (parseInt(ret.result) === 1) {
                                parent.tableToolbar._showMsg(txt + '成功!');
                                setTimeout(function() {
                                    location.href = location.href;
                                }, 1000)
                            }
                            else {
                                parent.tableToolbar._showMsg(ret.msg);
                                that.BindBtn();
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            that.BindBtn();
                        }
                    });
                }

                return false;
            },
            BindBtn: function() {
                var that = this;
                var obj = $("#tab_list a[data-class='a_Updata']");
                obj.css("color", "");
                obj.text("修改");
                obj.unbind("click")
                obj.click(function() {
                    that.Save(this, "Update")
                    return false;
                })
                obj = $("#tab_list a[data-class='a_Delete']");
                obj.css("color", "");
                obj.text("删除");
                obj.unbind("click")
                obj.click(function() {
                    that.Save(this, "Delete")
                    return false;
                })
                obj = $("#a_Add");
                obj.css("color", "");
                obj.text("添加");
                obj.unbind("click")
                obj.click(function() {
                    that.Save(this, "Add")
                    return false;
                })
            }
        }
        $(function() {
            OpenInvoice.BindBtn();
        })
    </script>

</body>
</html>
