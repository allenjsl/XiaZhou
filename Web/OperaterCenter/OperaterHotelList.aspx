<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterHotelList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterHotelList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register TagName="supplierControl" TagPrefix="uc6" Src="~/UserControl/SupplierControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <style type="text/css">
        /*样式重写*/body, html
        {
            overflow: visible;
            width: 100%;
        }
        .jidiao-r
        {
            border-right: 0px;
            border-top: 0px;
        }
    </style>
</head>
<body style="background-color: #fff">
    <form id="Fromhotel" runat="server">
    <asp:HiddenField runat="server" ID="hidroomTypePrices" />
    <input type="hidden" id="hotelStar" runat="server" />
    <div id="con_faq_3">
        <div class="jidiao-r">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b" id="roomtypelist">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span> 酒店名称：
                    </th>
                    <td width="40%">
                        <uc6:supplierControl ID="supplierControl1" runat="server" CallBack="HotelPage._AjaxContectInfo"
                            SupplierType="酒店" IsMust="true" />
                    </td>
                    <th width="15%" align="right">
                        <span class="border-l">星级：</span>
                    </th>
                    <td width="30%">
                        <asp:DropDownList class="inputselect" ID="ddlHotelStart" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        联系人：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContectName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th align="right">
                        联系电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContectPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <p>
                            联系传真：</p>
                    </th>
                    <td>
                        <asp:TextBox ID="txtContectFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th align="right">
                        前台电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtQianTaiTelephone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span> 入住-离店时间：
                    </th>
                    <td>
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="inputtext formsize80" valid="required"
                            errmsg="*请输入入住时间!"></asp:TextBox>至
                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="inputtext formsize80" valid="required"
                            errmsg="*请输入离店时间!"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="border-l"><span class="fontred">*</span>天数：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtroomDays" runat="server" CssClass="inputtext formsize50" valid="required|RegInteger|range"
                            errmsg="*请输入天数!|*天数输入有误!|*天数必须大于0!" min="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span> <a id="a_openInfoDiv" href="javascript:void(0);">房型</a>：
                    </th>
                    <td colspan="3">
                        <table id="tblHolderView" width="100%" border="0" cellspacing="0" cellpadding="0"
                            class="noborder">
                            <asp:Repeater ID="reproomtypelist" runat="server">
                                <ItemTemplate>
                                    <tr class="tempRow">
                                        <td style="padding: 0px;" width="75%">                                            
                                            <select class="inputselect" name="ddlRoomType" valid="required" errmsg="*请选择房型!">
                                                <option value="">--请选择--</option>
                                                <%#GetRoomType(SourceID, Eval("RoomId").ToString(),SueID)%>
                                            </select>
                                            单价
                                            <input type="text" name="txtunitPrice" class="inputtext formsize40" valid="required|isMoney"
                                                errmsg="*请输入单价!|*单价输入有误!"  value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("UnitPrice").ToString()) %>" />
                                            <select name="select" class="inputselect">
                                                <%# Getcalculate(((int)Eval("PriceType")).ToString())%>
                                            </select>
                                            数量
                                            <input type="text" name="txtRoomNumber" class="inputtext formsize40" valid="required|RegInteger"
                                                errmsg="*请输入数量!|*数量必须是正整数!" value="<%# Eval("Quantity") %>" />
                                            入住：<input type="text" name="txtCheckInDate" onfocus="WdatePicker()" value="<%#Eval("CheckInDate","{0:yyyy-MM-dd}") %>"
                                                style="width: 65px;" class="inputtext" />
                                            退房：<input type="text" name="txtCheckOutDate" onfocus="WdatePicker()" value="<%#Eval("CheckOutDate","{0:yyyy-MM-dd}") %>"
                                                style="width: 65px;" class="inputtext" />
                                            天数
                                            <input type="text" name="txtRoomItemDays" class="inputtext formsize40" valid="required|RegInteger"
                                                errmsg="*请输入天数!|*天数必须是正整数!"value="<%# Eval("Days") %>" />
                                            小计：
                                            <input type="text" class="inputtext formsize40" name="txtTotalMoney" valid="required|isMoney"
                                                errmsg="*请输入小计!|*小计输入有误!" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("Total").ToString()) %>" />
                                            元
                                        </td>
                                        <td style="padding: 0px;" width="15%" align="right">
                                            <a class="addbtn" href="javascript:void(0)">
                                                <img height="20" width="48" src="/images/addimg.gif" /></a>&nbsp; <a class="delbtn"
                                                    href="javascript:void(0)">
                                                    <img height="20" width="48" src="/images/delimg.gif" />
                                                </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:PlaceHolder ID="holderView" runat="server">
                                <tr class="tempRow">
                                    <td style="padding: 0px;" width="75%">                                        
                                        <select class="inputselect" style="width: 100px;" name="ddlRoomType" valid="required"
                                            errmsg="*请选择房型!">
                                            <option value="">--请选择--</option>
                                        </select>                                        
                                        单价
                                        <input type="text" name="txtunitPrice" class="inputtext formsize40" valid="required|isMoney"
                                            errmsg="*请输入单价!|*单价输入有误!" />
                                        <select name="select" class="inputselect">
                                            <%=Getcalculate("") %>
                                        </select>
                                        数量
                                        <input type="text" name="txtRoomNumber" class="inputtext formsize40" valid="required|RegInteger"
                                            errmsg="*请输入数量!|*数量必须是正整数!" />
                                        入住：<input type="text" name="txtCheckInDate" onfocus="WdatePicker()" style="width: 65px;"
                                            class="inputtext" />
                                        退房：<input type="text" name="txtCheckOutDate" onfocus="WdatePicker()" style="width: 65px;"
                                            class="inputtext" />
                                             天数
                                            <input type="text" name="txtRoomItemDays" class="inputtext formsize40" valid="required|RegInteger"
                                                errmsg="*请输入天数!|*天数必须是正整数!" value="" />
                                        小计：
                                        <input type="text" class="inputtext formsize40" name="txtTotalMoney" valid="required|isMoney"
                                            errmsg="*请输入小计!|*小计输入有误!" />
                                        元
                                    </td>
                                    <td style="padding: 0px;" width="15%" align="right">
                                        <a class="addbtn" href="javascript:void(0)">
                                            <img height="20" width="48" src="/images/addimg.gif" /></a>&nbsp; <a class="delbtn"
                                                href="javascript:void(0)">
                                                <img height="20" width="48" src="/images/delimg.gif" />
                                            </a>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        房间数量：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPayRoomNumbers" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        间
                    </td>
                    <th align="right">
                        <!--提供免房数量：-->
                    </th>
                    <td>
                        <!--<asp:TextBox ID="txtFreRoomNumber" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        间-->
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span> 是否含早：
                    </th>
                    <td align="left">
                        <asp:DropDownList class="inputselect" ID="ddlContainsEarly" runat="server" valid="required"
                            errmsg="*请选择是否含有早餐!">
                        </asp:DropDownList>
                    </td>
                    <th align="right">
                        <!--早餐费用：-->
                    </th>
                    <td align="left">
                        <!--单价
                        <asp:TextBox ID="txtunitPricesEarly" runat="server" CssClass="inputtext formsize50"></asp:TextBox>
                        元 <strong>*</strong> 人数
                        <asp:TextBox ID="txtPeopleNumEarly" runat="server" CssClass="inputtext formsize40"></asp:TextBox>
                        <strong>*</strong> 次数
                        <asp:TextBox ID="txtsequenceNumEarly" runat="server" CssClass="inputtext formsize40"></asp:TextBox>-->
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>结算费用：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTotalPrices" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                            errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:TextBox>
                        元
                    </td>
                    <th align="right">
                        <span class="fontred">*</span><span class="border-l">支付方式</span>：
                    </th>
                    <td>
                        <select name="SelPanyMent" class="inputselect">
                            <%=UtilsCommons.GetOperaterPanyMentList(PanyMent)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        用房要求：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOtherRemark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        费用明细：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCostParticu" runat="server" CssClass="inputtext formsize600"
                             TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        导游需知：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuidNotes" TextMode="MultiLine" runat="server" CssClass="inputtext formsize600">
