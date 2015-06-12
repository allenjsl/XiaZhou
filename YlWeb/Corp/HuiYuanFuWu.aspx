<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYuanFuWu.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.HuiYuanFuWu"
    MasterPageFile="~/MasterPage/M1.Master" Title="会员服务" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox" style="min-height:550px;">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 会员服务</div>
        <div class="basic_mainT">
            <h5>
                <asp:Literal runat="server" ID="ltr1"></asp:Literal></h5>
        </div>
        <div class="jifen_main margin_T16 fixed">
            <div class="jifen_leftbox">
                <div class="com_menu">
                    <ul>
                        <li><a href="huiyuanfuwu.aspx?s=1" id="i_index_1">积分兑换</a></li>
                        <li><a href="huiyuanfuwu.aspx?s=2" id="i_index_2">积分使用</a></li>
                        <li><a href="huiyuanfuwu.aspx?s=3" id="i_index_3">取消订单</a></li>
                        <li><a href="huiyuanfuwu.aspx?s=4" id="i_index_4">退款说明</a></li>
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

    <script type="text/javascript">
        $(document).ready(function() { $("#i_index_<%=OnIndex %>").addClass("on"); });
    </script>

</asp:Content>
