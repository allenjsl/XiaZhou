<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxInformation.aspx.cs" Inherits="Web.CustomerCenter.AjaxRequest.AjaxInformation" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="99%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" id="liststyle" style="margin:0 auto;">   
                   <asp:Repeater ID="rptZXJLList" runat="server">
                            <ItemTemplate>
                                <tr class="">
                                  <td align="center" class="tdicon"><img src="../images/y-kehugl05.gif" width="6" height="10" /></td>
                                    <td align="left"><%# Eval("RecordTime")%></td>
                                    <td align="left"><%# Eval("Number")%></td>
                                    <td align="left"><%# Eval("ReceptionName")%></td>
                                    <td align="left"><%# Eval("IsTelRecord")%></td>
                                </tr>
                            </ItemTemplate>
                    </asp:Repeater>
              </table>
              <div style="position:relative; height:31px;">
                  <div class="pages" id="div_AjaxPage">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                </div>
              </div>
    </div>
    </form>
</body>
</html>
