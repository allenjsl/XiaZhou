<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XingChengDan.aspx.cs" Inherits="EyouSoft.YlWeb.Print.XingChengDan" MasterPageFile="~/MasterPage/Print.Master" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table width="696" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center" class="xch_bigT"> <%=this.MingCheng %></td>
  </tr>
</table>


<div class="xch_box">
  <div class="xch-T">行程安排</div>
  
  <asp:Repeater ID="rpt" runat="server">
  <ItemTemplate>
  <dl>
     <dt>第<font class="day_color"><%#Eval("Tian") %></font>天 <%#Eval("QuJian1") %></dt>
     <dd><%#Eval("NeiRong") %> <%#string.IsNullOrEmpty(Eval("Zao").ToString() + Eval("Zhong").ToString() + Eval("Wan").ToString() + Eval("ZhuSu").ToString()) ? "" : "【 " + Eval("Zao").ToString() + " " + Eval("Zhong").ToString() + " " + Eval("Wan").ToString() + " " + Eval("ZhuSu").ToString() + " 】"%> </dd>
     <dd class="fontb">交通工具：<%#Eval("JiaoTongGongJu") %></dd>
  </dl>
  </ItemTemplate>
  </asp:Repeater>
 
</div>

<div class="xch_box">
  <div class="xch-T">费用说明</div>
  
  <%=this.FeiYongShuoMing %>

</div>

<%if (this.YouLunLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮) %>
<%{ %>
<div class="xch_box">
  <div class="xch-T">签证/签注</div>
  
  <%=this.QianZhengQianZhu%>

</div>
<%} %>

<div class="xch_box">
  <div class="xch-T">预订须知</div>
  
  <%=this.YuDingXuZhi %>

</div>

<div class="xch_box">
  <div class="xch-T">友情提示</div>
  
  <%=this.YouQingTiShi %>

</div>

<%if (this.YouLunLeiXing != EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮) %>
<%{ %>
<div class="xch_box">
  <div class="xch-T">游轮攻略</div>
  
  <%=this.YouLunGongLue%>

</div>
<%} %>

</asp:Content>
