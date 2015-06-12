<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="StudyRead.aspx.cs" Inherits="EyouSoft.Web.ManageCenter.Study.StudyRead" %>
<%@ Register Src="../../UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="24%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    标题：
                </td>
                <td width="76%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布对象：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbObj" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    内容：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
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
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布人：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbSender" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布时间：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label ID="lbTime" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
         <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="24%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    回复：
                </td>
                <td width="76%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtReply" runat="server" Height="96px" Width="492px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="28" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    <asp:Button ID="btnSave" runat="server" CssClass="baochun" 
                        onclick="btnSave_Click" Text="回复" />
                </td>
            </tr>
        </table>
        
        <asp:Repeater ID="rptReply" runat="server">
        <HeaderTemplate><table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td colspan="2" height="28" align="center" bgcolor="#b7e0f3" 
                    class="alertboxTableT">
                    <strong>回复信息</strong>
                </td>
            </tr></HeaderTemplate>
        <ItemTemplate>
        <tr><td align="left" bgcolor="#b7e0f3" width="150px" class="alertboxTableT">回复人：<%#Eval("OperatorName")%> <br />时&nbsp;&nbsp;间：<%#Eval("IssueTime","{0:yyyy-MM-dd}") %></td><td height="28" align="left" bgcolor="#e0e9ef"><%#Eval("ReplyInfo") %></td></tr>
        </ItemTemplate>
        <FooterTemplate> </table></FooterTemplate>
        </asp:Repeater>
        <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
    </div>
    </form>
</body>
</html>
