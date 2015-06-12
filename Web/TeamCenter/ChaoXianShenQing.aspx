<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaoXianShenQing.aspx.cs"
    Inherits="EyouSoft.Web.TeamCenter.ChaoXianShenQing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>超限申请</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox" id="div_AddPrice_ApplyPrice" style="width: 620px; position: static;
        padding-bottom: 0px;">
        <asp:PlaceHolder ID="phdKehu" runat="server">
            <div class="tanchuT" style="text-align: left;">
                <b class="fontred">客户单位超限：</b></div>
            <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            客户单位超限
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            <span class="th-line">欠款额度</span>
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            拖欠金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限时间
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            单团账龄期限
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            最长超时天数
                        </td>
                    </tr>
                    <%=PageHtml[0]%>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phdXiaoshou" runat="server">
            <div class="tanchuT" style="text-align: left;">
                <b class="fontred">销售员超限：</b></div>
            <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            销售员
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            <span class="th-line">欠款额度</span>
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            已确认垫款
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            团队预收
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            支出
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            拖欠金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限金额
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            超限时间
                        </td>
                    </tr>
                    <%=PageHtml[1]%>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
        </asp:PlaceHolder>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        申请人：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblApplyMan" runat="server" Text=""></asp:Label>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        垫付金额：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtApplyPrice" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        申请时间：
                    </td>
                    <td width="21%" align="left">
                        <asp:Label ID="lblApplyDateTime" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="12%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td width="53%" align="left">
                        <asp:TextBox ID="txtApplyRemarks" runat="server" CssClass="inputtext formsize180"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hideTourId" runat="server" />
        <asp:HiddenField ID="hideOrderId" runat="server" />
        <div class="alertbox-btn">
            <a href="javascript:void(0);" id="btnApplyPrice"><s class="baochun"></s>保 存</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;" id="btnCloseApplyPrice"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var ThisPage = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                goUrl: '<%=Request.QueryString["url"] %>'
            },
            Save: function() {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/ChaoXianShenQing.aspx?dotype=save&" + $.param(ThisPage.Data),
                    data: $("#btnCloseApplyPrice").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1" || ret.result == "2") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                if (ThisPage.Data.goUrl != "") {
                                    parent.parent.location.href = decodeURIComponent(ThisPage.Data.goUrl);
                                } else {
                                    parent.parent.location.href = parent.location.href;
                                }
                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                ThisPage.BindBtn();
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                })
            },
            BindBtn: function() {
                $("#btnApplyPrice").unbind("click").html('<s class="baochun"></s>保 存</a>').click(function() {
                    if ($.trim($("#<%=txtApplyPrice.ClientID %>").val()) != "") {
                        if (parent.tableToolbar.getFloat($("#<%=txtApplyPrice.ClientID %>").val()) > 0) {
                            $("#btnApplyPrice").html('<s class="baochun"></s>提交中..</a>').unbind("click");
                            ThisPage.Save();
                        } else {
                            parent.tableToolbar._showMsg("垫付金额格式不正确!");
                        }
                    } else {
                        parent.tableToolbar._showMsg("垫付金额必须大于0!");
                    }
                    return false;
                })
            }
        }

        $(function() {
            ThisPage.BindBtn();
        })
    </script>

</body>
</html>
