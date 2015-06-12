<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYiAnLi.aspx.cs" Inherits="EyouSoft.YlWeb.YouLunMeet.HuiYiAnLi"
    MasterPageFile="~/MasterPage/Boxy.Master" Title="���ֻ���" %>

<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="mainbox">
        <div class="basicT">
            ����λ�ã�άʫ������ > ���ֻ���</div>
        <div class="huiyi_box" style="min-height: 550px;">
            <div class="huiyi_nav">
                <ul class="fixed">
                    <li><a href="YouLunHuiYi.aspx">�������ֻ���</a></li>
                    <li><a class="nav_on" href="/Huiyi/HuiYiAnLi.aspx?type=<%=Request.QueryString["type"] %>">���鰸����ѡ</a></li>
                    <li><a href="/Huiyi/XiaoHuiYi.aspx?type=<%=Request.QueryString["type"] %>">С�ͻ���Ӵ�</a></li>
                    <li><a href="/Huiyi/DaHuiYi.aspx?type=<%=Request.QueryString["type"] %>">���ͻ������</a></li>
                    <li><a href="/Huiyi/YouLunShangWu.aspx?type=<%=Request.QueryString["type"] %>">�����������</a></li>
                    <li><a href="/Huiyi/YouLunFeiYong.aspx?type=<%=Request.QueryString["type"] %>">���ַ��ò���</a></li>
                    <li><a href="/Huiyi/HuiYiShenQing.aspx?type=<%=Request.QueryString["type"] %>">��������</a></li>
                </ul>
                <div class="huiyi_nav-R">
                    <a data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>" href="/Huiyi/HuiYiAnLi.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>">
                        �������ֻ���</a><a data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>"
                            href="/Huiyi/HuiYiAnLi.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>">�������ֻ���</a></div>
            </div>
            <div class="huiyi_casebox">
                <asp:Repeater ID="Repeater1" runat="server" 
                    onitemdatabound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <div class="huiyi_caselist fixed">
                            <div class="hy_caseL">
                                <em><%# Container.DataItem %></em>��</div>
                            <div class="hy_caseR leftBorder">
                                <ul>
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <li><a href="javascript:void 0;">
                                                <img src="<%#ErpFilepath+ Eval("FilePath")%>" /><p>
                                                    <%# Eval("MingCheng")%></p>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
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
        });
        setNav(4);
    })
</script>
</asp:content>