酒店名称：
入住-离店时间：
前台电话：
请提前联络。
                        </asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span> 返利：
                    </th>
                    <td>
                        <asp:DropDownList CssClass="inputselect" ID="Selprofit1" runat="server">
                            <asp:ListItem Value="0">有</asp:ListItem>
                            <asp:ListItem Value="1">无</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span><span class="border-l">状态</span>：
                    </th>
                    <td>
                        <select name="SelStatus" class="inputselect">
                            <%=UtilsCommons.GetOperaterStatusList(Status) %>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排酒店</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="actionType">
                    <tr>
                        <th align="center">
                            酒店名称
                        </th>
                        <th align="center">
                            房间数量
                        </th>
                        <!--<th align="center">
                            提供免房数量
                        </th>-->
                        <th align="center">
                            费用明细
                        </th>
                        <th align="center">
                            支付方式
                        </th>
                        <th align="center">
                            结算费用
                        </th>
                        <th align="center">
                            状态
                        </th>
                        <th align="center">
                            确认单
                        </th>
                        <th align="center">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="repHotellist" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Num")%>
                                </td>
                                <!--<td align="center">
                                    <%# Eval("FreeNumber")%>
                                </td>-->
                                <td align="center">
                                    <span title="<%# Eval("CostDetail") %>">
                                        <%# EyouSoft.Common.Utils.GetText2( Eval("CostDetail").ToString(),30,true) %></span>
                                </td>
                                <td align="center">
                                    <%# Eval("PaymentType").ToString()%>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%></b>
                                    <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>">
                                        <img src="/images/yufu.gif" /></a>
                                </td>
                                <td align="center">
                                    <%# Eval("Status").ToString()%>
                                </td>
                                <td align="center">
                                    <a href='<%#querenUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" border="0" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:void(0);" data-class="updateHotel">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />修改</a>
                                    <a href="javascript:void(0);" data-class="deleteHotel">
                                        <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" alt="" />删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:void(0);" data-class="showHotel">
                                        <img src="/images/y-delupdateicon.gif" alt="" data-id="<%# Eval("PlanId") %>" />查看</a>
                                    <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:PlaceHolder>
            <div class="hr_5">
            </div>
            <asp:PlaceHolder ID="panView" runat="server">
                <div class="mainbox cunline fixed">
                    <ul id="ul_btn_list">
                        <li class="cun-cy"><a id="btnSave" href="javascript:">保存</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
        <input type="hidden" id="hidUserNum" runat="server" />
    </div>
    </form>

    <script type="text/javascript">
        var HotelPage = {
            _sl: '<%=SL%>',
            _type: '<%=Utils.GetQueryStringValue("type") %>',
            _tourID: '<%=Utils.GetQueryStringValue("tourId") %>',
            _iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _daysDiff: function() {
                var daysStr = $("#roomtypelist").children().find("tr");
                var startTime = new Date(Date.parse(daysStr.find("#<%=txtStartTime.ClientID %>").val().replace(/-/g,
"/"))).getTime();
                var endTime = new Date(Date.parse(daysStr.find("#<%=txtEndTime.ClientID %>").val().replace(/-/g,
"/"))).getTime();

                var diffDays = 0;
                if (isNaN(startTime) && isNaN(endTime)) {
                    diffDays = 0;
                }
                else if (isNaN(startTime) || isNaN(endTime)) {
                    diffDays = 1;
                }
                else {
                    diffDays = Math.abs((startTime - endTime)) / (1000 * 60 * 60 * 24);
                }
                if (diffDays == 0) {
                    diffDays = 1;
                }
                $("#<%=txtroomDays.ClientID %>").val(diffDays);

            },
            _DeleteHotel: function(objID) {
                var _url = '/OperaterCenter/OperaterHotelList.aspx?sl=' + HotelPage._sl + '&type=' + HotelPage._type + '&tourId=' + HotelPage._tourID + "&iframeId=" + HotelPage._iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterHotelList.aspx?sl=' + HotelPage._sl + '&action=delete&planId=' + objID + '&tourid=' + HotelPage._tourID,
                    cache: false,
                    dataType: 'json',
                    success: function(ret) {
                        tableToolbar._showMsg(ret.msg, function() {
                            window.location.href = _url;
                        });
                        return false;
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _SaveHotel: function(planId, tourId) {
                var _Url = '/OperaterCenter/OperaterHotelList.aspx?sl=' + HotelPage._sl + '&type=' + HotelPage._type + '&tourId=' + HotelPage._tourID + '&iframeId=' + HotelPage._iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/OperaterCenter/OperaterHotelList.aspx?action=save&sl=' + HotelPage._sl + '&planId=' + planId + '&tourId=' + tourId,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: 'json',
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg, function() {
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _planId = '<%=Utils.GetQueryStringValue("planId") %>';
                                    HotelPage._SaveHotel(_planId, HotelPage._tourID);
                                });
                            });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                //入住时间计算               
                $("#<%=txtStartTime.ClientID %>").focus(function() {
                    WdatePicker({ onpicked: HotelPage._daysDiff });
                });
                $("#<%=txtEndTime.ClientID %>").focus(function() {
                    WdatePicker({ onpicked: HotelPage._daysDiff });
                });

                $("#<%=txtStartTime.ClientID %>,#<%=txtEndTime.ClientID %>").blur(function() {
                    HotelPage._daysDiff();
                });

                $("#actionType").find("a[data-class='updateHotel']").unbind("click");
                $("#actionType").find("a[data-class='updateHotel']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterHotelList.aspx?sl=' + HotelPage._sl + '&action=update&planId=' + planId + '&tourId=' + HotelPage._tourID + '&type=' + HotelPage._type + '&iframeId=' + HotelPage._iframeId;
                    }
                    return false;
                });

                //查看
                $("#actionType").find("a[data-class='showHotel']").unbind("click");
                $("#actionType").find("a[data-class='showHotel']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterHotelList.aspx?sl=' + HotelPage._sl + '&action=update&planId=' + planId + '&tourId=' + HotelPage._tourID + '&type=' + HotelPage._type + '&iframeId=' + HotelPage._iframeId + "&show=1";
                    }
                    return false;
                });

                $("#actionType").find("a[data-class='deleteHotel']").unbind("click");
                $("#actionType").find("a[data-class='deleteHotel']").click(function() {
                    var newThis = $(this);
                    tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            HotelPage._DeleteHotel(planId);
                        }
                    });
                    return false;
                });

                $("#actionType").find("a[data-class='Prepaid']").unbind("click");
                $("#actionType").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    HotelPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + HotelPage._type + '&sl=' + HotelPage._sl + '&tourId=' + HotelPage._tourID + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    if (!ValiDatorForm.validator($("#<%=Fromhotel.ClientID %>").get(0), "parent")) {
                        return;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planId") %>';
                        HotelPage._SaveHotel(planId, HotelPage._tourID);
                    }
                });
            },
            _InitTotalIncome: function() {
                //房型小计计算
                var roomToalPrice = 0;
                $("input[type='text'][name='txtTotalMoney']").each(function() {
                    var p = $(this).closest("tr");
                    var _unitPrices = $.trim(p.find("input[type='text'][name='txtunitPrice']").val());
                    var _roomNums = $.trim(p.find("input[type='text'][name='txtRoomNumber']").val());
                    var _days = tableToolbar.getInt(p.find("input[name='txtRoomItemDays']").val());
                    var TotalMoney = tableToolbar.calculate(_unitPrices, _roomNums, "*") * _days;
                    p.find("input[name='txtTotalMoney']").val(TotalMoney);
                    roomToalPrice = roomToalPrice + parseInt(TotalMoney);
                    $("#<%=txtTotalPrices.ClientID %>").val(roomToalPrice);
                });

                //早餐费用计算
                var totalPriceEarly = 0;
                var _UnitPricesEarly = $.trim($("#<%=txtunitPricesEarly.ClientID %>").val());
                var _PeopleNumEarly = $.trim($("#<%=txtPeopleNumEarly.ClientID %>").val());
                var money = tableToolbar.calculate(_UnitPricesEarly, _PeopleNumEarly, "*");
                var _SequenceNumEarly = $.trim($("#<%=txtsequenceNumEarly.ClientID %>").val());
                totalPriceEarly = tableToolbar.calculate(money, _SequenceNumEarly, "*");
                $("#<%=txtTotalPrices.ClientID %>").val(tableToolbar.calculate(roomToalPrice, totalPriceEarly, "+"));
            },
            _AjaxReqHotelRoom: function(Id, type, source, company) {
                $.newAjax({
                    type: "POST",
                    url: "/Ashx/GetSupplierContect.ashx?suppId=" + Id + "&type=" + type + "&source=" + source + "&company=" + company,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.tolist != "") {
                            if (ret.tolist.length > 0) {
                                var html = [], mTp = '<%=ProviderMoneyStr %>';
                                html.push('<tr><th width="75px">房型</th><th width="75px">散客价</th><th width="75px">淡季</th><th width="75px">旺季</th><th width="75px">平季</th><th width="75px">含早</th></tr>');
                                for (var i = 0; i < ret.tolist.length; i++) {
                                    $("#tblHolderView").find("select[name='ddlRoomType']").append("<option value='" + ret.tolist[i].RoomId + "," + ret.tolist[i].TypeName + "'>" + ret.tolist[i].TypeName + "</option>");
                                    var hidVal = $("#<%=hidroomTypePrices.ClientID %>").val() + "" + ret.tolist[i].RoomId + "," +
ret.tolist[i].PriceSK + "|";
                                    $("#<%=hidroomTypePrices.ClientID %>").val(hidVal);
                                    $("input[type='text'][name='txtunitPrice']").val(ret.tolist[0].PriceSK);

                                    if (ret.tolist[i].userNum) {
                                        $("#<%=txtPayRoomNumbers.ClientID %>").val(ret.tolist[i].userNum);
                                        $("#<%=hidUserNum.ClientID %>").val(ret.tolist[i].userNum);
                                    }

                                    html.push('<tr><td>' + ret.tolist[i].TypeName + '</td><td>' + mTp + ret.tolist[i].PriceSK + '</td><td>' + mTp + ret.tolist[i].PriceDJ + '</td><td>' + mTp + ret.tolist[i].PriceWJ + '</td><td>' + mTp + ret.tolist[i].PricePJ + '</td><td>' + (ret.tolist[i].IsBreakfast ? "含" : "不含") + '</td></tr>')
                                }
                            }
                            //从供应商选用显示参考价格
                            if (source == 1) { parent.ConfigPage.ShowListDiv(html.join('')); $("#a_openInfoDiv").click(function() { parent.ConfigPage.ShowListDiv(""); }); } else { $("#a_openInfoDiv").unbind("click"); }
                            //星级
                            $("#<%=ddlHotelStart.ClientID %>").val(ret.star);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _AjaxContectInfo: function(obj) {
                var supplierType = '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 %>';
                var companyId = "<%=this.SiteUserInfo.CompanyId %>";
                if (obj) {
                    $("#<%=supplierControl1.ClientText %>").val(obj.name);
                    $("#<%=supplierControl1.ClientValue %>").val(obj.id);
                    $("#<%=supplierControl1.ClientzyykValue %>").val(obj.hideID_zyyk);
                    $("#<%=supplierControl1.ClientIsyukong %>").val(obj.isYuKong);
                    $("#<%=txtContectName.ClientID %>").val(obj.contactname);
                    $("#<%=txtContectPhone.ClientID %>").val(obj.contacttel);
                    $("#<%=txtContectFax.ClientID %>").val(obj.contactfax);
                    //清空房型下拉框值
                    $("#tblHolderView").find("select[name='ddlRoomType'] option").each(function() {
                        if ($(this).val() != "") {
                            $(this).remove();
                        }
                    });
                    //清空价格隐藏域的值
                    $("#<%=hidroomTypePrices.ClientID %>").val("");
                    if (obj.isYuKong == "0") {
                        if (obj.id != "") {
                            HotelPage._AjaxReqHotelRoom(obj.id, supplierType, "1", companyId);
                        }
                    }
                    else {
                        if (obj.hideID_zyyk != "") {
                            HotelPage._AjaxReqHotelRoom(obj.hideID_zyyk, supplierType, "2", companyId);
                        }
                    }
                    $("#<%=txtQianTaiTelephone.ClientID %>").val(obj.jiuDianQianTaiTelephone);
                }
            },
            _PageInit: function() {
                HotelPage._BindBtn();
                HotelPage._DdlChangeEvent();
                $("#tblHolderView").autoAdd({ addCallBack: parent.ConfigPage.SetWinHeight, delCallBack: parent.ConfigPage.SetWinHeight });
                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }
            },
            _RoomCount: function() {
                var _totalNums = 0;
                $("input[name='txtRoomNumber']").each(function() {
                    var tr = $(this).closest("tr");
                    var thisNums = tr.find("input[name='txtRoomNumber']").val();
                    var _day = tr.find("input[name='txtRoomItemDays']").val();
                    var _num = tableToolbar.calculate(thisNums, _day, "*");
                    _totalNums = tableToolbar.calculate(_totalNums, _num, "+");
                });
                $("#<%=txtPayRoomNumbers.ClientID %>").val(_totalNums);
            },
            _DdlChangeEvent: function() {
                $("#<%=txtunitPricesEarly.ClientID %>,#<%=txtPeopleNumEarly.ClientID %>,#<%=txtsequenceNumEarly.ClientID %>").blur(function() {
                    HotelPage._InitTotalIncome();
                });


                $("input[name='txtRoomNumber'],input[name='txtunitPrice'],input[name='txtRoomItemDays']").blur(function() {
                    HotelPage._InitTotalIncome();
                    HotelPage._RoomCount();
                });

                //房型下拉事件
                $("#tblHolderView").find("select[name='ddlRoomType']").unbind("change");
                $("#tblHolderView").find("select[name='ddlRoomType']").change(function() {
                    var seleId = $(this).find("option:selected").val().split(',')[0];
                    var hidPriceVal = $("input[type='hidden'][name='hidroomTypePrices']").val();
                    if (hidPriceVal != null && hidPriceVal != "") {
                        var priArr = hidPriceVal.split('|');
                        if (priArr.length > 0) {
                            for (var i = 0; i < priArr.length; i++) {
                                if (priArr[i].split(',')[0] == seleId) {
                                    $(this).parent().parent().find("input[type='text'][name='txtunitPrice']").val(priArr[i].split(',')[1]);
                                }
                            }
                        }
                    }
                    HotelPage._InitTotalIncome();
                });
            }
        }


        $(function() {
            HotelPage._PageInit();
            parent.ConfigPage.SetWinHeight();

//            $("#a_openInfoDiv").click(function() {
//                parent.ConfigPage.ShowListDiv("");
//                return false;
//            })
        });
        
    </script>

</body>
</html>
