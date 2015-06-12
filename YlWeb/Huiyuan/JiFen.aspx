<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiFen.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.JiFen" MasterPageFile="~/MasterPage/HuiYuan.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user_headbox">
          <div class="user_head"><img src="<%=TuXiang %>" width="104px" height="100px" /></div>
          <div class="user_msg">
	          <p>欢迎您，<font class="font_blue"><%=this.HuiYuanInfo.XingMing %></font></p>
		        <ul>
		          <li>当前积分 <em class="user_jf"><%=KeYongJiFen.ToString("F2") %></em></li>
		          <li><a href="DingDan.aspx">待付款订单 <em class="user_dindan_num"><%=DaiFuKuanDingDanShu %></em></a></li>
        		  
		          <li><a href="YouKe.aspx">常用游客信息管理</a></li>
		          <li style="text-align:right;"><a href="ShouCang.aspx">查看我的收藏<em class="fontgreen"><%=ShouCangShu %></em></a></li>
		        </ul>
          </div>
        </div>

        <div class="menu_T">
         <h3>我的积分</h3>
        </div>

        <div class="user_table">
           <table width="100%" border="0" class="tablelist">
	          <tr>
	            <th align="left">订单</th>
		        <th align="center">购买时间</th>
		        <th align="center">订单金额</th>
		        <th align="center">获得积分</th>
	          </tr>
	          <asp:Literal runat="server" ID="ltr0"></asp:Literal>
           <asp:PlaceHolder ID="phdNoDat" runat="server" Visible="false">
                   <tr>
                     <td align="center" colspan="4">暂无积分</td>
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
