<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZhaoPin.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.ZhaoPin"
    MasterPageFile="~/MasterPage/M1.Master" Title="招贤纳士" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox" style="min-height: 550px;">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 招贤纳士</div>
        <div class="basic_mainT">
            <h5>
                招贤纳士</h5>
        </div>
        <div class="jifen_main margin_T16 fixed">
            <div class="jifen_leftbox">
                <div class="com_menu">
                    <ul>
                        <li><a href="jianjie.aspx">维诗达简介</a></li>
                        <li><a href="wenhua.aspx">企业文化</a></li>
                        <li><a href="fengcai.aspx">员工风采</a></li>
                        <li><a class="on" href="zhaopin.aspx">招贤纳士</a></li>
                        <li><a href="lianxi.aspx">联系我们</a></li>
                        <li><a href="jianjie.aspx?k=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.旅游度假资质 %>">旅游度假资质</a></li>
                        <li><a href="jianjie.aspx?k=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.广告业务 %>">广告业务</a></li>
                    </ul>
                </div>
            </div>
            <div class="jifen_rightbox">
                <div class="com_content">
                    <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                    <dl>
                        <dt><%#Eval("MingCheng")%></dt>
                        <dd>
                            <%#Eval("XiangXiJieShao")%>
                        </dd>
                    </dl>
                    </ItemTemplate>
                    </asp:Repeater>
                    <div class="chakan_more" id="i_div_more">
                        <a href="javascript:void(0)" onclick="more();" id="i_more">查看更多 +</a></div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">        
        var c_index = 1;
        function more() {
            if (c_index == -1) return;
            c_index++;
            $.ajax({ type: "post", cache: false, url: "ZhaoPin.aspx?dotype=more&index=" + c_index, dataType: "json",
                success: function(response) {
                    if (response.result == "0") {
                        c_index = -1;
                        $("#i_more").text("没有更多了");
                    }
                    $("#i_div_more").before(response.obj);
                }
            });
        }
    </script>

</asp:Content>
