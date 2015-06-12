<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavLeft.ascx.cs" Inherits="EyouSoft.YlWeb.UserControl.NavLeft" %>
<div class="L_side01">
    <div class="basic_leftT">
        <h5>
            分类导航</h5>
    </div>
    <ul class="L_side01box">
        <li data-class="dh">
            <div class="line_T">
                <h6>
                    <%=S1 %>航线</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png" class="jiantou"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtHangXian" runat="server"></asp:Literal>
            </div>
            <div style="display: none;" class="linemore li0">
                <div class="txt">
                    <ul>
                        <asp:Repeater ID="rptList_HangXian" runat="server">
                            <ItemTemplate>
                                <li><a title="<%# Eval("MingCheng")%>" href="/Hangqi/HangQiList.aspx?hx=<%# Eval("XinXiId") %>">
                                    <%# Eval("MingCheng")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </li>
        <asp:PlaceHolder runat="server" ID="phxl">
        <li data-class="dh">
            <div class="line_T">
                <h6>
                    <%=S1 %>系列</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png" class="jiantou"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtXiLie" runat="server"></asp:Literal>
            </div>
            <div style="display: none;" class="linemore li1">
                <div class="txt">
                    <ul>
                        <asp:Repeater ID="rptList_Xilie" runat="server">
                            <ItemTemplate>
                                <li><a title="<%# Eval("MingCheng")%>" href="/Hangqi/HangQiList.aspx?xl=<%# Eval("XiLieId") %>">
                                    <%# Eval("MingCheng")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </li>
        </asp:PlaceHolder>
        
        <asp:PlaceHolder runat="server" ID="phgs" Visible="false">
        <li data-class="dh">
            <div class="line_T">
                <h6>
                    <%=S1 %>公司</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png" class="jiantou"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtGongSi" runat="server"></asp:Literal>
            </div>
            <div style="display: none;" class="linemore li1">
                <div class="txt">
                    <ul>
                        <asp:Repeater ID="rptList_GongSi" runat="server">
                            <ItemTemplate>
                                <li><a title="<%# Eval("MingCheng")%>" href="/Hangqi/HangQiList.aspx?gs=<%# Eval("GongSiId") %>">
                                    <%# Eval("MingCheng")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </li>
        </asp:PlaceHolder>
        
        <li data-class="dh">
            <div class="line_T">
                <h6>
                    <%=S2 %></h6>
                <a href="javascript:void(0)">
                    <img src="/images/jiantouR.png"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtDiZhi" runat="server"></asp:Literal> </div>
            <div style="display: none;" class="linemore li2">
                <div class="txt">
                    <ul>
                        <asp:Repeater ID="rptList_DiZhi" runat="server">
                            <ItemTemplate>
                                <li><a href="/Hangqi/HangQiList.aspx?dz=<%# Eval("XinXiId") %>">
                                    <%# Eval("MingCheng")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </li>
        <li>
            <div class="line_T">
                <h6>
                    行程天数</h6>
                </div>
            <div class="line_Name">
                <a href="/Hangqi/HangQiList.aspx?xcs=1&xce=3">1-3天</a><a href="/Hangqi/HangQiList.aspx?xcs=4&xce=5">4-5天</a><a href="/Hangqi/HangQiList.aspx?xcs=6&xce=7">6-7天</a><a href="/Hangqi/HangQiList.aspx?xcs=8&xce=9">8-9天</a></div>
        </li>
        <li data-class="dh">
            <div class="line_T">
                <h6>
                    出发时间</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtChuFa" runat="server"></asp:Literal></div>
             <div style="display: none;" class="linemore li4">
                <div class="txt">
                    <ul>
                       <asp:Literal ID="txtChuFaLi" runat="server"></asp:Literal>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </li>
        <li>
            <div class="line_T">
                <h6>
                    价格区间</h6>
                </div>
            <div class="line_Name">
                <a href="/Hangqi/HangQiList.aspx?jgs=0&jge=1000">1000元以下</a><a href="/Hangqi/HangQiList.aspx?jgs=1000&jge=2000">1000-2000元</a><a href="/Hangqi/HangQiList.aspx?jgs=2000&jge=3000">2000-3000元</a><a href="/Hangqi/HangQiList.aspx?jgs=4000&jge=">4000元以上</a></div>
        </li>
    </ul>
</div>

<script src="/js/city.js"></script>

