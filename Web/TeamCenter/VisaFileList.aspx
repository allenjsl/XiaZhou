<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisaFileList.aspx.cs" Inherits="EyouSoft.Web.TeamCenter.VisaFileList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; height:350px;">
            <table width="99%" cellspacing="0" cellpadding="0" border="0" bgcolor="#FFFFFF" align="center"
                style="height: auto; zoom: 1; overflow: hidden;">
                <tbody>
                    <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                        <td width="50" height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            序号
                        </td>
                        <td bgcolor="#B7E0F3" align="left" class="alertboxTableT">
                            签证资料名称
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            下载
                        </td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td valign="middle" height="28" align="center">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="left">
                                    <a href="<%#Eval("FilePath")%>" target="_blank">
                                        <%#Eval("Name")%></a>
                                </td>
                                <td align="center">
                                    <span class="alertboxTableT"><a href="<%#Eval("FilePath")%>" target="_blank">
                                        <img src="/images/xiazai.gif">下载</a></span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <tr>
                        <td height="28" bgcolor="#E9F4F9" align="center" colspan="3">
                            <div style="float: none;" class="pages">
                                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>
</body>
</html>
