<%@ Page Title="长江游轮" Language="C#" MasterPageFile="~/MasterPage/Boxy.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="YlWeb._Default" %>
<%@ Register Src="/UserControl/NavLeft.ascx" TagName="NavLeft" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/GuanZhu.ascx" TagName="GuanZhu" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/BangZhu.ascx" TagName="BangZhu" TagPrefix="uc3" %>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<%@ Register Src="/UserControl/FLDH.ascx" TagName="FLDH" TagPrefix="uc3" %>
<asp:Content ID="ctHead" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox fixed">
        <div class="leftside">
            <uc3:FLDH ID="FLDH1" runat="server" YouLunLeiXing="长江游轮" />
            <style type="text/css">
            .hq_riliboxR {padding:10px 0 10px 14px;}
            .hq_riliboxR li {padding-right:20px;width:108px;height:40px;}
            .hq_riliboxR li a {font-size:20px;height:38px;line-height:38px;width:106px;}
            </style>
            <div class="basic_rightT" style="margin-top:10px;"><h5>游轮航期</h5></div>
            <div class="hq_riliboxR">
                <ul>
                <%for (var m = DateTime.Now; m <= DateTime.Now.AddMonths(11); m=m.AddMonths(1)) %>
                <%{ %>
                   <li><a href="/HangQi.aspx?y=<%=m.Year %>&m=<%=m.Month %>" class="<%=DateTime.Now.Month==m.Month?"on":"" %>" onmouseover="<%=DateTime.Now.Month==m.Month?"":"$(this).addClass('on')" %>" onmouseout="<%=DateTime.Now.Month==m.Month?"":"$(this).removeClass()" %>"><%=string.Format("{0:00}",m.Month) %>月</a></li>
                <%} %>
                </ul>
                <div class="clear"></div>
            </div>
            <div class="L_side02 margin_T16">
                <div class="basic_leftT">
                    <h5>
                        热销排行榜</h5>
                </div>
                
                <div style="height: 525px; overflow: hidden; position: relative;" id="divHotSellList">
                
                </div>
                
                <div class="caozuo_area">
                    <a id="caozuo_area_down" href="javascript:">
                        <img src="../images/y_jiantouB.png"></a><a id="caozuo_area_up" href="javascript:void(0);"
                            style="background: #D0D2D1;"><img src="../images/y_jiantouT.png"></a>
                </div>

            </div>
            <div class="L_side03 margin_T16">
                <div class="basic_leftT">
                    <h5>
                        合作游轮公司</h5>
                    <a class="more" href="/youlun/gongsis.aspx">查看更多</a></div>
                <%--<div class="youlun_name">皇家加勒比国际游轮公司</div>
                <div class="youlun_img"><img width="400" height="150" src="../images/22.jpg"></div>--%>
                <asp:Literal runat="server" ID="ltrGongSi1"></asp:Literal>
                <div class="youlun_list">
                    <ul>
                        <%--<asp:Repeater ID="rptList_GongSi" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:void(0)">
                                    <img src="<%#Eval("Logo")%>" class="i_erp_img"></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder ID="PhGongsi" Visible="false" runat="server">
                            <li>暂无信息</li>
                        </asp:PlaceHolder>--%>
                        <asp:Literal runat="server" ID="ltrGongSi2"></asp:Literal>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <uc3:GuanZhu ID="GuanZhu1" runat="server" />
            <uc3:BangZhu ID="BangZhu1" runat="server" />
        </div>
        <div class="rightside">
            <div id="divProductTeJiaList"></div>
            <div id="divProductHotList"></div>            
            <div class="jindiantu">
                <img width="820" height="234" src="../images/jindiantu.jpg"></div>
        </div>
    </div>
    </form>
    
    <script type="text/javascript">
        $(document).ready(function() {
            setNav(1);

            $("#caozuo_area_down").click(function() {
                var ul = $("#i_ul_rexiao");
                $(ul).stop();
                ul.animate({ marginTop: "-165px" }, 1000, "swing", function() {
                    $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this);
                });
            });

            $("#caozuo_area_up").click(function() {
                var ul = $("#i_ul_rexiao");
                $(ul).stop();
                ul.find("li:last").prependTo(ul);
                ul.css({ marginTop: "-160px" });
                ul.animate({ marginTop: "0px" }, 1000, "swing", function() { });
            });

            $.ajax({
                type: "get",
                cache: true,
                url: "/CommonPage/ProductTeJia.aspx?TypeId=<%=(int)EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 %>",
                dataType: "html",
                success: function(ret) {
                    $("#divProductTeJiaList").html(ret);
                },
                error: function() {
                }
            });
            $.ajax({
                type: "get",
                cache: true,
                url: "/Ashx/GetHotSell.ashx?LeiXing=<%=(int)EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 %>",
                dataType: "html",
                success: function(ret) {
                    $("#divHotSellList").html(ret);
                },
                error: function() {
                }
            });
            $.ajax({
                type: "get",
                cache: true,
                url: "/CommonPage/ProductHot.aspx?TypeId=<%=(int)EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 %>",
                dataType: "html",
                success: function(ret) {
                      $("#divProductHotList").html(ret);
                },
                error: function() {
                }
            });            
        });
    </script>
    <%--<script language="javascript" type="text/javascript">var _kc_userID="193821";</script><script language="JavaScript" type="text/javascript" src="http://eyclick.kkeye.com/js/stat.js"></script>--%>
</asp:Content>
