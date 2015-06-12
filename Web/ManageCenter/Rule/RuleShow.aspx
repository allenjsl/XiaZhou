<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuleShow.aspx.cs" Inherits="Web.ManageCenter.Rule.RuleShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    编号：
                </td>
                <td width="39%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbNum" runat="server"></asp:Label>
                </td>
                <td height="11%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    制度标题：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    制度内容：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef" colspan="3">
                    <p style="padding: 5px;">
                        <asp:Label ID="lbContent" runat="server"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbFile" runat="server"></asp:Label>
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    适用部门：
                </td>
                <td align="left">
                    <asp:Label runat="server" ID="lbApplydept"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布部门：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" ID="lbDept"></asp:Label>
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    发布人：
                </td>
                <td align="left">
                    <asp:Label runat="server" ID="lbIssuedName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布时间：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" ID="lbIssurdTime"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
