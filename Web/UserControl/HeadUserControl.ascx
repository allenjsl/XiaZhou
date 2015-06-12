<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeadUserControl.ascx.cs"
    Inherits="Web.UserControl.HeadUserControl" %>
<span class="login-info">您好! <b class="fontbsize12">
    <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></b></span>
<p class="quick-menu">
    <a href="/logout.aspx">安全退出</a><a href="javascript:void(0);">帮助</a>
    <asp:PlaceHolder ID="phPersonCenter" runat="server"><a href="/Default.aspx" id="gerenzx">
        个人中心</a></asp:PlaceHolder>
    <asp:PlaceHolder ID="phTools" runat="server"><a href="javascript:" id="SMS">常用工具</a></asp:PlaceHolder>
</p>

<script type="text/javascript">
        $(function() {
            var result="";
            result='<%=resultPrivs %>';
            $('#gerenzx').bt({
                contentSelector: function() {
                    return "<div class='grzx-navbox fixed'>"+result.split('*')[0]+"<a href='/UserCenter/SelfInfo/UpdatePassword.aspx?sl=1'><s></s>修改密码</a></div>";
                },
                positions: ['bottom'],
                fill: '#f4f4f4',
                strokeStyle: '#137cbf',
                strokeWidth: 2,
                noShadowOpts: { strokeStyle: "#137cbf", strokeWidth: 2 },
                spikeLength: 6,
                spikeGirth: 12,
                width: 200,
                overlap: 0,
                centerPointY: 14,
                cornerRadius: 0,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                padding: 3,
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            });
            /******常用工具******/
            $('#SMS').bt({
                contentSelector: function() {
                return "<div class='grzx-navbox fixed'>"+result.split('*')[1]+"</div>";
                },
                positions: ['bottom'],
                fill: '#f4f4f4',
                strokeStyle: '#137cbf',
                strokeWidth: 2,
                noShadowOpts: { strokeStyle: "#137cbf", strokeWidth: 2 },
                spikeLength: 6,
                spikeGirth: 12,
                width: 162,
                overlap: 0,
                centerPointY: 14,
                cornerRadius: 0,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                padding: 3,
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            });
        })
</script>

