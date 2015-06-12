<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamineA.aspx.cs" Inherits="Web.FinanceManage.Arrearage.ExamineA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>财务管理-欠款预警-审批</title>
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
        <div style="text-align: left;" class="tanchuT">
            <b class="fontred">客户单位超限：</b></div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    超限客户单位
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="th-line">欠款额度</span>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    拖欠金额
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    超限金额
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    超限时间
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 80px">
                    单团账龄期限
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 80px">
                    最长超时天数
                </td>
            </tr>
            <asp:Repeater ID="rpt_cwList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td style="padding: 5px 5px" height="28" align="center" bgcolor="#FFFFFF">
                            <%#Eval("Name")%>
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <b class="fontbsize12">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AmountOwed"), ProviderToMoney)%></b>
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <a href="javascript:void(0);" data-crmid="<%#Eval("Id") %>" data-crmname="<%#Eval("Name") %>">
                                <strong>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Arrear"), ProviderToMoney)%></strong></a>
                        </td>
                        <td align="right" bgcolor="#FFFFFF">
                            <b class="fontgreen">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Transfinite"), ProviderToMoney)%></b>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("TransfiniteTime"), ProviderToDate)%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%#Eval("Deadline")%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%#Eval("DeadDay")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="pan_msg" runat="server">
                <tr align="center">
                    <td style="padding: 5px 5px" colspan="7">
                        暂无数据！
                    </td>
                </tr>
            </asp:Panel>
        </table>
        <div class="hr_10">
        </div>
        <asp:Panel ID="pan_sw" runat="server">
            <div style="text-align: left;" class="tanchuT">
                <b class="fontred">销售员超限：</b></div>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        销售员
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <span class="th-line">欠款额度</span>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        已确认垫款
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团队预收
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        支出
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        拖欠金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        超限金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        超限时间
                    </td>
                </tr>
                <asp:Repeater ID="rpt_swList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center" height="28" bgcolor="#FFFFFF">
                                <%#Eval("Name")%>
                            </td>
                            <td align="right" bgcolor="#FFFFFF">
                                <strong>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AmountOwed"),ProviderToMoney)%></strong>
                            </td>
                            <td align="right" bgcolor="#FFFFFF">
                                <strong>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmAdvances"), ProviderToMoney)%></strong>
                            </td>
                            <td align="right" bgcolor="#FFFFFF">
                                <strong>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PreIncome"), ProviderToMoney)%></strong>
                            </td>
                            <td align="right" bgcolor="#FFFFFF">
                                <b class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPay"), ProviderToMoney)%></b>
                            </td>
                            <td align="right" bgcolor="#FFFFFF">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Arrear"), ProviderToMoney)%></b>
                            </td>
                            <td align="right" bgcolor="#FFFFFF">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Transfinite"), ProviderToMoney)%></b>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("TransfiniteTime"), ProviderToDate)%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="hr_10">
            </div>
        </asp:Panel>
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    申请人：
                </td>
                <td align="left">
                    <asp:Label ID="lbl_approverName" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    垫付金额：
                </td>
                <td align="left">
                    <asp:Label ID="lbl_disburseaount" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="14%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    申请时间：
                </td>
                <td width="21%" align="left">
                    <asp:Label ID="lbl_applyTime" runat="server" Text=""></asp:Label>
                </td>
                <td width="12%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td width="53%" align="left">
                    <asp:Label ID="lbl_remark" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <form runat="server">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    审批状态：
                </td>
                <td align="left">
                    <asp:RadioButton ID="BtnType1" runat="server" Checked="True" GroupName="BtnType"
                        Text="通过" />
                    <asp:RadioButton ID="BtnType2" runat="server" GroupName="BtnType" Text="不通过" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    审批人：
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_approver" Enabled="false" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="14%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    审批时间：
                </td>
                <td width="21%" align="left">
                    <asp:TextBox ID="txt_approveTime" Enabled="false" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                </td>
                <td width="12%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td width="53%" align="left">
                    <asp:TextBox ID="txt_remark" runat="server" CssClass="inputtext formsize180"></asp:TextBox>
                </td>
            </tr>
        </table>
        </form>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="phSubmit">
            <a href="javascript:void(0);" id="a_Save" hidefocus="true"><s class="baochun"></s>保
                存</a>
                </asp:PlaceHolder>
        </div>
    </div>

    <script type="text/javascript">
        var ExamineA = {
            Save: function(obj) {
                var that = this;
                var url = "/FinanceManage/Arrearage/ExamineA.aspx?" + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' });
                var obj = $(obj);
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" })

                obj.html("<s class=baochun></s>  提交中...");
                $.newAjax({
                    type: "post",
                    data: {
                        disburseId: Boxy.queryString("disburseid"), //垫付ID
                        remark: $("#<%=txt_remark.ClientID %>").val(), //审批意见
                        itemType: Boxy.queryString("itemtype"), //0：报价成功/1：成团/2：报名
                        itemId: Boxy.queryString("itemid"), //报价编号/团队编号/订单编号
                        status: $(":radio:checked").attr("id") == "<%=BtnType1.ClientID %>" ? '<%=(int)EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.通过 %>' : '<%=(int)EyouSoft.Model.EnumType.FinStructure.TransfiniteStatus.未通过 %>'//状态
                    },
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(data) {
                        if (parseInt(data.result) == 1) {
                            parent.tableToolbar._showMsg('操作成功!', function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg);
                            that.PageInit();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(parent.tableToolbar.errorMsg);
                        that.PageInit();
                    }
                });

                return false;
            },
            PageInit: function() {
                var that = this;
                var a_Save = $("#a_Save");
                a_Save.unbind("click");
                a_Save.click(function() {
                    that.Save(this);
                    return false;
                })
                a_Save.css("background-position", "0 0")
                a_Save.css("text-decoration", "none")
                a_Save.html("<s class=baochun></s>保 存")
            }
        }
        $(function() {
            ExamineA.PageInit();
        })
    </script>

</body>
</html>
