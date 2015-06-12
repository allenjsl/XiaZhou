<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractNumCollarList.aspx.cs" Inherits="Web.ContractManage.ContractNumCollarList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>合同管理-已领用合同份数</title>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:0 none;">
<div class="alertbox-outbox">
  	<div style="margin:0 auto; width:99%;">
  	  <table width="99%" border="0" cellspacing="0" cellpadding="0">
          <tr style="background:url(/images/y-formykinfo.gif) repeat-x center top;">
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">部门</td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">号段</td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">领用人</td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">领用时间</td>
          </tr>
          <asp:Repeater ID="RepList" runat="server">
            <ItemTemplate>
                  <tr>
                    <td height="28" align="center"><%#Eval("Depart")%></td>
                    <td align="center"><%#Eval("StartNum")%>-<%#Eval("EndNum")%></td>
                    <td align="center"><%#Eval("Useoperator")%></td>
                    <td align="center"><%#Eval("IssueTime", "{0:yyyy-MM-dd HH:mm}")%></td>
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