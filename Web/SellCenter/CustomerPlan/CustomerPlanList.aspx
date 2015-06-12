<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="CustomerPlanList.aspx.cs" Inherits="Web.SellCenter.CustomerPlanList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divAreaShow" style="display: none;">
        <div class='sk-linelistbox-sx fixed' style="z-index: 2">
            <a href="/SellCenter/CustomerPlan/CustomerPlanList.aspx?sl=<%=Request.QueryString["sl"] %>&dropAreaID=">
                全部关键字</a>
            <asp:Repeater ID="rptKsy" runat="server">
                <ItemTemplate>
                    <a title="<%#Eval("Key").ToString() %>" href="/SellCenter/CustomerPlan/CustomerPlanList.aspx?sl=<%#Request.QueryString["sl"] %>&keyID=<%#Eval("KeyId")%>">
                        <%#EyouSoft.Common.Utils.InputText(Eval("Key").ToString(),5)%>(<%#Eval("TourNum")%>)</a>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Literal ID="licMsg" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="mainbox">
        <form id="form1" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="txtTourID" id="txtTourID" class="inputtext formsize120"
                        value="<%=Request.QueryString["txtTourID"]%>" />
                    线路名称：<input type="text" name="txtAreaName" class="inputtext formsize120" id="txtAreaName"
                        value="<%=Request.QueryString["txtAreaName"]%>" />
                    线路区域：<select name="dropAreaID" id="dropAreaID" class="inputselect">
                        <%=EyouSoft.Common.UtilsCommons.GetAreaLineBySelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("dropAreaID")), SiteUserInfo.CompanyId)%>
                    </select>
                    出团时间：<input type="text" name="txtstardate" id="txtstardate" class="inputtext formsize80"
                        onfocus="WdatePicker()" value="<%=Request.QueryString["txtstardate"]%>">
                    至
                    <input type="text" name="txtenddate" id="txtenddate" class="inputtext formsize80"
                        name="backTime" onfocus="WdatePicker()" value="<%=Request.QueryString["txtenddate"]%>" />
                    <br />
                    天数：<input type="text" name="txtday" id="txtday" class="inputtext formsize40" value="<%=Request.QueryString["txtday"] %>" />
                    销售员：<uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="销售员" />
                    &nbsp;<input type="submit" class="search-btn" value="搜索" />
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    <input type="hidden" id="page" name="page" value="<%=Request.QueryString["page"] %>" />
                </p>
            </span>
        </div>
        </form>
        <div class="tablehead">
            <ul id="ullinesx" class="fixed">
                <li><s class="orderformicon"></s><a id="linesx" class="ztorderform" hidefocus="true"
                    href="javascript:void(0);" bt-xtitle="" title=""><span>线路筛选</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box"  style=" background-color:#fff">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="th-line" align="center" rowspan="2">
                            团号
                        </th>
                        <th class="th-line" align="center" rowspan="2">
                            线路区域
                        </th>
                        <th class="th-line" align="left" rowspan="2">
                            线路名称
                        </th>
                        <th class="th-line" align="center" rowspan="2">
                            发布人
                        </th>
                        <th class="th-line" align="center" rowspan="2">
                            出团日期
                        </th>
                        <th class="th-line" align="center" rowspan="2">
                            天数
                        </th>
                        <th class="th-line" align="center" rowspan="2">
                            价格
                        </th>
                        <th class="th-line" align="center" colspan="4">
                            人数
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            报名
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            订单
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            计调员
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            查看计调
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            状态
                        </th>
                    </tr>
                    <tr>
                        <th align="center" bgcolor="#137CBF" class="th-line nojiacu">
                            预
                        </th>
                        <th align="center" bgcolor="#137CBF" class="th-line nojiacu">
                            留
                        </th>
                        <th align="center" bgcolor="#137CBF" class="th-line nojiacu">
                            实
                        </th>
                        <th align="center" bgcolor="#137CBF" class="th-line nojiacu">
                            剩
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class='<%# Container.ItemIndex%2==0?"":"odd" %>'>
                                <td align="center" class="th-line">
                                    <%#Eval("TourCode")%><span style="display: <%#Eval("IsChange").ToString()=="True"?"":"none;"%>"
                                        class="<%#(Eval("IsChange").ToString()=="True"&&Eval("IsSure").ToString()=="True")?"fontgreen":"fontred"%>">(变)</span>
                                </td>
                                <td align="center" class="th-line">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="left" class="th-line">
                                    <a href="<%# GetPrintUrl(Eval("TourId").ToString())%>" target="_blank">
                                        <%#Eval("RouteName")%></a>
                                </td>
                                <td align="center" class="th-line" data-class="opertorInfo">
                                    <%#GetOperatorInfo(Eval("SourceId"), Eval("OperatorInfo"), Eval("SourceCompanyName"))%>
                                    <div id="infodate" style="display: none;">
                                    </div>
                                </td>
                                <td align="center" class="th-line">
                                    <%# EyouSoft.Common.Utils.GetDateTime(Eval("LDate") == null ? "" : Eval("LDate").ToString()).ToShortDateString()%>
                                </td>
                                <td align="center" class="th-line">
                                    <%#Eval("TourDays")%>
                                </td>
                                <td align="right" class="th-line" data-class="PriceInfo">
                                    <a href="#" id="jiage" data-ispostback="false" data-tourid="<%# Eval("TourId") %>"><b
                                        class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%></b></a>
                                    <div style="display: none;">
                                    </div>
                                </td>
                                <td align="center">
                                    <%#Eval("PlanPeopleNumber")%>
                                </td>
                                <td align="center">
                                    <%#Eval("LeavePeopleNumber")%>
                                </td>
                                <td align="center">
                                    <a href="<%# GetPrintUrlYk(Eval("tourId").ToString()) %>" target="_blank">
                                        <%#Eval("Adults")%><sup class="fontred">+<%#Eval("Childs")%></sup></a>
                                </td>
                                <td align="center">
                                    <%#Eval("PeopleNumberLast")%>
                                </td>
                                <td align="center" class="th-line">
                                    <%#GetHtmlByShouKeState(Eval("tourId"), Eval("TourShouKeStatus"), Eval("SourceId"), Eval("IsCheck"), Eval("TourType").ToString())%>
                                </td>
                                <td align="center" class="th-line">
                                    <a href="OrderPaid.aspx?tourID=<%#Eval("TourId") %>&sl=<%=Request.QueryString["sl"] %>"
                                        title="查看" class="fontblue">共计<%#Eval("OrderCount")%>单</a>
                                </td>
                                <td class="th-line" align="center">
                                    <%# GetTourPlaner(Eval("TourPlaner"))%>
                                </td>
                                <td align="center" data-class="GetJiDiaoIcon" data-tourid="<%# Eval("TourId")%>">
                                    <%# EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.TourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("TourStatus")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server" Text="<tr><td align='center' colspan='16'>暂无报价信息!</td></tr>"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border-top: 0 none;" class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var CustomerPlanList = {
            getAjaxData: function(params) {
                var resultmsg = "";
                var obj = $(params.obj);
                //是否第一次浏览该条数据
                var Ispostback = $.trim(obj.attr("data-ispostback"));
                if (params.objresult == null)//ajax请求数据
                {
                    if (Ispostback == "false") {
                        $.newAjax({
                            type: params.ajaxt,
                            cache: false,
                            url: params.url,
                            dataType: params.ajaxdt,
                            async: false,
                            success: function(ret) {
                                if (ret.result != null && ret.result == "true") {
                                    obj.attr("data-ispostback", "true");
                                    obj.next().html(ret.msg);
                                }
                                resultmsg = ret.msg;
                            },
                            error: function() {
                                if (arguments[1] != null)
                                    resultmsg = EnglishToChanges.Ping(arguments[1]);
                            }
                        });
                    }
                    else
                        resultmsg = $.trim(obj.next().html());
                }
                else
                    resultmsg = $(params.objresult).html();
                return resultmsg;
            },
            BindBT: function(params, newoptions) {
                var options = {};
                var jqueryobj = $(params.obj);
                options = {
                    contentSelector: function() {
                        var strResult = "";
                        strResult = CustomerPlanList.getAjaxData(params);
                        return strResult;
                    },
                    positions: ['bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 5,
                    spikeGirth: 15,
                    width: 220,
                    overlap: 0,
                    centerPointY: 4,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '200%' }
                }
                options = $.extend(options, newoptions);
                jqueryobj.bt(options);
            },
            Data: {
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                ajaxtype: 'AjaxLoadData'
            },
            OpenBaoMing: function(obj) {
                window.location.href = $(obj).attr("href") + "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
            },
            GetStringByArr: function(arr) {
                var list = new Array();
                //遍历按钮返回数组对象
                for (var i = 0; i < arr.length; i++) {
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (arr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(arr[i].find("input[type='checkbox']").val());
                    }
                }
                //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
                return list.join(',');
            },
            BindBtn: function() {
                //线路筛选
                $("#linesx").bt({
                    contentSelector: function() {
                        setTimeout(function() {
                        var h = tableToolbar.getInt($("div[class='tablelist-box']").height());
                            var _h = tableToolbar.getInt($("div[class='bt-content']").height());
                            if (h <= _h) {
                                $("div[class='tablelist-box']").css("height", _h);
                            }
                        }, 100);
                        return $("#divAreaShow").html();
                    },
                    positions: ['bottom'],
                    fill: "#fff",
                    strokeStyle: "#81b7d3",
                    width: "500",
                    spikeLength: "5",
                    noShadowOpts: { "strokeStyle": "#81b7d3", "strokeWidth": "2" },
                    cssStyles: { color: '#00387E', 'line-height': '200%' },
                    overlap: 0,
                    centerPointY: 4,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)'
                });
            }
        }



        $(function() {
            tableToolbar.init({});
            CustomerPlanList.BindBtn();
            //发布人详细信息
            $("#liststyle  tr").each(function() {
                var obj = $(this).find("td[data-class='opertorInfo'] a");
                var argument = obj.attr("data-comandsouid");
                var pa = new params();
                pa.obj = obj;
                pa.url = "CustomerPlanList.aspx?ajaxtype=GetOperatorInfo&argument=" + argument + "&sl=" + CustomerPlanList.Data.sl;
                pa.ajaxdt = "json";
                pa.ajaxt = "get";
                var newoptions = { "positions": ['left'] };
                CustomerPlanList.BindBT(pa, newoptions);
            })


            //气泡价格
            $("#liststyle tr").each(function(i) {
                var obj = $(this).find("td[data-class='PriceInfo'] a");
                if (obj.html() != null && i > 1) {
                    var tourid = $.trim(obj.attr("data-tourid"));
                    var newoptions = { "width": "680" };
                    var pa = new params();
                    pa.obj = obj;
                    pa.ajaxt = "GET";
                    pa.ajaxdt = "json";
                    pa.url = "CustomerPlanList.aspx?ajaxtype=MTourPriceStandard&argument=" + tourid + "&sl=" + CustomerPlanList.Data.sl;
                    CustomerPlanList.BindBT(pa, newoptions);
                }
            });

            //供应商泡泡
            BtFun.InitBindBt("GetJiDiaoIcon");
        })


        //自定义参数
        function params() {
            this.obj = null;
            this.ajaxt = "";
            this.ajaxdt = "";
            this.url = "";
            this.objresult = null; //为null是直接获取对象html，不为null为ajax一部请求数据
        }
        
        
    </script>

</asp:Content>
