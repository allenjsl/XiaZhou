<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTrading.aspx.cs" Inherits="Web.CustomerCenter.AjaxRequest.AjaxTrading" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <table width="99%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" id="Table1" style="margin:0 auto;">  
              <tr bgcolor="#d6eaf7">
                    <td align="center">团号</td>
                    <td align="left">线路名称</td>
                    <td align="center">订单号</td>
                    <td align="center">人数</td>
                    <td align="right">金额</td>
              </tr> 
               <asp:Repeater ID="rptJYJLList" runat="server">
                        <ItemTemplate>
                          <tr>
                            <td align="center"><%# Eval("TourCode")%></td>
                            <td align="left"><a href="#"><%# Eval("RouteName")%></a></td>
                            <td align="center"><%# Eval("OrderCode")%></td>
                            <td align="center"><%# Eval("PeopleCount")%></td>
                            <td align="right" class="fontbsize12"><%# Eval("Money")%></td>
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
