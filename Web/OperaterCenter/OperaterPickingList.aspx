<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterPickingList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterPickingList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/UserControl/SelectObject.ascx" TagName="SelectObject" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="hrSelect" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

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
    <form id="formpick" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_10">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>领料内容：
                    </th>
                    <td width="40%">
                        <uc1:SelectObject ID="SelectObject1" runat="server" SetTitle="选择物品" CallBackFun="PickPage._SelectObj" />
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT"><span class="fontred">*</span> 数量：</span>
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtNums" runat="server" CssClass="inputtext formsize50" valid="required|RegInteger|range"
                            errmsg="*请输入数量!|*数量必须是正整数!|*数量必须大于0!" min="1"></asp:TextBox>
                        <input type="hidden" name="hidObjNums" id="hidObjNums" />
                        个
                        <label id="SurplusNumTip" style="display: none; color: #999">
                            (剩余数量:<span style="color: #E23030" id="SurplusNum"></span>个)</label>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>单价：
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtUnitPrices" runat="server" CssClass="inputtext formsize50" valid="required|isMoney|range"
                            errmsg="*请输入单价!|*单价输入有误!|*单价必须大于0!" min="1"></asp:TextBox>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>结算费用：
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtTotalPrices" runat="server" CssClass="inputtext formsize50" valid="required|isMoney"
                            errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:TextBox>
                        元
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>领料人：
                    </th>
                    <td colspan="3">
                        <uc3:hrSelect ID="hrSelect" runat="server" CallBackFun="PickPage._AjaxPickName" SetTitle="领料人" />
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        导游需知：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuidNotes" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        其它备注：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOtherRemarks" TextMode="MultiLine" runat="server" CssClass="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        支付方式：
                    </th>
                    <td>
                        <select name="selPanyMent" class="inputselect">
                            <%=UtilsCommons.GetOperaterPanyMentList(panyMent)%>
                        </select>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>状态：
                    </th>
                    <td>
                        <select name="selStatus" class="inputselect">
                            <%=UtilsCommons.GetOperaterStatusList(status)%>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排领料</p>
                </h2>
                <table width="100%" id="actionType" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;">
                    <tr>
                        <th align="center" class="border-l">
                            领料内容
                        </th>
                        <th align="center">
                            领料人
                        </th>
                        <th align="center">
                            数量
                        </th>
                        <th align="center">
                            单价
                        </th>
                        <th align="center">
                            结算费用
                        </th>
                        <th align="center">
                            支付方式
                        </th>
                        <th align="center">
                            状态
                        </th>
                        <th align="center">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="repPickList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Num")%>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString( Eval("Price").ToString())%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetMoneyString(Eval("Confirmation"),ProviderToMoney)%>
                                    <a href="javascript:" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>">
                                        <img src="/images/yufu.gif" alt="" /></a>
                                </td>
                                <td align="center">
                                    <%# Eval("PaymentType").ToString()%>
                                </td>
                                <td align="center">
                                    <%# Eval("Status").ToString()%>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:" data-class="update">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" />
                                        修改</a> <a href="javascript:" data-class="delete">
                                            <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" />
                                            删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:" data-class="show">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" />
                                        查看</a>
                                    <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="hr_5">
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="panView" runat="server">
                <div class="mainbox cunline fixed">
                    <ul id="ul_btn_list">
                        <li class="cun-cy"><a href="javascript:" id="btnSave">保存</a> </li>
                    </ul>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var PickPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _Delete: function(_ObjID) {
                var _Url = '/OperaterCenter/OperaterPickingList.aspx?sl=' + PickPage.sl + '&type=' + PickPage.type + '&tourId=' + PickPage.tourId + "&iframeId=" + PickPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterPickingList.aspx?sl=' + PickPage.sl + '&action=delete&planId=' + _ObjID + PickPage.tourId,
                    cache: false,
                    dataType: 'json',
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _Save: function(tourId, planId) {
                var _Url = '/OperaterCenter/OperaterPickingList.aspx?sl=' + PickPage.sl + '&type=' + PickPage.type + '&tourId=' + PickPage.tourId + "&iframeId=" + PickPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: "/OperaterCenter/OperaterPickingList.aspx?action=save&tourId=" + tourId + "&planId=" + planId + "&sl=" + PickPage.sl,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _Url;
                            });
                        } else {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                $("#btnSave").text("保存").css("background-position", "0 0px").bind("click");
                                $("#btnSave").click(function() {
                                    $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                                    var _planId = '<%=Utils.GetQueryStringValue("planId") %>';
                                    PickPage._Save(PickPage.tourId, _planId);
                                });
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                $("#actionType").find("a[data-class='Prepaid']").unbind("click");
                $("#actionType").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    PickPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + PickPage.type + '&sl=' + PickPage.sl + '&tourId=' + PickPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#actionType").find("a[data-class='delete']").unbind("click");
                $("#actionType").find("a[data-class='delete']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planID = newThis.find("img").attr("data-ID");
                        if (planID) {
                            PickPage._Delete(planID);
                        }
                    });
                    return false;
                });

                $("#actionType").find("a[data-class='update']").unbind("click");
                $("#actionType").find("a[data-class='update']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterPickingList.aspx?type=' + PickPage.type + '&sl=' + PickPage.sl + '&tourId=' + PickPage.tourId + '&action=update&planId=' + planId + "&iframeId=" + PickPage.iframeId;
                    }
                    return false;
                });

                $("#actionType").find("a[data-class='show']").unbind("click");
                $("#actionType").find("a[data-class='show']").click(function() {
                    var planId = $(this).find("img").attr("data-ID");
                    if (planId) {
                        window.location.href = '/OperaterCenter/OperaterPickingList.aspx?type=' + PickPage.type + '&sl=' + PickPage.sl + '&tourId=' + PickPage.tourId + '&action=update&planId=' + planId + "&iframeId=" + PickPage.iframeId + "&show=1";
                    }
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    var objNums = $("#<%=txtNums.ClientID %>").val();
                    var stockNums = $("input[type='hidden'][name='hidObjNums']").val();
                    if (parseInt(objNums) - parseInt(stockNums) > 0) {
                        parent.parent.tableToolbar._showMsg("库存不足!");
                        $("#<%=txtNums.ClientID %>").focus();
                        return false;
                    }
                    if (!ValiDatorForm.validator($("#<%=formpick.ClientID %>").get(0), "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planId") %>';
                        PickPage._Save(PickPage.tourId, planId);
                    }
                });
            },
            _TotalPrices: function() {
                var ObjNums = $("#<%=txtNums.ClientID %>").val();
                var ObjPrices = $("#<%=txtUnitPrices.ClientID %>").val();
                $("#<%=txtTotalPrices.ClientID %>").val(tableToolbar.calculate(ObjNums, ObjPrices, "*"));
            },
            _PageInit: function() {
                this._BindBtn();

                $("#<%=SelectObject1.SelectNameClient%>").attr("valid", "required").attr("errmsg", "*请选择领料内容!").css("background-color", "#dadada").attr("readonly", "true");
                $("#<%=hrSelect.HrSelectNameClient %>").attr("valid", "required").attr("errmsg", "*请选择领料人!").css("background-color", "#dadada").attr("readonly", "true");

                $("#<%=txtNums.ClientID %>,#<%=txtUnitPrices.ClientID %>").blur(function() {
                    PickPage._TotalPrices();
                });


                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }
            },
            _SelectObj: function(obj) {
                $("#<%=SelectObject1.SelectNameClient %>").val(obj.text);
                $("#<%=SelectObject1.SelectIDClient %>").val(obj.value);
                $("#<%=txtUnitPrices.ClientID %>").val(obj.price);
                $("#SurplusNumTip").show();
                $("#SurplusNum").html(obj.nums);
                $("input[type='hidden'][name='hidObjNums']").val(obj.nums);
                var prices = $.trim($("#<%=txtUnitPrices.ClientID %>").val());
                var nums = $.trim($("#<%=txtNums.ClientID %>").val());
                $("#<%=txtTotalPrices.ClientID %>").val(tableToolbar.calculate(prices, nums, "*"));
            },
            _AjaxPickName: function(obj) {
                $("#<%=hrSelect.HrSelectNameClient %>").val(obj.text);
                $("#<%=hrSelect.HrSelectIDClient %>").val(obj.value);
            }
        }

        $(document).ready(function() {
            PickPage._PageInit();
            parent.ConfigPage.SetWinHeight();
        });
    </script>

</body>
</html>
