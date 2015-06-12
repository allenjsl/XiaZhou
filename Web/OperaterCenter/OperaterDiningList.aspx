<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperaterDiningList.aspx.cs"
    Inherits="Web.OperaterCenter.OperaterDiningList" %>

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
    <form id="formDin" runat="server">
    <div class="jidiao-r">
        <div id="con_faq_8">
            <table width="100%" cellpadding="0" cellspacing="0" class="jd-table01 line-b">
                <tr>
                    <th width="15%" align="right" class="border-l">
                        <span class="fontred">*</span>餐馆名称：
                    </th>
                    <td width="40%">
                        <uc6:supplierControl ID="supplierControl1" runat="server" IsMust="true" CallBack="DinPage._AjaxContectInfo"
                            Flag="0" SupplierType="用餐" />
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT">联系人：</span>
                    </th>
                    <td width="30%">
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th width="15%" align="right" class="border-l">
                        联系电话：
                    </th>
                    <td>
                        <asp:TextBox ID="txtContactPhone" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <th width="15%" align="right">
                        <span class="addtableT">联系传真：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtContactFax" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        用餐时间：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtDinningDate" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
                <!--<tr>
                    <th align="right" class="border-l">
                        价格组成：
                    </th>
                    <td colspan="3">
                        <table width="100%" border="0" id="tabDinPriceList" cellspacing="0" cellpadding="0"
                            class="noborder">
                            <asp:PlaceHolder ID="tabViewPrices" runat="server">
                                <tr class="tempRow">
                                    <td width="84%">
                                        <select class="inputselect" name="selAdultType">
                                            <%=GetAdultType("") %>
                                        </select>早
                                        <input name="txtFrequencyB" type="text" class="inputtext" style="width: 20px;" value="" />次
                                        <input name="txtPeopleNumsB" type="text" class="inputtext" style="width: 20px;" value="" />人
                                        <input name="txtPricesB" type="text" style="width: 30px;" class="inputtext" value="" />元&nbsp;&nbsp;
                                        中
                                        <input name="txtFrequencyL" type="text" style="width: 20px;" class="inputtext" value="" />次
                                        <input name="txtPeopleNumsL" type="text" style="width: 20px;" class="inputtext" value="" />人
                                        <input name="txtPricesL" type="text" style="width: 30px;" class="inputtext" value="" />元&nbsp;&nbsp;
                                        晚
                                        <input name="txtFrequencyS" type="text" style="width: 20px;" class="inputtext" value="" />次
                                        <input name="txtPeopleNumsS" type="text" style="width: 20px;" class="inputtext" value="" />人
                                        <input name="txtPricesS" type="text" style="width: 30px;" class="inputtext" value="" />元&nbsp;&nbsp;
                                    </td>
                                    <td width="16%" align="right">
                                        <a href="javascript:void(0)" class="addbtn">
                                            <img height="20" width="48" src="/images/addimg.gif" alt=""></a>&nbsp; <a href="javascript:void(0)"
                                                class="delbtn">
                                                <img height="20" width="48" src="/images/delimg.gif" alt=""></a>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:Repeater ID="repPriceList" runat="server">
                                <ItemTemplate>
                                    <tr class="tempRow">
                                        <td width="84%">
                                            <select class="inputselect" name="selAdultType">
                                                <%#GetAdultType(((int)Eval("Pricetyp")).ToString())%>
                                            </select>
                                            早
                                            <input name="txtFrequencyB" type="text" class="inputtext" style="width: 20px;" value="<%# Eval("TimeB") %>" />次
                                            <input name="txtPeopleNumsB" type="text" class="inputtext" style="width: 20px;" value="<%# Eval("PeopleB") %>" />人
                                            <input name="txtPricesB" type="text" class="inputtext" style="width: 30px;" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("PriceB").ToString())) %>" />元&nbsp;&nbsp;
                                            中
                                            <input name="txtFrequencyL" type="text" class="inputtext" style="width: 20px;" value="<%# Eval("TimeL") %>" />次
                                            <input name="txtPeopleNumsL" type="text" class="inputtext" style="width: 20px;" value="<%# Eval("PeopleL") %>" />人
                                            <input name="txtPricesL" type="text" class="inputtext" style="width: 30px;" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("PriceL").ToString())) %>" />元&nbsp;&nbsp;
                                            晚
                                            <input name="txtFrequencyS" type="text" class="inputtext" style="width: 20px;" value="<%# Eval("TimeS") %>" />次
                                            <input name="txtPeopleNumsS" type="text" class="inputtext" style="width: 20px;" value="<%# Eval("PeopleS") %>" />人
                                            <input name="txtPricesS" type="text" class="inputtext" style="width: 30px;" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(EyouSoft.Common.Utils.GetDecimal(Eval("PriceS").ToString()))%>" />元&nbsp;&nbsp;
                                        </td>
                                        <td width="16%" align="right">
                                            <a href="javascript:void(0)" class="addbtn">
                                                <img height="20" width="48" src="/images/addimg.gif"></a>&nbsp; <a href="javascript:void(0)"
                                                    class="delbtn">
                                                    <img height="20" width="48" src="/images/delimg.gif"></a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>-->
                <tr>
                    <th align="right" class="border-l">
                        费用明细：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtCostDetail" runat="server" class="inputtext formsize600"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right">
                        <span class="fontred">* </span><span class="addtableT">结算费用：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtTotalPrices" runat="server" CssClass="inputtext formsize80" valid="required|isMoney"
                            errmsg="*请输入结算费用!|*结算费用输入有误!"></asp:TextBox>
                        元
                    </td>
                    <th align="right">
                        <span class="addtableT">人数：</span>
                    </th>
                    <td>
                        <asp:TextBox ID="txtPeopleNum" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        导游需知：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtGuidNotes" runat="server" class="inputtext formsize600" 
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        其它备注：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtOtherMarks" runat="server" class="inputtext formsize600" 
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="border-l">
                        <span class="fontred">*</span>支付方式：
                    </th>
                    <td align="left">
                        <select name="selPanyMent" class="inputselect">
                            <%=UtilsCommons.GetOperaterPanyMentList(panMent)%>
                        </select>
                    </td>
                    <th align="right">
                        <span class="fontred">*</span>状态：
                    </th>
                    <td align="left">
                        <select name="SelStatus" class="inputselect">
                            <%=UtilsCommons.GetOperaterStatusList(Status)%>
                        </select>
                    </td>
                </tr>
            </table>
            <asp:PlaceHolder ID="phdShowList" runat="server">
                <h2>
                    <p>
                        已安排用餐</p>
                </h2>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01"
                    style="border-bottom: 1px solid #A9D7EC;" id="tabActionType">
                    <tr>
                        <th align="center" class="border-l">
                            餐馆名称
                        </th>
                        <th align="center">
                            用餐时间
                        </th>
                        <th align="center">
                            人数
                        </th>
                        <th align="center">
                            费用明细
                        </th>
                        <th align="right">
                            结算费用
                        </th>
                        <th align="center">
                            支付方式
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
                    <asp:Repeater ID="repDinnList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="border-l">
                                    <%# Eval("SourceName")%>
                                </td>
                                <td align="center">
                                    <%# UtilsCommons.GetDateString(Eval("StartDate"),ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%# Eval("Num")%>
                                </td>
                                <td align="center">
                                    <span title="<%#Eval("CostDetail") %>">
                                        <%# EyouSoft.Common.Utils.GetText(Eval("CostDetail").ToString(),30)%></span>
                                </td>
                                <td align="right">
                                    <%#UtilsCommons.GetMoneyString(Convert.ToDecimal(Eval("Confirmation")),ProviderToMoney)%>
                                    <a href="javascript:void(0);" data-class="Prepaid" title="预付申请" data-id="<%# Eval("PlanId") %>"
                                        data-soucesname="<%# Eval("SourceName")%>">
                                        <img src="/images/yufu.gif" /></a>
                                </td>
                                <td align="center">
                                    <%# Eval("PaymentType").ToString()%>
                                </td>
                                <td align="center">
                                    <%# Eval("Status").ToString()%>
                                </td>
                                <td align="center">
                                    <a href='<%#querenUrl %>?planId=<%# Eval("PlanId") %>' target="_blank">
                                        <img src="/images/y-kehuqueding.gif" /></a>
                                </td>
                                <td align="center">
                                    <%if (ListPower)
                                      { %>
                                    <a href="javascript:" data-class="updateDin">
                                        <img src="/images/y-delupdateicon.gif" data-id="<%# Eval("PlanId") %>" />
                                        修改</a> <a href="javascript:" data-class="deleteDin">
                                            <img src="/images/y-delicon.gif" data-id="<%# Eval("PlanId") %>" />
                                            删除</a>
                                    <%}
                                      else
                                      { %>
                                    <a href="javascript:" data-class="showDin">
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
        var DinPage = {
            sl: '<%=SL %>',
            type: '<%=Utils.GetQueryStringValue("Type") %>',
            tourId: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: '<%=Utils.GetQueryStringValue("iframeId") %>',
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _SaveDin: function(planId, tourId) {
                var _Url = '/OperaterCenter/OperaterDiningList.aspx?tourId=' + DinPage.tourId + '&sl=' + DinPage.sl + '&type=' + DinPage.type + '&iframeId=' + DinPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "POST",
                    url: '/OperaterCenter/OperaterDiningList.aspx?action=save&tourId=' + tourId + '&planId=' + planId + '&sl=' + DinPage.sl,
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
                                    DinPage._SaveDin(_planId, DinPage.tourId);
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
            _DeleteDin: function(_ObjID) {
                var _url = '/OperaterCenter/OperaterDiningList.aspx?tourId=' + DinPage.tourId + '&sl=' + DinPage.sl + '&type=' + DinPage.type + '&iframeId=' + DinPage.iframeId + "&m=" + new Date().getTime();
                $.newAjax({
                    type: "get",
                    url: '/OperaterCenter/OperaterDiningList.aspx?sl=' + DinPage.sl + '&action=delete&planId=' + _ObjID + '&tourid' + DinPage.tourId,
                    cache: false,
                    dataType: 'json',
                    success: function(data) {
                        if (data.result == "1") {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _url;
                            });
                            return false;
                        }
                        else {
                            parent.tableToolbar._showMsg(data.msg, function() {
                                window.location.href = _url;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            }, //价格计算
            _TotalPrices: function() {
                var totalprices = 0;
                var peopleNums = 0;
                $("input[type=text][name='txtPricesB']").each(function() {
                    var frequencyB = $(this).parent().parent(".tempRow").find("input[type=text][name='txtFrequencyB']").val();
                    var peoplenumsB = $(this).parent().parent(".tempRow").find("input[type=text][name='txtPeopleNumsB']").val();
                    var pricesB = $(this).parent().parent(".tempRow").find("input[type=text][name='txtPricesB']").val()

                    var frequencyL = $(this).parent().parent(".tempRow").find("input[type=text][name='txtFrequencyL']").val();
                    var peoplenumsL = $(this).parent().parent(".tempRow").find("input[type=text][name='txtPeopleNumsL']").val();
                    var pricesL = $(this).parent().parent(".tempRow").find("input[type=text][name='txtPricesL']").val();

                    var frequencyS = $(this).parent().parent(".tempRow").find("input[type=text][name='txtFrequencyS']").val();
                    var peoplenumsS = $(this).parent().parent(".tempRow").find("input[type=text][name='txtPeopleNumsS']").val();
                    var priceS = $(this).parent().parent(".tempRow").find("input[type=text][name='txtPricesS']").val();

                    var totalpricesB = tableToolbar.calculate(tableToolbar.calculate(frequencyB, peoplenumsB, "*"), pricesB, "*");
                    var totalpricesL = tableToolbar.calculate(tableToolbar.calculate(frequencyL, peoplenumsL, "*"), pricesL, "*");
                    var totalpricesS = tableToolbar.calculate(tableToolbar.calculate(frequencyS, peoplenumsS, "*"), priceS, "*");

                    totalprices += tableToolbar.calculate(tableToolbar.calculate(totalpricesB, totalpricesL, "+"), totalpricesS, "+");
                    peopleNums += tableToolbar.calculate(tableToolbar.calculate(peoplenumsB, peoplenumsL, "+"), peoplenumsS, "+");
                });
                $("#<%=txtTotalPrices.ClientID %>").val(totalprices);
                $("#<%=txtPeopleNum.ClientID %>").val(peopleNums);
            },
            _AjaxContectInfo: function(obj) {
                $("#<%=supplierControl1.ClientText %>").val(obj.name);
                $("#<%=supplierControl1.ClientValue %>").val(obj.id);
                $("#<%=txtContactName.ClientID %>").val(obj.contactname);
                $("#<%=txtContactPhone.ClientID %>").val(obj.contacttel);
                $("#<%=txtContactFax.ClientID %>").val(obj.contactfax);
            },
            _BindBtn: function() {
                $("#<%=txtDinningDate.ClientID %>").focus(function() { WdatePicker({ minDate: '%y-%M-#{%d' }); });

                $("#tabActionType").find("a[data-class='Prepaid']").unbind("click");
                $("#tabActionType").find("a[data-class='Prepaid']").click(function() {
                    var planId = $(this).attr("data-ID");
                    var soucesName = $(this).attr("data-soucesname");
                    DinPage._OpenBoxy("预付申请", '/OperaterCenter/PrepaidAppliaction.aspx?PlanId=' + planId + '&type=' + DinPage.type + '&sl=' + DinPage.sl + '&tourId=' + DinPage.tourId + '&souceName=' + escape(soucesName), "650px", "550px", true);
                    return false;
                });

                $("#tabActionType").find("[data-class='deleteDin']").unbind("click");
                $("#tabActionType").find("[data-class='deleteDin']").click(function() {
                    var newThis = $(this);
                    parent.tableToolbar.ShowConfirmMsg("确定删除此条数据吗?", function() {
                        var planId = newThis.find("img").attr("data-ID");
                        if (planId) {
                            DinPage._DeleteDin(planId);
                        }
                    });
                    return false;
                });

                $("#tabActionType").find("[data-class='updateDin']").unbind("click");
                $("#tabActionType").find("[data-class='updateDin']").click(function() {
                    var planID = $(this).find("img").attr("data-ID");
                    if (planID) {
                        window.location.href = '/OperaterCenter/OperaterDiningList.aspx?type=' + DinPage.type + '&sl=' + DinPage.sl + '&tourId=' + DinPage.tourId + '&action=update&planId=' + planID + '&iframeId=' + DinPage.iframeId;
                    }
                    return false;
                });


                $("#tabActionType").find("[data-class='showDin']").unbind("click");
                $("#tabActionType").find("[data-class='showDin']").click(function() {
                    var planID = $(this).find("img").attr("data-ID");
                    if (planID) {
                        window.location.href = '/OperaterCenter/OperaterDiningList.aspx?type=' + DinPage.type + '&sl=' + DinPage.sl + '&tourId=' + DinPage.tourId + '&action=update&planId=' + planID + '&iframeId=' + DinPage.iframeId + "&show=1";
                    }
                    return false;
                });

                $("#btnSave").unbind("click").text("保存").css("background-position", "0 0px");
                $("#btnSave").click(function() {
                    var form = $("#<%=formDin.ClientID %>").get(0);
                    if (!ValiDatorForm.validator(form, "parent")) {
                        return false;
                    } else {
                        $(this).text("保存中...").css("background-position", "0 -62px").unbind("click");
                        var planId = '<%=Utils.GetQueryStringValue("planId") %>';
                        DinPage._SaveDin(planId, DinPage.tourId);
                    }
                });

                $("input[type='text'][name='txtFrequencyB']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtPeopleNumsB']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtPricesB']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtFrequencyL']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtPeopleNumsL']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtPricesL']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtFrequencyS']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtPeopleNumsS']").blur(function() {
                    DinPage._TotalPrices();
                });
                $("input[type='text'][name='txtPricesS']").blur(function() {
                    DinPage._TotalPrices();
                });
            },
            _PageInit: function() {
                this._BindBtn();
                $("#tabDinPriceList").autoAdd({ addCallBack: parent.ConfigPage.SetWinHeight, delCallBack: parent.ConfigPage.SetWinHeight });

                if ('<%=Utils.GetQueryStringValue("show") %>' == "1") {
                    $("#ul_btn_list").parent("div").hide();
                }
            }
        }

        $(function() {
            DinPage._PageInit();
            parent.ConfigPage.SetWinHeight();
        });
    </script>

</body>
</html>
