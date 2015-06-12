<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FLDH.ascx.cs" Inherits="EyouSoft.YlWeb.UserControl.FLDH" %>
 <%@ OutputCache Duration="600" VaryByParam="none"%>
<%if (!this.IsIndex) %>
<%{ %>
<div class="L_side01">
    <div class="basic_leftT">
        <h5>
            分类导航</h5>
    </div>
<%} %>
    <ul class="L_side01box">
        <li>
            <div class="L-menu">
            <div class="line_T">
                <h6>
                    <%=S1 %>航线</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png" class="jiantou"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtHangXian" runat="server"></asp:Literal>
            </div>
            </div>
            <div class="sub-menu<%=MenuClass %>">
                <div class="name">
                        <asp:Repeater ID="rptList_HangXian" runat="server" OnItemDataBound="rptList_HangXian_ItemDataBound">
                            <ItemTemplate>
                                <asp:PlaceHolder ID="phdHangXianCJ" runat="server" Visible="false">
                                <dl>
                                   <dt><%#Eval("BieMing")%></dt>
                                   <asp:Repeater ID="rptHangXian" runat="server">
                                   <ItemTemplate>
                                   <dd><a title="<%# Eval("MingCheng")%>" href="/Hangqi/chaxun.aspx?lx=<%#(int)YouLunLeiXing %>&hx=<%# Eval("XinXiId") %>"><%# Eval("MingCheng")%></a></dd>
                                   </ItemTemplate>
                                   </asp:Repeater>
                                </dl>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="phdHangXianHY" runat="server" Visible="false">
                                <em>
                                <a title="<%# Eval("MingCheng")%>" href="/Hangqi/chaxun.aspx?lx=<%#(int)YouLunLeiXing %>&hx=<%# Eval("XinXiId") %>">
                                    <%# Eval("MingCheng")%></a>
                                </em>
                                </asp:PlaceHolder>
                            </ItemTemplate>
                        </asp:Repeater>
                </div>
                             <%=this.YouHuiLianJie %>
            </div>
        </li>
        <asp:PlaceHolder runat="server" ID="phxl">
            <li>
                <div class="L-menu">
                <div class="line_T">
                    <h6>
                        <%=S1 %>系列</h6>
                    <a href="javascript:;">
                        <img src="/images/jiantouR.png" class="jiantou"></a></div>
                <div class="line_Name">
                    <asp:Literal ID="txtXiLie" runat="server"></asp:Literal>
                </div>
                </div>
                <div class="sub-menu sub-menu02">
                    <div class="name02">
                            <asp:Repeater ID="rptList_Xilie" runat="server">
                                <ItemTemplate>
                               <dl>
                                  <dt><%#Eval("MingCheng")%></dt>
                                  <%#GetChuanZhis(Eval("MingCheng"),Eval("XiLieId")) %>
                               </dl>
                               <%#(Container.ItemIndex+1)%5==0?"<div class=\"clear\"></div>":string.Empty %>
                                </ItemTemplate>
                            </asp:Repeater>
                    </div>
                </div>
            </li>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phgs" Visible="false">
            <li>
                <div class="L-menu">
                <div class="line_T">
                    <h6>
                        <%=S1 %>公司</h6>
                    <a href="javascript:;">
                        <img src="/images/jiantouR.png" class="jiantou"></a></div>
                <div class="line_Name">
                    <asp:Literal ID="txtGongSi" runat="server"></asp:Literal>
                </div>
                </div>
                <div class="sub-menu sub-menu01">
                    <div class="name">
                            <asp:Repeater ID="rptList_GongSi" runat="server">
                                <ItemTemplate>
                                    <em><a title="<%# Eval("MingCheng")%>" href="/Hangqi/chaxun.aspx?lx=<%#(int)YouLunLeiXing %>&gs=<%# Eval("GongSiId") %>">
                                        <%# Eval("MingCheng")%></a></em>
                                </ItemTemplate>
                            </asp:Repeater>
                    </div>
                             <%=this.YouHuiLianJie %>
                </div>
            </li>
        </asp:PlaceHolder>
        <li>
            <div class="L-menu">
            <div class="line_T">
                <h6>
                    <%=S2 %></h6>
                <a href="javascript:void(0)">
                    <img src="/images/jiantouR.png"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtDiZhi" runat="server"></asp:Literal>
            </div>
            </div>
            <div class="sub-menu<%=MenuClass %>">
                <div class="name">
                        <asp:Repeater ID="rptList_DiZhi" runat="server">
                            <ItemTemplate>
                                <em><a href="/Hangqi/chaxun.aspx?lx=<%#(int)YouLunLeiXing %>&gk=<%# Eval("XinXiId") %>">
                                    <%# Eval("MingCheng")%></a></em>
                            </ItemTemplate>
                        </asp:Repeater>
                </div>
                             <%=this.YouHuiLianJie %>
            </div>
        </li>
        <li>
            <div class="L-menu">
            <div class="line_T">
                <h6>
                    行程天数</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png"></a>
            </div>
            <div class="line_Name">
                <a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=1">1-3天</a><a href="/Hangqichaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=2">4-5天</a><a
                    href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=3">6-7天</a><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=4">8-9天</a></div>
            </div>
            <div class="sub-menu<%=MenuClass %>">
                <div class="name">
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=1">1-3天</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=2">4-5天</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=3">6-7天</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&ts=4">8-9天</a></em>
                </div>
                             <%=this.YouHuiLianJie %>
            </div>
        </li>
        <li>
            <div class="L-menu">
            <div class="line_T">
                <h6>
                    出发时间</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png"></a></div>
            <div class="line_Name">
                <asp:Literal ID="txtChuFa" runat="server"></asp:Literal></div>
            </div>
            <div class="sub-menu<%=MenuClass %>">
                <div class="name">
                    <asp:Literal ID="txtChuFaLi" runat="server"></asp:Literal>
                </div>
                             <%=this.YouHuiLianJie %>
            </div>
        </li>
        <li>
            <div class="L-menu">
            <div class="line_T">
                <h6>
                    价格区间</h6>
                <a href="javascript:;">
                    <img src="/images/jiantouR.png"></a>
            </div>
            <div class="line_Name">
                <a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=1">1000元以下</a><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=2">1000-2000元</a><a
                    href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=3">2000-3000元</a></div>
            </div>
            <div class="sub-menu<%=MenuClass %>">
                <div class="name">
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=1">1000元以下</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=2">1000-2000元</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=3">2000-3000元</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=4">3000-4000元</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=5">4000-5000元</a></em>
                    <em><a href="/Hangqi/chaxun.aspx?lx=<%=(int)YouLunLeiXing %>&jg=6">5000元以上</a></em>
                </div>
                             <%=this.YouHuiLianJie %>
            </div>
        </li>
    </ul>
<%if (!this.IsIndex) %>
<%{ %>
</div>
<%} %>


<script type="text/javascript">
//左侧导航
$(document).ready(function(){
	$('.L_side01box > li').mousemove(function(){
	$(this).find('.sub-menu').show();
	if ($.browser.msie) {
        if (parseFloat($.browser.version) <= 6) {
            $(this).find('.sub-menu').bgiframe();
        }
    }
	$(this).addClass('curr');
	});
	$('.L_side01box > li').mouseleave(function(){
	$(this).find('.sub-menu').hide();
	$(this).removeClass('curr');
	});
});
</script>

