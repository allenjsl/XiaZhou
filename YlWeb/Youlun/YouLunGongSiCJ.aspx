<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Boxy.Master" AutoEventWireup="true"
    CodeBehind="YouLunGongSiCJ.aspx.cs" Inherits="EyouSoft.YlWeb.Youlun.YouLunGongSiCJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox">
        <div class="main fixed">
            <div class="C_left">
                <div class="left_sub" style="display:none;">
                    <div class="left_T fixed">
                        <h5>
                            为什么选择皇家加勒比邮轮?
                        </h5>
                    </div>
                    <div class="left_cont">
                        <ul class="xuanze_box">
                            <li>船只新<br />
                                安全新颖大</li>
                            <li>船只大<br />
                                设施多、活动多多</li>
                            <li>船只多<br />
                                航程多样全年销售好</li>
                            <li>服务好<br />
                                唯一拥有船陪服务</li>
                        </ul>
                    </div>
                </div>
                <div class="left_sub margin_T16" style="display:none;">
                    <div class="left_T fixed">
                        <h5>
                            皇家加勒比邮轮荣誉</h5>
                    </div>
                    <div class="left_cont">
                        <ul class="question_list">
                            <li><a href="#">>“最佳豪华邮轮”奖</a></li>
                            <li><a href="#">>“最佳会奖活动邮轮”奖</a></li>
                            <li><a href="#">>“亚洲旅游系列大奖”中荣获“最佳邮轮公司”奖</a></li>
                        </ul>
                    </div>
                </div>
                <div class="left_sub">
                    <div class="left_T fixed">
                        <h5>
                            常见问题</h5>
                        <a href="/corp/zixun.aspx?t=1" class="more">更多>></a></div>
                    <div class="left_cont">
                        <ul class="question_list">
                            <asp:Repeater ID="rptWT" runat="server">
                                <ItemTemplate>
                                   <li><a href="/corp/zixunxx.aspx?s=<%#Eval("ZiXunId") %>">
                                <%#Eval("BiaoTi") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <div class="left_sub margin_T16">
                    <div class="left_T">
                        <h5>
                            合作游轮公司</h5>
                    </div>
                    <div class="left_cont">
                        <ul class="company_list">
                            <asp:Repeater ID="rpthezuos" runat="server">
                                <ItemTemplate>
                                    <li style="overflow:hidden;"><a href="/youlun/YouLunGongSiCJ.aspx?id=<%# Eval("GongSiId") %>" title="<%#Eval("MingCheng") %>">
                                        <img src="<%#EyouSoft.YlWeb.TuPian.F1(ErpFilepath+Eval("Logo"),90,28) %>" style="width: 90px;
                                            height: 28px;" /><p>
                                            <%# Eval("MingCheng") %></p>
                                    </a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="youlun_list">
                        <ul>
                            <asp:Literal runat="server" ID="ltrGongSi2"></asp:Literal>
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
            <div class="C_right">
                <div class="Bright_box">
                    <div class="right_T">
                        <asp:Label ID="lblgongsi" runat="server" Text=""></asp:Label></div>
                    <div class="company_txt">
                        <p>
                            <asp:Literal ID="litInfo" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
                <asp:Repeater ID="rptxilies" runat="server">
                    <ItemTemplate>
                        <div class="Bright_box margin_T16">
                            <div class="right_T right_T02">
                                <%# Eval("MingCheng")%></div>
                            <div class="right_piclist">
                                <ul>
                                    <%# getYLbyXL(Eval("CompanyId").ToString(), Eval("XiLieId").ToString())%>
                                </ul>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
