<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="EyouSoft.YlWeb.map" Title="网站地图" MasterPageFile="~/MasterPage/Boxy.Master"%>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageIndex" runat="server">
         <div class="step_mainbox fixed">
         
            <div class="basicT"> 您的位置：维诗达游轮 > 网站地图</div>
            
            <div class="map_bar">
               <div class="map_barT">网站地图</div>
               <div class="map_box">
                  <dl>
                     <dt>长江游轮</dt>
                     <asp:Repeater ID="rptxl" runat="server" OnItemDataBound="rptxl_ItemDataBound">
                     <ItemTemplate>
                     <dd class="botline fixed">
                        <span><%#Eval("MingCheng")%></span>
                        <asp:Repeater ID="rptxld" runat="server">
                        <ItemTemplate>
                        <a href="/hangqi/chaxun.aspx?lx=0&xl=<%#Eval("xilieid") %>&cz=<%#Eval("chuanzhiid") %>"><%#Eval("mingcheng") %></a>
                        </ItemTemplate>
                        </asp:Repeater>
                     </dd>
                     </ItemTemplate>
                     </asp:Repeater>                     
                  </dl>
                  
                  <dl>
                     <dt>海洋邮轮</dt>
                     <dd class="fixed">
                     <asp:Repeater ID="rptgs" runat="server">
                     <ItemTemplate>
                        <a href="/hangqi/chaxun.aspx?lx=1&gs=<%#Eval("gongsiid") %>"><%#Eval("mingcheng") %></a>                     
                     </ItemTemplate>
                     </asp:Repeater>
                     </dd>
                  </dl>
                  
                  <dl>
                     <dt>新手指南</dt>
                     <dd class="fixed">
                        <a href="/corp/xinshouzhinan.aspx?s=1">如何订票</a>
                        <a href="/corp/xinshouzhinan.aspx?s=2">如何取票</a>
                        <a href="/corp/xinshouzhinan.aspx?s=3">游船旅行准备</a>
                        <a href="/corp/xinshouzhinan.aspx?s=4">旅行注意事项</a>
                     </dd>
                  </dl>

                  <dl>
                     <dt>会员服务</dt>
                     <dd class="fixed">
                        <a href="/corp/huiyuanfuwu.aspx?s=1">积分兑换</a>
                        <a href="/corp/huiyuanfuwu.aspx?s=2">积分使用</a>
                        <a href="/corp/huiyuanfuwu.aspx?s=3">取消订单</a>
                        <a href="/corp/huiyuanfuwu.aspx?s=4">退款说明</a>
                     </dd>
                  </dl>

                  <dl>
                     <dt>支付方式</dt>
                     <dd class="fixed">
                        <a href="/corp/zhifufangshi.aspx?s=1">在线支付</a>
                        <a href="/corp/zhifufangshi.aspx?s=2">门店支付</a>
                        <a href="/corp/zhifufangshi.aspx?s=3">淘宝支付</a>
                        <a href="/corp/zhifufangshi.aspx?s=4">银行转账</a>
                     </dd>
                  </dl>

                  <dl>
                     <dt>关于维诗达</dt>
                     <dd class="fixed">
                        <a href="/corp/jianjie.aspx">维诗达简介</a>
                        <a href="/corp/wenhua.aspx">企业文化</a>
                        <a href="/corp/fengcai.aspx">员工风采</a>
                        <a href="/corp/zhaopin.aspx">招贤纳士</a>
                     </dd>
                  </dl>
                  
               </div>
            </div>            
            
         </div>
</asp:Content>
