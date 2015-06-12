<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuc3.ascx.cs" Inherits="EyouSoft.Web.YouLun.WUC.wuc3" %>
<div style="background: none #f6f6f6; height:62px;" class="tablehead">
    <ul class="fixed">
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_0"
            hidefocus="true" href="HuiYi.aspx?sl=103"><span>长江游轮会议</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_1"
            hidefocus="true" href="HuiYi1.aspx?sl=103"><span>长江游轮小型会议</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_2"
            hidefocus="true" href="HuiYi2.aspx?sl=103"><span>长江游轮大型会议</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_3"
            hidefocus="true" href="HuiYi3.aspx?sl=103"><span>长江游轮商务服务</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_4"
            hidefocus="true" href="HuiYi4.aspx?sl=103"><span>海洋邮轮会议</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_5"
            hidefocus="true" href="HuiYi5.aspx?sl=103"><span>海洋邮轮小型会议</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_6"
            hidefocus="true" href="HuiYi6.aspx?sl=103"><span>海洋邮轮大型会议</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_7"
            hidefocus="true" href="HuiYi7.aspx?sl=103"><span>海洋邮轮商务服务</span></a></li><br />
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_8"
            hidefocus="true" href="HuiYi8.aspx?sl=103"><span>会议申请管理</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc3_index" id="wuc3_index_9"
            hidefocus="true" href="HuiYi9.aspx?sl=103"><span>会议案例</span></a></li>
    </ul>
</div>

<script type="text/javascript">
    $(".wuc3_index").removeClass("de-ztorderform");
    function wuc3(index) {
        $("#wuc3_index_" + index).addClass("de-ztorderform");
    }
</script>

