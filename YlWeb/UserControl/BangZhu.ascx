<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BangZhu.ascx.cs" Inherits="EyouSoft.YlWeb.UserControl.BangZhu" %>

<div class="help margin_T16">
    <div class="basic_leftT">
        <h5>
            帮助中心</h5>
    </div>
    <div id="n4Tab" class="n4Tab">
        <div class="TabTitle">
            <ul>
                <li class="active" onmouseover="nTabs('n4Tab',this);" id="n4Tab_Title0"><a href="javascript:void(0);">
                    <img src="/images/icon01.png">
                    问题解答</a></li>
                <li class="normal" onmouseover="nTabs('n4Tab',this);" id="n4Tab_Title1"><a href="javascript:void(0);">
                    <img src="/images/icon02.png">
                    <%=GL%></a></li>
            </ul>
        </div>
        <div class="TabContent">
            <div id="n4Tab_Content0" style="display: block;">
                <ul>
                    <asp:Repeater ID="rptList_WenDa" runat="server">
                        <ItemTemplate>
                            <li><a href="/corp/zixunxx.aspx?s=<%#Eval("ZiXunId") %>">
                                <%#Eval("BiaoTi") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="pHWenDa" Visible="false" runat="server">
                        <li><a href="javascript:;">暂无信息</a></li>
                    </asp:PlaceHolder>
                </ul>
                <div class="chakan_more">
                    <a href="/corp/zixunxx.aspx?t=<%=(int)LeiXing1 %>">查看更多 +</a></div>
            </div>
            <div class="none" id="n4Tab_Content1" style="display: none;">
                <ul>
                    <asp:Repeater ID="rptList_GongLue" runat="server">
                        <ItemTemplate>
                            <li><a href="/corp/zixunxx.aspx?s=<%#Eval("ZiXunId") %>">
                                <%#Eval("BiaoTi") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="pHGongLue" Visible="false" runat="server">
                        <li><a href="javascript:;">暂无信息</a></li>
                    </asp:PlaceHolder>
                </ul>
                <div class="chakan_more">
                    <a href="/corp/zixunxx.aspx?t=<%=(int)LeiXing2 %>">查看更多 +</a></div>
            </div>
        </div>
    </div>
</div>
