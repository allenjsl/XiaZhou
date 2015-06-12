<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JiFenHotList.ascx.cs"
    Inherits="EyouSoft.YlWeb.UserControl.JiFenHotList" %>
<div class="jifen_rank margin_T16">
    <h3>
        兑换排行榜</h3>
    <div class="jifen_rank_list">
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <dl onmouseover="show_king_list(this,<%#Container.ItemIndex+1 %>);" id="a<%#Container.ItemIndex+1 %>" class="i_jinfen_hot">
                    <dt class="sl01"><%#Container.ItemIndex+1 %></dt>
                    <dt class="sl02"><a href="<%#"/jifen/jifeninfo.aspx?id="+Eval("ShangPinId") %>">
                        <img alt="<%#Eval("MingCheng") %>" src="<%#ImageView(Eval("FuJians")) %>"></a></dt>
                    <dd class="sl03">
                        <a href="<%#"/jifen/jifeninfo.aspx?id="+Eval("ShangPinId") %>"><%#Eval("MingCheng")%></a></dd>
                </dl>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
