<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkPlanInfo.aspx.cs" Inherits="Web.UserCenter.WorkExchange.WorkExInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 5px auto">
            <tbody>
                <tr>
                    <td width="9%" height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        编&nbsp; 号：
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="19%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        计划标题：
                    </td>
                    <td width="81%" bgcolor="#E9F4F9" align="left">
                        &nbsp;<asp:Label ID="lblTtile" runat="server" Text=""></asp:Label>
&nbsp;</td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        计划内容：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        上传附件：
                    </td>
                    <td>
                        <a href="#">&nbsp;下载</a>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        计划说明：
                    </td>
                    <td>
                        <asp:Label ID="lblPlanRemarks" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        发布时间：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblDateTime" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        提交人：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblSubmitName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        接收人：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblGetName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        预计完成时间：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblFristDateTime" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        实际完成时间：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblSecondDateTime" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        状&nbsp; 态：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        上级部门评语：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblHigherRemarks" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        总经理评语：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblBossRemarks" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        填写时间：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblAddDateTime" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        最后修改时间：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblUpdateTime" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
