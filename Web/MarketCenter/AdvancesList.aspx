<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvancesList.aspx.cs" Inherits="EyouSoft.Web.MarketCenter.AdvancesList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" /></head>
 
<body style="background:0 none;">
<div class="alertbox-outbox">
  <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" style="margin:0 auto" id="liststyle">
    <tr style="background:url(../images/y-formykinfo.gif) repeat-x center top;">
    <td height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">序号</td>
    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">线路名称</td>
    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">出团时间</td>
    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">订单号</td>
    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">客户单位</td>
    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">人数</td>
    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">确定合同金额</td>
    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">未收款</td>
  </tr>
  <asp:Repeater ID="repList" runat="server">
    <ItemTemplate>
        <tr>
            <td  align="center"><%# (pageIndex - 1) * pageSize + (Container.ItemIndex + 1) %></td>
            <td height="28"  align="left"><%#Eval("RouteName")%></td>
            <td height="28"  align="center"><%#EyouSoft.Common.UtilsCommons.GetDateString((Eval("LDate")), ProviderToDate)%></td>
            <td  align="center"><%#Eval("OrderCode")%> </td>
            <td  align="center"><%#Eval("Customer")%></td>
            <td  align="center"><%#Eval("Adult")%><sup>+<%#Eval("Child")%></sup></td>
            <td  align="right"><b class="fontgreen"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmAdvances"), ProviderToMoney)%></b></td>
            <td height="28"  align="right"><b class="fontred"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Arrear"), ProviderToMoney)%></b></td>
          </tr>
    </ItemTemplate>
  </asp:Repeater>
  <tr bgcolor="#e9f4f9">
    <td height="23" colspan="7" align="right" class="alertboxTableT">合计：</td>
    <td align="right" class="alertboxTableT"><b class="fontred"><asp:Label ID="labSumMoney" runat="server"></asp:Label></b></td>
  </tr>
  <tr>
    <td height="23" colspan="8" align="right" class="alertboxTableT"><div style="position:relative; height:32px;">
      <div class="pages">
        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
      </div>
    </div></td>
  </tr>
</table>
</div>
</body>
</html>

