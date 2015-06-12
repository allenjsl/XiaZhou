<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryView.ascx.cs"
    Inherits="EyouSoft.YlWeb.UserControl.HistoryView" %>
 <%@ OutputCache Duration="36000" VaryByParam="Id" %>
<div class="historybox margin_T16">
    <h3>
        浏览了本产品的顾客还看过</h3>
    <div class="history_list" style="overflow:hidden; height:124px;">
        <span class="left_jt"><a href="javascript:void(0)" id="i_a_historyview_left">
            <img src="/images/history_l.png"></a></span> <span class="right_jt"><a href="javascript:void(0)" id="i_a_historyview_right">
                <img src="/images/histroy_r.png"></a></span>
        <div style="width: 1300px;">
        <ul style="width:auto">
            <asp:Literal ID="litList" runat="server"></asp:Literal>
        </ul>
        </div>
        <div class="clear">
        </div>
    </div>
</div>


<script type="text/javascript">

    $(document).ready(function() {
        $("#i_a_historyview_left").click(function() {
            var ul = $(this).closest("div").find("ul");
            $(ul).stop();
            ul.animate({ marginLeft: "-170px" }, 1000, "swing", function() {
                $(this).css({ marginLeft: "0px" }).find("li:first").appendTo(this);
            });
        });

        $("#i_a_historyview_right").click(function() {
            var ul = $(this).closest("div").find("ul");
            $(ul).stop();
            ul.find("li:last").prependTo(ul);
            ul.css({ marginLeft: "-153px" });
            ul.animate({ marginLeft: "0px" }, 1000, "swing", function() { });
        });
    });

</script>