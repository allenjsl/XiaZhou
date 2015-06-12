<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractNumUsedList.aspx.cs" Inherits="Web.ContractManage.ContractNumUsedList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>合同管理-已使用合同份数</title>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
</head>

<body style="background:0 none;">
<div class="alertbox-outbox">
  	<div style="margin:0 auto; width:99%;">
  	  <table width="99%" border="0" cellspacing="0" cellpadding="0">
          <tr style="background:url(../images/y-formykinfo.gif) repeat-x center top;">
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">团号</td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">线路名称</td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">金额</td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">销售员</td>
          </tr>
          <asp:Repeater ID="RepList" runat="server">
            <ItemTemplate>
                  <tr>
                    <td height="28" align="center"><%#Eval("TourCode")%></td>
                    <td align="center"><%#Eval("RouteName")%></td>
                    <td align="center"><%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("Money").ToString())%></td>
                    <td align="center"><%#Eval("Seller")%></td>
                  </tr>                
            </ItemTemplate>
          </asp:Repeater>
      </table>
  </div>
  <div class="hr_10"></div>
  <div style="position:relative; height:20px;"><div class="pages"><cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" /></div></div>
        
</div>
</body>
</html>
