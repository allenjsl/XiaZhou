<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterConfigPage.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterConfigPage" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register TagName="OperaterMenu" TagPrefix="uc1" Src="/UserControl/OperaterMenu.ascx" %>
<%@ Register TagName="OperaterPanyment" TagPrefix="uc2" Src="/UserControl/OperaterPanyment.ascx" %>
<%@ Register TagName="OperaterState" TagPrefix="uc3" Src="/UserControl/OperaterState.ascx" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    
    <div class="mainbox mainbox-whiteback">
        <div class="hr_10">
        </div>
        <div class="jd-mainbox fixed">
            <uc1:operatermenu id="OperaterMenu1" runat="server" />
            <div class="jdcz-main" style="background-color: #fff">
                <div class="hr_10">
                </div>
                <div class="jidiao-r">
                    <div class="bt">
                        <strong>
                            <img src="/images/jidiao-jt.gif" class="kai" alt="" />
                            团队信息 </strong>
                    </div>
                    <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01" id="zhanlai">
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                团号：
                            </th>
                            <td width="40%">
                                <asp:Literal ID="litTourCode" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                线路区域：
                            </th>
                            <td width="30%">
                                <asp:Literal ID="litAreaName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                线路名称：
                            </th>
                            <td>
                                <asp:Literal ID="litRouteName" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                天数：
                            </th>
                            <td>
                                <asp:Literal ID="litDays" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                出团时间：
                            </th>
                            <td>
                                <asp:Literal ID="litStartDate" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                人数：
                            </th>
                            <td>
                                <asp:Literal ID="litPeoples" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                回团时间：
                            </th>
                            <td>
                                <asp:Literal ID="litEndDate" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                带团导游：
                            </th>
                            <td>
                                <asp:Literal ID="litGuidNames" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                本团销售：
                            </th>
                            <td>
                                <asp:Literal ID="litSellers" runat="server"></asp:Literal>
                            </td>
                            <th align="right">
                                本团计调：
                            </th>
                            <td>
                                <asp:Literal ID="litOperaters" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                需安排计调项：
                            </th>
                            <td colspan="3">
                                <%=strOperaterPlanHtml%>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                <img src="/images/deng.gif" width="18px" height="22px" />
                                内部信息：
                            </th>
                            <td style="" colspan="3" align="left">
                                <asp:Literal ID="LitInterInfo" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                成本核算：
                            </th>
                            <td style="" colspan="3" align="left">
                                <asp:Literal ID="litCostCalculation" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <div class="bt">
                        <strong>
                            <img src="/images/jidiao-jt.gif" class="kai" alt="" />
                            行程安排 </strong>
                    </div>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01 line-b"
                        style="display: none">
                        <tbody>
                            <tr>
                                <th width="50" align="center" class="border-l">
                                    日期
                                </th>
                                <th align="center">
                                    区间
                                </th>
                                <th align="center">
                                    交通
                                </th>
                                <th align="center">
                                    住宿
                                </th>
                                <th align="center">
                                    用餐
                                </th>
                                <th align="center">
                                    景点
                                </th>
                            </tr>
                        </tbody>
                        <tbody>
                            <asp:Repeater ID="repSchedulePlan" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td rowspan="2" align="center" class="border-l">
                                            D<%# Eval("Days")%><br />
                                            <b class="fontbsize12">
                                                <%# Convert.ToDateTime(dt.AddDays(Convert.ToInt32(Eval("Days"))-1)).ToString("M/dd")%></b>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Section")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Traffic")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("Hotel")%>
                                        </td>
                                        <td align="center">
                                            <%# Convert.ToBoolean(Eval("Breakfast"))==true?"早":""%>
                                            <%#Convert.ToBoolean(Eval("Lunch"))==true?"中":""%>
                                            <%# Convert.ToBoolean(Eval("Supper"))==true?"晚":""%>
                                        </td>
                                        <td align="center" width="165px">
                                            <%# GetTourPlanSpot(Eval("TourPlanSpot"))%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="left">
                                            <%# Eval("Content")%>
                                        </td>
                                        <td align="center">
                                            <%# string.IsNullOrEmpty(Eval("FilePath").ToString()) ? "" : "<img src=\"" + Eval("FilePath").ToString() + "\" width=\"145\" height=\"105\" class=\"addpic\" alt=\"\" />"%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <div class="bt">
                        <strong>
                            <img src="/images/jidiao-jt.gif" class="kai" alt="" />
                            对外报价 </strong>
                    </div>
                    <div style="display: none;" id="tabViewForeignQuote">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                            style="border-bottom: 1px solid #A9D7EC; display: none;">
                            <tr>
                                <th width="100" align="center" class="border-l">
                                    项目
                                </th>
                                <th align="center">
                                    服务标准（分项）
                                </th>
                                <th align="center">
                                    单项报价
                                </th>
                            </tr>
                            <asp:Repeater ID="repQuoteList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" class="border-l">
                                            <%# Eval("ServiceType").ToString() %>
                                        </td>
                                        <td align="left">
                                            <%# Eval("ServiceStandard")%>
                                        </td>
                                        <td align="center">
                                            <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Quote"),ProviderToMoney)%>元/人
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01" style="border-bottom: 1px solid #A9D7EC;
                            display: none;">
                            <tr>
                                <th width="100" align="center" class="border-l">
                                    服务标准(整团)
                                </th>
                                <td>
                                    <asp:Literal ID="litServerStandard" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                            style="border-bottom: 1px solid #A9D7EC; display: none;">
                            <tr>
                                <td class="border-l" align="left" bgcolor="#F0F9FD" style="border-top: 0;">
                                    <img src="/images/chengren.gif" width="16" height="15" align="absmiddle" />
                                    成人价 ：<strong><asp:Literal ID="litAdultPrices" runat="server"></asp:Literal>
                                    </strong>元/人 &nbsp;&nbsp;
                                    <img src="/images/child.gif" align="absmiddle" />
                                    儿童价 ：<strong><asp:Literal ID="litChilrenPrices" runat="server"></asp:Literal></strong>
                                    元/人&nbsp;&nbsp;&nbsp;其它费用：&nbsp; <strong>
                                        <asp:Literal ID="litOtherPrices" runat="server"></asp:Literal></strong> 元/团&nbsp;&nbsp;&nbsp;&nbsp;<span
                                            class="fontred">*</span>合计价格： <strong>
                                                <asp:Literal ID="litAccountPrices" runat="server"></asp:Literal></strong>
                                    元<font color="#FF0000"></font>
                                </td>
                            </tr>
                        </table>
                        <%=TourPriceStandardHtml.ToString()%>
                        <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b" style="height: 5px;">
                            <asp:PlaceHolder runat="server" ID="phFWBZ" Visible="false">
                            <tr>
                                <th width="100" align="center" class="border-l" id="AccountCount">
                                    服务标准
                                </th>
                                <td>
                                    <asp:Literal ID="litCostAccount" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <th align="center" class="border-l" width="100">
                                    不含项目
                                </th>
                                <td>
                                    <asp:Literal ID="litObjectItem" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th align="center" class="border-l">
                                    购物安排
                                </th>
                                <td>
                                    <asp:Literal ID="litShoppPlan" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th align="center" class="border-l">
                                    儿童安排
                                </th>
                                <td>
                                    <asp:Literal ID="litChildrenPlan" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th align="center" class="border-l">
                                    自费项目
                                </th>
                                <td>
                                    <asp:Literal ID="litExpenceObj" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th align="center" class="border-l">
                                    注意事项
                                </th>
                                <td>
                                    <asp:Literal ID="litAttenTion" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th align="center" class="border-l">
                                    温馨提醒
                                </th>
                                <td>
                                    <asp:Literal ID="litWenxinTix" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="bt">
                        <strong>
                            <img src="/images/jidiao-jt.gif" class="kai" alt="" />
                            订单列表 </strong>
                    </div>
                    <table width="100%" id="tourOrderList" border="0" cellspacing="0" cellpadding="0"
                        class="jd-table01" style="border-bottom: 1px solid #A9D7EC; display: none">
                        <tr>
                            <th align="center" class="border-l">订单号</th>
                            <th align="center">下单人</th>
                            <th align="center">下单时间</th>
                            <th style="text-align:center;">销售员</th>
                            <th align="center">客源单位</th>
                            <th style="text-align: center;">人数</th>
                            <th align="left">&nbsp;&nbsp;确认合同金额</th>
                            <th align="center">查看</th>
                        </tr>
                        <asp:Repeater ID="repTourOrderList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center" class="border-l">
                                        <a href='<%#OrderUrl %>?tourId=<%# Eval("TourId") %>&tourType=<%#(int)((EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType),Eval("TourType").ToString())) %>' target="_blank"><%# Eval("OrderCode")%></a>
                                    </td>
                                    <td align="center"><%# Eval("Operator")%></td>
                                    <td align="center"><%# Eval("IssueTime")%></td>
                                    <td style="text-align:center;"><%#Eval("SellerName") %></td>
                                    <td align="center"><%# Eval("BuyCompanyName")%></td>
                                    <td style="text-align:center;"><%#Eval("Adults") %>+<%#Eval("Childs")%></td>
                                    <td style="text-align:left;">
                                        &nbsp;&nbsp;<b class="fontred"><%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), ProviderToMoney)%></b>                                        
                                        &nbsp;<%#GetBaoJiaInfo(Eval("TourType"), Eval("KeHuLevName"), Eval("BaoJiaBiaoZhunName"))%>
                                    </td>
                                    <td align="center">
                                        <a data-class="checkOrder" data-orderid="<%# Eval("OrderId") %>" href="javascript:void(0);">查看</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder runat="server" ID="phEmptyDingDan">
                        <tr>
                            <td colspan="8" style="text-align:center;">暂无订单信息</td>
                        </tr>
                        </asp:PlaceHolder>
                    </table>
                    <asp:PlaceHolder ID="planItemView" runat="server" Visible="true">
                        <h2 id="secTable">
                            <p>
                                <asp:PlaceHolder ID="holerView5" runat="server"><a class="hover" data-id="5">地接安排</a>
                                    <span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView10" runat="server"><a data-id="10">导游安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView1" runat="server"><a data-id="1">酒店安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView2" runat="server"><a data-id="2">用车安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView9" runat="server"><a data-id="9">大交通</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView3" runat="server"><a data-id="3">景点安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView4" runat="server"><a data-id="4">游轮安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView6" runat="server"><a data-id="6">用餐安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView7" runat="server"><a data-id="7">购物安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView8" runat="server"><a data-id="8">领料安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView11" runat="server"><a data-id="11">其它安排</a></asp:PlaceHolder>
                            </p>
                        </h2>
                        <div id="boxy-wrapper" style="position: relative;">
                            <div id="divShowList" class="floatbox" style="position: absolute;">
                                <div class="tlist">
                                    <a id="a_closeDiv" class="closeimg" href="javascript:void(0);">关闭</a>
                                    <table width="99%" border="0" id="tblItemInfoList">
                                    </table>
                                </div>
                            </div>
                            <iframe id="boxIframeId" class="iframeUrl" style="width: 100%" scrolling="no" frameborder="0"
                                src="/OperaterCenter/OperaterAyencyList.aspx?sl=<%=Utils.GetQueryStringValue("sl") %>&tourId=<%=Utils.GetQueryStringValue("tourId") %>&iframeId=boxIframeId">
                            </iframe>
                        </div>
                    </asp:PlaceHolder>
                    <div class="hr_10">
                    </div>
                </div>
            </div>
        </div>
        <div class="hr_10">
        </div>
    </div>
    
    <script type="text/javascript" src="/js/jquery.easydrag.handler.beta2.js"></script>

    <script type="text/javascript">
        var ConfigPage = {
            sl: '<%=Utils.GetQueryStringValue("sl") %>',
            tourID: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: 'boxIframeId',
            planID: '<%=Utils.GetQueryStringValue("planID") %>',
            pID: '<%=Utils.GetQueryStringValue("pID") %>',
            //修改操作
            actionType: '<%=Utils.GetQueryStringValue("action") %>',
            //折叠图标 内容隐藏，显示
            _imgIcoClick: function(obj) {
                if (obj.next().is(":hidden")) {
                    obj.find("img").attr("class", "guan").attr("src", "/images/jidiao-jtx.gif");
                    obj.next().fadeIn("slow");
                }
                else {
                    obj.find("img").attr("class", "kai").attr("src", "/images/jidiao-jt.gif");
                    obj.next().hide();
                }
            },
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _iframeUrlFun: function(objId, Url) {
                $(".iframeUrl").attr("src", Url);
                objId.addClass("hover");
                objId.siblings("a").removeClass("hover");
            },
            _Url: function(pageName) {
                return "/OperaterCenter/" + pageName + ".aspx?sl=" + ConfigPage.sl + "&tourId=" + ConfigPage.tourID + "&iframeId=" + ConfigPage.iframeId + "&m=" + new Date().getTime();
            },
            _UrlGlob: function(pageName, PlanId, show) {
                return "/OperaterCenter/" + pageName + ".aspx?sl=" + ConfigPage.sl + "&tourId=" + ConfigPage.tourID + "&action=" + ConfigPage.actionType + "&" + PlanId + "=" + ConfigPage.planID + "&iframeId=" + ConfigPage.iframeId + "&typeId=" + ConfigPage.pID + "&show=" + show + "&m=" + new Date().getTime();
            },
            _BindBtn: function() {
                //折叠图标
                $("div.bt").unbind("click");
                $("div.bt").click(function() {
                    ConfigPage._imgIcoClick($(this));
                    return false;
                });
                //订单查看
                $("#tourOrderList").find("[data-class='checkOrder']").unbind("click");
                $("#tourOrderList").find("[data-class='checkOrder']").click(function() {
                    var orderID = $(this).attr("data-OrderID");
                    ConfigPage._OpenBoxy("订单查看", '/CommonPage/OrderBaoZhang.aspx?OrderId=' + orderID + "&doType=show", "990px", "380px", true);
                    return false;
                });
                //iframeUrl
                $("#secTable p").find("a").unbind("click");
                $("#secTable p").find("a").click(function() {
                    var _dataAID = $(this).attr("data-id");
                    $("#tblItemInfoList").html("");
                    $("#divShowList").hide();
                    if (_dataAID) {
                        switch (_dataAID) {
                            case "11":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterOtherList'));
                                break;
                            case "1":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterHotelList'));
                                break;
                            case "2":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterCarList'));
                                break;
                            case "3":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterAttractionsList'));
                                break;
                            case "4":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterShipList'));
                                break;
                            case "5":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterAyencyList'));
                                break;
                            case "6":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterDiningList'));
                                break;
                            case "7":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterShoppingList'));
                                break;
                            case "8":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterPickingList'));
                                break;
                            case "9":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterLargeList'));
                                break;
                            case "10":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('OperaterGuiderList'));
                                break;
                            default: break;
                        }
                    }
                    return false;
                });

            },
            SetWinHeight: function() {
                var _opiframe = document.getElementById("boxIframeId");
                if (_opiframe && !window.opera) {
                    if (_opiframe.contentDocument && _opiframe.contentDocument.body.offsetHeight) {
                        _opiframe.height = _opiframe.contentDocument.body.offsetHeight;
                    }
                    else if (_opiframe.Document && _opiframe.Document.body.scrollHeight) {
                        _opiframe.height = _opiframe.Document.body.scrollHeight;
                    }
                }

            },
            _dataInit: function() {
                ConfigPage._BindBtn();
                //tourType 组团 散拼
                if ('<%=TourType %>' == "1" || '<%=TourType %>' == "3" || '<%=TourType %>' == "5" || '<%=TourType %>' == "7") {
                    //隐藏对外报价
                    $("#tabViewForeignQuote").find("table:eq(0)").hide();
                    $("#tabViewForeignQuote").find("table:eq(1)").hide();
                    $("#tabViewForeignQuote").find("table:eq(2)").hide();

                } else {
                    $("#tabViewForeignQuote").find("table:eq(2)").show();
                    //TourQType 1:分项 0:整团
                    if ('<%=BaoJiaType %>' == "1") {
                        $("#tabViewForeignQuote").find("table:eq(0)").show();
                        $("#tabViewForeignQuote").find("table:eq(1)").hide();
                    } else {
                        $("#tabViewForeignQuote").find("table:eq(1)").show();
                        $("#tabViewForeignQuote").find("table:eq(0)").hide();
                    }
                }

                //全局计调修改按钮控制
                var _this = $("#secTable p").find("a");
                var _s = '<%=EyouSoft.Common.Utils.GetQueryStringValue("show") %>';
                setTimeout(function() {
                    if (ConfigPage.actionType == "updateAir") {
                        ConfigPage.SetWinHeight();
                        ConfigPage._iframeUrlFun(_this.eq(4), ConfigPage._UrlGlob("OperaterLargeList", "planIdAir", _s));
                    }
                    if (ConfigPage.actionType == "updateTrain") {
                        ConfigPage.SetWinHeight();
                        ConfigPage._iframeUrlFun(_this.eq(4), ConfigPage._UrlGlob("OperaterLargeList", "planIdTrainId", _s));
                    }
                    if (ConfigPage.actionType == "updateBus") {
                        ConfigPage.SetWinHeight();
                        ConfigPage._iframeUrlFun(_this.eq(4), ConfigPage._UrlGlob("OperaterLargeList", "palnIdBus", _s));
                    }
                    if (ConfigPage.actionType == "updateC") {
                        ConfigPage.SetWinHeight();
                        ConfigPage._iframeUrlFun(_this.eq(6), ConfigPage._UrlGlob("OperaterShipList", "planId", _s));
                    }

                    if (ConfigPage.actionType == "update") {
                        ConfigPage.SetWinHeight();
                        if (ConfigPage.pID != "" && ConfigPage.planID != "") {
                            switch (ConfigPage.pID) {
                                case "1":
                                    ConfigPage._iframeUrlFun(_this.eq(2), ConfigPage._UrlGlob("OperaterHotelList", "PlanId", _s));
                                    break;
                                case "2":
                                    ConfigPage._iframeUrlFun(_this.eq(3), ConfigPage._UrlGlob("OperaterCarList", "PlanId", _s));
                                    break;
                                case "3":
                                    ConfigPage._iframeUrlFun(_this.eq(5), ConfigPage._UrlGlob("OperaterAttractionsList", "PlanId", _s));
                                    break;
                                case "4_1":
                                    ConfigPage._iframeUrlFun(_this.eq(6), ConfigPage._UrlGlob("OperaterShipList", "planId", _s));
                                    break;
                                case "5":
                                    ConfigPage._iframeUrlFun(_this.eq(0), ConfigPage._UrlGlob("OperaterAyencyList", "PlanId", _s));
                                    break;
                                case "6":
                                    ConfigPage._iframeUrlFun(_this.eq(7), ConfigPage._UrlGlob("OperaterDiningList", "PlanId", _s));
                                    break;
                                case "7":
                                    ConfigPage._iframeUrlFun(_this.eq(8), ConfigPage._UrlGlob("OperaterShoppingList", "PlanId", _s));
                                    break;
                                case "8":
                                    ConfigPage._iframeUrlFun(_this.eq(9), ConfigPage._UrlGlob("OperaterPickingList", "PlanId", _s));
                                    break;
                                case "10":
                                    ConfigPage._iframeUrlFun(_this.eq(1), ConfigPage._UrlGlob("OperaterGuiderList", "PlanId", _s));
                                    break;
                                case "11":
                                    ConfigPage._iframeUrlFun(_this.eq(10), ConfigPage._UrlGlob("OperaterOtherList", "PlanId", _s));
                                    break;
                                default: break;
                            }
                        }
                    };
                }, 500);
            },
            ShowListDiv: function(html) {
                if (html != "") {
                    $("#tblItemInfoList").html(html);
                    $("#divShowList").fadeIn("fast");
                } else {
                    if ($("#tblItemInfoList").find("tr").length > 0) {
                        $("#divShowList").fadeIn("fast");
                    }
                }

                $("#tblItemInfoList").find("tr").eq(0).attr("id", "i_tr_EasydragHandler");
                $("#divShowList").setHandler("i_tr_EasydragHandler"); //set easydrag handler
            }
        };
        $(document).ready(function() {
            ConfigPage._dataInit();
            $("#a_closeDiv").click(function() { $("#divShowList").fadeOut("fast"); return false; });
            $("#divShowList").easydrag(); //easydrag
        });
    </script>

</asp:Content>
