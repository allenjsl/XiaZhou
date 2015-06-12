<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoHuiYi.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyi.XiaoHuiYi"
    MasterPageFile="~/MasterPage/Boxy.Master" Title="游轮会议" %>

<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="mainbox">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 游轮会议</div>
        <div class="huiyi_box" style="min-height: 550px;">
            <div class="huiyi_nav">
                <ul class="fixed">
                    <li><a href="/Huiyi/HuiYiDesc.aspx?type=<%=Request.QueryString["type"] %>" id="i_a_t">海洋游轮会议</a></li>
                    <li><a href="/Huiyi/HuiYiAnLi.aspx?type=<%=Request.QueryString["type"] %>">会议案例精选</a></li>
                    <li><a class="nav_on" href="javascript:;">小型会议接待</a></li>
                    <li><a href="/Huiyi/DaHuiYi.aspx?type=<%=Request.QueryString["type"] %>">大型会议包租</a></li>
                    <li><a href="/Huiyi/YouLunShangWu.aspx?type=<%=Request.QueryString["type"] %>">游轮商务服务</a></li>
                    <li><a href="/Huiyi/YouLunFeiYong.aspx?type=<%=Request.QueryString["type"] %>">游轮费用测算</a></li>
                    <li><a href="/Huiyi/HuiYiShenQing.aspx?type=<%=Request.QueryString["type"] %>">会议申请</a></li>
                </ul>
                <div class="huiyi_nav-R">
                    <a data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介 %>" href="/Huiyi/HuiYiDesc.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介 %>">
                        长江游轮会议</a><a data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介 %>"
                            href="/Huiyi/HuiYiDesc.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介 %>">海洋游轮会议</a></div>
            </div>
            <div class="main fixed" id="n4Tab">
                <div class="C_left">
                    <ul class="xilie_menu">
                        <li><a class="on" onclick="nTabs_01('n4Tab',this);" id="n4Tab_Title0" href="javascript:;">
                            小型贵宾团接待</a></li>
                        <li><a onclick="nTabs_01('n4Tab',this);" id="n4Tab_Title1" href="javascript:;">游轮会议场地选择</a></li>
                        <li><a onclick="nTabs_01('n4Tab',this);" id="n4Tab_Title2" href="javascript:;">服务项目及流程</a></li>
                    </ul>
                </div>
                <div class="huiyi_content02" id="n4Tab_Content0">
                    <p class="huiyi_title02">
                        小型贵宾团接待</p>
                    <asp:Literal ID="lbjiedai" runat="server"></asp:Literal>
                </div>
                <div class="huiyi_content02" id="n4Tab_Content1" style="display: none">
                    <p class="huiyi_title02">
                        游轮会议场地选择</p>
                    <asp:Literal ID="lbxuanze" runat="server"></asp:Literal>
                </div>
                <div class="huiyi_content02" id="n4Tab_Content2" style="display: none">
                    <p class="huiyi_title02">
                        服务项目及流程</p>
                    <asp:Literal ID="lbliucheng" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>

<script type="text/javascript">
    $(function() {
        var type = '<%=Request.QueryString["type"] %>';
        $(".huiyi_nav-R").find("a").each(function() {
            if ($(this).attr("data-type") == type) {
                $(this).addClass("on");
            } else {
                $(this).removeClass("on");
            }
        })
        if (type == "7") {
            $("#i_a_t").text("长江游轮会议");
        }

        setNav(4);
    })
    function nTabs_01(tabObj, obj) {
        var tabList = $("#" + tabObj).find("a");
        for (i = 0; i < tabList.length; i++) {
            if (tabList[i].id == obj.id) {
                $("#" + tabObj + "_Title" + i).attr("class", "on");
                $("#" + tabObj + "_Content" + i).show();
            } else {
                $("#" + tabObj + "_Title" + i).attr("class", "");
                $("#" + tabObj + "_Content" + i).hide();
            }
        }
    }
</script>

</asp:Content>
