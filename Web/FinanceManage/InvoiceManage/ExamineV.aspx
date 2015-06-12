<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamineV.aspx.cs" Inherits="Web.FinanceManage.InvoiceManage.ExamineV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <span class="formtableT formtableT02">登记信息</span>
            <table id="tab_list" width="100%" border="0" align="center" cellpadding="0" cellspacing="0"
                style="margin: 0 auto">
                <tr>
                    <td width="5%" height="25" align="center" bgcolor="#B7E0F3">
                        <span class="alertboxTableT">序号</span>
                    </td>
                    <td width="10%" height="25" align="center" bgcolor="#B7E0F3">
                        开票日期
                    </td>
                    <td width="10%" height="25" align="right" bgcolor="#B7E0F3">
                        开票金额
                    </td>
                    <td height="25" align="center" bgcolor="#B7E0F3">
                        票据号
                    </td>
                    <td height="25" align="left" bgcolor="#B7E0F3">
                        备注
                    </td>
                </tr>
                <tr id="tr_sum">
                    <td height="28" colspan="2" align="right">
                        <strong>合计：</strong>
                    </td>
                    <td align="right">
                        <b class="fontred">0</b>
                    </td>
                    <td colspan="2" align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
            <span class="formtableT formtableT02">审核信息</span>
            <form runat="server">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批状态：
                    </td>
                    <td width="35%" align="left">
                        <input type="checkbox" id="chk_type" style="border: none;" />
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批人：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_Name" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批日期：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_dateTime" runat="server" ReadOnly="true" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批意见：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_ApproveRemark" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </form>
            <div class="hr_10">
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="baochun"></s>审
                核</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    <div id="cloneTr" style="display: none">
        <table>
            <tr>
                <td height="28" align="center">
                </td>
                <td align="center">
                </td>
                <td align="right">
                    <b class="fontred"></b>
                </td>
                <td align="center">
                    <input type="text" class="inputtext formsize120" name="txt_BillNo" size="10" value="" />
                </td>
                <td align="left">
                    <%=EyouSoft.Common.UtilsCommons.GetMoneyString("",ProviderToMoney) %>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        var ExamineV = {
            _parentPage: null,
            BindList: function() {
                var that = this;
                var parenttr = $(that._parentPage.$("#liststyle tr[isexaminev]"));
                var tr, i = 0;
                var trsum = $("#tr_sum");
                var sum = 0;
                parenttr.each(function() {
                    var obj = $(this);
                    sum = tableToolbar.calculate(sum, obj.attr("data-billamount"), "+");
                    i++;
                    tr = $("#cloneTr tr").clone(true);
                    tr.attr("data-billid", obj.attr("data-billid"))
                    tr.find("td:eq(0)").html(i);
                    tr.find("td:eq(1)").html($.trim(obj.attr("data-billtime")));
                    tr.find("td:eq(2) b").html($.trim(obj.attr("data-billamountStr")));
                    tr.find("td:eq(3) input:text").val($.trim(obj.attr("data-billno")));
                    tr.find("td:eq(4)").html($.trim(obj.attr("data-remark")));
                    trsum.before(tr);
                })
                if (i <= 1) {
                    trsum.remove();
                }
                else {
                    trsum.find("b").html('<%=this.ProviderMoneyStr %>' + sum);
                }
            },
            BingBtn: function() {
                var that = this;
                $("#a_Save").unbind("click").click(function() {
                    if ($("#chk_type:checked").length > 0) {
                        var that = this;
                        var obj = $(obj);
                        obj.unbind("click").css("background-position", "0 -57px").css("text-decoration", "none");
                        obj.html("<s class=baochun></s>  提交中...");
                        var data = {
                            BillIds: [],
                            BillNo: [],
                            ApproveRemark: ""
                        }

                        $("tr[data-billid]").each(function() {
                            var tr = $(this);
                            data.BillIds.push(tr.attr("data-billid"));
                            data.BillNo.push(tr.find("input:text").val());
                        })
                        data.BillIds = data.BillIds.join(',');
                        data.BillNo = data.BillNo.join(',');
                        data.ApproveRemark = $("#<%=txt_ApproveRemark.ClientID %>").val();
                        $.newAjax({
                            type: "post",
                            data: data,
                            cache: false,
                            url: "/FinanceManage/InvoiceManage/ExamineV.aspx?" + $.param({ doType: 'Save', sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' }),
                            dataType: "json",
                            success: function(data) {
                                if (parseInt(data.result) === 1) {
                                    parent.tableToolbar._showMsg('审核成功!', function() {
                                        window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                        parent.location.href = parent.location.href;
                                    });
                                }
                                else {
                                    parent.tableToolbar._showMsg("审核失败!");
                                }
                            },
                            error: function() {
                                parent.tableToolbar._showMsg(parent.tableToolbar.errorMsg);
                                that.BindBtn();
                            }
                        });
                    }
                    else {
                        parent.tableToolbar._showMsg('未确认审核状态!');
                    }
                    return false;
                })
            },
            PageInit: function() {
                var that = this;
                that._parentPage = parent.window;
                that.BindList();
                that.BingBtn();
            }
        }
        $(function() {
            ExamineV.PageInit();
        })
    </script>

</body>
</html>
