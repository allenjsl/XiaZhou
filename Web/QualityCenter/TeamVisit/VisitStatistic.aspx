<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitStatistic.aspx.cs"
    Inherits="Web.QualityCenter.TeamVisit.VisitStatistic" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <form method="get">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="89%" height="28" align="left" bgcolor="#C1E5F5">
                    <span class="alertboxTableT">回访时间：</span>
                    <input name="txtCrmStime" type="text" class="formsize80" id="txtCrmStime" onfocus="WdatePicker()"
                        value='<%=Request.QueryString["txtCrmStime"]%>' />
                    -
                    <input name="txtCrmEtime" type="text" class="formsize80" id="txtCrmEtime" onfocus="WdatePicker()"
                        value='<%=Request.QueryString["txtCrmEtime"]%>' />
                    <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button>
                </td>
            </tr>
        </table>
        <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
        </form>
        <div class="hr_5">
        </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" id="liststyle">
            <tr>
                <td height="23" width="30px" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    序号
                </td>
                <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    团号
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    出团日期
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    销售员
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    导游
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    计调员
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    回访类型
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    回访均分
                </td>
            </tr>
            <asp:Repeater ID="RepList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%#Container.ItemIndex+1%>
                        </td>
                        <td align="center">
                            <%#Eval("TourCode")%>
                        </td>
                        <td height="28" align="center">
                            <%#Eval("RouteName")%>
                        </td>
                        <td align="center">
                            <%#Eval("DateTime","{0:yyyy-MM-dd}")%>
                        </td>
                        <td align="center">
                            <%#Eval("Seller")%>
                        </td>
                        <td align="center">
                            <%#this.GetGuidName(Eval("GuideName"))%>
                        </td>
                        <td align="center">
                            <%#Eval("Planer")%>
                        </td>
                        <td align="center">
                            <%#Eval("ReturnType")%>
                        </td>
                        <td align="center">
                            <%#Eval("QualityScore")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="pages">
        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
    </div>
    <div class="hr_10">
    </div>
</body>
</html>
