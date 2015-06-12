<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FengCai.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.FengCai"
    MasterPageFile="~/MasterPage/M1.Master" Title="员工风采" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox" style="min-height: 550px;">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 员工风采</div>
        <div class="basic_mainT">
            <h5>
                员工风采</h5>
        </div>
        <div class="jifen_main margin_T16 fixed">
            <div class="jifen_leftbox">
                <div class="com_menu">
                    <ul>
                        <li><a href="jianjie.aspx">维诗达简介</a></li>
                        <li><a href="wenhua.aspx">企业文化</a></li>
                        <li><a class="on" href="fengcai.aspx">员工风采</a></li>
                        <li><a href="zhaopin.aspx">招贤纳士</a></li>
                        <li><a href="lianxi.aspx">联系我们</a></li>
                        <li><a href="jianjie.aspx?k=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.旅游度假资质 %>">旅游度假资质</a></li>
                        <li><a href="jianjie.aspx?k=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.广告业务 %>">广告业务</a></li>
                    </ul>
                </div>
            </div>
            <div class="jifen_rightbox">
                <div class="com_content">
                    <ul class="rongyu-list fixed" id="i_ul">
                        <asp:Repeater runat="server" ID="rpt">
                            <ItemTemplate>
                                <li><a href="javascript:void(0)">
                                    <img src="<%#ErpFilepath+Eval("Filepath") %>"><p>
                                        <%#Eval("MingCheng")%></p>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="chakan_more">
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
            $.ajax({ type: "post", cache: false, url: "FengCai.aspx?dotype=more&index=" + c_index, dataType: "json",
                success: function(response) {
                    if (response.result == "0") {
                        c_index = -1;
                        $("#i_more").text("没有更多了");
                    }
                    $("#i_ul").append(response.obj);
                }
            });
        }
    </script>

</asp:Content>
