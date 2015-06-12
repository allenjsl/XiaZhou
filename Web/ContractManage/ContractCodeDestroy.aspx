<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractCodeDestroy.aspx.cs"
    Inherits="Web.ContractManage.ContractCodeDestroy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合同管理-合同销号</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    合同编号
                </td>
                <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    领用人
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    领用时间
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    订单号
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    客户单位
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    人数
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    合同金额
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    销售员
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    操作
                </td>
            </tr>
            <asp:Repeater runat="server" ID="repList">
                <ItemTemplate>
                    <tr class="tr_Destroy" data-st="<%#(int)((EyouSoft.Model.EnumType.ConStructure.ContractStatus)Eval("ContractStatus")) %>"
                        data-id="<%#Eval("ContractId") %>">
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            <%#Eval("ContractCode")%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <label data-class="use">
                                <%#Eval("UseName")%></label>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%#Eval("CollarTime", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%#Eval("OrderCode")%>
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            <%#Eval("RouteName")%>
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            <%#Eval("BuyCompanyName")%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%#(int)Eval("Adults")>0?((int)Eval("Adults") + (int)Eval("Childs") + (int)Eval("Others")).ToString():""%>
                        </td>
                        <td align="right" bgcolor="#FFFFFF" class="fontred">
                            <strong>
                                <%#(decimal)Eval("SumPrice") > 0 ? (EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), this.ProviderToMoney)) : ""%></strong>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%#Eval("SellerName")%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <label>
                                <input type="radio" name="radio<%#(Container.ItemIndex + 1)%>" value="<%=(int)EyouSoft.Model.EnumType.ConStructure.ContractStatus.销号 %>"
                                    style="border: none;" />销号</label>
                            <label>
                                <input type="radio" name="radio<%#(Container.ItemIndex + 1)%>" value="<%=(int)EyouSoft.Model.EnumType.ConStructure.ContractStatus.作废 %>"
                                    style="border: none;" />作废</label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="ContractCodeDestroy.ResetForm();" hidefocus="true">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var ContractCodeDestroy = {
            Query: {/*URL参数对象*/
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
            },
            ResetForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Save: function() {
                if ($("input:radio:checked").length == 0) {
                    tableToolbar._showMsg("-请选择操作!");
                    return false;
                }
                else {
                    var obj = $(".tr_Destroy");
                    var stas = new Array();
                    var cids = new Array();
                    $(".tr_Destroy").each(function() {
                        var length = $(this).find("input[type='radio']:checked").length;
                        if (length > 0) {
                            stas.push($(this).find("input[type='radio']:checked").val());
                            cids.push($(this).attr("data-id"));
                        }
                    });

                    $("#btnSave").unbind("click");
                    $("#btnSave").addClass("alertbox-btn_a_active");
                    $("#btnSave").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ContractManage/ContractCodeDestroy.aspx?";
                    url += $.param({
                        doType: 'save',
                        sl: this.Query.sl,
                        sta: stas.join(','),
                        cid: cids.join(',')
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        dataType: "json",
                        success: function(result) {
                            if (result.result == 'True') {
                                tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
                                });
                            }
                            else { tableToolbar._showMsg(result.msg); }
                            return false;
                        },
                        error: function() {
                            //ajax异常--你懂得
                            tableToolbar._showMsg("服务器忙！");
                            ContractCodeDestroy.BindBtn();
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").click(function() {
                    ContractCodeDestroy.Save();
                    return false;
                })
                $("#btnSave").attr("class", "");
                $("#btnSave").html("<s class=\"baochun\"></s>保 存");
            }
        }

        $(function() {
            ContractCodeDestroy.BindBtn();
            $(".tr_Destroy").each(function() {
                if ($(this).attr("data-st") == '<%=(int)EyouSoft.Model.EnumType.ConStructure.ContractStatus.销号 %>') {
                    $(this).find("input[type='radio'][value='<%=(int)EyouSoft.Model.EnumType.ConStructure.ContractStatus.销号 %>']").attr("checked", 'checked');
                }
                if ($(this).attr("data-st") == '<%=(int)EyouSoft.Model.EnumType.ConStructure.ContractStatus.作废 %>') {
                    $(this).find("input[type='radio'][value='<%=(int)EyouSoft.Model.EnumType.ConStructure.ContractStatus.作废 %>']").attr("checked", 'checked');
                }
            })
        })
    </script>

    </form>
</body>
</html>
