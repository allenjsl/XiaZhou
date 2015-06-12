<%@ Page Title="游轮会议" Language="C#" MasterPageFile="~/MasterPage/Boxy.Master" AutoEventWireup="true"
    CodeBehind="YouLunHuiYi.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyi.YouLunHuiYi" %>

<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="mainbox">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 游轮会议</div>
        <div class="huiyi_box">
            <ul class="huiyi_index fixed">
                <li style="margin-right: 64px;"><a class="img-info" href="/Huiyi/HuiYiDesc.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介 %>">
                    <img width="546" height="323" class="imgborder" id="ImgChangJiang" runat="server"
                        src="/images/cj_img.jpg" alt="" /><div class="name">
                            进入长江游轮会议专区</div>
                </a>
                    <p class="title-txt">
                        长江游轮会议</p>
                    <p>
                        <label runat="server" id="Content_CJ">
                        </label>
                    </p>
                </li>
                <li><a class="img-info" href="/Huiyi/HuiYiDesc.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介 %>">
                    <img width="546" height="323" class="imgborder" runat="server" id="ImgHaiYang"
                        src="/images/hy_img.jpg" alt="" /><div class="name">
                            进入海洋邮轮会议专区</div>
                </a>
                    <p class="title-txt">
                        海洋邮轮会议</p>
                    <p>
                        <label runat="server" id="Content_HY">
                        </label>
                    </p>
                </li>
            </ul>
        </div>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function() { setNav(4); });
    </script>
</asp:Content>
