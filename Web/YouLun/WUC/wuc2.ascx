<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuc2.ascx.cs" Inherits="EyouSoft.Web.YouLun.WUC.wuc2" %>
<div style="background: none #f6f6f6;" class="tablehead">
    <ul class="fixed">
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_0"
            hidefocus="true" href="WangZhan.aspx?sl=100"><span>网站介绍</span></a></li>
        <%--<li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_1"
            hidefocus="true" href="RongRu.aspx?sl=100"><span>公司荣誉</span></a></li>--%>
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_2"
            hidefocus="true" href="FengCai.aspx?sl=100"><span>员工风采</span></a></li>  
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_3"
            hidefocus="true" href="ZhaoPin.aspx?sl=100"><span>招贤纳士</span></a></li>   
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_4"
            hidefocus="true" href="YouQingLianJie.aspx?sl=100"><span>友情链接</span></a></li>   
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_5"
            hidefocus="true" href="WangZhan1.aspx?sl=100"><span>新手指南</span></a></li>   
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_6"
            hidefocus="true" href="WangZhan2.aspx?sl=100"><span>会员服务</span></a></li>   
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_7"
            hidefocus="true" href="WangZhan3.aspx?sl=100"><span>支付方式</span></a></li>
        <%--<li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_8"
            hidefocus="true" href="WangZhan4.aspx?sl=100"><span>售后服务</span></a></li>       --%>
        <%--<li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_9"
            hidefocus="true" href="WangZhan5.aspx?sl=100"><span>礼品卡</span></a></li>    --%>
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_10"
            hidefocus="true" href="WangZhan6.aspx?sl=100"><span>合同条款</span></a></li>
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_11"
            hidefocus="true" href="WangZhan7.aspx?sl=100"><span>旅客须知</span></a></li>    
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_12"
            hidefocus="true" href="YouQingLianJie.aspx?sl=100&leixing=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.长江优惠信息 %>"><span>长江优惠</span></a></li>   
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_13"
            hidefocus="true" href="YouQingLianJie.aspx?sl=100&leixing=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.海洋优惠信息 %>"><span>海洋优惠</span></a></li>   
        <li><s class="orderformicon"></s><a class="ztorderform wuc2_index" id="wuc2_index_14"
            hidefocus="true" href="YiJianFanKui.aspx?sl=100"><span>意见反馈</span></a></li>   
    </ul>
</div>

<script type="text/javascript">
    $(".wuc2_index").removeClass("de-ztorderform");
    function wuc2(index) {
        $("#wuc2_index_" + index).addClass("de-ztorderform");
    }
</script>
