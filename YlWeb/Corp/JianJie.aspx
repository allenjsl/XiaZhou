<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JianJie.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.JianJie"
    MasterPageFile="~/MasterPage/M1.Master" Title="关于维诗达" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox" style="min-height: 550px;">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 关于维诗达</div>
        <div class="basic_mainT">
            <h5>
                <%=(EyouSoft.Model.EnumType.YlStructure.WzKvKey)EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("k")) %></h5>
        </div>
        <div class="jifen_main margin_T16 fixed">
            <div class="jifen_leftbox">
                <div class="com_menu">
                    <ul>
                        <li><a class="<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("k"))==(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.维诗达简介?"on":"" %>" href="jianjie.aspx">维诗达简介</a></li>
                        <li><a href="wenhua.aspx">企业文化</a></li>
                        <li><a href="fengcai.aspx">员工风采</a></li>
                        <li><a href="zhaopin.aspx">招贤纳士</a></li>
                        <li><a href="lianxi.aspx">联系我们</a></li>
                        <li><a class="<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("k"))==(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.旅游度假资质?"on":"" %>" href="jianjie.aspx?k=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.旅游度假资质 %>">旅游度假资质</a></li>
                        <li><a class="<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("k"))==(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.广告业务?"on":"" %>"href="jianjie.aspx?k=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.广告业务 %>">广告业务</a></li>
                    </ul>
                </div>
            </div>
            <div class="jifen_rightbox">
                <div class="com_content">
                    <asp:Literal runat="server" ID="ltr"></asp:Literal>                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
