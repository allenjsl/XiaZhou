<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShouHouFuWu.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.ShouHouFuWu"
    MasterPageFile="~/MasterPage/M1.Master" Title="售后服务" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox" style="min-height: 550px;">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 售后服务</div>
        <div class="basic_mainT">
            <h5>
                <asp:Literal runat="server" ID="ltr1"></asp:Literal></h5>
        </div>
        <div class="jifen_main margin_T16 fixed">
            <div class="jifen_leftbox">
                <div class="com_menu">
                    <ul>
                        <li><a href="shouhoufuwu.aspx?s=1" id="i_index_1">售后政策</a></li>
                        <li><a href="shouhoufuwu.aspx?s=2" id="i_index_2">价格保护</a></li>
                        <li><a href="shouhoufuwu.aspx?s=3" id="i_index_3">退款说明</a></li>
                        <li><a href="shouhoufuwu.aspx?s=4" id="i_index_4">取消订单</a></li>
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
