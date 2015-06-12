<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuc1.ascx.cs" Inherits="EyouSoft.Web.YouLun.WUC.wuc1" %>
<div style="background: none #f6f6f6;" class="tablehead">
    <ul class="fixed">
        <li><s class="orderformicon"></s><a class="ztorderform wuc1_index" id="wuc1_index_0"
            hidefocus="true" href="HuiYuan.aspx?sl=99&t=0"><span>注册会员</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc1_index" id="wuc1_index_1"
            hidefocus="true" href="HuiYuan.aspx?sl=99&t=1"><span>非注册会员</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc1_index" id="wuc1_index_2"
            hidefocus="true" href="HuiYuanDianPing.aspx?sl=99&t=2"><span>会员点评</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc1_index" id="wuc1_index_3"
            hidefocus="true" href="HuiYuanWenDa.aspx?sl=99&t=3"><span>会员问答</span></a></li>
    </ul>
</div>

<script type="text/javascript">
    $(".wuc1_index").removeClass("de-ztorderform");
    function wuc1(index) {
        $("#wuc1_index_" + index).addClass("de-ztorderform");
    }
</script>

