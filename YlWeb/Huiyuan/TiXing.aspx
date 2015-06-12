<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TiXing.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.TiXing" MasterPageFile="~/MasterPage/HuiYuan.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
<form id="form1" runat="server">
<div class="user_table">
<table width="100%" border="0" class="tablelist">
           <tr>
             <th align="left">通知提醒</th>
           </tr>
           <asp:Repeater ID="rpt" runat="server">
               <ItemTemplate>
                   <tr>
                     <td align="left"><%#Eval("BiaoTi") %><a class="blue" target="_blank" href="/corp/zixunxx.aspx?s=<%#Eval("ZiXunId") %>">【详情】</a></td>
                   </tr>
                </ItemTemplate>
           </asp:Repeater>
           <asp:PlaceHolder ID="phdNoDat" runat="server" Visible="false">
                   <tr>
                     <td align="center">暂无通知</td>
                   </tr>
           </asp:PlaceHolder>
         </table>
<table width="100%" border="0" class="margin_T16">
	          <tr>
		        <td>
        <div>
            <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                margin:0; clear: both">
            </div>    
        </div>
		        </td>
	          </tr>
           </table>
 </div>
 </form>
</asp:Content>