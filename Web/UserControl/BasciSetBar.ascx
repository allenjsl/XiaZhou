<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BasciSetBar.ascx.cs"
    Inherits="Web.UserControl.BasciSetBar" %>
<div style="background: none #f6f6f6;" class="tablehead">
    <ul class="fixed">
        <%if (PrivsPage[0] == "1")
          {%>
        <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" memuid="1"
            hidefocus="true" href="CityList.aspx?sl=82&memuid=1"><span>城市管理</span></a></li>
        <%} if (PrivsPage[1] == "1")
          {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="2" hidefocus="true"
            href="RouterAreaList.aspx?sl=82&memuid=2"><span>线路区域</span></a></li>
        <%}
          if (PrivsPage[2] == "1")
          {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="3" hidefocus="true"
            href="/SystemSet/TourItemList.aspx?sl=82&type=1&itemtype=1&memuid=3"><span>团队项目管理</span></a></li><%} if (PrivsPage[3] == "1")
        {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="4" hidefocus="true"
            href="/SystemSet/QuoteStandardList.aspx?sl=82&memuid=4"><span>报价标准</span></a></li>
        <%} if (PrivsPage[4] == "1")
        {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="5" hidefocus="true"
            href="/SystemSet/GuestLevelList.aspx?sl=82&memuid=5"><span>客户等级</span></a></li>
        <%} if (PrivsPage[5] == "1")
        {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="6" hidefocus="true"
            href="/SystemSet/PayStyleList.aspx?sl=82&memuid=6"><span>支付方式</span></a></li>
        <%} if (PrivsPage[6] == "1")
        {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="7" hidefocus="true"
            href="/SystemSet/MemberTypeList.aspx?sl=82&memuid=7"><span>会员类型</span></a></li>
        <%} if (PrivsPage[7] == "1")
        {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="8" hidefocus="true"
            href="/SystemSet/InsuranceList.aspx?sl=82&memuid=8"><span>保险类型</span></a></li>
        <%} if (PrivsPage[8] == "1")
        {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="9" hidefocus="true"
            href="/SystemSet/CarPlaceList.aspx?sl=82&memuid=9"><span>上车地点管理</span></a></li>
        <%} if (PrivsPage[9] == "1")
        {%>
        <li><s class="orderformicon"></s><a class="ztorderform" memuid="10" hidefocus="true"
            href="/SystemSet/CarTypeList.aspx?sl=82&memuid=10"><span>车型管理</span></a></li>
        <%} %>
    </ul>
</div>

<script type="text/javascript">
    //当前菜单序号
    var memuid = <%=Request.QueryString["memuid"]==null?"1":Request.QueryString["memuid"]%>;
    $(".tablehead").children(".fixed").children().each(function() {
        var Aobject = $(this).find("a");
        $(Aobject).attr("class", "ztorderform");
        if ($(Aobject).attr("memuid") == memuid) {
            $(Aobject).attr("class", "ztorderform de-ztorderform");
        }
    });
</script>

