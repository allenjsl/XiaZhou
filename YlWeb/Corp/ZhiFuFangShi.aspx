<%@ Page Language="C#" MasterPageFile="~/MasterPage/M1.Master" AutoEventWireup="true" CodeBehind="ZhiFuFangShi.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.ZhiFuFangShi" Title="支付方式" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox" style="min-height: 550px;">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 支付方式</div>
        <div class="basic_mainT">
            <h5>
                <asp:Literal runat="server" ID="ltr1"></asp:Literal></h5>
        </div>
        <div class="jifen_main margin_T16 fixed">
            <div class="jifen_leftbox">
                <div class="com_menu">
                    <ul>
                        <li><a href="zhifufangshi.aspx?s=1" id="i_index_1">在线支付</a></li>
                        <li><a href="zhifufangshi.aspx?s=2" id="i_index_2">门店支付</a></li>
                        <li><a href="zhifufangshi.aspx?s=3" id="i_index_3">淘宝支付</a></li>
                        <li><a href="zhifufangshi.aspx?s=4" id="i_index_4">银行转账</a></li>
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
