<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="Web.FinanceManage.InvoiceManage.Show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发票管理-查看-</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        开票日期：
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_billTime" runat="server"></asp:Label>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        开票金额：
                    </td>
                    <td align="left">
                        <b class="fontred">
                            <asp:Label ID="lbl_billAmount" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        票据号：
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_billNo" runat="server"></asp:Label>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注：
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_remark" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批状态：
                    </td>
                    <td width="35%" align="left">
                        已审批
                    </td>
                    <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批人：
                    </td>
                    <td width="35%" align="left">
                        <asp:Label ID="lbl_approver" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批日期：
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_approveTime" runat="server"></asp:Label>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批意见：
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_approveRemark" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    </form>
</body>
</html>
