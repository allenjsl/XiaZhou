<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuc5.ascx.cs" Inherits="EyouSoft.Web.YouLun.WUC.wuc5" %>
<div style="background: none #f6f6f6;" class="tablehead">
    <ul class="fixed">
        <li><s class="orderformicon"></s><a class="ztorderform wuc5_index" id="wuc5_index_0"
            hidefocus="true" href="LiPinKa.aspx?sl=106"><span>礼品卡管理</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc5_index" id="wuc5_index_1"
            hidefocus="true" href="LiPinKaDingDan.aspx?sl=106"><span>礼品卡订单管理</span></a></li>
    </ul>
</div>

<script type="text/javascript">
    $(".wuc5_index").removeClass("de-ztorderform");
    function wuc5(index) {
        $("#wuc5_index_" + index).addClass("de-ztorderform");
    }
</script>

