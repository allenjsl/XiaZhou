<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiPinKa.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.LiPinKa" MasterPageFile="~/MasterPage/HuiYuan.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user_headbox">
          <div class="user_head"><img src="<%=this.HuiYuanInfo.TuXiang %>" width="104px" height="100px" /></div>
          <div class="user_msg">
	          <p>欢迎您，<font class="font_blue"><%=this.HuiYuanInfo.XingMing %></font></p>
		        <ul>
		          <li>当前积分 <em class="user_jf"><%=this.HuiYuanInfo.KeYongJiFen.ToString("F2") %></em></li>
		          <li><a href="DingDan.aspx">待付款订单 <em class="user_dindan_num"><%=this.HuiYuanInfo.DaiFuKuanDingDanShu %></em></a></li>
        		  
		          <li><a href="YouKe.aspx">常用游客信息管理</a></li>
		          <li style="text-align:right;"><a href="ShouCang.aspx">查看我的收藏<em class="fontgreen"><%=this.HuiYuanInfo.ShouCangShu %></em></a></li>
		        </ul>
          </div>
        </div>

        <div class="menu_T">
         <h3>我的礼品卡</h3>
        </div>

        <div class="user_table">
           <table width="100%" border="0" class="tablelist">
	          <tr>
		        <th align="center">面值</th>
		        <th align="center">数量</th>
		        <th align="center">购买时间</th>
		        <th align="center">购买金额</th>
		        <th align="center">状态</th>
	          </tr>
           <asp:Repeater ID="rpt" runat="server">
               <ItemTemplate>
	          <tr>
		        <td><b class="font14"><%#Eval("JinE1","{0:C2}")%>元</b></td>
		        <td align="center"><b class="font14"><%#Eval("ShuLiang")%></b></td>
		        <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></td>
		        <td align="center"><%#Eval("JinE", "{0:C2}")%></td>
		        <td align="center"><b class='<%#(int)Eval("FuKuanStatus")==(int)EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款?"green font14":"font_yellow font14"%>'><%#Eval("FuKuanStatus")%></b></td>
	          </tr>
                </ItemTemplate>
           </asp:Repeater>
           <asp:PlaceHolder ID="phdNoDat" runat="server" Visible="false">
                   <tr>
                     <td align="center" colspan="5">暂无礼品卡</td>
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
