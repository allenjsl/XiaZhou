<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuc4.ascx.cs" Inherits="EyouSoft.Web.YouLun.WUC.wuc4" %>
<div style="background: none #f6f6f6;" class="tablehead">
    <ul class="fixed">
        <li><s class="orderformicon"></s><a class="ztorderform wuc4_index" id="wuc4_index_0"
            hidefocus="true" href="JiFenDuiHuan.aspx?sl=105"><span>积分商品管理</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc4_index" id="wuc4_index_1"
            hidefocus="true" href="JiFenDingDan.aspx?sl=105"><span>兑换订单管理</span></a></li>
    </ul>
</div>

<script type="text/javascript">
    $(".wuc4_index").removeClass("de-ztorderform");
    function wuc4(index) {
        $("#wuc4_index_" + index).addClass("de-ztorderform");
    }
</script>
