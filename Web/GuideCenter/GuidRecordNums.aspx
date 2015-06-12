<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuidRecordNums.aspx.cs"
    Inherits="Web.GuideCenter.GuidRecordNums" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

</head>
<body style="background: 0 none;">
    <form id="Form1" action="GuidRecordNums.aspx" method="get">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>               
                <td width="89%" height="28" align="left" bgcolor="#C1E5F5">
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    <input type="hidden" name="Guid" value="<%=Request.QueryString["Guid"] %>" />
                    <span class="alertboxTableT">出团时间：</span>
                    <input name="txtStartTime" type="text" class="inputtext formsize80" onfocus="WdatePicker();" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtStartTime") %>"/>
                    -
                    <input name="txtEndTime" type="text" class="inputtext formsize80" onfocus="WdatePicker();"  value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndTime") %>"/>
                    <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button>
                        
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td width="6%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td width="22%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号
                </td>
                <td width="31%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    线路名称
                </td>
                <td width="15%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    出团时间
                </td>
                <td width="13%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    天数
                </td>
                <td width="13%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    人数
                </td>
            </tr>
            <asp:Repeater ID="replist" runat="server">
                <ItemTemplate>
                    <tr>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            <%# Eval("TourCode")%>
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            <%# Eval("RouteName")%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%# Eval("DayCount")%>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <%# Eval("RealPeopleNumber")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="lab_Msg" runat="server" Visible="false"></asp:Label>
        </table>
        <div style="position: relative; height: 32px;">
            <div class="pages">
                
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
